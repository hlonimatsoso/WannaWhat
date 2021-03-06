using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using IdentityServer4.Configuration;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using WannaWhat.Server.Data;
using WannaWhat.Shared.Models;
using System.Reflection;
using System;
using WannaWhat.Server.Interfaces;
using WannaWhat.Server.Services;
using IdentityServer4.Services;

namespace WannaWhat.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string ConnectionString => Configuration.GetConnectionString("DefaultConnection");


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WannaWhatDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<WannaWhatUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<WannaWhatDbContext>();

            //services.AddIdentityServer()
            //    .AddApiAuthorization<WannaWhatUser, WannaWhatDbContext>();


            var migrationsAssembly = typeof(WannaWhatDbContext).GetTypeInfo().Assembly.GetName().Name;

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.UserInteraction.LoginUrl = "Identity/Account/Login";
                options.UserInteraction.LogoutUrl = "Identity/Account/Logout";
                options.Authentication = new IdentityServer4.Configuration.AuthenticationOptions()
                {
                    CookieLifetime = TimeSpan.FromHours(10), // ID server cookie timeout set to 10 hours
                    CookieSlidingExpiration = true
                };
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(ConnectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(ConnectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                options.EnableTokenCleanup = true;
            })
            //.AddAspNetIdentity<WannaWhatUser>()
            .AddApiAuthorization<WannaWhatUser, WannaWhatDbContext>();

            builder.AddDeveloperSigningCredential();

            //services.AddAuthentication()
            //    .AddIdentityServerJwt()
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookie";
                options.DefaultChallengeScheme = "oidc";
            }).AddCookie("cookie")
             .AddIdentityServerJwt()
          .AddOpenIdConnect("oidc", options =>
          {
              options.Authority = "https://localhost:5001";
              options.RequireHttpsMetadata = true;

              options.ClientId = "blazor";
              options.ClientSecret = "blazor_secret";
              options.ResponseType = "code";
              options.UsePkce = true;

              options.Scope.Clear();
              options.Scope.Add("openid");
              options.Scope.Add("profile");
              options.Scope.Add("custom");
              options.Scope.Add("offline_access");
              options.Scope.Add("email");
              options.Scope.Add("phone");
              options.Scope.Add("location");
              options.Scope.Add("companyDetails");
              options.Scope.Add("api1");
              options.Scope.Add("api1.admin");

              // not mapped by default
              options.ClaimActions.MapJsonKey("website", "website");

              options.ClaimActions.MapUniqueJsonKey("MappedGender", "gender");
              options.ClaimActions.MapUniqueJsonKey("MappedDOB", "dob");

              // keeps id_token smaller
              options.GetClaimsFromUserInfoEndpoint = true;
              options.SaveTokens = true;

              options.SignInScheme = "cookie";

              options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
              {
                  NameClaimType = "Name"
              };

              // not mapped by default
              options.ClaimActions.MapJsonKey("gender", "gender");
          })
            .AddGoogle(options =>
            {
                options.ClientId = "923984788102-5co1eqq3ehl6ju5qss1pp0jjg9vjao8v.apps.googleusercontent.com";
                options.ClientSecret = "A5JU6Ms43lpAsGjProvR3s9G";

            }).AddFacebook(options =>
            {
                options.ClientId = "sadddad";
                options.ClientSecret = "asdsd";

            }).AddTwitter(options =>
            {
                options.ConsumerKey = "sadddad";
                options.ConsumerSecret = "asdsd";

            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<IRegistrationHelper, RegistrationService>();
            services.AddScoped<IProfileService, WannaWhatProfileService>();


            //services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days.  You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapBlazorHub();
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                //endpoints.MapFallbackToFile("/_Host");

            });

            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
        }
    }
}

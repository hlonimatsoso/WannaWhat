using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

using WannaWhat.Shared.Models;

namespace WannaWhat.Server.Data
{
    public class WannaWhatDbContext : ApiAuthorizationDbContext<WannaWhatUser>
    {
        public WannaWhatDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<UserPersonality> UserPersonalities { get; set; }

        public DbSet<UserInterest> UserInterests { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WannaWhatUser>()
                   .HasOne(u => u.UserInfo)
                   .WithOne(x => x.User);

            builder.Entity<UserInterest>().HasKey(ui => new { ui.UserId, ui.InterestId });

            builder.Entity<UserPersonality>().HasKey(ui => new { ui.UserId, ui.PersonalityId });

            builder.Entity<UserInterest>()
                .HasOne<WannaWhatUser>(sc => sc.User)
                .WithMany(s => s.UserInerests)
                .HasForeignKey(sc => sc.UserId);


            builder.Entity<UserInterest>()
                .HasOne<Interest>(sc => sc.Interest)
                .WithMany(s => s.UserInterests)
                .HasForeignKey(sc => sc.InterestId);

            builder.Entity<UserPersonality>()
                .HasOne<WannaWhatUser>(sc => sc.User)
                .WithMany(s => s.UserPersonalities)
                .HasForeignKey(sc => sc.UserId);


            builder.Entity<UserPersonality>()
                .HasOne<Personality>(sc => sc.Personality)
                .WithMany(s => s.UserPesonalities)
                .HasForeignKey(sc => sc.PersonalityId);

            base.OnModelCreating(builder);
        }
    }
}

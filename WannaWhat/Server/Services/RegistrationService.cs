using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.Server.Data;
using WannaWhat.Server.Interfaces;
using WannaWhat.Shared.Models;

namespace WannaWhat.Server.Services
{
    public class RegistrationService : IRegistrationHelper
    {

        public WannaWhatDbContext DbContext { get; set; }


        public RegistrationService(WannaWhatDbContext cxt)
        {
            DbContext = cxt;
        }

        public void InsertUser(UserInfo info)
        {
            DbContext.UserInfo.Add(info);
            DbContext.SaveChanges();
        }
    }
}

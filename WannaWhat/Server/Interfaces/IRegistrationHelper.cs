using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaWhat.Shared.Models;

namespace WannaWhat.Server.Interfaces
{
    public interface IRegistrationHelper
    {
        void InsertUser(UserInfo info);
        UserInfo GetUserInfo(string id);
    }
}

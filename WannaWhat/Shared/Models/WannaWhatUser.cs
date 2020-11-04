using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace WannaWhat.Shared.Models
{
    public class WannaWhatUser : IdentityUser
    {

        public bool IsActive { get; set; }

        public UserInfo UserInfo { get; set; }

        public List<UserInterest> UserInerests { get; set; }

        public List<UserPersonality> UserPersonalities { get; set; }

    }
}

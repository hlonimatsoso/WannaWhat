using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WannaWhat.Shared.Models
{
    public class WannaWhatUser : IdentityUser
    {

        public UserInfo UserInfo { get; set; }
        
        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WannaWhat.Shared.Models
{
    public class UserInfo
    {
        [Key]
        public int UserInfoId { get; set; }

        public DateTime DOB { get; set; }

        public char Gender { get; set; }

        public byte Age { get; set; }

        public BodyType BodyType { get; set; }

        public byte Height { get; set; }

        public EyeColor EyeColor { get; set; }

        public HairColor HairColor { get; set; }

        [ForeignKey("UserId")]
        public WannaWhatUser User { get; set; }

        public string UserId { get; set; }


    }
}

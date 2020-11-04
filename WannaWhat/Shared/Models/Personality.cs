using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WannaWhat.Shared.Models
{
   public class Personality
    {
        [Key]
        public string PersonalityId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string PersonalityName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; }

        public List<UserPersonality> UserPesonalities { get; set; }

    }
}

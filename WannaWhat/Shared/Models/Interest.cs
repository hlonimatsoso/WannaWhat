using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WannaWhat.Shared.Models
{
   public class Interest
    {
        [Key]
        public string InterestId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; }

        public List<UserInterest> UserInterests { get; set; }
    }
}

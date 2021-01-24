using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("usr_business")]
    public partial class usr_business
    {
        [Key]
        public long id_user_business { get; set; }
        [Required]
        [StringLength(20)]
        public string usr_business_name { get; set; }
        [Required]
        [StringLength(50)]
        public string usr_business_description { get; set; }
        [Required]
        public string usr_business_identification { get; set; }

        [ForeignKey(nameof(id_user_business))]
        [InverseProperty(nameof(usr_user.usr_business))]
        public virtual usr_user id_user_businessNavigation { get; set; }
    }
}

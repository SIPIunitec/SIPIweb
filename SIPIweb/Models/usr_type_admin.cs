using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("usr_type_admin")]
    public partial class usr_type_admin
    {
        public usr_type_admin()
        {
            usr_users = new HashSet<usr_user>();
        }

        [Key]
        public long id_user_type { get; set; }
        [Required]
        [StringLength(50)]
        public string usr_type_name { get; set; }
        [Required]
        [StringLength(500)]
        public string usr_type_description { get; set; }
        [Required]
        public string Discriminator { get; set; }

        [InverseProperty(nameof(usr_user.id_user_typeNavigation))]
        public virtual ICollection<usr_user> usr_users { get; set; }
    }
}

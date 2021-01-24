using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("usr_resource_usage")]
    public partial class usr_resource_usage
    {
        public usr_resource_usage()
        {
            usr_resource_usage_users = new HashSet<usr_resource_usage_user>();
        }

        [Key]
        public long id_resource_use { get; set; }
        [StringLength(50)]
        public string usr_resource_description { get; set; }

        [InverseProperty(nameof(usr_resource_usage_user.id_resource_useNavigation))]
        public virtual ICollection<usr_resource_usage_user> usr_resource_usage_users { get; set; }
    }
}

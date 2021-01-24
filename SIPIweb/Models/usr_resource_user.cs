using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("usr_resource_user")]
    public partial class usr_resource_user
    {
        public usr_resource_user()
        {
            usr_resource_usage_users = new HashSet<usr_resource_usage_user>();
        }

        [Key]
        public long id_user_app_user { get; set; }
        public long id_resource { get; set; }
        public long id_user { get; set; }
        [Required]
        [StringLength(150)]
        public string resource_user_identification { get; set; }
        public string resource_userPrimary_value { get; set; }
        public int? resource_probablyUsed { get; set; }

        [ForeignKey(nameof(id_resource))]
        [InverseProperty(nameof(res_resource.usr_resource_users))]
        public virtual res_resource id_resourceNavigation { get; set; }
        [ForeignKey(nameof(id_user))]
        [InverseProperty(nameof(usr_user.usr_resource_users))]
        public virtual usr_user id_userNavigation { get; set; }
        [InverseProperty(nameof(usr_resource_usage_user.id_user_app_userNavigation))]
        public virtual ICollection<usr_resource_usage_user> usr_resource_usage_users { get; set; }
    }
}

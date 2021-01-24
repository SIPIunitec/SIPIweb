using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("usr_resource_usage_user")]
    public partial class usr_resource_usage_user
    {
        [Key]
        public long id_resource_usage_user { get; set; }
        public long id_resource_use { get; set; }
        public long id_user_app_user { get; set; }

        [ForeignKey(nameof(id_resource_use))]
        [InverseProperty(nameof(usr_resource_usage.usr_resource_usage_users))]
        public virtual usr_resource_usage id_resource_useNavigation { get; set; }
        [ForeignKey(nameof(id_user_app_user))]
        [InverseProperty(nameof(usr_resource_user.usr_resource_usage_users))]
        public virtual usr_resource_user id_user_app_userNavigation { get; set; }
    }
}

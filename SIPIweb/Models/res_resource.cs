using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("res_resource")]
    [Index(nameof(id_resource_type), Name = "IX_res_resource_id_resource_type")]
    public partial class res_resource
    {
        public res_resource()
        {
            usr_resource_users = new HashSet<usr_resource_user>();
        }

        [Key]
        public long id_resource { get; set; }
        [Required]
        [StringLength(50)]
        public string res_name { get; set; }
        public long id_resource_type { get; set; }
        public int resource_code { get; set; }
        public bool? resource_enable { get; set; }

        [ForeignKey(nameof(id_resource_type))]
        [InverseProperty(nameof(res_type_admin.res_resources))]
        public virtual res_type_admin id_resource_typeNavigation { get; set; }
        [InverseProperty(nameof(usr_resource_user.id_resourceNavigation))]
        public virtual ICollection<usr_resource_user> usr_resource_users { get; set; }
    }
}

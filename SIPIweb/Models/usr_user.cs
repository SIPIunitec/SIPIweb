using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("usr_user")]
    [Index(nameof(id_user_type), Name = "IX_user_id_user_type")]
    public partial class usr_user
    {
        public usr_user()
        {
            usr_resource_users = new HashSet<usr_resource_user>();
        }

        [Key]
        public long id_user { get; set; }
        [Required]
        [StringLength(25)]
        public string user_login { get; set; }
        [Required]
        [StringLength(15)]
        public string user_pass { get; set; }
        [Required]
        [StringLength(50)]
        public string user_email { get; set; }
        [Column(TypeName = "date")]
        public DateTime user_birthday { get; set; }
        [Column(TypeName = "date")]
        public DateTime user_createdday { get; set; }
        public long id_user_type { get; set; }
        public long id_city_localization { get; set; }
        public bool user_ready { get; set; }

        [ForeignKey(nameof(id_city_localization))]
        [InverseProperty(nameof(res_geo_city_admin.usr_users))]
        public virtual res_geo_city_admin id_city_localizationNavigation { get; set; }
        [ForeignKey(nameof(id_user_type))]
        [InverseProperty(nameof(usr_type_admin.usr_users))]
        public virtual usr_type_admin id_user_typeNavigation { get; set; }
        [InverseProperty("id_user_businessNavigation")]
        public virtual usr_business usr_business { get; set; }
        [InverseProperty("id_user_personNavigation")]
        public virtual usr_person usr_person { get; set; }
        [InverseProperty(nameof(usr_resource_user.id_userNavigation))]
        public virtual ICollection<usr_resource_user> usr_resource_users { get; set; }
    }
}

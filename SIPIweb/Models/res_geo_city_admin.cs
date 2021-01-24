using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("res_geo_city_admin")]
    [Index(nameof(id_state), Name = "IX_geo_city_id_geo_state")]
    public partial class res_geo_city_admin
    {
        public res_geo_city_admin()
        {
            usr_users = new HashSet<usr_user>();
        }

        [Key]
        public long id_city { get; set; }
        public long id_state { get; set; }
        [Required]
        [StringLength(50)]
        public string city_name { get; set; }

        [ForeignKey(nameof(id_state))]
        [InverseProperty(nameof(res_geo_state_admin.res_geo_city_admins))]
        public virtual res_geo_state_admin id_stateNavigation { get; set; }
        [InverseProperty(nameof(usr_user.id_city_localizationNavigation))]
        public virtual ICollection<usr_user> usr_users { get; set; }
    }
}

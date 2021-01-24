using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("res_geo_state_admin")]
    [Index(nameof(id_country), Name = "IX_geo_state_id_geo_country")]
    public partial class res_geo_state_admin
    {
        public res_geo_state_admin()
        {
            res_geo_city_admins = new HashSet<res_geo_city_admin>();
        }

        [Key]
        public long id_state { get; set; }
        public long id_country { get; set; }
        [Required]
        [StringLength(50)]
        public string state_name { get; set; }

        [ForeignKey(nameof(id_country))]
        [InverseProperty(nameof(res_geo_country_admin.res_geo_state_admins))]
        public virtual res_geo_country_admin id_countryNavigation { get; set; }
        [InverseProperty(nameof(res_geo_city_admin.id_stateNavigation))]
        public virtual ICollection<res_geo_city_admin> res_geo_city_admins { get; set; }
    }
}

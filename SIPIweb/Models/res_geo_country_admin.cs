using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("res_geo_country_admin")]
    public partial class res_geo_country_admin
    {
        public res_geo_country_admin()
        {
            res_geo_state_admins = new HashSet<res_geo_state_admin>();
        }

        [Key]
        public long id_country { get; set; }
        [Required]
        [StringLength(50)]
        public string country_name { get; set; }
        public int country_cod { get; set; }

        [InverseProperty(nameof(res_geo_state_admin.id_countryNavigation))]
        public virtual ICollection<res_geo_state_admin> res_geo_state_admins { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Keyless]
    public partial class view_Resource
    {
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
        public long Expr1 { get; set; }
        [Required]
        [StringLength(150)]
        public string resource_user_identification { get; set; }
        public string resource_userPrimary_value { get; set; }
        public int? resource_probablyUsed { get; set; }
        [StringLength(50)]
        public string usr_resource_description { get; set; }
        [Required]
        [StringLength(50)]
        public string res_name { get; set; }
        [Required]
        [StringLength(50)]
        public string res_type_description { get; set; }
        [Required]
        [StringLength(50)]
        public string res_type_name { get; set; }
        public long id_user_app_user { get; set; }
        public long id_resource_usage_user { get; set; }
        public long id_resource { get; set; }
        public long id_resource_use { get; set; }
        public long id_resource_type { get; set; }
    }
}

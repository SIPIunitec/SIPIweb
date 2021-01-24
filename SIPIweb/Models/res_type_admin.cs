using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("res_type_admin")]
    public partial class res_type_admin
    {
        public res_type_admin()
        {
            res_resources = new HashSet<res_resource>();
        }

        [Key]
        public long id_resource_type { get; set; }
        [Required]
        [StringLength(50)]
        public string res_type_name { get; set; }
        [Required]
        [StringLength(100)]
        public string res_type_description { get; set; }

        [InverseProperty(nameof(res_resource.id_resource_typeNavigation))]
        public virtual ICollection<res_resource> res_resources { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("usr_person")]
    public partial class usr_person
    {
        [Key]
        public long id_user_person { get; set; }
        [Required]
        [StringLength(50)]
        public string usr_person_firstname { get; set; }
        [Required]
        [StringLength(50)]
        public string usr_person_lastname { get; set; }
        public long? id_country_primary { get; set; }
        [StringLength(10)]
        public string document_prefix_Primary { get; set; }
        [StringLength(15)]
        public string document_country_primary { get; set; }
        public long? id_country_secundary { get; set; }
        [StringLength(15)]
        public string document_country_secundary { get; set; }
        [StringLength(50)]
        public string blood_type { get; set; }
        [StringLength(50)]
        public string gender { get; set; }
        public bool? person_ready { get; set; }

        [ForeignKey(nameof(id_user_person))]
        [InverseProperty(nameof(usr_user.usr_person))]
        public virtual usr_user id_user_personNavigation { get; set; }
    }
}

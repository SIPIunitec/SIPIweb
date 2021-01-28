using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("tbl_usuarioPersona_tmp")]
    public partial class tbl_usuarioPersona_tmp
    {
        [Key]
        public long id_persona_tmp { get; set; }
        [Required]
        [StringLength(50)]
        public string persona_nombres { get; set; }
        [StringLength(50)]
        public string persona_apellidos { get; set; }
        [StringLength(25)]
        public string persona_login { get; set; }
        [StringLength(50)]
        public string persona_email { get; set; }
        [StringLength(50)]
        public string persona_origen { get; set; }
        [Column(TypeName = "date")]
        public DateTime? persona_createdDay { get; set; }
        public string Observaciones { get; set; }
        public bool Estatus { get; set; }
    }
}

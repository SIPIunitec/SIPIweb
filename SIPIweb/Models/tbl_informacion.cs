using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("tbl_informacion")]
    [Index(nameof(id_usuario), Name = "IX_informacion_id_usuario")]
    public partial class tbl_informacion
    {
        [Key]
        public long id_informacion { get; set; }
        [Required]
        [StringLength(150)]
        public string informacion_titulo { get; set; }
        [Required]
        [StringLength(500)]
        public string informacion_cuerpo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime informacion_fechaPublicacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? informacion_fechaLimite { get; set; }
        public long id_usuario { get; set; }

        [ForeignKey(nameof(id_usuario))]
        [InverseProperty(nameof(tbl_usuario.tbl_informacions))]
        public virtual tbl_usuario id_usuarioNavigation { get; set; }
    }
}

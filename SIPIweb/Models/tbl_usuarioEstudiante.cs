using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("tbl_usuarioEstudiante")]
    public partial class tbl_usuarioEstudiante
    {
        [Key]
        public long id_usuarioEstudiante { get; set; }
        [Column(TypeName = "date")]
        public DateTime usuarioEstudiante_ingreso { get; set; }

        [ForeignKey(nameof(id_usuarioEstudiante))]
        [InverseProperty(nameof(tbl_usuario.tbl_usuarioEstudiante))]
        public virtual tbl_usuario id_usuarioEstudianteNavigation { get; set; }
    }
}

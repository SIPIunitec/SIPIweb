using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Keyless]
    [Table("tbl_historico")]
    public partial class tbl_historico
    {
        public int id_historia { get; set; }
        public int id_usuario { get; set; }
        [Column(TypeName = "date")]
        public DateTime fecha_accion { get; set; }
        [Required]
        [StringLength(50)]
        public string actividad { get; set; }
    }
}

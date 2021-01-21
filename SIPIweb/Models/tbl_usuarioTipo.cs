using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("tbl_usuarioTipo")]
    public partial class tbl_usuarioTipo
    {
        public tbl_usuarioTipo()
        {
            tbl_usuarios = new HashSet<tbl_usuario>();
        }

        [Key]
        public int id_usuarioTipo { get; set; }
        [Required]
        [StringLength(150)]
        public string usuarioTipo_nombre { get; set; }

        [InverseProperty(nameof(tbl_usuario.id_usuarioTipoNavigation))]
        public virtual ICollection<tbl_usuario> tbl_usuarios { get; set; }
    }
}

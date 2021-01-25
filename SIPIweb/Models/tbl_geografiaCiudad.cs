using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("tbl_geografiaCiudad")]
    public partial class tbl_geografiaCiudad
    {
        public tbl_geografiaCiudad()
        {
            tbl_usuarioPersonaid_ciudad_nacimientoNavigations = new HashSet<tbl_usuarioPersona>();
            tbl_usuarioPersonaid_ciudad_ubicacionNavigations = new HashSet<tbl_usuarioPersona>();
        }

        [Key]
        public long id_ciudad { get; set; }
        public long id_estado { get; set; }
        [Required]
        [StringLength(100)]
        public string ciudad_nombre { get; set; }

        [ForeignKey(nameof(id_estado))]
        [InverseProperty(nameof(tbl_geografiaEstado.tbl_geografiaCiudads))]
        public virtual tbl_geografiaEstado id_estadoNavigation { get; set; }
        [InverseProperty(nameof(tbl_usuarioPersona.id_ciudad_nacimientoNavigation))]
        public virtual ICollection<tbl_usuarioPersona> tbl_usuarioPersonaid_ciudad_nacimientoNavigations { get; set; }
        [InverseProperty(nameof(tbl_usuarioPersona.id_ciudad_ubicacionNavigation))]
        public virtual ICollection<tbl_usuarioPersona> tbl_usuarioPersonaid_ciudad_ubicacionNavigations { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("tbl_usuarioPersona")]
    public partial class tbl_usuarioPersona
    {
        public tbl_usuarioPersona()
        {
            tbl_usuarioAsignaRols = new HashSet<tbl_usuarioAsignaRol>();
        }

        [Key]
        public long id_persona { get; set; }
        [Required]
        [StringLength(50)]
        public string persona_nombres { get; set; }
        [StringLength(50)]
        public string persona_apellidos { get; set; }
        [StringLength(102)]
        public string persona_nombreCompleto { get; set; }
        [Column(TypeName = "date")]
        public DateTime? persona_nacimiento { get; set; }
        [StringLength(15)]
        public string persona_sangre { get; set; }
        public long? id_ciudad_nacimiento { get; set; }
        public long? id_ciudad_ubicacion { get; set; }
        [Column(TypeName = "date")]
        public DateTime? persona_createdDay { get; set; }
        [Column(TypeName = "date")]
        public DateTime? persona_actualizacionDay { get; set; }

        [ForeignKey(nameof(id_ciudad_nacimiento))]
        [InverseProperty(nameof(tbl_geografiaCiudad.tbl_usuarioPersonaid_ciudad_nacimientoNavigations))]
        public virtual tbl_geografiaCiudad id_ciudad_nacimientoNavigation { get; set; }
        [ForeignKey(nameof(id_ciudad_ubicacion))]
        [InverseProperty(nameof(tbl_geografiaCiudad.tbl_usuarioPersonaid_ciudad_ubicacionNavigations))]
        public virtual tbl_geografiaCiudad id_ciudad_ubicacionNavigation { get; set; }
        [ForeignKey(nameof(id_persona))]
        [InverseProperty(nameof(tbl_usuario.tbl_usuarioPersona))]
        public virtual tbl_usuario id_personaNavigation { get; set; }
        [InverseProperty(nameof(tbl_usuarioAsignaRol.id_personaNavigation))]
        public virtual ICollection<tbl_usuarioAsignaRol> tbl_usuarioAsignaRols { get; set; }
    }
}

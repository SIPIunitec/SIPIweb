using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("tbl_usuarioAsignaRol")]
    public partial class tbl_usuarioAsignaRol
    {
        [Key]
        public long id_persona { get; set; }
        [Key]
        public int id_usuarioRoles { get; set; }
        [Column(TypeName = "date")]
        public DateTime AsignaRolFecha { get; set; }

        [ForeignKey(nameof(id_persona))]
        [InverseProperty(nameof(tbl_usuarioPersona.tbl_usuarioAsignaRols))]
        public virtual tbl_usuarioPersona id_personaNavigation { get; set; }
        [ForeignKey(nameof(id_usuarioRoles))]
        [InverseProperty(nameof(tbl_usuarioRole.tbl_usuarioAsignaRols))]
        public virtual tbl_usuarioRole id_usuarioRolesNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    public partial class tbl_usuarioRole
    {
        public tbl_usuarioRole()
        {
            tbl_usuarioAsignaRols = new HashSet<tbl_usuarioAsignaRol>();
        }

        [Key]
        public int id_usuarioRoles { get; set; }
        [Required]
        [StringLength(50)]
        public string usuarioRol_nombre { get; set; }

        [InverseProperty(nameof(tbl_usuarioAsignaRol.id_usuarioRolesNavigation))]
        public virtual ICollection<tbl_usuarioAsignaRol> tbl_usuarioAsignaRols { get; set; }
    }
}

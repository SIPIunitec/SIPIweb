using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("tbl_usuario")]
    public partial class tbl_usuario
    {
        public tbl_usuario()
        {
            tbl_informacions = new HashSet<tbl_informacion>();
        }

        [Key]
        public long id_usuario { get; set; }
        public int id_usuarioTipo { get; set; }
        [Required]
        [StringLength(25)]
        public string usuario_login { get; set; }
        [Required]
        [StringLength(15)]
        public string usuario_pass { get; set; }
        [Required]
        [StringLength(50)]
        public string usuario_email { get; set; }
        [Column(TypeName = "date")]
        public DateTime usuario_createdDay { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario_origen { get; set; }

        [ForeignKey(nameof(id_usuarioTipo))]
        [InverseProperty(nameof(tbl_usuarioTipo.tbl_usuarios))]
        public virtual tbl_usuarioTipo id_usuarioTipoNavigation { get; set; }
        [InverseProperty("id_personaNavigation")]
        public virtual tbl_usuarioPersona tbl_usuarioPersona { get; set; }
        [InverseProperty(nameof(tbl_informacion.id_usuarioNavigation))]
        public virtual ICollection<tbl_informacion> tbl_informacions { get; set; }
    }
}

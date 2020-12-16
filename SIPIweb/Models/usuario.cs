using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("usuario")]
    public partial class usuario
    {
        public usuario()
        {
            informacions = new HashSet<informacion>();
        }

        [Key]
        public long id_usuario { get; set; }
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
        public DateTime usuario_birthday { get; set; }
        [Column(TypeName = "date")]
        public DateTime usuario_createdday { get; set; }
        public int? usuario_tipo { get; set; }

        [InverseProperty(nameof(informacion.id_usuarioNavigation))]
        public virtual ICollection<informacion> informacions { get; set; }
    }
}

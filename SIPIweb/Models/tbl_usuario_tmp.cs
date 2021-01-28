using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("tbl_usuario_tmp")]
    public partial class tbl_usuario_tmp
    {
        [Key]
        public long id_usuario_tmp { get; set; }
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

        [StringLength(50)]
        public string usuario_origen { get; set; }
        public string Observaciones { get; set; }
        public bool Estatus { get; set; }
    }
}

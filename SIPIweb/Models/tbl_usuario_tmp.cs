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

        [Required]
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

        [Display(Name = "Falla de Carga")]
        public bool Estatus { get; set; }

        [StringLength(200)]
        public string Observaciones { get; set; }
    }
}

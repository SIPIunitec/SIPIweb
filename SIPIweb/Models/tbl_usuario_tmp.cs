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

        [Display(Name = "Tipo de Usuario")]
        public int id_usuarioTipo { get; set; }

        [Required]
        [StringLength(25), Display(Name = "Nombre de Usuario")]
        public string usuario_login { get; set; }

        [Required]
        [StringLength(15), Display(Name = "Clave de Usuario"), DataType(DataType.Password)]
        public string usuario_pass { get; set; }

        [Required]
        [StringLength(50), Display(Name = "Correo principal de Usuario"), DataType(DataType.EmailAddress)]
        public string usuario_email { get; set; }


        [Display(Name = "Fecha Creación de Usuario"), DataType(DataType.Date)]
        public DateTime usuario_createdDay { get; set; }

        [Display(Name = "Errores en Migración"), DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }

        [Display(Name = "Error en Migración")]
        public bool Estatus { get; set; }

        [StringLength(50), Display(Name = "Origen de los datos")]
        public string usuario_Origen { get; set; }
    }
}

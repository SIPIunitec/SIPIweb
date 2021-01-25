using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIPIweb.Models
{
    public class Metadata
    {
        public  class tbl_usuarioValidadores
        {
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

            [Required]
            [StringLength(50), Display(Name = "Origen de los datos")]
            public string usuario_origen { get; set; }

            [ForeignKey(nameof(id_usuarioTipo))]
            [InverseProperty(nameof(tbl_usuarioTipo.tbl_usuarios)), Display(Name = "Tipo de Usuario")]
            public virtual tbl_usuarioTipo id_usuarioTipoNavigation { get; set; }

            [InverseProperty("id_personaNavigation")]
            public virtual tbl_usuarioPersona tbl_usuarioPersona { get; set; }

            [InverseProperty(nameof(tbl_informacion.id_usuarioNavigation))]
            public virtual ICollection<tbl_informacion> tbl_informacions { get; set; }
        }

        public class tbl_usuarioValidadores_tmp
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

            [Required]
            [StringLength(50), Display(Name = "Origen de los datos")]
            public string usuario_origen { get; set; }
        }

    }
}

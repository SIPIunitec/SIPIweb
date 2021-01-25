using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    [Table("tbl_geografiaEstado")]
    public partial class tbl_geografiaEstado
    {
        public tbl_geografiaEstado()
        {
            tbl_geografiaCiudads = new HashSet<tbl_geografiaCiudad>();
        }

        [Key]
        public long id_estado { get; set; }
        public long id_pais { get; set; }
        [Required]
        [StringLength(100)]
        public string estado_nombre { get; set; }

        [ForeignKey(nameof(id_pais))]
        [InverseProperty(nameof(tbl_geografiaPai.tbl_geografiaEstados))]
        public virtual tbl_geografiaPai id_paisNavigation { get; set; }
        [InverseProperty(nameof(tbl_geografiaCiudad.id_estadoNavigation))]
        public virtual ICollection<tbl_geografiaCiudad> tbl_geografiaCiudads { get; set; }
    }
}

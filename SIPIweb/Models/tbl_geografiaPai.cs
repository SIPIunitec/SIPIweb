using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SIPIweb.Models
{
    public partial class tbl_geografiaPai
    {
        [Key]
        public long id_pais { get; set; }
        [Required]
        [StringLength(100)]
        public string pais_nombre { get; set; }
        [Required]
        [StringLength(3)]
        public string pais_codigoTelefono { get; set; }
    }
}

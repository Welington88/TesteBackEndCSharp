using System;
using System.ComponentModel.DataAnnotations;

namespace TesteBackEndCSharp.Models
{
	public class Money
	{
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public String? Moeda { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime data_inicio { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime data_fim { get; set; }
    }
}


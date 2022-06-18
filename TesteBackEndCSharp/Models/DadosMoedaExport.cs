using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteBackEndCSharp.Models
{
	[NotMapped]
	public class DadosMoedaExport
	{
		public String? ID_MOEDA { get; set; }

		public DateTime DATA_REF { get; set; }

        public String? VL_COTACAO { get; set; }
    }
}


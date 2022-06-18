using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteBackEndCSharp.Models
{
    [NotMapped]
    public class DadosMoeda
	{
        public String? ID_MOEDA { get; set; }

        public DateTime DATA_REF { get; set; }

    }
}


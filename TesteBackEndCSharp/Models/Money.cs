using System;
namespace TesteBackEndCSharp.Models
{
	public class Money
	{
        public int id { get; set; }

        public String? Moeda { get; set; }

        public DateTime data_inicio { get; set; }

        public DateTime data_fim { get; set; }
    }
}


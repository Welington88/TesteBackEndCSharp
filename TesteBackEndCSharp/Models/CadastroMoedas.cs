using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TesteBackEndCSharp.Models
{
    [NotMapped]
    public class CadastroMoedas
    {
        public CadastroMoedas()
        {
        }

        public CadastroMoedas(String Moeda, int Codigo)
        {
            this.Moeda = Moeda.Trim();
            this.Codigo = Codigo;
        }

        public String? Moeda { get; set; }

        public int Codigo { get; set; }

        public List<CadastroMoedas> listaMoedas { get; set; }

        public CadastroMoedas getListaMoedas(string moeda) {

            var cadastroMoeda = getListaMoedas().Where(m => m.Moeda == moeda).FirstOrDefault();
            if (cadastroMoeda is null)
            {
                throw new Exception("Moeda não encontrada");
            }
            return cadastroMoeda;
        }

        public CadastroMoedas getListaMoedas(int codigo)
        {

            var cadastroMoeda = getListaMoedas().Where(m => m.Codigo == codigo).FirstOrDefault();
            if (cadastroMoeda is null)
            {
                throw new Exception("Moeda não encontrada");
            }
            return cadastroMoeda;
        }

        public List<CadastroMoedas> getListaMoedas()
        {
            var lista = new List<CadastroMoedas>() {
                new CadastroMoedas("AFN ",66),
                new CadastroMoedas("ALL ",49),
                new CadastroMoedas("ANG ",33),
                new CadastroMoedas("ARS ",3),
                new CadastroMoedas("AWG ",6),
                new CadastroMoedas("BOB ",56),
                new CadastroMoedas("BYN ",64),
                new CadastroMoedas("CAD ",25),
                new CadastroMoedas("CDF ",58),
                new CadastroMoedas("CLP ",16),
                new CadastroMoedas("COP ",37),
                new CadastroMoedas("CRC ",52),
                new CadastroMoedas("CUP ",8),
                new CadastroMoedas("CVE ",51),
                new CadastroMoedas("CZK ",29),
                new CadastroMoedas("DJF ",36),
                new CadastroMoedas("DZD ",54),
                new CadastroMoedas("EGP ",12),
                new CadastroMoedas("EUR ",20),
                new CadastroMoedas("FJD ",38),
                new CadastroMoedas("GBP ",22),
                new CadastroMoedas("GEL ",48),
                new CadastroMoedas("GIP ",18),
                new CadastroMoedas("HTG ",63),
                new CadastroMoedas("ILS ",40),
                new CadastroMoedas("IRR ",17),
                new CadastroMoedas("ISK ",11),
                new CadastroMoedas("JPY ",9),
                new CadastroMoedas("KES ",21),
                new CadastroMoedas("KMF ",19),
                new CadastroMoedas("LBP ",42),
                new CadastroMoedas("LSL ",4),
                new CadastroMoedas("MGA ",35),
                new CadastroMoedas("MGB ",26),
                new CadastroMoedas("MMK ",69),
                new CadastroMoedas("MRO ",53),
                new CadastroMoedas("MRU ",15),
                new CadastroMoedas("MUR ",7),
                new CadastroMoedas("MXN ",41),
                new CadastroMoedas("MZN ",43),
                new CadastroMoedas("NIO ",23),
                new CadastroMoedas("NOK ",62),
                new CadastroMoedas("OMR ",34),
                new CadastroMoedas("PEN ",45),
                new CadastroMoedas("PGK ",2),
                new CadastroMoedas("PHP ",24),
                new CadastroMoedas("RON ",5),
                new CadastroMoedas("SAR ",44),
                new CadastroMoedas("SBD ",32),
                new CadastroMoedas("SGD ",70),
                new CadastroMoedas("SLL ",10),
                new CadastroMoedas("SOS ",61),
                new CadastroMoedas("SSP ",47),
                new CadastroMoedas("SZL ",55),
                new CadastroMoedas("THB ",39),
                new CadastroMoedas("TRY ",13),
                new CadastroMoedas("TTD ",67),
                new CadastroMoedas("UGX ",59),
                new CadastroMoedas("USD ",1),
                new CadastroMoedas("UYU ",46),
                new CadastroMoedas("VES ",68),
                new CadastroMoedas("VUV ",57),
                new CadastroMoedas("WST ",28),
                new CadastroMoedas("XAF ",30),
                new CadastroMoedas("XAU ",60),
                new CadastroMoedas("XDR ",27),
                new CadastroMoedas("XOF ",14),
                new CadastroMoedas("XPF ",50),
                new CadastroMoedas("ZAR ",65),
                new CadastroMoedas("ZWL ",31)
            };

            return lista.ToList();
        }
    }
}


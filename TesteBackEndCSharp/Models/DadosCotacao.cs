using System;
namespace TesteBackEndCSharp.Models
{
    public class DadosCotacao
    {
        
        public string? vlr_cotacao { get; set; }
       
        public long cod_cotacao { get; set; }

        public string? dat_cotacao { get; set; }

        private DateTime converterDataCotacao(string value)
        {
            var dataTexto = value.ToString();
            if (dataTexto is null)
                { return DateTime.Today; }
            var listaData = dataTexto.Split("/");
            var dia = listaData[0];
            var mes = listaData[1];
            var ano = listaData[2].Substring(0,4);
            var retorno = new DateTime( int.Parse(ano) , int.Parse(mes) , int.Parse(dia));
            return retorno;
        }

        private decimal converterValorCotacao(string value)
        {
            var valorString = value;
            if (valorString is null)
                { return decimal.MinValue; }
            var retorno = decimal.Parse(valorString.Replace(",", "."));
            return retorno;
        }

    }
}


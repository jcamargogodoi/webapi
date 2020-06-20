using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Zurich.Models;

namespace WebAPI.Zurich.Business
{
    public class ValidacaoGeral
    {
        public ValidacaoGeral()
        {

        }
        public bool ValidaCamposSeguro(SeguradoDtos objSegurado)
        {
            if ((objSegurado == null || objSegurado.Nome == "" || objSegurado.Nome == null))
            {
                return false;
            }
            return true;
        }

        public bool ValidaIdade(int Idade)
        {
            if (Idade < 18 || Idade > 103)
            {
                return false;
            }
            return true;
        }

    }
}
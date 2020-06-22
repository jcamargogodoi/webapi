using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Zurich.Models;

namespace WebAPI.Zurich.Business
{
    public class SeguradoBo
    {
        public bool ValidaNome(string Nome)
        {
            if (String.IsNullOrEmpty(Nome) || Nome.Length < 6)
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

        /// <summary>
        /// Valida se um cpf é válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public bool ValidarCPF(string cpf)
        {
            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cpf = Util.Util.RemoveNaoNumericos(cpf);

            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }


    }
}
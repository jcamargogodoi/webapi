using System;
using WebAPI.Zurich.Models;

namespace WebAPI.Zurich.Business
{
    public class VeiculoBo
    {
        public bool ValidaVeiculo(string nome)
        {
            if (String.IsNullOrEmpty(nome) || nome.Length < 6)
            {
                return false;
            }
            return true;
        }
        public bool ValidaValor(double ValorVeiculo)
        {
            if (ValorVeiculo < 2000.00 )
            {
                return false;
            }
            return true;
        }
    }
}
using System;
using WebAPI.Zurich.Models;
namespace WebAPI.Zurich.Business
{
    static class Constants
    {
        public const double MARGEM_SEGURANCA = 3; //percentual
        public const double LUCRO = 5;   //percentual
    }
    public class SeguroBo
    {
        double taxaRisco;
        double premioRisco;
        double premioPuro;
        double premioComercial;
        public Seguro calcularSeguro(double valorVeiculo)
        {
            Seguro listaSeguro = new Seguro();
            taxaRisco = CalcularTaxaRisco(valorVeiculo);
            premioRisco = CalcularPremioRisco(taxaRisco, valorVeiculo);
            premioPuro = CalcularPremioPuro(premioRisco);
            premioComercial = CalcularPremioComercial(premioPuro);

            listaSeguro.TaxaRisco = taxaRisco;
            listaSeguro.PremioRisco = premioRisco;
            listaSeguro.PremioPuro = premioPuro;
            listaSeguro.PremioComercial = premioComercial;

            return listaSeguro;   
        }

        public double CalcularTaxaRisco(double valorVeiculo)
        {
            return (valorVeiculo * 5) / (2 * valorVeiculo);
        }
        public double CalcularPremioRisco(double taxaRisco, double valorVeiculo)
        {
            return ((taxaRisco/100) * valorVeiculo);
        }
        public double CalcularPremioPuro(double premioRisco)
        {
            return (premioRisco * (1 + (Constants.MARGEM_SEGURANCA / 100)));
        }
        public double CalcularPremioComercial(double premioPuro)
        {
            return Math.Round(((1+(Constants.LUCRO/100)) * premioPuro),2);
        }
    }
}


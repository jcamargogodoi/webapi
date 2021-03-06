﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Zurich.Models;

namespace WebAPI.Zurich.Repository
{
    interface ISeguroRepository
    {
        IEnumerable<Seguro> GetAll();
        IEnumerable<Seguro> GetById(int Id);
        void Add(Seguro obj);
        void Update(Seguro obj);
        void Delete(int Id);
        void Save();
        List<Seguro> VerificarExisteCadastroSeguroSegurado(Seguro obj);
        List<Seguro> VerificarExisteSeguroParaSegurado(Seguro obj);
        List<Seguro> VerificarExisteSeguroParaVeiculo(Seguro obj);
        List<Seguro> VerificarExisteSeguro(int Id);
        Array GerarListaMediaSeguros();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebAPI.Zurich.Models;

namespace WebAPI.Zurich.Repository
{
    public class SeguradoRepository : ISeguradoRepository
    {
        private EF_Context _objEntidades;
        public SeguradoRepository()
        {
            _objEntidades = new EF_Context();
        }

        public SeguradoRepository(EF_Context _objEntidades)
        {
            _objEntidades = this._objEntidades;
        }

        public void Add(Segurado obj)
        {
            _objEntidades.segurado.Add(obj);
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Segurado> GetAll()
        {
            return _objEntidades.segurado.ToList();
        }

        public IEnumerable<Segurado> GetById(int Id)
        {
            return _objEntidades.segurado.Where(x => x.Id == Id).ToList();
        }

        public void Save()
        {
            _objEntidades.SaveChanges();
        }

        public void Update(Segurado obj)
        {
            _objEntidades.Entry(obj).State = EntityState.Modified;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        public List<Segurado> VerificarExisteSegurado(Segurado obj)
        {
            return _objEntidades.segurado.Where(x => x.Id == obj.Id).ToList();
        }

        public void Delete(int Id)
        {
                var itemToRemoveSegu = _objEntidades.segurado.SingleOrDefault(x => x.Id == Id);
                _objEntidades.segurado.Remove(itemToRemoveSegu);
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

        //public List<Segurado> GetById(int Id)
        //{
        //    return _objEntidades.segurado.Where(x => x.Id == Id).ToList();
        //}
    }
}
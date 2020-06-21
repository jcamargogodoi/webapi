using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebAPI.Zurich.Models;

namespace WebAPI.Zurich.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private EF_Context _objEntidades;
        public VeiculoRepository()
        {
            _objEntidades = new EF_Context();
        }

        public VeiculoRepository(EF_Context _objEntidades)
        {
            _objEntidades = this._objEntidades;
        }

        public void Add(Veiculo obj)
        {
            _objEntidades.veiculo.Add(obj);
        }
        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Veiculo> GetAll()
        {
            return _objEntidades.veiculo.ToList();
        }

        public List<Veiculo> GetById(int Id)
        {
            return _objEntidades.veiculo.Where(x => x.Id == Id).ToList();
        }
        public void Save()
        {
            _objEntidades.SaveChanges();
        }

        public void Update(Veiculo obj)
        {
            _objEntidades.Entry(obj).State = EntityState.Modified;
        }
        IEnumerable<Veiculo> IVeiculoRepository.GetAll()
        {
            return _objEntidades.veiculo.ToList();
        }
    }
}
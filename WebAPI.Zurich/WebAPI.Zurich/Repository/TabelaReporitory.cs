
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Zurich.Models;

namespace WebAPI.Zurich.Repository
{
    public class TabelaRepository : ITabelaRepository
    {

        private EF_Context _objEntidades;
        public TabelaRepository()
        {
                _objEntidades = new EF_Context();
        }

        public TabelaRepository(EF_Context _objEntidades)
        {
            _objEntidades = this._objEntidades;
        }

        public void Add(Tabela obj)
        {
            _objEntidades.tabela.Add(obj);
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tabela> GetAll()
        {
            return _objEntidades.tabela.ToList();
        }

        public Tabela GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _objEntidades.SaveChanges();
        }

        public void Update(Tabela obj)
        {
            throw new NotImplementedException();
        }
    }
}
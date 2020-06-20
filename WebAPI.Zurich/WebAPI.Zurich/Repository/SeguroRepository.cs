using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebAPI.Zurich.Models;

namespace WebAPI.Zurich.Repository
{
    public class SeguroRepository : ISeguroRepository
    {
        private EF_Context _objEntidades;
        public SeguroRepository()
        {
            _objEntidades = new EF_Context();
        }

        public SeguroRepository(EF_Context _objEntidades)
        {
            _objEntidades = this._objEntidades;
        }

        public IEnumerable<Seguro> GetAll()
        {
            return _objEntidades.seguro.Include(m => m.Veiculo)
                                .Include(m => m.Segurado)
                                .ToList();
        }

        public IEnumerable<Seguro> GetById(int Id)
        {
            return _objEntidades.seguro.Include(m => m.Veiculo)
                                .Include(m => m.Segurado)
                                .Where(x => x.SeguradoRefId == Id)

                                .ToList();

        }

        public List<Seguro> VerificarExisteCadastroSeguroSegurado(Seguro obj)
        {
            return _objEntidades.seguro.Where(x => x.SeguradoRefId == obj.SeguradoRefId && x.VeiculoRefId == obj.VeiculoRefId).ToList();
        }

        public List<Seguro> VerificarExisteSeguroParaSegurado(Seguro obj)
        {
            return _objEntidades.seguro.Where(x => x.SeguradoRefId == obj.SeguradoRefId).ToList();
        }

        

        public void Add(Seguro obj)
        {
            _objEntidades.seguro.Add(obj);
        }

        public void Delete(int Id)
        {
            var itemToRemove = _objEntidades.seguro.SingleOrDefault(x => x.Id == Id);
            if (itemToRemove != null)
                _objEntidades.seguro.Remove(itemToRemove);
        }

        public void Save()
        {
            _objEntidades.SaveChanges();
        }

        public void Update(Seguro obj)
        {
            _objEntidades.Entry(obj).State = EntityState.Modified;
        }

        public string GerarListaMediaSeguros()
        {
           

            var query = from seguro in _objEntidades.seguro
                        group seguro by new { seguro.SeguradoRefId, seguro.ValorSeguro } into f
                        let seguroTotal = _objEntidades.seguro.Sum(x => x.ValorSeguro)
                        let mediaSalarial = _objEntidades.seguro.Average(x => x.ValorSeguro)
                        orderby f.Key.SeguradoRefId, f.Key.ValorSeguro
                        select new
                        {
                            ValorSeguro = f.Key.ValorSeguro,
                            SeguradoRefId = f.Key.SeguradoRefId,
                            Total = seguroTotal,
                            Media = mediaSalarial
                        };

            
            return "Ajustar o linq não está retornando";
        }
    }
}
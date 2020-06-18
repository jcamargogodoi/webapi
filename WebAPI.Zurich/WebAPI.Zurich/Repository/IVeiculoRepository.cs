using System.Collections.Generic;
using WebAPI.Zurich.Models;

namespace WebAPI.Zurich.Repository
{
    interface IVeiculoRepository
    {
        IEnumerable<Veiculo> GetAll();
        List<Veiculo> GetById(int Id);

        void Add(Veiculo obj);
        void Update(Veiculo obj);

        void Delete(int Id);

        void Save();
    }
}

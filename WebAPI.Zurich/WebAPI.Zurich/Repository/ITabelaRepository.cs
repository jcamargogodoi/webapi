using System.Collections.Generic;
using WebAPI.Zurich.Models;

namespace WebAPI.Zurich.Repository
{
    public interface ITabelaRepository
    {
        IEnumerable<Tabela> GetAll();
        Tabela GetById(int Id);

        void Add(Tabela obj);
        void Update(Tabela obj);

        void Delete(int Id);

        void Save();

    }
}
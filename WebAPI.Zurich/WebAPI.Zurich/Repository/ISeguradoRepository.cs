using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Zurich.Models;

namespace WebAPI.Zurich.Repository
{
    interface ISeguradoRepository
    {
        IEnumerable<Segurado> GetAll();
        IEnumerable<Segurado> GetById(int Id);

        List<Segurado> VerificarExisteSegurado(Segurado obj);

        void Add(Segurado obj);
        void Update(Segurado obj);

        void Delete(int Id);

        void Save();
    }
}

using _5school.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories.Interfaces
{
    public interface IStreamRepository
    {
        void Add(Entities.Stream model);
        Entities.Stream GetForEdit(int id);
        void Update(Entities.Stream model);
        Entities.Stream GetStreamById(int id);
        List<Entities.Stream> GetAll();
        void Delete(int id);
    }
}

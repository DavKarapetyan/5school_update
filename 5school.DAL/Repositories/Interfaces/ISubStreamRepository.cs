using _5school.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories.Interfaces
{
    public interface ISubStreamRepository
    {
        void Add(SubStream model);
        SubStream GetForEdit(int id);
        void Update(SubStream model);
        SubStream GetSubStreamById(int id);
        List<SubStream> GetAll();
        void Delete(int id);
    }
}

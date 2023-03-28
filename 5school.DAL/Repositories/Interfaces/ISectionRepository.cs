using _5school.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories.Interfaces
{
    public interface ISectionRepository
    {
        void Add(Section model);
        Section GetById(int id);
        Section GetForEdit(int id);
        List<Section> GetAll();
        void Update(Section model);
        void Delete(int id);
    }
}

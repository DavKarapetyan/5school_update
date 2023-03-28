using _5school.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories.Interfaces
{
    public interface IPageRepository
    {
        void Add(Page model);
        Page GetById(int id);
        Page GetForEdit(int id);
        List<Page> GetAll();
        void Update(Page model);
        void Delete(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _5school.DAL.Entities;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        void Add(Group model);
        Group GetForEdit(int id);
        void Update(Group model);
        Group GetGroupById(int id);
        List<Group> GetAll();
        void Delete(int id);
    }
}

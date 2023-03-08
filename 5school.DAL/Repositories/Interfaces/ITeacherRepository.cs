using _5school.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        void Add(Teacher model);
        Teacher GetForEdit(int id);
        void Update(Teacher model);
        Teacher GetTeacherById(int id);
        List<Teacher> GetAll();
        void Delete(int id);
    }
}

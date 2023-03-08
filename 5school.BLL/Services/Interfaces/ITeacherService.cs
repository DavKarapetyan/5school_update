using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services.Interfaces
{
    public interface ITeacherService
    {
        public List<TeacherVM> GetTeachers(CultureType cultureType);
        public TeacherVM GetTeacherById(int id, CultureType cultureType);
        public TeacherAddEditVM GetTeacherForEdit(int id, CultureType cultureType);
        public void Add(TeacherAddEditVM model);
        public void Update(TeacherAddEditVM model, CultureType cultureType);
        public void Delete(int id);

    }
}

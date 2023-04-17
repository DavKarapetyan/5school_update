using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services.Interfaces
{
    public interface IPageService
    {
        void Add(PageVM model);
        void Update(PageVM model, CultureType cultureType);
        void Delete(int id);
        PageVM GetById(int id, CultureType cultureType);
        PageVM GetForEdit(int id, CultureType cultureType);
        List<PageVM> GetAll(CultureType cultureType);
    }
}

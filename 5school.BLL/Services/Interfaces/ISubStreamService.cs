using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services.Interfaces
{
    public interface ISubStreamService
    {
        public List<SubStreamVM> GetSubStreams(CultureType cultureType);
        public SubStreamVM GetSubStreamById(int id, CultureType cultureType);
        public SubStreamAddEditVM GetSubStreamForEdit(int id, CultureType cultureType);
        public void Add(SubStreamAddEditVM model);
        public void Update(SubStreamAddEditVM model, CultureType cultureType);
        public void Delete(int id);
    }
}

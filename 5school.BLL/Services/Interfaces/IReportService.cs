using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services.Interfaces
{
    public interface IReportService
    {
        public List<ReportVM> GetReports(CultureType cultureType);
        public ReportVM GetReportById(int id, CultureType cultureType);
        public void Add(ReportVM model);
        public void Update(ReportVM model, CultureType cultureType);
        public void Delete(int id);
    }
}

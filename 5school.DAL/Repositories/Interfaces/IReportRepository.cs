using _5school.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories.Interfaces
{
    public interface IReportRepository
    {
        void Add(Report model);
        Report GetForEdit(int id);
        void Update(Report model);
        Report GetReportById(int id);
        List<Report> GetAll();
        void Delete(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _5school.DAL.Entities;
using _5school.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _5school.DAL.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly _5schoolDbContext _context;
        public ReportRepository(_5schoolDbContext context)
        {
            _context = context;
        }

        public void Add(Report model)
        {
            _context.Reports.Add(model);
        }

        public void Delete(int id)
        {
            _context.Reports.Remove(GetReportById(id));
        }

        public List<Report> GetAll()
        {
            var data = _context.Reports.Select(r => new Report
            {
                Id = r.Id,
                Name = r.Name,
                FilePath = r.FilePath,
            }).ToList();
            return data;
        }

        public Report GetForEdit(int id)
        {
            var data = _context.Reports.Where(r => r.Id == id).FirstOrDefault();
            return data;
        }

        public Report GetReportById(int id)
        {
            var data = _context.Reports.Where(r => r.Id == id).AsNoTracking().FirstOrDefault();
            return data;
        }

        public void Update(Report model)
        {
            var data = _context.Reports.FirstOrDefault(r => r.Id == model.Id);
            data.Name = model.Name;
            data.FilePath = model.FilePath;
        }
    }
}

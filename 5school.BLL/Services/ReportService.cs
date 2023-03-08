using _5school.BLL.Services.Interfaces;
using _5school.BLL.ViewModels;
using _5school.DAL.Entities;
using _5school.DAL.Enums;
using _5school.DAL.Repositories;
using _5school.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IUnitOfWork _uow;
        private readonly ITranslateService _translateService;
        public ReportService(IReportRepository reportRepository, IUnitOfWork uow, ITranslateService translateService)
        {
            _reportRepository = reportRepository;
            _uow = uow;
            _translateService = translateService;
        }

        public void Add(ReportVM model)
        {
            Report report = new Report()
            {
                Name = model.Name,
                FilePath= model.FilePath,
            };
            _reportRepository.Add(report);
            _uow.Save();
        }

        public void Delete(int id)
        {
            _reportRepository.Delete(id);
            _uow.Save();
        }

        public ReportVM GetReportById(int id, CultureType cultureType)
        {
            var report = _reportRepository.GetForEdit(id);
            if (cultureType != CultureType.am)
            {
                report = _translateService.Convert(report, "Reports", id, cultureType, new List<int> { id }) as Report;
            }
            ReportVM model = new ReportVM
            {
                Id = id,
                Name = report.Name,
                FilePath = report.FilePath,
            };

            return model;
        }

        public List<ReportVM> GetReports(CultureType cultureType)
        {
            var reports = _reportRepository.GetAll();
            if (cultureType != CultureType.am)
            {
                reports = _translateService.Convert(reports, "Reports", 0, cultureType, reports.Select(g => g.Id).ToList()) as List<Report>;
            }
            var list = reports.Select(g => new ReportVM
            {
                Id = g.Id,
                Name = g.Name,
                FilePath = g.FilePath,
            }).ToList();
            return list;
        }

        public void Update(ReportVM model, CultureType cultureType)
        {
            var entity = _reportRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.am)
            {
                entity.Name = model.Name;
                entity.FilePath = model.FilePath;
                _reportRepository.Update(entity);
            }
            else
            {
                var tableName = "Reports";
                _translateService.Fill(model, cultureType, tableName, model.Id);
            }
            _uow.Save();
        }
    }
}

using _5school.BLL.Services.Interfaces;
using _5school.BLL.ViewModels;
using _5school.DAL.Entities;
using _5school.DAL.Enums;
using _5school.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IUnitOfWork _uow;
        private readonly ITranslateService _translateService;
        public SectionService(ISectionRepository sectionRepository, IUnitOfWork uow, ITranslateService translateService)
        {
            _sectionRepository = sectionRepository;
            _uow = uow;
            _translateService = translateService;
        }
        public void Add(SectionAddEditVM model)
        {
            var data = new Section()
            { 
                ImageFile = model.ImageFile,
                IsDeleted = model.IsDeleted,
                PageId = model.PageId,
                Text = model.Text,
                Title = model.Title,
            };
            _sectionRepository.Add(data);
            _uow.Save();
        }

        public void Delete(int id)
        {
            _sectionRepository.Delete(id);
            _uow.Save();
        }

        public List<SectionVM> GetAll(CultureType cultureType)
        {
            var sections = _sectionRepository.GetAll();
            if (cultureType != CultureType.am) 
            {
                sections = _translateService.Convert(sections, "Sections", 0, cultureType, sections.Select(g => g.Id).ToList()) as List<Section>;
            }
            var list = sections.Select(s => new SectionVM 
            {
                Id = s.Id,
                ImageFile = s.ImageFile,
                IsDeleted = s.IsDeleted,
                Text = s.Text,
                Title = s.Title,
            }).ToList();
            return list;
        }

        public SectionVM GetById(int id, CultureType cultureType)
        {
            var section = _sectionRepository.GetById(id);
            if (cultureType != CultureType.am) 
            {
                section = _translateService.Convert(section, "Sections", id, cultureType, new List<int> { id }) as Section;
            }
            var sectionVM = new SectionVM
            { 
                Id = section.Id,
                ImageFile = section.ImageFile,
                IsDeleted = section.IsDeleted,
                Text = section.Text,
                Title = section.Title,
            };
            return sectionVM;
        }

        public SectionAddEditVM GetForEdit(int id, CultureType cultureType)
        {
            var section = _sectionRepository.GetForEdit(id);
            if (cultureType != CultureType.en) 
            {
                section = _translateService.Convert(section, "Sections", id, cultureType, new List<int> { id}) as Section;
            }
            var sectionAddEditVM = new SectionAddEditVM 
            {
                Id = id,
                ImageFile = section.ImageFile,
                IsDeleted = section.IsDeleted,
                PageId = section.PageId,
                Text = section.Text,
                Title = section.Title,
            };
            return sectionAddEditVM;
        }

        public List<SectionVM> GetSectionsByPageId(int id, CultureType cultureType)
        {
            var sections = _sectionRepository.GetAll().Where(s => s.Id == id);
            if (cultureType != CultureType.am) 
            {
                sections = _translateService.Convert(sections, "Sections", 0, cultureType, sections.Select(s => s.Id).ToList()) as List<Section>;
            }
            var list = sections.Select(s => new SectionVM
            {
                Id = s.Id,
                ImageFile = s.ImageFile,
                IsDeleted = s.IsDeleted,
                Text = s.Text,
                Title = s.Title,
            }).ToList();
            return list;
        }

        public void Update(SectionAddEditVM model, CultureType cultureType)
        {
            var entity = _sectionRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.am)
            {
                entity.Text = model.Text;
                entity.Title = model.Title;
                entity.IsDeleted = model.IsDeleted;
                entity.ImageFile = model.ImageFile;
                entity.PageId = model.PageId;
                _sectionRepository.Update(entity);
            }
            else 
            {
                var tableName = "Sections";
                _translateService.Fill(model, cultureType, tableName, model.Id);
            }
            _uow.Save();
        }
    }
}

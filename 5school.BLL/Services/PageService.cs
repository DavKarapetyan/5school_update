using _5school.BLL.Services.Interfaces;
using _5school.BLL.ViewModels;
using _5school.DAL.Entities;
using _5school.DAL.Enums;
using _5school.DAL.Repositories;
using _5school.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly ISectionService _sectionService;
        private readonly IUnitOfWork _uow;
        private readonly ITranslateService _translateService;

        public PageService(IPageRepository pageRepository, IUnitOfWork uow, ISectionService sectionService, ITranslateService translateService)
        {
            _pageRepository = pageRepository;
            _uow = uow;
            _sectionService = sectionService;
            _translateService = translateService;

        }
        public void Add(PageVM model)
        {
            var data = new Page()
            {
                IsDeleted = false,
                Title = model.Title,
            };
            _pageRepository.Add(data);
            _uow.Save();
        }

        public void Delete(int id)
        {
            _pageRepository.Delete(id);
            _uow.Save();
        }

        public List<PageVM> GetAll(CultureType cultureType)
        {
            var pages = _pageRepository.GetAll();
            if (cultureType != CultureType.am)
            {
                pages = _translateService.Convert(pages, "Pages", 0, cultureType, pages.Select(g => g.Id).ToList()) as List<Page>;
            }
            var list = pages.Select(g => new PageVM
            {
                Id = g.Id,
                IsDeleted = g.IsDeleted,
                Title = g.Title,
            }).ToList();
            return list;
        }

        public PageVM GetById(int id, CultureType cultureType)
        {
            var page = _pageRepository.GetById(id);
            if (cultureType != CultureType.am) 
            {
                page = _translateService.Convert(page, "Pages", id, cultureType, new List<int> { id }) as Page;
            }
            var pageVM = new PageVM 
            {
                Id = id,
                IsDeleted = page.IsDeleted,
                Title = page.Title,
                Sections = _sectionService.GetSectionsByPageId(id, cultureType)
            };
            return pageVM;
        }

        public PageVM GetForEdit(int id, CultureType cultureType)
        {
            var page = _pageRepository.GetForEdit(id);
            if (cultureType != CultureType.am)
            {
                page = _translateService.Convert(page, "Pages", id, cultureType, new List<int> { id }) as Page;
            }
            var pageVM = new PageVM
            {
                Id = id,
                IsDeleted = page.IsDeleted,
                Title = page.Title,
                Sections = _sectionService.GetSectionsByPageId(id, cultureType)
            };
            return pageVM;
        }

        public void Update(PageVM model, CultureType cultureType)
        {
            var entity = _pageRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.am)
            {
                entity.Title = model.Title;
                entity.IsDeleted = false;
                _pageRepository.Update(entity);
            }
            else
            {
                var tableName = "Pages";
                _translateService.Fill(model, cultureType, tableName, model.Id);
            }
            _uow.Save();
        }
    }
}

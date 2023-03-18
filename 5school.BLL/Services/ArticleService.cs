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
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _uow;
        private readonly ITranslateService _translateService;
        public ArticleService(IArticleRepository articleRepository, IUnitOfWork uow, ITranslateService translateService)
        {
            _articleRepository = articleRepository;
            _uow = uow;
            _translateService = translateService;

        }
        public void Add(ArticleVM model)
        {
            var article = new Article()
            {
                ArticleType = model.ArticleType,
                Description = model.Description,
                ImageFile = model.ImageFile,
                Title = model.Title,
                IsDeleted = false,
            };
            _articleRepository.Add(article);
            _uow.Save();
        }

        public void Delete(int id)
        {
            _articleRepository.Delete(id);
            _uow.Save();
        }

        public List<ArticleVM> GetAll(CultureType cultureType)
        {
            var articles = _articleRepository.GetAll();
            if (cultureType != CultureType.am)
            {
                articles = _translateService.Convert(articles, "Articles", 0, cultureType, articles.Select(a => a.Id).ToList()) as List<Article>;
            }
            var list = articles.Select(a => new ArticleVM
            {
                ArticleType = a.ArticleType,
                Description = a.Description,
                Id = a.Id,
                ImageFile = a.ImageFile,
                Title = a.Title,
                IsDeleted = a.IsDeleted,
            }).ToList();
            return list;
        }
        public List<ArticleVM> GetAllByArticleType(CultureType cultureType, ArticleType articleType)
        {
            var articles = _articleRepository.GetAll().Where(a => a.ArticleType == articleType).ToList();
            if (cultureType != CultureType.am)
            {
                articles = _translateService.Convert(articles, "Articles", 0, cultureType, articles.Select(a => a.Id).ToList()) as List<Article>;
            }
            var list = articles.Select(a => new ArticleVM
            {
                ArticleType = a.ArticleType,
                Description = a.Description,
                Id = a.Id,
                ImageFile = a.ImageFile,
                Title = a.Title,
                IsDeleted = a.IsDeleted,
            }).ToList();
            return list;
        }

        public ArticleVM GetById(int id, CultureType cultureType)
        {
            var article = _articleRepository.GetById(id);
            if (cultureType != CultureType.am)
            {
                article = _translateService.Convert(article, "Articles", id, cultureType, new List<int> { id }) as Article;
            }
            var model = new ArticleVM
            {
                ArticleType = article.ArticleType,
                Description = article.Description,
                Id = article.Id,
                ImageFile = article.ImageFile,
                Title = article.Title,
                IsDeleted = article.IsDeleted,
            };
            return model;
        }

        public ArticleVM GetForEdit(int id, CultureType cultureType)
        {
            var article = _articleRepository.GetForEdit(id);
            if (cultureType != CultureType.am)
            {
                article = _translateService.Convert(article, "Articles", id, cultureType, new List<int> { id }) as Article;
            }
            var model = new ArticleVM
            {
                ArticleType = article.ArticleType,
                Description = article.Description,
                Id = article.Id,
                ImageFile = article.ImageFile,
                Title = article.Title,
                IsDeleted = article.IsDeleted,
            };
            return model;
        }

        public void Update(ArticleVM model, CultureType cultureType)
        {
            var entity = _articleRepository.GetForEdit(model.Id);
            if (cultureType != CultureType.am)
            {
                entity.ArticleType = model.ArticleType;
                entity.Description = model.Description;
                entity.ImageFile = model.ImageFile;
                entity.Title = model.Title;
                entity.IsDeleted = false;
            }
            else
            {
                var tableName = "Articles";
                _translateService.Fill(model, cultureType, tableName, model.Id);
            }
        }
    }
}

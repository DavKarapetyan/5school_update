using _5school.DAL.Entities;
using _5school.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly _5schoolDbContext _context;
        public ArticleRepository(_5schoolDbContext context)
        {
            _context = context;                
        }
        public void Add(Article model)
        {
            _context.Articles.Add(model);
        }

        public void Delete(int id)
        {
            var data = GetForEdit(id);
            data.IsDeleted = true;
        }

        public List<Article> GetAll()
        {
            var data = _context.Articles.Select(a => new Article() 
            {
                Description = a.Description,
                Id = a.Id,
                ArticleType = a.ArticleType,
                ImageFile = a.ImageFile,
                Title = a.Title,
                IsDeleted = a.IsDeleted,
            }).ToList();
            return data;
        }

        public Article GetById(int id)
        {
            var data = _context.Articles.Where(a => a.Id == id).AsNoTracking().FirstOrDefault();
            return data;
        }

        public Article GetForEdit(int id)
        {
            var data = _context.Articles.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public void Update(Article model)
        {
            var data = _context.Articles.FirstOrDefault(a => a.Id == model.Id);
            data.Title = model.Title;
            data.Description = model.Description;
            data.ArticleType = model.ArticleType;
            data.ImageFile = model.ImageFile;
            data.IsDeleted = model.IsDeleted;
        }
    }
}

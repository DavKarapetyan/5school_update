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
    public class PageRepository : IPageRepository
    {
        private readonly _5schoolDbContext _context;
        public PageRepository(_5schoolDbContext context)
        {
            _context = context;
        }
        public void Add(Page model)
        {
            _context.Pages.Add(model);
        }

        public void Delete(int id)
        {
            var data = GetForEdit(id);
            data.IsDeleted = true;
        }

        public List<Page> GetAll()
        {
            var data = _context.Pages.ToList();
            return data;
        }

        public Page GetById(int id)
        {
            var data = _context.Pages.Where(p => p.Id == id).AsNoTracking().FirstOrDefault();
            return data;
        }

        public Page GetForEdit(int id)
        {
            var data = _context.Pages.Where(p => p.Id == id).FirstOrDefault();
            return data;
        }

        public void Update(Page model)
        {
            var data = _context.Pages.FirstOrDefault(p => p.Id == model.Id);
            data.Title = model.Title;
        }
    }
}

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
    public class SectionRepository : ISectionRepository
    {
        private readonly _5schoolDbContext _context;
        public SectionRepository(_5schoolDbContext context)
        {
            _context = context;
        }
        public void Add(Section model)
        {
            _context.Sections.Add(model);
        }

        public void Delete(int id)
        {
            var data = GetForEdit(id);
            data.IsDeleted = true;
        }

        public List<Section> GetAll()
        {
            var data = _context.Sections.Select(s => new Section() { 
                Id = s.Id,
                ImageFile = s.ImageFile,
                IsDeleted = s.IsDeleted,
                Page = s.Page,
                PageId = s.PageId,
                Text = s.Text,
                Title = s.Title,
            }).ToList();
            return data;
        }

        public Section GetById(int id)
        {
            var data = _context.Sections.Where(s => s.Id == id).AsNoTracking().FirstOrDefault();
            return data;
        }

        public Section GetForEdit(int id)
        {
            var data = _context.Sections.Where(s => s.Id == id).FirstOrDefault();
            return data;
        }

        public void Update(Section model)
        {
            var data = _context.Sections.FirstOrDefault(p => p.Id == model.Id);
            data.IsDeleted = model.IsDeleted;
            data.Text = model.Text;
            data.Title = model.Title;
            data.Page = model.Page;
            data.PageId = model.PageId;
            data.ImageFile = model.ImageFile;
            
        }
    }
}

using _5school.DAL.Repositories.Interfaces;
using _5school.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _5school.DAL.Repositories
{
    public class StreamRepository : IStreamRepository
    {
        private readonly _5schoolDbContext _context;
        public StreamRepository(_5schoolDbContext context)
        {
            _context = context;
        }

        public void Add(Entities.Stream model)
        {
            _context.Streams.Add(model);
        }

        public void Delete(int id)
        {
            var data = GetForEdit(id);
            data.IsDeleted = true;
        }

        public List<Entities.Stream> GetAll()
        {
            var data = _context.Streams.Select(s => new Entities.Stream { 
                Id= s.Id,
                Name= s.Name,
                SubStreams= s.SubStreams,
                ImageFile = s.ImageFile,
                IsDeleted = s.IsDeleted,
            }).ToList();
            return data;
        }

        public Entities.Stream GetForEdit(int id)
        {
            var data = _context.Streams.Where(s => s.Id == id).FirstOrDefault();
            return data;
        }

        public Entities.Stream GetStreamById(int id)
        {
            var data = _context.Streams.Where(s => s.Id == id).AsNoTracking().FirstOrDefault();
            return data;
        }

        public void Update(Entities.Stream model)
        {
            var data = _context.Streams.FirstOrDefault(s => s.Id == model.Id);
            data.Name = model.Name;
            data.SubStreams = model.SubStreams;
            data.ImageFile = model.ImageFile;
            data.IsDeleted = model.IsDeleted;
        }
    }
}

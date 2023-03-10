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
    public class SubStreamRepository : ISubStreamRepository
    {
        private readonly _5schoolDbContext _context;
        public SubStreamRepository(_5schoolDbContext context)
        {
            _context = context;
        }

        public void Add(SubStream model)
        {
            _context.SubStreams.Add(model);
        }

        public void Delete(int id)
        {
            _context.SubStreams.Remove(GetSubStreamById(id));
        }

        public List<SubStream> GetAll()
        {
            var data = _context.SubStreams.Select(ss => new SubStream
            {
                Id = ss.Id,
                Name = ss.Name,
                Stream = ss.Stream,
                StreamId = ss.StreamId,
                Classes = ss.Classes,
                ImageFile = ss.ImageFile,
                StreamItem = ss.StreamItem,
                Teacher = ss.Teacher,
            }).ToList();
            return data;
        }

        public SubStream GetForEdit(int id)
        {
            var data = _context.SubStreams.Where(ss => ss.Id == id).FirstOrDefault();
            return data;
        }

        public SubStream GetSubStreamById(int id)
        {
            var data = _context.SubStreams.Where(ss => ss.Id == id).AsNoTracking().FirstOrDefault();
            return data;
        }

        public void Update(SubStream model)
        {
            var data = _context.SubStreams.FirstOrDefault(ss => ss.Id == model.Id);
            data.Name = model.Name;
            data.Stream = model.Stream;
            data.StreamId = model.StreamId;
            data.ImageFile = model.ImageFile;
            data.StreamItem = model.StreamItem;
            data.Classes = model.Classes;
            data.Teacher = model.Teacher;
        }
    }
}

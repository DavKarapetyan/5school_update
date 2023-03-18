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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly _5schoolDbContext _context;
        public TeacherRepository(_5schoolDbContext context)
        {
            _context = context;
        }

        public void Add(Teacher model)
        {
            _context.Teachers.Add(model);
        }

        public void Delete(int id)
        {
            var data = GetForEdit(id);
            data.IsDeleted = true;
        }

        public List<Teacher> GetAll()
        {
            var data = _context.Teachers.Select(t => new Teacher
            { 
                Id = t.Id,
                FirstName= t.FirstName,
                LastName= t.LastName,
                Group= t.Group,
                GroupId= t.GroupId,
                ImagePath= t.ImagePath,
                Position= t.Position,
                IsDeleted= t.IsDeleted,
            }).AsNoTracking().ToList();
            return data;
        }

        public Teacher GetForEdit(int id)
        {
            var data = _context.Teachers.Where(t => t.Id == id).FirstOrDefault();
            return data;
        }

        public Teacher GetTeacherById(int id)
        {
            var entity = _context.Teachers.Where(t => t.Id == id).AsNoTracking().FirstOrDefault();
            return entity;
        }

        public void Update(Teacher model)
        {
            var data = _context.Teachers.FirstOrDefault(t => t.Id == model.Id);
            data.FirstName = model.FirstName;
            data.ImagePath = model.ImagePath;
            data.LastName = model.LastName;
            data.GroupId = model.GroupId;
            data.Position = model.Position;
            data.IsDeleted = model.IsDeleted;
        }
    }
}

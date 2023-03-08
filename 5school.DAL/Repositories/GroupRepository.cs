using _5school.DAL.Repositories.Interfaces;
using _5school.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly _5schoolDbContext _context;
        public GroupRepository(_5schoolDbContext context)
        {
            _context = context;
        }

        public void Add(Group model)
        {
            _context.Groups.Add(model);
        }

        public void Delete(int id)
        {
            _context.Groups.Remove(GetGroupById(id));
        }

        public List<Group> GetAll()
        {
            var data = _context.Groups.Select(g => new Group
            {
                Id = g.Id,
                Name = g.Name,
                Teachers = g.Teachers.Select(t => new Teacher() 
                {
                    FirstName = t.FirstName,
                    Group = t.Group,
                    LastName = t.LastName,
                    GroupId = t.GroupId,
                    Id = t.Id,
                    ImagePath = t.ImagePath,
                    Position = t.Position,
                }).ToList(),
            }).AsNoTracking().ToList();
            return data;
        }

        public Group GetForEdit(int id)
        {
            var data = _context.Groups.Where(a => a.Id == id).Select(g => new Group 
            {
                Id = g.Id,
                Name = g.Name,
                Teachers = g.Teachers.Select(t => new Teacher()
                {
                    FirstName = t.FirstName,
                    Group = t.Group,
                    LastName = t.LastName,
                    GroupId = t.GroupId,
                    Id = t.Id,
                    ImagePath = t.ImagePath,
                    Position = t.Position,
                }).ToList(),
            }).FirstOrDefault();
            return data;
        }

        public Group GetGroupById(int id)
        {
            var data = _context.Groups.Where(a => a.Id == id).AsNoTracking().FirstOrDefault();
            return data;
        }


        public void Update(Group model)
        {
            var data = _context.Groups.FirstOrDefault(a => a.Id == model.Id);
            data.Name = model.Name;
            data.Teachers = model.Teachers;
        }
    }
}

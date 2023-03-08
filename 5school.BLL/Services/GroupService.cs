using _5school.BLL.Services.Interfaces;
using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using _5school.DAL.Repositories.Interfaces;
using _5school.DAL.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _5school.BLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ITranslateService _translateService;
        private readonly IUnitOfWork _uow;
        public GroupService(IGroupRepository groupRepository, ITranslateService translateService, IUnitOfWork uow)
        {
            _groupRepository = groupRepository;
            _translateService = translateService;
            _uow = uow;
        }

        public void Add(GroupVM model)
        {
            DAL.Entities.Group group = new DAL.Entities.Group()
            { 
                Name= model.Name,
            };
            _groupRepository.Add(group);
            _uow.Save();
        }

        public void Delete(int id)
        {
            _groupRepository.Delete(id);
            _uow.Save();
        }

        public GroupVM GetGroupById(int id, CultureType cultureType)
        {
            var group = _groupRepository.GetForEdit(id);
            if (cultureType != CultureType.am)
            {
                group = _translateService.Convert(group, "Groups", id,cultureType, new List<int> { id}) as DAL.Entities.Group;
            }
            GroupVM model = new GroupVM
            { 
                Id = id,
                Name = group.Name,
                Teachers = group.Teachers.Select(t => new TeacherVM() 
                {
                    FirstName = t.FirstName,
                    GroupName = t.Group.Name,
                    Id = t.Id,
                    ImagePath = t.ImagePath,
                    LastName = t.LastName,
                    Position = t.Position
                }).ToList(),
            };

            return model;
        }

        public List<GroupVM> GetGroups(CultureType cultureType)
        {
            var groups = _groupRepository.GetAll();
            if (cultureType != CultureType.am)
            {
                groups = _translateService.Convert(groups, "Groups", 0, cultureType, groups.Select(g => g.Id).ToList()) as List<DAL.Entities.Group>;
            }
            var list = groups.Select(g => new GroupVM 
            {
                Id = g.Id,
                Name = g.Name,
                Teachers = g.Teachers.Select(t => new TeacherVM()
                {
                    FirstName = t.FirstName,
                    GroupName = t.Group.Name,
                    Id = t.Id,
                    ImagePath = t.ImagePath,
                    LastName = t.LastName,
                    Position = t.Position,
                }).ToList(),
            }).ToList();
            return list;
        }

        public void Update(GroupVM model, CultureType cultureType)
        {
            var entity = _groupRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.am)
            {
                entity.Name = model.Name;
                _groupRepository.Update(entity);
            }
            else
            {
                var tableName = "Groups";
                _translateService.Fill(model, cultureType, tableName, model.Id);
            }
            _uow.Save();
        }
    }
}

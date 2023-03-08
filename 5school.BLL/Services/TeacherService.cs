using _5school.BLL.Services.Interfaces;
using _5school.BLL.ViewModels;
using _5school.DAL.Entities;
using _5school.DAL.Enums;
using _5school.DAL.Repositories;
using _5school.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _uow;
        private readonly ITranslateService _translateService;
        public TeacherService(ITeacherRepository teacherRepository, IUnitOfWork uow, ITranslateService translateService)
        {
            _teacherRepository = teacherRepository;
            _uow = uow;
            _translateService = translateService;
        }

        public void Add(TeacherAddEditVM model)
        {
            Teacher subStream = new Teacher
            {
                FirstName = model.FirstName,
                GroupId = model.GroupId,
                LastName = model.LastName,
                ImagePath= model.ImagePath,
                Position = model.Position,
            };
            _teacherRepository.Add(subStream);
            _uow.Save();
        }

        public void Delete(int id)
        {
            _teacherRepository.Delete(id);
            _uow.Save();
        }

        public TeacherVM GetTeacherById(int id, CultureType cultureType)
        {
            var teacher = _teacherRepository.GetForEdit(id);
            if (cultureType != CultureType.am)
            {
                teacher = _translateService.Convert(teacher, "Teachers", id, cultureType, new List<int> { id }) as Teacher;
            }
            TeacherVM model = new TeacherVM
            {
                Id = id,
                FirstName= teacher.FirstName,
                ImagePath = teacher.ImagePath,
                LastName = teacher.LastName,
                Position = teacher.Position,
                GroupName = teacher.Group.Name
            };

            return model;
        }

        public TeacherAddEditVM GetTeacherForEdit(int id, CultureType cultureType)
        {
            var teacher = _teacherRepository.GetForEdit(id);
            if (cultureType != CultureType.am)
            {
                teacher = _translateService.Convert(teacher, "Teachers", id, cultureType, new List<int> { id }) as Teacher;
            }
            TeacherAddEditVM model = new TeacherAddEditVM
            {
                Id = id,
                FirstName = teacher.FirstName,
                GroupId = teacher.GroupId,
                ImagePath = teacher.ImagePath,
                LastName = teacher.LastName,
                Position = teacher.Position,
            };

            return model;
        }

        public List<TeacherVM> GetTeachers(CultureType cultureType)
        {
            var teachers = _teacherRepository.GetAll();
            if (cultureType != CultureType.am)
            {
                teachers = _translateService.Convert(teachers, "Teachers", 0, cultureType, teachers.Select(g => g.Id).ToList()) as List<Teacher>;
            }
            var list = teachers.Select(t => new TeacherVM
            {
                FirstName = t.FirstName,
                LastName = t.LastName,
                Id = t.Id,
                ImagePath = t.ImagePath,
                Position = t.Position,
                GroupName = t.Group.Name
            }).ToList();
            return list;
        }

        public void Update(TeacherAddEditVM model, CultureType cultureType)
        {
            var entity = _teacherRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.am)
            {
                entity.Position = model.Position;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.ImagePath = model.ImagePath;
                entity.GroupId = model.GroupId;
            }
            else
            {
                var tableName = "Teachers";
                _translateService.Fill(model, cultureType, tableName, model.Id);
            }
            _uow.Save();
        }
    }
}

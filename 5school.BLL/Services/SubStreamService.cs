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
    public class SubStreamService : ISubStreamService
    {
        private readonly ISubStreamRepository _subStreamRepository;
        private readonly IUnitOfWork _uow;
        private readonly ITranslateService _translateService;
        public SubStreamService(ISubStreamRepository subStreamRepository, IUnitOfWork uow, ITranslateService translateService)
        {
            _subStreamRepository = subStreamRepository;
            _translateService = translateService;
            _uow = uow;
        }
        public void Add(SubStreamAddEditVM model)
        {
            SubStream subStream = new SubStream
            { 
                Name = model.Name,
                StreamId = model.StreamId,
                Classes = model.Classes,
                ImageFile = model.ImageFile,
                StreamItem = model.StreamItem,
                TeacherId = model.TeacherId
            };
            _subStreamRepository.Add(subStream);
            _uow.Save();
        }

        public void Delete(int id)
        {
            _subStreamRepository.Delete(id);
            _uow.Save();
        }

        public SubStreamVM GetSubStreamById(int id, CultureType cultureType)
        {
            var subStream = _subStreamRepository.GetForEdit(id);
            if (cultureType != CultureType.am)
            {
                subStream = _translateService.Convert(subStream, "SubStreams", id, cultureType, new List<int> { id }) as SubStream;
            }
            SubStreamVM model = new SubStreamVM
            {
                Id = id,
                Name = subStream.Name,
                Classes = subStream.Classes,
                StreamItem = subStream.StreamItem,
                ImageFile = subStream.ImageFile,
            };

            return model;
        }

        public SubStreamAddEditVM GetSubStreamForEdit(int id, CultureType cultureType)
        {
            var subStream = _subStreamRepository.GetForEdit(id);
            if (cultureType != CultureType.am)
            {
                subStream = _translateService.Convert(subStream, "SubStreams", id, cultureType, new List<int> { id }) as SubStream;
            }
            SubStreamAddEditVM model = new SubStreamAddEditVM
            {
                Id = id,
                Name = subStream.Name,
                StreamId = subStream.StreamId,
                Classes = subStream.Classes,
                TeacherId = subStream.TeacherId,
                StreamItem = subStream.StreamItem,
                ImageFile = subStream.ImageFile
            };

            return model;
        }

        public List<SubStreamVM> GetSubStreams(CultureType cultureType)
        {
            var subStreams = _subStreamRepository.GetAll();
            if (cultureType != CultureType.am)
            {
                subStreams = _translateService.Convert(subStreams, "SubStreams", 0, cultureType, subStreams.Select(g => g.Id).ToList()) as List<SubStream>;
            }
            var list = subStreams.Select(ss => new SubStreamVM
            {
                Id = ss.Id,
                Name = ss.Name,
                Classes = ss.Classes,
                Teacher = $"{ss.Classes}-{ss.Teacher.FirstName} {ss.Teacher.LastName}",
                ImageFile = ss.ImageFile,
                StreamItem = ss.StreamItem,
            }).ToList();
            return list;
        }

        public void Update(SubStreamAddEditVM model, CultureType cultureType)
        {
            var entity = _subStreamRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.en)
            {
                entity.Name = model.Name;
                entity.StreamId = model.StreamId;
                entity.ImageFile = model.ImageFile;
                entity.StreamItem = model.StreamItem;
                entity.TeacherId = model.TeacherId;
                entity.Classes = model.Classes;
                _subStreamRepository.Update(entity);
            }
            else
            {
                var tableName = "SubStreams";
                _translateService.Fill(model, cultureType, tableName, model.Id);
            }
            _uow.Save();
        }
    }
}

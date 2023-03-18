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
    public class StreamService : IStreamService
    {
        private readonly IStreamRepository _streamRepository;
        private readonly IUnitOfWork _uow;
        private readonly ITranslateService _translateService;
        public StreamService(IStreamRepository streamRepository, IUnitOfWork uow, ITranslateService translateService)
        {
            _streamRepository = streamRepository;
            _uow = uow;
            _translateService = translateService;
        }

        public void Add(StreamVM model)
        {
            DAL.Entities.Stream stream = new DAL.Entities.Stream()
            {
                Name = model.Name,
                ImageFile = model.ImageFile,
                IsDeleted = false,
            };
            _streamRepository.Add(stream);
            _uow.Save();
        }

        public void Delete(int id)
        {
            _streamRepository.Delete(id);
            _uow.Save();
        }

        public StreamVM GetStreamById(int id, CultureType cultureType)
        {
            var stream = _streamRepository.GetForEdit(id);
            if (cultureType != CultureType.am)
            {
                stream = _translateService.Convert(stream, "Streams", id, cultureType, new List<int> { id }) as DAL.Entities.Stream;
            }
            StreamVM model = new StreamVM
            {
                Id = id,
                Name = stream.Name,
                ImageFile = stream.ImageFile,
                IsDeleted = stream.IsDeleted,
            };

            return model;
        }

        public List<StreamVM> GetStreams(CultureType cultureType)
        {
            var streams = _streamRepository.GetAll();
            if (cultureType != CultureType.am)
            {
                streams = _translateService.Convert(streams, "Streams", 0, cultureType, streams.Select(g => g.Id).ToList()) as List<DAL.Entities.Stream>;
            }
            var list = streams.Select(g => new StreamVM
            {
                Id = g.Id,
                Name = g.Name,
                ImageFile = g.ImageFile,
                IsDeleted = g.IsDeleted,
            }).ToList();
            return list;
        }

        public void Update(StreamVM model, CultureType cultureType)
        {
            var entity = _streamRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.am)
            {
                entity.Name = model.Name;
                entity.ImageFile = model.ImageFile;
                entity.IsDeleted = false;
                _streamRepository.Update(entity);
            }
            else
            {
                var tableName = "Streams";
                _translateService.Fill(model, cultureType, tableName, model.Id);
            }
            _uow.Save();
        }
    }
}

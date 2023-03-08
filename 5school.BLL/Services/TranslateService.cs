using _5school.BLL.Services.Interfaces;
using _5school.DAL.Entities;
using _5school.DAL.Enums;
using _5school.DAL.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly ITranslateRepository _translateRepository;
        public TranslateService(ITranslateRepository translateRepository)
        {
            _translateRepository = translateRepository;
        }

        public object Convert(object model, string tableName, int primaryKey, CultureType cultureType, List<int> primaryKeys)
        {
            if (model is IEnumerable)
            {
                var translates = _translateRepository.List(p => p.TableName == tableName && primaryKeys.Contains(p.PrimaryKey) && p.CultureType == cultureType);

                foreach (var item in model as IEnumerable)
                {
                    var fields = item.GetType().GetProperties().ToList();
                    var pk = (int)fields.FirstOrDefault(p => p.Name == "Id").GetValue(item);
                    fields.Where(p => p.PropertyType == typeof(string) && !p.Name.Contains("File")).ToList().
                        ForEach(p => p.SetValue(item, translates.FirstOrDefault(t => t.PrimaryKey == pk && t.FieldName == p.Name) != null ? translates.FirstOrDefault(t => t.PrimaryKey == pk && t.FieldName == p.Name).Value : p.GetValue(item)));
                }
                return model;
            }
            var values = _translateRepository.List(t => t.TableName == tableName && t.PrimaryKey == primaryKey && t.CultureType == cultureType);
            var type = model.GetType();
            var props = type.GetProperties().Where(p => !p.Name.Contains("Id") && !p.Name.Contains("Image"));
            foreach (var item in values)
            {
                var prop = type.GetProperty(item.FieldName);
                prop.SetValue(model, item.Value, null);
            }
            return model;
        }

        public void Fill(object model, CultureType cultureType, string tableName, int primaryKey)
        {
            var currentValues = _translateRepository.List(p => p.TableName == tableName && p.PrimaryKey == primaryKey && p.CultureType == cultureType);

            List<Translate> translates = new List<Translate>();
            var type = model.GetType();
            var props = type.GetProperties().Where(p => p.PropertyType == typeof(string) && !p.Name.Contains("File") && !p.Name.Contains("Image") && p.GetValue(model, null) != null);
            foreach (var item in props)
            {
                translates.Add(new Translate
                {
                    TableName = tableName,
                    CultureType = cultureType,
                    FieldName = item.Name,
                    PrimaryKey = primaryKey,
                    Value = item.GetValue(model, null)?.ToString()
                });
            }
            _translateRepository.AddRange(translates);
            _translateRepository.RemoveRange(currentValues.ToList());
        }
    }
}

using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services.Interfaces
{
    public interface IStreamService
    {
        public List<StreamVM> GetStreams(CultureType cultureType);
        public StreamVM GetStreamById(int id, CultureType cultureType);
        public void Add(StreamVM model);
        public void Update(StreamVM model, CultureType cultureType);
        public void Delete(int id); 
    }
}

using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services.Interfaces
{
    public interface IGroupService
    {
        public List<GroupVM> GetGroups(CultureType cultureType);
        public GroupVM GetGroupById(int id, CultureType cultureType);
        public void Add(GroupVM model);
        public void Update(GroupVM model, CultureType cultureType);
        public void Delete(int id);
    }
}

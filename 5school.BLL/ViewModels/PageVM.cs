using _5school.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.ViewModels
{
    public class PageVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public List<SectionVM> Sections { get; set; }
    }
}

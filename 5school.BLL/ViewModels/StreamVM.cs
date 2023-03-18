using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.ViewModels
{
    public class StreamVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageFile { get; set; }
        public bool IsDeleted { get; set; }
        public CultureType Culture { get; set; }
    }
}

using _5school.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.ViewModels
{
    public class SubStreamVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageFile { get; set; }
        public string StreamItem { get; set; }
        public string Classes { get; set; }
        public string Teacher { get; set; }
        public bool IsDeleted { get; set; }
    }
}

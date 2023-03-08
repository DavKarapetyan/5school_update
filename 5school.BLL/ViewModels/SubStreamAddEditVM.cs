using _5school.DAL.Entities;
using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.ViewModels
{
    public class SubStreamAddEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageFile { get; set; }
        public string StreamItem { get; set; }
        public string Classes { get; set; }
        public int TeacherId { get; set; }
        public int StreamId { get; set; }
        public CultureType Culture { get; set; }
    }
}

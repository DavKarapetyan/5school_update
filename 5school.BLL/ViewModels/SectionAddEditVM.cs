using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.ViewModels
{
    public class SectionAddEditVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Text { get; set; }
        public string? ImageFile { get; set; }
        public bool IsDeleted { get; set; }
        public int PageId { get; set; }
    }
}

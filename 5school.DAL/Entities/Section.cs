using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Text { get; set; }
        public string? ImageFile { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("Page")]
        public int PageId { get; set; }
        public Page Page { get; set; }  
    }
}

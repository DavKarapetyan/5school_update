using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.ViewModels
{
    public class ArticleVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public bool IsDeleted { get; set; }
        public ArticleType ArticleType { get; set; }
        public CultureType CultureType { get; set; }
    }
}

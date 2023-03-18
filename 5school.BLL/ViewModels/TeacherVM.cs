using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.ViewModels
{
    public class TeacherVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public bool IsDeleted { get; set; }
        public Position Position { get; set; }
        public string GroupName { get; set; }
    }
}

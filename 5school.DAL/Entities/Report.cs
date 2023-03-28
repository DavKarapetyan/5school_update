using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string FilePath { get; set; }
        public ReportType ReportType { get; set; }
    }
}

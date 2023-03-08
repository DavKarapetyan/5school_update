using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Entities
{
    public class Translate
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string Value { get; set; }
        public string FieldName { get; set; }
        public int PrimaryKey { get; set; }
        public CultureType CultureType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Entities
{
    public class SubStream
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageFile { get; set; }
        public string StreamItem { get;set; }
        public string Teacher { get; set; }
        public string Classes { get; set; }
        [ForeignKey("StreamId")]
        public Stream Stream { get; set; }
        public int StreamId { get; set; }
    }
}

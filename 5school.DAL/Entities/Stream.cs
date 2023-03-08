using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Entities
{
    public class Stream
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string ImageFile { get; set; }
        public virtual ICollection<SubStream> SubStreams { get; set; }
    }
}

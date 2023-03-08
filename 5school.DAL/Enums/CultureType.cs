using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Enums
{
    public enum CultureType
    {
        [Display(Name = "ՀԱՅ")]
        am = 1,
        [Display(Name = "РУ")]
        ru = 2,
        [Display(Name = "EN")]
        en = 3,
    }
}

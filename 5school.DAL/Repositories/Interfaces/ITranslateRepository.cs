using _5school.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories.Interfaces
{
    public interface ITranslateRepository
    {
        List<Translate> List(Expression<Func<Translate, bool>> predicate);
        void AddRange(List<Translate> translators);
        void RemoveRange(List<Translate> translators);
    }
}

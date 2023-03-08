using _5school.DAL.Entities;
using _5school.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories
{
    public class TranslateRepository : ITranslateRepository
    {
        private readonly _5schoolDbContext _context;
        public TranslateRepository(_5schoolDbContext context)
        {
            _context = context;
        }

        public void AddRange(List<Translate> translators)
        {
            _context.Translates.AddRange(translators);
        }

        public List<Translate> List(Expression<Func<Translate, bool>> predicate)
        {
            var data = _context.Translates.Where(predicate).ToList();
            return data;
        }

        public void RemoveRange(List<Translate> translators)
        {
            _context.Translates.RemoveRange(translators);
        }
    }
}

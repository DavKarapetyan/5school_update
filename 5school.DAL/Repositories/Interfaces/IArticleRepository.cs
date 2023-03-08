using _5school.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.DAL.Repositories.Interfaces
{
    public interface IArticleRepository
    {
        void Add(Article model);
        void Update(Article model);
        void Delete(int id);
        Article GetById(int id);
        Article GetForEdit(int id);
        List<Article> GetAll();
    }
}

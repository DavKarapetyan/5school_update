using _5school.BLL.ViewModels;
using _5school.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5school.BLL.Services.Interfaces
{
    public interface IArticleService
    {
        void Add(ArticleVM model);
        void Update(ArticleVM model,CultureType cultureType);
        void Delete(int id);
        ArticleVM GetById(int id,CultureType cultureType);
        ArticleVM GetForEdit(int id, CultureType cultureType);
        List<ArticleVM> GetAll(CultureType cultureType);
        List<ArticleVM> GetAllByArticleType(CultureType cultureType, ArticleType articleType);

    }
}

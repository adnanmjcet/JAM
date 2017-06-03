using CommonLayer.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICategory
    {
        List<CategoryModel> CategoryList();

        CategoryModel GetCategory(CategoryModel model);

        int Save(CategoryModel model);

        CategoryModel GetById(int id);

        List<UserCategoryMappingModel> UserCategoryList();

        void UpdateCategory(List<CategoryModel> lstmodel, int userid);

        void AddCategoryMapping(int userID);
    }
}

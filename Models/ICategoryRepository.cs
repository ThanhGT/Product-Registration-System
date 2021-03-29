using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TT_LAB2.Models
{
    public interface ICategoryRepository
    {
        //pure getter method
        IEnumerable<Category> Categories { get; }

        //getting category by id's
        Category this[int id] { get; }

        //add method
        Category AddCategory(Category category);
        //update method
        Category UpdateCategory(Category category);
        //delete method
        void DeleteCategory(int id);

    }
}

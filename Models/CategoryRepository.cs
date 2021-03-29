using System.Collections.Generic;

namespace TT_LAB2.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private Dictionary<int, Category> items;

        //easily accessible list of category names
        public List<string> CategoryNames { get; set; }

        // Constructor
        public CategoryRepository()
        {
            items = new Dictionary<int, Category>();
            //sample data
            new List<Category>
            {
                new Category { CategoryId=101, CategoryName="Mobile"},
                new Category { CategoryId=201, CategoryName="Audio"},
            }.ForEach(c => AddCategory(c));
        }

        public Category this[int id] => items.ContainsKey(id) ? items[id] : null;
        public IEnumerable<Category> Categories => items.Values;
        public Category AddCategory(Category category)
        {
            if (category.CategoryId == 0)
            {
                int key = items.Count;
                while (items.ContainsKey(key))
                { key++; }
            }
            items[category.CategoryId] = category;

            return category;
        }

        public void DeleteCategory(int id)
        {
            items.Remove(id);
        }

        public Category UpdateCategory(Category category) =>
            AddCategory(category);
    }
}

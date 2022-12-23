using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class CategoryCRUD
    {
        private readonly ApplicationDbContext _context;
        public CategoryCRUD(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.categories.ToList();
        }
        public Category GetCategoryById(int id)
        {
            var c = _context.categories.Where(x => x.Category_Id == id).FirstOrDefault();
            if (c != null)
                return c;
            else
                return null;

        }
        public int AddCategory(Category cat)
        {
            _context.categories.Add(cat);
            int res = _context.SaveChanges();
            return res;

        }
        public int UpdateCategory(Category cat)
        {

            _context.categories.Update(cat);
            int res = _context.SaveChanges();
            return res;
        }
        public int DeleteCategory(int id)
        {
            int res = 0;
            var c = _context.categories.Where(x => x.Category_Id == id).FirstOrDefault();
            if (c != null)
            {
                _context.categories.Remove(c);
                res = _context.SaveChanges();
            }
            return res;
        }
    }
}

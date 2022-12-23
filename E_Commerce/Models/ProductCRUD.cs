using System.Data;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace E_Commerce.Models
{
    public class ProductCRUD
    {
        private readonly ApplicationDbContext _context;
        public ProductCRUD(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            
            try
            {
                var product_list = from q in _context.products
                                   join w in _context.categories
                                   on q.Category_Id equals w.Category_Id
                                   into Cat
                                   from w in Cat.DefaultIfEmpty()
                                   select new Product
                                   {
                                       Product_Id = q.Product_Id,
                                       Product_Name = q.Product_Name,
                                       Product_Desc = q.Product_Desc,
                                       Price = q.Price,
                                       Product_Img = q.Product_Img,
                                       Category_Id = q.Category_Id,
                                       Category_Name = w == null ? "" : w.Category_Name

                                   };

                return product_list;
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Product GetProductById(int id)
        {
            var product = _context.products.Where(x => x.Product_Id == id).FirstOrDefault();
            if (product != null)
                return product;
            else
                return null;

        }
        public int AddProduct(Product prod)
        {
            _context.products.Add(prod);
            int res = _context.SaveChanges();
            return res;

        }
        public int UpdateProduct(Product prod)
        {

            _context.products.Update(prod);
            int res = _context.SaveChanges();
            return res;
        }
        public int DeleteProduct(int id)
        {
            int res = 0;
            var product = _context.products.Where(x => x.Product_Id == id).FirstOrDefault();
            if (product != null)
            {
                _context.products.Remove(product);
                res = _context.SaveChanges();

            }
            return res;
        }

    }
}

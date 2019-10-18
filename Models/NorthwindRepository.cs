using System.Linq;

namespace Northwind.Models
{
    public class NorthwindRepository : INorthwindRepository
    {
        private readonly NorthwindContext _db;

        public NorthwindRepository(NorthwindContext context)
        {
            _db = context;
        }

        public IQueryable<Product> Products => _db.Products;
        public IQueryable<Category> Categories => _db.Categories;
        public IQueryable<Discount> Discounts => _db.Discounts;
        public IQueryable<Customer> Customers => _db.Customers;
    }
}
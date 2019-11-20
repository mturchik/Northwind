using System.Linq;

namespace Northwind.Models
{
    public interface INorthwindRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Discount> Discounts { get; }
        IQueryable<Customer> Customers { get; }
        void AddCustomer(Customer customer);
        IQueryable<Employee> Employee { get; }
        void AddEmployee (Employee employee);
    }
}
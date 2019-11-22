using System.Linq;

namespace Northwind.Models
{
    public interface INorthwindRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Discount> Discounts { get; }
        void AddDiscount(Discount discount);
        void EditDiscount(Discount discount);
        void DeleteDiscount(int id);
        IQueryable<Customer> Customers { get; }
        void AddCustomer(Customer customer);
        IQueryable<Employee> Employee { get; }
        void AddEmployee (Employee employee);
    }
}
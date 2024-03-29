﻿using System.Linq;

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

        public void AddDiscount(Discount discount)
        {
            _db.Discounts.Add(discount);
            _db.SaveChanges();
        }

        public void EditDiscount(Discount discount)
        {
            _db.Discounts.Remove(_db.Discounts.Find(discount.DiscountId));
            _db.Add(discount);
            _db.SaveChanges();
        }

        public void DeleteDiscount(int id)
        {
            _db.Discounts.Remove(_db.Discounts.Find(id));
            _db.SaveChanges();
        }

        public IQueryable<Customer> Customers => _db.Customers;

        public IQueryable<Employee> Employee => _db.Employee;

        public void AddCustomer(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }

        public void AddEmployee(Employee employee)
        {
            _db.Employee.Add(employee);
            _db.SaveChanges();
        }
    }
}
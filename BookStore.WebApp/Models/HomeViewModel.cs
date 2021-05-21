using BookStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.WebApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Customer> Customers { get; set; } = Enumerable.Empty<Customer>();
        public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
        public IEnumerable<Employee> Employees { get; set; } = Enumerable.Empty<Employee>();
        public IEnumerable<Employee> NewEmployees { get; set; } = Enumerable.Empty<Employee>();
        public IEnumerable<Employee> BirthdayEmployees { get; set; } = Enumerable.Empty<Employee>();
        public decimal TotalSum { get; set; }
        public decimal MonthlySum { get; set; }
        public decimal YearlySum { get; set; }
    }
}
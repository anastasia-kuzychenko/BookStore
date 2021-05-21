using BookStore.Models;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public interface IUnitOfWork
    {
        IRepository<Book> Books { get; }
        IRepository<Order> Orders { get; }
        IRepository<Employee> Employees { get; }
        IRepository<Customer> Customers { get; }
        IRepository<Author> Authors { get; }
        Task Commit();
    }
}
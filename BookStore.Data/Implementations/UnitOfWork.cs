using BookStore.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookStore.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly BookStoreDbContext _context;

        private IRepository<Book> _books;
        private IRepository<Order> _orders;
        private IRepository<Customer> _customers;
        private IRepository<Employee> _emploees;
        private IRepository<Author> _authors;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(BookStoreDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IRepository<Book> Books => _books ??= new Repository<Book>(_context);
        public IRepository<Order> Orders => _orders ??= new Repository<Order>(_context);
        public IRepository<Customer> Customers => _customers ??= new Repository<Customer>(_context);
        public IRepository<Employee> Employees => _emploees ??= new Repository<Employee>(_context);
        public IRepository<Author> Authors => _authors ??= new Repository<Author>(_context);

        public async Task Commit()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when try to save changes in db");
                throw;
            }
        }
    }
}
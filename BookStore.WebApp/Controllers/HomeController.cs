using BookStore.Services;
using BookStore.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

/*
 TODO
    - create searche + pagination
    - add sorting
    - authorization
 */

/*
 DONE
    - implement home page methods
    - include db store
    - implement validation
    - implement order details
    - implement editing of all entities
    - implement deletion of all entities
    - implement creation book with author
    - implement order creation with autho compleat books employees and customers
 */

namespace BookStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly IEmployeesService _employeesService;
        private readonly ICustomerService _customersService;
        private readonly IOrdersService _ordersService;

        public HomeController(
            IBooksService booksService, 
            IEmployeesService employeesService, 
            ICustomerService customerService,
            IOrdersService ordersService)
        {
            _booksService = booksService;
            _employeesService = employeesService;
            _customersService = customerService;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                Books = await _booksService.GetTop(),
                Employees = await _employeesService. GetTop(),
                Customers = await _customersService.GetTop(),
                BirthdayEmployees = await _employeesService.GetBirthdayEmployees(),
                NewEmployees = await _employeesService.GetNewEmployees(),
                TotalSum = await _ordersService.GetTotalSum(),
                MonthlySum = await _ordersService.GetMonthlySum(),
                YearlySum = await _ordersService.GetYearlySum()
            };
            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace BookStore.WebApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly IEmployeesService _employeesService;
        private readonly ICustomerService _customersService;
        private readonly IOrdersService _ordersService;

        public OrdersController(
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

        [HttpGet]
        public async Task<IActionResult> Get(
            string sortOrder, 
            string keyWord, 
            PaymentType? paymentType, 
            OrderState? state, 
            decimal? minSum, 
            decimal? maxSum, 
            int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.KeyWord = keyWord;
            ViewBag.MinSum = minSum;
            ViewBag.MaxSum = maxSum;
            ViewBag.PaymentType = paymentType;
            ViewBag.State = state;

            var model = await _ordersService.Get(sortOrder, keyWord, paymentType, state, minSum, maxSum, pageNumber);
            return View(model);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _ordersService.GetById(id);
            return View(model);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Create()
        {
            var books = await _booksService.Get();
            var employees = await _employeesService.Get();
            var customers = await _customersService.Get();
            ViewBag.Books = new SelectList(books, nameof(Book.Id), nameof(Book.Name));
            ViewBag.Employees = new SelectList(employees, nameof(Employee.Id), nameof(Employee.FullName));
            ViewBag.Customers = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName));
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(Order model)
        {
            await _ordersService.Create(model);
            return RedirectToAction(nameof(Get));
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _ordersService.GetById(id);
            var books = await _booksService.Get();
            var employees = await _employeesService.Get();
            var customers = await _customersService.Get();
            ViewBag.Books = new SelectList(books, nameof(Book.Id), nameof(Book.Name));
            ViewBag.Employees = new SelectList(employees, nameof(Employee.Id), nameof(Employee.FullName));
            ViewBag.Customers = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName));
            return View(model);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(Order model)
        {
            if (ModelState.IsValid)
            {
                await _ordersService.Edit(model);
                return RedirectToAction(nameof(Get));
            }
            return View(model);
        }
    }
}
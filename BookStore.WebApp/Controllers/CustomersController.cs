using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStore.WebApp.Controllers
{
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customersService;

        public CustomersController(ICustomerService customersService)
        {
            _customersService = customersService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string sortOrder, string keyWord, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.KeyWord = keyWord;

            var model = await _customersService.Get(sortOrder, keyWord, pageNumber);
            return View(model);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _customersService.GetById(id);
            return View(model);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(Customer model)
        {
            if (ModelState.IsValid)
            {
                await _customersService.Create(model);
                return RedirectToAction(nameof(Get));
            }
            return View(model);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _customersService.GetById(id);
            return View(model);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(Customer model)
        {
            if (ModelState.IsValid)
            {
                await _customersService.Edit(model);
                return RedirectToAction(nameof(Get));
            }
            return View(model);
        }
    }
}
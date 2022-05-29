using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStore.WebApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string sortOrder, string keyWord, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.KeyWord = keyWord;

            var model = await _employeesService.Get(sortOrder, keyWord, pageNumber);
            return View(model);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _employeesService.GetById(id);
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
        public async Task<IActionResult> Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                await _employeesService.Create(model);
                return RedirectToAction(nameof(Get));
            }
            return View(model);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _employeesService.GetById(id);
            return View(model);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                await _employeesService.Edit(model);
                return RedirectToAction(nameof(Get));
            }
            return View(model);
        }
    }
}
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStore.WebApp.Controllers
{
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _authorsService.Get();
            return View(model);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _authorsService.GetById(id);
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
        public async Task<IActionResult> Create(Author model)
        {
            if (ModelState.IsValid)
            {
                await _authorsService.Create(model);
                return RedirectToAction(nameof(Get));
            }
            return View(model);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _authorsService.GetById(id);
            return View(model);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(Author model)
        {
            if (ModelState.IsValid)
            {
                await _authorsService.Edit(model);
                return RedirectToAction(nameof(Get));
            }
            return View(model);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _authorsService.Delete(id);
            return RedirectToAction(nameof(Get));
        }
    }
}
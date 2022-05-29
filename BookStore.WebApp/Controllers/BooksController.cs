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
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly IAuthorsService _authorsService;

        public BooksController(IBooksService booksService, IAuthorsService authorsService)
        {
            _booksService = booksService;
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string sortOrder, string keyWord, decimal? minPrise, decimal? maxPrise, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.KeyWord = keyWord;
            ViewBag.MinPrise = minPrise;
            ViewBag.MaxPrise = maxPrise;

            var model = await _booksService.Get(sortOrder, keyWord, minPrise, maxPrise, pageNumber);
            return View(model);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _booksService.GetById(id);
            return View(model);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Create()
        {
            var authors = await _authorsService.Get();
            ViewBag.Authors = new SelectList(authors, nameof(Author.Id), nameof(Author.FullName));
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(Book model)
        {
            if (ModelState.IsValid)
            {
                await _booksService.Create(model);
                return RedirectToAction(nameof(Get));
            }
            return View(model);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _booksService.GetById(id);
            var authors = await _authorsService.Get();
            ViewBag.Authors = new SelectList(authors, nameof(Author.Id), nameof(Author.FullName), model.Author);
            return View(model);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(Book model)
        {
            if (ModelState.IsValid)
            {
                await _booksService.Edit(model);
                return RedirectToAction(nameof(Get));
            }
            return View(model);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _booksService.Delete(id);
            return RedirectToAction(nameof(Get));
        }
    }
}
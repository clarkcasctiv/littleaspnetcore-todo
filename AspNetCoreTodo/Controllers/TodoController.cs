using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        private readonly UserManager<IdentityUser> _userManager;

        public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Get to-do items from database

            var currentuser = await _userManager.GetUserAsync(User);
            if (currentuser == null)
            {
                return Challenge();
            }

            var items = await _todoItemService.GetIncompleteItemsAsync(currentuser);

            // Put items into a model

            var model = new TodoViewModel
            {
                Items = items
            };

            // Render view using the model
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var currentuser = await _userManager.GetUserAsync(User);

            if (currentuser == null)
            {
                return Challenge();
            }

            var successful = await _todoItemService.AddItemsAsync(newItem, currentuser);
            if (!successful)
            {
                return BadRequest("Could not add Item");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }
            //if (string.IsNullOrEmpty(id))
            //{
            //    return RedirectToAction("Index");
            //}

            var currentuser = await _userManager.GetUserAsync(User);

            if (currentuser == null)
            {
                return Challenge();
            }

            var successful = await _todoItemService.MarkAsDoneAsync(id, currentuser);

            if (!successful)
            {
                return BadRequest("Could not mark item as done");
            }

            return RedirectToAction("Index");
        }
    }
}
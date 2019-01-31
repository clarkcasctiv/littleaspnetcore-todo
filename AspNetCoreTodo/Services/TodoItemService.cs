using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItemsAsync(TodoItem newItem, ApplicationUser user)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.OwnerId = user.Id;

            _context.Items.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user)
        {
            return await _context.Items.Where(x => x.IsDone == false && x.OwnerId == user.Id).ToArrayAsync();
        }

        public async Task<bool> MarkAsDoneAsync(Guid id, ApplicationUser user)
        {
            var item = await _context.Items.Where(x => x.Id == id && x.OwnerId == user.Id).SingleOrDefaultAsync();

            if (item == null)
            {
                return false;
            }

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
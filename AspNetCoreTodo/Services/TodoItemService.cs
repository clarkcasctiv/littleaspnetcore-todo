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

        public async Task<bool> AddItemsAsync(TodoItem newItem, IdentityUser user)
        {
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(4);
            newItem.OwnerId = user.Id;

            _context.Items.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user)
        {
            return await _context.Items.Where(x => x.IsDone == false && x.OwnerId == user.Id).ToArrayAsync();
        }

        public async Task<bool> MarkAsDoneAsync(int id, IdentityUser user)
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
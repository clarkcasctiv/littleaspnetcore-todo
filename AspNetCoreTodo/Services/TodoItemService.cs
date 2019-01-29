using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
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

        public async Task<bool> AddItemsAsync(TodoItem newItem)
        {
            // Random no = new Random();
            // newItem.Id = Convert.ToInt32(no);
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(4);

            _context.Items.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            return await _context.Items.Where(x => x.IsDone == false).ToArrayAsync();
        }

        public async Task<bool> MarkAsDoneAsync(int id)
        {
            var item = await _context.Items.Where(x => x.Id == id).SingleOrDefaultAsync();

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
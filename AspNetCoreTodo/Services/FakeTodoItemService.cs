using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            var item1 = new TodoItem
            {
                Title = "Shopping",
                DueAt = DateTimeOffset.Now.AddDays(3)
            };
            var item2 = new TodoItem
            {
                Title = "Learn ASP.Net Core",
                DueAt = DateTimeOffset.Now.AddDays(5)
            };

            return Task.FromResult(new[] { item1, item2 });
        }
    }
}
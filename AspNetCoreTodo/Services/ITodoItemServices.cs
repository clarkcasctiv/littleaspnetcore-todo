using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync();
        Task<bool> AddItemsAsync(TodoItem newItem);
        Task<bool> MarkAsDoneAsync(int id);
    }
}
using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user);

        Task<bool> AddItemsAsync(TodoItem newItem, ApplicationUser user);

        Task<bool> MarkAsDoneAsync(Guid id, ApplicationUser user);
    }
}
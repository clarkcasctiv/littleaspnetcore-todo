using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user);
        Task<bool> AddItemsAsync(TodoItem newItem, IdentityUser user);
        Task<bool> MarkAsDoneAsync(int id, IdentityUser user);
    }
}
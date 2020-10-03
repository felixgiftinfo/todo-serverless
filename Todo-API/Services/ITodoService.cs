using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo_API.DTOs;
using Todo_API.Models;

namespace Todo_API.Services
{
    public interface ITodoService
    {
        Task<TodoReadDTO> AddTodo(TodoDTO todo);
        Task<bool> UpdateTodo(TodoDTO todo, string id);
        Task<bool> UpdateMissedTodo( string id);
        Task<long> DeleteTodo(string id);
        Task<IEnumerable<TodoReadDTO>> GetTodoList();
        Task<TodoReadDTO> GetTodoById(string id);

    }
}

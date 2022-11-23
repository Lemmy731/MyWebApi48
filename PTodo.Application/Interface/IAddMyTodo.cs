using ETodo.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTodo.Application.Interface
{
    public interface IAddMyTodo
    {
        Task<string> AddMyTodos(UTodoItemDto uTodoItemDto);
    }
}

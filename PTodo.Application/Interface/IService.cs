using ETodo.Domain.Dto;
using ETodo.Domain.Entity;
using MTodo.Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTodo.Application.Interface
{
    public interface IService
    {
        Task<string> AddTodo(UTodoItemDto uTodoItemDto);
        Task<List<AppTodoItem>> AllTodoUser();
        Task<AppTodoItem> Update(string taskname);
        Task<Tokens> Login(SigninDto signinDto);
        Task<string> Register(RegiUserDto regiUserDto);
        Task<string> VerifyPassword(PasswordDto passwordDto);
    }
      
}

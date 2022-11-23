using ETodo.Domain.Dto;
using MTodo.Infrastruture;
using PTodo.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTodo.Application.Implementation
{
     public class AddMyTodo : IAddMyTodo
    {
        private readonly RDbContext _rDbContext;

        public AddMyTodo(RDbContext rDbContext)
        {
            _rDbContext = rDbContext;
        }
        public async Task<string> AddMyTodos(UTodoItemDto uTodoItemDto)
        {
            var activity = new Activity()
            {
                //UserId = Convert.ToInt32(uTodoItemDto.Email),
                TaskName = uTodoItemDto.TaskName,
                TaskStatus = uTodoItemDto.TaskStatus,
                Date = uTodoItemDto.Date,
            };
            var result = await _rDbContext.Activities.AddAsync(activity);
            var save = _rDbContext.SaveChanges();
            if (save != 0)
            {
                return ("save successfully");
            }
            return "not successful"; 

            
        }
    }
}

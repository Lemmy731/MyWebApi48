using ETodo.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MTodo.Infrastruture;
using PTodo.Application.Interface;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PTodoController : ControllerBase
    {
        private readonly IService _service;
        private readonly IAddMyTodo _addMyTodo;

        public PTodoController(IService service, IAddMyTodo addMyTodo)
        {
            _service = service;
            _addMyTodo = addMyTodo;
        }
        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody] UTodoItemDto uTodoItemDto)
        {
            try
            {
           
            if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var utodo = new UTodoItemDto()
                {
                    TaskName = uTodoItemDto.TaskName,
                    TaskStatus = uTodoItemDto.TaskStatus,
                    Date = uTodoItemDto.Date,
                    Email = uTodoItemDto.Email 
                };
                var result = await _service.AddTodo(utodo);
                if (result != String.Empty)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }

        }

        [HttpGet]
        public async Task<IActionResult> AllTodoUser()
        {
            try
            {
                var result = await _service.AllTodoUser();
                if (result == null)
                {
                    return NotFound("users not fund");
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut]
        public async Task<AppTodoItem> Update([FromBody] string taskname)
        {
            
            
                //var apptodo = new UTodoItemDto()
                //{

                //    TaskName = uTodoItemDto.TaskName,
                //    TaskStatus = uTodoItemDto.TaskStatus,
                //    Date = uTodoItemDto.Date,
                //    Email = uTodoItemDto.Email
                //};
                var result = await _service.Update(taskname);
               
                    return result;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]SigninDto signinDto)
        {
            try
            {
                var result = await _service.Login(signinDto);
                if (!result.Contains("login successful"))
                {
                    return BadRequest();
                }
                return StatusCode(200, "sucessful");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        
        }

        [HttpPost("AddMyTodos")]
        public async Task<IActionResult> AddMyTodos([FromBody]UTodoItemDto uTodoItemDto)
        {
            try
            {
                var result = await _addMyTodo.AddMyTodos(uTodoItemDto);
                if (result != null)
                {
                    return StatusCode(200, "successful");
                }
            
                    return StatusCode(400, "unsuccessful");
                
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          

            
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegiUserDto regiUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _service.Register(regiUser);
                if (result.Contains("successful register"))
                {
                    return StatusCode(200, "successful");
                }
                
               return StatusCode(400,"not successful");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}

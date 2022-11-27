using ETodo.Domain.Dto;
using ETodo.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize (Roles = "Admin")]
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
                if (result == null)
                {
                    return BadRequest();
                }
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        
        }
        [HttpPost("VerifyPassword")]
        public async Task<IActionResult> VerifyPassword(PasswordDto passwordDto)
        {
            try
            {
                var result = await _service.VerifyPassword(passwordDto);
                if (result.Contains("success"))
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
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

        [Authorize]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegiUserDto regiUserDto)
        {
            
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                //var regiUserDto = new RegiUserDto()
                //{
                //   // FirstName = regiUser.FirstName,
                //   // SecondName = regiUser.SecondName,

                //    Email = regiUser.Email
                //};
               // var regiUserDto = new RegiUserDto();
                var result = await _service.Register(regiUserDto);
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

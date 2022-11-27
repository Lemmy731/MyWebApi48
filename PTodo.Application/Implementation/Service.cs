using ETodo.Domain.Dto;
using ETodo.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MTodo.Infrastruture;
using PTodo.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTodo.Application.Implementation
{
    public class Service : IService
    {
        private readonly SignInManager<AppTodoItem> _signInManger;
        private readonly UserManager<AppTodoItem> _userManager;
        private readonly IJwtService _jwtService;

        public Service(SignInManager<AppTodoItem> signInManager, UserManager<AppTodoItem> userManager, IJwtService jwtService)
        {
            _signInManger = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }
        public List<UTodoItemDto> Todoes { get; set; } = new List<UTodoItemDto>();
        public async Task<string> AddTodo(UTodoItemDto uTodoItemDto)
        {
            var appTodoItem = new AppTodoItem()
            {
                UserName = uTodoItemDto.Email,
                //TaskName = uTodoItemDto.TaskName,
                //TaskStatus = uTodoItemDto.TaskStatus,
                //Date = uTodoItemDto.Date


            };
           var result = await _userManager.CreateAsync(appTodoItem);
            if(result.Succeeded)
            {
                return "successfully added";
            }
            return "not successful";
        }
       
        public async Task<List<AppTodoItem>> AllTodoUser()
        {
            var result =  _userManager.Users.ToList();
            
                return result;

            
        }
        public async Task<AppTodoItem> Update(string taskname)
        {
            //var apptodoitem = new AppTodoItem()
            //{

            //    UserName = uTodoItemDto.Email,
            //    TaskName = uTodoItemDto.TaskName,
            //    Date = uTodoItemDto.Date,
            //    TaskStatus = uTodoItemDto.TaskStatus,
            //};

            var user = await _userManager.FindByNameAsync(taskname);

            //var result = await _userManager.UpdateAsync(all);
           return user;
        }
        public async Task<Tokens> Login(SigninDto signinDto)
        {
            //var regiuser = new AppTodoItem()
            //{
            //    UserName = signinDto.Email,
            //    PasswordHash = signinDto.Password
            //};
            var result =  await _signInManger.PasswordSignInAsync(signinDto.Email, signinDto.Password, false, false);
            var result1= await _userManager.FindByEmailAsync(signinDto.Email);
            if (result1 != null)
            {
                return _jwtService.Authenticate(result1);
              
            }
            return null;
        } 

        public async Task<string> VerifyPassword(PasswordDto passwordDto)
        {
            var apptodo = new AppTodoItem()
            {
                UserName = passwordDto.Email,
               
            };
            
            if (apptodo.UserName == passwordDto.Email)
            {
                var result = await _userManager.CheckPasswordAsync(apptodo, passwordDto.Password);
                if (result)
                {
                    return "success";
                }
            }
       
            return "not success";

        }

        public async Task<string> Register(RegiUserDto regiUserDto)
        {
            var apptodo = new AppTodoItem()
            {
                
                UserName = regiUserDto.Email,
                Email = regiUserDto.Email,
                FirstName = regiUserDto.FirstName,
                SecondName = regiUserDto.SecondName
              
                
            };
           var result =  await _userManager.CreateAsync(apptodo, regiUserDto.Password);
            if( result.Succeeded)
            {
                return "successful register";
            }
            return "not successful";
        }

        public async Task<AppTodoItem> FindByUser(string Email)
        {
            
            var result = await _userManager.FindByNameAsync(Email);
            if(result != null)
            {
                return result;
            }
            return null  ; 

        }

     



    }
}

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

        public Service(SignInManager<AppTodoItem> signInManager, UserManager<AppTodoItem> userManager)
        {
            _signInManger = signInManager;
            _userManager = userManager;
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
        public async Task<string> Login(SigninDto signinDto)
        {
            var regiuser = new AppTodoItem()
            {
                UserName = signinDto.Email,
                PasswordHash = signinDto.Password
            };
            var result =  await _signInManger.PasswordSignInAsync(regiuser.PasswordHash, regiuser.UserName, false, false);
            if (result.Succeeded)
            {
                return "login successful";
            }
            return "unable to login";
        }

        public async Task<string> Register(RegiUserDto regiUserDto)
        {
            var apptodo = new AppTodoItem()
            {
                FirstName = regiUserDto.FirstName,
                SecondName = regiUserDto.SecondName,
                UserName = regiUserDto.Email,
                Email = regiUserDto.Email,
                
            };
           var result =  await _userManager.CreateAsync(apptodo,regiUserDto.Password);
            if( result.Succeeded)
            {
                return "successful register";
            }
            return "not successful";
        }

     



    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CustomerApplication.API.Services;
using CustomerApplication.Model.DataTransferObject;
using CustomerApplication.Model.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CustomerApplication.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;


        public UsersController(IUserService userService)
          {

            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            // return basic user info (without password) and token to store client side
            return Ok(new {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TelephoneNumber = user.TelephoneNumber,
                Email = user.Email,
                Username = user.Username,

                
             
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            // map dto to entity
            //var user = _mapper.Map<User>(userDto);

            Employee user = new Employee
            {
                  
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                TelephoneNumber = userDto.TelephoneNumber,
                Email = userDto.Email,
                CompanyId = userDto.CompanyId

    };


            try
            {
                // save 
                _userService.Create(user, userDto.Password);
                return Ok();
            } 
            catch(Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _userService.GetAll();
            return Ok(users);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user =  _userService.GetById(id);
   
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            Employee user = new Employee
            {
                Id = id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                TelephoneNumber = userDto.TelephoneNumber,
                Email = userDto.Email,
                CompanyId = userDto.CompanyId
               
            };


            try 
            {
                // save 
                _userService.Update(user, userDto.Password);
                return Ok();
            } 
            catch(Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        
    
    }
}

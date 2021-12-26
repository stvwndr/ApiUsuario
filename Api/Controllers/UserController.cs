using Domain.Services;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("user"), ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDomain _domain;
        private readonly INotification _notification;

        public UserController(UserDomain domain, INotification notification)
        {
            _domain = domain;
            _notification = notification;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                List<User> users = _domain.GetUsers(); 
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, _notification.GetError(ex)); 
            }
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserRequest request)
        {
            try
            {
                return StatusCode(401, _domain.CreateUser(request));
            }
            catch(Exception ex)
            {
                return StatusCode(500, _notification.GetError(ex));
            }
        }
    }
}

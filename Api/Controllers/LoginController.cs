using Domain.Services;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers
{
    [Route("login"), ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginDomain _domain;
        private readonly INotification _notification;

        public LoginController(LoginDomain domain, INotification notification)
        {
            _domain = domain;
            _notification = notification;
        }

        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = _domain.Login(request);
                return StatusCode(200, response);
            }
            catch(UnauthorizedAccessException ex)
            {
                return StatusCode(401, _notification.GetError(ex));
            }
            catch(Exception ex)
            {
                var error = new { Message = ex.Message };
                return StatusCode(400, _notification.GetError(ex));
            }
        }
    }
}

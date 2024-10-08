﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetWebAPI.Models;
using System.Security.Claims;

namespace ProjetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("Professors")]
        [Authorize(Roles = "Professor")]
        public IActionResult ProfessorsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.GivenName}, you are an {currentUser.Role}");
        }


        [HttpGet("Students")]
        [Authorize(Roles = "Student")]
        public IActionResult StudentsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.GivenName}, you are a {currentUser.Role}");
        }

        [HttpGet("ProfessorsAndStudents")]
        [Authorize(Roles = "Professor,Student")]
        public IActionResult ProfessorsAndStudentsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.GivenName}, you are an {currentUser.Role}");
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hi, you're on public property");
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}

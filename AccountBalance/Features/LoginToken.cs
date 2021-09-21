using AccountBalance.Database;
using AccountBalance.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AccountBalance.Features
{
    [ApiController]
    public class LoginTokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginTokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("/auth/logintoken")]
        public async Task<IActionResult> LoginToken(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new LoginToken.Query(username, password));
                return response == null ? BadRequest(response) : Ok(response);
            }
            return BadRequest("Some requests are not valid");
        }
    }

    public static class LoginToken
    {
        //Query
        public record Query(string username, string password) : IRequest<Response>;

        //Handler
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly UserManager<IdentityUser> _userManager;
            private IConfiguration _config;

            public Handler(UserManager<IdentityUser> userManager, IConfiguration config)
            {
                _userManager = userManager;
                _config = config;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.username);
                if(user == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Invalid credentials"
                    };
                }

                var result = await _userManager.CheckPasswordAsync(user, request.password);

                if (!result)
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Invalid credentials"
                    };

                IdentityOptions _options = new();

                var claims = new[]
                {
                    new Claim("UserName", user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSettings:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _config["AuthSettings:Issuer"],
                    audience : _config["AuthSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials : new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );

                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return new Response
                {
                    IsSuccess = true,
                    Message = tokenString
                };
            }
        }

        public record Response
        {
            public bool IsSuccess { get; init; }
            public string Message { get; init; }
        }

    }
}

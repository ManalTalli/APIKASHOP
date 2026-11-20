using Azure.Core;
using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;


namespace KASHOP.BLL.Settings
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configration;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signinManeger;

        private readonly IEmailService _emailService;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configration,ITokenService tokenService,
            SignInManager<ApplicationUser> signinManeger, IEmailService EmailService)
        {
            _userManager = userManager;
            _configration = configration;
            _tokenService = tokenService;
            _signinManeger = signinManeger;
            _emailService = EmailService;
        }

        public async Task<(bool Success, List<string> Errors)> RegisterAsync(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToList());

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);
            var link = $"https://localhost:7146/api/auth/confirm-email?userId={user.Id}&token={encodedToken}";

            await _emailService.SendEmailAsync(user.Email,"Confirm your email","" +
                "<div>" +
                $"<h2>welcom {user.UserName}</h2>"+
                $"<p>please confirm your email by clicking the link below "+
                $"<a href='{link}'>Confirm Email</a>"+
                "</div>");

            await _userManager.AddToRoleAsync(user, "User");
            return (true, []);
        }
        public async Task<(bool Success, List<string> Errors, LoginResponse response)> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return (false, ["invalid email or password"], null);

         
            var result =await _signinManeger.CheckPasswordSignInAsync(user, request.Password, true);

            if (result.IsNotAllowed)
                return (false, ["confirm your email"],null);

            if (result.IsLockedOut)
                return (false, ["Account is locked ,try again later"], null);

            if (!result.Succeeded)
                return (false, ["invalid email or password"], null);

            var token = await _tokenService.GenerateJwt(user);
            var response = new LoginResponse()
            {
                Token = token
            };
            return (true, [], response);
        }

        public async Task<(bool Success,string?Error)> ConfirmEmailAsync(string userId,string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return (false, "invalid confirmation link");

            if (user.EmailConfirmed)
                return (true, null);

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded ? (true, null) : (false, "invalid or expired confirmation link");
        }

    }
}

﻿using AutoMapper;
using DAL.Constant;
using DAL.Dto;
using DAL.Request.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities.Constant;

namespace DAL.Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        private readonly string _connectStrings;
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _permissionRepository;
        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<Role> roleManager, IConfiguration config, IMapper mapper,
            IPermissionRepository permissionRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _connectStrings = config.GetConnectionString(ConstantSystem.DB_CONNECT);
            _mapper = mapper;
            _config = config;
            _permissionRepository = permissionRepository;
        }

        public async Task<string> Authenticate(AuthenticateRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return null;
            }
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.PassWord, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            var role = _userManager.GetRolesAsync(user).Result;
            var permissions = await _permissionRepository.GetAllByUserId(user.Id);

            var claims = new[]
            {
                new Claim(SystemConstants.UserClaim.Id, user.Id.ToString()),
                new Claim(SystemConstants.UserClaim.Permissions,JsonConvert.SerializeObject(permissions)),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.Role, string.Join(",",role))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"], _config["Tokens:Issuer"], claims, expires: DateTime.Now.AddHours(2), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Create(AuthenticateRequest model)
        {

            var user = new User()
            {
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.PassWord);

            if (result.Succeeded)
            {
                return true;
            }
            return false;

        }
    }
}

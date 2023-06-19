using System;
using Microsoft.AspNetCore.Mvc;
using WalletService.Dtos;
using WalletService.Utils;
using AutoMapper;
using WalletService.Repositories;
using WalletService.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace WalletService.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo repo;
        private readonly IMapper dbMapper;

        private readonly IConfiguration config;

        private static readonly TimeSpan expirationLength = TimeSpan.FromHours(8);

        public AuthController(IAuthRepo repository, IMapper mapper, IConfiguration configuration)
        {
            repo = repository;
            dbMapper = mapper;
            config = configuration;
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] AuthCreateDto account)
        {
            string hashedCode = HashValues.Compute(account.code);
            AuthInsertDto newAccount = new AuthInsertDto()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                accountID = account.accountID,
                isAdmin = account.isAdmin,
                accountHash = hashedCode
            };

            var newAuth = dbMapper.Map<Auth>(newAccount);
            repo.CreateAccount(newAuth);

            var saved = repo.SaveChanges();
            if (!saved)
            {
                return BadRequest();
            }

            var result = dbMapper.Map<AuthMainDto>(newAccount);
            return Ok(result);
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] AuthLoginDto account)
        {

            var authenticatedUser = repo.findAccount(account.accountID, account.code);
            if (authenticatedUser == null) return NotFound();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(config["JWT:Key"]);

            var claims = new List<Claim>
            {
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Sub , account.accountID),
                new Claim ("accountid" , account.accountID),
                new Claim ("isadmin", authenticatedUser.isAdmin.ToString())
            };

            Console.Write($"Wallet {authenticatedUser.isAdmin.ToString()}");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(expirationLength),
                Issuer = config["JWT:Issuer"],
                Audience = config["JWT:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            AuthValidDto userWithToken = new AuthValidDto
            {
                token = tokenHandler.WriteToken(token),
                CreatedAt = authenticatedUser.CreatedAt.Value,
                accountID = authenticatedUser.accountID,
                isAdmin = authenticatedUser.isAdmin
            };

            return Ok(userWithToken);
        }
    }

}
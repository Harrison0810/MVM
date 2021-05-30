using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MVM.Domain.Helpers;
using MVM.Domain.Models;
using MVM.Infrastructure.Contracts;
using MVM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace MVM.Domain.Services
{
    public class AuthService : IAuthService
    {
        #region Properties

        private readonly IDBMmvRepository _dbMmvRepository;
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor

        public AuthService(
            IDBMmvRepository dbMmvRepository,
            IConfiguration configuration
        )
        {
            _dbMmvRepository = dbMmvRepository;
            _configuration = configuration;
        }

        #endregion

        #region Public Methods

        MessageModel<AuthModel> IAuthService.Login(AuthModel authModel)
        {
            MessageModel<AuthModel> message = new MessageModel<AuthModel>() { Data = new AuthModel() };

            try
            {
                UsersContract userContract = _dbMmvRepository.Users.FindBy(x => x.UserName == authModel.UserName).FirstOrDefault();

                string password = Helper.EncodePassword(authModel.Password, _configuration.GetValue<string>("Salt"));

                if (!(userContract is null) && password == userContract.Password)
                {
                    string key = _configuration.GetSection("Jwt")["Key"];
                    string baseUrl = _configuration.GetSection("Jwt")["BaseUrl"];

                    SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    SigningCredentials signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(new GenericIdentity(userContract.UserId.ToString(), "Token"), new[]
                    {
                        new Claim("Id", userContract.UserId.ToString())
                        //new Claim("Rol", "")
                    });

                    Claim[] claims = new[]
                    {
                        claimsIdentity.FindFirst("Id")
                        //claims.FindFirst("Rol"),
                    };

                    JwtSecurityToken tokeOptions = new JwtSecurityToken(
                        issuer: baseUrl,
                        audience: baseUrl,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signinCredentials
                    );

                    string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                    message.Data.Token = tokenString;
                    message.Status = true;
                }
                else
                {
                    message.Message = Messages.AuthFail;
                }
            }
            catch (Exception Ex)
            {
                // Save Log
                _dbMmvRepository.Log.Add(new LogContract
                {
                    Action = Messages.Error,
                    CreationDate = DateTime.Now,
                    Description = Ex.Message
                });
            }

            return message;
        }

        #endregion
    }
}

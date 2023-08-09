﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.DTOs;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class UserServices
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private TokenServices _tokenService;

        public UserServices(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenServices tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task Login(LoginUserDTO loginUserDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUserDTO.Username, loginUserDTO.Password, isPersistent: false, lockoutOnFailure: false);
            
            if (!result.Succeeded) 
                throw new ApplicationException("Usuário não autenticado!");

            User user = new User();

            _tokenService.GenerateToken(user);
        }

        public async Task RegisterAsync(CreateUserDTO createUserDTO)
        {
            User user = _mapper.Map<User>(createUserDTO);

            IdentityResult result = await _userManager.CreateAsync(user, createUserDTO.Password);

            if (!result.Succeeded)
                throw new ApplicationException("Falha ao cadastrar usuário!");
        }
    }
}
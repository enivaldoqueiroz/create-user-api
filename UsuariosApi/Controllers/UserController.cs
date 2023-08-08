﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using UsuariosApi.Data.DTOs;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        private RegisterServices _registerServices;

        public UserController(RegisterServices registerServices)
        {
            _registerServices = registerServices;
        }

        [HttpPost]
        public async Task<IActionResult> CadastroUsuario(CreateUserDTO createUserDTO)
        {
            await _registerServices.RegisterAsync(createUserDTO);

            return Ok("Usuário Cadastrado!");
        }
    }
}

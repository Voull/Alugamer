﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alugamer.Auth;
using Alugamer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Alugamer.Controllers
{
    public class LoginController : Controller
    {
        private ErroLogin erroLogin;

        public LoginController()
        {
            erroLogin = new ErroLogin();
        }

        public IActionResult Index()
        {
            if (TokenService.GetUserInfo(HttpContext) != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Login(string nomeUsuario, string senha)
        {
            LoginHandler loginHandler = new LoginHandler(HttpContext);
            if(!loginHandler.AuthLogin(nomeUsuario, senha))
            {
                return Unauthorized(JsonConvert.SerializeObject(erroLogin.GeraErroLogin(ERRO_LOGIN.ERRO_LOGIN_INVALIDO)));
            }

            return NoContent();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            TokenService.RevokeToken(HttpContext);

            return RedirectToAction("Index", "Login");
        }
    }
}

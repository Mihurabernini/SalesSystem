﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Areas.Users.Models;
using SalesSystem.Controllers;
using SalesSystem.Data;
using SalesSystem.Library;
using SalesSystem.Models;

namespace SalesSystem.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize]
    public class UsersController : Controller
    {
        //vamos a implementar el paginador en el controlador de usuarios
        // crearemos unos atributos
        private SignInManager<IdentityUser> _signInManager;
        private LUser _user;
        private static DataPaginador<InputModelRegister> models;

        public UsersController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context
            )
        {
            _signInManager = signInManager;
            _user = new LUser(userManager, signInManager, roleManager, context);
        }
        public IActionResult Users(int id, String filtrar, int registros)
        {
            if (_signInManager.IsSignedIn(User))
            {
                //poner el paginador
                Object[] objects = new Object[3];
                var data = _user.getTUsuariosAsync(filtrar, 0);
                if (0 < data.Result.Count)
                {
                    var url = Request.Scheme + "://" + Request.Host.Value;
                    objects = new LPaginador<InputModelRegister>().paginador(data.Result, id, registros, "Users", "Users", "Users", url);
                }
                else
                {
                    objects[0] = "No hay datos que mostrar";
                    objects[1] = "No hay datos que mostrar";
                    objects[2] = new List<InputModelRegister>();
                }
                models = new DataPaginador<InputModelRegister>
                {
                    List = (List<InputModelRegister>)objects[2],
                    Pagi_info = (String)objects[0],
                    Pagi_navegacion = (String)objects[1],
                    Input = new InputModelRegister(),
                };
                return View(models);
        }
            else
            {
                return Redirect("/");
    }

}
//metodo de acción para cerrar sesion en la aplicacion
public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}

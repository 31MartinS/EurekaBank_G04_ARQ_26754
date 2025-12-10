using Microsoft.AspNetCore.Mvc;
using EurabankWebClient.Models;

namespace EurabankWebClient.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            // Si ya está autenticado, redirigir a la lista
            if (HttpContext.Session.GetString("Autenticado") == "true")
            {
                return RedirectToAction("Index", "Banco");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // Credenciales quemadas
            if (model.Usuario == "MONSTER" && model.Contraseña == "monster9")
            {
                // Guardar en sesión
                HttpContext.Session.SetString("Autenticado", "true");
                HttpContext.Session.SetString("Usuario", model.Usuario);
                
                return RedirectToAction("Index", "Banco");
            }

            model.ErrorMessage = "Usuario o contraseña incorrectos";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MvcCoreEmpty.Models;

namespace MvcCoreEmpty.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Vista1()
        {
            return View();
        }
        public IActionResult Vista2()
        {
            return View();
        }

        public IActionResult VistaPersona()
        {
            Persona persona = new Persona();
            persona.Nombre = "Alumno";
            persona.Email = "alumno@tajamar365.com";
            persona.Edad = 67;
            return View(persona);
        }
    }
}

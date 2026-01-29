using Microsoft.AspNetCore.Mvc;

namespace MvcCoreLinqToSql.Controllers;

public class EmpleadosController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
using Microsoft.AspNetCore.Mvc;
using MvcCoreLinqToSql.Models;
using MvcCoreLinqToSql.Repositories;

namespace MvcCoreLinqToSql.Controllers;

public class EnfermosController : Controller
{
    private RepositoryEnfermos repo;

    public EnfermosController()
    {
        repo = new RepositoryEnfermos();
    }
    
    public IActionResult Index()
    {
        List<Enfermo> enfermos = repo.GetEnfermos();
        if (enfermos == null)
        {
            ViewData["Mensaje"] = "No hay enfermos";
            return View();
        }
        return View(enfermos);
    }


    public async Task<IActionResult> Delete(string inscripcion)
    {
        await repo.DeleteEnfermoByInscripcion(inscripcion);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(string inscripcion)
    {
        Enfermo enfermo = await repo.GetDetailsEnfermoByInscripcion(inscripcion);
        if (enfermo == null)
        {
            ViewData["Mensaje"] = "No hay datos del enfermo";
            return View();
        }
        return View("Details",enfermo);
    }
}
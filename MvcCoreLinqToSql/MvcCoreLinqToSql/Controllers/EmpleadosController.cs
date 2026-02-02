using Microsoft.AspNetCore.Mvc;
using MvcCoreLinqToSql.Models;
using MvcCoreLinqToSql.Repositories;

namespace MvcCoreLinqToSql.Controllers;

public class EmpleadosController : Controller
{
    private RepositoryEmpleados repo;

    public EmpleadosController()
    {
        repo = new RepositoryEmpleados();
    }

    public IActionResult Index()
    {
        List<Empleado> empleados = this.repo.GetEmpleados();

        return View(empleados);
    }


    public IActionResult Details(int id)
    {
        Empleado empleado = repo.FindEmpleado(id);
        
        return View(empleado);
    }


    public IActionResult BuscadorEmpleado()
    {
      
     return View();   
    }  
    [HttpPost]
    public IActionResult BuscadorEmpleado(string oficio, int salario)
    {
        List<Empleado> empleados = repo.GetEmpleadosOficiosSalario(oficio, salario);
        if (empleados == null)
        {
            ViewData["MENSAJE"]="No existen empleados con oficio " +
                                ""+oficio +" y salario "+salario;
            return  View();
        }
            return View(empleados);
    }

    
    public IActionResult DatosEmpleado()
    {
        List<string> oficios= repo.GetOficios();
        ViewData["OFICIOS"] = oficios;
        return View();
    }
    [HttpPost]
    public IActionResult DatosEmpleado(string  oficio)
    {
        ResumenEmpleado resumen = repo.GetEmpleadosOficio(oficio);
        List<string> oficios= repo.GetOficios();
        ViewData["OFICIOS"] = oficios;
        if (resumen == null)
        {
            ViewData["MENSAJE"]="No hay empleados con el oficio: "+oficio;
            return View();
        }
        return View(resumen);
    }
}
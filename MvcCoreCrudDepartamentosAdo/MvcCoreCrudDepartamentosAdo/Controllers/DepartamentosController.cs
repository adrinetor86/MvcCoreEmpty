using Microsoft.AspNetCore.Mvc;
using MvcCoreCrudDepartamentosAdo.Models;
using MvcCoreCrudDepartamentosAdo.Repositories;

namespace MvcCoreCrudDepartamentosAdo.Controllers;

public class DepartamentosController : Controller
{
    private RepositoryDepartamento repo;

    public DepartamentosController()
    {
        repo = new RepositoryDepartamento();
    }
    
    public async Task<IActionResult> Index()
    {
        List<Departamento> departamentos = await this.repo.GetDepartamentosAsync();
        return View(departamentos);
    }

    
    public async Task<IActionResult> Edit(int idDept)
    {
        Departamento departamento = await this.repo.GetDepartamentoByIdAsync(idDept);
        return View(departamento);
    } 
    
    [HttpPost]
    public async Task<IActionResult> Edit(int idDept,string nombre,string localidad)
    {
        await repo.UpdateDepartamentoByIdAsync(idDept, nombre, localidad);
        
        return RedirectToAction("Index");
    }
    
    
    public async Task<IActionResult> NewDepartamento()
    {
        return View();
    } 
    [HttpPost]
    public async Task<IActionResult> NewDepartamento(int idDept,string nombre,string localidad)
    {

        await repo.CreateDepartamentoByIdAsync(idDept, nombre, localidad);
        return RedirectToAction("Index");
    } 
    
}
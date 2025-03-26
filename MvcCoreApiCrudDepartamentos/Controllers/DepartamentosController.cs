using Microsoft.AspNetCore.Mvc;
using MvcCoreApiCrudDepartamentos.Models;
using MvcCoreApiCrudDepartamentos.Services;

namespace MvcCoreApiCrudDepartamentos.Controllers
{
    public class DepartamentosController : Controller
    {
        private ServiceDepartamentos service;

        public DepartamentosController(ServiceDepartamentos service)
        {
            this.service = service;
        }
        public IActionResult Cliente()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            List<Departamento> data = await this.service.GetDepartamentosAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Departamento dept)
        {
            await this.service.InsertDepartamentoAsync(dept.IdDepartamento, dept.Nombre, dept.Localidad);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Departamento data = await
                this.service.FindDepartamento(id);
            return View(data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Departamento dept = await this.service.FindDepartamento(id);
            return View(dept);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Departamento dept)
        {
            await this.service.UpdateDepartamentoAsync(dept.IdDepartamento, dept.Nombre, dept.Localidad);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteDepartamentoAsync(id);
            return RedirectToAction("Index");
        }
    }
}

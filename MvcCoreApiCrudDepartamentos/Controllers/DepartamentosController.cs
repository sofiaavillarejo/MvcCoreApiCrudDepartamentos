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

    }
}

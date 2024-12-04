using CapaBL;
using CapaEN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DamarisPérez._2024_Prueba.Técnica.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: CategoriaController
        public async Task<ActionResult> Index()
        {
            var categorias = await CategoriaBL.GetAll();
            return View(categorias);
        }

        // GET: CategoriaController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var categoria = await CategoriaBL.GetById(new Categoria { Id = id });
            return View(categoria);
        }

        // GET: CategoriaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await CategoriaBL.Create(categoria);

                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al crear la categoría.");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear la categoría: " + ex.Message);

                if (ex.InnerException != null)
                {
                    ModelState.AddModelError("", "Detalles del error: " + ex.InnerException.Message);
                }
            }

            return View(categoria);
        }

        // GET: CategoriaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var categoria = await CategoriaBL.GetById(new Categoria { Id = id });
            return View(categoria);
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Categoria categoria)
        {
            try
            {
                var result = await CategoriaBL.Update(categoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(categoria);
            }
        }

        // GET: CategoriaController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = await CategoriaBL.GetById(new Categoria { Id = id });
            return View(categoria);
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Categoria categoria)
        {
            try
            {
                var result = await CategoriaBL.Delete(categoria);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(categoria);
            }
        }
    }
}

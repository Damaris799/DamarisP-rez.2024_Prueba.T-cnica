using CapaBL;
using CapaEN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DamarisPérez._2024_Prueba.Técnica.Controllers
{
    public class ProductoController : Controller
    {
        // GET: ProductoController
        public async Task<ActionResult> Index()
        {
            var productos = await ProductoBL.GetAll();
            return View(productos);
        }

        // GET: ProductoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var producto = await ProductoBL.GetById(new Producto { Id = id });
            return View(producto);

        }

        // GET: ProductoController/Create
        public async Task<ActionResult> Create()
        {
            // Obtener la lista de categorías para que el usuario pueda elegir una
            ViewData["CategoriaId"] = new SelectList(await CategoriaBL.GetAll(), "Id", "Nombre");
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Producto producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await ProductoBL.Create(producto);

                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error al crear el producto.");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el producto: " + ex.Message);

                if (ex.InnerException != null)
                {
                    ModelState.AddModelError("", "Detalles del error: " + ex.InnerException.Message);
                }
            }

            // Volver a cargar la lista de categorías en caso de error
            ViewData["CategoriaId"] = new SelectList(await CategoriaBL.GetAll(), "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // GET: ProductoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var producto = await ProductoBL.GetById(new Producto { Id = id });
            if (producto == null)
            {
                return NotFound();
            }

            // Rellenar la lista de categorías para el formulario
            ViewData["CategoriaId"] = new SelectList(await CategoriaBL.GetAll(), "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await ProductoBL.Update(producto);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al editar el producto: " + ex.Message);
            }

            // Rellenar la lista de categorías si hay error
            ViewData["CategoriaId"] = new SelectList(await CategoriaBL.GetAll(), "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // GET: ProductoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var producto = await ProductoBL.GetById(new Producto { Id = id });
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Producto producto)
        {
            try
            {
                var result = await ProductoBL.Delete(producto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(producto);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;
using System.Diagnostics.Metrics;

namespace Shopping.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DataContext _context;
        public CategoriesController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View( await _context.categories.ToListAsync());
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category categories)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.AddAsync(categories);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException mensaje)
                {
                    if (mensaje.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una categoria con el mismo nombre");
                        return View(categories);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, mensaje.InnerException.Message);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category categorias)
        {
            //El id que se recibe por parametro es el que se recibe igual en el Get Edit
            if (id != categorias.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException mensaje)
                {
                    if (mensaje.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un pais con el mismo nombre");
                        return View(categorias);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, mensaje.InnerException.Message);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var categoriaDB =await _context.categories.FindAsync(Id);
            if (categoriaDB == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.categories.Remove(categoriaDB);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
;        }

        
    }
}

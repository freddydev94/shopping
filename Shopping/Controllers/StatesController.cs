using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Models;
using System.Diagnostics.Metrics;

namespace Shopping.Controllers
{
    public class StatesController : Controller
    {
        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }
        public IActionResult AddEstado(int id)
        {
            if(id == null)
            {
                return RedirectToAction("Index","Countries");
            }
            
            StateViewModels estado = new()
            {
                cId = id,
            };

            return View(estado);
        }

        [HttpPost]
        public async Task<IActionResult> AddEstado(StateViewModels estado)
        {
            if (ModelState.IsValid)
            {
                State st = new()
                {
                    countryId = estado.cId,
                    Nombre = estado.Nombre
                };
                await _context.AddAsync(st);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Countries", new { Id = estado.cId });
            }

            return View();
        }

        public async Task<IActionResult> EditState(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.states.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            StateViewModels estado = new()
            {
                Id = state.Id,
                Nombre = state.Nombre,
                cId = state.countryId
            };
            return View(estado);
          
        }

        [HttpPost]
        public async Task<IActionResult> EditState(int id, StateViewModels estado)
        {
            if (id != estado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                 var estadoDB = await _context.states.FindAsync(estado.Id);
                 estadoDB.Nombre = estado.Nombre;
                 _context.Update(estadoDB);
                 await _context.SaveChangesAsync();
                
               
                 return RedirectToAction("Details","Countries",(new {id = estadoDB.countryId}));
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,int idCountry)
        {
            if (id == null)
            {
                return NotFound();
            }
            var state = await _context.states.FindAsync(id);
            _context.states.Remove(state);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details","Countries", new { id = idCountry});
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var states = await _context.states
                .Include(c => c.cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (states == null)
            {
                return NotFound();
            }

            return View(states);
        }

    }
}

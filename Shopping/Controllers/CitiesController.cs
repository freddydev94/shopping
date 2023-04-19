using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Models;

namespace Shopping.Controllers
{
    public class CitiesController : Controller
    {
        private readonly DataContext _context;

        public CitiesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Details", "States", new {id = id});
            }

            CitiesViewModels cities= new()
            {
                statesId = id,
            };
            return View(cities);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CitiesViewModels cities)
        {
            Console.WriteLine(cities.Id);
            
            if (ModelState.IsValid)
            {
                City ciudad = new()
                {
                    statesId = cities.statesId,
                    Nombre = cities.Nombre
                };
                await _context.AddAsync(ciudad);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "States", new { Id = cities.statesId });
            }

            return View(new {id = cities.statesId });

        }


        public async Task<IActionResult> Details(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            return View(await _context.cities
                .FirstOrDefaultAsync(m => m.Id == id));
        }

        public async Task<IActionResult> Delete(int id, int idEstado)
        {
            if(id == null)
            {
                return NotFound();
            }

            var city = await _context.cities.FindAsync(id);
            _context.cities.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details","States", new {id = city.statesId });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            CitiesViewModels cities = new()
            {
                Id = city.Id,
                Nombre = city.Nombre,
                statesId = city.statesId
            };
            return View(cities);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CitiesViewModels city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var cityDB = await _context.cities.FindAsync(city.Id);
                cityDB.Nombre = city.Nombre;
                _context.Update(cityDB);
                await _context.SaveChangesAsync();


                return RedirectToAction("Details", "States", (new { id = cityDB.statesId }));
            }
            return View();

        }
    }
}

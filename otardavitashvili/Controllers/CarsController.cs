using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using otardavitashvili.DB;
using otardavitashvili.Models;

namespace otardavitashvili.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cars.Include(c => c.Person).ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var car = await _context.Cars
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.CarId == id);

            return car == null ? NotFound() : View(car);
        }

        public IActionResult Create()
        {
            ViewBag.Persons = _context.People
                .Select(p => new {
                    p.PersonId,
                    FullName = p.PersonName + " " + p.PersonLastName
                })
                .ToList();

            return View();
        }



        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Model,ReleaseDate,Speed,PersonId")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.CarId = Guid.NewGuid();
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Re-populate ViewBag on validation failure
            ViewBag.Persons = _context.People
                .Select(p => new {
                    p.PersonId,
                    FullName = p.PersonName + " " + p.PersonLastName
                })
                .ToList();

            return View(car);
        }


        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            ViewData["PersonId"] = new SelectList(
                _context.People.Select(p => new {
                    p.PersonId,
                    FullName = p.PersonName + " " + p.PersonLastName
                }),
                "PersonId", "FullName", car.PersonId);

            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CarId,Model,ReleaseDate,Speed,PersonId")] Car car)
        {
            if (id != car.CarId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["PersonId"] = new SelectList(
                _context.People.Select(p => new {
                    p.PersonId,
                    FullName = p.PersonName + " " + p.PersonLastName
                }),
                "PersonId", "FullName", car.PersonId);

            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var car = await _context.Cars
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.CarId == id);

            return car == null ? NotFound() : View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(Guid id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }
    }
}

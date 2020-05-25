using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlquilerCanchas.Database;
using AlquilerCanchas.Models;

namespace AlquilerCanchas.Controllers
{
    public class BarriosController : Controller
    {
        private readonly AlquilerCanchasDbContext _context;

        public BarriosController(AlquilerCanchasDbContext context)
        {
            _context = context;
        }

        // GET: Barrios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Barrio.ToListAsync());
        }

        // GET: Barrios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barrio = await _context.Barrio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barrio == null)
            {
                return NotFound();
            }

            return View(barrio);
        }

        // GET: Barrios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Barrios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] Barrio barrio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(barrio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(barrio);
        }

        // GET: Barrios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barrio = await _context.Barrio.FindAsync(id);
            if (barrio == null)
            {
                return NotFound();
            }
            return View(barrio);
        }

        // POST: Barrios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] Barrio barrio)
        {
            if (id != barrio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barrio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarrioExists(barrio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(barrio);
        }

        // GET: Barrios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barrio = await _context.Barrio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barrio == null)
            {
                return NotFound();
            }

            return View(barrio);
        }

        // POST: Barrios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var barrio = await _context.Barrio.FindAsync(id);
            _context.Barrio.Remove(barrio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarrioExists(int id)
        {
            return _context.Barrio.Any(e => e.Id == id);
        }
    }
}

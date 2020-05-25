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
    public class TurnoCanchasController : Controller
    {
        private readonly AlquilerCanchasDbContext _context;

        public TurnoCanchasController(AlquilerCanchasDbContext context)
        {
            _context = context;
        }

        // GET: TurnoCanchas
        public async Task<IActionResult> Index()
        {
            var alquilerCanchasDbContext = _context.TurnoCancha.Include(t => t.Cancha).Include(t => t.Turno);
            return View(await alquilerCanchasDbContext.ToListAsync());
        }

        // GET: TurnoCanchas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnoCancha = await _context.TurnoCancha
                .Include(t => t.Cancha)
                .Include(t => t.Turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnoCancha == null)
            {
                return NotFound();
            }

            return View(turnoCancha);
        }

        // GET: TurnoCanchas/Create
        public IActionResult Create()
        {
            ViewData["CanchaId"] = new SelectList(_context.Cancha, "Id", "Id");
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Id");
            return View();
        }

        // POST: TurnoCanchas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CanchaId,TurnoId")] TurnoCancha turnoCancha)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turnoCancha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CanchaId"] = new SelectList(_context.Cancha, "Id", "Id", turnoCancha.CanchaId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Id", turnoCancha.TurnoId);
            return View(turnoCancha);
        }

        // GET: TurnoCanchas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnoCancha = await _context.TurnoCancha.FindAsync(id);
            if (turnoCancha == null)
            {
                return NotFound();
            }
            ViewData["CanchaId"] = new SelectList(_context.Cancha, "Id", "Id", turnoCancha.CanchaId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Id", turnoCancha.TurnoId);
            return View(turnoCancha);
        }

        // POST: TurnoCanchas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CanchaId,TurnoId")] TurnoCancha turnoCancha)
        {
            if (id != turnoCancha.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turnoCancha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoCanchaExists(turnoCancha.Id))
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
            ViewData["CanchaId"] = new SelectList(_context.Cancha, "Id", "Id", turnoCancha.CanchaId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Id", turnoCancha.TurnoId);
            return View(turnoCancha);
        }

        // GET: TurnoCanchas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnoCancha = await _context.TurnoCancha
                .Include(t => t.Cancha)
                .Include(t => t.Turno)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnoCancha == null)
            {
                return NotFound();
            }

            return View(turnoCancha);
        }

        // POST: TurnoCanchas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turnoCancha = await _context.TurnoCancha.FindAsync(id);
            _context.TurnoCancha.Remove(turnoCancha);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoCanchaExists(int id)
        {
            return _context.TurnoCancha.Any(e => e.Id == id);
        }
    }
}

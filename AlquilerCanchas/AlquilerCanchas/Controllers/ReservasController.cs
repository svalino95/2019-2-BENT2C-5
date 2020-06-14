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
    public class ReservasController : Controller
    {
        private readonly AlquilerCanchasDbContext _context;

        public ReservasController(AlquilerCanchasDbContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {


            var alquilerCanchasDbContext = _context.Reserva.Include(r => r.Cancha).Include(r => r.Turno).Include(r => r.Usuario);
            return View(await alquilerCanchasDbContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Cancha)
                .Include(r => r.Turno)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["CanchaId"] = new SelectList(_context.Set<Cancha>(), "Id", "Nombre");
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Descripcion");
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Username");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comentarios,CanchaId,UsuarioId,FechaReserva,Monto,TurnoId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CanchaId"] = new SelectList(_context.Set<Cancha>(), "Id", "Nombre", reserva.CanchaId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Descripcion", reserva.TurnoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Username", reserva.UsuarioId);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["CanchaId"] = new SelectList(_context.Set<Cancha>(), "Id", "Nombre", reserva.CanchaId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Descripcion", reserva.TurnoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Username", reserva.UsuarioId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comentarios,CanchaId,UsuarioId,FechaReserva,Monto,TurnoId")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["CanchaId"] = new SelectList(_context.Set<Cancha>(), "Id", "Nombre", reserva.CanchaId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Descripcion", reserva.TurnoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Username", reserva.UsuarioId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Cancha)
                .Include(r => r.Turno)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.Id == id);
        }
    }
}

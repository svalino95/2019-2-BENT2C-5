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
    public class EstadoReservasController : Controller
    {
        private readonly AlquilerCanchasDbContext _context;

        public EstadoReservasController(AlquilerCanchasDbContext context)
        {
            _context = context;
        }

        // GET: EstadoReservas
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadoReserva.ToListAsync());
        }

        // GET: EstadoReservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoReserva = await _context.EstadoReserva
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadoReserva == null)
            {
                return NotFound();
            }

            return View(estadoReserva);
        }

        // GET: EstadoReservas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoReservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] EstadoReserva estadoReserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoReserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoReserva);
        }

        // GET: EstadoReservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoReserva = await _context.EstadoReserva.FindAsync(id);
            if (estadoReserva == null)
            {
                return NotFound();
            }
            return View(estadoReserva);
        }

        // POST: EstadoReservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] EstadoReserva estadoReserva)
        {
            if (id != estadoReserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoReserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoReservaExists(estadoReserva.Id))
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
            return View(estadoReserva);
        }

        // GET: EstadoReservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoReserva = await _context.EstadoReserva
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadoReserva == null)
            {
                return NotFound();
            }

            return View(estadoReserva);
        }

        // POST: EstadoReservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadoReserva = await _context.EstadoReserva.FindAsync(id);
            _context.EstadoReserva.Remove(estadoReserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoReservaExists(int id)
        {
            return _context.EstadoReserva.Any(e => e.Id == id);
        }
    }
}

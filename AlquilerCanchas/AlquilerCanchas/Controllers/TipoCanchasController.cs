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
    public class TipoCanchasController : Controller
    {
        private readonly AlquilerCanchasDbContext _context;

        public TipoCanchasController(AlquilerCanchasDbContext context)
        {
            _context = context;
        }

        // GET: TipoCanchas
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoCancha.ToListAsync());
        }

        // GET: TipoCanchas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCancha = await _context.TipoCancha
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoCancha == null)
            {
                return NotFound();
            }

            return View(tipoCancha);
        }

        // GET: TipoCanchas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoCanchas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] TipoCancha tipoCancha)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCancha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCancha);
        }

        // GET: TipoCanchas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCancha = await _context.TipoCancha.FindAsync(id);
            if (tipoCancha == null)
            {
                return NotFound();
            }
            return View(tipoCancha);
        }

        // POST: TipoCanchas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] TipoCancha tipoCancha)
        {
            if (id != tipoCancha.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCancha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCanchaExists(tipoCancha.Id))
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
            return View(tipoCancha);
        }

        // GET: TipoCanchas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCancha = await _context.TipoCancha
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoCancha == null)
            {
                return NotFound();
            }

            return View(tipoCancha);
        }

        // POST: TipoCanchas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoCancha = await _context.TipoCancha.FindAsync(id);
            _context.TipoCancha.Remove(tipoCancha);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoCanchaExists(int id)
        {
            return _context.TipoCancha.Any(e => e.Id == id);
        }
    }
}

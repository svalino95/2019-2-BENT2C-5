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
    public class CanchasController : Controller
    {
        private readonly AlquilerCanchasDbContext _context;

        public CanchasController(AlquilerCanchasDbContext context)
        {
            _context = context;
        }

        // GET: Canchas
        public async Task<IActionResult> Index()
        {
            var alquilerCanchasDbContext = _context.Cancha.Include(c => c.Club).Include(c => c.TipoCancha);
            return View(await alquilerCanchasDbContext.ToListAsync());
        }

        // GET: Canchas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancha = await _context.Cancha
                .Include(c => c.Club)
                .Include(c => c.TipoCancha)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancha == null)
            {
                return NotFound();
            }

            return View(cancha);
        }

        // GET: Canchas/Create
        public IActionResult Create()
        {
            ViewData["ClubId"] = new SelectList(_context.Club, "Id", "Nombre");
            ViewData["TipoCanchaId"] = new SelectList(_context.TipoCancha, "Id", "Descripcion");
            return View();
        }

        // POST: Canchas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,TipoCanchaId,Precio,ClubId")] Cancha cancha)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cancha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClubId"] = new SelectList(_context.Club, "Id", "Nombre", cancha.ClubId);
            ViewData["TipoCanchaId"] = new SelectList(_context.TipoCancha, "Id", "Descripcion", cancha.TipoCanchaId);
            return View(cancha);
        }

        // GET: Canchas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancha = await _context.Cancha.FindAsync(id);
            if (cancha == null)
            {
                return NotFound();
            }
            ViewData["ClubId"] = new SelectList(_context.Club, "Id", "Nombre", cancha.ClubId);
            ViewData["TipoCanchaId"] = new SelectList(_context.TipoCancha, "Id", "Descripcion", cancha.TipoCanchaId);
            return View(cancha);
        }

        // POST: Canchas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,TipoCanchaId,Precio,ClubId")] Cancha cancha)
        {
            if (id != cancha.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cancha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CanchaExists(cancha.Id))
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
            ViewData["ClubId"] = new SelectList(_context.Club, "Id", "Nombre", cancha.ClubId);
            ViewData["TipoCanchaId"] = new SelectList(_context.TipoCancha, "Id", "Descripcion", cancha.TipoCanchaId);
            return View(cancha);
        }

        // GET: Canchas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancha = await _context.Cancha
                .Include(c => c.Club)
                .Include(c => c.TipoCancha)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancha == null)
            {
                return NotFound();
            }

            return View(cancha);
        }

        // POST: Canchas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cancha = await _context.Cancha.FindAsync(id);
            _context.Cancha.Remove(cancha);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Buscar(int? tipoId, string nombre, int? clubId, int? turnoId )
        {
            var canchas = _context
                .Cancha
                .Include(x => x.TipoCancha)
                .Include(x => x.TurnosCancha).ThenInclude(x => x.Turno)
                .Include(x => x.Club)
                .Where(x => x.TipoCancha.Descripcion == nombre)
                /*.Where(x => (string.IsNullOrWhiteSpace(nombre) || x.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                            && (!tipoId.HasValue || x.TipoCanchaId == tipoId.Value)
                            && (!clubId.HasValue || x.ClubId == clubId.Value)
                            && (!turnoId.HasValue || x.TurnosCancha.Any(turno => turno.TurnoId == turnoId.Value)))*/
                .ToList();

            var tipos = _context.TipoCancha.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descripcion,
                Selected = x.Id == tipoId
            }).ToList();
            tipos.Insert(0, new SelectListItem() { Value = string.Empty, Text = "Sin filtro de tipos" });

            var clubs = _context.Club.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Nombre,
                Selected = x.Id == clubId //PUEDE QUE FALTEN LOS OTROS CAMPOS DE CLUB Y PINCHE DIRECCION Y CANCHAS
            }).ToList();
            clubs.Insert(0, new SelectListItem() { Value = string.Empty, Text = "Sin filtro de clubs" });

            var turnos = _context.TurnoCancha.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.TurnoId.ToString(),
                Selected = x.Id == turnoId
            }).ToList();
            turnos.Insert(0, new SelectListItem() { Value = string.Empty, Text = "Sin filtro de turno" });

            
            ViewBag.TipoCanchas = tipos;
            ViewBag.Clubs = clubs;
            ViewBag.Turnos = turnos;

            return View(canchas);
        }
        private bool CanchaExists(int id)
        {
            return _context.Cancha.Any(e => e.Id == id);
        }
    }
}

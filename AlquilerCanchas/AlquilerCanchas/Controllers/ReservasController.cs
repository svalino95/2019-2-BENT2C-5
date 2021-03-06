﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlquilerCanchas.Database;
using AlquilerCanchas.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
            var alquilerCanchasDbContext = _context.Reserva.Include(r => r.Cancha).Include(r => r.Estado).Include(r => r.Turno).Include(r => r.Usuario);
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
                .Include(r => r.Estado)
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
        [HttpGet]

        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancha = _context
                .Cancha
                .Include(x => x.TipoCancha).ThenInclude(x => x.Canchas)
                .FirstOrDefault(x => x.Id == id);



            ViewData["CanchaId"] = new SelectList(_context.Cancha, "Id", "Nombre", cancha.Id);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Descripcion", cancha.TurnosCancha);

            Reserva reserva = new Reserva()
            {
                CanchaId = cancha.Id,
                Cancha = cancha

            };

            return View(reserva);
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CanchaId,UsuarioId,FechaReserva,Comentarios,TurnoId")] Reserva reserva)
        {




            CanchaReservada(reserva);
            ValidaFecha(reserva.FechaReserva);

            /*  var tipoCancha = _context
                  .TipoCancha
                  .Include(x => x.Canchas).ThenInclude(x => x.TipoCancha)
                  .FirstOrDefault(x => x.Id == reserva.Cancha.TipoCancha.Id);*/

            if (ModelState.IsValid)

            {



                reserva.EstadoId = 1;

                // Buscamos en el contexto de Cancha el precio
                var cancha = await _context.Cancha             
                .Include(c => c.TipoCancha)
                .FirstOrDefaultAsync(m => m.Id == reserva.CanchaId);

                //se asigna el precio a la Reserva

                reserva.Monto = cancha.Precio;

                reserva.UsuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); //Asigno la reserva al usuario logueado.
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListadoReserva));
            }
            ViewData["CanchaId"] = new SelectList(_context.Cancha, "Id", "Nombre", reserva.CanchaId);
            ViewData["EstadoId"] = new SelectList(_context.EstadoReserva, "Id", "Descripcion", reserva.EstadoId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Descripcion", reserva.TurnoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Username", reserva.UsuarioId);

            return View(reserva);


            //       return RedirectToAction(nameof(ListadoReserva));

        }

        // GET: Reservas/Cancelar
        [HttpGet]

        public IActionResult Cancelar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reserva reserva =  _context.Reserva.Find(id);

            reserva.EstadoId = 3;

            if (reserva == null)
            {
                return NotFound();
            }


            ViewData["CanchaId"] = new SelectList(_context.Cancha, "Id", "Nombre", reserva.CanchaId);
            ViewData["EstadoId"] = new SelectList(_context.EstadoReserva, "Id", "Descripcion", reserva.EstadoId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Descripcion", reserva.TurnoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Dni", reserva.UsuarioId);

            return View(reserva);
        }

        // POST: Reservas/Cancelar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id , Reserva reserva)
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
                return RedirectToAction(nameof(ListadoReserva));
            }
          
            ViewData["EstadoId"] = new SelectList(_context.EstadoReserva, "Id", "Descripcion", reserva.EstadoId);
          
          

            return RedirectToAction(nameof(ListadoReserva));


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
            ViewData["CanchaId"] = new SelectList(_context.Cancha, "Id", "Nombre", reserva.CanchaId);
            ViewData["EstadoId"] = new SelectList(_context.EstadoReserva, "Id", "Descripcion", reserva.EstadoId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Descripcion", reserva.TurnoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Dni", reserva.UsuarioId);
            return View(reserva);


        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CanchaId,EstadoId,UsuarioId,FechaReserva,Monto,Comentarios,TurnoId")] Reserva reserva)
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
            ViewData["CanchaId"] = new SelectList(_context.Cancha, "Id", "Nombre", reserva.CanchaId);
            ViewData["EstadoId"] = new SelectList(_context.EstadoReserva, "Id", "Descripcion", reserva.EstadoId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "Id", "Descripcion", reserva.TurnoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Username", reserva.UsuarioId);
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
                .Include(r => r.Estado)
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


        [HttpGet]

        //listas las reservas del usuario por ID de logg actual
        public IActionResult ListadoReserva()
        {
            int UsuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var reservas = _context
                .Reserva
                .Include(r => r.Cancha)
                .Include(r => r.Estado)
                .Include(r => r.Turno)
                .Include(r => r.Usuario)
                .Include(r => r.Cancha.TipoCancha)
                 .Where(reserva => reserva.UsuarioId == UsuarioId && reserva.EstadoId != 3)
                .ToList();

            return View(reservas);
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.Id == id);
        }

        private void CanchaReservada(Reserva reserva)
        {
            bool resFecha = false;
            bool resHora = false;

            bool cancha = false;


            cancha = _context.Reserva.Any(e => e.CanchaId == reserva.CanchaId);
            resFecha = _context.Reserva.Any(e => e.FechaReserva == reserva.FechaReserva);
            resHora = _context.Reserva.Any(e => e.TurnoId == reserva.TurnoId);

            if (resFecha && resHora && cancha)
            {

                ModelState.AddModelError(string.Empty, "El turno se encuentra ocupado");





            }

        }
        private void ValidaFecha(DateTime fechaReci)
        {

            if (fechaReci <= DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "La fecha no debe ser menor a hoy");
            }


        }



    }
}
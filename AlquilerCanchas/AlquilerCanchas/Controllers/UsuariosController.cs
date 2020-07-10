using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlquilerCanchas.Database;
using AlquilerCanchas.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AlquilerCanchas.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AlquilerCanchasDbContext _context;

        public UsuariosController(AlquilerCanchasDbContext context)
        {
            _context = context;
        }

        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
      
        public async Task<IActionResult> Login([Bind("Id,Email,Contrasenia")] string email, string password)
        {
            string returnUrl = TempData["returnUrl"] as string;

            Usuario usuario = _context.Usuario.FirstOrDefault(usr => usr.Email == email);

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(password);

                // Se realiza HASH a la contraseña

                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);

    
                //Falta validar que el email exista, sino lanza error
               
                //if (usuario.Contrasenia.SequenceEqual(data))
                if(usuario != null && usuario.Contrasenia.SequenceEqual(data))
                {

                    //creacion de identidad del usuario ingresado
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, email));
                    //Autoriza los roles
                    identity.AddClaim(new Claim(ClaimTypes.Role, usuario.Rol.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    // En este paso se hace para el login del usuario al sistema

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Guardo la fecha de último acceso que es ahora.

                    usuario.FechaUltimoAcceso = DateTime.Now;
                     _context.SaveChanges();

                    

                    if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    //TempData["JustLoggedIn"] = true;
                    // return RedirectToAction("Create", "Reservas");
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            //info incorrecta
            ViewBag.Error = "Usuario o contraseña incorrectos";
            ViewBag.Email = email;
            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        // GET: Usuarios/SignUp
        public IActionResult SignUp()
        {
            return View();
        }


        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewBag.SelectRoles = new SelectList(Enum.GetNames(typeof(Rol)), "Id");
            return View();
        }

        // POST: Usuarios/SignUp
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Id,Username,Contrasenia,Email,Dni,FechaDeNacimineto,Telefono,Rol")] Usuario usuario, string password)
        {
             //valido mail         
            mailExistente(usuario.Email);

            //valido fecha mayor de edad
            validaMayorEdad(usuario.FechaDeNacimineto);


            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);

                usuario.Contrasenia = data;
              

                if (ModelState.IsValid)
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "Usuarios");
                }
            }
            else
            {
                ModelState.AddModelError("Contrasenia", "La contraseña no puede estar vacía");
            }
            ViewBag.SelectRoles = new SelectList(Enum.GetNames(typeof(Rol)), "Id");

            return View(usuario);
        }


    

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Username,Contrasenia,Email,Dni,FechaDeNacimineto,Telefono,Rol")] Usuario usuario, string password)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            //valido mail         
            mailExistente(usuario.Email);

            //valido fecha mayor de edad
            validaMayorEdad(usuario.FechaDeNacimineto);



            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);

                usuario.Contrasenia = data;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }
      
        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }

        private void validaMayorEdad( DateTime fechaRec)
        {
            //se valida la fecha de nacimiento menor a 18 

            if (fechaRec > DateTime.Now.AddYears(-18))
            {
                ModelState.AddModelError(string.Empty, "Debe ser mayor de edad");
            }
        }


        private void mailExistente(string nuevomail)
        {
            if (_context.Usuario.Any(x => Comparar(x.Email, nuevomail)))
            {
                ModelState.AddModelError(string.Empty, "mail no disponible");
            }
        }

        private static bool Comparar(string a, string b) //comparamos los 2 mail el ingresado y si existe uno 
        {
            return a.Where(x => !char.IsWhiteSpace(x)).Select(char.ToUpperInvariant)
                .SequenceEqual(b.Where(x => !char.IsWhiteSpace(x)).Select(char.ToUpperInvariant));
        }

    }
}
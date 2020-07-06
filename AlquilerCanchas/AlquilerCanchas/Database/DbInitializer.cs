using AlquilerCanchas.Database;
using AlquilerCanchas.Models;
using System.Linq;

namespace AlquilerCanchas.Database
{
    public static class DbInitializer
    {
        public static void Initialize(AlquilerCanchasDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Usuario.Any())
            {
                return;   // DB has been seeded
            }

            byte[] data = System.Text.Encoding.ASCII.GetBytes("123456");
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);

            Usuario usuario1 = new Usuario()
            {

                Rol = Rol.Administrador,
                Username = "svalino",
                Contrasenia = data,
                Email = "santiago.valino@gmail.com",
                Dni = "39064563",
                Telefono = "47478888",


            };

            Usuario usuario2 = new Usuario()
            {

                Rol = Rol.Usuario,
                Username = "Prueba",
                Contrasenia = data,
                Email = "santiago.valino@external-market.com.ar",
                Dni = "39064563",
                Telefono = "47478888",


            };
            context.Usuario.Add(usuario1);

            context.Usuario.Add(usuario2);



            var SocieF = new Club()
            {
                Nombre = "Sociedad de Fomento",
                Direccion = "Chile 110"
            };
            context.Club.Add(SocieF);

            var Tipo11 = new TipoCancha()
            {
                Descripcion = "11",
                Id = 1,
            };
            context.TipoCancha.Add(Tipo11);

            var Tipo8 = new TipoCancha()
            {
                Descripcion = "8",
                Id = 2,
            };
            context.TipoCancha.Add(Tipo8);

            var estadoPendiente = new EstadoReserva()
            {

                Descripcion = "Pendiente"
            };
            context.EstadoReserva.Add(estadoPendiente);


            var estadoAceptado = new EstadoReserva()
            {

                Descripcion = "Aceptado"
            };
            context.EstadoReserva.Add(estadoAceptado);
            var estadoCancelado = new EstadoReserva()
            {

                Descripcion = "Cancelado"
            };
            context.EstadoReserva.Add(estadoCancelado);


            var Turno1 = new Turno()
            {
                Descripcion = "20 a 21",
                horaFin = 20,
                horaInicio = 21,
            };
            context.Turno.Add(Turno1);

            var Turno2 = new Turno()
            {
                Descripcion = "21 a 22",
                horaFin = 22,
                horaInicio = 23,
            };
            context.Turno.Add(Turno2);

            var Cancha1 = new Cancha()

            {

                Nombre = "Argentina",
                Precio = 1200,
                TipoCanchaId = 1,
                ClubId = 1

            };
            context.Cancha.Add(Cancha1); 


            context.SaveChanges();
        }
    }
}
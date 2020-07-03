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
            };
            context.TipoCancha.Add(Tipo11);

            var Tipo8 = new TipoCancha()
            {
                Descripcion = "8",
            };
            context.TipoCancha.Add(Tipo8);


            var Cancha1 = new Cancha()
            {
               Nombre = "C1",
               TipoCanchaId = 1,
               Precio = 900,
            };
            context.Cancha.Add(Cancha1);

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
            /*         var autor3 = new Autor()
                     {
                         Nombre = "Andrea",
                         Apellido = "González"
                     };
                     context.Autores.Add(autor3);

                     var editorial = new Editorial() { Nombre = "Planeta" };
                     context.Editoriales.Add(editorial);

                     var libro1 = new Libro()
                     {
                         Titulo = "Un libro de aventuras",
                         Editorial = editorial,
                         Genero = aventura,
                         AnioPublicado = 1997,
                         Stock = 20
                     };
                     context.Libros.Add(libro1);

                     var libro2 = new Libro()
                     {
                         Titulo = "Un libro de terror",
                         Editorial = editorial,
                         Genero = terror,
                         AnioPublicado = 2003,
                         Stock = 10
                     };
                     context.Libros.Add(libro1);

                     var libro3 = new Libro()
                     {
                         Titulo = "Un libro de Sci-fi",
                         Editorial = editorial,
                         Genero = scifi,
                         AnioPublicado = 2015,
                         Stock = 3
                     };
                     context.Libros.Add(libro1);

                     context.LibrosAutores.Add(new LibroAutor() { Autor = autor1, Libro = libro1 }); */



            context.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AlquilerCanchas.Models
{
    public class Usuario
    {
        [Display(Name = "ID del Usuario")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(100, ErrorMessage = "La longitud máxima es de 20 caracteres")]
        [Display(Name = "Nombre de Usuario")]
        public string Username { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public byte[] Contrasenia { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(100, ErrorMessage = "La longitud máxima es de 100 caracteres")]
        [Display(Name = "Correo electrónico")]
        [EmailAddress(ErrorMessage = "El campo debe ser una dirección de correo electrónico")]
        public string Email { get; set; }
      
      
        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(100, ErrorMessage = "La longitud máxima es de 12 caracteres")]
        [Display(Name = "DNI")]
        public string Dni { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaDeNacimineto { get; set; }


        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(100, ErrorMessage = "La longitud máxima es de 100 caracteres")]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Display(AutoGenerateField = false)]
        public DateTime? FechaUltimoAcceso { get; set; }

        [Display(Name = "Listado de reservas")]
        public ICollection<Reserva> Reserva { get; set; }

    }
}
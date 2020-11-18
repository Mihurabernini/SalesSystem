using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Areas.Customers.Models
{
    public class InputModelRegister
    {
        [Required(ErrorMessage = "El campo dni es obligatorio.")]
        public string Nid { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo apellido es obligatorio.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El campo email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es una dirección de correo electrónico válida.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo direccion es obligatorio.")]
        public string Direction { get; set; }
        [Required(ErrorMessage = "El campo telefono es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(([+]?34) ?)?(6(([0-9]{8})|([0-9]{2} [0-9]{6})|([0-9]{2} [0-9]{3} [0-9]{3}))|9(([0-9]{8})|([0-9]{2} [0-9]{6})|([1-9] [0-9]{7})|([0-9]{2} [0-9]{3} [0-9]{3})|([0-9]{2} [0-9]{2} [0-9]{2} [0-9]{2})))", ErrorMessage = " El formato de telefono ingresado no es válido")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public bool Credit { get; set; }
        public byte[] Image { get; set; }
        public int IdClient { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
    }
}

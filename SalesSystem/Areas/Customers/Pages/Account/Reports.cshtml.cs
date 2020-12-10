using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SalesSystem.Areas.Customers.Models;
using SalesSystem.Data;
using SalesSystem.Library;

namespace SalesSystem.Areas.Customers.Pages.Account
{
    [Authorize]
    public class ReportsModel : PageModel
    {
        private LCustomers _customer;
        private static int idClient = 0;
        public static string Money = "€";
        private static string _errorMessage;
        public static InputModelRegister _dataClient;
        //private LCodes _codes;
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public ReportsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _customer = new LCustomers(context);
        }

        public IActionResult OnGet(int id)
        {
            if (idClient == 0)
            {
                idClient = id;
            }
            else
            {
                if (idClient != id)
                {
                    idClient = 0;
                    return Redirect("/Customers/Customers?area=Customers");
                }
            }
            _dataClient = _customer.getTClientReport(id);
            Input = new InputModel
            {
                DataClient = _dataClient,
                ErrorMessage = _errorMessage
            };
            _errorMessage = "";
            return Page();
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
     {
            public string Money { get; set; } = "€";
            [Required(ErrorMessage = "Seleccione una opción")]
            public int RadioOptions { get; set; }
            [Required(ErrorMessage = "El campo pago es obligatorio")]
            [RegularExpression(@"^[0-9]+([.][0-9]+)?€", ErrorMessage = "El pago no es correcto")]
            public Decimal Payment { get; set; }

            public InputModelRegister DataClient { get; set; }
            [TempData]
            public string ErrorMessage { get; set; }
        }
    }
}

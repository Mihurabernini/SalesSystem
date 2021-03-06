﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Areas.Customers.Models
{
    public class TClients
    {
        [Key]
        public int IdClient { get; set; }
        public string Nid { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public bool Credit { get; set; }
        public byte[] Image { get; set; }
        public List<TReports_clients> TReports_clients { get; set; }
    }
}

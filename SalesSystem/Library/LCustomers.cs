using SalesSystem.Areas.Customers.Models;
using SalesSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Library
{
    public class LCustomers : ListObject
    {
       public LCustomers(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<InputModelRegister> getTClients(String valor, int id)
        {
            List<TClients> listTClients;
            var clientsList = new List<InputModelRegister>();

            if (valor == null && id.Equals(0))
            {
                listTClients = _context.TClients.ToList();
            }
            else
            {
                if (id.Equals(0))
                {
                    listTClients = _context.TClients.Where(u => u.Nid.StartsWith(valor) || u.Name.StartsWith(valor) ||
                    u.LastName.StartsWith(valor) || u.Email.StartsWith(valor)).ToList();
                }
                else
                {
                    listTClients = _context.TClients.Where(u => u.IdClient.Equals(id)).ToList();
                }
            }
            if (!listTClients.Count.Equals(0))
            {
                foreach (var item in listTClients)
                {
                    clientsList.Add(new InputModelRegister
                    {
                        IdClient = item.IdClient,
                        Nid = item.Nid,
                        LastName = item.Name,
                        Email = item.Email,
                        Phone = item.Phone,
                        Credit = item.Credit,
                        Direction = item.Direccion,
                        Image = item.Image,
                    });
                }
            }
            return clientsList;
        }

    }
}

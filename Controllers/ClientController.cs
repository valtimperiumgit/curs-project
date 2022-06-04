using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Valtimperium.ViewModels;

namespace Valtimperium.Controllers
{


    [Authorize]
    public class ClientController : Controller
    {
        private readonly ValtimperContext _context;

        public ClientController(ValtimperContext context)
        {
            _context = context;
        }
        public IActionResult Index(int idClient)
        {   
            var orders = _context.Ords.Where(order => order.IdClient == idClient).ToList();
            
            List<CabinetViewModel> model = new List<CabinetViewModel>();

            foreach(var order in orders)
            {
                model.Add(new CabinetViewModel { 
                order = order,
                product = _context.Products.FirstOrDefault(prod => prod.IdProduct == order.IdProduct)

                });
            }

            return View(model);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Valtimperium.ViewModels;

namespace Valtimperium.Controllers
{
      public class MainController : Controller
      {

          private readonly ValtimperContext _context;

          public MainController(ValtimperContext context)
          {
              _context = context;
          }

          public IActionResult Main()
          {
              return View();
          }

          public IActionResult Catalog(string selectType)
          {   
              var types = _context.Types.ToList();

              if (selectType == null || selectType == "Все товары")
              {
                  var products = _context.Products.ToList();
                  List<string> typesName = new List<string>();
                  foreach (var type in types)
                      typesName.Add(type.Name);

                  CatalogViewModel model = new CatalogViewModel
                  {
                      products = products,
                      types = typesName
                  };
                  return View(model);
              }

              else
              {
                 var typeForId = _context.Types.FirstOrDefault(t => t.Name == selectType);
                 var products = _context.Products.Where(p => p.IdType == typeForId.IdType).ToList();
                  List<string> typesName = new List<string>();
                  foreach (var type in types)
                      typesName.Add(type.Name);

                  CatalogViewModel model = new CatalogViewModel
                  {
                      products = products,
                      types = typesName
                  };
                  return View(model);
              }
          }
          public IActionResult DetailsProduct(int id)
          {
              var product = _context.Products.FirstOrDefault(p => p.IdProduct == id);
              return View(product);
          }

          public IActionResult About()
        {
            return View();
        }

          [Authorize]
          [HttpGet]
          public IActionResult OrderProduct(int IdProduct)
          {
              Product product = _context.Products.FirstOrDefault(p => p.IdProduct == IdProduct);
              return View(product);
          }

        [HttpPost]
        public async Task<IActionResult> OrderProductAsync(int idClient, int idProduct, string cardNumber, int pin, int count, string address)
        {

            var product = _context.Products.FirstOrDefault(p => p.IdProduct == idProduct);

            if (ModelState.IsValid)
            {

                
                var card = _context.BankCards.FirstOrDefault(card => card.Number == cardNumber.ToString() && card.Pin == pin);


                if (card == null)
                {
                    ViewData["ErrorLog"] = "Банковская карта с таким номером и пин-кодом не найдена";
                    return View(product);
                }

                else
                {
                    if (card.Money < product.Price * count)
                    {
                        ViewData["ErrorLog"] = "На вашей карте с номером " + cardNumber + " недостаточно средств";
                        return View(product);
                    }

                    card.Money = card.Money - product.Price * count;

                    Ord order = new Ord
                    {
                        IdClient = idClient,
                        IdProduct = idProduct,
                        AdressDelivery = address,
                        DateOrder = DateTime.Now,
                        CountProduct = count,
                        TotalPrice = product.Price * count,

                    };

                    _context.Ords.Add(order);
                    await _context.SaveChangesAsync();
                    return Redirect("/Main/Catalog");
                }
            }
            ViewData["ErrorLog"] = "Введены некорректные данные";
            return View(product);
        }

      }
}


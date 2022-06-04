using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Valtimperium.Controllers
{
   public class LogController : Controller
    {

        private readonly ValtimperContext _context;
    
        public LogController(ValtimperContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Clients.FirstOrDefault(u => u.Login == login && u.Password == password);
                
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                new Claim("Login", user.Login),
                new Claim("Name", user.Name),
                new Claim("Phone", user.Phone.ToString()),
                new Claim("Surname", user.Surname),
                new Claim("IdClient", user.IdClient.ToString())
                    };
                    var identity = new ClaimsIdentity(claims, "login");
                    
                    var principal = new ClaimsPrincipal(identity);
      
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return Redirect("/Main/Catalog");
                }


                ViewData["ErrorLog"] = "Пользователь с таким логином и паролем не найден";
                Client client = new Client { Login = login, Password = password };
                return View(client);

            }

            ViewData["ErrorLog"] = "Введенные логин или пароль некорректны";
                return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Main/Main");
        }


        public IActionResult Register()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Register(string login, string password, string name, string surname, int phone)
        {
            if (ModelState.IsValid)
            {
                var checkPhone = _context.Clients.FirstOrDefault(c => c.Phone == phone.ToString());
                var checkLogin = _context.Clients.FirstOrDefault(c => c.Login == login);

                if (login == password)
                {
                    ViewData["ErrorReg"] = "Пароль и логин должны отличаться";
                    return View();
                }

                if (phone.ToString().Length < 9 || phone.ToString().Length > 9)
                {
                    ViewData["ErrorReg"] = "Телефон должен содержать 9 цифр";
                    return View();
                }

                if (checkPhone == null && checkLogin == null)
                {
                    Client newClient = new Client
                    {
                        Login = login,
                        Phone = phone.ToString(),
                        Name = name,
                        Password = password,
                        Surname = surname,
                    };

                    _context.Clients.Add(newClient);
                    _context.SaveChanges();
                    return Redirect("/Log/Login");
                    


                }

                else
                {
                    ViewData["ErrorReg"] = "Пользователь с таким логином или телефоном уже существует";
                    return View();
                }
            }

            ViewData["ErrorReg"] = "Данные введены некорректно";
            return View();
        } 
    }
}

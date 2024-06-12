using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Xml.Linq;
using SE1726_Group6_A2.Models;

namespace SE1726_Group6_A2.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly MyStoreContext _context;
        private readonly IConfiguration _configuration;

        public LoginModel(MyStoreContext storeContext, IConfiguration configuration)
        {
            _context = storeContext;
            _configuration = configuration;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost(string username, string pass)
        {
            var adminName = _configuration["Staff:Name"];
            var password = _configuration["Staff:Password"];
            var role = _configuration["Staff:Role"];
            if (username == adminName)
            {
                var old = _context.Staffs.FirstOrDefault(x => x.Name == adminName);
                if (old == null)
                {
                    Staff staff = new Staff()
                    {
                        Name = adminName,
                        Password = password,
                        Role = int.Parse(role)
                    };
                    _context.Staffs.Add(staff);
                    _context.SaveChanges();
                }
                var old2 = _context.Staffs.FirstOrDefault(x => x.Name == adminName);
                if (pass == old2.Password)
                {
                    HttpContext.Session.SetString("acc", JsonConvert.SerializeObject(old2));
                    return RedirectToPage("/Home");
                }
                ViewData["content"] = "Wrong Password";
                return Page();

            }
            var login = _context.Staffs.FirstOrDefault(s => s.Name == username);
            if (login == null)
            {
                ViewData["content"] = "No user found";
                return Page();
            }
            if (login.Password == pass)
            {
                HttpContext.Session.SetString("acc", JsonConvert.SerializeObject(login));
                return RedirectToPage("/Home");
            }
            else
            {
                //sai mk
                ViewData["content"] = "Wrong Password";
            }
            return Page();
        }
    }
}

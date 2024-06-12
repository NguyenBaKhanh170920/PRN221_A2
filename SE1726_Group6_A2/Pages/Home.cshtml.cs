using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Principal;
using SE1726_Group6_A2.Models;

namespace SE1726_Group6_A2.Pages
{
    public class HomeModel : PageModel
    {
        private readonly MyStoreContext _storeContext;
        public HomeModel(MyStoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public void OnGet()
        {
        }
    }
}

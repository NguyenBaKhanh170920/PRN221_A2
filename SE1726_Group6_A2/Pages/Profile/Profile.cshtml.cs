using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using SE1726_Group6_A2.Models;

namespace SE1726_Group6_A2.Pages.Profile
{
    public class ProfileModel : PageModel
    {
        private readonly MyStoreContext _storeContext;
        public ProfileModel(MyStoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost(string name, string pass, int role, int id)
        {
            var old = _storeContext.Staffs.FirstOrDefault(x => x.StaffId == id);
            old.Name = name;
            old.Password = pass;
            _storeContext.Update(old);
            _storeContext.SaveChanges();
            HttpContext.Session.SetString("acc", JsonConvert.SerializeObject(old));
            return RedirectToAction(nameof(OnGet));
        }
    }
}

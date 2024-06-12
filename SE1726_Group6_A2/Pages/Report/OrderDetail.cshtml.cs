using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE1726_Group6_A2.Models;

namespace SE1726_Group6_A2.Pages.Report
{
    public class OrderDetailModel : PageModel
    {
        private readonly MyStoreContext _storeContext;
        public OrderDetailModel(MyStoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public void OnGet(int? id)
        {
            var list= _storeContext.OrderDetails.Where(x=>x.OrderId==id).ToList();
            ViewData["data"]=list;
        }
    }
}

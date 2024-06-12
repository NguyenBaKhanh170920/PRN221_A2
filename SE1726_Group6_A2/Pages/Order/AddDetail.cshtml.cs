using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE1726_Group6_A2.Models;
namespace SE1726_Group6_A2.Pages.OrderDetails
{
    public class AddModel : PageModel
    {
        public MyStoreContext context = new MyStoreContext();

        public int orderId { get;set; } = 0;
        public List<Product> products { get; set; }=new List<Product>();
        public IActionResult OnGet(int orderId)
        {
            this.orderId = orderId;
            products = context.Products.ToList();
            return Page();
        }
        public IActionResult OnPost(int OrderId,int ProductId,int Quantity,int UnitPrice)
        {
            OrderDetail od = new OrderDetail
            {
                OrderId =OrderId,
                ProductId=ProductId,
                Quantity=Quantity,
                UnitPrice=UnitPrice
            };
            context.OrderDetails.Add(od);
            context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}

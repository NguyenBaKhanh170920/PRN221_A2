using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1726_Group6_A2.Models;
namespace SE1726_Group6_A2.Pages.Order
{
    public class UpdateDetailModel : PageModel
    {
        public MyStoreContext context = new MyStoreContext();

        [BindProperty]
        public OrderDetail orderDetail { get; set; } = new OrderDetail();

        public IActionResult OnGet(int orderDetailId)
        {

            orderDetail = context.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailId);

            return Page();
        }

        public IActionResult OnPost(int orderDetailId)
        {
            var existingOrderDetail = context.OrderDetails.Find(orderDetail.OrderDetailId);

            existingOrderDetail.ProductId = orderDetail.ProductId;
            existingOrderDetail.Quantity = orderDetail.Quantity;
            existingOrderDetail.UnitPrice = orderDetail.UnitPrice;
            context.SaveChanges();
            return RedirectToPage("/Order/Index");
        }
    }
}

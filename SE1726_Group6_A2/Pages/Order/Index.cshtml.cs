using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1726_Group6_A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SE1726_Group6_A2.Pages.Order
{
    public class IndexModel : PageModel
    {
        public MyStoreContext context = new MyStoreContext();

        public List<Models.Order> orders { get; set; } = new List<Models.Order>();

        public int orderId { get; set; } = 0;
        public List<Models.OrderDetail> orderDetail { get; set; } = new List<Models.OrderDetail>();

        [BindProperty]
        public DateTime? SearchDate { get; set; }

        [BindProperty]
        public DateTime? FilterToDate { get; set; }

        public IActionResult OnGet()
        {
            // Default filter for one month ago to today
            DateTime oneMonthAgo = DateTime.Now.AddMonths(-1);
            orders = context.Orders
                            .Where(o => o.OrderDate >= oneMonthAgo && o.OrderDate <= DateTime.Now)
                            .ToList();
            FilterToDate = DateTime.Now;
            return Page();
        }

        public IActionResult OnPostSearch(DateTime? searchDate)
        {
            SearchDate = searchDate;
            orders = context.Orders
                            .Where(o => o.OrderDate == searchDate)
                            .ToList();
            return Page();
        }

        public IActionResult OnPostFilter(DateTime? to)
        {
            FilterToDate = to;
            DateTime oneMonthAgo = to.HasValue ? to.Value.AddMonths(-1) : DateTime.Now.AddMonths(-1);
            DateTime filterTo = to ?? DateTime.Now;

            orders = context.Orders
                            .Where(o => o.OrderDate >= oneMonthAgo && o.OrderDate <= filterTo)
                            .ToList();
            return Page();
        }

        public IActionResult OnGetDetailOrder(int orderId)
        {
            this.orderId = orderId;
            orders = context.Orders.ToList();
            orderDetail = context.OrderDetails.Include(od => od.Product).Where(od => od.OrderId == orderId).ToList();
            return Page();
        }

        public IActionResult OnGetDeleteOrder(int orderId)
        {
            // Delete order details first
            var orderDetails = context.OrderDetails.Where(od => od.OrderId == orderId);

            var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (orderDetails != null)
            {
                context.OrderDetails.RemoveRange(orderDetails);
            }
            if (order != null)
            {
                context.Orders.Remove(order);
            }
            context.SaveChanges();
            // Refresh orders list
            orders = context.Orders.ToList();
            return Page();
        }

        public IActionResult OnGetDeleteOrderDetail(int orderDetailId)
        {
            var orderDelete = context.OrderDetails.FirstOrDefault(o => o.OrderDetailId == orderDetailId);
            int orderId = orderDelete.OrderId;
            context.OrderDetails.Remove(orderDelete);
            context.SaveChanges();

            orderDetail = context.OrderDetails.Include(od => od.Product).Where(od => od.OrderId == orderId).ToList();
            orders = context.Orders.ToList();
            return Page();
        }
    }
}

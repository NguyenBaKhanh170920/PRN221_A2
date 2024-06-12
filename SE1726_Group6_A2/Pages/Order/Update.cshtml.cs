using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1726_Group6_A2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SE1726_Group6_A2.Pages.Order
{
    public class UpdateModel : PageModel
    {
        private readonly MyStoreContext _context = new MyStoreContext();

        public List<Staff> staffs { get; set; } = new List<Staff>();

        public Models.Order orderUpdate { get; set; } = new Models.Order();

        public string orderDate { get; set; } = "";

        public IActionResult OnGet(int orderId)
        {
            staffs = _context.Staffs.ToList();
            orderUpdate = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);

            orderDate = orderUpdate.OrderDate.ToString("yyyy-MM-dd");
            return Page();
        }

        public IActionResult OnPost(int orderId, DateTime orderDate, int staffId)
        {

            Models.Order order = new Models.Order
            {
                OrderId = orderId,
                OrderDate = orderDate,
                StaffId = staffId
            };
            _context.Orders.Update(order);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}

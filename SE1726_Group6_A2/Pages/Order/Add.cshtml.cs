using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE1726_Group6_A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SE1726_Group6_A2.Pages.Order
{
    public class AddModel : PageModel
    {
        public MyStoreContext context = new MyStoreContext();

        public List<Staff> staffs { get; set; } = new List<Staff>();

        // Property to hold the default order date as a string
        public string OrderDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        // Property to hold the selected staff ID

        // Property to hold the selected staff name

        public IActionResult OnPost()
        {
            Models.Order order = new Models.Order
            {
                OrderDate = DateTime.Now,
                StaffId =2 
                
            };
            context.Orders.Add(order);
            context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}

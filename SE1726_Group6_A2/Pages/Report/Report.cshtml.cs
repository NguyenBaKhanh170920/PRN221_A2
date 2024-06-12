using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SE1726_Group6_A2.Models;

namespace SE1726_Group6_A2.Pages.Report
{
    public class ReportModel : PageModel
    {

        private readonly MyStoreContext _context;
        public ReportModel(MyStoreContext context)
        {
            _context = context;
        }
        public void OnGet(DateTime? date1, DateTime? date2)
        {
            //kiem tra session

            var acc = JsonConvert.DeserializeObject<Staff>(HttpContext.Session.GetString("acc"));
            if (acc != null)
            {
                LogOut();
            }
            Load(date1, date2, acc);

        }

        private IActionResult LogOut()
        {
            return RedirectToPage("/Login/LogOut");
        }
        public void OnPost(DateTime date1, DateTime date2)
        {
            var acc = JsonConvert.DeserializeObject<Staff>(HttpContext.Session.GetString("acc"));
            if (acc != null)
            {
                LogOut();
            }
            Load(date1, date2, acc);
        }
        private void Load(DateTime? date1, DateTime? date2, Staff acc)
        {
            if (date1 == null || date2 == null)
            {
                date1 = DateTime.Now.AddMonths(-1);
                date2 = DateTime.Now;
            }
            ViewData["date1"] = date1;
            ViewData["date2"] = date2;
            if (acc.Role == 1)//admin
            {
                //get all order
                //var data = _context.Orders.Where(x => x.OrderDate < date2 && date1 < x.OrderDate).ToList();
                var data = from a in _context.Orders
                        join b in _context.Staffs on a.StaffId equals b.StaffId
                        where a.OrderDate < date2 && date1 < a.OrderDate
                        select new ViewDTO
                        {
                            Id = a.OrderId,
                            DateTime = a.OrderDate,
                            Name = b.Name
                        };
                ViewData["data"] = data;
            }
            else
            {
                //get my order
                //var data = _context.Orders.Where(x => x.StaffId == acc.StaffId
                //&& x.OrderDate < date2 && date1 < x.OrderDate).ToList();
                var data = from a in _context.Orders
                       join b in _context.Staffs on a.StaffId equals b.StaffId
                       where a.OrderDate < date2 && date1 < a.OrderDate &&a.StaffId == acc.StaffId
                       select new ViewDTO
                       {
                           Id = a.OrderId,
                           DateTime = a.OrderDate,
                           Name = b.Name
                       };
                ViewData["data"] = data;
            }
        }
    }
    class ViewDTO
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
    }
}

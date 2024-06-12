using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1726_Group6_A2.Models;

namespace SE1726_Group6_A2.Pages.ManageStaff
{
    public class IndexModel : PageModel
    {
        private readonly MyStoreContext _context;
        [BindProperty(SupportsGet = true)]
        public string SearchByName { get; set; }

        public IndexModel(MyStoreContext context)
        {
            _context = context;
        }


        public IList<Staff> Staff { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var staff = from s in _context.Staffs
                        select s;

            if (!string.IsNullOrEmpty(SearchByName))
            {
                staff = staff.Where(s => s.Name.Contains(SearchByName));
            }

            Staff = await staff.ToListAsync();
        }
        

    }
}

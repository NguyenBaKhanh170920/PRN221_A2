using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SE1726_Group6_A2.Models;

namespace SE1726_Group6_A2.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly MyStoreContext _context;

        public IndexModel(MyStoreContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; } = default!;

        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Price { get; set; }
        public async Task OnGetAsync()
        {
            Product = await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
        }

        public async Task OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Price))
            {
                Product = await _context.Products
                    .Where(p => p.ProductName.Contains(Name))
                    .Include(p => p.Category)
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(Price) && string.IsNullOrEmpty(Name))
            {
                int.TryParse(Price, out int price);

                Product = await _context.Products
                    .Where(p => p.UnitPrice == price)
                    .Include(p => p.Category)
                    .ToListAsync();
            }
            else if(!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Price))
            {
                int.TryParse(Price, out int price);
                Product = await _context.Products
                    .Where(p => p.UnitPrice == price && p.ProductName.Contains(Name))
                    .Include(p => p.Category)
                    .ToListAsync();
            }
            else
            {
                Product = await _context.Products.ToListAsync();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TabloidMVC.Data;
using TabloidMVC.Models;

namespace TabloidMVC.Views.Categories
{
    public class IndexModel : PageModel
    {
        private readonly TabloidMVC.Data.TabloidMVCContext _context;

        public IndexModel(TabloidMVC.Data.TabloidMVCContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}

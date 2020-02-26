using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEDT.Data;
using projetEDT.Models;

namespace projetEDT.Pages.UEs
{
    public class DetailsModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;

        public DetailsModel(projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public UE UE { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UE = await _context.UE.FirstOrDefaultAsync(m => m.ID == id);

            if (UE == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

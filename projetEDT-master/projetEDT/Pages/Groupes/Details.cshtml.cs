using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEDT.Data;
using projetEDT.Models;

namespace projetEDT.Pages.Groupes
{
    public class DetailsModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;

        public DetailsModel(projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Groupe Groupe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Groupe = await _context.Groupe
                .Include(g => g.LUE).FirstOrDefaultAsync(m => m.ID == id);

            if (Groupe == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

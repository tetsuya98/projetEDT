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

namespace projetEDT.Pages.Salles
{
    public class DetailsModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;

        public DetailsModel(projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Salle Salle { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Salle = await _context.Salle
                .Include(s => s.LeBatiment).FirstOrDefaultAsync(m => m.ID == id);

            if (Salle == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

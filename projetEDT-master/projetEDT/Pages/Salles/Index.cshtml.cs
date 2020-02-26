using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEDT.Data;
using projetEDT.Models;

namespace projetEDT.Pages.Salles
{
    public class IndexModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;

        public IndexModel(projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Salle> Salle { get;set; }

        public async Task OnGetAsync()
        {
            Salle = await _context.Salle
                .Include(s => s.LeBatiment).ToListAsync();
        }
    }
}

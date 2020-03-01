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
    public class IndexModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;

        public IndexModel(projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Groupe> Groupe { get;set; }

        public async Task OnGetAsync()
        {
            Groupe = await _context.Groupe
                .Include(g => g.LUE).ToListAsync();
        }
    }
}

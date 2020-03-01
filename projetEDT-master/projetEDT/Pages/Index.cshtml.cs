using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using projetEDT.Models;

namespace projetEDT.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly projetEDT.Data.ApplicationDbContext _context;


        public IndexModel(ILogger<IndexModel> logger, projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Seance> Seance { get; set; }

        public async Task OnGetAsync()
        {
            Seance = await _context.Seance
                .Include(s => s.LUE)
                .Include(s => s.LaSalle)
                .ThenInclude(s => s.LeBatiment)
                .Include(s => s.LeGroupe)
                .Include(s => s.Type).ToListAsync();
        }

        /*public void OnGet()
        {

        }*/

    }
}

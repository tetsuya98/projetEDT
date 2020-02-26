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
    public class IndexModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;

        public IndexModel(projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UE> UE { get;set; }

        public async Task OnGetAsync()
        {
            UE = await _context.UE.ToListAsync();
        }
    }
}

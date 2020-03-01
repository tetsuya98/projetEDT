using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using projetEDT.Data;
using projetEDT.Models;

namespace projetEDT.Pages.Salles
{
    [Authorize(Roles = "Gestionnaire")]
    public class CreateModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;

        public CreateModel(projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BatimentID"] = new SelectList(_context.Batiment, "ID", "NomBatiment");
            return Page();
        }

        [BindProperty]
        public Salle Salle { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Salle.Add(Salle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetEDT.Data;
using projetEDT.Models;

namespace projetEDT.Pages.Batiments
{
    [Authorize(Roles = "Gestionnaire")]
    public class EditModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;

        public EditModel(projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Batiment Batiment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Batiment = await _context.Batiment.FirstOrDefaultAsync(m => m.ID == id);

            if (Batiment == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Batiment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatimentExists(Batiment.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BatimentExists(int id)
        {
            return _context.Batiment.Any(e => e.ID == id);
        }
    }
}

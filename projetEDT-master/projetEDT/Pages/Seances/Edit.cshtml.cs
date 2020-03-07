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

namespace projetEDT.Pages.Seances
{
    [Authorize(Roles = "Gestionnaire")]
    public class EditModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;
        public List<Groupe> listGroupes = new List<Groupe>();

        public EditModel(projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Seance Seance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Seance = await _context.Seance
                .Include(s => s.LUE)
                .Include(s => s.LaSalle)
                .ThenInclude(s => s.LeBatiment)
                .Include(s => s.LeGroupe)
                .Include(s => s.Type).FirstOrDefaultAsync(m => m.ID == id);

            if (Seance == null)
            {
                return NotFound();
            }
           ViewData["UEID"] = new SelectList(_context.UE, "ID", "Intitule");
           ViewData["SalleID"] = new SelectList(_context.Salle, "ID", "toString");
           //ViewData["GroupeID"] = new SelectList(_context.Groupe, "ID", "toString");
           ViewData["TypeID"] = new SelectList(_context.TypeSeance, "ID", "Intitule");
            var Groupes = _context.Groupe.ToList();
            /*Groupe nullGrp = new Groupe();
            nullGrp.ID = -1;
            nullGrp.NomGroupe = "Tous le Monde";
            listGroupes.Add(nullGrp);*/

            foreach (Groupe grp in Groupes)
            {
                listGroupes.Add(grp);
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

            _context.Attach(Seance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeanceExists(Seance.ID))
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

        private bool SeanceExists(int id)
        {
            return _context.Seance.Any(e => e.ID == id);
        }
    }
}

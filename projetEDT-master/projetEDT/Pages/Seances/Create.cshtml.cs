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

namespace projetEDT.Pages.Seances
{
    [Authorize(Roles = "Gestionnaire")]
    public class CreateModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;
        public string test;

        public List<Groupe> listGroupes = new List<Groupe>(); 

        public CreateModel(projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["UEID"] = new SelectList(_context.UE, "ID", "Intitule");
            ViewData["SalleID"] = new SelectList(_context.Salle, "ID", "toString");
            //ViewData["GroupeID"] = new SelectList(_context.Groupe, "ID", "NomGroupe");
            ViewData["TypeID"] = new SelectList(_context.TypeSeance, "ID", "Intitule");
            var Groupes = _context.Groupe.ToList();
            /*Groupe nullGrp = new Groupe();
            nullGrp.ID = -1;
            nullGrp.NomGroupe = "Tout le Monde";
            nullGrp.LUE = new UE();
            listGroupes.Add(nullGrp);*/

            foreach (Groupe grp in Groupes)
            {
                listGroupes.Add(grp);
            }


            return Page();
        }

        [BindProperty]
        public Seance Seance { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Seance.GroupeID == (-1))
            {
                Seance.GroupeID = -1;
            }
           // if (Seance.LeGroupe.toString.Contains(Seance.LUE.Intitule))
           // {
                  _context.Seance.Add(Seance);
                 await _context.SaveChangesAsync();

                 return RedirectToPage("./Index");
            //}
            //else
            //{
               // return Page();
           // }


         

            
        }
    }
}

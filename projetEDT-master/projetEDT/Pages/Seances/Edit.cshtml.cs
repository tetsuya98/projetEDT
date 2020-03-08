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
        public bool testSalle = false;
        public bool testGrp = false;

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

            /*_context.Attach(Seance).State = EntityState.Modified;

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

            return RedirectToPage("./Index");*/

            DateTime lejour = Seance.Jour;
            int idsalle = Seance.SalleID;
            int idgrp = (int)Seance.GroupeID;
            DateTime lheure = Seance.HeureDebut;
            int? SID = Seance.ID;
            int cpt = 0;
            int cpt2 = 0;

            var seance = from s in _context.Seance where s.Jour == lejour select s;
            seance = seance.Where(s => s.SalleID == idsalle); //Toute les séance avec la même salle le même jour
            seance = seance.Where(s => s.ID != SID); //Pour qu'il puisse se sauvegarder sans modification

            foreach (Seance item in seance) //Vérifie que 2 séances avec la même salle ne se chevauche pas
            {
                var diff = (item.HeureDebut.TimeOfDay - Seance.HeureDebut.TimeOfDay).TotalHours;
                if (diff != 0)
                {
                    if (diff < Seance.Duree && diff > 0)
                    {
                        cpt += 1;
                        //Console.WriteLine("cpt : {0}, diff : {1}, cette seance : {2}, les seances : {3}, duree : {4}", cpt, diff, Seance.HeureDebut, item.HeureDebut, Seance.Duree);
                    }
                    if (diff > (item.Duree * -1) && diff < 0)
                    {
                        cpt += 1;
                        //Console.WriteLine("cpt : {0}, diff : {1}, cette seance : {2}, les seances : {3}, duree {4}", cpt, diff, Seance.HeureDebut, item.HeureDebut, item.Duree);
                    }

                }
                else
                {
                    cpt += 1;
                }

            }

            seance = from s in _context.Seance where s.Jour == lejour select s;
            seance = seance.Where(s => s.GroupeID == idgrp); //Toute les séance avec le même groupe le même jour
            seance = seance.Where(s => s.ID != SID); //Pour qu'il puisse se sauvegarder sans modification

            foreach (Seance item in seance) //Vérifie que 2 séances avec le même groupe ne se chevauche pas
            {
                var diff = (item.HeureDebut.TimeOfDay - Seance.HeureDebut.TimeOfDay).TotalHours; //différence entre les herues de début
                if (diff != 0)
                {
                    if (diff < Seance.Duree && diff > 0)
                    {
                        cpt2 += 1;
                    }
                    if (diff > (item.Duree * -1) && diff < 0)
                    {
                        cpt2 += 1;
                    }

                }
                else
                {
                    cpt2 += 1;
                }

            }

            if (cpt == 0 && cpt2 == 0)
            {

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
            else
            {
                if (cpt != 0)
                {
                    testSalle = true;
                }
                if (cpt2 != 0)
                {
                    testGrp = true;
                }

                _context.Attach(Seance).State = EntityState.Modified;
                
                OnGetAsync(SID);
                return Page();
            }


        }

        private bool SeanceExists(int id)
        {
            return _context.Seance.Any(e => e.ID == id);
        }
    }
}

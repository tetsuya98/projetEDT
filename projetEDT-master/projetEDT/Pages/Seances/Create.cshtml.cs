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
        public bool testSalle = false;
        public bool testGrp = false;

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
            Groupe nullGrp = new Groupe();
            nullGrp.ID = -1;
            nullGrp.NomGroupe = "Tout le Monde";
            nullGrp.LUE = new UE();
            listGroupes.Add(nullGrp);

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

            if (Seance.GroupeID == (-1)) //Ne sert à rien car j'ai commenter le groupe Tout le Monde
            {
                Seance.GroupeID = null;
            }

            DateTime lejour = Seance.Jour;
            int idsalle = Seance.SalleID;
            int idgrp = (int)Seance.GroupeID;
            DateTime lheure = Seance.HeureDebut;
            int cpt = 0;
            int cpt2 = 0;

            var seance = from s in _context.Seance where s.Jour == lejour select s;
            seance = seance.Where(s => s.SalleID == idsalle); //Toute les séance avec la même salle le même jour

            foreach (Seance item in seance) //Vérifie que 2 séances avec la même salle ne se chevauche pas
            {

                var diff = (item.HeureDebut.TimeOfDay - Seance.HeureDebut.TimeOfDay).TotalHours;
                Console.WriteLine("diff : {0}", diff);
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
                    
                }else
                {
                    cpt += 1;
                }

            }

            seance = from s in _context.Seance where s.Jour == lejour select s;
            seance = seance.Where(s => s.GroupeID == idgrp); //Toute les séance avec le même groupe le même jour

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
                } else
                {
                    cpt2 += 1;
                }

            }

            if (cpt == 0 && cpt2 == 0)
            {

                _context.Seance.Add(Seance);
                await _context.SaveChangesAsync();

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

                OnGet();
                return Page();
            }


        }
    }
}

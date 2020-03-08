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
            var Groupes = _context.Groupe.ToList(); //Je récupère les groupes pour avoir leur UE aves
            Groupe nullGrp = new Groupe(); //Je crée un groupe Tous le Monde
            nullGrp.ID = -1;
            nullGrp.NomGroupe = "Tout le Monde";
            nullGrp.LUE = new UE();
            listGroupes.Add(nullGrp);

            foreach (Groupe grp in Groupes) //Je remplis ma liste avec mes groupes
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

            if (Seance.GroupeID == (-1)) //Si le groupe est Tout le Monde
            {
                Seance.GroupeID = null;
            }

            //Je récupère les infos sur la séance que je veux créer
            DateTime lejour = Seance.Jour;
            int idsalle = Seance.SalleID;
            DateTime lheure = Seance.HeureDebut;
            int cpt = 0; //Compteur pour savoir si je peux ou pas créer ma séance
            int cpt2 = 0;

            var seance = from s in _context.Seance where s.Jour == lejour select s;
            seance = seance.Where(s => s.SalleID == idsalle); //Toute les séance avec la même salle le même jour

            foreach (Seance item in seance) //Vérifie que 2 séances avec la même salle ne se chevauche pas
            {

                var diff = (item.HeureDebut.TimeOfDay - Seance.HeureDebut.TimeOfDay).TotalHours;
                Console.WriteLine("diff : {0}", diff);
                if (diff != 0)
                {
                    if (diff < Seance.Duree && diff > 0) //Si ma séance est avant mais en chevauche une après
                    {
                        cpt += 1;
                        //Console.WriteLine("cpt : {0}, diff : {1}, cette seance : {2}, les seances : {3}, duree : {4}", cpt, diff, Seance.HeureDebut, item.HeureDebut, Seance.Duree);
                    }
                    if (diff > (item.Duree * -1) && diff < 0) //Si ma séance est après mais en chevauche une avant
                    {
                        cpt += 1;
                        //Console.WriteLine("cpt : {0}, diff : {1}, cette seance : {2}, les seances : {3}, duree {4}", cpt, diff, Seance.HeureDebut, item.HeureDebut, item.Duree);
                    }
                    
                }else //Si ma séance est en même temps qu'une autre
                {
                    cpt += 1;
                }

            }

            if (Seance.GroupeID == null) //Si mon est Tout le Monde
            {
                int? nl = null;
                seance = from s in _context.Seance where s.Jour == lejour select s;
                seance = seance.Where(s => s.GroupeID == nl); //Toute les séance avec le même groupe le même jour
            } else //Si mon groupe n'est pas Tout le Monde
            {
                int idgrp = (int)Seance.GroupeID;
                seance = from s in _context.Seance where s.Jour == lejour select s;
                seance = seance.Where(s => s.GroupeID == idgrp); //Toute les séance avec le même groupe le même jour
            }

            foreach (Seance item in seance) //Vérifie que 2 séances avec le même groupe ne se chevauche pas
            {
                var diff = (item.HeureDebut.TimeOfDay - Seance.HeureDebut.TimeOfDay).TotalHours; //différence entre les herues de début
                if (diff != 0)
                {
                    if (diff < Seance.Duree && diff > 0) //Si ma séance est avant mais en chevauche une après
                    {
                        cpt2 += 1;
                    }
                    if (diff > (item.Duree * -1) && diff < 0) //Si ma séance est après mais en chevauche une avant
                    {
                        cpt2 += 1;
                    }
                } else
                {
                    cpt2 += 1; //Si ma séance est en même temps qu'une autre
                }

            }

            if (cpt == 0 && cpt2 == 0) //Si il n'y a pas de problème de disponibilité
            {

                _context.Seance.Add(Seance);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else //Sinon
            {
                if (cpt != 0) //Salle non disponible
                {
                    testSalle = true; //Afficher l'indisponibilité
                }
                if (cpt2 != 0) //Groupe non disponible
                {
                    testGrp = true; //Afficher l'indisponibilité
                }

                OnGet(); //Je récupère les entités pour créer une séance
                return Page(); //Je retourne à la page de création de séance
            }


        }
    }
}

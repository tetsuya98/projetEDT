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
        public bool testTime = false;
        public bool testDate = false;

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
            var Groupes = _context.Groupe.ToList(); //Je récupère les groupes pour avoir leur UE avec
            Groupe nullGrp = new Groupe(); //Je créer un groupe Tout le Monde
            nullGrp.ID = -1;
            nullGrp.NomGroupe = "Tout le Monde";
            listGroupes.Add(nullGrp);

            foreach (Groupe grp in Groupes) //Je remplis ma liste avec mes groupes
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
            if (Seance.GroupeID == (-1)) //Si mon groupe est Tout le Monde
            {
                Seance.GroupeID = null;
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

            

            //Je récupère les infos de la séance que je veux créer
            DateTime lejour = Seance.Jour;
            int idsalle = Seance.SalleID;
            DateTime lheure = Seance.HeureDebut;
            int? SID = Seance.ID;
            int cpt = 0; //Compteur pour savoir si il y a des indisponibilités
            int cpt2 = 0;
            int cpt3 = 0;
            int cpt4 = 0;

            DateTime tot = new DateTime(2010, 10, 10, 7, 0, 0); //Pas cours avant 7h
            var diff = (tot.TimeOfDay - Seance.HeureDebut.TimeOfDay).TotalHours;
            Console.WriteLine(diff);
            if (diff > 0)
            {
                cpt3 += 1;
            }

            DateTime tard = new DateTime(2010, 10, 10, 21, 0, 0); //Pad cours après 21h
            diff = (tard.TimeOfDay - Seance.HeureDebut.TimeOfDay).TotalHours;
            Console.WriteLine(diff);
            if (diff <= 0)
            {
                cpt3 += 1;
            }

            if ((int)Seance.Jour.DayOfWeek == 0) //Pas de cours le dimanche
            {
                cpt4 += 4;
            }

            var seance = from s in _context.Seance where s.Jour == lejour select s;
            seance = seance.Where(s => s.SalleID == idsalle); //Toute les séance avec la même salle le même jour
            seance = seance.Where(s => s.ID != SID); //Pour qu'il puisse sauvegarder sans modification

            foreach (Seance item in seance) //Vérifie que 2 séances avec la même salle ne se chevauche pas
            {
                 diff = (item.HeureDebut.TimeOfDay - Seance.HeureDebut.TimeOfDay).TotalHours;
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

                }
                else //Si ma séance est en même temps qu'une autre
                {
                    cpt += 1;
                }

            }

            if (Seance.GroupeID == null) //Si le groupe est Tout le Monde
            {
                int? nl = null;
                seance = from s in _context.Seance where s.Jour == lejour select s;
                seance = seance.Where(s => s.GroupeID == nl); //Toute les séance avec le même groupe le même jour
                seance = seance.Where(s => s.ID != SID); //Pour qu'il puisse se sauvegarder sans modification
            }
            else //Sinon
            {
                int idgrp = (int)Seance.GroupeID;
                seance = from s in _context.Seance where s.Jour == lejour select s;
                seance = seance.Where(s => s.GroupeID == idgrp); //Toute les séance avec le même groupe le même jour
                seance = seance.Where(s => s.ID != SID); //Pour qu'il puisse se sauvegarder sans modification
            }

            

            foreach (Seance item in seance) //Vérifie que 2 séances avec le même groupe ne se chevauche pas
            {
                 diff = (item.HeureDebut.TimeOfDay - Seance.HeureDebut.TimeOfDay).TotalHours; //différence entre les herues de début
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

                }
                else //Si ma séance est en même temps qu'une autre
                {
                    cpt2 += 1;
                }

            }
             
            if (cpt == 0 && cpt2 == 0 && cpt3 == 0 && cpt4 == 0) //Si il n'y a pas d'indisponibilité
            {

                _context.Attach(Seance).State = EntityState.Modified; //Je récupère mes modifications

                try
                {
                    await _context.SaveChangesAsync(); //Je sauvegarde mes modifications dans la base
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

                return RedirectToPage("./Index"); //Je retourne à l'index
            }
            else //Si il y a des indisponibilités
            {
                if (cpt != 0) //Salle non disponible
                {
                    testSalle = true; //affiche l'indisponibilité
                }
                if (cpt2 != 0) //Groupe non disponiblie
                {
                    testGrp = true; //affiche l'indisponibilité
                }
                if (cpt3 != 0) //Trop tôt/tard
                {
                    testTime = true; //Afficher l'indisponibilité
                }
                if (cpt4 != 0) //Cours le Dimanche
                {
                    testDate = true; //Afficher l'indisponibilité
                }

                _context.Attach(Seance).State = EntityState.Modified; //Pour pouvoir recharger la page sans créer de conflit
                
                OnGetAsync(SID); //Je récupère les entités pour pouvoir modifier une séance
                return Page(); //Je recharge la page
            }


        }

        private bool SeanceExists(int id)
        {
            return _context.Seance.Any(e => e.ID == id);
        }
    }
}

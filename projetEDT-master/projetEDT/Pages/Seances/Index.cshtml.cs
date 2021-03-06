﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEDT.Data;
using projetEDT.Models;

namespace projetEDT.Pages.Seances
{
    public class IndexModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;

        public IndexModel(projetEDT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Seance> Seance { get;set; }

        public async Task OnGetAsync()
        {
            Seance = await _context.Seance
                .Include(s => s.LUE)
                .Include(s => s.LaSalle)
                .ThenInclude(s => s.LeBatiment)
                .Include(s => s.LeGroupe)
                .Include(s => s.Type)
                .OrderBy(s => s.Jour)
                .ThenBy(s => s.HeureDebut.TimeOfDay) //TimeOfDay parce que ce DateTime contient la bonne heure mais aussi le jour ou on a créé la séance
                .ToListAsync();
        }
    }
}

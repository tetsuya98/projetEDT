﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using projetEDT.Data;
using projetEDT.Models;

namespace projetEDT.Pages.Batiments
{
    [Authorize(Roles = "Gestionnaire")]
    public class DeleteModel : PageModel
    {
        private readonly projetEDT.Data.ApplicationDbContext _context;

        public DeleteModel(projetEDT.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Batiment = await _context.Batiment.FindAsync(id);

            if (Batiment != null)
            {
                _context.Batiment.Remove(Batiment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

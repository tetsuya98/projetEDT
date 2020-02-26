using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projetEDT.Models;

namespace projetEDT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<projetEDT.Models.Batiment> Batiment { get; set; }
        public DbSet<projetEDT.Models.Groupe> Groupe { get; set; }
        public DbSet<projetEDT.Models.Salle> Salle { get; set; }
        public DbSet<projetEDT.Models.Seance> Seance { get; set; }
        public DbSet<projetEDT.Models.UE> UE { get; set; }
        public DbSet<projetEDT.Models.TypeSeance> TypeSeance { get; set; }
    }
}

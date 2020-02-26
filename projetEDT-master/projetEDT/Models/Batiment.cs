using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEDT.Models
{
    public class Batiment
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Batiment")]
        public String NomBatiment { get; set; }
        public ICollection<Salle> LesSalles { get; set; }

    }
}

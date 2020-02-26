using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEDT.Models
{
    public class TypeSeance
    {
        public int ID { get; set; }
        [Required]
        public String Intitule { get; set; }
        public ICollection<Seance> LesSeances { get; set; }
    }
}

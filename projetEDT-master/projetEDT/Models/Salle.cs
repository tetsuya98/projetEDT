using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEDT.Models
{
    public class Salle
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Salle")]
        public String NomSalle { get; set; }
        [Display(Name = "Batiment")]
        public int BatimentID { get; set; }
        public Batiment LeBatiment { get; set; }
        public string toString
        {
            get
            {
                if (LeBatiment != null)
                {
                    return LeBatiment.NomBatiment + "" + NomSalle;
                }
                return NomSalle;

            }
        }
    }
}

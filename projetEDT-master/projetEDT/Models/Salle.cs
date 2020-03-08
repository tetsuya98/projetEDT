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
        [Display(Name = "Batiment")]
        public Batiment LeBatiment { get; set; }
        public string toString //Affichage du nom de la salle avec son batiment
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

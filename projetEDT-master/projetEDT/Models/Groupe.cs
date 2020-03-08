using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEDT.Models
{
    public class Groupe
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Groupe")]
        public String NomGroupe { get; set; }
        [Display(Name = "UE")]
        public int UEID { get; set; }
        [Display(Name = "UE")]
        public UE LUE { get; set; }
        public string toString //Affichage du groupe avec son UE
        {
            get
            {
                if (LUE != null)
                {
                    return NomGroupe + " - " + LUE.Intitule;
                }      
                return NomGroupe;
            }
        }
        public ICollection<Seance> LesSeances { get; set; }
    }
}

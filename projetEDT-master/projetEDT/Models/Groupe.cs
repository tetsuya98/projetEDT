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
        public int UEID { get; set; }
        public UE LUE { get; set; }
        public string toString
        {
            get
            {
                if (LUE != null)
                {
                    return NomGroupe + " - " + LUE.Intitule;
                }
                if (ID == null)
                {
                    return "Tout le Monde";
                }
                return NomGroupe;
            }
        }
        public ICollection<Seance> LesSeances { get; set; }
    }
}

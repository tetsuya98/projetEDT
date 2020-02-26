using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEDT.Models
{
    public class Seance
    {
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Jour { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Heur de Début")]
        public DateTime HeureDebut { get; set; }
        public int Duree { get; set; }
        [Display(Name = "Salle")]
        public int SalleID { get; set; }
        [Display(Name = "Salle")]
        public Salle LaSalle { get; set; }
        [Display(Name = "UE")]
        public int UEID { get; set; }
        [Display(Name = "UE")]
        public UE LUE { get; set; }
        [Display(Name = "Groupe")]
        public int? GroupeID { get; set; }
        [Display(Name = "Groupe")]
        public Groupe LeGroupe { get; set; }
        public int TypeID { get; set; }
        public TypeSeance Type { get; set; }
    }
}

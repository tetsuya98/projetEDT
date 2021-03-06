﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetEDT.Models
{
    public class UE
    {
        public int ID { get; set; }
        [Required]
        public String Intitule { get; set; }
        [Required]
        public String Numero { get; set; }
        public ICollection<Seance> LesSeances { get; set; }
        public ICollection<Groupe> LesGroupes { get; set; }
        [Display(Name = "UE")]
        public string toString //Affichage de l'UE avec son intitule
        {
            get
            {
                return Numero + " - " + Intitule;
            }
        }
    }
}

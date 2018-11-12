﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Uczen
    {
        public int ID { get; set; }
        public int IDKlasa { get; set; }
        public int IDRodzic { get; set; }

        public virtual ICollection<Ocena> Oceny { get; set; }
        public virtual ICollection<Uwaga> Uwagi { get; set; }
        public virtual ICollection<Spoznienia> Spoznienia { get; set; }
        public virtual ICollection<Nieobecnosc> Nieobecnosci { get; set; }


        public virtual Rodzic Rodzic { get; set; }
        public virtual Klasa Klasa { get; set; }


    }

}
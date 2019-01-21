﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    
    public class Ocena
    {
        public int ID { get; set; }
        public double ocena { get; set; }
        public int waga { get; set; }
        public DateTime data { get; set; }
        public string tresc { get; set; }
        public int PrzedmiotID { get; set; }
        public int NauczycielID { get; set; }
        public int UczenID { get; set; }
        public int? IdEdytujacego { get; set; }
        public DateTime? dataEdycji { get; set; }

        public virtual Uczen Uczen { get; set; }
        public virtual Nauczyciel Nauczyciel { get; set; }
        public virtual Przedmiot Przedmiot { get; set; }
       
    }
}
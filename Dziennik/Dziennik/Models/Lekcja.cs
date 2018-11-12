﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Lekcja
    {
        public int ID { get; set; }
        public int IDPrzedmiot { get; set; }
        public DateTime date { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }
    }
}
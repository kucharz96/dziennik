using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Rodzic
    {
        public int ID;

        public virtual ICollection<Ogloszenie_dla_rodzicow> Ogloszenia { get; set; }
        public virtual ICollection<Uczen> Uczniowie { get; set; }
    }
}
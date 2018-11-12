using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Spoznienie
    {
        public int ID { get; set; }
        public int IDUczen { get; set; }
        public int IDLekcja { get; set; }

        public virtual Uczen Uczen { get; set; }
        public virtual Lekcja Lekcja { get; set; }

    }
}
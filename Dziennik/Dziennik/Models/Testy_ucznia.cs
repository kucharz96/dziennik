using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Testy_ucznia
    {
        public int ID { get; set; }
        public int IDUczen { get; set; }
        public int IDTest { get; set; }

        public virtual Uczen Uczen { get; set; }
        public virtual Test Test { get; set; }

    }
}
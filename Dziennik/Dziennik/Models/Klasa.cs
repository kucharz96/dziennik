using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik.Models
{
    public enum klasa
    {
        kl1,kl2,kl3
    }
    public class Klasa
    {
        public int ID { get; set; }
        public string nazwa { get; set; }
        [Key]
        [ForeignKey("Wychowawca")]
        public int IDNauczyciel { get; set; }
        public klasa level { get; set; }

        public virtual Nauczyciel Wychowawca { get; set; }
        public virtual ICollection<Nauczyciel> Nauczyciele { get; set; }
        public virtual ICollection<Uczen> Uczniowie { get; set; }
        public virtual ICollection<Przedmiot> Przedmioty { get; set; }
        public virtual ICollection<Lekcja> Lekcje { get; set; }
        public virtual ICollection<Plik> Pliki{ get; set; }
        public virtual ICollection<Test> Testy { get; set; }
    }
}
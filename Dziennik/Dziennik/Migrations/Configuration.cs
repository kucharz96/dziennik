namespace Dziennik.Migrations
{
    using Dziennik.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Dziennik.DAL.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Dziennik.DAL.Context context)
        {
            Debug.WriteLine("janek");
            var rodzice = new List<Rodzic>
            {
            new Rodzic{imie = "Janusz", nazwisko = "Kucharski", login = "kucharz96", haslo="1234"},
            new Rodzic{imie = "Kamila", nazwisko = "Jarmoc", login = "elkamilaszczy", haslo="1234" },
            new Rodzic{imie = "Maria", nazwisko = "Krasucki", login = "rafonix", haslo="1234" },
            };
            rodzice.ForEach(s => context.Rodzice.Add(s));
            context.SaveChanges();

            var uczniowie = new List<Uczen>
            {
            new Uczen{imie = "Jan", nazwisko = "Kucharski", login = "kucharz96", haslo="1234",RodzicID=1},
            new Uczen{imie = "Kamil", nazwisko = "Jarmoc", login = "elkamilaszczy", haslo="1234",RodzicID=2 },
            new Uczen{imie = "Marcin", nazwisko = "Krasucki", login = "rafonix", haslo="1234",RodzicID=3 },
            };

            uczniowie.ForEach(s => context.Uczniowie.Add(s));
            context.SaveChanges();

            var administratorzy = new List<Administrator>
            {
            new Administrator{imie = "Jan", nazwisko = "Kowalski", login = "kucharz96", haslo="1234"},
            
            };

            administratorzy.ForEach(s => context.Administratorzy.Add(s));
            context.SaveChanges();



        }
    }
}

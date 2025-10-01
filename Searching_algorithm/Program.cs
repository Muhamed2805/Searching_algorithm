using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplikacija
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string izbor = PrikaziMeni();

                if (izbor.Equals("T", StringComparison.OrdinalIgnoreCase))
                {
                    PokreniTakmicenje();
                }
                else if (izbor.Equals("P", StringComparison.OrdinalIgnoreCase))
                {
                    PokreniKorisnickuPretragu();
                }
                else
                {
                    Console.WriteLine(" Nepoznata opcija, pokušajte ponovo.\n");
                    continue;
                }

                Console.WriteLine("\nŽelite li završiti program? (Y/N)");
                string izlaz = Console.ReadLine() ?? "N";
                if (izlaz.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }

         
        // MENI
         
        private static string PrikaziMeni()
        {
            Console.WriteLine("=== Glavni Meni ===");
            Console.WriteLine("[T] Takmičenje algoritama");
            Console.WriteLine("[P] Korisnička pretraga");
            Console.Write("Odaberite opciju: ");

            return Console.ReadLine() ?? "";
        }

         
        // TAKMIČENJE ALGORITAMA
         
        private static void PokreniTakmicenje()
        {
            Console.WriteLine("\n--- Takmičenje algoritama ---");

            var osobe = UcitajOsobe();

            // Generiši nasumičan JMBG za pretragu
            Random rnd = new Random();
            int target = osobe[rnd.Next(osobe.Count)].DajJMBG();
            Console.WriteLine($"Pretražujemo JMBG: {target}");

            // Dictionary za hash pretragu
            var mapa = osobe.ToDictionary(o => o.DajJMBG(), o => o);

            // Pokreni sve algoritme
            var rezultati = new List<(string ime, long vrijeme, Osob? osoba)>
            {
                ("Linear Search",    Algoritmi.Izmjeri(() => Algoritmi.LinearSearch(osobe, target))),
                ("Binary Search",    Algoritmi.Izmjeri(() => Algoritmi.BinarySearch(osobe, target))),
                ("Interpolation",    Algoritmi.Izmjeri(() => Algoritmi.InterpolationSearch(osobe, target))),
                ("Jump Search",      Algoritmi.Izmjeri(() => Algoritmi.JumpSearch(osobe, target))),
                ("Ternary Search",   Algoritmi.Izmjeri(() => Algoritmi.TernarySearch(osobe, 0, osobe.Count-1, target))),
                ("Exponential",      Algoritmi.Izmjeri(() => Algoritmi.ExponentialSearch(osobe, target))),
                ("Fibonacci",        Algoritmi.Izmjeri(() => Algoritmi.FibonacciSearch(osobe, target))),
                ("Hash Search",      Algoritmi.Izmjeri(() => Algoritmi.HashSearch(mapa, target))),
            };

            Console.WriteLine("\n=== Rezultati ===");
            foreach (var (ime, vrijeme, osoba) in rezultati)
            {
                Console.WriteLine($"{ime,-20} | Vrijeme: {vrijeme,8} ticks | Pronađeno: {(osoba != null ? osoba.ToString() : "Nema rezultata")}");
            }
        }

         
        // KORISNIČKA PRETRAGA
         
        private static void PokreniKorisnickuPretragu()
        {
            Console.WriteLine("\n--- Korisnička pretraga ---");

            var osobe = UcitajOsobe();
            var mapa = osobe.ToDictionary(o => o.DajJMBG(), o => o);

            Console.Write("Unesite JMBG za pretragu: ");
            string unos = Console.ReadLine() ?? "";

            if (!int.TryParse(unos, out int jmbg))
            {
                Console.WriteLine(" Pogrešan unos.");
                return;
            }

            Console.WriteLine("\nOdaberite algoritam:");
            Console.WriteLine("1 - Linear Search");
            Console.WriteLine("2 - Binary Search");
            Console.WriteLine("3 - Interpolation");
            Console.WriteLine("4 - Jump Search");
            Console.WriteLine("5 - Ternary Search");
            Console.WriteLine("6 - Exponential");
            Console.WriteLine("7 - Fibonacci");
            Console.WriteLine("8 - Hash Search");
            Console.Write("Izbor: ");

            string izbor = Console.ReadLine() ?? "";
            Osob? rezultat = null;

            switch (izbor)
            {
                case "1": rezultat = Algoritmi.LinearSearch(osobe, jmbg); break;
                case "2": rezultat = Algoritmi.BinarySearch(osobe, jmbg); break;
                case "3": rezultat = Algoritmi.InterpolationSearch(osobe, jmbg); break;
                case "4": rezultat = Algoritmi.JumpSearch(osobe, jmbg); break;
                case "5": rezultat = Algoritmi.TernarySearch(osobe, 0, osobe.Count-1, jmbg); break;
                case "6": rezultat = Algoritmi.ExponentialSearch(osobe, jmbg); break;
                case "7": rezultat = Algoritmi.FibonacciSearch(osobe, jmbg); break;
                case "8": rezultat = Algoritmi.HashSearch(mapa, jmbg); break;
                default: Console.WriteLine(" Nepoznat algoritam."); return;
            }

            Console.WriteLine("\nRezultat pretrage:");
            Console.WriteLine(rezultat != null ? rezultat.ToString() : "Nije pronađeno.");
        }

         
        // POMOĆNE METODE
         
        private static List<Osob> UcitajOsobe()
        {
            BazaPodataka baza = new Fake();
            var osobe = new List<Osob>();

            for (int i = 0; i < baza.ime().Length; i++)
            {
                osobe.Add(new Osob(
                    baza.ime()[i],
                    baza.prezime()[i],
                    DateTime.Parse(baza.datum_rodenja()[i]),
                    baza.visina()[i],
                    baza.tezina()[i],
                    baza.jmbg()[i]
                ));
            }

            // Sortirano po JMBG jer većina algoritama to traži
            osobe.Sort();
            return osobe;
        }
    }

     
    // FAKE BAZA (test podaci)
     
    public class Fake : BazaPodataka
    {
        public string[] ime() => new[] { "Amer", "Hašim", "Muhamed", "Edin" };
        public string[] prezime() => new[] { "Mehmedić", "Čosić", "Karamujić", "Džeko" };
        public string[] datum_rodenja() => new[] { "1999-01-06", "1900-01-01", "1900-01-01", "1986-03-17" };
        public int[] visina() => new[] { 180, 185, 180, 193 };
        public int[] tezina() => new[] { 100, 80, 80, 80 };
        public int[] jmbg() => new[] { 1999932, 2324253, 23242643, 1233129 };
    }
}

using System;

namespace Aplikacija
{
    public class Osob : IComparable<Osob>
    {
        // Properties (javne, mogu se čitati, ali ne mijenjati izvana)
        public string Ime { get; private set; } = "Mujo";
        public string Prezime { get; private set; } = "Mujic";
        public DateTime DatumRodjenja { get; private set; } = new DateTime(1900, 1, 1);
        public int Visina { get; private set; } = 180;
        public int Tezina { get; private set; } = 100;
        public int JMBG { get; private set; }

        // Konstruktor samo sa JMBG-om
        public Osob(int jmbg)
        {
            JMBG = jmbg;
        }

        // Konstruktor sa svim podacima
        public Osob(string ime, string prezime, DateTime datumRodjenja, int visina, int tezina, int jmbg)
        {
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            Visina = visina;
            Tezina = tezina;
            JMBG = jmbg;
        }

        // Implementacija IComparable – poređenje po JMBG-u
        public int CompareTo(Osob? drugaOsoba)
        {
            if (drugaOsoba == null) return 1;
            return JMBG.CompareTo(drugaOsoba.JMBG);
        }

        // Lijep ispis
        public override string ToString()
        {
            return $"{Ime} {Prezime}, JMBG: {JMBG}, Rođen: {DatumRodjenja:yyyy-MM-dd}, Visina: {Visina} cm, Težina: {Tezina} kg";
        }
    }

    // Interfejs baze podataka
    public interface BazaPodataka
    {
        string[] ime();
        string[] prezime();
        string[] datum_rodenja();
        int[] visina();
        int[] tezina();
        int[] jmbg();
    }
}

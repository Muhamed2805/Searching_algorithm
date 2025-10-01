using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Aplikacija
{
    public static class Algoritmi
    {
        // LINEAR SEARCH
        public static Osob? LinearSearch(List<Osob> lista, int jmbg)
        {
            foreach (var o in lista)
            {
                if (o.DajJMBG() == jmbg)
                    return o;
            }
            return null;
        }

        // BINARY SEARCH
         
        public static Osob? BinarySearch(List<Osob> lista, int jmbg)
        {
            int left = 0, right = lista.Count - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                int cmp = lista[mid].DajJMBG().CompareTo(jmbg);

                if (cmp == 0) return lista[mid];
                else if (cmp < 0) left = mid + 1;
                else right = mid - 1;
            }
            return null;
        }

         
        // INTERPOLATION SEARCH
         
        public static Osob? InterpolationSearch(List<Osob> lista, int jmbg)
        {
            int low = 0, high = lista.Count - 1;

            while (low <= high && jmbg >= lista[low].DajJMBG() && jmbg <= lista[high].DajJMBG())
            {
                if (low == high)
                {
                    if (lista[low].DajJMBG() == jmbg) return lista[low];
                    return null;
                }

                int pos = low + (int)((double)(high - low) / (lista[high].DajJMBG() - lista[low].DajJMBG()) * (jmbg - lista[low].DajJMBG()));

                if (lista[pos].DajJMBG() == jmbg) return lista[pos];
                if (lista[pos].DajJMBG() < jmbg) low = pos + 1;
                else high = pos - 1;
            }
            return null;
        }

         
        // JUMP SEARCH
         
        public static Osob? JumpSearch(List<Osob> lista, int jmbg)
        {
            int n = lista.Count;
            int step = (int)Math.Sqrt(n);
            int prev = 0;

            while (lista[Math.Min(step, n) - 1].DajJMBG() < jmbg)
            {
                prev = step;
                step += (int)Math.Sqrt(n);
                if (prev >= n) return null;
            }

            while (prev < Math.Min(step, n))
            {
                if (lista[prev].DajJMBG() == jmbg) return lista[prev];
                prev++;
            }

            return null;
        }

         
        // TERNARY SEARCH
         
        public static Osob? TernarySearch(List<Osob> lista, int left, int right, int jmbg)
        {
            if (right >= left)
            {
                int mid1 = left + (right - left) / 3;
                int mid2 = right - (right - left) / 3;

                if (lista[mid1].DajJMBG() == jmbg) return lista[mid1];
                if (lista[mid2].DajJMBG() == jmbg) return lista[mid2];

                if (jmbg < lista[mid1].DajJMBG()) return TernarySearch(lista, left, mid1 - 1, jmbg);
                else if (jmbg > lista[mid2].DajJMBG()) return TernarySearch(lista, mid2 + 1, right, jmbg);
                else return TernarySearch(lista, mid1 + 1, mid2 - 1, jmbg);
            }
            return null;
        }

         
        // EXPONENTIAL SEARCH
         
        public static Osob? ExponentialSearch(List<Osob> lista, int jmbg)
        {
            if (lista[0].DajJMBG() == jmbg) return lista[0];

            int i = 1;
            while (i < lista.Count && lista[i].DajJMBG() <= jmbg)
                i *= 2;

            return BinarySearch(lista.GetRange(i / 2, Math.Min(i, lista.Count) - i / 2), jmbg);
        }

         
        // FIBONACCI SEARCH
         
        public static Osob? FibonacciSearch(List<Osob> lista, int jmbg)
        {
            int n = lista.Count;

            int fibMMm2 = 0;   // (m-2)'th Fibonacci No.
            int fibMMm1 = 1;   // (m-1)'th Fibonacci No.
            int fibM = fibMMm2 + fibMMm1;  // m'th Fibonacci

            while (fibM < n)
            {
                fibMMm2 = fibMMm1;
                fibMMm1 = fibM;
                fibM = fibMMm2 + fibMMm1;
            }

            int offset = -1;

            while (fibM > 1)
            {
                int i = Math.Min(offset + fibMMm2, n - 1);

                if (lista[i].DajJMBG() < jmbg)
                {
                    fibM = fibMMm1;
                    fibMMm1 = fibMMm2;
                    fibMMm2 = fibM - fibMMm1;
                    offset = i;
                }
                else if (lista[i].DajJMBG() > jmbg)
                {
                    fibM = fibMMm2;
                    fibMMm1 = fibMMm1 - fibMMm2;
                    fibMMm2 = fibM - fibMMm1;
                }
                else return lista[i];
            }

            if (fibMMm1 == 1 && lista[offset + 1].DajJMBG() == jmbg)
                return lista[offset + 1];

            return null;
        }

         
        // HASH SEARCH (Dictionary)
         
        public static Osob? HashSearch(Dictionary<int, Osob> mapa, int jmbg)
        {
            return mapa.TryGetValue(jmbg, out Osob? osoba) ? osoba : null;
        }

        
        // MJERENJE VREMENA
        
        public static (Osob? osoba, long vrijeme) Izmjeri(Func<Osob?> algoritam)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var rezultat = algoritam();
            sw.Stop();
            return (rezultat, sw.ElapsedTicks);
        }
    }
}

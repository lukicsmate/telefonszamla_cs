using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        public static int Masodperc(int ora, int perc, int mp)
        {
            return ora * 3600 + perc * 60 + mp;
        }

        public static bool Mobil(string szam)
        {
            return szam.StartsWith("39") ||
                   szam.StartsWith("41") ||
                   szam.StartsWith("71");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Telefonszám: ");
            string telefonszam = Console.ReadLine();

            if (Mobil(telefonszam)) { Console.WriteLine("Mobil"); }
            else { Console.WriteLine("Vezetékes"); }
                

            Console.WriteLine("Hívás kezdete (óra perc mp): ");
            string[] kezdet = Console.ReadLine().Split(' ');

            Console.WriteLine("Hívás vége (óra perc mp): ");
            string[] vege = Console.ReadLine().Split(' ');

            int k = Masodperc(int.Parse(kezdet[0]),int.Parse(kezdet[1]),int.Parse(kezdet[2]));

            int v = Masodperc(int.Parse(vege[0]),int.Parse(vege[1]),int.Parse(vege[2]));

            int ido = v - k;
            int perc = (ido + 59) / 60;

            Console.WriteLine("Számlázott percek: " + perc);

            var sorok = new List<string>();
            using (StreamReader sr = new StreamReader("HIVASOK.TXT"))
            {
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().ToString();
                    sorok.Add(sor);
                }
            }



            int csucs = 0;
            int csucsonKivul = 0;

            int mobilPerc = 0;
            int vezetekesPerc = 0;

            decimal osszeg = 0;

            Console.WriteLine("\n3. feladat");

            for (int i = 0; i < sorok.Count(); i += 2)
            {
                string[] adatok = sorok[i].Split(' ');

                int kezd = Masodperc(int.Parse(adatok[0]),int.Parse(adatok[1]),int.Parse(adatok[2]));

                int veg = Masodperc(int.Parse(adatok[3]),int.Parse(adatok[4]),int.Parse(adatok[5]));

                string szam = sorok[i + 1];

                int idotartam = veg - kezd;
                int percek = (idotartam + 59) / 60;

                Console.WriteLine(percek + " " + szam);

                bool mobil = Mobil(szam);

                if (kezd >= 7 * 3600 && kezd < 18 * 3600)
                {
                    csucs++;

                    if (mobil)
                        osszeg += percek * 69.175m;
                    else
                        osszeg += percek * 30m;
                }
                else
                {
                    csucsonKivul++;
                }

                if (mobil) { mobilPerc += percek; }
                else { vezetekesPerc += percek; }
                    
            }

            Console.WriteLine("\n4. feladat");
            Console.WriteLine("Csúcsidőben: " + csucs);
            Console.WriteLine("Csúcsidőn kívül: " + csucsonKivul);

            Console.WriteLine("\n5. feladat");
            Console.WriteLine("Mobil percek: " + mobilPerc);
            Console.WriteLine("Vezetékes percek: " + vezetekesPerc);

            Console.WriteLine("\n6. feladat");
            Console.WriteLine("Fizetendő: " + osszeg.ToString("0.00") + " Ft");

            Console.ReadKey();
        }
    }
}

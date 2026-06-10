using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance
{
    public class SpravceFinanci
    {
        public List<Transakce> SeznamTransakci { get; set; }

        public SpravceFinanci()
        {
            SeznamTransakci = new List<Transakce>();
        }

        public void PridatTransakci(Transakce novaTransakce)
        {
            SeznamTransakci.Add(novaTransakce);
            Console.WriteLine("Transakce byla uspesne pridana.");
        }

        public double SpoctiZustatek()
        {
            double zustatek = 0;
            foreach (Transakce t in SeznamTransakci)
            {
                if (t is Prijem) zustatek = zustatek + t.Castka;
                else if (t is Vydaj) zustatek = zustatek - t.Castka;
            }
            return zustatek;
        }

        public void VypisVsechnyTransakce()
        {
            if (SeznamTransakci.Count == 0)
            {
                Console.WriteLine("Zatim nemas zadne transakce.");
                return;
            }

            Console.WriteLine("--- HISTORIE TRANSAKCI ---");
            foreach (Transakce t in SeznamTransakci)
            {
                t.VypisDetail();
            }
            Console.WriteLine("--------------------------");
        }

        public void VykresliGrafMesicu()
        {
            Console.WriteLine("--- MESICNI GRAF (1 krizek = 1000 Kc) ---");

            for (int m = 1; m <= 12; m++)
            {
                double prijmyMesic = 0;
                double vydajeMesic = 0;

                foreach (Transakce t in SeznamTransakci)
                {
                    if (t.Datum.Month == m)
                    {
                        if (t is Prijem) prijmyMesic = prijmyMesic + t.Castka;
                        else if (t is Vydaj) vydajeMesic = vydajeMesic + t.Castka;
                    }
                }

                if (prijmyMesic > 0 || vydajeMesic > 0)
                {
                    Console.WriteLine("\nMesic cislo " + m + ":");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Prijmy (" + prijmyMesic + " Kc): ");
                    int pocetKrizkuPrijem = Convert.ToInt32(prijmyMesic / 1000); 

                    for (int i = 0; i < pocetKrizkuPrijem; i++) { Console.Write("X"); }
                    Console.WriteLine(); 

                  
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Vydaje (" + vydajeMesic + " Kc): ");
                    int pocetKrizkuVydaj = Convert.ToInt32(vydajeMesic / 1000);
                    for (int i = 0; i < pocetKrizkuVydaj; i++) { Console.Write("X"); }
                    Console.WriteLine();

                    Console.ResetColor();
                }
            }
            Console.WriteLine("\n-----------------------------------------");
        }
    }
}

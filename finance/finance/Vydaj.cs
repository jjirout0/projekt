using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance
{
    public class Vydaj : Transakce
    {
        public Vydaj(double castka, string kategorie, string popis) : base(castka, kategorie, popis)
        {
        }

        public override void VypisDetail()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("[-] " + Datum.ToString("dd.MM.yyyy") + " | " + Kategorie + " | -" + Castka + " Kc | " + Popis);

            Console.ResetColor();
        }

        public override string ToCsvString()
        {
            return "Vydaj;" + base.ToCsvString();
        }
    }
}

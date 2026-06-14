using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance
{
    public class Prijem : Transakce
    {
        public Prijem(double castka, string kategorie, string popis) : base(castka, kategorie, popis)
        {
        }

        public override void VypisDetail()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("[+] " + Datum.ToString("dd.MM.yyyy") + " | " + Kategorie + " | +" + Castka + " Kc | " + Popis);

            Console.ResetColor();
        }

        public override string ToCsvString()
        {
            return "Prijem;" + base.ToCsvString();
        }
    }
}

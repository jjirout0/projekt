using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance
{

    public abstract class Transakce
    {
        
        public DateTime Datum { get; set; }
        public double Castka { get; set; }
        public string Kategorie { get; set; }
        public string Popis { get; set; }

        
        public Transakce(double castka, string kategorie, string popis)
        {
            Datum = DateTime.Now;

            Castka = castka;
            Popis = popis;

            if (kategorie == "")
            {
                Kategorie = "Nezrazeno";
            }
            else
            {
                Kategorie = kategorie;
            }
        }

        public virtual void VypisDetail()
        {
            Console.WriteLine(Datum.ToString("dd.MM.yyyy") + " - " + Kategorie + " - " + Castka + " Kc - " + Popis);
        }

        public virtual string ToCsvString()
        {
            return Datum.ToString() + ";" + Castka + ";" + Kategorie + ";" + Popis;
        }
    }

}


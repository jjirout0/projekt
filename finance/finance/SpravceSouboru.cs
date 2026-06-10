using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance
{
    public class SpravceSouboru
    {
        private string nazevSouboru = "data.txt";

        public void UlozData(List<Transakce> seznam)
        {
            using (StreamWriter sw = new StreamWriter(nazevSouboru))
            {
                foreach (Transakce t in seznam)
                {
                    sw.WriteLine(t.ToCsvString());
                }
            }
            Console.WriteLine("Data byla uspesne ulozena.");
        }

        public List<Transakce> NactiData()
        {
            List<Transakce> nactenySeznam = new List<Transakce>();

            if (File.Exists(nazevSouboru))
            {
                using (StreamReader sr = new StreamReader(nazevSouboru))
                {
                    string radek;
                    while ((radek = sr.ReadLine()) != null)
                    {
                        string[] dily = radek.Split(';');


                        string typ = dily[0];
                        double castka = Convert.ToDouble(dily[2]); 
                        string kategorie = dily[3];
                        string popis = dily[4];
                        DateTime datum = Convert.ToDateTime(dily[1]); 

                        
                        if (typ == "Prijem")
                        {
                            Prijem p = new Prijem(castka, kategorie, popis);
                            p.Datum = datum; 
                            nactenySeznam.Add(p);
                        }
                        else if (typ == "Vydaj")
                        {
                            Vydaj v = new Vydaj(castka, kategorie, popis);
                            v.Datum = datum;
                            nactenySeznam.Add(v);
                        }
                    }
                }
            }

            return nactenySeznam;
        }
    }
}

namespace finance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SpravceFinanci spravce = new SpravceFinanci();
            SpravceSouboru soubory = new SpravceSouboru();

            Console.WriteLine("Nacitam ulozena data...");
            spravce.SeznamTransakci = soubory.NactiData();

            while (true)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@"
  _____  _                              
 |  ___|(_) _ __    __ _  _ __    ___  ___ 
 | |_   | || '_ \  / _` || '_ \  / __|/ _ \
 |  _|  | || | | || (_| || | | || (__|  __/
 |_|    |_||_| |_| \__,_||_| |_| \___|\___|
");
                Console.ResetColor();
                
                Console.WriteLine("Aktualni zustatek: " + spravce.SpoctiZustatek() + " Kc\n");

                Console.WriteLine("1 - Pridat Prijem");
                Console.WriteLine("2 - Pridat Vydaj");
                Console.WriteLine("3 - Vypsat vsechny transakce");
                Console.WriteLine("4 - Zobrazit mesicni graf");
                Console.WriteLine("5 - ADMIN: Pridat transakci s vlastnim datem");
                Console.WriteLine("6 - Hledat podle kategorie");
                Console.WriteLine("7 - Ulozit a ukoncit program");
                Console.Write("\nVyber moznost: ");

                string volba = Console.ReadLine();

                Console.WriteLine();

                switch (volba)
                {
                    case "1":
                        Console.Write("Zadej castku: ");
                        double castkaPrijmu = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Zadej kategorii: ");
                        string katPrijmu = Console.ReadLine();
                        Console.Write("Zadej popis: ");
                        string popPrijmu = Console.ReadLine();

                        Prijem novyPrijem = new Prijem(castkaPrijmu, katPrijmu, popPrijmu);
                        spravce.PridatTransakci(novyPrijem);
                        break;

                    case "2":
                        Console.Write("Zadej castku: ");
                        double castkaVydaje = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Zadej kategorii: ");
                        string katVydaje = Console.ReadLine();
                        Console.Write("Zadej popis: ");
                        string popVydaje = Console.ReadLine();

                        Vydaj novyVydaj = new Vydaj(castkaVydaje, katVydaje, popVydaje);
                        spravce.PridatTransakci(novyVydaj);
                        break;

                    case "3":
                        spravce.VypisVsechnyTransakce();
                        break;

                    case "4":
                        spravce.VykresliGrafMesicu();
                        break;

                    case "5":
                        Console.WriteLine("--- ADMIN REZIM ---");
                        Console.Write("Chces pridat Prijem (napis 1) nebo Vydaj (napis 2)? ");
                        string adminTyp = Console.ReadLine();

                        Console.Write("Zadej castku: ");
                        double adminCastka = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Zadej kategorii: ");
                        string adminKat = Console.ReadLine();
                        Console.Write("Zadej popis: ");
                        string adminPop = Console.ReadLine();

                        Console.Write("Zadej datum (ve formatu DD.MM.RRRR, napr. 15.03.2023): ");
                        DateTime adminDatum = Convert.ToDateTime(Console.ReadLine());

                        if (adminTyp == "1")
                        {
                            Prijem p = new Prijem(adminCastka, adminKat, adminPop);
                            p.Datum = adminDatum;
                            spravce.PridatTransakci(p);
                        }
                        else if (adminTyp == "2")
                        {
                            Vydaj v = new Vydaj(adminCastka, adminKat, adminPop);
                            v.Datum = adminDatum;
                            spravce.PridatTransakci(v);
                        }
                        else
                        {
                            Console.WriteLine("Spatna volba typu.");
                        }
                        break;

                    case "6":
                        Console.Write("Zadej kategorii, kterou chces najit (napr. Jidlo): ");
                        string hledano = Console.ReadLine();
                        spravce.VypisPodleKategorie(hledano);
                        break;

                    case "7":
                        soubory.UlozData(spravce.SeznamTransakci);
                        Console.WriteLine("Konec programu. Dekuji za pouziti!");
                        return;

                    default:
                        Console.WriteLine("Spatna volba, zkus to znovu.");
                        break;
                }


                Console.WriteLine("\nStiskni Enter pro navrat do menu...");
                Console.ReadLine();
            }
        }
    }
}




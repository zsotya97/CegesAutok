using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CegesAutok
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. feladat: Beolvasás
            List<Adatok> lista = new(File.ReadAllLines("autok.txt").Select(x => new Adatok(x)));
            //VÉGE


            //2. feladat: Linq utolsó
            Console.WriteLine($"2.feladat\n{lista.Last().Nap}. nap rendszám: {lista.Last(x=>x.Statusz==0 &&x.Nap==lista.Last().Nap).Rendszam}");
            //VÉGE



            //3. feladat
            Console.Write($"3.feladat\nNap: ");
            int nap = int.Parse(Console.ReadLine());
            Console.WriteLine($"Forgalom a(z) {nap}. napon: ");
            foreach (var item in lista.Where(x => x.Nap == nap))
            {
                var statusz = item.Statusz == 0 ? "ki" : "be";
                Console.WriteLine($"{item.Ido} {item.Rendszam} {item.Az} {statusz}");
            }
            //VÉGE



            //4. feladat: a stat egy sorban az egész 4. feladat teljes linq lekérdezéssel
            Console.WriteLine($"4.feladat");
            var stat = lista.GroupBy(x => x.Rendszam).ToList().Select(x=>new {szamolt0 = x.Where(x=>x.Statusz==0).Count(),szamolt1= x.Where(x => x.Statusz == 1).Count() }).Where(x=>x.szamolt0!=x.szamolt1).Count();
            Console.WriteLine($"A hónap végén {stat} autót nem hoztak vissza.");
            //VÉGE



            //5. feladat: linq max min különbség
            Console.WriteLine($"5.feladat");
            var osszesitett = lista.GroupBy(x => x.Rendszam).ToList().OrderBy(x=>x.Key).Select(x=>new {Rendszam = x.Key, km = x.Last().Km-x.First().Km});
            foreach (var item in osszesitett)
            {
                Console.WriteLine($"{item.Rendszam} {item.km} km");
            }
            //VÉGE



            ///6. feladat: Sikerült linq val megoldani, és halmazt is használtam hozzá. 
            ///Érettségi előtt használtam ,  minden elem csak egyszer szerepelhet benne.
            var legtobb = lista.GroupBy(x => x.Rendszam).ToList().OrderBy(x => x.Key)
                .Select(x => new { Rendszam = x.Key, Elso=x.First().Km,Masodik =x.First(y=>x.First().Km!=y.Km).Km,Azon1= x.First().Az,Azon2=x.Skip(2).First().Az });
            int megtett = 0;
            var max = new { Azon = 0, km = 0 };
            foreach (var temp in legtobb)
            {
                int elso = temp.Elso;
                int utolso = temp.Masodik;
                megtett = utolso - elso;
                if (max.km < megtett) max = new { Azon = temp.Azon2, km = megtett };
                HashSet<int> km = new HashSet<int>(lista.Where(x => x.Rendszam == temp.Rendszam).Select(x => x.Km).Skip(2));
                for (int i = 1; i <= km.Count - 2; i += 1)
                {
                    int eredmeny = km.ElementAt(i) - km.ElementAt(i - 1);
                    if (eredmeny > max.km) max = new { Azon = temp.Azon2, km = eredmeny };
                }
            }
            Console.WriteLine($"6. feladat:\nLeghoszabb út: {max.km}, személy : {max.Azon}");
            //VÉGE



            //7. feladat: kiiratás
            Console.Write($"7. feladat:\nRendszam:");
            string rendszam = Console.ReadLine();
            StreamWriter kiir = new StreamWriter($"{rendszam}_menetlevel.txt", false, Encoding.UTF8);
            
                var list = lista.Where(x => x.Rendszam == rendszam&&x.Statusz==0).GroupBy(x => x.Az).ToList();
                foreach (var item in lista) 
                {
                    if (item.Rendszam == rendszam && item.Statusz == 0)
                    {
                        kiir.Write($"{item.Az}\t{item.Nap}.\t{item.Ido}\t{item.Km} km \t");
                    }
                    if (item.Rendszam == rendszam && item.Statusz == 1)
                    {
                        kiir.WriteLine($"{item.Nap}.\t{item.Ido}\t{item.Km} km");
                    }
            }
            kiir.Close();
            Console.WriteLine("Menetlevél kész.");
            //VÉGE

        }
    }
}

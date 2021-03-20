using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CegesAutok
{
    class Adatok
    {
        public int Nap { get; private set; }
        public string Ido { get; private set; }
        public string Rendszam { get; private set; }
        public int Az { get; private set; }
        public int Km { get; private set; }
        public int Statusz { get; private set; }
        public Adatok(string sor)
        {
            var temp = sor.Split();
            Nap = int.Parse(temp[0]);
            Ido = temp[1];
            Rendszam = temp[2];
            Az= int.Parse(temp[3]);
            Km = int.Parse(temp[4]);
            Statusz = int.Parse(temp[5]);

        }
    }
}

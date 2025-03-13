using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class Produkt
    {
		private string nazwaProduktu;
        private float cenaProduktu;

        public Produkt(string nazwaProduktu, float cenaProduktu)
        {
            this.cenaProduktu = cenaProduktu;
            this.nazwaProduktu = nazwaProduktu;
        }

	}
}

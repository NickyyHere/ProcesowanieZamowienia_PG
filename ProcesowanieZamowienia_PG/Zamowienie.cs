using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class Zamowienie
    {
        private StanyZamowien stanZamowienia;
        private List<Produkt> produkty = new List<Produkt>();
        private Klienci typKlienta;
        private Adres adresZamowienia;
        private IPlatnosci sposobPlatnosci;

        public Zamowienie(StanyZamowien stanZamowienia, List<Produkt> produkty, Adres adresZamowienia, IPlatnosci sposobPlatnosci) 
        {
            this.stanZamowienia = stanZamowienia;
            this.produkty = produkty;
            this.adresZamowienia=adresZamowienia;
            this.sposobPlatnosci = sposobPlatnosci;
        }

    }
}

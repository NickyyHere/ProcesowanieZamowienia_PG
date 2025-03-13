using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class Adres
    {
        private String kraj, miasto, ulica, kodPocztowy;
        public Adres(string kraj, string miasto, string ulica, string kodPocztowy)
        {
            this.kraj = kraj;
            this.miasto = miasto;
            this.ulica = ulica;
            this.kodPocztowy = kodPocztowy;
        }
        public string getKraj()
        {
            return kraj;
        }
        public string getMiasto()
        {
            return miasto;
        }
        public string getUlica()
        {
            return ulica;
        }
        public string getKodPocztowy()
        {
            return kodPocztowy;
        }
        public override string ToString()
        {
            return $"{ulica}, {miasto} {kodPocztowy}, {kraj}";
        }
    }
}

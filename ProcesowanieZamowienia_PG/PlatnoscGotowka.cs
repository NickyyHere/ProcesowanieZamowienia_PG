﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class PlatnoscGotowka : IPlatnosci
    {
        public void Przetworz()
        {
            Console.WriteLine("Przetwarzanie zamówienia, oprata gotówką za pobraniem");
        }
    }
}

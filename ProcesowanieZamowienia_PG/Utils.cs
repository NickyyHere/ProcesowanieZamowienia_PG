﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class Utils
    {
        private static Utils? instance;

        private Utils() { }
        public static Utils Instance
        {
            get
            {
                if (instance == null) instance = new Utils();
                return instance;
            }
        }
        public string StateToString(OrderStates orderState)
        {
            return orderState switch
            {
                OrderStates.NEW => "Nowe",
                OrderStates.STORAGE => "W magazynie",
                OrderStates.RETURNED => "Zwrócone",
                OrderStates.SENT => "Wysłane",
                OrderStates.CLOSED => "Zamknięte",
                _ => throw new Exception("Nieznany stan zamówienia")
            };
        }

        public string ClientToString(Clients client)
        {
            return client switch
            {
                Clients.COMPANY => "Firma",
                Clients.NATURAL_PERSON => "Osoba prywatna",
                _ => throw new Exception("Nieznany typ klienta")
            };
        }
    }
}

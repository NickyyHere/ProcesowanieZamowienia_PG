using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class CreditCardPayment : IPayment
    {
        public OrderStates Process(Order order)
        {
            return OrderStates.STORAGE;
        }
        public override string ToString()
        {
            return "Karta kredytowa";
        }
    }
}

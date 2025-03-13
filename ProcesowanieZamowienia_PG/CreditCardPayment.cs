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
            Address address = order.OrderAddress;

            if (!address.IsNull())
            {
                return OrderStates.ERROR;
            }

            Console.WriteLine("Przetwarzanie zamówienia Kartą Kredytową");
            return OrderStates.NEW;
        }
    }
}

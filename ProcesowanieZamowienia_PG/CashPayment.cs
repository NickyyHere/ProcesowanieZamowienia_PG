using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class CashPayment : IPayment
    {
        public OrderStates Process(Order order)
        {
            Address address = order.OrderAddress;

            if (!address.IsNull())
            {
                return OrderStates.ERROR;
            }
            if (order.GetOrderValue() >= 2500)
            {
                return OrderStates.RETURNED;
            }

            Console.WriteLine("Przetwarzanie zamówienia, opłata gotówką za pobraniem");
            return OrderStates.NEW;
        }
        public override string ToString()
        {
            return "Gotówka za pobraniem";
        }
    }
}

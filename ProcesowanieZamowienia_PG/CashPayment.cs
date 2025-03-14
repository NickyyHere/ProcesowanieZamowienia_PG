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
            if (OrderController.Instance.GetOrderValue(order) >= 2500)
            {
                return OrderStates.RETURNED;
            }
            return OrderStates.STORAGE;
        }
        public override string ToString()
        {
            return "Gotówka za pobraniem";
        }
    }
}

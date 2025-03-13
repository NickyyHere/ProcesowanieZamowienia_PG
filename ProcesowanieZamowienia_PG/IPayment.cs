using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal interface IPayment
    {
        public OrderStates Process(Order order);
    }
}

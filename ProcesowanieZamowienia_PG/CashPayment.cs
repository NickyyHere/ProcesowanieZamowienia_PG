namespace ProcesowanieZamowienia_PG
{
    internal class CashPayment : IPayment
    {
        public OrderStates Process(Order order) 
        {  
            return order.GetOrderValue() >= 2500 ? OrderStates.RETURNED : OrderStates.STORAGE;
        }
        public override string ToString()
        {
            return "Gotówka za pobraniem";
        }
    }
}

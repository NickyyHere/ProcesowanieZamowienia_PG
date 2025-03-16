namespace ProcesowanieZamowienia_PG
{
    internal interface IPayment
    {
        public OrderStates Process(Order order);
    }
}

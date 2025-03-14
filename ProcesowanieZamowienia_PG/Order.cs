using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class Order
    {
        private static int _orderId = 0;
        private OrderStates _state;
        public int OrderId { get; private set; }
        public OrderStates OrderState {
            get { return _state; }
            private set 
            {
                if (_state == value) return;

                if (OrderAddress.IsNull())
                    _state = OrderStates.ERROR;
                else
                    _state = value;
                switch (_state)
                {
                    case OrderStates.NEW:
                        Console.WriteLine("Utworzono nowe zamówienie");
                        break;
                    case OrderStates.STORAGE:
                        Console.WriteLine("Zamówienie przekazane do magazynu");
                        break;
                    case OrderStates.SENT:
                        Console.WriteLine("Zamówienie wysłane do klienta");
                        break;
                    case OrderStates.RETURNED:
                        Console.WriteLine("Zamówienie zostało zwrócone do klienta");
                        break;
                    case OrderStates.ERROR:
                        Console.WriteLine("Błąd w zamówieniu! Proszę edytować zamówienie.");
                        break;
                    case OrderStates.CLOSED:
                        Console.WriteLine("Zamówienie zostało zamknięte");
                        break;
                }
            } 
        }
        public Dictionary<Product, int> Products = new Dictionary<Product, int>();
        public Clients ClientType {  get; private set; }
        public Address OrderAddress { get; private set; }
        public IPayment PaymentMethod { get; private set; }

        public Order(Clients clientType, Address orderAddress, IPayment paymentMethod)
        {
            OrderAddress = orderAddress;
            OrderId = _orderId++;
            OrderState = OrderStates.NEW;
            ClientType = clientType;
            PaymentMethod = paymentMethod;
        }
        public void EditOrder(Clients clientType, Address orderAddress, IPayment paymentMethod)
        {
            ClientType = clientType;
            OrderAddress = orderAddress;
            PaymentMethod = paymentMethod;
            OrderState = OrderStates.NEW;
        }
        public void AddProduct(Product product, int amount)
        {
            Products.Add(product, amount);
        }
        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
        public void ProcessOrder()
        {
            OrderState = PaymentMethod.Process(this);
        }
        public void CloseOrder()
        {
            OrderState = OrderStates.CLOSED;
        }
        public void SendOrder()
        {
            OrderState = OrderStates.SENT;
        }
        public override string ToString()
        {
            return $"Zamówienie nr. {OrderId}, Status zamówienia: {Utils.Instance.StateToString(OrderState)}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class Order
    {
        public int OrderId { get; private set; }
        public OrderStates OrderState { get; private set; }
        public Dictionary<Product, int> Products = new Dictionary<Product, int>();
        public Clients ClientType {  get; private set; }
        public Address OrderAddress { get; private set; }
        public IPayment PaymentMethod { get; private set; }

        public Order(int orderId, OrderStates orderState, Dictionary<Product, int> products, Clients clientType, Address orderAddress, IPayment paymentMethod)
        {
            OrderId = orderId;
            OrderState = orderState;
            Products = products;
            ClientType = clientType;
            OrderAddress = orderAddress;
            PaymentMethod = paymentMethod;
        }

        public void ShowOrder()
        {
            Utils utils = Utils.Instance;
            Console.WriteLine($"Identyfikator zamówienia: {OrderId}\n" +
                $"Stan zamówienia: {utils.StateToString(OrderState)}\n" +
                $"Produkty w zamówieniu:\n");
            Console.WriteLine("{0, -20} {1, -15} {2, -15}", "Nazwa", "Ilość sztuk", "Cena za sztukę");
            foreach (var produkt in Products)
            {
                Console.WriteLine("{0, -20} {1, -15} {2, -15} zł", produkt.Key.ProductName, produkt.Value, produkt.Key.ProductPrice);
            }
            Console.WriteLine($"Adres zamówienia: {OrderAddress}\n" +
                $"Sposób płatności: {utils.PaymentMethodToString(PaymentMethod)}\n" +
                $"Typ klienta: {utils.ClientToString(ClientType)}");
        }
        public float GetOrderValue()
        {
            float suma = 0f;
            foreach (var product in Products)
            {
                suma += product.Key.ProductPrice * product.Value;
            }
            return suma;
        }

        public void ZlozZamowienie()
        {
            OrderState = PaymentMethod.Process(this);
        }
        public void ZmienStanZamowienia(OrderStates state)
        {
            this.OrderState = state;
        }
    }
}

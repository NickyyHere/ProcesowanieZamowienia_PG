using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class OrderController
    {
        private static OrderController? _instance;
        private OrderController() { }
        public static OrderController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OrderController();
                return _instance;
            }
        }

        public void ShowAllOrders(List<Order> orders, OrderStates? filter = null)
        {
            foreach (var order in orders)
            {
                if (filter == null)
                {
                    Console.WriteLine(order);
                }
                else
                {
                    if (order.OrderState == filter)
                    {
                        Console.WriteLine(order);
                    }
                }
            }
        }

        public void ShowOrderDetails(Order order)
        {
            Utils utils = Utils.Instance;
            Console.WriteLine($"Identyfikator zamówienia: {order.OrderId}\n" +
                $"Stan zamówienia: {utils.StateToString(order.OrderState)}\n" +
                $"Produkty w zamówieniu:");
            Console.WriteLine("{0, -20} {1, -15} {2, -15}", "Nazwa", "Ilość sztuk", "Cena za sztukę");
            foreach (var produkt in order.Products)
            {
                Console.WriteLine("{0, -20} {1, -15} {2, -15} zł", produkt.Key.ProductName, produkt.Value, produkt.Key.ProductPrice);
            }
            Console.WriteLine($"Całkowita wartość zamówienia: {OrderController.Instance.GetOrderValue(order)}\n" +
                $"Adres zamówienia: {order.OrderAddress}\n" +
                $"Sposób płatności: {order.PaymentMethod}\n" +
                $"Typ klienta: {utils.ClientToString(order.ClientType)}");
        }
        public Order UpsertOrder(Order? order = null) // UpSert - Update or Insert
        {
            Clients clientType;
            Address address;
            IPayment paymentMethod;

            switch (Utils.Instance.IntegerInput("Wybierz typ klienta:\n1 - Firma\n2 - Osoba prywatna\nWprowadzenie innej liczby spowoduje anulowanie tworzenia zamówienia\n> ")) 
            {
                case 1:
                    clientType = Clients.COMPANY;
                    break;
                case 2:
                    clientType = Clients.NATURAL_PERSON;
                    break;
                default:
                    return null; // Oznacza anulowanie tworzenia zamówienia
            }

            Console.WriteLine("Podaj adres zamówienia:");
            Console.Write("Kraj: ");
            string country = Console.ReadLine() ?? "";
            Console.Write("Miasto: ");
            string city = Console.ReadLine() ?? "";
            Console.Write("Ulica: ");
            string street = Console.ReadLine() ?? "";
            Console.Write("Kod pocztowy: ");
            string postalCode = Console.ReadLine() ?? "";
            address = new Address(country, city, street, postalCode);

            switch (Utils.Instance.IntegerInput("Wybierz metodę płatności:\n1 - Karta kredytowa\n2 - Gotówka\nWprowadzenie innej liczby spowoduje anulowanie tworzenia zamówienia\n> "))
            {
                case 1:
                    paymentMethod = new CreditCardPayment();
                    break;
                case 2:
                    paymentMethod = new CashPayment();
                    break;
                default:
                    return null;
            }
            if(order == null)
            {
                return new Order(clientType, address, paymentMethod);
            }
            order.EditOrder(clientType, address, paymentMethod);
            Console.WriteLine("Zamówienie zostało zedytowane");
            return order;
        }
        public float GetOrderValue(Order order)
        {
            float suma = 0f;
            foreach (var product in order.Products)
            {
                suma += product.Key.ProductPrice * product.Value;
            }
            return suma;
        }
        public Order SelectOrder(List<Order> orders, OrderStates? filter = null)
        {
            ShowAllOrders(orders, filter);
            int orderId = Utils.Instance.IntegerInput("Wprowadź numer zamówienia: ");
            if(orderId < 0 || orderId > orders.Count - 1)
            {
                Console.WriteLine("Nie ma zamówienia o podanym numerze");
                return null;
            }
            return orders[orderId];
        }
        
        public void AddProductToOrder(Order order, List<Product> products)
        {
            Console.WriteLine("Wybierz produkt do dodania do zamówienia:");
            Product product = ProductController.Instance.SelectProduct(products);
            int amount = Utils.Instance.IntegerInput("Podaj ilość sztuk: ");
            if (amount < 1)
            {
                Console.WriteLine("Ilość sztuk musi być większa od 0, dodanie produktu zostaje anulowane.");
                return;
            }
            order.AddProduct(product, amount);
            Console.WriteLine("Produkt został dodany do zamówienia");
        }

        public void RemoveProductFromOrder(Order order)
        {
            if (order.Products.Count == 0)
            {
                Console.WriteLine("Brak produktów w zamówieniu");
                return;
            }
            Console.WriteLine("Wybierz produkt do usunięcia z zamówienia:");
            int i = 1;
            foreach (var product in order.Products)
            {
                Console.WriteLine($"{i}. {product.Key.ProductName} - {product.Value} sztuk");
                i++;
            }
            int productId = Utils.Instance.IntegerInput("Wprowadź numer produktu do usunięcia z zamówienia: ");
            if (productId < 1 || productId > order.Products.Count)
            {
                Console.WriteLine("Nie ma produktu o podanym numerze");
                return;
            }
            order.Products.Remove(order.Products.ElementAt(productId - 1).Key);
            Console.WriteLine("Produkt został usunięty z zamówienia");
        }

        public void ProcessOrder(Order order)
        {
            if(order.Products.Count == 0)
            {
                Console.WriteLine("Nie można przetworzyć zamówienia bez produktów");
                return;
            }
            if(order.OrderState != OrderStates.NEW)
            {
                if(order.OrderState == OrderStates.ERROR)
                {
                    Console.WriteLine("Błąd w zamówieniu! Proszę edytować zamówienie.");
                    return;
                }
                else
                {
                    Console.WriteLine("Zamówienie zostało już przetworzone");
                    return;
                }
                
            }
            order.ProcessOrder();
            Console.WriteLine($"Zamówienie nr. {order.OrderId} zostało przetworzone");
        }

        public void CloseOrder(Order order)
        {
            if(order.OrderState != OrderStates.SENT)
            {
                Console.WriteLine("Zamówienie nie zostało jeszcze wysłane");
                return;
            }
            order.CloseOrder();
            Console.WriteLine($"Zamówienie nr. {order.OrderId} zostało zamknięte");
        }

        public void SendOrder(Order order)
        {
            if(order.OrderState != OrderStates.STORAGE)
            {
                Console.WriteLine("Zamówienie nie jest gotowe do wysłania");
                return;
            }
            order.SendOrder();
            Console.WriteLine($"Zamówienie nr. {order.OrderId} zostało wysłane");
        }
    }
}

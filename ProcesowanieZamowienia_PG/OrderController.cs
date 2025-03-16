namespace ProcesowanieZamowienia_PG
{
    internal class OrderController
    {
        public List<Order> Orders { get; private set; }

        public OrderController(List<Order> orders) {
            Orders = orders;
        }

        public void ShowAllOrders(OrderStates? filter = null)
        {
            foreach (var order in Orders)
            {
                if (filter == null)
                {
                    Console.WriteLine(order);
                }
                else if(order.OrderState == filter)
                {
                    Console.WriteLine(order);
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
            Console.WriteLine($"Całkowita wartość zamówienia: {order.GetOrderValue()}\n" +
                $"Adres zamówienia: {order.OrderAddress}\n" +
                $"Sposób płatności: {order.PaymentMethod}\n" +
                $"Typ klienta: {utils.ClientToString(order.ClientType)}");
        }
        public void UpsertOrder(Order? order = null) // UpSert - Update or Insert
        {
            if (order != null && (order.OrderState != OrderStates.ERROR || order.OrderState != OrderStates.NEW))
            {
                Console.WriteLine("Można edytować tylko nowe, lub błędne zamówienia");
                return;
            }
            Clients clientType;
            Address address;
            IPayment paymentMethod;
            foreach (Clients client in Enum.GetValues(typeof(Clients)))
            {
                Console.WriteLine($"{(int)client} - {Utils.Instance.ClientToString(client)}");
            }
            clientType = (Clients)Utils.Instance.IntegerInput("Wybierz typ klienta (Wprowadzenie innej liczby spowoduje anulowanie tworzenia zamówienia):\n> ");


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
                    return;
            }
            if(order == null)
            {
                Orders.Add(new Order(clientType, address, paymentMethod));
            }
            else
            {
                order.EditOrder(clientType, address, paymentMethod);
                Console.WriteLine("Zamówienie zostało zedytowane");
            }
        }
        public Order SelectOrder(OrderStates? filter = null)
        {
            ShowAllOrders(filter);
            int orderId = Utils.Instance.IntegerInput("Wprowadź numer zamówienia: ");
            if(orderId < 0 || orderId > Orders.Count - 1)
            {
                Console.WriteLine("Nie ma zamówienia o podanym numerze");
                return null;
            }
            return Orders[orderId];
        }
        
        public void AddProductToOrder(Order order, ProductController productController)
        {
            if (order.OrderState != OrderStates.ERROR || order.OrderState != OrderStates.NEW)
            {
                Console.WriteLine("Można edytować tylko nowe, lub błędne zamówienia");
                return;
            }
            Console.WriteLine("Wybierz produkt do dodania do zamówienia:");
            Product product = productController.SelectProduct();
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
            if (order.OrderState != OrderStates.ERROR || order.OrderState != OrderStates.NEW)
            {
                Console.WriteLine("Można edytować tylko nowe, lub błędne zamówienia");
                return;
            }
            if (order.Products.Count == 0)
            {
                Console.WriteLine("Brak produktów w zamówieniu");
                return;
            }
            Console.WriteLine("Wybierz produkt do usunięcia z zamówienia:");
            int i = 0;
            foreach (var product in order.Products)
            {
                Console.WriteLine($"{i}. {product.Key.ProductName} - {product.Value} sztuk");
                i++;
            }
            int productId = Utils.Instance.IntegerInput("Wprowadź numer produktu do usunięcia z zamówienia: ");
            if (productId < 0 || productId > order.Products.Count)
            {
                Console.WriteLine("Nie ma produktu o podanym numerze");
                return;
            }
            order.Products.Remove(order.Products.ElementAt(productId).Key);
            Console.WriteLine("Produkt został usunięty z zamówienia");
        }

        public void ProcessOrder(Order order)
        {
            if (order == null) {
                Console.WriteLine("Nie wybrano zamówienia do przetworzenia");
                return;
            };
            if (order.Products.Count == 0)
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
        }

        public void CloseOrder(Order order)
        {
            if(order.OrderState != OrderStates.SENT)
            {
                Console.WriteLine("Zamówienie nie zostało jeszcze wysłane");
                return;
            }
            order.CloseOrder();
        }

        public void SendOrder(Order order)
        {
            if(order.OrderState != OrderStates.STORAGE)
            {
                Console.WriteLine("Zamówienie nie jest gotowe do wysłania");
                return;
            }
            order.SendOrder();
        }
    }
}

namespace ProcesowanieZamowienia_PG
{
    internal class OrderController
    {
        public List<Order> Orders { get; private set; }
        public Order _currentOrder;
        public Order CurrentOrder { 
            get 
            { 
                return _currentOrder;
            } 
            private set 
            {
                _currentOrder = value;
            } 
        }

        public OrderController() {
            Orders = new List<Order>();
        }

        public void ShowAllOrders(OrderStates? [] filter = null)
        {
            foreach (var order in Orders)
            {
                if (filter == null)
                {
                    Console.WriteLine(order);
                }
                else if(filter.Contains(order.OrderState))
                {
                    Console.WriteLine(order);
                }
            }
        }

        public void ShowOrderDetails()
        {
            Console.WriteLine($"Identyfikator zamówienia: {CurrentOrder.OrderId}\n" +
                $"Stan zamówienia: {Utils.StateToString(CurrentOrder.OrderState)}\n" +
                $"Produkty w zamówieniu:");
            Console.WriteLine("{0, -20} {1, -15} {2, -15}", "Nazwa", "Ilość sztuk", "Cena za sztukę");
            foreach (var produkt in CurrentOrder.Products)
            {
                Console.WriteLine("{0, -20} {1, -15} {2, -15} zł", produkt.Key.ProductName, produkt.Value, produkt.Key.ProductPrice);
            }
            Console.WriteLine($"Całkowita wartość zamówienia: {CurrentOrder.GetOrderValue()} zł\n" +
                $"Adres zamówienia: {CurrentOrder.OrderAddress}\n" +
                $"Sposób płatności: {CurrentOrder.PaymentMethod}\n" +
                $"Typ klienta: {Utils.ClientToString(CurrentOrder.ClientType)}");
        }
        public void UpsertOrder(Order order) // UpSert - Update or Insert
        {
            Clients clientType;
            Address address;
            IPayment paymentMethod;
            foreach (Clients client in Enum.GetValues(typeof(Clients)))
            {
                Console.WriteLine($"{(int)client} - {Utils.ClientToString(client)}");
            }
            clientType = (Clients)Utils.IntegerInput("Wybierz typ klienta (Wprowadzenie innej liczby spowoduje anulowanie tworzenia zamówienia):\n> ");


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

            switch (Utils.IntegerInput("Wybierz metodę płatności:\n1 - Karta kredytowa\n2 - Gotówka\nWprowadzenie innej liczby spowoduje anulowanie tworzenia zamówienia\n> "))
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

            order.EditOrder(clientType, address, paymentMethod);

            if (!Orders.Contains(order))
            {
                Orders.Add(order);
            }
        }
        public void SelectOrder(OrderStates? [] filter = null)
        {
            ShowAllOrders(filter);
            int orderId = Utils.IntegerInput("Wprowadź numer zamówienia: ");
            if(orderId < 0 || orderId > Orders.Count - 1)
            {
                Console.WriteLine("Nie ma zamówienia o podanym numerze");
                return;
            }
            if(filter != null && !filter.Contains(Orders[orderId].OrderState))
            {
                Console.WriteLine("Nie można wybrać zamówienia o podanym stanie");
                return;
            }
            CurrentOrder = Orders[orderId];
        }
        
        public void AddProductToOrder(ProductController productController)
        {
            Console.WriteLine("Wybierz produkt do dodania do zamówienia:");
            Product product = productController.SelectProduct();
            int amount = Utils.IntegerInput("Podaj ilość sztuk: ");
            if (amount < 1)
            {
                Console.WriteLine("Ilość sztuk musi być większa od 0, dodanie produktu zostaje anulowane.");
                return;
            }
            CurrentOrder.AddProduct(product, amount);
            Console.WriteLine("Produkt został dodany do zamówienia");
        }

        public void RemoveProductFromOrder()
        {
            if (CurrentOrder.Products.Count == 0)
            {
                Console.WriteLine("Brak produktów w zamówieniu");
                return;
            }
            Console.WriteLine("Wybierz produkt do usunięcia z zamówienia:");
            int i = 0;
            foreach (var product in CurrentOrder.Products)
            {
                Console.WriteLine($"{i}. {product.Key.ProductName} - {product.Value} sztuk");
                i++;
            }
            int productId = Utils.IntegerInput("Wprowadź numer produktu do usunięcia z zamówienia: ");
            if (productId < 0 || productId > CurrentOrder.Products.Count)
            {
                Console.WriteLine("Nie ma produktu o podanym numerze");
                return;
            }
            CurrentOrder.Products.Remove(CurrentOrder.Products.ElementAt(productId).Key);
            Console.WriteLine("Produkt został usunięty z zamówienia");
        }

        public void ProcessOrder()
        {
            if (CurrentOrder.Products.Count == 0)
            {
                Console.WriteLine("Nie można przetworzyć zamówienia bez produktów");
                return;
            }
            CurrentOrder.ChangeOrderState(CurrentOrder.PaymentMethod.Process(CurrentOrder));
        }

        public void CloseOrder()
        {
            if (CurrentOrder == null)
            {
                Console.WriteLine("Nie wybrano zamówienia do zamknięcia");
                return;
            };
            CurrentOrder.ChangeOrderState(OrderStates.CLOSED);
        }

        public void SendOrder()
        {
            CurrentOrder.ChangeOrderState(OrderStates.SENT);
        }

        // Inicjalizacja przykładowej listy zamówień
        public void InitOrderList(List<Product> products)
        {
            // Poprawne, W magazynie
            Order tempOrder = new Order(Clients.COMPANY, new Address("Polska", "Warszawa", "Mazowiecka 12", "00-001"), new CreditCardPayment());
            tempOrder.AddProduct(products[0], 5);
            tempOrder.AddProduct(products[4], 5);
            tempOrder.AddProduct(products[5], 10);
            tempOrder.ChangeOrderState(OrderStates.STORAGE);
            Orders.Add(tempOrder);
            // Poprawne, Nowe
            tempOrder = new Order(Clients.NATURAL_PERSON, new Address("Polska", "Gdańsk", "Warszawska 74/2", "12-345"), new CashPayment());
            tempOrder.AddProduct(products[9], 1);
            tempOrder.AddProduct(products[10], 1);
            Orders.Add(tempOrder);
            // Błędne
            tempOrder = new Order(Clients.COMPANY, new Address("Polska", "Katowice", "", "51-125"), new CreditCardPayment());
            tempOrder.AddProduct(products[3], 6);
            tempOrder.AddProduct(products[5], 2);
            tempOrder.AddProduct(products[7], 9);
            Orders.Add(tempOrder);
            // Poprawne, Wysłane
            tempOrder = new Order(Clients.NATURAL_PERSON, new Address("Polska", "Kraków", "Wrocławska 12", "33-333"), new CreditCardPayment());
            tempOrder.AddProduct(products[1], 1);
            tempOrder.AddProduct(products[2], 1);
            tempOrder.AddProduct(products[6], 1);
            tempOrder.ChangeOrderState(OrderStates.SENT);
            Orders.Add(tempOrder);
            // Poprawne, Zwrócone
            tempOrder = new Order(Clients.COMPANY, new Address("Polska", "Warszawa", "Mazowiecka 12", "00-001"), new CreditCardPayment());
            tempOrder.AddProduct(products[8], 21);
            tempOrder.AddProduct(products[11], 10);
            tempOrder.ChangeOrderState(OrderStates.RETURNED);
            Orders.Add(tempOrder);
            // Poprawne, Zamknięte
            tempOrder = new Order(Clients.NATURAL_PERSON, new Address("Polska", "Gdańsk", "Warszawska 74/2", "12-345"), new CashPayment());
            tempOrder.AddProduct(products[0], 1);
            tempOrder.AddProduct(products[5], 1);
            tempOrder.AddProduct(products[7], 1);
            tempOrder.ChangeOrderState(OrderStates.CLOSED);
            Orders.Add(tempOrder);
            Console.Clear();
        }
    }
}

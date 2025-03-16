namespace ProcesowanieZamowienia_PG
{
    class Program
    {
        static List<Order> OrderList = new List<Order>();
        static List<Product> ProductList = new List<Product>();
        static OrderController OrderController = new OrderController(OrderList);
        static ProductController ProductController = new ProductController(ProductList);

        static void Main(string[] args)
        {
            InitProductList();
            InitOrderList();

            int input = -1;
            do
            {
                DisplayMenu();
                input = Utils.Instance.IntegerInput("Wybierz opcję: ");
                if (input == 0)
                {
                    Console.WriteLine("Kończenie...");
                }
                else
                {
                    MenuController(input);
                }
            } while (input != 0);

        }
        static void DisplayMenu()
        {
            Console.WriteLine("===MENU===");
            Console.WriteLine("1. Utwórz zamówienie");
            Console.WriteLine("2. Edytuj zamówienie");
            Console.WriteLine("3. Wyświetl zamówienia");
            Console.WriteLine("4. Wyświetl konkretne zamówienie");
            Console.WriteLine("5. Przetwórz zamówienie");
            Console.WriteLine("6. Wyślij zamówienie");
            Console.WriteLine("7. Zamknij zamówienie");
            Console.WriteLine("8. Dodaj produkt do zamówienia");
            Console.WriteLine("9. Usuń produkt z zamówienia");
            Console.WriteLine("0. Wyjdź");
        }
        static void MenuController(int input)
        {
            Order o;
            switch (input)
            {
                case 1:
                    OrderController.UpsertOrder();
                    break;
                case 2:
                    o = OrderController.SelectOrder();
                    if (o != null)
                    {
                        OrderController.UpsertOrder(o);
                    }
                    break;
                case 3:
                    int choice = Utils.Instance.IntegerInput("Czy filtrować zamówienia po ich stanie?\n1 - TAK\n2 - NIE\n> ");
                    if (choice == 1)
                    {
                        foreach (OrderStates state in Enum.GetValues(typeof(OrderStates)))
                        {
                            Console.WriteLine($"{(int)state} - {Utils.Instance.StateToString(state)}");
                        }
                        OrderStates filterState = (OrderStates)Utils.Instance.IntegerInput("Wybierz stan zamówienia: ");
                        OrderController.ShowAllOrders(filterState);
                    }
                    else
                        OrderController.ShowAllOrders();
                    break;
                case 4:
                    o = OrderController.SelectOrder();
                    if (o != null)
                    {
                        OrderController.ShowOrderDetails(o);
                    }
                    break;
                case 5:
                    o = OrderController.SelectOrder(OrderStates.NEW);
                    if (o != null)
                    {
                        OrderController.ProcessOrder(o);
                    }
                    break;
                case 6:
                    o = OrderController.SelectOrder(OrderStates.STORAGE);
                    if (o != null)
                        OrderController.SendOrder(o);
                    break;
                case 7:
                    o = OrderController.SelectOrder(OrderStates.SENT);
                    if (o != null)
                        OrderController.CloseOrder(o);
                    break;
                case 8:
                    o = OrderController.SelectOrder();
                    if (o != null)
                    { 
                        OrderController.AddProductToOrder(o, ProductController);
                    }
                    break;
                case 9:
                    o = OrderController.SelectOrder();
                    if (o != null)
                    {
                        OrderController.RemoveProductFromOrder(o);
                    }
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja");
                    break;
            }
        }
        static void InitProductList()
        {
            ProductList.Add(new Product("Monitor", 499.99f));
            ProductList.Add(new Product("Karta graficzna", 2499.99f));
            ProductList.Add(new Product("Procesor", 2199.39f));
            ProductList.Add(new Product("Karta dźwiękowa", 299.99f));
            ProductList.Add(new Product("RAM", 99.99f));
            ProductList.Add(new Product("HDD", 79.99f));
            ProductList.Add(new Product("SSD", 114.99f));
            ProductList.Add(new Product("Obudowa", 320.50f));
            ProductList.Add(new Product("Zasilacz", 399.99f));
            ProductList.Add(new Product("Klawiatura", 57.80f));
            ProductList.Add(new Product("Myszka", 45.30f));
            ProductList.Add(new Product("Głośniki", 99.99f));
            ProductList.Add(new Product("Słuchawki", 170.99f));
            Console.Clear();
        }

        static void InitOrderList()
        {
            Order tempOrder = new Order(Clients.COMPANY, new Address("Polska", "Warszawa", "Mazowiecka 12", "00-001"), new CreditCardPayment());
            tempOrder.AddProduct(ProductList[0], 5);
            tempOrder.AddProduct(ProductList[4], 5);
            tempOrder.AddProduct(ProductList[5], 10);
            tempOrder.ProcessOrder();
            OrderList.Add(tempOrder);
            tempOrder = new Order(Clients.NATURAL_PERSON, new Address("Polska", "Gdańsk", "Warszawska 74/2", "12-345"), new CashPayment());
            tempOrder.AddProduct(ProductList[9], 1);
            tempOrder.AddProduct(ProductList[10], 1);
            OrderList.Add(tempOrder);
            tempOrder = new Order(Clients.COMPANY, new Address("Niemcy", "Hamburg", "", "20652"), new CreditCardPayment());
            tempOrder.AddProduct(ProductList[3], 6);
            tempOrder.AddProduct(ProductList[5], 2);
            tempOrder.AddProduct(ProductList[7], 9);
            OrderList.Add(tempOrder);
            Console.Clear();
        }
    }
}


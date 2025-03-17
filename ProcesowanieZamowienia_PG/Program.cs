namespace ProcesowanieZamowienia_PG
{
    class Program
    {
        static OrderController OrderController = new OrderController();
        static ProductController ProductController = new ProductController();
        
        static void Main(string[] args)
        {
            ProductController.InitProductList();
            OrderController.InitOrderList(ProductController.Products);

            int input = -1;
            do
            {
                DisplayMenu();
                input = Utils.IntegerInput("Wybierz opcję: ");
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
            switch (input)
            {
                case 1:
                    OrderController.UpsertOrder(new Order());
                    break;
                case 2:
                    OrderController.SelectOrder([OrderStates.NEW, OrderStates.ERROR]);
                    if (OrderController.CurrentOrder != null)
                    {
                        OrderController.UpsertOrder(OrderController.CurrentOrder);
                    }
                    break;
                case 3:
                    int choice = Utils.IntegerInput("Czy filtrować zamówienia po ich stanie?\n1 - TAK\n2 - NIE\n> ");
                    if (choice == 1)
                    {
                        foreach (OrderStates state in Enum.GetValues(typeof(OrderStates)))
                        {
                            Console.WriteLine($"{(int)state} - {Utils.StateToString(state)}");
                        }
                        OrderStates filterState = (OrderStates)Utils.IntegerInput("Wybierz stan zamówienia: ");
                        OrderController.ShowAllOrders([filterState]);
                    }
                    else
                        OrderController.ShowAllOrders();
                    break;
                case 4:
                    OrderController.SelectOrder();
                    if (OrderController.CurrentOrder != null)
                    {
                        OrderController.ShowOrderDetails();
                    }
                    break;
                case 5:
                    OrderController.SelectOrder([OrderStates.NEW]);
                    if (OrderController.CurrentOrder != null)
                    {
                        OrderController.ProcessOrder();
                    }
                    break;
                case 6:
                    OrderController.SelectOrder([OrderStates.STORAGE]);
                    if (OrderController.CurrentOrder != null)
                        OrderController.SendOrder();
                    break;
                case 7:
                    OrderController.SelectOrder([OrderStates.SENT]);
                    if (OrderController.CurrentOrder != null)
                        OrderController.CloseOrder();
                    break;
                case 8:
                    OrderController.SelectOrder([OrderStates.NEW, OrderStates.ERROR]);
                    if (OrderController.CurrentOrder != null)
                    { 
                        OrderController.AddProductToOrder(ProductController);
                    }
                    break;
                case 9:
                    OrderController.SelectOrder([OrderStates.NEW, OrderStates.ERROR]);
                    if (OrderController.CurrentOrder != null)
                    {
                        OrderController.RemoveProductFromOrder();
                    }
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja");
                    break;
            }
        }
    }
}


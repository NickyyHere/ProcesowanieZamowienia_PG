namespace ProcesowanieZamowienia_PG
{
    class Program
    {
        static OrderController _orderController = new OrderController();
        static ProductController _productController = new ProductController();
        
        static void Main(string[] args)
        {
            _productController.InitProductList();
            _orderController.InitOrderList(_productController.Products);

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
                    _orderController.UpsertOrder(new Order());
                    break;
                case 2:
                    _orderController.SelectOrder([OrderStates.NEW, OrderStates.ERROR]);
                    if (_orderController.CurrentOrder != null)
                    {
                        _orderController.UpsertOrder(_orderController.CurrentOrder);
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
                        _orderController.ShowAllOrders([filterState]);
                    }
                    else
                        _orderController.ShowAllOrders();
                    break;
                case 4:
                    _orderController.SelectOrder();
                    if (_orderController.CurrentOrder != null)
                    {
                        _orderController.ShowOrderDetails();
                    }
                    break;
                case 5:
                    _orderController.SelectOrder([OrderStates.NEW]);
                    if (_orderController.CurrentOrder != null)
                    {
                        _orderController.ProcessOrder();
                    }
                    break;
                case 6:
                    _orderController.SelectOrder([OrderStates.STORAGE]);
                    if (_orderController.CurrentOrder != null)
                        _orderController.SendOrder();
                    break;
                case 7:
                    _orderController.SelectOrder([OrderStates.SENT]);
                    if (_orderController.CurrentOrder != null)
                        _orderController.CloseOrder();
                    break;
                case 8:
                    _orderController.SelectOrder([OrderStates.NEW, OrderStates.ERROR]);
                    if (_orderController.CurrentOrder != null)
                    { 
                        _orderController.AddProductToOrder(_productController);
                    }
                    break;
                case 9:
                    _orderController.SelectOrder([OrderStates.NEW, OrderStates.ERROR]);
                    if (_orderController.CurrentOrder != null)
                    {
                        _orderController.RemoveProductFromOrder();
                    }
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja");
                    break;
            }
        }
    }
}


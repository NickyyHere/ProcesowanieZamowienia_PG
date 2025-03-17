namespace ProcesowanieZamowienia_PG
{
    internal static class Utils
    {
        public static string StateToString(OrderStates orderState)
        {
            return orderState switch
            {
                OrderStates.NEW => "Nowe",
                OrderStates.STORAGE => "W magazynie",
                OrderStates.RETURNED => "Zwrócone",
                OrderStates.SENT => "Wysłane",
                OrderStates.CLOSED => "Zamknięte",
                _ => "Błąd"
            };
        }

        public static string ClientToString(Clients client)
        {
            return client switch
            {
                Clients.COMPANY => "Firma",
                Clients.NATURAL_PERSON => "Osoba prywatna",
                _ => "Nieznany typ klienta"
            };
        }

        public static int IntegerInput(string message = "")
        {
            int choice;
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.Write($"Podana wartość powinna być liczbą całkowitą\n> ");
            }
            return choice;
        }
    }
}

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

                if (OrderAddress.IsValid())
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

        public Order() {
            OrderId = _orderId++;
            OrderAddress = new Address();
            OrderState = OrderStates.ERROR; // W celu wyciszenia powiadomienia o błędzie przy tworzeniu nowego zamówienia
            PaymentMethod = new CashPayment();
        }
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
            if(Products.ContainsKey(product))
                Products[product] += amount;
            else
                Products.Add(product, amount);
        }
        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
        public void ChangeOrderState(OrderStates orderState)
        {
            OrderState = orderState;
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
        public override string ToString()
        {
            return $"Zamówienie nr. {OrderId}, Status zamówienia: {Utils.StateToString(OrderState)}";
        }
    }
}

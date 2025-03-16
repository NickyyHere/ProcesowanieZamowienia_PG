namespace ProcesowanieZamowienia_PG
{
    internal class Product
    {
        private static int _productId = 0;
        public int ProductId { get; private set; }
		public string ProductName { get; private set; }
        public float ProductPrice { get; private set; }

        public Product(string productName, float productPrice)
        {
            ProductId = _productId++;
            ProductPrice = productPrice;
            ProductName = productName;
        }

        public override string ToString() 
        {
            return $"Produkt nr. {ProductId}. {ProductName} - {ProductPrice} zł";
        }
    }
}

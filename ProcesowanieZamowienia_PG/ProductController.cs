namespace ProcesowanieZamowienia_PG
{
    internal class ProductController
    {
        public List<Product> Products { get; private set; }
        public ProductController(List<Product> products)
        {
            Products = products;
        }
        public void ShowProducts()
        {
            foreach (Product product in Products)
            {
                Console.WriteLine(product);
            }
        }

        public Product SelectProduct()
        {
            ShowProducts();
            int productId = Utils.Instance.IntegerInput("Wprowadź numer produktu: ");
            if (productId < 0 || productId > Products.Count - 1)
            {
                Console.WriteLine("Nie ma produktu o podanym numerze");
                return null;
            }
            return Products[productId];
        }
    }
}

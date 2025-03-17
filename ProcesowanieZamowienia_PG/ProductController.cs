namespace ProcesowanieZamowienia_PG
{
    internal class ProductController
    {
        public List<Product> Products { get; private set; }
        public ProductController()
        {
            Products = new List<Product>();
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
            int productId = Utils.IntegerInput("Wprowadź numer produktu: ");
            if (productId < 0 || productId > Products.Count - 1)
            {
                Console.WriteLine("Nie ma produktu o podanym numerze");
                return null;
            }
            return Products[productId];
        }

        public void InitProductList()
        {
            Products.Add(new Product("Monitor", 499.99f));
            Products.Add(new Product("Karta graficzna", 2499.99f));
            Products.Add(new Product("Procesor", 2199.39f));
            Products.Add(new Product("Karta dźwiękowa", 299.99f));
            Products.Add(new Product("RAM", 99.99f));
            Products.Add(new Product("HDD", 79.99f));
            Products.Add(new Product("SSD", 114.99f));
            Products.Add(new Product("Obudowa", 320.50f));
            Products.Add(new Product("Zasilacz", 399.99f));
            Products.Add(new Product("Klawiatura", 57.80f));
            Products.Add(new Product("Myszka", 45.30f));
            Products.Add(new Product("Głośniki", 99.99f));
            Products.Add(new Product("Słuchawki", 170.99f));
            Console.Clear();
        }
    }
}

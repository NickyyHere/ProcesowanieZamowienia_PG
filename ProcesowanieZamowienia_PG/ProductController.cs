using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class ProductController
    {
        private static ProductController? instance;

        private ProductController() { }
        public static ProductController Instance
        {
            get
            {
                if (instance == null) instance = new ProductController();
                return instance;
            }
        }
        public void ShowProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
        }

        public Product SelectProduct(List<Product> products)
        {
            ShowProducts(products);
            int productId = Utils.Instance.IntegerInput("Wprowadź numer produktu: ");
            if (productId < 0 || productId > products.Count - 1)
            {
                Console.WriteLine("Nie ma produktu o podanym numerze");
                return null;
            }
            return products[productId];
        }
    }
}

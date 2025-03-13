using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesowanieZamowienia_PG
{
    internal class Product
    {
		public string ProductName { get; private set; }
        public float ProductPrice { get; private set; }

        public Product(string productName, float productPrice)
        {
            ProductPrice = productPrice;
            ProductName = productName;
        }
    }
}

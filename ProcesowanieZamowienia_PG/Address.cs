namespace ProcesowanieZamowienia_PG
{
    internal class Address
    {
        public string Country { get; private set; }
        public string City{ get; private set; }
        public string Street { get; private set; }
        public string PostalCode { get; private set; }

        public Address(string country, string city, string street, string postalCode)
        {
            Country = country;
            City = city;
            Street = street;
            PostalCode = postalCode;
        }
        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(Country) || string.IsNullOrWhiteSpace(City) || string.IsNullOrWhiteSpace(Street) || string.IsNullOrWhiteSpace(PostalCode);
        }
        public override string ToString()
        {
            return $"{Street}, {City} {PostalCode}, {Country}";
        }
    }
}

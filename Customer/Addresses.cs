namespace LNE_ERP
{
    public class Addresses
    {
        // Properties
        public int AddressID { get; set; }
        public string Streetname { get; set; } = string.Empty;
        public string Housenumber { get; set; } = string.Empty;
        public int Postalcode { get; set; }
        public string City { get; set; } = string.Empty;
    }
}

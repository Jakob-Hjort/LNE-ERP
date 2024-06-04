namespace LNE_ERP
{
    public class Company
    {
        // Properties
        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string StreetName { get; set; } = string.Empty;

        public string StreetNumber { get; set; } = string.Empty;

        public string HouseNumber { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public Currency Currency { get; set; }
    }
}

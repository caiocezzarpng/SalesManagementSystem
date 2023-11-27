using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.Models
{
    public class Department
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 5, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {
            this.Name = string.Empty;
        }

        public Department(string name)
        {
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime? initial, DateTime? final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }

}

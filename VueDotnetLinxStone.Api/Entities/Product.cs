using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VueDotnetLinxStone.Api.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [Precision(7, 2)]
        public decimal Price { get; set; }
        [StringLength(80)]
        public string BarCode { get; set; }
        //[StringLength(100)]
        //public string? Category { get; set; }
        //public string Description { get; set; }
        public string Image { get; set; }
    }

    public class ProductCreateDto
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string BarCode { get; set; }
        public string Image { get; set; }
    }

    public class FakeStoreProductDto
    {
        public string title { get; set; }
        public decimal price { get; set; }
        public string? description { get; set; }
        public string? category { get; set; }
        public string image { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Models
{
    public class Product
    {
        public Product()
        {
            CreateDate = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public byte[] Image { get; set; }
        public double Rating { get; set; }
    }
}

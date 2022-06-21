using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItemAPI.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title field required!")]
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        [Range(1, 50, ErrorMessage = "Quantity should be from 1 to 50")]
        [Required(ErrorMessage = "Specify the quantity")]
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string Seller { get; set; }
    }
}

using ProductDeliveryManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProductDeliveryManagement.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Order date"), DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime OrderDate { get; set; } = DateTime.Today;
        [Required, StringLength(50), Display(Name = "Customer name")]
        public string CustomerName { get; set; }
        [Required, StringLength(150), Display(Name = "Customer address")]
        public string CustomerAddress { get; set; }
        [Required, StringLength(20), Display(Name = "Customer phone")]
        public string CustomerPhone { get; set; }
        [Required, ForeignKey("Product"), Display(Name = "Product id")]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; } = 1;
        //
        public virtual Product Product { get; set; }
        public OrderDelivery OrderDelivery { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
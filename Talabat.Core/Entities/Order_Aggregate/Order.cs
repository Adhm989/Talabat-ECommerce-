using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity 
    {
        public Order()
        {

        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal , string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            PaymentIntentId= paymentIntentId;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public Address ShippingAddress { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>(); // Navigational Prop
        public decimal SubTotal { get; set; } // shipping not included price * quantity 
        //[NotMapped]
        //public decimal Total { get => SubTotal + DeliveryMethod.Cost; }  // derived attribute
        public decimal GetTotal() =>
            SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; } 
    }
}

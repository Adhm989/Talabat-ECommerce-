using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.DTOs
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public Address ShippingAddress { get; set; }

        public DateTimeOffset OrderDate { get; set; } 
        public string OrderStatus { get; set; } 
        //public DeliveryMethod DeliveryMethod { get; set; }

        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodcost { get; set; }

        public ICollection<OrderItemDTO> Items { get; set; }  // Navigational Prop
        public decimal SubTotal { get; set; } // shipping not included price * quantity 
        //[NotMapped]
        //public decimal Total { get => SubTotal + DeliveryMethod.Cost; }  // derived attribute
        
        public string PaymentIntent { get; set; }
        public decimal Total { get; set; }
    }
}

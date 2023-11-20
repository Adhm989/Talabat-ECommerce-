using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class DeliveryMethod : BaseEntity
    {
        public DeliveryMethod()
        {

        }
        public DeliveryMethod(string name, decimal cost, string deliveryTime, string description)
        {
            Name = name;
            Cost = cost;
            DeliveryTime = deliveryTime;
            Description = description;
        }

        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
    }
}

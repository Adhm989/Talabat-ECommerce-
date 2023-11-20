using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.Order_Specifications
{
    public class OrderWithPaymentIntentIdSpecs : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentIdSpecs(string paymentIntentId) : base(O => O.PaymentIntentId == paymentIntentId)
        {
        }
    }
}

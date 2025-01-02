using Shared.Events.common;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class OrderCreatedEvent:IEvent
    {
        public Guid OrdereId { get; set; }
        public Guid BuyerId { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }

    }
}

using Shared.Events.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class StockReservedEvent :IEvent
    {
        public Guid OrdereId { get; set; }
        public Guid BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

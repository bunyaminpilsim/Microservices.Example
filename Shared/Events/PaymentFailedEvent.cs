using Shared.Events.common;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class PaymentFailedEvent :IEvent
    {
        public Guid OrdereId { get; set; }
        public string Message { get; set; }
    }
}

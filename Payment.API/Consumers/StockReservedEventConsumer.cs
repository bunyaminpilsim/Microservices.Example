using MassTransit;
using Shared.Events;

namespace Payment.API.Consumers
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        readonly IPublishEndpoint _publishEndpoint;

        public StockReservedEventConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            if (true)
            {
                PaymentCompletedEvent paymentCompletedEvent = new()
                {
                    OrderId = context.Message.OrdereId
                };
                _publishEndpoint.Publish(paymentCompletedEvent);
                Console.WriteLine("Ödeme Başarlı");
            }
            else
            {
                PaymentFailedEvent paymentFailedEvent = new()
                {
                    OrdereId = context.Message.OrdereId,
                    Message = "akiye yetersizz.."
                };
                _publishEndpoint.Publish(paymentFailedEvent);
                Console.WriteLine("Ödeme Başarısızz");

            }
            return Task.CompletedTask;
        }
    }
}

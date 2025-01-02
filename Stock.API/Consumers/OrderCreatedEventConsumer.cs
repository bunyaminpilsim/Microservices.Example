﻿using MassTransit;
using MongoDB.Driver;
using Shared;
using Shared.Events;
using Shared.Messages;
using Stock.API.Services;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        IMongoCollection<Stock.API.Models.Entities.Stock> _stockCollection;
        ISendEndpointProvider _sendEndpointProvider;
        IPublishEndpoint _publishEndpoint;
        public OrderCreatedEventConsumer(MongoDBService mongoDBService1, ISendEndpointProvider sendEndpointProvider , IPublishEndpoint publishEndpoint)
        {
            _stockCollection = mongoDBService1.GetCollection<Stock.API.Models.Entities.Stock>();
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
        }


        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            Console.WriteLine(context.Message.OrdereId + " - " + context.Message.BuyerId);

            List<bool> stockResult = new();
            foreach (OrderItemMessage orderItem in context.Message.OrderItems)
            {
                stockResult.Add((await _stockCollection.FindAsync(s => s.ProductId == orderItem.ProductId && s.Count >= orderItem.Count)).Any());
            }
            if (stockResult.TrueForAll(sr => sr.Equals(true)))
            {
                foreach (OrderItemMessage orderItem in context.Message.OrderItems)
                {
                    Stock.API.Models.Entities.Stock stock = await (await _stockCollection.FindAsync(s => s.ProductId == orderItem.ProductId)).FirstOrDefaultAsync();

                    stock.Count -= orderItem.Count;
                    await _stockCollection.FindOneAndReplaceAsync(s => s.ProductId == orderItem.ProductId, stock);
                }
                StockReservedEvent stockReservedEvent = new()
                {
                    BuyerId = context.Message.BuyerId,
                    OrdereId = context.Message.OrdereId,
                    TotalPrice = context.Message.TotalPrice,
                };
                ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.Payment_StockReservedEventQueue}"));
                await sendEndpoint.Send(stockReservedEvent);
                await Console.Out.WriteLineAsync("Stok işlemleri Başarlı");

            }
            else
            {
                //olumsuz dönerse
                StockNotReservedEvent stockNotReservedEvent = new()
                {
                    BuyerId = context.Message.BuyerId,
                    OrdereId = context.Message.OrdereId,
                    Message ="...."
                };
                await _publishEndpoint.Publish(stockNotReservedEvent);
                await Console.Out.WriteLineAsync("Stok işlemleri Başarısız");

            }
            //return Task.CompletedTask;
        }
    }
}

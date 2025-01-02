
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Consumers;
using Order.API.Models;
using Shared;

namespace Order.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<OrderAPIDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
            });

            builder.Services.AddMassTransit(configurator =>
            {
                configurator.AddConsumer<PaymentCompletedEventConsumer>();

                configurator.AddConsumer<StockNotReservedEventConsumer>();

                configurator.AddConsumer<PaymentFailedEventConsumer>();
                configurator.UsingRabbitMq((context, _configurator) =>
                {
                    _configurator.Host(builder.Configuration["RabbitMQ"]);

                    _configurator.ReceiveEndpoint(RabbitMQSettings.Order_PaymentCompletedEventQueue, e => e.ConfigureConsumer<PaymentCompletedEventConsumer>(context));

                    _configurator.ReceiveEndpoint(RabbitMQSettings.Order_StockNotReservedEventQueue, e => e.ConfigureConsumer<StockNotReservedEventConsumer>(context));

                    _configurator.ReceiveEndpoint(RabbitMQSettings.Order_PaymentFailedEventQueue, e => e.ConfigureConsumer<PaymentFailedEventConsumer>(context));
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}



using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using OrderService.Common;
using OrderService.Orders;
using OrderService.Persistence;
using Shared.Events;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<OrderContext>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddServiceDiscovery(o => o.UseConsul());
builder.Services.AddHttpClient<CreateShipping.Client>(c =>
{
    c.BaseAddress = new Uri("http://shipping-service/");
})
.AddServiceDiscovery()
.AddRoundRobinLoadBalancer();
builder.Services.AddFastEndpoints()
    .SwaggerDocument();


var app = builder.Build();
ServiceLocator.SetLocatorProvider(builder.Services.BuildServiceProvider());
DomainEvents.Mediator = () => ServiceLocator.Current.GetInstance<IMediator>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
}).UseSwaggerGen(); ;

app.Run();
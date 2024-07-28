

using FastEndpoints;
using FastEndpoints.Swagger;
using OrderService.Persistence;
using OrderService.Shipping;
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
builder.Services.AddFastEndpoints()
    .SwaggerDocument();
builder.Services.AddServiceDiscovery(o => o.UseConsul());

builder.Services.AddHttpClient<CreateShipping.Client>(c =>
{
    c.BaseAddress = new Uri("http://shipping-service/");
}).AddServiceDiscovery()
.AddRoundRobinLoadBalancer(); ;


var app = builder.Build();

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
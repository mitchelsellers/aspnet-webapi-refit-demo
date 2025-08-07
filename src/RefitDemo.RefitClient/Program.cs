using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using RefitDemo.Api;
using RefitDemo.Api.Models;

var builder = Host.CreateApplicationBuilder(args);

string baseUrl = "https://localhost:7205/";

builder.Services.AddRefitClient<IApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

using var host = builder.Build();

var api = host.Services.GetRequiredService<IApi>();

// Product API
var products = await api.GetProducts();
Console.WriteLine($"Product GET successful. Count: {products.Count}");
var createdProduct = await api.AddProduct(new ProductDto { Name = "Tablet", Price = 299.99M });
Console.WriteLine($"Product POST successful. Created Id: {createdProduct.Id}");

// Order API
var orders = await api.GetOrders();
Console.WriteLine($"Order GET successful. Count: {orders.Count}");
var createdOrder = await api.CreateOrder(new OrderDto { ProductName = "Laptop", Quantity = 2 });
Console.WriteLine($"Order POST successful. Created Id: {createdOrder.Id}");

// User API
var users = await api.GetUsers();
Console.WriteLine($"User GET successful. Count: {users.Count}");
var createdUser = await api.RegisterUser(new UserDto { Username = "charlie" });
Console.WriteLine($"User POST successful. Created Id: {createdUser.Id}");

// Message API
var messages = await api.GetMessages();
Console.WriteLine($"Message GET successful. Count: {messages.Count}");
var createdMessage = await api.SendMessage(new MessageDto { Content = "Hello, world!" });
Console.WriteLine($"Message POST successful. Created Id: {createdMessage.Id}");

// WeatherForecast API
var forecasts = await api.GetWeatherForecast();
// Lots of special stuff here
Console.WriteLine($"Was Response Status Code Success: {forecasts.IsSuccessStatusCode}");
Console.WriteLine($"Was response fully successful: {forecasts.IsSuccessful}");
Console.WriteLine($"WeatherForecast GET successful. Count: {forecasts.Content?.Count}");
Console.ReadLine();


// Consolidated Refit interface
public interface IApi
{
    HttpClient Client { get; }

    // Product
    [Get("/product")]
    Task<List<ProductDto>> GetProducts();
    [Post("/product")]
    Task<ProductDto> AddProduct([Body] ProductDto product);

    // Order
    [Get("/order")]
    Task<List<OrderDto>> GetOrders();
    [Post("/order")]
    Task<OrderDto> CreateOrder([Body] OrderDto order);

    // User
    [Get("/user")]
    Task<List<UserDto>> GetUsers();
    [Post("/user")]
    Task<UserDto> RegisterUser([Body] UserDto user);

    // Message
    [Get("/message")]
    Task<List<MessageDto>> GetMessages();
    [Post("/message")]
    Task<MessageDto> SendMessage([Body] MessageDto message);

    // WeatherForecast
    [Get("/weatherforecast")]
    Task<IApiResponse<List<WeatherForecast>>> GetWeatherForecast();
}

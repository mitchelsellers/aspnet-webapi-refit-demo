using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RefitDemo.Api;
using RefitDemo.Api.Models;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddTransient<ProductApiService>();
builder.Services.AddTransient<OrderApiService>();
builder.Services.AddTransient<UserApiService>();
builder.Services.AddTransient<MessageApiService>();
builder.Services.AddTransient<WeatherForecastApiService>();

using var host = builder.Build();

var productService = host.Services.GetRequiredService<ProductApiService>();
var orderService = host.Services.GetRequiredService<OrderApiService>();
var userService = host.Services.GetRequiredService<UserApiService>();
var messageService = host.Services.GetRequiredService<MessageApiService>();
var weatherService = host.Services.GetRequiredService<WeatherForecastApiService>();

// Call Product API (GET)
var products = await productService.GetProductsAsync();
Console.WriteLine($"Product API call successful. Count: {products.Count}");
// Call Product API (POST)
var newProduct = new ProductDto { Name = "Tablet", Price = 299.99M };
var createdProduct = await productService.AddProductAsync(newProduct);
Console.WriteLine($"Product POST call successful. Created Id: {createdProduct?.Id}");

// Call Order API (GET)
var orders = await orderService.GetOrdersAsync();
Console.WriteLine($"Order API call successful. Count: {orders.Count}");
// Call Order API (POST)
var newOrder = new OrderDto { ProductName = "Laptop", Quantity = 2 };
var createdOrder = await orderService.CreateOrderAsync(newOrder);
Console.WriteLine($"Order POST call successful. Created Id: {createdOrder?.Id}");

// Call User API (GET)
var users = await userService.GetUsersAsync();
Console.WriteLine($"User API call successful. Count: {users.Count}");
// Call User API (POST)
var newUser = new UserDto { Username = "charlie" };
var createdUser = await userService.RegisterUserAsync(newUser);
Console.WriteLine($"User POST call successful. Created Id: {createdUser?.Id}");

// Call Message API (GET)
var messages = await messageService.GetMessagesAsync();
Console.WriteLine($"Message API call successful. Count: {messages.Count}");
// Call Message API (POST)
var newMessage = new MessageDto { Content = "Hello, world!" };
var createdMessage = await messageService.SendMessageAsync(newMessage);
Console.WriteLine($"Message POST call successful. Created Id: {createdMessage?.Id}");

// Call WeatherForecast API (GET only)
var forecasts = await weatherService.GetWeatherForecastAsync();
Console.WriteLine($"WeatherForecast API call successful. Count: {forecasts.Count}");

Console.Read();

// Service implementations
public class ProductApiService
{
    private readonly HttpClient _httpClient;
    public ProductApiService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7205/");
    }
    public async Task<List<ProductDto>> GetProductsAsync() =>
        await _httpClient.GetFromJsonAsync<List<ProductDto>>("product") ?? new();
    public async Task<ProductDto?> AddProductAsync(ProductDto product)
    {
        var response = await _httpClient.PostAsJsonAsync("product", product);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDto>();
    }
}

public class OrderApiService
{
    private readonly HttpClient _httpClient;
    public OrderApiService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7205/");
    }
    public async Task<List<OrderDto>> GetOrdersAsync() =>
        await _httpClient.GetFromJsonAsync<List<OrderDto>>("order") ?? new();
    public async Task<OrderDto?> CreateOrderAsync(OrderDto order)
    {
        var response = await _httpClient.PostAsJsonAsync("order", order);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<OrderDto>();
    }
}

public class UserApiService
{
    private readonly HttpClient _httpClient;
    public UserApiService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7205/");
    }
    public async Task<List<UserDto>> GetUsersAsync() =>
        await _httpClient.GetFromJsonAsync<List<UserDto>>("user") ?? new();
    public async Task<UserDto?> RegisterUserAsync(UserDto user)
    {
        var response = await _httpClient.PostAsJsonAsync("user", user);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDto>();
    }
}

public class MessageApiService
{
    private readonly HttpClient _httpClient;
    public MessageApiService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7205/");
    }
    public async Task<List<MessageDto>> GetMessagesAsync() =>
        await _httpClient.GetFromJsonAsync<List<MessageDto>>("message") ?? new();
    public async Task<MessageDto?> SendMessageAsync(MessageDto message)
    {
        var response = await _httpClient.PostAsJsonAsync("message", message);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MessageDto>();
    }
}

public class WeatherForecastApiService
{
    private readonly HttpClient _httpClient;
    public WeatherForecastApiService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:7205/");
    }
    public async Task<List<WeatherForecast>> GetWeatherForecastAsync() =>
        await _httpClient.GetFromJsonAsync<List<WeatherForecast>>("weatherforecast") ?? new();
}

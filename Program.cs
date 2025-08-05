using System.Net;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Listen on all network interfaces (localhost and LAN IPs)
builder.WebHost.UseUrls("http://0.0.0.0:5080");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Always enable Swagger (for internal/testing use only)
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

// Automatically open browser on startup only in Development environment
if (app.Environment.IsDevelopment())
{
    try
    {
        // Get local IPv4 address
        string localIp = Dns.GetHostEntry(Dns.GetHostName())
            .AddressList
            .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString() ?? "localhost";
        var url = $"http://{localIp}:5080/swagger";
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
    catch { /* Ignore errors */ }
}

app.Run();

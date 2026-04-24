using EsteknikCRM.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// API dışarı aç
builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔥 Swagger'ı HER ZAMAN aç (test için)
app.UseSwagger();
app.UseSwaggerUI();

// Test endpoint
app.MapGet("/", () => "EsteknikCRM API çalışıyor");

// ❗ Şimdilik bunu kapat (HTTP kullanıyorsun)
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
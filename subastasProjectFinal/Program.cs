using Microsoft.Extensions.Options;
using MongoDB.Driver;
using subastasProjectFinal.Connection;
using subastasProjectFinal.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar configuración de MongoDB
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

// Registrar servicios adicionales
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<NewsletterService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<HistoryService>();
builder.Services.AddScoped<FavoriteService>();
builder.Services.AddScoped<FAQService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<BidService>();
builder.Services.AddScoped<AuctionService>();

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
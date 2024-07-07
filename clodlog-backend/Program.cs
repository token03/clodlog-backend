using clodlog_backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CardService
builder.Services.AddSingleton<SetService>(sp => new SetService("pokemon-tcg-data/sets/"));
builder.Services.AddSingleton<CardService>(sp => 
{
    var setService = sp.GetRequiredService<SetService>();
    var cardService = new CardService (
        "pokemon-tcg-data/cards/en/", 
        "pokemon-tcg-data/prices/",
        setService
        );
    setService.SetCardService(cardService);
    return cardService;
});

// Add controllers
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

app.Services.GetRequiredService<CardService>();
app.Services.GetRequiredService<SetService>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
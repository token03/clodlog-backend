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
    var cardService = new CardService("pokemon-tcg-data/cards/en/", setService);
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

var app = builder.Build();

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
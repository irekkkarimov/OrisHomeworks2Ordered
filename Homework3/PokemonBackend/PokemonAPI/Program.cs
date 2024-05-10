using PokemonAPI.Services;
using PokemonAPI.Services.PokeApiService;
using PokemonAPI.Services.PokemonCacheHandler;
using Redis.OM;

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(
    new RedisConnectionProvider(builder.Configuration.GetConnectionString("REDIS_CONNECTION_STRING")!));
builder.Services.AddHostedService<IndexCreationService>();
builder.Services.AddScoped<IPokemonCacheHandler, PokemonCacheHandler>();
builder.Services.AddScoped<IPokeApiService, PokeApiService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
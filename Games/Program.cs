// using Games.Data;
// using Games.Endpoints;

// var builder = WebApplication.CreateBuilder(args);

// var connString = builder.Configuration.GetConnectionString("GameStore");
// builder.Services.AddSqlite<GameStoreContext>(connString);
// var app = builder.Build();

// app.MapGamesEndpoints();
// app.Run();
using Games.Data;
using Games.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Debugging output to check connection string
Console.WriteLine("Connection String: " + builder.Configuration.GetConnectionString("GameStore"));

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();

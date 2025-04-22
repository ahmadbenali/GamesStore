using WebApplication1.Dtos;
using WebApplication1.GameEndPoint;


var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapGamesEndPoint();

app.Run();

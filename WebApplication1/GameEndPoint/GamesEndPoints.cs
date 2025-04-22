using WebApplication1.Dtos;

namespace WebApplication1.GameEndPoint;

public static class GamesEndPoints
{

    const string GetGameEndPointName="GetGame";

    //NOTE , THIS LIST NOT THREAD SAFE
    private static readonly List<GameDto> games=[
        new(
            1,
            "helo",
            "attack",
            20,
            new DateOnly(2000,2,2)
        ),
        new(
            2,
            "call of duty",
            "attack",
            30,
            new DateOnly(2002,3,2)
        )
    ];

    public static WebApplication MapGamesEndPoint(this WebApplication app)
    {
        var group = app.MapGroup("Games").WithParameterValidation();

        //GET GAMES
        group.MapGet("/",() => games);

        //GET GAMES / ID
        group.MapGet("/{id}",(int id) => 
        {
            GameDto?game =games.Find(games => games.Id==id);

            return game is null ? Results.NotFound():Results.Ok(game);// IF you put game , will be error
        })
        .WithName(GetGameEndPointName);

        //POST GAMES
        group.MapPost("/",(CreateGame newGame)=>
        {
            // if(string.IsNullOrEmpty(newGame.Name))
            // {
            //     return Results.BadRequest("Name is Required");
            // }
             GameDto game = new(

                games.Count +1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);

            games.Add(game);
            //TO RETURN TO THE CLINET 
            return Results.CreatedAtRoute(GetGameEndPointName,new{id=game.Id},game);
        });

        //PUT GAMES / ID
        group.MapPut("/{id}",(int id,CreateGame newGame) =>
        {
            var index =  games.FindIndex(games =>games.Id == id);

            if(index == -1)
            return Results.NotFound();

            
            games[index]=new GameDto(
                id,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate

            );

            return Results.NoContent();


        });

        //DELETE 

        group.MapDelete("/{id}",(int id)=>
        {

            games.RemoveAll(games => games.Id == id);//NOTE , IT WORKS JUST IN REMOVEALL NOT IN REMOVE
            return Results.NoContent();
        });

    return app;
    }
   
}

using Games.Dtos;

namespace Games.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";
    private static readonly List<GameDto> games = [
        new(1, "Street Fighter", "Fighting", 100.00m, new DateOnly(2021, 1, 1)),
    new(2, "Super Mario", "Platformer", 200.00m, new DateOnly(2021, 1, 1)),
    new(3, "Super Mario 64", "Platformer", 300.00m, new DateOnly(2021, 1, 1))];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        // GET /games 
        group.MapGet("/", () => games);

        // GET /games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.FirstOrDefault(g => g.Id == id);
            return game is null ? Results.NotFound(new { Message = "Game not found" }) : Results.Ok(game);
        }).WithName(GetGameEndPointName);

        // POST /games

        group.MapPost("/", (CreateGamesDtos newGame) =>
        {
            GameDto game = new(games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseData);
            games.Add(game);
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        // PUT /games/{id}

        group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
        {
            var index = games.FindIndex(g => g.Id == id);
            if (index == -1)
            {
                return Results.NotFound(new { Message = "Game not found" });
            }
            games[index] = new GameDto(id, updateGame.Name, updateGame.Genre, updateGame.Price, updateGame.ReleaseData);
            return Results.NoContent();
        });

        // DELETE /games/{id}

        group.MapDelete("/{id}", (int id) =>
        {
            var index = games.FindIndex(g => g.Id == id);
            games.RemoveAt(index);
            return Results.NoContent();
        });
        return group;
    }
}

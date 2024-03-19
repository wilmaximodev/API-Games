namespace TrybeGames;

public class TrybeGamesDatabase
{
    public List<Game> Games = new List<Game>();

    public List<GameStudio> GameStudios = new List<GameStudio>();

    public List<Player> Players = new List<Player>();

    // 4. Crie a funcionalidade de buscar jogos desenvolvidos por um estúdio de jogos
    public List<Game> GetGamesDevelopedBy(GameStudio gameStudio)
    {
        var games = Games.Where(g => g.DeveloperStudio == gameStudio.Id).ToList();
        return games;
    }

    // 5. Crie a funcionalidade de buscar jogos jogados por uma pessoa jogadora
    public List<Game> GetGamesPlayedBy(Player player)
    {
        var playedGames = Games.Where(g => g.Players.Contains(player.Id)).ToList();
        return playedGames;
    }

    // 6. Crie a funcionalidade de buscar jogos comprados por uma pessoa jogadora
    public List<Game> GetGamesOwnedBy(Player playerEntry)
    {
        var ownedGames = Games.Where(g => playerEntry.GamesOwned.Contains(g.Id)).ToList();
        return ownedGames;
    }


    // 7. Crie a funcionalidade de buscar todos os jogos junto do nome do estúdio desenvolvedor
    public List<GameWithStudio> GetGamesWithStudio()
    {
        var gamesWithStudio = Games.Join(GameStudios,
        game => game.DeveloperStudio,
        studio => studio.Id,
        (game, studio) => new GameWithStudio
        {
            GameName = game.Name,
            StudioName = studio.Name,
            NumberOfPlayers = game.Players.Count,
        }).ToList();

        return gamesWithStudio;
    }
    
    // 8. Crie a funcionalidade de buscar todos os diferentes Tipos de jogos dentre os jogos cadastrados
    public List<GameType> GetGameTypes()
    {
        var gameTypes = Games.Select(g => g.GameType).Distinct().ToList();
        return gameTypes;
    }

    // 9. Crie a funcionalidade de buscar todos os estúdios de jogos junto dos seus jogos desenvolvidos com suas pessoas jogadoras
    public List<StudioGamesPlayers> GetStudiosWithGamesAndPlayers()
    {
        var studiosWithGamesAndPlayers = GameStudios.Select(studio => new StudioGamesPlayers {
            GameStudioName = studio.Name,
            Games = Games.Where(g => g.DeveloperStudio == studio.Id).Select(g => new GamePlayer {
                GameName = g.Name,
                Players = Players.Where(p => g.Players.Contains(p.Id)).ToList()
            }).ToList()
        }).ToList();

        return studiosWithGamesAndPlayers;
    }
}

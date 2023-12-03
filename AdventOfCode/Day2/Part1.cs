namespace Day2;

internal static class Part1
{

    private static Dictionary<string,int> _config = new Dictionary<string, int>
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 },
    };

    public static int GetSumOfIds(string[] games)
    {
        var sumOfIds = games
            .Select(GetGameResult)
            .Where(result => result.HighestScores["red"] <= _config["red"])
            .Where(result => result.HighestScores["green"] <= _config["green"])
            .Where(result => result.HighestScores["blue"] <= _config["blue"])
            .Sum(result => result.ID);

        return sumOfIds;
    }

    private static GameResult GetGameResult(string game)
    {
        var split = game.Split(':');
        var id = int.Parse(split.First().Split(" ").Last());
        var gameRuns = split.Last().Split(";");

        var gameResult = new GameResult(id);

        foreach (var gameRun in gameRuns)
        {
            var rgb = gameRun.Trim().Split(",").Select(part => part.Trim().Split(" "));

            foreach (var info in rgb)
            {
                var color = info.Last();
                var count = int.Parse(info.First());

                var currentScore = gameResult.HighestScores[color];

                if (currentScore < count)
                {
                    gameResult.HighestScores[color] = count;
                }
            }
        }

        return gameResult;
    }

    private class GameResult
    {
        public GameResult(int iD)
        {
            ID = iD;
        }

        public int ID { get; set; }
        public Dictionary<string, int> HighestScores { get; set; } = new()
    {
        { "red", 0 },
        { "green", 0 },
        { "blue", 0 },
    };
    }
}

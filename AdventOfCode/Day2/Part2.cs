namespace Day2;

internal static class Part2
{
    public static int GetSumOfPows(string[] games)
    {
        return games
            .Select(GetGameResult)
            .Select(game => game.CubesNeeded["red"] * game.CubesNeeded["green"] * game.CubesNeeded["blue"])
            .Sum();
    }

    private static GameResult GetGameResult(string game)
    {
        var gameRuns = game.Split(':').Last().Split(";");

        var gameResult = new GameResult();

        foreach (var gameRun in gameRuns)
        {
            var rgb = gameRun.Trim().Split(",").Select(part => part.Trim().Split(" "));

            foreach (var info in rgb)
            {
                var color = info.Last();
                var count = int.Parse(info.First());

                var currentScore = gameResult.CubesNeeded[color];

                if (currentScore < count)
                {
                    gameResult.CubesNeeded[color] = count;
                }
            }
        }

        return gameResult;
    }

    private class GameResult
    {
        public Dictionary<string, int> CubesNeeded { get; set; } = new()
        {
            { "red", 0 },
            { "green", 0 },
            { "blue", 0 },
        };
    }
}

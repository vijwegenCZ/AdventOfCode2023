namespace Template;

internal static class Part1
{
    public static int GetPoints(string[] inputLines)
    {
        var sum = inputLines
            .Select(line => GetMatchcard(line))
            .Select(matchcard => GetMatchcardPoints(matchcard))
            .Select(points => points == 0 ? 0 : Math.Pow(2, points - 1))
            .Select(Convert.ToInt32)
            .Sum();

        return sum;
    }

    private static int GetMatchcardPoints(Matchcard matchcard)
    {
        return matchcard.WinningNumbers.Intersect(matchcard.ExistingNumbers).Count();
    }

    private static Matchcard GetMatchcard(string line)
    {
        var removedMetaData = line.Split(":").Last().Trim().Split("|");

        var winningNumbers = GetNumbers(removedMetaData.First());
        var existingNumbers = GetNumbers(removedMetaData.Last());

        return new Matchcard(winningNumbers, existingNumbers);
    }

    private static IEnumerable<int> GetNumbers(string line)
    {
        return line
                    .Trim()
                    .Split(" ")
                    .Where(num => !string.IsNullOrWhiteSpace(num))
                    .Select(int.Parse);
    }

    private sealed record Matchcard(IEnumerable<int> WinningNumbers, IEnumerable<int> ExistingNumbers);
}

namespace Template;

internal static class Part2
{
    public static int GetCountOfScratchCards(string[] inputLines)
    {
        var originalCards = inputLines
            .Select((line, index) => KeyValuePair.Create(index, GetScratchcard(line)))
            .ToDictionary(pair => pair.Key, pair => pair.Value);

        var originalCardsWinningNumber = new Dictionary<int, int>();

        var resultingCards = originalCards.Count;

        foreach(var originalCard in originalCards)
        {
            var totalCountOfNestedCards = 0;
            var card = originalCard.Value;
            var cardsToEvaluate = card.RelevantNumbers;

            var nested = cardsToEvaluate.Select(c => originalCards[c]).ToList();

            while (nested.Any())
            {
                totalCountOfNestedCards += nested.Count;

                var foo = nested
                    .SelectMany(s => s.RelevantNumbers)
                    .Select(s => originalCards[s]);

                nested = foo.ToList();
            }

            originalCardsWinningNumber[originalCard.Key] = totalCountOfNestedCards;
        }

        var wins = originalCards
            .Select(s => originalCardsWinningNumber[s.Key])
            .Sum();

        return resultingCards + wins;
    }

    private static Scratchcard GetScratchcard(string line)
    {
        var removedMetaData = line.Split(":").Last().Trim().Split("|");

        var winningNumbers = GetNumbers(removedMetaData.First());
        var existingNumbers = GetNumbers(removedMetaData.Last());

        return new Scratchcard(winningNumbers, existingNumbers);
    }

    private static IEnumerable<int> GetNumbers(string line)
    {
        return line
                    .Trim()
                    .Split(" ")
                    .Where(num => !string.IsNullOrWhiteSpace(num))
                    .Select(int.Parse);
    }

    private sealed class Scratchcard
    {
        public Scratchcard(IEnumerable<int> winningNumbers, IEnumerable<int> existingNumbers)
        {            
            RelevantNumbers = winningNumbers.Intersect(existingNumbers);
        }

        public IEnumerable<int> RelevantNumbers { get; }
    }
}

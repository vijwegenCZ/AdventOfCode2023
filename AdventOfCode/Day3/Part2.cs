using System.Text.RegularExpressions;

namespace Template;

internal static class Part2
{
    private static readonly Regex _numberRegex = new(@"[0-9]+");
    private static readonly Regex _gearRegex = new(@"\*");

    public static int GetGearSum(string[] inputLines)
    {
        var currentSum = 0;

        IEnumerable<Number> prevNumbers = new List<Number>();
        var currentNumbers = GetNumbers(inputLines[0]);
        var nextNumbers = GetNumbers(inputLines[1]);
        var nextIndex = 1;

        for (int index = 0; index < inputLines.Length; index++)
        {
            var line = inputLines[index];

            var currentGears = GetGearPositions(line);

            nextIndex = nextIndex == (inputLines.Length - 1)
                ? nextIndex
                : nextIndex + 1;

            foreach (var currentGear in currentGears)
            {
                var relevantPrevNumbers = prevNumbers
                     .Where(number => IsAdjacent(number, currentGear));

                var relevantCurrentNumbers = currentNumbers
                    .Where(number => IsAdjacent(number, currentGear));

                var relevantNextNumbers = nextNumbers
                    .Where(number => IsAdjacent(number, currentGear));

                var totalCount = relevantPrevNumbers.Count() + relevantCurrentNumbers.Count() + relevantNextNumbers.Count();

                if (totalCount == 2)
                {
                    var currentProduct = 1;

                    Console.Write($"Index: {index + 1}. ");

                    currentProduct = AddValuesFromList(relevantPrevNumbers, currentProduct);
                    currentProduct = AddValuesFromList(relevantCurrentNumbers, currentProduct);
                    currentProduct = AddValuesFromList(relevantNextNumbers, currentProduct);

                    Console.WriteLine($"Product: {currentProduct}.");

                    currentSum += currentProduct;
                }
            }

            prevNumbers = currentNumbers.ToList();
            currentNumbers = nextNumbers.ToList();
            nextNumbers = GetNumbers(inputLines[nextIndex]);
        }

        return currentSum;
    }

    private static bool IsAdjacent(Number s, int currentGear)
    {
        return
            s.IndexStart == currentGear + 1
            || s.IndexStart == currentGear
            || s.IndexEnd == currentGear - 1
            || s.IndexEnd == currentGear
            || s.IndexStart >= currentGear - 1 && s.IndexEnd <= currentGear + 1;
    }

    private static int AddValuesFromList(IEnumerable<Number> relevantPrevNumbers, int currentProduct)
    {
        foreach (var numberValue in relevantPrevNumbers.Select(number => number.Value))
        {
            Console.Write($"Num: {numberValue}. ");
            currentProduct *= numberValue;
        }

        return currentProduct;
    }

    private static IEnumerable<Number> GetNumbers(string line)
    {
        return _numberRegex
                        .Matches(line)
                        .Select(match => new Number(
                            match.Index + 1, 
                            match.Index + match.Length, 
                            int.Parse(match.Value)));
    }

    private static IEnumerable<int> GetGearPositions(string input)
    {
        return _gearRegex
            .Matches(input)
            .Select(match => match.Index + 1);
    }

    private sealed record Number(int IndexStart, int IndexEnd, int Value);
}

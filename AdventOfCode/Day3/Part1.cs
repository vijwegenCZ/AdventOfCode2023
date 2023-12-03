using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Template;

internal static class Part1
{
    private static Regex _numberRegex = new(@"[0-9]+");
    private static Regex _symbolRegex = new(@"[^a-zA-Z0-9.]");

    public static int GetPartNumberSum(string[] inputLines)
    {
        var currentSum = 0;

        IEnumerable<int> prevSymbolPositions = new List<int>();
        var currentSymbolPositions = GetSymbolPositions(inputLines[0]);
        var nextSymbolPositions = GetSymbolPositions(inputLines[1]);
        var nextIndex = 1;

        for (int index = 0; index < inputLines.Length; index++)
        {
            var line = inputLines[index];

            var currentNumbers = _numberRegex
                .Matches(line)
                .Select(match => new Number
                {
                    IndexStart = match.Index + 1,
                    IndexEnd = match.Index + match.Length,
                    Value = int.Parse(match.Value)
                });

            nextIndex = nextIndex == (inputLines.Length - 1)
                ? nextIndex
                : nextIndex + 1;

            foreach (var currentNumber in currentNumbers)
            {
                var relevantPrevSymbols = prevSymbolPositions
                    .Where(s => s >= currentNumber.IndexStart -1 && s <= currentNumber.IndexEnd + 1);

                var relevantCurrentSymbols = currentSymbolPositions
                    .Where(s => s >= currentNumber.IndexStart - 1 && s <= currentNumber.IndexEnd + 1);
                
                var relevantNextSymbols = nextSymbolPositions
                    .Where(s => s >= currentNumber.IndexStart - 1 && s <= currentNumber.IndexEnd + 1);

                if(relevantPrevSymbols.Any() || relevantCurrentSymbols.Any() || relevantNextSymbols.Any())
                {
                    Console.WriteLine($"Summed {currentNumber.Value}");
                    currentSum += currentNumber.Value;
                }
            }

            prevSymbolPositions = currentSymbolPositions.ToList();
            currentSymbolPositions = nextSymbolPositions.ToList();
            nextSymbolPositions = GetSymbolPositions(inputLines[nextIndex]);
        }

        return currentSum;
    }

    private static IEnumerable<int> GetSymbolPositions(string input)
    {
        return _symbolRegex
            .Matches(input)
            .Select(match => match.Index +1);
    }

    private class Number
    {
        public int IndexStart { get; set; }
        public int IndexEnd { get; set; }
        public int Value { get; set; }
    }
}

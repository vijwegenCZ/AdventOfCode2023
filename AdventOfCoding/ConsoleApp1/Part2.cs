using System.Text.RegularExpressions;

namespace Day1;

internal static class Part2
{
    private static readonly Regex _digitRegex = new(@"(?<=(one|two|three|four|five|six|seven|eight|nine|[0-9]))");

    public static int GetSum(IEnumerable<string> lines)
    {
        return lines
            .Select((line =>
            {
                var matches = _digitRegex
                    .Matches(line);

                var nums = matches
                    .Select(match => ParseMatch(match))
                    .Where(num => num != null)
                    .Cast<int>()
                    .ToList();

                return GetTwoDigitNumber(nums);
            }))
            .Sum();
    }

    private static int? ParseMatch(Match match)
    {
        var value = match.Groups[1].Value;

        if(value.Length == 1)
        {
            return int.Parse(value);
        }

        var digit = value switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            "zero" => 0,
            _ => null as int?
        };

        return digit;
    }

    private static int GetTwoDigitNumber(IReadOnlyList<int> nums)
    {
        var firstDigit = nums.FirstOrDefault();
        var lastDigit = nums.LastOrDefault();

        return int.Parse($"{firstDigit}{lastDigit}");
    }
}

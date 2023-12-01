namespace Day1;

internal static class Part1
{
    private static int? GetNumbers(char character)
    {
        if (int.TryParse(new[] { character }, out var num))
        {
            return num;
        }
        else
        {
            return null as int?;
        }
    }

    private static int GetTwoDigitNumber(IEnumerable<int?> nums)
    {
        var firstDigit = nums.FirstOrDefault() ?? 0;
        var lastDigit = nums.LastOrDefault() ?? 0;

        return int.Parse($"{firstDigit}{lastDigit}");
    }

    public static int GetSum(IEnumerable<string> lines)
    {
        return lines
            .Select((line =>
            {
                var nums = line
                    .Select(GetNumbers)
                    .Where(num => num != null);

                return GetTwoDigitNumber(nums);
            }))
            .Sum();
    }
}

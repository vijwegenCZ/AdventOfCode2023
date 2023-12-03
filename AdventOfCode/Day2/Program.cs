using Day2;

var games = File.ReadAllLines("Input.txt");

Console.WriteLine($"Part1: {Part1.GetSumOfIds(games)}");
Console.WriteLine($"Part2: {Part2.GetSumOfPows(games)}");

using Template;

var input = File.ReadAllLines("Input.txt");

Console.WriteLine($"Part1: {Part1.GetPartNumberSum(input)}");
Console.WriteLine($"Part2: {Part2.DoStuff(input)}");
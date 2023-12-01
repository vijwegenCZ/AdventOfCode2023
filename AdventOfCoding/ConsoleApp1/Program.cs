
using System.Runtime.CompilerServices;

using Day1;

var fileLines = File.ReadAllLines("input.txt");

var sumPart1 = Part1.GetSum(fileLines);

Console.WriteLine($"Sum Part1: {sumPart1}");

var sumPart2 = Part2.GetSum(fileLines);

Console.WriteLine($"Sum Part2: {sumPart2}");
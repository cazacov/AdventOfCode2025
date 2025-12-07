using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography.X509Certificates;

namespace Day06
{
    //  Advent of Code 2025, day 6: https://adventofcode.com/2025/day/6
    internal class Program
    {
        public record Problem(char Operation, List<string> Values);

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 6");
            var input = ReadInput("input.txt");
            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<Problem> problems)
        {

            var result = problems.Sum(problem =>
                {
                    var numbers = problem.Values.Select(x => Int64.Parse(x));
                    if (problem.Operation == '+')
                    {
                        return numbers.Sum();
                    }
                    else
                    {
                        return numbers.Aggregate(1, (long x, long y) => x * y);
                    }
                }
            );
            Console.WriteLine($"Puzzle 1: {result}");
        }

        private static void Puzzle2(List<Problem> problems)
        {

            var result = problems.Sum(problem =>
                {
                    var numbers = new List<long>();
                    for (int i = 0; i < problem.Values[0].Length; i++)
                    {
                        var n = 0;
                        for (var j = 0; j < problem.Values.Count; j++)
                        {
                            if (problem.Values[j][i] != ' ')
                            {
                                n *= 10;
                                n += (int)(problem.Values[j][i]) - (int)'0';
                            }
                        }
                        numbers.Add(n);
                    }
                    if (problem.Operation == '+')
                    {
                        return numbers.Sum();
                    }
                    else
                    {
                        return numbers.Aggregate(1, (long x, long y) => x * y);
                    }
                }
            );
            Console.WriteLine($"Puzzle 2: {result}");
        }

        private static List<Problem> ReadInput(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            var result = new List<Problem>();
            var n = lines.Length - 1;
            var opLine = lines[n];
            var index = 0;
            do
            {
                var next = opLine[1..].IndexOfAny("+*".ToCharArray(), index);
                if (next == -1)
                {
                    next = opLine.Length;
                }

                var values = new List<string>();
                for (var i = 0; i < n; i++)
                {
                    values.Add(lines[i].Substring(index, next - index));
                }
                result.Add(new Problem(opLine[index], values));
                index = next + 1;
            } while (index < opLine.Length);
            return result;
        }
    }
}

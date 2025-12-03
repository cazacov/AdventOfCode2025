namespace Day03
{
    //  Advent of Code 2025, day 3: https://adventofcode.com/2025/day/3
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 3");
            var input = ReadInput("input.txt");
            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(string[] input)
        {
            long result=0;
            foreach (var battery in input)
            {
                var jo = MaxJoltage(battery);
                result += jo;
            }
            Console.WriteLine($"Puzzle 1 {result}");
        }

        private static void Puzzle2(string[] input)
        {
            long result = 0;
            foreach (var battery in input)
            {
                var jo = MaxJoltage2(battery);
                result += jo;
            }
            Console.WriteLine($"Puzzle 2 {result}");
        }

        private static long MaxJoltage(string battery)
        {
            for (var i = 9; i >= 0; i--)
            {
                var c = (char)(48 + i);
                var bat = battery[..^1];
                var pos = bat.IndexOf(c);
                if (pos > -1)
                {
                    var b2 = battery[(pos+1)..];
                    for (var j = 9; j >= 0; j--)
                    {
                        var cc = (char)(48 + j);
                        var pos2 = b2.IndexOf(cc);
                        if (pos2 > -1)
                        {
                            return i * 10 + j;
                        }
                    }
                }
            }
            return 0;
        }

        private static long MaxJoltage2(string battery)
        {
            long result = 0;
            var left = 0;
            for (var d = 11; d >= 0; d--)
            {
                var right = battery.Length - d;
                var bat = battery.Substring(left, right - left);
                for (var i = 9; i >= 0; i--)
                {
                    var c = (char)(48 + i);
                    var pos = bat.IndexOf(c);
                    if (pos > -1)
                    {
                        result = result * 10 + i;
                        left = pos + 1 + left;
                        break;
                    }
                }
            }
            return result;
        }

        private static string[] ReadInput(string fileName)
        {
            return File.ReadAllLines(fileName);
        }
    }
}

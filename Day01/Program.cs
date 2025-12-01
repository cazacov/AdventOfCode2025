

namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 1");
            var input = ReadInput("input.txt");
            Puzzle1(input);
            //Puzzle2(input);
        }

        private static void Puzzle1(int[] input)
        {
            Console.Write("Puzzle 1: ");
            var pos = 50;
            var count = 0;
            foreach (var distance in input)
            {
                pos += distance;
                pos = (pos + 100) % 100;
                if (pos == 0)
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }


        private static int[] ReadInput(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            return lines.Select(x =>
            {
                var n = int.Parse(x.Substring(1));
                return x[0] == 'R' ? n : -n;
            }).ToArray();
        }
    }
}

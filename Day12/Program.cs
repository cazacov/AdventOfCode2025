using System.Reflection.Metadata.Ecma335;

namespace Day12
{
    //  Advent of Code 2025, day 12: https://adventofcode.com/2025/day/12
    internal class Program
    {
        public record Region(int Width, int Height, List<int> Counters);
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 12");
            var (presents, regions) = ReadInput("input.txt");
            Puzzle1(presents, regions);
        }

        private static void Puzzle1(List<bool[,]> presents, List<Region> regions)
        {
            Console.WriteLine("Puzzle 1");
        }

        private static (List<bool[,]>, List<Region>) ReadInput(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            var presents = new List<bool[,]>();
            for (int i = 0; i < 6; i++)
            {
                var pLines = lines.Skip(i * 5 + 1).Take(3).ToList();
                var present = new bool[3, 3];
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        present[y,x] = pLines[y][x] == '#';
                    }
                }
                presents.Add(present);
            }

            var regions = new List<Region>();
            for (int y = 30; y < lines.Length; y++)
            {
                var line = lines[y];
                var p1 = line.IndexOf('x');
                var width = Int32.Parse(line.Substring(0, p1));
                var p2 = line.IndexOf(':');
                var height = Int32.Parse(line.Substring(p1 + 1, p2 - p1 - 1));
                var p3 = line.IndexOf(" ");
                var countStr = line.Substring(p3 + 1);
                var counters = countStr.Split(" ").Select(x => Int32.Parse(x)).ToList();
                regions.Add(new Region(width, height, counters));
            }
            return (presents, regions);
        }


    }
}

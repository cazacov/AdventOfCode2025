using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Xml;

namespace Day08
{
    //  Advent of Code 2025, day 8: https://adventofcode.com/2025/day/8
    internal class Program
    {
        [DebuggerDisplay("{X} {Y} {Z} - {Network}")]
        public class Pos(long x, long y, long z)
        {
            public long X = x; public long Y = y; public long Z = z;
            public int Network = 0;
        }

        [DebuggerDisplay("{Pos1.X},{Pos1.Y},{Pos1.Z} - {Pos2.X},{Pos2.Y},{Pos2.Z} - {Distance}")]
        public class Pair(Pos pos1, Pos pos2)
        {
            public Pos Pos1 = pos1;
            public Pos Pos2 = pos2;
            public long Distance = 0;
            public int network = 0;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 8");
            var input = ReadInput("input.txt");
            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<Pos> input)
        {
            var pairs = new List<Pair>();
            for (int i = 0; i < input.Count - 1; i++)
            {
                for (int j = i + 1; j < input.Count; j++)
                {
                    var pair = new Pair(input[i], input[j]);
                    pair.Distance = (input[i].X - input[j].X)* (input[i].X - input[j].X)
                        + (input[i].Y - input[j].Y) * (input[i].Y - input[j].Y)
                        + (input[i].Z - input[j].Z) * (input[i].Z - input[j].Z);
                    pairs.Add(pair);
                }
            }
            pairs = pairs.OrderBy(p => p.Distance).ToList();
            for (int i = 0; i < input.Count; i++)
            {
                input[i].Network = i + 1;
            }


            var nConnections = 1000;
            var network = 0;
            var connections = 0;
            var minDistance = 0;
            var index = 0;

            while(index  < pairs.Count && connections  < nConnections)
            {
                var pair = pairs[index];
                Console.Write($"Pair {pair.Pos1.X},{pair.Pos1.Y},{pair.Pos1.Z} ({pair.Pos1.Network}) -- {pair.Pos2.X},{pair.Pos2.Y},{pair.Pos2.Z} ({pair.Pos2.Network})");

                if (pair.Pos1.Network == pair.Pos2.Network)
                {
                    Console.WriteLine(" SKIP");
                    connections++;
                }
                else
                {
                    var toDelete = Math.Max(pair.Pos1.Network, pair.Pos2.Network);
                    var keep = Math.Min(pair.Pos1.Network, pair.Pos2.Network);
                    input.Where(x => x.Network == toDelete).ToList().ForEach(x => x.Network = keep);
                    connections++;
                    Console.WriteLine($" Wired {toDelete} and {keep}, used wire N {connections}");
                }
                index++;
            }

            var nets = Enumerable.Range(1, input.Count).Select(n => new
            {
                Network = n,
                Length = input.Count(pos => pos.Network == n)
            }).ToList();
            nets = nets.OrderByDescending(n => n.Length).ToList();
            var result = nets.Take(3).Select(n => n.Length).Aggregate(1L, (long p, int q) => p * q);
            Console.WriteLine($"Puzzle 1: {result}");
        }

        private static void Puzzle2(List<Pos> input)
        {
            var pairs = new List<Pair>();
            for (int i = 0; i < input.Count - 1; i++)
            {
                for (int j = i + 1; j < input.Count; j++)
                {
                    var pair = new Pair(input[i], input[j]);
                    pair.Distance = (input[i].X - input[j].X) * (input[i].X - input[j].X)
                        + (input[i].Y - input[j].Y) * (input[i].Y - input[j].Y)
                        + (input[i].Z - input[j].Z) * (input[i].Z - input[j].Z);
                    pairs.Add(pair);
                }
            }
            pairs = pairs.OrderBy(p => p.Distance).ToList();
            for (int i = 0; i < input.Count; i++)
            {
                input[i].Network = i + 1;
            }


            var network = 0;
            var connections = 0;
            var minDistance = 0;
            var index = 0;

            while (index < pairs.Count)
            {
                var pair = pairs[index];
                Console.Write($"Pair {pair.Pos1.X},{pair.Pos1.Y},{pair.Pos1.Z} ({pair.Pos1.Network}) -- {pair.Pos2.X},{pair.Pos2.Y},{pair.Pos2.Z} ({pair.Pos2.Network})");

                if (pair.Pos1.Network == pair.Pos2.Network)
                {
                    Console.WriteLine(" SKIP");
                }
                else
                {
                    var toDelete = Math.Max(pair.Pos1.Network, pair.Pos2.Network);
                    var keep = Math.Min(pair.Pos1.Network, pair.Pos2.Network);
                    input.Where(x => x.Network == toDelete).ToList().ForEach(x => x.Network = keep);
                    Console.WriteLine($" Wired {toDelete} and {keep}, used wire N {connections}");

                    if (input.All(x => x.Network == keep))
                    {
                        Console.WriteLine($"Puzzle 2: {pair.Pos1.X * pair.Pos2.X}");
                        break;
                    }
                }
                index++;
            }
        }

        private static List<Pos> ReadInput(string fileName)
        {
            var result = new List<Pos>();
            var lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                result.Add(new Pos(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
                    
            }
            return result;
        }
    }
}

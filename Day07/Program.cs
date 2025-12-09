namespace Day07
{
    //  Advent of Code 2025, day 7: https://adventofcode.com/2025/day/7
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 7");
            var (start, splitters) = ReadInput("input.txt");
            Puzzle1(start, splitters);
            Puzzle2(start, splitters);
        }

        private static void Puzzle1(int start, List<List<int>> splitters)
        {
            var beams = new HashSet<int>();
            beams.Add(start);
            var splits = 0;
            for (var i = 0; i < splitters.Count; i++)
            {
                var nextBeams = new HashSet<int>();
                foreach (var beam in beams)
                {
                    if (!splitters[i].Contains(beam))
                    {
                        nextBeams.Add(beam);
                    }
                    else
                    {
                        splits++;
                        nextBeams.Add(beam-1);
                        nextBeams.Add(beam + 1);
                    }
                }
                beams = nextBeams;
            }
            Console.WriteLine($"Puzzle 1: {splits}");
        }

        private class Beam(int pos, long cardinality)
        {
            public readonly int Pos = pos;
            public long Cardinality = cardinality;
        };

        private static void AddBeam(List<Beam> existingBeams, int pos, long cardinality)
        {
            var nb = existingBeams.FirstOrDefault(b => b.Pos == pos);
            if (nb == null)
            {
                existingBeams.Add(new Beam(pos, cardinality));
            }
            else
            {
                nb.Cardinality += cardinality;
            }
        }

        private static void Puzzle2(int start, List<List<int>> splitters)
        {
            var beams = new List<Beam>();
            beams.Add(new Beam(start, 1));
            for (var i = 0; i < splitters.Count; i++)
            {
                var nextBeams = new List<Beam>();
                for (var b = 0; b < beams.Count; b++)
                {
                    var beam = beams[b];
                    if (!splitters[i].Contains(beam.Pos))
                    {
                        AddBeam(nextBeams, beam.Pos, beam.Cardinality);
                    }
                    else
                    {
                        AddBeam(nextBeams, beam.Pos - 1, beam.Cardinality);
                        AddBeam(nextBeams, beam.Pos + 1, beam.Cardinality);
                    }
                }
                beams = nextBeams;
            }
            Console.WriteLine($"Puzzle 2: {beams.Sum(x => x.Cardinality)}");
        }

        private static (int, List<List<int>>) ReadInput(string fileName)
        {
            var input = File.ReadAllLines(fileName);
            var start = input[0].IndexOf('S');
            var splitters = new List<List<int>>();
            for (var i = 1; i < input.Length; i++)
            {
                var s = new List<int>();
                var pos = input[i].IndexOf('^');
                while (pos >= 0)
                {
                    s.Add(pos);
                    pos = input[i].IndexOf('^', pos + 1);
                }
                splitters.Add(s);
            }
            return (start, splitters);
        }
    }
}

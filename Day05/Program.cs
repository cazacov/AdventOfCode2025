namespace Day05
{
    //  Advent of Code 2025, day 5: https://adventofcode.com/2025/day/5
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 5");
            var(fresh, all) = ReadInput("input.txt");
            Puzzle1(fresh, all);
            Puzzle2(fresh);
        }

        private static void Puzzle1(List<Range> ranges, List<long> all)
        {
            Console.WriteLine(all.Count(x => ranges.Exists(r => x >= r.from && x <= r.to)));
        }

        private static void Puzzle2(List<Range> ranges)
        {
            var fresh = new List<Range>();

            foreach (var range in ranges)
            {
                var newItem = range;
                var overlap = true;
                while (overlap)
                {
                    overlap = false;
                    for (var i = 0; i < fresh.Count; i++)
                    {
                        var fr = fresh[i].from;
                        var to = fresh[i].to;
                        if (newItem.from <= fr && newItem.to >= fr
                            || newItem.from <= to && newItem.from >= fr)
                        {
                            fresh.RemoveAt(i);
                            newItem = new Range(Math.Min(fr, newItem.from), Math.Max(to, newItem.to));
                            overlap = true;
                            break;
                        }
                    }
                }
                fresh.Add(newItem);
            }

            Console.WriteLine(fresh.Sum(r => r.to - r.from + 1));
        }

        private static (List<Range>, List<long>) ReadInput(string fileName)
        {
            var fresh = new List<Range>();
            var all = new List<long>();
            var lines = File.ReadAllLines(fileName);
            int i = 0;
            while (i < lines.Length && lines[i] != "")
            {
                var parts = lines[i].Split('-');
                fresh.Add(new Range(long.Parse(parts[0]), long.Parse(parts[1])));
                i++;
            }
            i++;
            while (i < lines.Length)
            {
                all.Add(long.Parse(lines[i]));
                i++;
            }
            return (fresh, all);
        }
    }
}

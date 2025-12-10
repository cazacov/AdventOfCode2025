using System.Numerics;

namespace Day10
{
    //  Advent of Code 2025, day 10: https://adventofcode.com/2025/day/10
    internal class Program
    {
        public class Machine
        {
            public int Target;
            public List<int> Buttons = new List<int>();
            public List<int> Joltages = new List<int>();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 10");
            var input = ReadInput("input.txt");
            Puzzle1(input);
        }

        private static void Puzzle1(List<Machine> machines)
        {
            Console.WriteLine($"Puzzle 1: {machines.Sum(m => ButtonClicks(m))}");
        }

        private static int ButtonClicks(Machine machine)
        {
            var minClciks = Int32.MaxValue;

            var combinations = 1 << machine.Buttons.Count;
            for (int n = 0; n < combinations; n++)
            {
                var clicks = 0;
                var mask = 1;
                var state = 0;
                for (int j = 0; j < machine.Buttons.Count; j++)
                {
                    if ((n & (1 << j)) != 0)
                    {
                        state ^= machine.Buttons[j];
                        clicks++;
                    }
                    if (state == machine.Target)
                    {
                        break;
                    }
                }

                if (state == machine.Target && clicks < minClciks)
                {
                    minClciks = clicks;
                }
            }
            return minClciks;
        }

        private static List<Machine> ReadInput(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            var result = new List<Machine>();

            foreach (var line in lines)
            {
                var machine = new Machine();
                var parts = line.Split("[]".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                machine.Target = ToTarget(parts[0]);
                var str = parts[1].Trim();
                parts = str.Split("{}".ToCharArray());
                var buttons = parts[0].Trim();
                var joltages = parts[1].Trim();
                foreach (var button in buttons.Split(' '))
                {
                    var xorMask = 0;
                    var s = button[1..^1];
                    foreach (var bs in s.Split(','))
                    {
                        var bit = Int32.Parse(bs);
                        xorMask |= 1 << bit;
                    }
                    machine.Buttons.Add(xorMask);
                }
                result.Add(machine);
            }
            return result;
        }

        private static int ToTarget(string input)
        {
            var result = 0;
            foreach (var t in input.Reverse())
            {
                result <<= 1;
                result |= (t == '#') ? 1 : 0;
            }
            return result;
        }
    }
}

using System.Numerics;
using System.Runtime.CompilerServices;

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
            Puzzle2(input);
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

        private static void Puzzle2(List<Machine> machines)
        {
            Console.WriteLine($"Puzzle 2: {machines.Sum(m => JoltageClicks(m))}");
        }

        private static int JoltageClicks(Machine machine)
        {
            Console.Write(".");
            var count = machine.Joltages.Count;
            var maxButtonClicks = new List<int>();

            foreach (var button in machine.Buttons)
            {
                var m = Int32.MaxValue;
                for (var i = 0; i < count; i++)
                {
                    if ((button & (1 << i)) != 0)
                    {
                        if (m > machine.Joltages[i])
                        {
                            m = machine.Joltages[i];
                        }
                    }
                }
                maxButtonClicks.Add(m);
            }
            var clicks = machine.Joltages.Sum();
            var clickHistory = Enumerable.Range(0, machine.Buttons.Count).Select(_ => 0).ToArray();

            var buttons = new List<int[]>();
            foreach (var button in machine.Buttons)
            {
                var switches = new List<int>();
                for (int j = 0; j < machine.Joltages.Count; j++)
                {
                    if ((button & (1 << j)) != 0)
                    {
                        switches.Add(j);
                    }
                }
                buttons.Add(switches.ToArray());
            }
            FindClicksRec(0, 0, ref clicks, buttons, maxButtonClicks, machine.Joltages.ToArray() /*, clickHistory */);
            return clicks;
        }

        private static void FindClicksRec(int index, int clicksSpent, ref int clicks, List<int[]> machineButtons,
            List<int> maxButtonClicks, int[] joltages//, int[] clickHistory
            )
        {
            var upper = Math.Min(clicks - clicksSpent, maxButtonClicks[index]);
            var n = joltages.Length;
            var buttons = machineButtons[index];

            for (int i = 0; i <= upper; i++)
            {
                bool found = false;
                for (int j = 0; j < buttons.Length; j++)
                {
                    if (joltages[buttons[j]] < i) {
                        return;
                    }
                }

                //                clickHistory[index] = i;

                for (int j = 0; j < buttons.Length; j++)
                {
                    joltages[buttons[j]] -= i;
                }
                if (joltages.All(j => j == 0))
                {
                    var res = clicksSpent + i;
                    found = true;
                    if (res < clicks)
                    {
                        clicks = res;
                        Console.Write($"BEST {clicks}  ");
                    }
                    else
                    {
                        //Console.Write($"FOUND {clicks}  ");
                    }
                    /*
                    for (int k = 0; k <= index; k++)
                    {
                        Console.Write($"{clickHistory[k]} ");
                    }
                    Console.WriteLine();
                    */
                    //clickHistory[index] = 0;
                }

                if (!found && index < machineButtons.Count - 1)
                {
                    FindClicksRec(index + 1, clicksSpent + i, ref clicks, machineButtons, maxButtonClicks, joltages /*, clickHistory */);
                }
                for (int j = 0; j < buttons.Length; j++)
                {
                    joltages[buttons[j]] += i;
                }
                //clickHistory[index] = 0;
            }
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
                        var bit = int.Parse(bs);
                        xorMask |= 1 << bit;
                    }
                    machine.Buttons.Add(xorMask);
                }

                foreach (var joltage in joltages.Split(","))
                {
                    machine.Joltages.Add(int.Parse(joltage));                    
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

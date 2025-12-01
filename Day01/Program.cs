namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 1");
            var input = ReadInput("input.txt");
            Puzzle1(input);
            Puzzle2(input);
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

        private static void Puzzle2(int[] input)
        {
            Console.Write("Puzzle 2: ");
            var pos = 50;
            var count = 0;
            foreach (var distance in input)
            {
                var newPos = pos + distance;
                if (newPos > 0)
                {
                    count += newPos / 100;
                }
                else
                {
                    count += pos > 0 ? 1 : 0;   //starting point was positive, destination is negative -> zero passed at least once
                    count += -newPos / 100;     // repeated zeros
                }
                pos = newPos % 100;
                if (pos < 0)
                {
                    pos += 100;
                }
            }
            Console.WriteLine(count);
        }

        private static int[] ReadInput(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            return lines.Select(x =>
            {
                var n = int.Parse(x[1..]);
                return x[0] == 'R' ? n : -n;
            }).ToArray();
        }
    }
}

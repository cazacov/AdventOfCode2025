namespace Day02
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 2");
            var input = ReadInput("input.txt");
            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<Tuple<long, long>> input)
        {
            Console.Write("Puzzle 1: ");
            long result = 0;
            foreach (var range in input)
            {
                for (var i = range.Item1; i <= range.Item2; i++)
                {
                    var render = i.ToString();
                    if (IsInvalid(render))
                    {
                        result += i;
                    }
                }
            }
            Console.WriteLine(result);
        }

        private static bool IsInvalid(string render)
        {
            if (render.Length % 2 != 0)
            {
                return false;
            }
            var halfLength = render.Length / 2;
            return render[..halfLength] == render[halfLength..];
        }

        private static void Puzzle2(List<Tuple<long, long>> input)
        {
            Console.Write("Puzzle 2: ");
            long result = 0;
            foreach (var range in input)
            {
                for (var i = range.Item1; i <= range.Item2; i++)
                {
                    var render = i.ToString();
                    if (IsInvalid2(render))
                    {
                        result += i;
                    }
                }
            }
            Console.WriteLine(result);
        }
        
        private static bool IsInvalid2(string render)
        {
            var l = render.Length;
            if (l < 2)
            {
                return false;
            }
            var c = render[0];
            if (render.ToCharArray().All(x => x == c))
            {
                return true;
            }

            for (var partLength = 2; partLength <= l / 2; partLength++)
            {
                if (l % partLength != 0)
                {
                    continue;
                }

                var partsCount = l / partLength;
                for (var index = 0; index < partLength; index++)
                {
                    var cc = render[index];
                    // compare first part char with corresponding chars in other parts
                    for (var partNo = 1; partNo < partsCount; partNo++)
                    {
                        if (render[partNo * partLength + index] != cc)
                        {
                            goto checkNextPartLength;
                        }
                    }
                }
                return true;
checkNextPartLength:; // GOTO (when used carefully) can make sense to escape deeply nested loops 
            }
            return false;
        }

        private static List<Tuple<long, long>> ReadInput(string fileName)
        {
            var result = new List<Tuple<long, long>>();
            var lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
                var fragments = line.Split(",");
                foreach (var fragment in fragments)
                {
                    var parts = fragment.Split("-");
                    result.Add(new Tuple<long, long>(long.Parse(parts[0]), long.Parse(parts[1])));
                }
            }
            return result;
        }
    }
}

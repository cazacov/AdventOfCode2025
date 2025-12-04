namespace Day04
{
    //  Advent of Code 2025, day 4: https://adventofcode.com/2025/day/4
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 4");
            var input = ReadInput("input.txt");
            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(bool[,] input)
        {
            var maxy = input.GetLength(0);
            var maxx = input.GetLength(0);

            var accessible = 0;
            for (var y = 0; y < maxy; y++)
            {
                for (var x = 0; x < maxx; x++)
                {
                    if (input[y,x])
                    {
                        if(CountAdjacentRolls(input, x, y, maxx, maxy) < 5)
                        {
                            accessible++;
                        }
                    }
                }
            }
            Console.WriteLine($"Puzzle 1: {accessible}");
        }

        private static void Puzzle2(bool[,] input)
        {
            var maxy = input.GetLength(0);
            var maxx = input.GetLength(1);

            var removed = new List<Tuple<int, int>>();
            var wasRemoved = true;
            while (wasRemoved)
            {
                wasRemoved = false;
                for (var y = 0; y < maxy; y++)
                {
                    for (var x = 0; x < maxx; x++)
                    {
                        if (input[y, x])
                        {
                            if (CountAdjacentRolls(input, x, y, maxx, maxy) < 5)
                            {
                                input[y, x] = false;
                                wasRemoved = true;
                                removed.Add(new Tuple<int, int>(x, y));
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"Puzzle 2: {removed.Count}");
            DisplayMap(input, removed);
        }

        private static void DisplayMap(bool[,] input, List<Tuple<int, int>> removed)
        {
            var maxy = input.GetLength(0);
            var maxx = input.GetLength(1);

            var c = Console.ForegroundColor;

            for (var y = 0; y < maxy; y++)
            {
                for (var x = 0; x < maxx; x++)
                {
                    if (input[y, x])
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write('@');
                    } else if (removed.Any(item => item.Item1 == x && item.Item2 == y))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write('@');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }
            Console.ForegroundColor = c;
        }

        private static int CountAdjacentRolls(bool[,] input, int x, int y, int maxx, int maxy)
        {
            var rolls = 0;
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    var xx = x + i;
                    var yy = y + j;
                    if (xx < 0 || xx >= maxx) continue;
                    if (yy < 0 || yy >= maxy) continue;
                    if (input[yy, xx])
                    {
                        rolls++;
                    }
                }
            }
            return rolls;
        }


        private static bool[,] ReadInput(string fileName)
        {
            var lines =  File.ReadAllLines(fileName);

            var maxx = lines[0].Length;
            var maxy = lines.Length;
            var map = new bool[maxy, maxx];
            for (var y = 0; y < maxy; y++)
            {
                for (var x = 0; x < maxx; x++)
                {
                    if (lines[y][x] == '@')
                    {
                        map[y, x] = true;
                    }
                }
            }
            return map;
        }
    }
}

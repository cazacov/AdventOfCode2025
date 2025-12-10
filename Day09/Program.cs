using System.ComponentModel.Design;

namespace Day09
{
    //  Advent of Code 2025, day 9: https://adventofcode.com/2025/day/9
    internal class Program
    {
        record Point(int X, int Y);

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 9");
            var input = ReadInput("input.txt");
            Puzzle1(input);
            Puzzle2(input);
        }

        private static void Puzzle1(List<Point> input)
        {
            var s = 0L;
            for (var i = 0; i < input.Count - 1; i++)
            {
                for (var j = i + 1; j < input.Count; j++)
                {
                    var p1 = input[i];
                    var p2 = input[j];
                    var dx = Math.Abs(p1.X - p2.X) + 1;
                    var dy = Math.Abs(p1.Y - p2.Y) + 1;
                    var sq = (long)dx * (long)dy;
                    if (sq > s)
                    {
                        s = sq;
                    }
                }
            }
            Console.WriteLine($"Puzzle 1: {s}");
        }

        private static void Puzzle2(List<Point> input)
        {
            input.Add(input.First()); // make loop

            var (reds, mapX, mapY) = Squeeze(input);

            var top = reds.Max(p => p.Y);
            var right = reds.Max(p => p.X);
            
            // draw border
            var colored = new bool[right + 1, top + 1];
            for (int i = 0; i < input.Count - 1; i++)
            {
                var p1 = reds[i];
                var p2 = reds[i + 1];
                var dx = p2.X - p1.X;
                var dy = p2.Y - p1.Y;
                if (dx != 0)
                {
                    for (var x = p1.X; x != p2.X; x += Math.Sign(dx))
                    {
                        colored[x, p1.Y] = true;
                    }
                }
                else
                {
                    for (var y = p1.Y; y != p2.Y; y += Math.Sign(dy))
                    {
                        colored[p1.X, y] = true;
                    }
                }
            }

            // Locate point inside
            var topPoints = reds.Where(p => p.Y == top).OrderBy(p => p.X).ToList();
            var inside = new Point((topPoints[0].X + topPoints[1].X) / 2, top - 1);

            // Paint green
            var toPaint = new HashSet<Point>()
            {
                inside
            };

            while (toPaint.Any())
            {
                var p = toPaint.First();
                colored[p.X, p.Y] = true;
                toPaint.Remove(p);
                for (int x = p.X - 1; x <= p.X + 1; x++)
                {
                    for (int y = p.Y - 1; y <= p.Y + 1; y++)
                    {
                        if (!colored[x, y])
                        {
                            toPaint.Add(new Point(x, y));
                        }
                    }
                }
            }

            var s = 0L;
            for (var i = 0; i < input.Count - 2; i++)
            {
                for (var j = i + 1; j < input.Count; j++)
                {
                    var p1 = reds[i];
                    var p2 = reds[j];

                    if (!(AllColored(p1.X, p1.Y, p2.X, p1.Y, colored)
                          && AllColored(p2.X, p1.Y, p2.X, p2.Y, colored)
                          && AllColored(p2.X, p2.Y, p1.X, p2.Y, colored)
                          && AllColored(p1.X, p2.Y, p1.X, p1.Y, colored)
                        ))
                    {
                        continue;
                    }

                    long x1 = mapX[p1.X / 2];
                    long x2 = mapX[p2.X / 2];
                    long y1 = mapY[p1.Y / 2];
                    long y2 = mapY[p2.Y / 2];
                    var dx = Math.Abs(x1 - x2) + 1;
                    var dy = Math.Abs(y1 - y2) + 1;
                    var sq = (long)dx * (long)dy;
                    if (sq > s)
                    {
                        s = sq;
                    }
                }
            }
            Console.WriteLine($"Puzzle 2: {s}");
        }

        private static bool AllColored(int x1, int y1, int x2, int y2, bool[,] colored)
        {
            if (x1 != x2)
            {
                var dx = Math.Sign(x2 - x1);
                for (var x = x1; x != x2; x += dx)
                {
                    if (!colored[x, y1])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                var dy = Math.Sign(y2 - y1);
                for (var y = y1; y != y2; y += dy)
                {
                    if (!colored[x1, y])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private static (List<Point> red, List<int> mapX, List<int> mapY) Squeeze(List<Point> input)
        {
            var xx = input.Select(p => p.X).OrderBy(x => x).Distinct().ToList();
            var yy = input.Select(p => p.Y).OrderBy(x => x).Distinct().ToList();

            var result = new List<Point>();
            foreach (var point in input)
            {
                var xPos = xx.IndexOf(point.X);
                var yPos =  yy.IndexOf(point.Y);
                result.Add(new Point(xPos * 2, yPos * 2));
            }
            return (result, xx, yy);
        }


        private static List<Point> ReadInput(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            return lines.Select(l =>
            {
                var parts = l.Split(",");
                return new Point(Int32.Parse(parts[0]), Int32.Parse(parts[1]));
            }).ToList();
        }
    }
}

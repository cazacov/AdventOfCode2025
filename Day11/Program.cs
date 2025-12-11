namespace Day11
{
    //  Advent of Code 2025, day 11: https://adventofcode.com/2025/day/11
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 11");
            var input = ReadInput("input.txt");
        }

        private static string[] ReadInput(string fileName)
        {
            return File.ReadAllLines(fileName);
        }
    }
}

using static Day11.Program;

namespace Day11
{
    //  Advent of Code 2025, day 11: https://adventofcode.com/2025/day/11
    internal class Program
    {
        public class Device
        {
            public string Name;
            public List<string> Outs = new();
            public long PathCount = 0;
            public int Distance = int.MaxValue;
            public int UnprocessedInputs = 0;

            public override bool Equals(object? obj)
            {
                return this.Equals(obj as Device);
            }

            public bool Equals(Device other)
            {
                if (other == null) return false;
                return this.Name == other.Name;
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2025, Day 11");
            var devices = ReadInput("input.txt");
            if (!devices.ContainsKey("out"))
            {
                devices["out"] = new Device() { Name = "out" };
            }
            Puzzle1(devices);
            Puzzle2(devices);
        }

        private static void Puzzle1(Dictionary<string, Device> devices)
        {
            Console.WriteLine(PathCount("you", "out", devices));

        }

        private static void Puzzle2(Dictionary<string, Device> devices)
        {
            // svr -> fft ->  dac -> out
            var n1 = PathCount("svr", "fft", devices) * PathCount("fft", "dac", devices) * PathCount("dac", "out", devices);

            // svr -> dac ->  fft -> out
            var n2 = PathCount("svr", "dac", devices) * PathCount("dac", "fft", devices) * PathCount("fft", "out", devices);
            Console.WriteLine(n1 + n2);
        }

        private static long PathCount(string start, string target, Dictionary<string, Device> devices)
        {
            if (!devices.ContainsKey(target))
            {
                devices[target] = new Device() { Name = target};
            }
            var mydevices = CleanList(devices, start, target);
            foreach (var device in mydevices.Values)
            {
                device.UnprocessedInputs = mydevices.Values.Count(d => d.Outs.Contains(device.Name));
            }
            foreach (var device in mydevices.Values)
            {
                device.PathCount = 0;
            }
            mydevices[start].PathCount = 1;
            var todo = new HashSet<Device>() { mydevices[start] };
            while (todo.Any())
            {
                var item = todo.OrderBy(x => x.UnprocessedInputs).First();
                todo.Remove(item);
                foreach (var outKey in item.Outs)
                {
                    var outItem = mydevices[outKey];
                    outItem.PathCount += item.PathCount;
                    outItem.UnprocessedInputs -= 1;
                    todo.Add(outItem);
                }
            }

            if (!mydevices.ContainsKey(target))
            {
                return 0;
            }
            var result = mydevices[target].PathCount;
            return result;
        }

        private static Dictionary<string, Device> CleanList(Dictionary<string, Device> devices, string start, string target)
        {
           
            foreach (var devicesValue in devices.Values)
            {
                devicesValue.Distance = Int32.MaxValue;
            }
            var todo = new HashSet<Device>() { devices[start] };
            devices[start].Distance = 0;
            while (todo.Any())
            {
                var item = todo.OrderBy(x => x.Distance).First();
                todo.Remove(item);
                foreach (var outKey in item.Outs)
                {
                    var outItem = devices[outKey];
                    var d = item.Distance + 1;
                    if (outItem.Distance > d)
                    {
                        outItem.Distance = d;
                    }
                    todo.Add(outItem);
                }
            }
            var result = new Dictionary<string, Device>();
            foreach (var device in devices.Where(d => d.Value.Distance < Int32.MaxValue))
            {
                result[device.Key] = device.Value;
            }
            return result;

        }

        private static Dictionary<string, Device> ReadInput(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            var devices = new List<Device>();
            foreach (var line in lines)
            {
                var i = line.IndexOf(':');
                var device = new Device()
                {
                    Name = line.Substring(0, i)
                };
                var outs = line.Substring(i+2).Split(' ');
                device.Outs.AddRange(outs);
                devices.Add(device);
            }
            return devices.ToDictionary(device => device.Name);
        }
    }
}

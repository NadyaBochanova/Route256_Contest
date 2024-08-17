using System.Diagnostics;

namespace Route256_11082024
{
    internal class Task2
    {
        public void Run(ResourceReader reader)
        {
            long time = 0;
            Int64 memory = 0;
            var sw = new Stopwatch();

            while (reader.TryGetResource(nameof(Task3), out string input, out string output))
            {
                long before = GC.GetTotalMemory(false);
                sw.Restart();

                var actual = ReadInput(input);

                sw.Stop();
                time = Math.Max(time, sw.ElapsedMilliseconds);

                long after = GC.GetTotalMemory(false);
                var consumedInMegabytes = (after - before) / (1024 * 1024);
                memory = Math.Max(memory, consumedInMegabytes);

                if (!string.Equals(actual, output.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception(nameof(Task3));
                }
            }

            if (memory > 256)
            {
                throw new OutOfMemoryException(nameof(Task3));
            }

            if (time > 2000)
            {
                throw new TimeoutException(nameof(Task3));
            }
        }

        string ReadInput(string input)
        {
            return "";
        }
    }
}

namespace Route256_11082024
{
    internal class Task3
    {
        public void Run(ResourceReader reader)
        {
            while (reader.TryGetResource(nameof(Task1), out string input, out string output))
            {
                var actual = ReadInput(input);

                if (!string.Equals(actual, output.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception(nameof(Task1));
                }
            }
        }

        string ReadInput(string input)
        {
            var inputs = input.Trim().Split("\n");

            var result = new List<string>(inputs.Length);

            int i = 1;
            do
            {
                var count = int.Parse(inputs[i]);
                i++;

                var requests = new List<string>(count);
                for (int j = 0; j < count; j++)
                {
                    requests.Add(inputs[i + j]);
                }

                result.AddRange(GetResult(requests));

                i += count;
            }
            while (i < inputs.Length);

            return string.Join("\n", result);
        }

        List<string> GetResult(List<string> requests)
        {
            var output = new List<string>(requests.Count);

            var pairs = new KeyValuePair<int, string>[requests.Count];

            int sec = 0;
            foreach (var request in requests)
            {
                var strs = request.Split(' ');

                if (request.StartsWith('C'))
                {
                    pairs[sec] = new KeyValuePair<int, string>(Convert.ToInt32(strs.ElementAt(2)), strs.ElementAt(1));
                }
                else
                {
                    var time = Convert.ToInt32(strs.ElementAt(2));
                    var id = Convert.ToInt32(strs.ElementAt(1));

                    bool isFind = false;
                    for (int i = time - 1; i >= 0; i--)
                    {
                        if (pairs[i].Key == id)
                        {
                            for (int j = i + 1; j < time; j++)
                            {
                                if (pairs[j].Value == pairs[i].Value)
                                {
                                    output.Add("404");
                                    isFind = true;
                                    break;
                                }
                            }

                            if (!isFind)
                            {
                                output.Add(pairs[i].Value);
                                isFind = true;
                            }

                            break;
                        }
                    }

                    if (!isFind)
                        output.Add("404");
                }

                sec++;
            }

            return output;
        }
    }
}

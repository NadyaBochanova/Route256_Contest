namespace Route256_11082024
{
    internal class Task3
    {
        private string _separator = string.Empty;
        private const string TaskName = nameof(Task3);

        public void Run(ResourceReader reader)
        {
            while (reader.TryGetResource(TaskName, out string input, out string expectedResult))
            {
                if (string.IsNullOrEmpty(_separator))
                    _separator = input.GetSeparator();

                var actualResult = ReadInput(input);

                if (!string.Equals(actualResult, expectedResult.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception(TaskName);
                }
            }
        }

        string ReadInput(string input)
        {
            var inputs = input.Trim().Split(_separator);

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

            return string.Join(_separator, result);
        }

        private List<string> GetResult(List<string> requests)
        {
            var result = new List<string>(requests.Count);

            var pairs = new KeyValuePair<int, string>[requests.Count];

            int sec = 0;
            foreach (var request in requests)
            {
                var strs = request.Split(' ');

                if (request.StartsWith('C'))
                {
                    var name = strs.ElementAt(1);
                    var id = Convert.ToInt32(strs.ElementAt(2));

                    pairs[sec] = new KeyValuePair<int, string>(id, name);
                }
                else
                {
                    var id = Convert.ToInt32(strs.ElementAt(1));
                    var time = Convert.ToInt32(strs.ElementAt(2));

                    bool isFind = false;
                    var i = time;
                    while (i > 0)
                    {
                        i--;

                        if (pairs[i].Key == id)
                        {
                            isFind = true;

                            var j = i + 1;
                            while (j < time)
                            {
                                if (pairs[j++].Value == pairs[i].Value)
                                {
                                    isFind = false;
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    result.Add(isFind ? pairs[i].Value : "404");
                }

                sec++;
            }

            return result;
        }
    }
}

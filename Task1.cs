namespace Route256_11082024
{
    internal class Task1
    {
        private string _separator = string.Empty;
        private const string TaskName = nameof(Task1);

        public void Run(ResourceReader reader)
        {
            while(reader.TryGetResource(TaskName, out string input, out string expectedResult))
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
            int groupsCount = int.Parse(inputs[0]);

            var result = new List<string>(groupsCount);

            int i = 1;
            do
            {
                if (inputs[i].Length == 1)
                    result.Add("0");
                else
                    result.Add(GetBiggestSalary(inputs[i]));
                i++;
            }
            while (i < inputs.Length);

            return string.Join(_separator, result);
        }

        private string GetBiggestSalary(string salary)
        {
            int firstDigit = int.Parse(salary.Substring(0, 1));
            for (int i = 1; i < salary.Length; i++)
            {
                var secondDigit = int.Parse(salary.Substring(i, 1));

                if (firstDigit < secondDigit)
                {
                    return salary.Remove(i - 1, 1);
                }

                firstDigit = secondDigit;
            }
            return salary.Substring(0, salary.Length - 1);
        }
    }
}

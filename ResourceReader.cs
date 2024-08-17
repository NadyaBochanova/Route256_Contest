using System.Reflection;

namespace Route256_11082024
{
    internal class ResourceReader
    {
        private const string AnsverFileEndWith = ".a";

        private readonly Assembly _assembly;

        private readonly Dictionary<string, Queue<string>> _map;

        public ResourceReader()
        {
            _assembly = Assembly.GetExecutingAssembly();
            var lst = _assembly.GetManifestResourceNames().ToList();
            
            _map = new Dictionary<string, Queue<string>>(lst.Count);

            foreach (var name in lst.Where(x => !x.EndsWith(AnsverFileEndWith)))
            {
                var task = name.Split('.').ElementAt(2);

                if(!_map.ContainsKey(task))
                    _map[task] = new Queue<string>(lst.Count);

                _map[task].Enqueue(name);
            }
        }

        public bool TryGetResource(string taskName, out string input, out string output)
        {
            input = string.Empty;
            output = string.Empty;

            if (_map.ContainsKey(taskName) && _map[taskName].Any())
            {
                var inputResource = _map[taskName].Dequeue();
                var outputResource = $"{inputResource}{AnsverFileEndWith}";

                input = Read(inputResource);
                output = Read(outputResource);

                return true;
            }

            return false;
        }

        private string Read(string name)
        {
            using Stream stream = _assembly.GetManifestResourceStream(name);
            using StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TextComposerLib.Text.Tree
{
    // TODO: Complete tis class
    public class TreeTextComposer :
        IReadOnlyDictionary<string, TreeTextComposer>
    {
        private readonly List<TreeTextComposer> _orderedChildNodes 
            = new List<TreeTextComposer>();

        private readonly Dictionary<string, TreeTextComposer> _namedChildNodes
            = new Dictionary<string, TreeTextComposer>();


        public TreeTextComposer ParentNode { get; }

        public bool IsRoot 
            => ParentNode is null;

        public bool IsLeaf 
            => _namedChildNodes.Count == 0;


        public int Count 
            => _namedChildNodes.Count;

        public IEnumerable<string> Keys 
            => _namedChildNodes.Keys;

        public IEnumerable<TreeTextComposer> Values 
            => _namedChildNodes.Values;
        
        public TreeTextComposer this[string key]
        {
            get
            {
                var keysList = 
                    key.Split('.').Select(k => k.Trim()).ToArray();

                var n = key.Length;
                var node = this;
                for (var i = 0; i < n; i++)
                    node = node.GetChild(keysList[i]);

                return node;
            }
        }


        public TreeTextComposer()
        {
            ParentNode = null;
        }

        private TreeTextComposer([NotNull] TreeTextComposer parentNode)
        {
            ParentNode = parentNode;
        }


        public bool ContainsChild(string childName)
        {
            return _namedChildNodes.ContainsKey(childName);
        }

        public bool ContainsKey(string key)
        {
            var keysList = 
                key.Split('.').Select(k => k.Trim()).ToArray();
            
            var n = key.Length;
            var node = this;
            for (var i = 0; i < n - 1; i++)
                if (!node.TryGetChild(keysList[i], out node))
                    return false;

            return node.ContainsChild(keysList[^1]);
        }

        public TreeTextComposer GetChild(string childName)
        {
            return _namedChildNodes[childName];
        }

        public bool TryGetChild(string childName, out TreeTextComposer value)
        {
            return _namedChildNodes.TryGetValue(childName, out value);
        }

        public bool TryGetValue(string key, out TreeTextComposer value)
        {
            var keysList = 
                key.Split('.').Select(k => k.Trim()).ToArray();

            var n = key.Length;
            var node = this;
            for (var i = 0; i < n - 1; i++)
                if (!node.TryGetChild(keysList[i], out node))
                {
                    value = null;
                    return false;
                }

            return node.TryGetChild(keysList[^1], out value);
        }

        public IEnumerator<KeyValuePair<string, TreeTextComposer>> GetEnumerator()
        {
            return _namedChildNodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

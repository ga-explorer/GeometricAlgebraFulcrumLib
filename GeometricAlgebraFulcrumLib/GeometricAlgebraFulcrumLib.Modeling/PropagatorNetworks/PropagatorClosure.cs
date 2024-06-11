using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.PropagatorNetworks
{
    public class PropagatorClosure :
        IPropagatorClosure
    {
        internal static PropagatorClosure CreateFromClientCells(IPropagatorCell cell)
        {
            var closure = new PropagatorClosure(cell.ParentNetwork);

            foreach (var inputCell in cell.ClientCells)
            {
                //if (inputCell.IsEmpty) continue;

                closure._cellValueDictionary.Add(inputCell.Name, inputCell.Value);
            }

            return closure;
        }


        private readonly Dictionary<string, IPropagatorValue> _cellValueDictionary
            = new Dictionary<string, IPropagatorValue>();


        public IPropagatorNetwork ParentNetwork { get; }

        public IEnumerable<IPropagatorCell> Cells 
            => _cellValueDictionary.Keys.Select(k => ParentNetwork[k]);

        public int Count 
            => _cellValueDictionary.Count;

        public IEnumerable<string> Keys 
            => _cellValueDictionary.Keys;

        public IEnumerable<IPropagatorValue> Values 
            => _cellValueDictionary.Values;
        
        public IPropagatorValue this[string key]
        {
            get => _cellValueDictionary[key];
            set
            {
                if (value is null)
                    throw new ArgumentException();

                if (_cellValueDictionary.ContainsKey(key))
                    _cellValueDictionary[key] = value;
                else
                    _cellValueDictionary.Add(key, value);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private PropagatorClosure(IPropagatorNetwork parentNetwork)
        {
            ParentNetwork = parentNetwork;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(string key)
        {
            return _cellValueDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(string key, out IPropagatorValue value)
        {
            return _cellValueDictionary.TryGetValue(key, out value);
        }

        
        //public void SetValue(string cellName, IPropagatorValue value)
        //{
        //    if (_cellValueDictionary.ContainsKey(cellName))
        //        _cellValueDictionary[cellName] = value;
        //    else
        //        _cellValueDictionary.Add(cellName, value);
        //}

        //public IPropagatorValue GetValue(string cellName)
        //{
        //    return _cellValueDictionary[cellName];
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<string, IPropagatorValue>> GetEnumerator()
        {
            return _cellValueDictionary.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            var cellValueText =
                _cellValueDictionary
                    .Select(p => $"{p.Key} := {p.Value}")
                    .Concatenate("," + Environment.NewLine);

            return new LinearTextComposer()
                .AppendLine("Closure [")
                .IncreaseIndentation()
                .AppendLine(cellValueText)
                .DecreaseIndentation()
                .Append("]")
                .ToString();
        }
    }
}

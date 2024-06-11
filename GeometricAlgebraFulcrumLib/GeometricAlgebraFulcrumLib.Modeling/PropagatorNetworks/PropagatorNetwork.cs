using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.PropagatorNetworks
{
    public class PropagatorNetwork :
        IPropagatorNetwork
    {
        protected Dictionary<string, IPropagatorCell> CellsDictionary { get; }
            = new Dictionary<string, IPropagatorCell>();
        

        public IEnumerable<IPropagatorCell> Cells
            => CellsDictionary.Values;

        public IEnumerable<IPropagator> Propagators
            => CellsDictionary.Values.SelectMany(c => c.ClientPropagators).Distinct();

        public bool ModifyEnabled { get; private set; } 

        public bool DebugMode { get; set; }
        
        public ulong Step { get; private set; }
        
        public int Count 
            => CellsDictionary.Count;

        public IPropagatorCell this[string key] 
            => CellsDictionary[key];

        public IEnumerable<string> Keys 
            => CellsDictionary.Keys;

        public IEnumerable<IPropagatorCell> Values 
            => CellsDictionary.Values;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong NextStep()
        {
            Step++;

            return Step;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void BeginModify()
        {
            if (ModifyEnabled)
                throw new InvalidOperationException();

            ModifyEnabled = true;
            ResetCellValues();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EndModify()
        {
            if (!ModifyEnabled)
                throw new InvalidOperationException();

            ModifyEnabled = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ResetCellValues()
        {
            if (!ModifyEnabled)
                throw new InvalidOperationException();

            foreach (var cell in Cells)
                cell.ResetValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AddCell(IPropagatorCell cell)
        {
            if (!ModifyEnabled)
                throw new InvalidOperationException();

            if (!ReferenceEquals(cell.ParentNetwork, this))
                throw new InvalidOperationException();

            CellsDictionary.Add(cell.Name, cell);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(string key)
        {
            return CellsDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(string key, out IPropagatorCell value)
        {
            return CellsDictionary.TryGetValue(key, out value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DebugMessage(string text)
        {
            if (DebugMode) Console.WriteLine(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<string, IPropagatorCell>> GetEnumerator()
        {
            return CellsDictionary.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            var cellsText =
                CellsDictionary.Values
                    .Select(c => c.ToString())
                    .Concatenate(", " + Environment.NewLine);

            var propagatorsText =
                Propagators
                    .Select(p => p.ToString())
                    .Concatenate(", " + Environment.NewLine);

            var composer = new LinearTextComposer();

            composer
                .AppendLine("Propagator network {")
                .IncreaseIndentation()
                .AppendLine("Cells [")
                .IncreaseIndentation()
                .AppendLine(cellsText)
                .DecreaseIndentation()
                .AppendLine("], ")
                .AppendLine("Propagators [")
                .IncreaseIndentation()
                .AppendLine(propagatorsText)
                .DecreaseIndentation()
                .AppendLine("]")
                .DecreaseIndentation()
                .Append("}");

            return composer.ToString();
        }
    }
}

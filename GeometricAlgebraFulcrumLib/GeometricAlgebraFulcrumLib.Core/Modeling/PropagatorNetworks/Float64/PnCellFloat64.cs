using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.PropagatorNetworks.Float64
{
    public sealed class PnCellFloat64 :
        IPropagatorCell<double>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IPropagatorValue DefaultMerge(IPropagatorValue oldValue, IPropagatorValue newValue)
        {
            // New value and old value are similar, do nothing
            if (((PnValueFloat64) oldValue).IsEquivalentTo((PnValueFloat64) newValue))
                return oldValue;

            // Contradiction
            throw new InvalidDataException();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PnCellFloat64 Create(IPropagatorNetwork parentNetwork, string name)
        {
            return new PnCellFloat64(
                parentNetwork, 
                name, 
                DefaultMerge
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PnCellFloat64 Create(IPropagatorNetwork parentNetwork, string name, Func<IPropagatorValue, IPropagatorValue, IPropagatorValue> mergeFunction)
        {
            return new PnCellFloat64(
                parentNetwork, 
                name, 
                mergeFunction
            );
        }


        private readonly HashSet<IPropagator> _clientPropagatorsSet 
            = new HashSet<IPropagator>();

        public IPropagatorNetwork ParentNetwork { get; }

        public string Name { get; }
        
        public PnValueFloat64 ValueFloat64 { get; private set; } 
            = PnValueFloat64.Empty;
        
        public IPropagatorValue Value 
            => ValueFloat64;

        public bool IsEmpty 
            => Value.IsEmpty;

        public IEnumerable<IPropagator> ClientPropagators
            => _clientPropagatorsSet;

        public IEnumerable<IPropagatorCell> ClientCells
            => ClientPropagators.SelectMany(p => p.InputCells).Distinct();

        public Func<IPropagatorValue, IPropagatorValue, IPropagatorValue> MergeFunction { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private PnCellFloat64(IPropagatorNetwork parentNetwork, string name, Func<IPropagatorValue, IPropagatorValue, IPropagatorValue> mergeFunction)
        {
            if (!parentNetwork.ModifyEnabled)
                throw new InvalidOperationException();

            ParentNetwork = parentNetwork;
            Name = name;
            MergeFunction = mergeFunction;

            ParentNetwork.DebugMessage($"Created {this}");
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ResetValue()
        {
            if (!ParentNetwork.ModifyEnabled)
                throw new InvalidOperationException();

            ValueFloat64 = PnValueFloat64.Empty;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PnValueFloat64 Update(int value)
        {
            return (PnValueFloat64) Update(
                (IPropagatorValue) PnValueFloat64.Create(value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PnValueFloat64 Update(double value)
        {
            return (PnValueFloat64) Update(
                (IPropagatorValue) PnValueFloat64.Create(value)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PnValueFloat64 Update(PnValueFloat64 value)
        {
            return (PnValueFloat64) Update(
                (IPropagatorValue) value
            );
        }

        public IPropagatorValue Update(IPropagatorValue value)
        {
            if (ParentNetwork.ModifyEnabled)
                throw new InvalidOperationException();

            // Cell value is unknown
            if (IsEmpty)
            {
                // New value is unknown, no new information is provided
                if (value.IsEmpty) 
                    return PnValueFloat64.Empty;

                // New value is useful, update cell value and alert client propagators
                ValueFloat64 = (PnValueFloat64)value;
            }

            // Cell value is partially\fully known
            else
            {
                // New value is unknown, no new information is provided
                if (value.IsEmpty)
                    return ValueFloat64;

                // Merge new value with old value, might result in contradiction error
                var newValue = (PnValueFloat64) MergeFunction(Value, value);

                // New value is equivalent to old value, no new information is provided
                if (ValueFloat64.IsEquivalentTo(newValue)) 
                    return ValueFloat64;

                ValueFloat64 = newValue;
            }
            
            ParentNetwork.DebugMessage($"Updated {this}");

            var closure = this.GetClientCellsClosure();

            {
                var propagatorsText =
                    ClientPropagators
                        .Select(p => p.ToString())
                        .Concatenate("," + Environment.NewLine);

                ParentNetwork.DebugMessage(
                    new LinearTextComposer()
                        .AppendLine("Alerting propagators: ")
                        .IncreaseIndentation()
                        .AppendLine(propagatorsText)
                        .DecreaseIndentation()
                        .AppendLine("Using " + closure)
                        .ToString()
                ); 
            }

            foreach (var clientPropagator in ClientPropagators)
                clientPropagator.Propagate(closure);

            return ValueFloat64;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddClientPropagator(IPropagator clientPropagator)
        {
            if (!ParentNetwork.ModifyEnabled)
                throw new InvalidOperationException();

            if (!IsEmpty)
                throw new InvalidOperationException();

            _clientPropagatorsSet.Add(clientPropagator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"Cell {Name} := {ValueFloat64}";
        }
    }
}

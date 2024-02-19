using GeometricAlgebraFulcrumLib.Lite.PropagatorNetworks.Float64;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.PropagatorNetworks
{
    public static class PropagatorNetworksUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IPropagatorClosure GetClientCellsClosure(this IPropagatorCell cell)
        {
            return PropagatorClosure.CreateFromClientCells(cell);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddClientPropagators(this IPropagatorCell cell, IEnumerable<IPropagator> clientPropagators)
        {
            foreach (var clientPropagator in clientPropagators)
                cell.AddClientPropagator(clientPropagator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddClientPropagators(this IPropagatorCell cell, params IPropagator[] clientPropagators)
        {
            foreach (var clientPropagator in clientPropagators)
                cell.AddClientPropagator(clientPropagator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValueFloat64(this IPropagatorClosure closure, string cellName, int value)
        {
            closure[cellName] = PnValueFloat64.Create(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValueFloat64(this IPropagatorClosure closure, string cellName, double value)
        {
            closure[cellName] = PnValueFloat64.Create(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IPropagatorValue<T> GetValue<T>(this IPropagatorClosure closure, string cellName)
        {
            return (IPropagatorValue<T>) closure[cellName];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PnValueFloat64 GetValueFloat64(this IPropagatorClosure closure, string cellName)
        {
            return (PnValueFloat64) closure[cellName];
        }

    }
}

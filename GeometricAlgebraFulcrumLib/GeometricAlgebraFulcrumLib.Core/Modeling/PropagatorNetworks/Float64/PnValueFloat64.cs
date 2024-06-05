using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.PropagatorNetworks.Float64
{
    public sealed record PnValueFloat64
        : IPropagatorValue<double>
    {
        public static PnValueFloat64 Empty { get; } 
            = new PnValueFloat64(double.NaN);

        public static PnValueFloat64 Zero { get; }
            = new PnValueFloat64(0d);
        
        public static PnValueFloat64 One { get; }
            = new PnValueFloat64(1d);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PnValueFloat64 Create(double value)
        {
            return new PnValueFloat64(value);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator PnValueFloat64(double value)
        {
            return new PnValueFloat64(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double(PnValueFloat64 value)
        {
            return value.Value;
        }


        public double Value { get; }

        public bool IsEmpty 
            => double.IsNaN(Value);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private PnValueFloat64(double value = double.NaN)
        {
            Value = value;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEquivalentTo(IPropagatorValue<double> otherValue)
        {
            if (IsEmpty || otherValue.IsEmpty)
                return false;

            return Value.IsNearEqual(otherValue.Value);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return IsEmpty 
                ? "<empty>" 
                : Value.ToString("G");
        }
    }
}

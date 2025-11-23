using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    /// <summary>
    /// Represents a single knot (parameter value) with multiplicity in a B-spline knot vector.
    /// Immutable.
    /// </summary>
    public sealed record BSplineKnot
    {
        /// <summary>Start index of this knot within the knot vector (inclusive).</summary>
        public int Index1 { get; }

        /// <summary>End index of this knot within the knot vector (inclusive).</summary>
        public int Index2 => Index1 + Multiplicity - 1;

        /// <summary>Multiplicity of the knot (>= 1).</summary>
        public int Multiplicity { get; }

        /// <summary>The parameter value of this knot.</summary>
        public double Value { get; }

        /// <summary>True if multiplicity == 1.</summary>
        public bool IsSimple => Multiplicity == 1;

        /// <summary>True if multiplicity > 1.</summary>
        public bool IsMultiple => Multiplicity > 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal BSplineKnot(int index1, double value, int multiplicity)
        {
            if (index1 < 0)
                throw new ArgumentOutOfRangeException(nameof(index1), "Index must be non-negative.");

            if (multiplicity < 1)
                throw new ArgumentOutOfRangeException(nameof(multiplicity), "Multiplicity must be >= 1.");

            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid knot value.", nameof(value));

            Index1 = index1;
            Value = value;
            Multiplicity = multiplicity;
        }

        /// <summary>True if this knot covers the given knot-vector index.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(int index) => index >= Index1 && index <= Index2;

        /// <summary>Return a copy with a changed multiplicity.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BSplineKnot WithMultiplicity(int newMultiplicity)
        {
            if (newMultiplicity < 1)
                throw new ArgumentOutOfRangeException(nameof(newMultiplicity));

            return new BSplineKnot(Index1, Value, newMultiplicity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() =>
            IsSimple ? $"<{Value:G}>" : $"<{Value:G}>^{Multiplicity}";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Compare(BSplineKnot a, BSplineKnot b) => a.Value.CompareTo(b.Value);

        public override int GetHashCode() => HashCode.Combine(Value, Multiplicity, Index1);

        /// <summary>
        /// Check if this knot equals another within numerical tolerance.
        /// </summary>
        public bool Equals(BSplineKnot? other, double tolerance)
        {
            if (other is null) return false;
            if (Math.Abs(Value - other.Value) > tolerance) return false;
            if (Multiplicity != other.Multiplicity) return false;
            return true;
        }
    }
}
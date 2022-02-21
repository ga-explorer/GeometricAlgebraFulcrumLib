namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    public sealed record Axis
    {
        public static Axis PositiveX { get; } = new Axis(0, true);

        public static Axis PositiveY { get; } = new Axis(1, true);

        public static Axis PositiveZ { get; } = new Axis(2, true);

        public static Axis PositiveW { get; } = new Axis(3, true);

        public static Axis NegativeX { get; } = new Axis(0, false);

        public static Axis NegativeY { get; } = new Axis(1, false);

        public static Axis NegativeZ { get; } = new Axis(2, false);

        public static Axis NegativeW { get; } = new Axis(3, false);


        public static implicit operator Axis(ulong basisVectorIndex)
        {
            return new Axis(basisVectorIndex);
        }


        public ulong BasisVectorIndex { get; }

        public bool IsPositive { get; }

        public bool IsNegative => !IsPositive;


        public Axis(ulong basisVectorIndex, bool isPositive = true)
        {
            BasisVectorIndex = basisVectorIndex;
            IsPositive = isPositive;
        }
    }
}
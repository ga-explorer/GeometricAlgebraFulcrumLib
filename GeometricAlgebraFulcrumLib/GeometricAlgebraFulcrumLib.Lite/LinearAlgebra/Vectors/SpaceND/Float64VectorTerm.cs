using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND
{
    public sealed record Float64VectorTerm :
        ILinearElement
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm operator +(Float64VectorTerm b1)
        {
            return b1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm operator -(Float64VectorTerm b1)
        {
            return new Float64VectorTerm(b1.BasisVector, -b1.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm operator +(Float64VectorTerm b1, Float64VectorTerm b2)
        {
            if (!b1.BasisVector.Equals(b2.BasisVector))
                throw new InvalidOperationException();

            return new Float64VectorTerm(b1.BasisVector, b1.Scalar + b2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm operator -(Float64VectorTerm b1, Float64VectorTerm b2)
        {
            if (!b1.BasisVector.Equals(b2.BasisVector))
                throw new InvalidOperationException();

            return new Float64VectorTerm(b1.BasisVector, b1.Scalar - b2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm operator *(Float64VectorTerm b1, double s2)
        {
            return new Float64VectorTerm(b1.BasisVector, b1.Scalar * s2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm operator *(double s1, Float64VectorTerm b2)
        {
            return new Float64VectorTerm(b2.BasisVector, s1 * b2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorTerm operator /(Float64VectorTerm b1, double s2)
        {
            return new Float64VectorTerm(b1.BasisVector, b1.Scalar / s2);
        }


        public LinBasisVector BasisVector { get; }

        public int Index
            => BasisVector.Index;

        private double _scalar;
        public double Scalar
        {
            get => _scalar;
            private set
            {
                if (!value.IsValid())
                    throw new ArgumentException(nameof(value));

                _scalar = value;
            }
        }

        public double ScalarValue
            => Scalar;

        public bool IsNegative
            => Scalar < 0;

        public bool IsZero
            => Scalar == 0;

        public bool IsPositive
            => Scalar > 0;

        public bool IsNonZero
            => Scalar != 0;

        public int VSpaceDimensions
            => BasisVector.VSpaceDimensions;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Float64VectorTerm(LinBasisVector basisBlade, double scalar)
        {
            BasisVector = basisBlade;

            if (!scalar.IsValid())
                throw new ArgumentException(nameof(scalar));

            _scalar = scalar;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out LinBasisVector basisVector, out double scalar)
        {
            basisVector = BasisVector;
            scalar = Scalar;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return BasisVector.IsValid() &&
                   ScalarValue.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexItemRecord<double> GetVectorIndexScalarRecord()
        {
            return new IndexItemRecord<double>(
                BasisVector.Index,
                ScalarValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64VectorTerm SetScalar(double scalar)
        {
            Scalar = scalar;

            return this;
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64VectorTerm GetCopy()
        {
            return new Float64VectorTerm(BasisVector, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64VectorTerm GetScaledCopy(int scalarFactor)
        {
            return new Float64VectorTerm(
                BasisVector,
                Scalar * scalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64VectorTerm GetScaledCopy(double scalarFactor)
        {
            return new Float64VectorTerm(
                BasisVector,
                Scalar * scalarFactor
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64VectorTerm InPlaceNegative()
        {
            Scalar = -Scalar;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64VectorTerm InPlaceTimes(int scalarFactor)
        {
            Scalar *= scalarFactor;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64VectorTerm InPlaceTimes(double scalarFactor)
        {
            Scalar *= scalarFactor;

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ENorm()
        {
            return Scalar.Abs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ENormSquared()
        {
            return Scalar * Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64VectorTerm Negative()
        {
            return new Float64VectorTerm(
                BasisVector,
                -Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64VectorTerm Times(int scalarFactor)
        {
            return new Float64VectorTerm(
                BasisVector,
                Scalar * scalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64VectorTerm Times(double scalarFactor)
        {
            return new Float64VectorTerm(
                BasisVector,
                Scalar * scalarFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64VectorTerm EInverse()
        {
            return Times(1d / (Scalar * Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESp(Float64VectorTerm term2)
        {
            return BasisVector.Index == term2.Index
                ? Scalar * term2.Scalar
                : 0d;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Float64VectorTerm? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Scalar, other.Scalar) &&
                   Equals(BasisVector, other.BasisVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(Scalar, BasisVector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({Scalar}){BasisVector}";
        }

    }
}
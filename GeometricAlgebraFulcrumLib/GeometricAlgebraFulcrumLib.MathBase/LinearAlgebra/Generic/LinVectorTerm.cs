using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;

public sealed record LinVectorTerm<T> :
    ILinearElement<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> operator +(LinVectorTerm<T> b1)
    {
        return b1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> operator -(LinVectorTerm<T> b1)
    {
        return new LinVectorTerm<T>(b1.BasisVector, -b1.Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> operator +(LinVectorTerm<T> b1, LinVectorTerm<T> b2)
    {
        if (!b1.BasisVector.Equals(b2.BasisVector))
            throw new InvalidOperationException();

        return new LinVectorTerm<T>(b1.BasisVector, b1.Scalar + b2.Scalar);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> operator -(LinVectorTerm<T> b1, LinVectorTerm<T> b2)
    {
        if (!b1.BasisVector.Equals(b2.BasisVector))
            throw new InvalidOperationException();

        return new LinVectorTerm<T>(b1.BasisVector, b1.Scalar - b2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> operator *(LinVectorTerm<T> b1, double s2)
    {
        return new LinVectorTerm<T>(b1.BasisVector, b1.Scalar * s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> operator *(LinVectorTerm<T> b1, T s2)
    {
        return new LinVectorTerm<T>(b1.BasisVector, b1.Scalar * s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> operator *(double s1, LinVectorTerm<T> b2)
    {
        return new LinVectorTerm<T>(b2.BasisVector, s1 * b2.Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> operator /(LinVectorTerm<T> b1, double s2)
    {
        return new LinVectorTerm<T>(b1.BasisVector, b1.Scalar / s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorTerm<T> operator /(LinVectorTerm<T> b1, T s2)
    {
        return new LinVectorTerm<T>(b1.BasisVector, b1.Scalar / s2);
    }


    public LinBasisVector BasisVector { get; }

    public int Index
        => BasisVector.Index;

    private Scalar<T> _scalar;
    public Scalar<T> Scalar
    {
        get => _scalar;
        private set
        {
            if (!value.IsValid())
                throw new ArgumentException(nameof(value));

            _scalar = value;
        }
    }

    [NotNull]
    public T ScalarValue
        => Scalar.ScalarValue;

    public IScalarProcessor<T> ScalarProcessor
        => Scalar.ScalarProcessor;
    
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
    internal LinVectorTerm(LinBasisVector basisBlade, Scalar<T> scalar)
    {
        BasisVector = basisBlade;

        if (!scalar.IsValid())
            throw new ArgumentException(nameof(scalar));

        _scalar = scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinVectorTerm(LinBasisVector basisBlade, IScalarProcessor<T> scalarProcessor, T scalarValue)
    {
        BasisVector = basisBlade;
        
        if (!scalarProcessor.IsValid(scalarValue))
            throw new ArgumentException(nameof(scalarValue));

        _scalar = scalarProcessor.CreateScalar(scalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out LinBasisVector basisBlade, out Scalar<T> scalar)
    {
        basisBlade = BasisVector;
        scalar = Scalar;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return BasisVector.IsValid() &&
               ScalarProcessor.IsValid(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexItemRecord<T> GetVectorIndexScalarRecord()
    {
        return new IndexItemRecord<T>(
            BasisVector.Index,
            ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> SetScalar(Scalar<T> scalar)
    {
        Scalar = scalar;

        return this;
    }

    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> GetCopy()
    {
        return new LinVectorTerm<T>(BasisVector, Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> GetScaledCopy(int scalarFactor)
    {
        return new LinVectorTerm<T>(
            BasisVector,
            Scalar * scalarFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> GetScaledCopy(double scalarFactor)
    {
        return new LinVectorTerm<T>(
            BasisVector,
            Scalar * scalarFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> GetScaledCopy(T scalarFactor)
    {
        return new LinVectorTerm<T>(
            BasisVector,
            Scalar * scalarFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> GetScaledCopy(Scalar<T> scalarFactor)
    {
        return new LinVectorTerm<T>(
            BasisVector,
            Scalar * scalarFactor
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> InPlaceNegative()
    {
        Scalar = -Scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> InPlaceTimes(int scalarFactor)
    {
        Scalar *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> InPlaceTimes(double scalarFactor)
    {
        Scalar *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> InPlaceTimes(T scalarFactor)
    {
        Scalar *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> InPlaceTimes(Scalar<T> scalar)
    {
        Scalar *= scalar;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENorm()
    {
        return Scalar.Abs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENormSquared()
    {
        return Scalar * Scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> Negative()
    {
        return new LinVectorTerm<T>(
            BasisVector,
            -Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> Times(int scalarFactor)
    {
        return new LinVectorTerm<T>(
            BasisVector,
            Scalar * scalarFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> Times(double scalarFactor)
    {
        return new LinVectorTerm<T>(
            BasisVector,
            Scalar * scalarFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> Times(T scalarFactor)
    {
        return new LinVectorTerm<T>(
            BasisVector,
            Scalar * scalarFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> Times(Scalar<T> scalarFactor)
    {
        return new LinVectorTerm<T>(
            BasisVector,
            Scalar * scalarFactor
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> EInverse()
    {
        return Times((1d / (Scalar * Scalar)));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ESp(LinVectorTerm<T> term2)
    {
        return BasisVector.Index == term2.Index
            ? Scalar * term2.Scalar
            : ScalarProcessor.CreateScalarZero();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(LinVectorTerm<T>? other)
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
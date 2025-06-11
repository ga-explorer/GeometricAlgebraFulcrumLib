using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.System;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;

public sealed record LinFloat64VectorTerm :
    IFloat64LinearAlgebraElement
{
    
    public static LinFloat64VectorTerm operator +(LinFloat64VectorTerm b1)
    {
        return b1;
    }

    
    public static LinFloat64VectorTerm operator -(LinFloat64VectorTerm b1)
    {
        return new LinFloat64VectorTerm(b1.BasisVector, -b1.Scalar);
    }

    
    public static LinFloat64VectorTerm operator +(LinFloat64VectorTerm b1, LinFloat64VectorTerm b2)
    {
        if (!b1.BasisVector.Equals(b2.BasisVector))
            throw new InvalidOperationException();

        return new LinFloat64VectorTerm(b1.BasisVector, b1.Scalar + b2.Scalar);
    }

    
    public static LinFloat64VectorTerm operator -(LinFloat64VectorTerm b1, LinFloat64VectorTerm b2)
    {
        if (!b1.BasisVector.Equals(b2.BasisVector))
            throw new InvalidOperationException();

        return new LinFloat64VectorTerm(b1.BasisVector, b1.Scalar - b2.Scalar);
    }

    
    public static LinFloat64VectorTerm operator *(LinFloat64VectorTerm b1, double s2)
    {
        return new LinFloat64VectorTerm(b1.BasisVector, b1.Scalar * s2);
    }

    
    public static LinFloat64VectorTerm operator *(double s1, LinFloat64VectorTerm b2)
    {
        return new LinFloat64VectorTerm(b2.BasisVector, s1 * b2.Scalar);
    }

    
    public static LinFloat64VectorTerm operator /(LinFloat64VectorTerm b1, double s2)
    {
        return new LinFloat64VectorTerm(b1.BasisVector, b1.Scalar / s2);
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


    
    internal LinFloat64VectorTerm(LinBasisVector basisVector, double scalar)
    {
        Debug.Assert(basisVector.IsValid() && scalar.IsValid());

        if (basisVector.IsPositive)
        {
            BasisVector = basisVector;
            _scalar = scalar;
        }
        else
        {
            BasisVector = basisVector.Negative();
            _scalar = -scalar;
        }
    }


    
    public void Deconstruct(out LinBasisVector basisVector, out double scalar)
    {
        basisVector = BasisVector;
        scalar = Scalar;
    }


    
    public bool IsValid()
    {
        return BasisVector.IsValid() &&
               ScalarValue.IsValid();
    }

    
    public Tuple<int, double> GetVectorIndexScalarRecord()
    {
        return new Tuple<int, double>(
            BasisVector.Index,
            ScalarValue
        );
    }

    
    public LinFloat64VectorTerm SetScalar(double scalar)
    {
        Scalar = scalar;

        return this;
    }



    
    public LinFloat64VectorTerm GetCopy()
    {
        return new LinFloat64VectorTerm(BasisVector, Scalar);
    }

    
    public LinFloat64VectorTerm GetScaledCopy(int scalarFactor)
    {
        return new LinFloat64VectorTerm(
            BasisVector,
            Scalar * scalarFactor
        );
    }

    
    public LinFloat64VectorTerm GetScaledCopy(double scalarFactor)
    {
        return new LinFloat64VectorTerm(
            BasisVector,
            Scalar * scalarFactor
        );
    }


    
    public LinFloat64VectorTerm InPlaceNegative()
    {
        Scalar = -Scalar;

        return this;
    }

    
    public LinFloat64VectorTerm InPlaceTimes(int scalarFactor)
    {
        Scalar *= scalarFactor;

        return this;
    }

    
    public LinFloat64VectorTerm InPlaceTimes(double scalarFactor)
    {
        Scalar *= scalarFactor;

        return this;
    }


    
    public double ENorm()
    {
        return Scalar.Abs();
    }

    
    public double ENormSquared()
    {
        return Scalar * Scalar;
    }

    
    public LinFloat64VectorTerm Negative()
    {
        return new LinFloat64VectorTerm(
            BasisVector,
            -Scalar
        );
    }

    
    public LinFloat64VectorTerm Times(int scalarFactor)
    {
        return new LinFloat64VectorTerm(
            BasisVector,
            Scalar * scalarFactor
        );
    }

    
    public LinFloat64VectorTerm Times(double scalarFactor)
    {
        return new LinFloat64VectorTerm(
            BasisVector,
            Scalar * scalarFactor
        );
    }

    
    public LinFloat64VectorTerm EInverse()
    {
        return Times(1d / (Scalar * Scalar));
    }


    
    public double ESp(LinFloat64VectorTerm term2)
    {
        return BasisVector.Index == term2.Index
            ? Scalar * term2.Scalar
            : 0d;
    }

    
    
    public LinFloat64Vector ToLinVector()
    {
        if (IsZero)
            return LinFloat64Vector.Zero;

        var basisScalarDictionary =
            new SingleItemDictionary<int, double>(Index, ScalarValue);

        return LinFloat64Vector.Create(basisScalarDictionary);
    }


    
    public bool Equals(LinFloat64VectorTerm? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return Equals(Scalar, other.Scalar) &&
               Equals(BasisVector, other.BasisVector);
    }

    
    public override int GetHashCode()
    {
        return HashCode.Combine(Scalar, BasisVector);
    }


    
    public override string ToString()
    {
        return $"({Scalar}){BasisVector}";
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public sealed record LinFloat64Trivector3D :
    ILinFloat64KVector3D
{
    public static LinFloat64Trivector3D Zero { get; }
        = new LinFloat64Trivector3D(0d);

    public static LinFloat64Trivector3D E123 { get; }
        = new LinFloat64Trivector3D(1d);

    public static LinFloat64Trivector3D NegativeE123 { get; }
        = new LinFloat64Trivector3D(-1d);

    public static LinFloat64Trivector3D InverseE123 { get; }
        = new LinFloat64Trivector3D(-1d);

    
    public static LinFloat64Trivector3D Create(double scalar123)
    {
        return new LinFloat64Trivector3D(scalar123);
    }


    
    public static LinFloat64Trivector3D operator +(LinFloat64Trivector3D mv)
    {
        return mv;
    }

    
    public static LinFloat64Trivector3D operator -(LinFloat64Trivector3D mv)
    {
        return new LinFloat64Trivector3D(
            -mv.Scalar123
        );
    }


    
    public static LinFloat64Trivector3D operator +(LinFloat64Trivector3D mv1, LinFloat64Trivector3D mv2)
    {
        return new LinFloat64Trivector3D(
            mv1.Scalar123 + mv2.Scalar123
        );
    }

    
    public static LinFloat64Trivector3D operator -(LinFloat64Trivector3D mv1, LinFloat64Trivector3D mv2)
    {
        return new LinFloat64Trivector3D(
            mv1.Scalar123 - mv2.Scalar123
        );
    }

    
    public static LinFloat64Trivector3D operator *(LinFloat64Trivector3D mv1, double mv2)
    {
        return new LinFloat64Trivector3D(
            mv1.Scalar123 * mv2
        );
    }

    
    public static LinFloat64Trivector3D operator *(double mv1, LinFloat64Trivector3D mv2)
    {
        return new LinFloat64Trivector3D(
            mv1 * mv2.Scalar123
        );
    }

    
    public static LinFloat64Trivector3D operator /(LinFloat64Trivector3D mv1, double mv2)
    {
        return new LinFloat64Trivector3D(
            mv1.Scalar123 / mv2
        );
    }

    
    public static LinFloat64Trivector3D operator /(double mv1, LinFloat64Trivector3D mv2)
    {
        return new LinFloat64Trivector3D(
            mv1 / -mv2.Scalar123
        );
    }


    public int VSpaceDimensions
        => 3;

    public int Grade
        => 3;

    public double Scalar
        => 0d;

    public double Scalar1
        => 0d;

    public double Scalar2
        => 0d;

    public double Scalar3
        => 0d;

    public double Scalar12
        => 0d;

    public double Scalar13
        => 0d;

    public double Scalar23
        => 0d;

    public double Scalar123 { get; }

    public int Count
        => 8;

    public double this[int index]
    {
        get
        {
            if (index is < 0 or > 7)
                throw new IndexOutOfRangeException();

            return index == 7
                ? Scalar123
                : 0d;
        }
    }


    
    private LinFloat64Trivector3D(double scalar123)
    {
        Scalar123 = scalar123;
    }


    
    public bool IsValid()
    {
        return Scalar123.IsValid();
    }

    
    public bool IsZero()
    {
        return Scalar123.IsZero();
    }

    
    public bool IsNearZero(double zeroEpsilon = 1e-12d)
    {
        return Scalar123.IsNearZero(zeroEpsilon);
    }

    
    public double Norm()
    {
        return Scalar123.Abs();
    }

    
    public double NormSquared()
    {
        return Scalar123.Square();
    }

    
    public LinFloat64Multivector3D ToMultivector3D()
    {
        return LinFloat64Multivector3D.Create(this);
    }

    
    public LinFloat64Trivector3D Negative()
    {
        return new LinFloat64Trivector3D(-Scalar123);
    }

    
    public LinFloat64Trivector3D GradeInvolution()
    {
        return Negative();
    }

    
    public LinFloat64Trivector3D Reverse()
    {
        return Negative();
    }

    
    public LinFloat64Trivector3D CliffordConjugate()
    {
        return this;
    }


    
    public LinFloat64Scalar3D DirectionToUnitNormal3D()
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return Scalar123.IsPositive()
            ? LinFloat64Scalar3D.E0
            : LinFloat64Scalar3D.NegativeE0;
    }

    
    public LinFloat64Scalar3D DirectionToNormal3D()
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return LinFloat64Scalar3D.Create(1d / Scalar123);
    }

    
    public LinFloat64Scalar3D DirectionToNormal3D(double scalingFactor)
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return LinFloat64Scalar3D.Create(scalingFactor / Scalar123);
    }

    
    public LinFloat64Scalar3D NormalToUnitDirection3D()
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return Scalar123.IsPositive()
            ? LinFloat64Scalar3D.E0
            : LinFloat64Scalar3D.NegativeE0;
    }

    
    public LinFloat64Scalar3D NormalToDirection3D()
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return LinFloat64Scalar3D.Create(1d / Scalar123);
    }

    
    public LinFloat64Scalar3D NormalToDirection3D(double scalingFactor)
    {
        if (Scalar123.IsZero())
            return LinFloat64Scalar3D.E0;

        return LinFloat64Scalar3D.Create(scalingFactor / Scalar123);
    }


    
    public LinFloat64Scalar3D Dual3D()
    {
        return LinFloat64Scalar3D.Create(-Scalar123);
    }

    
    public LinFloat64Scalar3D Dual3D(double scalingFactor)
    {
        return LinFloat64Scalar3D.Create(-Scalar123 * scalingFactor);
    }

    
    public LinFloat64Scalar3D UnDual3D()
    {
        return LinFloat64Scalar3D.Create(Scalar123);
    }

    
    public LinFloat64Scalar3D UnDual3D(double scalingFactor)
    {
        return LinFloat64Scalar3D.Create(Scalar123 * scalingFactor);
    }
    

    
    public double Sp(LinFloat64Scalar3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Vector3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Bivector3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Trivector3D mv2)
    {
        return -(Scalar123 * mv2.Scalar123);
    }

    
    public double Sp(LinFloat64Multivector3D mv2)
    {
        var mv = 0d;

        if (!IsZero() && !mv2.KVector3.IsZero())
            mv += Sp(mv2.KVector3);

        return mv;
    }

    
    
    public LinFloat64Trivector3D Op(LinFloat64Scalar3D mv2)
    {
        return Create(
            Scalar123 * mv2.Scalar
        );
    }
    
    
    public LinFloat64Scalar3D Op(LinFloat64Vector3D mv2)
    {
        return LinFloat64Scalar3D.Zero;
    }
    
    
    public LinFloat64Scalar3D Op(LinFloat64Bivector3D mv2)
    {
        return LinFloat64Scalar3D.Zero;
    }

    
    public LinFloat64Scalar3D Op(LinFloat64Trivector3D mv2)
    {
        return LinFloat64Scalar3D.Zero;
    }
    
    
    public LinFloat64Multivector3D Op(LinFloat64Multivector3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += Op(mv2.KVector0);

        return mv;
    }


    
    public XGaFloat64HigherKVector ToXGaFloat64Trivector(XGaFloat64Processor processor)
    {
        return processor
            .CreateTrivectorComposer()
            .SetTrivectorTerm(0, 1, 2, Scalar123)
            .GetHigherKVector();
    }

    
    public XGaFloat64HigherKVector ToXGaFloat64Trivector()
    {
        return XGaFloat64Processor
            .Euclidean
            .CreateTrivectorComposer()
            .SetTrivectorTerm(0, 1, 2, Scalar123)
            .GetHigherKVector();
    }


    
    public IEnumerator<double> GetEnumerator()
    {
        yield return Scalar;
        yield return Scalar1;
        yield return Scalar2;
        yield return Scalar12;
        yield return Scalar3;
        yield return Scalar13;
        yield return Scalar23;
        yield return Scalar123;
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    public override string ToString()
    {
        return $"({Scalar123})<1,2,3>";
    }
}
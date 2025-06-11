using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;

public sealed record LinFloat64Vector4D :
    ILinFloat64Vector4D,
    ILinFloat64Multivector4D
{
    public static LinFloat64Vector4D Zero { get; }
        = Create(0, 0, 0, 0);

    public static LinFloat64Vector4D E1 { get; }
        = Create(1, 0, 0, 0);

    public static LinFloat64Vector4D E2 { get; }
        = Create(0, 1, 0, 0);

    public static LinFloat64Vector4D E3 { get; }
        = Create(0, 0, 1, 0);

    public static LinFloat64Vector4D E4 { get; }
        = Create(0, 0, 0, 1);

    public static LinFloat64Vector4D NegativeE1 { get; }
        = Create(-1, 0, 0, 0);

    public static LinFloat64Vector4D NegativeE2 { get; }
        = Create(0, -1, 0, 0);

    public static LinFloat64Vector4D NegativeE3 { get; }
        = Create(0, 0, -1, 0);

    public static LinFloat64Vector4D NegativeE4 { get; }
        = Create(0, 0, 0, -1);

    public static LinFloat64Vector4D Symmetric { get; }
        = Create(1, 1, 1, 1);

    public static LinFloat64Vector4D UnitSymmetric { get; }
        = Create(0.5d, 0.5d, 0.5d, 0.5d);

    public static IReadOnlyList<LinFloat64Vector4D> BasisVectors { get; }
        = [E1, E2, E3, E4];


    
    public static LinFloat64Vector4D Create(int x, int y, int z, int w)
    {
        return new LinFloat64Vector4D(x, y, z, w);
    }

    
    public static LinFloat64Vector4D Create(double x, double y, double z, double w)
    {
        return new LinFloat64Vector4D(x, y, z, w);
    }

    
    public static LinFloat64Vector4D Create(ITriplet<double> v, double s)
    {
        return Create(v.Item1, v.Item2, v.Item3, s);
    }

    
    public static LinFloat64Vector4D Create(IQuad<double> v)
    {
        return Create(v.Item1, v.Item2, v.Item3, v.Item4);
    }

    
    public static LinFloat64Vector4D CreateAffineVector(double x, double y, double z)
    {
        return Create(x, y, z, 0);
    }

    
    public static LinFloat64Vector4D CreateAffinePoint(double x, double y, double z)
    {
        return Create(x, y, z, 1);
    }


    
    public static LinFloat64Vector4D operator -(LinFloat64Vector4D v1)
    {
        return Create(-v1.X, -v1.Y, -v1.Z, -v1.W);
    }

    
    public static LinFloat64Vector4D operator +(LinFloat64Vector4D v1, ILinFloat64Vector4D v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    
    public static LinFloat64Vector4D operator +(ILinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    
    public static LinFloat64Vector4D operator +(LinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    
    public static LinFloat64Vector4D operator -(LinFloat64Vector4D v1, ILinFloat64Vector4D v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    
    public static LinFloat64Vector4D operator -(ILinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    
    public static LinFloat64Vector4D operator -(LinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    
    public static LinFloat64Vector4D operator *(LinFloat64Vector4D v1, double s)
    {
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }

    
    public static LinFloat64Vector4D operator *(double s, LinFloat64Vector4D v1)
    {
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }

    
    public static LinFloat64Vector4D operator /(LinFloat64Vector4D v1, double s)
    {
        Debug.Assert(!s.IsNearZero());

        s = 1.0d / s;
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }


    public double X
        => Scalar1;

    public double Y
        => Scalar2;

    public double Z
        => Scalar3;

    public double W
        => Scalar4;

    public int VSpaceDimensions
        => 4;

    public double Item1
        => X;

    public double Item2
        => Y;

    public double Item3
        => Z;

    public double Item4
        => W;

    public double Scalar
        => 0d;

    public double Scalar1 { get; }

    public double Scalar2 { get; }

    public double Scalar3 { get; }

    public double Scalar4 { get; }

    public double Scalar12
        => 0d;

    public double Scalar13
        => 0d;

    public double Scalar23
        => 0d;

    public double Scalar14
        => 0d;

    public double Scalar24
        => 0d;

    public double Scalar34
        => 0d;

    public double Scalar123
        => 0d;

    public double Scalar124
        => 0d;

    public double Scalar134
        => 0d;

    public double Scalar234
        => 0d;

    public double Scalar1234
        => 0d;

    public int Count
        => 16;

    /// <summary>
    /// Get or set the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public double this[int index]
    {
        get
        {
            if (index is < 0 or > 15)
                throw new IndexOutOfRangeException();

            return index switch
            {
                1 => X,
                2 => Y,
                4 => Z,
                8 => W,
                _ => 0d
            };
        }
    }


    
    private LinFloat64Vector4D(double x, double y, double z, double w)
    {
        Scalar1 = x;
        Scalar2 = y;
        Scalar3 = z;
        Scalar4 = w;
    }


    
    public bool IsValid()
    {
        return X.IsValid() &&
               Y.IsValid() &&
               Z.IsValid() &&
               W.IsValid();
    }

    
    public bool IsZero()
    {
        return X.IsZero() &&
               Y.IsZero() &&
               Z.IsZero() &&
               W.IsZero();
    }

    
    public bool IsNearZero(double zeroEpsilon = 1E-12)
    {
        return Norm().IsNearZero(zeroEpsilon);
    }

    
    public double Norm()
    {
        return (X.Square() + Y.Square() + Z.Square() + W.Square()).Sqrt();
    }

    
    public double NormSquared()
    {
        return X.Square() + Y.Square() + Z.Square() + W.Square();
    }

    //public Float64Multivector4D ToMultivector4D()
    //{
    //    throw new NotImplementedException();
    //}


    
    public IEnumerator<double> GetEnumerator()
    {
        yield return Scalar;     //0000
        yield return Scalar1;    //0001
        yield return Scalar2;    //0010
        yield return Scalar12;   //0011
        yield return Scalar3;    //0100
        yield return Scalar13;   //0101
        yield return Scalar23;   //0110
        yield return Scalar123;  //0111
        yield return Scalar4;    //1000
        yield return Scalar14;   //1001
        yield return Scalar24;   //1010
        yield return Scalar124;  //1011
        yield return Scalar34;   //1100
        yield return Scalar134;  //1101
        yield return Scalar234;  //1110
        yield return Scalar1234; //1111
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    public override string ToString()
    {
        return $"({X:G})<1> + ({Y:G})<2> + ({Z:G})<4> + ({W:G})<8>";
    }
}
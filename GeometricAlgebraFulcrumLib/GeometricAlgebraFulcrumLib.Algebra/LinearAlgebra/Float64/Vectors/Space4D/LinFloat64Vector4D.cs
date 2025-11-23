using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

public readonly record struct LinFloat64Vector4D :
    ILinFloat64Vector4D,
    ILinFloat64Multivector4D
{
    public sealed record ToleranceComparer :
        IEqualityComparer<LinFloat64Vector4D>
    {
        public int DecimalPlaces { get; }

        public double Tolerance { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ToleranceComparer(double tolerance = Float64Utils.DefaultTolerance)
            : this((int)Math.Ceiling(-Math.Abs(tolerance).Log10()))
        {
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ToleranceComparer(int decimalPlaces = 12)
        {
            if (decimalPlaces is < 3 or > 30)
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces));

            DecimalPlaces = decimalPlaces;
            Tolerance = Math.Pow(10, -decimalPlaces);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(LinFloat64Vector4D a, LinFloat64Vector4D b)
        {
            return (a - b).L2NormSquared() < Tolerance * Tolerance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(LinFloat64Vector4D v)
        {
            return HashCode.Combine(
                Math.Round(v.X, DecimalPlaces - 3), 
                Math.Round(v.Y, DecimalPlaces - 3), 
                Math.Round(v.Z, DecimalPlaces - 3), 
                Math.Round(v.W, DecimalPlaces - 3)
            );
        }
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<LinFloat64Vector4D, T> CreateToleranceDictionary<T>(int decimalPlaces = 12)
    {
        return new Dictionary<LinFloat64Vector4D, T>(
            new ToleranceComparer(decimalPlaces)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<LinFloat64Vector4D, T> CreateToleranceDictionary<T>(double tolerance = Float64Utils.DefaultTolerance)
    {
        return new Dictionary<LinFloat64Vector4D, T>(
            new ToleranceComparer(tolerance)
        );
    }


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

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Create(double x, double y, double z, double w)
    {
        return new LinFloat64Vector4D(x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Create(int x, int y, int z, int w)
    {
        return new LinFloat64Vector4D(x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Create(ITriplet<double> v, double s)
    {
        return Create(v.Item1, v.Item2, v.Item3, s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D Create(IQuad<double> v)
    {
        return Create(v.Item1, v.Item2, v.Item3, v.Item4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D CreateAffineVector(double x, double y, double z)
    {
        return Create(x, y, z, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D CreateAffinePoint(double x, double y, double z)
    {
        return Create(x, y, z, 1);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator (double, double, double, double)(LinFloat64Vector4D v)
    {
        return (v.X, v.Y, v.Z, v.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinFloat64Vector4D((double, double, double, double) v)
    {
        return Create(v.Item1, v.Item2, v.Item3, v.Item4);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator -(LinFloat64Vector4D v1)
    {
        return Create(-v1.X, -v1.Y, -v1.Z, -v1.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator +(LinFloat64Vector4D v1, ILinFloat64Vector4D v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator +(ILinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator +(LinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator -(LinFloat64Vector4D v1, ILinFloat64Vector4D v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator -(ILinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator -(LinFloat64Vector4D v1, LinFloat64Vector4D v2)
    {
        return Create(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator *(LinFloat64Vector4D v1, double s)
    {
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator *(double s, LinFloat64Vector4D v1)
    {
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4D operator /(LinFloat64Vector4D v1, double s)
    {
        Debug.Assert(!s.IsNearZero());

        s = 1.0d / s;
        return Create(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
    }


    public double X { get; }

    public double Y { get; }

    public double Z { get; }

    public double W { get; }

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

    public Float64Scalar Scalar
        => Float64Scalar.Zero;

    public Float64Scalar Scalar1 
        => X;

    public Float64Scalar Scalar2 
        => Y;

    public Float64Scalar Scalar3 
        => Z;

    public Float64Scalar Scalar4
        => W;

    public Float64Scalar Scalar12
        => Float64Scalar.Zero;

    public Float64Scalar Scalar13
        => Float64Scalar.Zero;

    public Float64Scalar Scalar23
        => Float64Scalar.Zero;

    public Float64Scalar Scalar14
        => Float64Scalar.Zero;

    public Float64Scalar Scalar24
        => Float64Scalar.Zero;

    public Float64Scalar Scalar34
        => Float64Scalar.Zero;

    public Float64Scalar Scalar123
        => Float64Scalar.Zero;

    public Float64Scalar Scalar124
        => Float64Scalar.Zero;

    public Float64Scalar Scalar134
        => Float64Scalar.Zero;

    public Float64Scalar Scalar234
        => Float64Scalar.Zero;

    public Float64Scalar Scalar1234
        => Float64Scalar.Zero;

    public int Count
        => 16;
    
    public double this[int i] 
        => i switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            3 => W,
            _ => Float64Scalar.Zero
        };


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Vector4D(double x, double y, double z, double w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;

        Debug.Assert(IsValid());
    }

    public void Deconstruct(out double x, out double y, out double z, out double w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return X.IsValid() &&
               Y.IsValid() &&
               Z.IsValid() &&
               W.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return X.IsZero() &&
               Y.IsZero() &&
               Z.IsZero() &&
               W.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double tolerance = Float64Utils.DefaultTolerance)
    {
        return L2Norm().IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double L2Norm()
    {
        return (X.Square() + Y.Square() + Z.Square() + W.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double L2NormSquared()
    {
        return X.Square() + Y.Square() + Z.Square() + W.Square();
    }

    //public Float64Multivector4D ToMultivector4D()
    //{
    //    throw new NotImplementedException();
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D Times(double s)
    {
        Debug.Assert(s.IsValid());

        return Create(X * s, Y * s, Z * s, W * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D Divide(double s)
    {
        Debug.Assert(s.IsValid() && !s.IsNearZero());

        s = 1.0d / s;
        return Create(X * s, Y * s, Z * s, W * s);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D DivideByENorm()
    {
        return Divide(L2Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D DivideByENormSquared()
    {
        return Divide(L2NormSquared());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseMatrix OuterProduct(LinFloat64Vector4D v)
    {
        var m = DenseMatrix.Create(4, 4, 0.0);

        m[0, 0] = X * v.X;
        m[0, 1] = X * v.Y;
        m[0, 2] = X * v.Z;
        m[0, 3] = X * v.W;

        m[1, 0] = Y * v.X;
        m[1, 1] = Y * v.Y;
        m[1, 2] = Y * v.Z;
        m[1, 3] = Y * v.W;

        m[2, 0] = Z * v.X;
        m[2, 1] = Z * v.Y;
        m[2, 2] = Z * v.Z;
        m[2, 3] = Z * v.W;

        m[3, 0] = W * v.X;
        m[3, 1] = W * v.Y;
        m[3, 2] = W * v.Z;
        m[3, 3] = W * v.W;

        return m;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Scalar> GetEnumerator()
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToTupleString()
    {
        return $"({Scalar1:G10}, {Scalar2:G10}, {Scalar3:G10}, {Scalar4:G10})";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({X:G})<1> + ({Y:G})<2> + ({Z:G})<4> + ({W:G})<8>";
    }

    Float64Scalar IReadOnlyList<Float64Scalar>.this[int index] => this[index];
}
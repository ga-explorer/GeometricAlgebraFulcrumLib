using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Euclidean2D;

public sealed record Ega2Multivector :
    IGeometricElement
{
    public static Ega2Multivector Zero { get; }
        = new Ega2Multivector(0, 0, 0, 0);

    public static Ega2Multivector E0 { get; }
        = new Ega2Multivector(1, 0, 0, 0);

    public static Ega2Multivector E0Negative { get; }
        = new Ega2Multivector(-1, 0, 0, 0);
        
    public static Ega2Multivector E1 { get; }
        = new Ega2Multivector(0, 1, 0, 0);
        
    public static Ega2Multivector E1Negative { get; }
        = new Ega2Multivector(0, -1, 0, 0);
        
    public static Ega2Multivector E2 { get; }
        = new Ega2Multivector(0, 0, 1, 0);
        
    public static Ega2Multivector E2Negative { get; }
        = new Ega2Multivector(0, 0, -1, 0);
        
    public static Ega2Multivector E1E2 { get; }
        = new Ega2Multivector(0, 0, 0, 1);
        
    public static Ega2Multivector E1E2Negative { get; }
        = new Ega2Multivector(0, 0, 0, -1);

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector CreateScalar(double scalar0)
    {
        return new Ega2Multivector(scalar0, 0d, 0d, 0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector CreateVector(double scalar1, double scalar2)
    {
        return new Ega2Multivector(0d, scalar1, scalar2, 0d);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector CreateVector(IFloat64Vector2D vector)
    {
        return new Ega2Multivector(0d, vector.X, vector.Y, 0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector CreateBivector(double scalar12)
    {
        return new Ega2Multivector(0d, 0d, 0d, scalar12);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector CreateScalarBivector(double scalar0, double scalar12)
    {
        return new Ega2Multivector(scalar0, 0d, 0d, scalar12);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector Create(double scalar0, IFloat64Vector2D vector, double scalar12)
    {
        return new Ega2Multivector(scalar0, vector.X, vector.Y, scalar12);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector Create(double scalar0, double scalar1, double scalar2, double scalar12)
    {
        return new Ega2Multivector(scalar0, scalar1, scalar2, scalar12);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Ega2Multivector(double scalar)
    {
        return new Ega2Multivector(scalar, 0, 0, 0);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Ega2Multivector(Float64Vector2D vector)
    {
        return new Ega2Multivector(0, vector.X, vector.Y, 0);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator +(Ega2Multivector mv1)
    {
        return mv1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator -(Ega2Multivector mv1)
    {
        return new Ega2Multivector(
            -mv1.Scalar0,
            -mv1.Scalar1,
            -mv1.Scalar2,
            -mv1.Scalar12
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator +(Ega2Multivector mv1, double mv2)
    {
        return new Ega2Multivector(
            mv1.Scalar0 + mv2,
            mv1.Scalar1,
            mv1.Scalar2,
            mv1.Scalar12
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator +(double mv1, Ega2Multivector mv2)
    {
        return new Ega2Multivector(
            mv1 + mv2.Scalar0,
            mv2.Scalar1,
            mv2.Scalar2,
            mv2.Scalar12
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator +(Ega2Multivector mv1, IFloat64Vector2D mv2)
    {
        return new Ega2Multivector(
            mv1.Scalar0,
            mv1.Scalar1 + mv2.X,
            mv1.Scalar2 + mv2.Y,
            mv1.Scalar12
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator +(IFloat64Vector2D mv1, Ega2Multivector mv2)
    {
        return new Ega2Multivector(
            mv2.Scalar0,
            mv1.X + mv2.Scalar1,
            mv1.Y + mv2.Scalar2,
            mv2.Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator +(Ega2Multivector mv1, Ega2Multivector mv2)
    {
        return new Ega2Multivector(
            mv1.Scalar0 + mv2.Scalar0,
            mv1.Scalar1 + mv2.Scalar1,
            mv1.Scalar2 + mv2.Scalar2,
            mv1.Scalar12 + mv2.Scalar12
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator -(Ega2Multivector mv1, double mv2)
    {
        return new Ega2Multivector(
            mv1.Scalar0 - mv2,
            mv1.Scalar1,
            mv1.Scalar2,
            mv1.Scalar12
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator -(double mv1, Ega2Multivector mv2)
    {
        return new Ega2Multivector(
            mv1 - mv2.Scalar0,
            -mv2.Scalar1,
            -mv2.Scalar2,
            -mv2.Scalar12
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator -(Ega2Multivector mv1, IFloat64Vector2D mv2)
    {
        return new Ega2Multivector(
            mv1.Scalar0,
            mv1.Scalar1 - mv2.X,
            mv1.Scalar2 - mv2.Y,
            mv1.Scalar12
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator -(IFloat64Vector2D mv1, Ega2Multivector mv2)
    {
        return new Ega2Multivector(
            -mv2.Scalar0,
            mv1.X - mv2.Scalar1,
            mv1.Y - mv2.Scalar2,
            -mv2.Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator -(Ega2Multivector mv1, Ega2Multivector mv2)
    {
        return new Ega2Multivector(
            mv1.Scalar0 - mv2.Scalar0,
            mv1.Scalar1 - mv2.Scalar1,
            mv1.Scalar2 - mv2.Scalar2,
            mv1.Scalar12 - mv2.Scalar12
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator *(Ega2Multivector mv1, double mv2)
    {
        return new Ega2Multivector(
            mv1.Scalar0 * mv2,
            mv1.Scalar1 * mv2,
            mv1.Scalar2 * mv2,
            mv1.Scalar12 * mv2
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator *(double mv1, Ega2Multivector mv2)
    {
        return new Ega2Multivector(
            mv1 * mv2.Scalar0,
            mv1 * mv2.Scalar1,
            mv1 * mv2.Scalar2,
            mv1 * mv2.Scalar12
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ega2Multivector operator /(Ega2Multivector mv1, double mv2)
    {
        mv2 = 1d / mv2;

        return new Ega2Multivector(
            mv1.Scalar0 * mv2,
            mv1.Scalar1 * mv2,
            mv1.Scalar2 * mv2,
            mv1.Scalar12 * mv2
        );
    }


    public double Scalar0 { get; }

    public double Scalar1 { get; }

    public double Scalar2 { get; }

    public double Scalar12 { get; }

    public double this[int grade, int index]
    {
        get
        {
            return grade switch
            {
                < 0 or > 2 => throw new ArgumentOutOfRangeException(nameof(grade)),
                0 => index == 0 ? Scalar0 : throw new IndexOutOfRangeException(nameof(index)),
                1 => index switch
                {
                    0 => Scalar1,
                    1 => Scalar2,
                    _ => throw new IndexOutOfRangeException(nameof(index))
                },
                _ => index == 0 ? Scalar12 : throw new IndexOutOfRangeException(nameof(index))
            };
        }
    }

    public double this[int id]
    {
        get
        {
            return id switch
            {
                0 => Scalar0,
                1 => Scalar1,
                2 => Scalar2,
                3 => Scalar12,
                _ => throw new IndexOutOfRangeException()
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Ega2Multivector(double scalar0, double scalar1, double scalar2, double scalar12)
    {
        Scalar0 = scalar0;
        Scalar1 = scalar1;
        Scalar2 = scalar2;
        Scalar12 = scalar12;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar0.IsValid() &&
               Scalar1.IsValid() &&
               Scalar2.IsValid() &&
               Scalar12.IsValid();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector GetScalarPart()
    {
        return new Ega2Multivector(Scalar0, 0d, 0d, 0d);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector GetVectorPart()
    {
        return new Ega2Multivector(0d, Scalar1, Scalar2, 0d);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector GetBivectorPart()
    {
        return new Ega2Multivector(0d, 0d, 0d, Scalar12);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector GetScalarBivectorPart()
    {
        return new Ega2Multivector(Scalar0, 0d, 0d, Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector2D GetVectorPartAsTuple2D()
    {
        return Float64Vector2D.Create((Float64Scalar)Scalar1, (Float64Scalar)Scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double NormSquared()
    {
        return Scalar0 * Scalar0 + 
               Scalar1 * Scalar1 + 
               Scalar2 * Scalar2 + 
               Scalar12 * Scalar12;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Norm()
    {
        return Math.Sqrt(NormSquared());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector GradeInvolution()
    {
        return new Ega2Multivector(
            Scalar0,
            -Scalar1,
            -Scalar1,
            Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector Reverse()
    {
        return new Ega2Multivector(
            Scalar0,
            Scalar1,
            Scalar1,
            -Scalar12
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector CliffordConjugate()
    {
        return new Ega2Multivector(
            Scalar0,
            -Scalar1,
            -Scalar1,
            -Scalar12
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector Inverse()
    {
        var normSquaredInv = 1d / NormSquared();

        return new Ega2Multivector(
            Scalar0 * normSquaredInv,
            Scalar1 * normSquaredInv,
            Scalar2 * normSquaredInv,
            -Scalar12 * normSquaredInv
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Sp(Ega2Multivector mv2)
    {
        return Scalar0 * mv2.Scalar0 + Scalar1 * mv2.Scalar1 + Scalar2 * mv2.Scalar2 - Scalar12 * mv2.Scalar12;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector Op(Ega2Multivector mv2)
    {
        return new Ega2Multivector(
            Scalar0 * mv2.Scalar0,
            Scalar0 * mv2.Scalar1 + Scalar1 * mv2.Scalar0,
            Scalar0 * mv2.Scalar2 + Scalar2 * mv2.Scalar0,
            Scalar0 * mv2.Scalar12 + Scalar1 * mv2.Scalar2 - Scalar2 * mv2.Scalar1 + Scalar12 * mv2.Scalar0
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector Gp(Ega2Multivector mv2)
    {
        return new Ega2Multivector(
            Scalar0 * mv2.Scalar0 + Scalar1 * mv2.Scalar1 + Scalar2 * mv2.Scalar2 - Scalar12 * mv2.Scalar12,
            Scalar0 * mv2.Scalar1 + Scalar1 * mv2.Scalar0 - Scalar2 * mv2.Scalar12 + Scalar12 * mv2.Scalar2,
            Scalar0 * mv2.Scalar2 + Scalar1 * mv2.Scalar12 + Scalar2 * mv2.Scalar0 - Scalar12 * mv2.Scalar1,
            Scalar0 * mv2.Scalar12 + Scalar1 * mv2.Scalar2 - Scalar2 * mv2.Scalar1 + Scalar12 * mv2.Scalar0
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector Lcp(Ega2Multivector mv2)
    {
        return new Ega2Multivector(
            Scalar0 * mv2.Scalar0 + Scalar1 * mv2.Scalar1 + Scalar2 * mv2.Scalar2 - Scalar12 * mv2.Scalar12,
            Scalar0 * mv2.Scalar1 + Scalar1 * mv2.Scalar0 - Scalar2 * mv2.Scalar12,
            Scalar0 * mv2.Scalar2 + Scalar1 * mv2.Scalar12 + Scalar2 * mv2.Scalar0,
            Scalar0 * mv2.Scalar12 + Scalar12 * mv2.Scalar0
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ega2Multivector Rcp(Ega2Multivector mv2)
    {
        return new Ega2Multivector(
            Scalar0 * mv2.Scalar0 + Scalar1 * mv2.Scalar1 + Scalar2 * mv2.Scalar2 - Scalar12 * mv2.Scalar12,
            Scalar0 * mv2.Scalar1 + Scalar1 * mv2.Scalar0 + Scalar12 * mv2.Scalar2,
            Scalar0 * mv2.Scalar2 + Scalar2 * mv2.Scalar0 - Scalar12 * mv2.Scalar1,
            Scalar0 * mv2.Scalar12 + Scalar12 * mv2.Scalar0
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"'{Scalar0:G}'<> + '{Scalar1:G}'<1> + '{Scalar2:G}'<2> + '{Scalar12:G}'<1,2>";
    }
}
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga201;

public sealed partial class Ga201KVector3
{
    public static Ga201KVector3 Zero { get; } = new Ga201KVector3();
    
    public static Ga201KVector3 E123 { get; } = new Ga201KVector3() { Scalar123 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector3 Create(params double[] scalarArray)
    {
        return new Ga201KVector3
        {
            Scalar123 = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector3 operator +(Ga201KVector3 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector3 operator -(Ga201KVector3 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector3 operator *(Ga201KVector3 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector3 operator *(double mv1, Ga201KVector3 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector3 operator /(Ga201KVector3 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator +(Ga201KVector3 mv1, Ga201KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator -(Ga201KVector3 mv1, Ga201KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator +(Ga201KVector3 mv1, Ga201KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator -(Ga201KVector3 mv1, Ga201KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator +(Ga201KVector3 mv1, Ga201KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator -(Ga201KVector3 mv1, Ga201KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator +(Ga201KVector3 mv1, Ga201Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator -(Ga201KVector3 mv1, Ga201Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator +(Ga201KVector3 mv1, double mv2)
    {
        return Ga201Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator +(double mv1, Ga201KVector3 mv2)
    {
        return Ga201Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator -(Ga201KVector3 mv1, double mv2)
    {
        return Ga201Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201Multivector operator -(double mv1, Ga201KVector3 mv2)
    {
        return Ga201Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar123 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3(double scalar)
    {
        Scalar123 = scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar123);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar123 == 0d;
    
        return _isZero.Value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double zeroEpsilon = 1e-12d)
    {
        var norm = this.Norm();
    
        return norm > -zeroEpsilon && norm < zeroEpsilon;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetMultivectorArray()
    {
        var scalarArray = new double[8];
        
        scalarArray[7] = Scalar123;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 Times(double mv2)
    {
        return new Ga201KVector3()
        {
            Scalar123 = Scalar123 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 Negative()
    {
        return new Ga201KVector3()
        {
            Scalar123 = -Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 Conjugate()
    {
        return Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector0 UnDual()
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector0 Dual(Ga201KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector0 UnDual(Ga201KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201Multivector Add(Ga201KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga201Multivector.Create(this);
        if (IsZero()) return Ga201Multivector.Create(mv2);
        
        return Ga201Multivector.Create(
            mv2,
            Ga201KVector1.Zero,
            Ga201KVector2.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201Multivector Add(Ga201KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga201Multivector.Create(this);
        if (IsZero()) return Ga201Multivector.Create(mv2);
        
        return Ga201Multivector.Create(
            Ga201KVector0.Zero,
            mv2,
            Ga201KVector2.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201Multivector Add(Ga201KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga201Multivector.Create(this);
        if (IsZero()) return Ga201Multivector.Create(mv2);
        
        return Ga201Multivector.Create(
            Ga201KVector0.Zero,
            Ga201KVector1.Zero,
            mv2,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 Add(Ga201KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga201KVector3()
        {
            Scalar123 = Scalar123 + mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201Multivector Add(Ga201Multivector mv2)
    {
        if (mv2.IsZero()) return Ga201Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga201Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            Add(mv2.KVector3)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201Multivector Subtract(Ga201KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga201Multivector.Create(this);
        if (IsZero()) return Ga201Multivector.Create(mv2.Negative());
        
        return Ga201Multivector.Create(
            mv2.Negative(),
            Ga201KVector1.Zero,
            Ga201KVector2.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201Multivector Subtract(Ga201KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga201Multivector.Create(this);
        if (IsZero()) return Ga201Multivector.Create(mv2.Negative());
        
        return Ga201Multivector.Create(
            Ga201KVector0.Zero,
            mv2.Negative(),
            Ga201KVector2.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201Multivector Subtract(Ga201KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga201Multivector.Create(this);
        if (IsZero()) return Ga201Multivector.Create(mv2.Negative());
        
        return Ga201Multivector.Create(
            Ga201KVector0.Zero,
            Ga201KVector1.Zero,
            mv2.Negative(),
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201KVector3 Subtract(Ga201KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga201KVector3()
        {
            Scalar123 = Scalar123 - mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga201Multivector Subtract(Ga201Multivector mv2)
    {
        if (mv2.IsZero()) return Ga201Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga201Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            Subtract(mv2.KVector3)
        );
    }
    
}

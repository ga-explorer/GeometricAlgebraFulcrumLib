using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga4;

public sealed partial class Ga4KVector3
{
    public static Ga4KVector3 Zero { get; } = new Ga4KVector3();
    
    public static Ga4KVector3 E123 { get; } = new Ga4KVector3() { Scalar123 = 1d };
    
    public static Ga4KVector3 E124 { get; } = new Ga4KVector3() { Scalar124 = 1d };
    
    public static Ga4KVector3 E134 { get; } = new Ga4KVector3() { Scalar134 = 1d };
    
    public static Ga4KVector3 E234 { get; } = new Ga4KVector3() { Scalar234 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector3 Create(params double[] scalarArray)
    {
        return new Ga4KVector3
        {
            Scalar123 = scalarArray[0],
            Scalar124 = scalarArray[1],
            Scalar134 = scalarArray[2],
            Scalar234 = scalarArray[3]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector3 operator +(Ga4KVector3 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector3 operator -(Ga4KVector3 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector3 operator *(Ga4KVector3 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector3 operator *(double mv1, Ga4KVector3 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector3 operator /(Ga4KVector3 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector3 mv1, Ga4KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector3 mv1, Ga4KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector3 mv1, Ga4KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector3 mv1, Ga4KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector3 mv1, Ga4KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector3 mv1, Ga4KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector3 operator +(Ga4KVector3 mv1, Ga4KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector3 operator -(Ga4KVector3 mv1, Ga4KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector3 mv1, Ga4Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector3 mv1, Ga4Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector3 mv1, double mv2)
    {
        return Ga4Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(double mv1, Ga4KVector3 mv2)
    {
        return Ga4Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector3 mv1, double mv2)
    {
        return Ga4Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(double mv1, Ga4KVector3 mv2)
    {
        return Ga4Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar123 { get; init; }
    
    public double Scalar124 { get; init; }
    
    public double Scalar134 { get; init; }
    
    public double Scalar234 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar123) &&
            !double.IsNaN(Scalar124) &&
            !double.IsNaN(Scalar134) &&
            !double.IsNaN(Scalar234);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar123 == 0d &&
            Scalar124 == 0d &&
            Scalar134 == 0d &&
            Scalar234 == 0d;
    
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
        var scalarArray = new double[16];
        
        scalarArray[7] = Scalar123;
        scalarArray[11] = Scalar124;
        scalarArray[13] = Scalar134;
        scalarArray[14] = Scalar234;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar123,
            Scalar124,
            Scalar134,
            Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 Times(double mv2)
    {
        return new Ga4KVector3()
        {
            Scalar123 = Scalar123 * mv2,
            Scalar124 = Scalar124 * mv2,
            Scalar134 = Scalar134 * mv2,
            Scalar234 = Scalar234 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 Negative()
    {
        return new Ga4KVector3()
        {
            Scalar123 = -Scalar123,
            Scalar124 = -Scalar124,
            Scalar134 = -Scalar134,
            Scalar234 = -Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 Conjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector1 Dual()
    {
        return new Ga4KVector1()
               {
                   Scalar1 = Scalar234,
                   Scalar2 = -Scalar134,
                   Scalar3 = Scalar124,
                   Scalar4 = -Scalar123
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector1 UnDual()
    {
        return new Ga4KVector1()
               {
                   Scalar1 = Scalar234,
                   Scalar2 = -Scalar134,
                   Scalar3 = Scalar124,
                   Scalar4 = -Scalar123
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Dual(Ga4KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 UnDual(Ga4KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector1 Dual(Ga4KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector1 UnDual(Ga4KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            mv2,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            this,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            mv2,
            Ga4KVector2.Zero,
            this,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            Ga4KVector1.Zero,
            mv2,
            this,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 Add(Ga4KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga4KVector3()
        {
            Scalar123 = Scalar123 + mv2.Scalar123,
            Scalar124 = Scalar124 + mv2.Scalar124,
            Scalar134 = Scalar134 + mv2.Scalar134,
            Scalar234 = Scalar234 + mv2.Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            this,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4Multivector mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga4Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            Add(mv2.KVector3),
            mv2.KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            mv2.Negative(),
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            this,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            mv2.Negative(),
            Ga4KVector2.Zero,
            this,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            Ga4KVector1.Zero,
            mv2.Negative(),
            this,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 Subtract(Ga4KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga4KVector3()
        {
            Scalar123 = Scalar123 - mv2.Scalar123,
            Scalar124 = Scalar124 - mv2.Scalar124,
            Scalar134 = Scalar134 - mv2.Scalar134,
            Scalar234 = Scalar234 - mv2.Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            this,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4Multivector mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga4Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            Subtract(mv2.KVector3),
            mv2.KVector4.Negative()
        );
    }
    
}

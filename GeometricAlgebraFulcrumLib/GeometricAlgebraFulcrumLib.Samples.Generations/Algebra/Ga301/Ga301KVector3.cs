using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga301;

public sealed partial class Ga301KVector3
{
    public static Ga301KVector3 Zero { get; } = new Ga301KVector3();
    
    public static Ga301KVector3 E123 { get; } = new Ga301KVector3() { Scalar123 = 1d };
    
    public static Ga301KVector3 E124 { get; } = new Ga301KVector3() { Scalar124 = 1d };
    
    public static Ga301KVector3 E134 { get; } = new Ga301KVector3() { Scalar134 = 1d };
    
    public static Ga301KVector3 E234 { get; } = new Ga301KVector3() { Scalar234 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 Create(params double[] scalarArray)
    {
        return new Ga301KVector3
        {
            Scalar123 = scalarArray[0],
            Scalar124 = scalarArray[1],
            Scalar134 = scalarArray[2],
            Scalar234 = scalarArray[3]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 operator +(Ga301KVector3 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 operator -(Ga301KVector3 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 operator *(Ga301KVector3 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 operator *(double mv1, Ga301KVector3 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 operator /(Ga301KVector3 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator +(Ga301KVector3 mv1, Ga301KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator -(Ga301KVector3 mv1, Ga301KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator +(Ga301KVector3 mv1, Ga301KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator -(Ga301KVector3 mv1, Ga301KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator +(Ga301KVector3 mv1, Ga301KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator -(Ga301KVector3 mv1, Ga301KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 operator +(Ga301KVector3 mv1, Ga301KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 operator -(Ga301KVector3 mv1, Ga301KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator +(Ga301KVector3 mv1, Ga301Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator -(Ga301KVector3 mv1, Ga301Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator +(Ga301KVector3 mv1, double mv2)
    {
        return Ga301Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator +(double mv1, Ga301KVector3 mv2)
    {
        return Ga301Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator -(Ga301KVector3 mv1, double mv2)
    {
        return Ga301Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector operator -(double mv1, Ga301KVector3 mv2)
    {
        return Ga301Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar123 { get; init; }
    
    public double Scalar124 { get; init; }
    
    public double Scalar134 { get; init; }
    
    public double Scalar234 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3()
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
    public Ga301KVector3 Times(double mv2)
    {
        return new Ga301KVector3()
        {
            Scalar123 = Scalar123 * mv2,
            Scalar124 = Scalar124 * mv2,
            Scalar134 = Scalar134 * mv2,
            Scalar234 = Scalar234 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 Negative()
    {
        return new Ga301KVector3()
        {
            Scalar123 = -Scalar123,
            Scalar124 = -Scalar124,
            Scalar134 = -Scalar134,
            Scalar234 = -Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 Conjugate()
    {
        return new Ga301KVector3()
               {
                   Scalar234 = -Scalar234
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector1 UnDual()
    {
        return new Ga301KVector1()
               {
                   Scalar1 = Scalar234
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector0 Dual(Ga301KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector0 UnDual(Ga301KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector1 Dual(Ga301KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector1 UnDual(Ga301KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301Multivector Add(Ga301KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga301Multivector.Create(this);
        if (IsZero()) return Ga301Multivector.Create(mv2);
        
        return Ga301Multivector.Create(
            mv2,
            Ga301KVector1.Zero,
            Ga301KVector2.Zero,
            this,
            Ga301KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301Multivector Add(Ga301KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga301Multivector.Create(this);
        if (IsZero()) return Ga301Multivector.Create(mv2);
        
        return Ga301Multivector.Create(
            Ga301KVector0.Zero,
            mv2,
            Ga301KVector2.Zero,
            this,
            Ga301KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301Multivector Add(Ga301KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga301Multivector.Create(this);
        if (IsZero()) return Ga301Multivector.Create(mv2);
        
        return Ga301Multivector.Create(
            Ga301KVector0.Zero,
            Ga301KVector1.Zero,
            mv2,
            this,
            Ga301KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 Add(Ga301KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga301KVector3()
        {
            Scalar123 = Scalar123 + mv2.Scalar123,
            Scalar124 = Scalar124 + mv2.Scalar124,
            Scalar134 = Scalar134 + mv2.Scalar134,
            Scalar234 = Scalar234 + mv2.Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301Multivector Add(Ga301KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga301Multivector.Create(this);
        if (IsZero()) return Ga301Multivector.Create(mv2);
        
        return Ga301Multivector.Create(
            Ga301KVector0.Zero,
            Ga301KVector1.Zero,
            Ga301KVector2.Zero,
            this,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301Multivector Add(Ga301Multivector mv2)
    {
        if (mv2.IsZero()) return Ga301Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga301Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            Add(mv2.KVector3),
            mv2.KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301Multivector Subtract(Ga301KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga301Multivector.Create(this);
        if (IsZero()) return Ga301Multivector.Create(mv2.Negative());
        
        return Ga301Multivector.Create(
            mv2.Negative(),
            Ga301KVector1.Zero,
            Ga301KVector2.Zero,
            this,
            Ga301KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301Multivector Subtract(Ga301KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga301Multivector.Create(this);
        if (IsZero()) return Ga301Multivector.Create(mv2.Negative());
        
        return Ga301Multivector.Create(
            Ga301KVector0.Zero,
            mv2.Negative(),
            Ga301KVector2.Zero,
            this,
            Ga301KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301Multivector Subtract(Ga301KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga301Multivector.Create(this);
        if (IsZero()) return Ga301Multivector.Create(mv2.Negative());
        
        return Ga301Multivector.Create(
            Ga301KVector0.Zero,
            Ga301KVector1.Zero,
            mv2.Negative(),
            this,
            Ga301KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301KVector3 Subtract(Ga301KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga301KVector3()
        {
            Scalar123 = Scalar123 - mv2.Scalar123,
            Scalar124 = Scalar124 - mv2.Scalar124,
            Scalar134 = Scalar134 - mv2.Scalar134,
            Scalar234 = Scalar234 - mv2.Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301Multivector Subtract(Ga301KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga301Multivector.Create(this);
        if (IsZero()) return Ga301Multivector.Create(mv2.Negative());
        
        return Ga301Multivector.Create(
            Ga301KVector0.Zero,
            Ga301KVector1.Zero,
            Ga301KVector2.Zero,
            this,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga301Multivector Subtract(Ga301Multivector mv2)
    {
        if (mv2.IsZero()) return Ga301Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga301Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            Subtract(mv2.KVector3),
            mv2.KVector4.Negative()
        );
    }
    
}

using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga22;

public sealed partial class Ga22KVector1
{
    public static Ga22KVector1 Zero { get; } = new Ga22KVector1();
    
    public static Ga22KVector1 E1 { get; } = new Ga22KVector1() { Scalar1 = 1d };
    
    public static Ga22KVector1 E2 { get; } = new Ga22KVector1() { Scalar2 = 1d };
    
    public static Ga22KVector1 E3 { get; } = new Ga22KVector1() { Scalar3 = 1d };
    
    public static Ga22KVector1 E4 { get; } = new Ga22KVector1() { Scalar4 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector1 Create(params double[] scalarArray)
    {
        return new Ga22KVector1
        {
            Scalar1 = scalarArray[0],
            Scalar2 = scalarArray[1],
            Scalar3 = scalarArray[2],
            Scalar4 = scalarArray[3]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector1 operator +(Ga22KVector1 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector1 operator -(Ga22KVector1 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector1 operator *(Ga22KVector1 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector1 operator *(double mv1, Ga22KVector1 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector1 operator /(Ga22KVector1 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(Ga22KVector1 mv1, Ga22KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(Ga22KVector1 mv1, Ga22KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector1 operator +(Ga22KVector1 mv1, Ga22KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector1 operator -(Ga22KVector1 mv1, Ga22KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(Ga22KVector1 mv1, Ga22KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(Ga22KVector1 mv1, Ga22KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(Ga22KVector1 mv1, Ga22KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(Ga22KVector1 mv1, Ga22KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(Ga22KVector1 mv1, Ga22Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(Ga22KVector1 mv1, Ga22Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(Ga22KVector1 mv1, double mv2)
    {
        return Ga22Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(double mv1, Ga22KVector1 mv2)
    {
        return Ga22Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(Ga22KVector1 mv1, double mv2)
    {
        return Ga22Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(double mv1, Ga22KVector1 mv2)
    {
        return Ga22Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar1 { get; init; }
    
    public double Scalar2 { get; init; }
    
    public double Scalar3 { get; init; }
    
    public double Scalar4 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar1) &&
            !double.IsNaN(Scalar2) &&
            !double.IsNaN(Scalar3) &&
            !double.IsNaN(Scalar4);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar1 == 0d &&
            Scalar2 == 0d &&
            Scalar3 == 0d &&
            Scalar4 == 0d;
    
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
        
        scalarArray[1] = Scalar1;
        scalarArray[2] = Scalar2;
        scalarArray[4] = Scalar3;
        scalarArray[8] = Scalar4;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar1,
            Scalar2,
            Scalar3,
            Scalar4
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 Times(double mv2)
    {
        return new Ga22KVector1()
        {
            Scalar1 = Scalar1 * mv2,
            Scalar2 = Scalar2 * mv2,
            Scalar3 = Scalar3 * mv2,
            Scalar4 = Scalar4 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 Negative()
    {
        return new Ga22KVector1()
        {
            Scalar1 = -Scalar1,
            Scalar2 = -Scalar2,
            Scalar3 = -Scalar3,
            Scalar4 = -Scalar4
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 Conjugate()
    {
        return new Ga22KVector1()
               {
                   Scalar1 = -Scalar1,
                   Scalar2 = -Scalar2,
                   Scalar3 = Scalar3,
                   Scalar4 = Scalar4
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector3 Dual()
    {
        return new Ga22KVector3()
               {
                   Scalar123 = -Scalar4,
                   Scalar124 = Scalar3,
                   Scalar134 = Scalar2,
                   Scalar234 = -Scalar1
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector3 UnDual()
    {
        return new Ga22KVector3()
               {
                   Scalar123 = -Scalar4,
                   Scalar124 = Scalar3,
                   Scalar134 = Scalar2,
                   Scalar234 = -Scalar1
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector0 Dual(Ga22KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector0 UnDual(Ga22KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 Dual(Ga22KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 UnDual(Ga22KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector2 Dual(Ga22KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector2 UnDual(Ga22KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector3 Dual(Ga22KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector3 UnDual(Ga22KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Add(Ga22KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2);
        
        return Ga22Multivector.Create(
            mv2,
            this,
            Ga22KVector2.Zero,
            Ga22KVector3.Zero,
            Ga22KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 Add(Ga22KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga22KVector1()
        {
            Scalar1 = Scalar1 + mv2.Scalar1,
            Scalar2 = Scalar2 + mv2.Scalar2,
            Scalar3 = Scalar3 + mv2.Scalar3,
            Scalar4 = Scalar4 + mv2.Scalar4
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Add(Ga22KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2);
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            this,
            mv2,
            Ga22KVector3.Zero,
            Ga22KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Add(Ga22KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2);
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            this,
            Ga22KVector2.Zero,
            mv2,
            Ga22KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Add(Ga22KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2);
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            this,
            Ga22KVector2.Zero,
            Ga22KVector3.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Add(Ga22Multivector mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga22Multivector.Create(
            mv2.KVector0,
            Add(mv2.KVector1),
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Subtract(Ga22KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2.Negative());
        
        return Ga22Multivector.Create(
            mv2.Negative(),
            this,
            Ga22KVector2.Zero,
            Ga22KVector3.Zero,
            Ga22KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector1 Subtract(Ga22KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga22KVector1()
        {
            Scalar1 = Scalar1 - mv2.Scalar1,
            Scalar2 = Scalar2 - mv2.Scalar2,
            Scalar3 = Scalar3 - mv2.Scalar3,
            Scalar4 = Scalar4 - mv2.Scalar4
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Subtract(Ga22KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2.Negative());
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            this,
            mv2.Negative(),
            Ga22KVector3.Zero,
            Ga22KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Subtract(Ga22KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2.Negative());
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            this,
            Ga22KVector2.Zero,
            mv2.Negative(),
            Ga22KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Subtract(Ga22KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2.Negative());
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            this,
            Ga22KVector2.Zero,
            Ga22KVector3.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Subtract(Ga22Multivector mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga22Multivector.Create(
            mv2.KVector0.Negative(),
            Subtract(mv2.KVector1),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative()
        );
    }
    
}

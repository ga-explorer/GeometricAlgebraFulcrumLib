using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga501;

public sealed partial class Ga501KVector1
{
    public static Ga501KVector1 Zero { get; } = new Ga501KVector1();
    
    public static Ga501KVector1 E1 { get; } = new Ga501KVector1() { Scalar1 = 1d };
    
    public static Ga501KVector1 E2 { get; } = new Ga501KVector1() { Scalar2 = 1d };
    
    public static Ga501KVector1 E3 { get; } = new Ga501KVector1() { Scalar3 = 1d };
    
    public static Ga501KVector1 E4 { get; } = new Ga501KVector1() { Scalar4 = 1d };
    
    public static Ga501KVector1 E5 { get; } = new Ga501KVector1() { Scalar5 = 1d };
    
    public static Ga501KVector1 E6 { get; } = new Ga501KVector1() { Scalar6 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501KVector1 Create(params double[] scalarArray)
    {
        return new Ga501KVector1
        {
            Scalar1 = scalarArray[0],
            Scalar2 = scalarArray[1],
            Scalar3 = scalarArray[2],
            Scalar4 = scalarArray[3],
            Scalar5 = scalarArray[4],
            Scalar6 = scalarArray[5]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501KVector1 operator +(Ga501KVector1 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501KVector1 operator -(Ga501KVector1 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501KVector1 operator *(Ga501KVector1 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501KVector1 operator *(double mv1, Ga501KVector1 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501KVector1 operator /(Ga501KVector1 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator +(Ga501KVector1 mv1, Ga501KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator -(Ga501KVector1 mv1, Ga501KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501KVector1 operator +(Ga501KVector1 mv1, Ga501KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501KVector1 operator -(Ga501KVector1 mv1, Ga501KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator +(Ga501KVector1 mv1, Ga501KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator -(Ga501KVector1 mv1, Ga501KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator +(Ga501KVector1 mv1, Ga501KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator -(Ga501KVector1 mv1, Ga501KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator +(Ga501KVector1 mv1, Ga501KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator -(Ga501KVector1 mv1, Ga501KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator +(Ga501KVector1 mv1, Ga501KVector5 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator -(Ga501KVector1 mv1, Ga501KVector5 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator +(Ga501KVector1 mv1, Ga501Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator -(Ga501KVector1 mv1, Ga501Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator +(Ga501KVector1 mv1, double mv2)
    {
        return Ga501Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator +(double mv1, Ga501KVector1 mv2)
    {
        return Ga501Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator -(Ga501KVector1 mv1, double mv2)
    {
        return Ga501Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga501Multivector operator -(double mv1, Ga501KVector1 mv2)
    {
        return Ga501Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar1 { get; init; }
    
    public double Scalar2 { get; init; }
    
    public double Scalar3 { get; init; }
    
    public double Scalar4 { get; init; }
    
    public double Scalar5 { get; init; }
    
    public double Scalar6 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar1) &&
            !double.IsNaN(Scalar2) &&
            !double.IsNaN(Scalar3) &&
            !double.IsNaN(Scalar4) &&
            !double.IsNaN(Scalar5) &&
            !double.IsNaN(Scalar6);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar1 == 0d &&
            Scalar2 == 0d &&
            Scalar3 == 0d &&
            Scalar4 == 0d &&
            Scalar5 == 0d &&
            Scalar6 == 0d;
    
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
        var scalarArray = new double[64];
        
        scalarArray[1] = Scalar1;
        scalarArray[2] = Scalar2;
        scalarArray[4] = Scalar3;
        scalarArray[8] = Scalar4;
        scalarArray[16] = Scalar5;
        scalarArray[32] = Scalar6;
        
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
            Scalar4,
            Scalar5,
            Scalar6
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 Times(double mv2)
    {
        return new Ga501KVector1()
        {
            Scalar1 = Scalar1 * mv2,
            Scalar2 = Scalar2 * mv2,
            Scalar3 = Scalar3 * mv2,
            Scalar4 = Scalar4 * mv2,
            Scalar5 = Scalar5 * mv2,
            Scalar6 = Scalar6 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 Negative()
    {
        return new Ga501KVector1()
        {
            Scalar1 = -Scalar1,
            Scalar2 = -Scalar2,
            Scalar3 = -Scalar3,
            Scalar4 = -Scalar4,
            Scalar5 = -Scalar5,
            Scalar6 = -Scalar6
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 Conjugate()
    {
        return new Ga501KVector1()
               {
                   Scalar2 = Scalar2,
                   Scalar3 = Scalar3,
                   Scalar4 = Scalar4,
                   Scalar5 = Scalar5,
                   Scalar6 = Scalar6
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector5 UnDual()
    {
        return new Ga501KVector5()
               {
                   Scalar12345 = -Scalar6,
                   Scalar12346 = Scalar5,
                   Scalar12356 = -Scalar4,
                   Scalar12456 = Scalar3,
                   Scalar13456 = -Scalar2
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector0 Dual(Ga501KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector0 UnDual(Ga501KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 Dual(Ga501KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 UnDual(Ga501KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector2 Dual(Ga501KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector2 UnDual(Ga501KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector3 Dual(Ga501KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector3 UnDual(Ga501KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector4 Dual(Ga501KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector4 UnDual(Ga501KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector5 Dual(Ga501KVector6 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector5 UnDual(Ga501KVector6 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Add(Ga501KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2);
        
        return Ga501Multivector.Create(
            mv2,
            this,
            Ga501KVector2.Zero,
            Ga501KVector3.Zero,
            Ga501KVector4.Zero,
            Ga501KVector5.Zero,
            Ga501KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 Add(Ga501KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga501KVector1()
        {
            Scalar1 = Scalar1 + mv2.Scalar1,
            Scalar2 = Scalar2 + mv2.Scalar2,
            Scalar3 = Scalar3 + mv2.Scalar3,
            Scalar4 = Scalar4 + mv2.Scalar4,
            Scalar5 = Scalar5 + mv2.Scalar5,
            Scalar6 = Scalar6 + mv2.Scalar6
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Add(Ga501KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2);
        
        return Ga501Multivector.Create(
            Ga501KVector0.Zero,
            this,
            mv2,
            Ga501KVector3.Zero,
            Ga501KVector4.Zero,
            Ga501KVector5.Zero,
            Ga501KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Add(Ga501KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2);
        
        return Ga501Multivector.Create(
            Ga501KVector0.Zero,
            this,
            Ga501KVector2.Zero,
            mv2,
            Ga501KVector4.Zero,
            Ga501KVector5.Zero,
            Ga501KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Add(Ga501KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2);
        
        return Ga501Multivector.Create(
            Ga501KVector0.Zero,
            this,
            Ga501KVector2.Zero,
            Ga501KVector3.Zero,
            mv2,
            Ga501KVector5.Zero,
            Ga501KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Add(Ga501KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2);
        
        return Ga501Multivector.Create(
            Ga501KVector0.Zero,
            this,
            Ga501KVector2.Zero,
            Ga501KVector3.Zero,
            Ga501KVector4.Zero,
            mv2,
            Ga501KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Add(Ga501KVector6 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2);
        
        return Ga501Multivector.Create(
            Ga501KVector0.Zero,
            this,
            Ga501KVector2.Zero,
            Ga501KVector3.Zero,
            Ga501KVector4.Zero,
            Ga501KVector5.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Add(Ga501Multivector mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga501Multivector.Create(
            mv2.KVector0,
            Add(mv2.KVector1),
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4,
            mv2.KVector5,
            mv2.KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Subtract(Ga501KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2.Negative());
        
        return Ga501Multivector.Create(
            mv2.Negative(),
            this,
            Ga501KVector2.Zero,
            Ga501KVector3.Zero,
            Ga501KVector4.Zero,
            Ga501KVector5.Zero,
            Ga501KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501KVector1 Subtract(Ga501KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga501KVector1()
        {
            Scalar1 = Scalar1 - mv2.Scalar1,
            Scalar2 = Scalar2 - mv2.Scalar2,
            Scalar3 = Scalar3 - mv2.Scalar3,
            Scalar4 = Scalar4 - mv2.Scalar4,
            Scalar5 = Scalar5 - mv2.Scalar5,
            Scalar6 = Scalar6 - mv2.Scalar6
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Subtract(Ga501KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2.Negative());
        
        return Ga501Multivector.Create(
            Ga501KVector0.Zero,
            this,
            mv2.Negative(),
            Ga501KVector3.Zero,
            Ga501KVector4.Zero,
            Ga501KVector5.Zero,
            Ga501KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Subtract(Ga501KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2.Negative());
        
        return Ga501Multivector.Create(
            Ga501KVector0.Zero,
            this,
            Ga501KVector2.Zero,
            mv2.Negative(),
            Ga501KVector4.Zero,
            Ga501KVector5.Zero,
            Ga501KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Subtract(Ga501KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2.Negative());
        
        return Ga501Multivector.Create(
            Ga501KVector0.Zero,
            this,
            Ga501KVector2.Zero,
            Ga501KVector3.Zero,
            mv2.Negative(),
            Ga501KVector5.Zero,
            Ga501KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Subtract(Ga501KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2.Negative());
        
        return Ga501Multivector.Create(
            Ga501KVector0.Zero,
            this,
            Ga501KVector2.Zero,
            Ga501KVector3.Zero,
            Ga501KVector4.Zero,
            mv2.Negative(),
            Ga501KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Subtract(Ga501KVector6 mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return Ga501Multivector.Create(mv2.Negative());
        
        return Ga501Multivector.Create(
            Ga501KVector0.Zero,
            this,
            Ga501KVector2.Zero,
            Ga501KVector3.Zero,
            Ga501KVector4.Zero,
            Ga501KVector5.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga501Multivector Subtract(Ga501Multivector mv2)
    {
        if (mv2.IsZero()) return Ga501Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga501Multivector.Create(
            mv2.KVector0.Negative(),
            Subtract(mv2.KVector1),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative(),
            mv2.KVector5.Negative(),
            mv2.KVector6.Negative()
        );
    }
    
}

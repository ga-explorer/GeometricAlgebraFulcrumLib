using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga3;

public sealed partial class Ga3KVector1
{
    public static Ga3KVector1 Zero { get; } = new Ga3KVector1();
    
    public static Ga3KVector1 E1 { get; } = new Ga3KVector1() { Scalar1 = 1d };
    
    public static Ga3KVector1 E2 { get; } = new Ga3KVector1() { Scalar2 = 1d };
    
    public static Ga3KVector1 E3 { get; } = new Ga3KVector1() { Scalar3 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 Create(params double[] scalarArray)
    {
        return new Ga3KVector1
        {
            Scalar1 = scalarArray[0],
            Scalar2 = scalarArray[1],
            Scalar3 = scalarArray[2]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 operator +(Ga3KVector1 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 operator -(Ga3KVector1 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 operator *(Ga3KVector1 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 operator *(double mv1, Ga3KVector1 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 operator /(Ga3KVector1 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector1 mv1, Ga3KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector1 mv1, Ga3KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 operator +(Ga3KVector1 mv1, Ga3KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 operator -(Ga3KVector1 mv1, Ga3KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector1 mv1, Ga3KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector1 mv1, Ga3KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector1 mv1, Ga3Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector1 mv1, Ga3Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector1 mv1, double mv2)
    {
        return Ga3Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(double mv1, Ga3KVector1 mv2)
    {
        return Ga3Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector1 mv1, double mv2)
    {
        return Ga3Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(double mv1, Ga3KVector1 mv2)
    {
        return Ga3Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar1 { get; init; }
    
    public double Scalar2 { get; init; }
    
    public double Scalar3 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar1) &&
            !double.IsNaN(Scalar2) &&
            !double.IsNaN(Scalar3);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar1 == 0d &&
            Scalar2 == 0d &&
            Scalar3 == 0d;
    
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
        
        scalarArray[1] = Scalar1;
        scalarArray[2] = Scalar2;
        scalarArray[4] = Scalar3;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar1,
            Scalar2,
            Scalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 Times(double mv2)
    {
        return new Ga3KVector1()
        {
            Scalar1 = Scalar1 * mv2,
            Scalar2 = Scalar2 * mv2,
            Scalar3 = Scalar3 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 Negative()
    {
        return new Ga3KVector1()
        {
            Scalar1 = -Scalar1,
            Scalar2 = -Scalar2,
            Scalar3 = -Scalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 Conjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 Dual()
    {
        return new Ga3KVector2()
               {
                   Scalar12 = -Scalar3,
                   Scalar13 = Scalar2,
                   Scalar23 = -Scalar1
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 UnDual()
    {
        return new Ga3KVector2()
               {
                   Scalar12 = Scalar3,
                   Scalar13 = -Scalar2,
                   Scalar23 = Scalar1
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector0 Dual(Ga3KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector0 UnDual(Ga3KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 Dual(Ga3KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 UnDual(Ga3KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 Dual(Ga3KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 UnDual(Ga3KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Add(Ga3KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2);
        
        return Ga3Multivector.Create(
            mv2,
            this,
            Ga3KVector2.Zero,
            Ga3KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 Add(Ga3KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga3KVector1()
        {
            Scalar1 = Scalar1 + mv2.Scalar1,
            Scalar2 = Scalar2 + mv2.Scalar2,
            Scalar3 = Scalar3 + mv2.Scalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Add(Ga3KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2);
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            this,
            mv2,
            Ga3KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Add(Ga3KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2);
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            this,
            Ga3KVector2.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Add(Ga3Multivector mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga3Multivector.Create(
            mv2.KVector0,
            Add(mv2.KVector1),
            mv2.KVector2,
            mv2.KVector3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Subtract(Ga3KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2.Negative());
        
        return Ga3Multivector.Create(
            mv2.Negative(),
            this,
            Ga3KVector2.Zero,
            Ga3KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 Subtract(Ga3KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga3KVector1()
        {
            Scalar1 = Scalar1 - mv2.Scalar1,
            Scalar2 = Scalar2 - mv2.Scalar2,
            Scalar3 = Scalar3 - mv2.Scalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Subtract(Ga3KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2.Negative());
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            this,
            mv2.Negative(),
            Ga3KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Subtract(Ga3KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2.Negative());
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            this,
            Ga3KVector2.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Subtract(Ga3Multivector mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga3Multivector.Create(
            mv2.KVector0.Negative(),
            Subtract(mv2.KVector1),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative()
        );
    }
    
}

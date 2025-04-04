using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga3;

public sealed partial class Ga3KVector2
{
    public static Ga3KVector2 Zero { get; } = new Ga3KVector2();
    
    public static Ga3KVector2 E12 { get; } = new Ga3KVector2() { Scalar12 = 1d };
    
    public static Ga3KVector2 E13 { get; } = new Ga3KVector2() { Scalar13 = 1d };
    
    public static Ga3KVector2 E23 { get; } = new Ga3KVector2() { Scalar23 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 Create(params double[] scalarArray)
    {
        return new Ga3KVector2
        {
            Scalar12 = scalarArray[0],
            Scalar13 = scalarArray[1],
            Scalar23 = scalarArray[2]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 operator +(Ga3KVector2 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 operator -(Ga3KVector2 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 operator *(Ga3KVector2 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 operator *(double mv1, Ga3KVector2 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 operator /(Ga3KVector2 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector2 mv1, Ga3KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector2 mv1, Ga3KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector2 mv1, Ga3KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector2 mv1, Ga3KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 operator +(Ga3KVector2 mv1, Ga3KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 operator -(Ga3KVector2 mv1, Ga3KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector2 mv1, Ga3Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector2 mv1, Ga3Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector2 mv1, double mv2)
    {
        return Ga3Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(double mv1, Ga3KVector2 mv2)
    {
        return Ga3Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector2 mv1, double mv2)
    {
        return Ga3Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(double mv1, Ga3KVector2 mv2)
    {
        return Ga3Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar12 { get; init; }
    
    public double Scalar13 { get; init; }
    
    public double Scalar23 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar12) &&
            !double.IsNaN(Scalar13) &&
            !double.IsNaN(Scalar23);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar12 == 0d &&
            Scalar13 == 0d &&
            Scalar23 == 0d;
    
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
        
        scalarArray[3] = Scalar12;
        scalarArray[5] = Scalar13;
        scalarArray[6] = Scalar23;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar12,
            Scalar13,
            Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 Times(double mv2)
    {
        return new Ga3KVector2()
        {
            Scalar12 = Scalar12 * mv2,
            Scalar13 = Scalar13 * mv2,
            Scalar23 = Scalar23 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 Negative()
    {
        return new Ga3KVector2()
        {
            Scalar12 = -Scalar12,
            Scalar13 = -Scalar13,
            Scalar23 = -Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 Conjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 Dual()
    {
        return new Ga3KVector1()
               {
                   Scalar1 = Scalar23,
                   Scalar2 = -Scalar13,
                   Scalar3 = Scalar12
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 UnDual()
    {
        return new Ga3KVector1()
               {
                   Scalar1 = -Scalar23,
                   Scalar2 = Scalar13,
                   Scalar3 = -Scalar12
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector0 Dual(Ga3KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector0 UnDual(Ga3KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 Dual(Ga3KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector1 UnDual(Ga3KVector3 kv2)
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
            Ga3KVector1.Zero,
            this,
            Ga3KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Add(Ga3KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2);
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            mv2,
            this,
            Ga3KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 Add(Ga3KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga3KVector2()
        {
            Scalar12 = Scalar12 + mv2.Scalar12,
            Scalar13 = Scalar13 + mv2.Scalar13,
            Scalar23 = Scalar23 + mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Add(Ga3KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2);
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            Ga3KVector1.Zero,
            this,
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
            mv2.KVector1,
            Add(mv2.KVector2),
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
            Ga3KVector1.Zero,
            this,
            Ga3KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Subtract(Ga3KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2.Negative());
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            mv2.Negative(),
            this,
            Ga3KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector2 Subtract(Ga3KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga3KVector2()
        {
            Scalar12 = Scalar12 - mv2.Scalar12,
            Scalar13 = Scalar13 - mv2.Scalar13,
            Scalar23 = Scalar23 - mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Subtract(Ga3KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2.Negative());
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            Ga3KVector1.Zero,
            this,
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
            mv2.KVector1.Negative(),
            Subtract(mv2.KVector2),
            mv2.KVector3.Negative()
        );
    }
    
}

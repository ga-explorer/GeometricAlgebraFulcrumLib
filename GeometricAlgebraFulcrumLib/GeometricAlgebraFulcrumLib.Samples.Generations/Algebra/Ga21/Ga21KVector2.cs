using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga21;

public sealed partial class Ga21KVector2
{
    public static Ga21KVector2 Zero { get; } = new Ga21KVector2();
    
    public static Ga21KVector2 E12 { get; } = new Ga21KVector2() { Scalar12 = 1d };
    
    public static Ga21KVector2 E13 { get; } = new Ga21KVector2() { Scalar13 = 1d };
    
    public static Ga21KVector2 E23 { get; } = new Ga21KVector2() { Scalar23 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 Create(params double[] scalarArray)
    {
        return new Ga21KVector2
        {
            Scalar12 = scalarArray[0],
            Scalar13 = scalarArray[1],
            Scalar23 = scalarArray[2]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 operator +(Ga21KVector2 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 operator -(Ga21KVector2 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 operator *(Ga21KVector2 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 operator *(double mv1, Ga21KVector2 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 operator /(Ga21KVector2 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(Ga21KVector2 mv1, Ga21KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(Ga21KVector2 mv1, Ga21KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(Ga21KVector2 mv1, Ga21KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(Ga21KVector2 mv1, Ga21KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 operator +(Ga21KVector2 mv1, Ga21KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 operator -(Ga21KVector2 mv1, Ga21KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(Ga21KVector2 mv1, Ga21Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(Ga21KVector2 mv1, Ga21Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(Ga21KVector2 mv1, double mv2)
    {
        return Ga21Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(double mv1, Ga21KVector2 mv2)
    {
        return Ga21Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(Ga21KVector2 mv1, double mv2)
    {
        return Ga21Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(double mv1, Ga21KVector2 mv2)
    {
        return Ga21Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar12 { get; init; }
    
    public double Scalar13 { get; init; }
    
    public double Scalar23 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2()
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
    public Ga21KVector2 Times(double mv2)
    {
        return new Ga21KVector2()
        {
            Scalar12 = Scalar12 * mv2,
            Scalar13 = Scalar13 * mv2,
            Scalar23 = Scalar23 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 Negative()
    {
        return new Ga21KVector2()
        {
            Scalar12 = -Scalar12,
            Scalar13 = -Scalar13,
            Scalar23 = -Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 Conjugate()
    {
        return new Ga21KVector2()
               {
                   Scalar12 = Scalar12,
                   Scalar13 = Scalar13,
                   Scalar23 = -Scalar23
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector1 Dual()
    {
        return new Ga21KVector1()
               {
                   Scalar1 = -Scalar23,
                   Scalar2 = -Scalar13,
                   Scalar3 = Scalar12
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector1 UnDual()
    {
        return new Ga21KVector1()
               {
                   Scalar1 = -Scalar23,
                   Scalar2 = -Scalar13,
                   Scalar3 = Scalar12
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector0 Dual(Ga21KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector0 UnDual(Ga21KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector1 Dual(Ga21KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector1 UnDual(Ga21KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Add(Ga21KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga21Multivector.Create(this);
        if (IsZero()) return Ga21Multivector.Create(mv2);
        
        return Ga21Multivector.Create(
            mv2,
            Ga21KVector1.Zero,
            this,
            Ga21KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Add(Ga21KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga21Multivector.Create(this);
        if (IsZero()) return Ga21Multivector.Create(mv2);
        
        return Ga21Multivector.Create(
            Ga21KVector0.Zero,
            mv2,
            this,
            Ga21KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 Add(Ga21KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga21KVector2()
        {
            Scalar12 = Scalar12 + mv2.Scalar12,
            Scalar13 = Scalar13 + mv2.Scalar13,
            Scalar23 = Scalar23 + mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Add(Ga21KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga21Multivector.Create(this);
        if (IsZero()) return Ga21Multivector.Create(mv2);
        
        return Ga21Multivector.Create(
            Ga21KVector0.Zero,
            Ga21KVector1.Zero,
            this,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Add(Ga21Multivector mv2)
    {
        if (mv2.IsZero()) return Ga21Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga21Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            Add(mv2.KVector2),
            mv2.KVector3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Subtract(Ga21KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga21Multivector.Create(this);
        if (IsZero()) return Ga21Multivector.Create(mv2.Negative());
        
        return Ga21Multivector.Create(
            mv2.Negative(),
            Ga21KVector1.Zero,
            this,
            Ga21KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Subtract(Ga21KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga21Multivector.Create(this);
        if (IsZero()) return Ga21Multivector.Create(mv2.Negative());
        
        return Ga21Multivector.Create(
            Ga21KVector0.Zero,
            mv2.Negative(),
            this,
            Ga21KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector2 Subtract(Ga21KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga21KVector2()
        {
            Scalar12 = Scalar12 - mv2.Scalar12,
            Scalar13 = Scalar13 - mv2.Scalar13,
            Scalar23 = Scalar23 - mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Subtract(Ga21KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga21Multivector.Create(this);
        if (IsZero()) return Ga21Multivector.Create(mv2.Negative());
        
        return Ga21Multivector.Create(
            Ga21KVector0.Zero,
            Ga21KVector1.Zero,
            this,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Subtract(Ga21Multivector mv2)
    {
        if (mv2.IsZero()) return Ga21Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga21Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            Subtract(mv2.KVector2),
            mv2.KVector3.Negative()
        );
    }
    
}

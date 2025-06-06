using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga2;

public sealed partial class Ga2KVector1
{
    public static Ga2KVector1 Zero { get; } = new Ga2KVector1();
    
    public static Ga2KVector1 E1 { get; } = new Ga2KVector1() { Scalar1 = 1d };
    
    public static Ga2KVector1 E2 { get; } = new Ga2KVector1() { Scalar2 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 Create(params double[] scalarArray)
    {
        return new Ga2KVector1
        {
            Scalar1 = scalarArray[0],
            Scalar2 = scalarArray[1]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 operator +(Ga2KVector1 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 operator -(Ga2KVector1 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 operator *(Ga2KVector1 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 operator *(double mv1, Ga2KVector1 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 operator /(Ga2KVector1 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2KVector1 mv1, Ga2KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2KVector1 mv1, Ga2KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 operator +(Ga2KVector1 mv1, Ga2KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 operator -(Ga2KVector1 mv1, Ga2KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2KVector1 mv1, Ga2Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2KVector1 mv1, Ga2Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2KVector1 mv1, double mv2)
    {
        return Ga2Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(double mv1, Ga2KVector1 mv2)
    {
        return Ga2Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2KVector1 mv1, double mv2)
    {
        return Ga2Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(double mv1, Ga2KVector1 mv2)
    {
        return Ga2Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar1 { get; init; }
    
    public double Scalar2 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar1) &&
            !double.IsNaN(Scalar2);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar1 == 0d &&
            Scalar2 == 0d;
    
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
        var scalarArray = new double[4];
        
        scalarArray[1] = Scalar1;
        scalarArray[2] = Scalar2;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar1,
            Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 Times(double mv2)
    {
        return new Ga2KVector1()
        {
            Scalar1 = Scalar1 * mv2,
            Scalar2 = Scalar2 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 Negative()
    {
        return new Ga2KVector1()
        {
            Scalar1 = -Scalar1,
            Scalar2 = -Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 Conjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 Dual()
    {
        return new Ga2KVector1()
               {
                   Scalar1 = Scalar2,
                   Scalar2 = -Scalar1
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 UnDual()
    {
        return new Ga2KVector1()
               {
                   Scalar1 = -Scalar2,
                   Scalar2 = Scalar1
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector0 Dual(Ga2KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector0 UnDual(Ga2KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 Dual(Ga2KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 UnDual(Ga2KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Add(Ga2KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga2Multivector.Create(this);
        if (IsZero()) return Ga2Multivector.Create(mv2);
        
        return Ga2Multivector.Create(
            mv2,
            this,
            Ga2KVector2.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 Add(Ga2KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga2KVector1()
        {
            Scalar1 = Scalar1 + mv2.Scalar1,
            Scalar2 = Scalar2 + mv2.Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Add(Ga2KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga2Multivector.Create(this);
        if (IsZero()) return Ga2Multivector.Create(mv2);
        
        return Ga2Multivector.Create(
            Ga2KVector0.Zero,
            this,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Add(Ga2Multivector mv2)
    {
        if (mv2.IsZero()) return Ga2Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga2Multivector.Create(
            mv2.KVector0,
            Add(mv2.KVector1),
            mv2.KVector2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Subtract(Ga2KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga2Multivector.Create(this);
        if (IsZero()) return Ga2Multivector.Create(mv2.Negative());
        
        return Ga2Multivector.Create(
            mv2.Negative(),
            this,
            Ga2KVector2.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector1 Subtract(Ga2KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga2KVector1()
        {
            Scalar1 = Scalar1 - mv2.Scalar1,
            Scalar2 = Scalar2 - mv2.Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Subtract(Ga2KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga2Multivector.Create(this);
        if (IsZero()) return Ga2Multivector.Create(mv2.Negative());
        
        return Ga2Multivector.Create(
            Ga2KVector0.Zero,
            this,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Subtract(Ga2Multivector mv2)
    {
        if (mv2.IsZero()) return Ga2Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga2Multivector.Create(
            mv2.KVector0.Negative(),
            Subtract(mv2.KVector1),
            mv2.KVector2.Negative()
        );
    }
    
}

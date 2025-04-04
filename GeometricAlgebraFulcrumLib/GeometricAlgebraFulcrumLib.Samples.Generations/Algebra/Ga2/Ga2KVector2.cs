using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga2;

public sealed partial class Ga2KVector2
{
    public static Ga2KVector2 Zero { get; } = new Ga2KVector2();
    
    public static Ga2KVector2 E12 { get; } = new Ga2KVector2() { Scalar12 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 Create(params double[] scalarArray)
    {
        return new Ga2KVector2
        {
            Scalar12 = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 operator +(Ga2KVector2 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 operator -(Ga2KVector2 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 operator *(Ga2KVector2 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 operator *(double mv1, Ga2KVector2 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 operator /(Ga2KVector2 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2KVector2 mv1, Ga2KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2KVector2 mv1, Ga2KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2KVector2 mv1, Ga2KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2KVector2 mv1, Ga2KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2KVector2 mv1, Ga2Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2KVector2 mv1, Ga2Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2KVector2 mv1, double mv2)
    {
        return Ga2Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(double mv1, Ga2KVector2 mv2)
    {
        return Ga2Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2KVector2 mv1, double mv2)
    {
        return Ga2Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(double mv1, Ga2KVector2 mv2)
    {
        return Ga2Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar12 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2(double scalar)
    {
        Scalar12 = scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar12);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar12 == 0d;
    
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
        
        scalarArray[3] = Scalar12;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 Times(double mv2)
    {
        return new Ga2KVector2()
        {
            Scalar12 = Scalar12 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 Negative()
    {
        return new Ga2KVector2()
        {
            Scalar12 = -Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 Conjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector0 Dual()
    {
        return new Ga2KVector0()
               {
                   Scalar = Scalar12
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector0 UnDual()
    {
        return new Ga2KVector0()
               {
                   Scalar = -Scalar12
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector0 Dual(Ga2KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector0 UnDual(Ga2KVector2 kv2)
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
            Ga2KVector1.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Add(Ga2KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga2Multivector.Create(this);
        if (IsZero()) return Ga2Multivector.Create(mv2);
        
        return Ga2Multivector.Create(
            Ga2KVector0.Zero,
            mv2,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 Add(Ga2KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga2KVector2()
        {
            Scalar12 = Scalar12 + mv2.Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Add(Ga2Multivector mv2)
    {
        if (mv2.IsZero()) return Ga2Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga2Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            Add(mv2.KVector2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Subtract(Ga2KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga2Multivector.Create(this);
        if (IsZero()) return Ga2Multivector.Create(mv2.Negative());
        
        return Ga2Multivector.Create(
            mv2.Negative(),
            Ga2KVector1.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Subtract(Ga2KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga2Multivector.Create(this);
        if (IsZero()) return Ga2Multivector.Create(mv2.Negative());
        
        return Ga2Multivector.Create(
            Ga2KVector0.Zero,
            mv2.Negative(),
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector2 Subtract(Ga2KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga2KVector2()
        {
            Scalar12 = Scalar12 - mv2.Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Subtract(Ga2Multivector mv2)
    {
        if (mv2.IsZero()) return Ga2Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga2Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            Subtract(mv2.KVector2)
        );
    }
    
}

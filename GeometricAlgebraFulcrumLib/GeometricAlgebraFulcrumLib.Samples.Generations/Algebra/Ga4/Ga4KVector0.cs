using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga4;

public sealed partial class Ga4KVector0
{
    public static Ga4KVector0 Zero { get; } = new Ga4KVector0();
    
    public static Ga4KVector0 E { get; } = new Ga4KVector0() { Scalar = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 Create(params double[] scalarArray)
    {
        return new Ga4KVector0
        {
            Scalar = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 operator +(Ga4KVector0 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 operator -(Ga4KVector0 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 operator *(Ga4KVector0 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 operator *(double mv1, Ga4KVector0 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 operator /(Ga4KVector0 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 operator +(Ga4KVector0 mv1, Ga4KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 operator -(Ga4KVector0 mv1, Ga4KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector0 mv1, Ga4KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector0 mv1, Ga4KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector0 mv1, Ga4KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector0 mv1, Ga4KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector0 mv1, Ga4KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector0 mv1, Ga4KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector0 mv1, Ga4Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector0 mv1, Ga4Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 operator +(Ga4KVector0 mv1, double mv2)
    {
        return new Ga4KVector0(mv1.Scalar + mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 operator +(double mv1, Ga4KVector0 mv2)
    {
        return new Ga4KVector0(mv1 + mv2.Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 operator -(Ga4KVector0 mv1, double mv2)
    {
        return new Ga4KVector0(mv1.Scalar - mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 operator -(double mv1, Ga4KVector0 mv2)
    {
        return new Ga4KVector0(mv1 - mv2.Scalar);
    }
    
    public double Scalar { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0(double scalar)
    {
        Scalar = scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar == 0d;
    
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
        
        scalarArray[0] = Scalar;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Times(double mv2)
    {
        return new Ga4KVector0()
        {
            Scalar = Scalar * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Negative()
    {
        return new Ga4KVector0()
        {
            Scalar = -Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Conjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 Dual()
    {
        return new Ga4KVector4()
               {
                   Scalar1234 = Scalar
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 UnDual()
    {
        return new Ga4KVector4()
               {
                   Scalar1234 = Scalar
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Dual(Ga4KVector0 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 UnDual(Ga4KVector0 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector1 Dual(Ga4KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector1 UnDual(Ga4KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector2 Dual(Ga4KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector2 UnDual(Ga4KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 Dual(Ga4KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector3 UnDual(Ga4KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 Dual(Ga4KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 UnDual(Ga4KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Add(Ga4KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga4KVector0()
        {
            Scalar = Scalar + mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            this,
            mv2,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            this,
            Ga4KVector1.Zero,
            mv2,
            Ga4KVector3.Zero,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            this,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            mv2,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            this,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4Multivector mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga4Multivector.Create(
            Add(mv2.KVector0),
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Subtract(Ga4KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga4KVector0()
        {
            Scalar = Scalar - mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            this,
            mv2.Negative(),
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            this,
            Ga4KVector1.Zero,
            mv2.Negative(),
            Ga4KVector3.Zero,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            this,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            mv2.Negative(),
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            this,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4Multivector mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga4Multivector.Create(
            Subtract(mv2.KVector0),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative()
        );
    }
    
}

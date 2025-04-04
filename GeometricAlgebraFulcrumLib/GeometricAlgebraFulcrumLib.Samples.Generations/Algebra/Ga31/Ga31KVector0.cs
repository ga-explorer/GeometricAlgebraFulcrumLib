using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga31;

public sealed partial class Ga31KVector0
{
    public static Ga31KVector0 Zero { get; } = new Ga31KVector0();
    
    public static Ga31KVector0 E { get; } = new Ga31KVector0() { Scalar = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Create(params double[] scalarArray)
    {
        return new Ga31KVector0
        {
            Scalar = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 operator +(Ga31KVector0 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 operator -(Ga31KVector0 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 operator *(Ga31KVector0 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 operator *(double mv1, Ga31KVector0 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 operator /(Ga31KVector0 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 operator +(Ga31KVector0 mv1, Ga31KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 operator -(Ga31KVector0 mv1, Ga31KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31KVector0 mv1, Ga31KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31KVector0 mv1, Ga31KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31KVector0 mv1, Ga31KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31KVector0 mv1, Ga31KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31KVector0 mv1, Ga31KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31KVector0 mv1, Ga31KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31KVector0 mv1, Ga31Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31KVector0 mv1, Ga31Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 operator +(Ga31KVector0 mv1, double mv2)
    {
        return new Ga31KVector0(mv1.Scalar + mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 operator +(double mv1, Ga31KVector0 mv2)
    {
        return new Ga31KVector0(mv1 + mv2.Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 operator -(Ga31KVector0 mv1, double mv2)
    {
        return new Ga31KVector0(mv1.Scalar - mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 operator -(double mv1, Ga31KVector0 mv2)
    {
        return new Ga31KVector0(mv1 - mv2.Scalar);
    }
    
    public double Scalar { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0(double scalar)
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
    public Ga31KVector0 Times(double mv2)
    {
        return new Ga31KVector0()
        {
            Scalar = Scalar * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 Negative()
    {
        return new Ga31KVector0()
        {
            Scalar = -Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 Conjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 Dual()
    {
        return new Ga31KVector4()
               {
                   Scalar1234 = -Scalar
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 UnDual()
    {
        return new Ga31KVector4()
               {
                   Scalar1234 = Scalar
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 Dual(Ga31KVector0 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 UnDual(Ga31KVector0 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector1 Dual(Ga31KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector1 UnDual(Ga31KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector2 Dual(Ga31KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector2 UnDual(Ga31KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector3 Dual(Ga31KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector3 UnDual(Ga31KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 Dual(Ga31KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 UnDual(Ga31KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 Add(Ga31KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga31KVector0()
        {
            Scalar = Scalar + mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Add(Ga31KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2);
        
        return Ga31Multivector.Create(
            this,
            mv2,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Add(Ga31KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2);
        
        return Ga31Multivector.Create(
            this,
            Ga31KVector1.Zero,
            mv2,
            Ga31KVector3.Zero,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Add(Ga31KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2);
        
        return Ga31Multivector.Create(
            this,
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            mv2,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Add(Ga31KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2);
        
        return Ga31Multivector.Create(
            this,
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Add(Ga31Multivector mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga31Multivector.Create(
            Add(mv2.KVector0),
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 Subtract(Ga31KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga31KVector0()
        {
            Scalar = Scalar - mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Subtract(Ga31KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2.Negative());
        
        return Ga31Multivector.Create(
            this,
            mv2.Negative(),
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Subtract(Ga31KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2.Negative());
        
        return Ga31Multivector.Create(
            this,
            Ga31KVector1.Zero,
            mv2.Negative(),
            Ga31KVector3.Zero,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Subtract(Ga31KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2.Negative());
        
        return Ga31Multivector.Create(
            this,
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            mv2.Negative(),
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Subtract(Ga31KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2.Negative());
        
        return Ga31Multivector.Create(
            this,
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Subtract(Ga31Multivector mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga31Multivector.Create(
            Subtract(mv2.KVector0),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative()
        );
    }
    
}

using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga41;

public sealed partial class Ga41KVector0
{
    public static Ga41KVector0 Zero { get; } = new Ga41KVector0();
    
    public static Ga41KVector0 E { get; } = new Ga41KVector0() { Scalar = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Create(params double[] scalarArray)
    {
        return new Ga41KVector0
        {
            Scalar = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 operator +(Ga41KVector0 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 operator -(Ga41KVector0 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 operator *(Ga41KVector0 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 operator *(double mv1, Ga41KVector0 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 operator /(Ga41KVector0 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 operator +(Ga41KVector0 mv1, Ga41KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 operator -(Ga41KVector0 mv1, Ga41KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector0 mv1, Ga41KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector0 mv1, Ga41KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector0 mv1, Ga41KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector0 mv1, Ga41KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector0 mv1, Ga41KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector0 mv1, Ga41KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector0 mv1, Ga41KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector0 mv1, Ga41KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector0 mv1, Ga41Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector0 mv1, Ga41Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 operator +(Ga41KVector0 mv1, double mv2)
    {
        return new Ga41KVector0(mv1.Scalar + mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 operator +(double mv1, Ga41KVector0 mv2)
    {
        return new Ga41KVector0(mv1 + mv2.Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 operator -(Ga41KVector0 mv1, double mv2)
    {
        return new Ga41KVector0(mv1.Scalar - mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 operator -(double mv1, Ga41KVector0 mv2)
    {
        return new Ga41KVector0(mv1 - mv2.Scalar);
    }
    
    public double Scalar { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0(double scalar)
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
        var scalarArray = new double[32];
        
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
    public Ga41KVector0 Times(double mv2)
    {
        return new Ga41KVector0()
        {
            Scalar = Scalar * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Negative()
    {
        return new Ga41KVector0()
        {
            Scalar = -Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Conjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 Dual()
    {
        return new Ga41KVector5()
               {
                   Scalar12345 = -Scalar
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 UnDual()
    {
        return new Ga41KVector5()
               {
                   Scalar12345 = Scalar
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Dual(Ga41KVector0 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 UnDual(Ga41KVector0 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 Dual(Ga41KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 UnDual(Ga41KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector2 Dual(Ga41KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector2 UnDual(Ga41KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector3 Dual(Ga41KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector3 UnDual(Ga41KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector4 Dual(Ga41KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector4 UnDual(Ga41KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 Dual(Ga41KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 UnDual(Ga41KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Add(Ga41KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga41KVector0()
        {
            Scalar = Scalar + mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            this,
            mv2,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            this,
            Ga41KVector1.Zero,
            mv2,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            this,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            mv2,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            this,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            mv2,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            this,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41Multivector mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga41Multivector.Create(
            Add(mv2.KVector0),
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4,
            mv2.KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Subtract(Ga41KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga41KVector0()
        {
            Scalar = Scalar - mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            this,
            mv2.Negative(),
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            this,
            Ga41KVector1.Zero,
            mv2.Negative(),
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            this,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            mv2.Negative(),
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            this,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            mv2.Negative(),
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            this,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41Multivector mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga41Multivector.Create(
            Subtract(mv2.KVector0),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative(),
            mv2.KVector5.Negative()
        );
    }
    
}

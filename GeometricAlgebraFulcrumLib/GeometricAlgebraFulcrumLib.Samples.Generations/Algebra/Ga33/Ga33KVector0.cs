using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga33;

public sealed partial class Ga33KVector0
{
    public static Ga33KVector0 Zero { get; } = new Ga33KVector0();
    
    public static Ga33KVector0 E { get; } = new Ga33KVector0() { Scalar = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 Create(params double[] scalarArray)
    {
        return new Ga33KVector0
        {
            Scalar = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 operator +(Ga33KVector0 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 operator -(Ga33KVector0 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 operator *(Ga33KVector0 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 operator *(double mv1, Ga33KVector0 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 operator /(Ga33KVector0 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 operator +(Ga33KVector0 mv1, Ga33KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 operator -(Ga33KVector0 mv1, Ga33KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector0 mv1, Ga33KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector0 mv1, Ga33KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector0 mv1, Ga33KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector0 mv1, Ga33KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector0 mv1, Ga33KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector0 mv1, Ga33KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector0 mv1, Ga33KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector0 mv1, Ga33KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector0 mv1, Ga33KVector5 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector0 mv1, Ga33KVector5 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector0 mv1, Ga33Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector0 mv1, Ga33Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 operator +(Ga33KVector0 mv1, double mv2)
    {
        return new Ga33KVector0(mv1.Scalar + mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 operator +(double mv1, Ga33KVector0 mv2)
    {
        return new Ga33KVector0(mv1 + mv2.Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 operator -(Ga33KVector0 mv1, double mv2)
    {
        return new Ga33KVector0(mv1.Scalar - mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector0 operator -(double mv1, Ga33KVector0 mv2)
    {
        return new Ga33KVector0(mv1 - mv2.Scalar);
    }
    
    public double Scalar { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0(double scalar)
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
        var scalarArray = new double[64];
        
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
    public Ga33KVector0 Times(double mv2)
    {
        return new Ga33KVector0()
        {
            Scalar = Scalar * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 Negative()
    {
        return new Ga33KVector0()
        {
            Scalar = -Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 Conjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 Dual()
    {
        return new Ga33KVector6()
               {
                   Scalar123456 = Scalar
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 UnDual()
    {
        return new Ga33KVector6()
               {
                   Scalar123456 = Scalar
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 Dual(Ga33KVector0 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 UnDual(Ga33KVector0 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector1 Dual(Ga33KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector1 UnDual(Ga33KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector2 Dual(Ga33KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector2 UnDual(Ga33KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector3 Dual(Ga33KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector3 UnDual(Ga33KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector4 Dual(Ga33KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector4 UnDual(Ga33KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 Dual(Ga33KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 UnDual(Ga33KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 Dual(Ga33KVector6 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 UnDual(Ga33KVector6 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 Add(Ga33KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga33KVector0()
        {
            Scalar = Scalar + mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            this,
            mv2,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            this,
            Ga33KVector1.Zero,
            mv2,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            this,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            mv2,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            this,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            mv2,
            Ga33KVector5.Zero,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            this,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            mv2,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector6 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            this,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33Multivector mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga33Multivector.Create(
            Add(mv2.KVector0),
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4,
            mv2.KVector5,
            mv2.KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 Subtract(Ga33KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga33KVector0()
        {
            Scalar = Scalar - mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            this,
            mv2.Negative(),
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            this,
            Ga33KVector1.Zero,
            mv2.Negative(),
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            this,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            mv2.Negative(),
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            this,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            mv2.Negative(),
            Ga33KVector5.Zero,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            this,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            mv2.Negative(),
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector6 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            this,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33Multivector mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga33Multivector.Create(
            Subtract(mv2.KVector0),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative(),
            mv2.KVector5.Negative(),
            mv2.KVector6.Negative()
        );
    }
    
}

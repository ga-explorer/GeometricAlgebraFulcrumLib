using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga33;

public sealed partial class Ga33KVector6
{
    public static Ga33KVector6 Zero { get; } = new Ga33KVector6();
    
    public static Ga33KVector6 E123456 { get; } = new Ga33KVector6() { Scalar123456 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector6 Create(params double[] scalarArray)
    {
        return new Ga33KVector6
        {
            Scalar123456 = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector6 operator +(Ga33KVector6 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector6 operator -(Ga33KVector6 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector6 operator *(Ga33KVector6 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector6 operator *(double mv1, Ga33KVector6 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector6 operator /(Ga33KVector6 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector6 mv1, Ga33KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector6 mv1, Ga33KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector6 mv1, Ga33KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector6 mv1, Ga33KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector6 mv1, Ga33KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector6 mv1, Ga33KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector6 mv1, Ga33KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector6 mv1, Ga33KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector6 mv1, Ga33KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector6 mv1, Ga33KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector6 mv1, Ga33KVector5 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector6 mv1, Ga33KVector5 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector6 mv1, Ga33Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector6 mv1, Ga33Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector6 mv1, double mv2)
    {
        return Ga33Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(double mv1, Ga33KVector6 mv2)
    {
        return Ga33Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector6 mv1, double mv2)
    {
        return Ga33Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(double mv1, Ga33KVector6 mv2)
    {
        return Ga33Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar123456 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6(double scalar)
    {
        Scalar123456 = scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar123456);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar123456 == 0d;
    
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
        
        scalarArray[63] = Scalar123456;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar123456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 Times(double mv2)
    {
        return new Ga33KVector6()
        {
            Scalar123456 = Scalar123456 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 Negative()
    {
        return new Ga33KVector6()
        {
            Scalar123456 = -Scalar123456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 Conjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 Dual()
    {
        return new Ga33KVector0()
               {
                   Scalar = Scalar123456
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 UnDual()
    {
        return new Ga33KVector0()
               {
                   Scalar = Scalar123456
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 Dual(Ga33KVector6 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 UnDual(Ga33KVector6 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            mv2,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            mv2,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            mv2,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            mv2,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            mv2,
            Ga33KVector5.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            mv2,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 Add(Ga33KVector6 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga33KVector6()
        {
            Scalar123456 = Scalar123456 + mv2.Scalar123456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33Multivector mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga33Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4,
            mv2.KVector5,
            Add(mv2.KVector6)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            mv2.Negative(),
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            mv2.Negative(),
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            mv2.Negative(),
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            mv2.Negative(),
            Ga33KVector4.Zero,
            Ga33KVector5.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            mv2.Negative(),
            Ga33KVector5.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            mv2.Negative(),
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector6 Subtract(Ga33KVector6 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga33KVector6()
        {
            Scalar123456 = Scalar123456 - mv2.Scalar123456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33Multivector mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga33Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative(),
            mv2.KVector5.Negative(),
            Subtract(mv2.KVector6)
        );
    }
    
}

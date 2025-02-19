using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga401;

public sealed partial class Ga401KVector5
{
    public static Ga401KVector5 Zero { get; } = new Ga401KVector5();
    
    public static Ga401KVector5 E12345 { get; } = new Ga401KVector5() { Scalar12345 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 Create(params double[] scalarArray)
    {
        return new Ga401KVector5
        {
            Scalar12345 = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 operator +(Ga401KVector5 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 operator -(Ga401KVector5 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 operator *(Ga401KVector5 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 operator *(double mv1, Ga401KVector5 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 operator /(Ga401KVector5 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector5 mv1, Ga401KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector5 mv1, Ga401KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector5 mv1, Ga401KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector5 mv1, Ga401KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector5 mv1, Ga401KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector5 mv1, Ga401KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector5 mv1, Ga401KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector5 mv1, Ga401KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector5 mv1, Ga401KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector5 mv1, Ga401KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector5 mv1, Ga401Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector5 mv1, Ga401Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector5 mv1, double mv2)
    {
        return Ga401Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(double mv1, Ga401KVector5 mv2)
    {
        return Ga401Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector5 mv1, double mv2)
    {
        return Ga401Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(double mv1, Ga401KVector5 mv2)
    {
        return Ga401Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar12345 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5(double scalar)
    {
        Scalar12345 = scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar12345);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar12345 == 0d;
    
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
        
        scalarArray[31] = Scalar12345;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 Times(double mv2)
    {
        return new Ga401KVector5()
        {
            Scalar12345 = Scalar12345 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 Negative()
    {
        return new Ga401KVector5()
        {
            Scalar12345 = -Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 Conjugate()
    {
        return Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector0 UnDual()
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector0 Dual(Ga401KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector0 UnDual(Ga401KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2);
        
        return Ga401Multivector.Create(
            mv2,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            Ga401KVector3.Zero,
            Ga401KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2);
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            mv2,
            Ga401KVector2.Zero,
            Ga401KVector3.Zero,
            Ga401KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2);
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            mv2,
            Ga401KVector3.Zero,
            Ga401KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2);
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            mv2,
            Ga401KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2);
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            Ga401KVector3.Zero,
            mv2,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 Add(Ga401KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga401KVector5()
        {
            Scalar12345 = Scalar12345 + mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401Multivector mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga401Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4,
            Add(mv2.KVector5)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2.Negative());
        
        return Ga401Multivector.Create(
            mv2.Negative(),
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            Ga401KVector3.Zero,
            Ga401KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2.Negative());
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            mv2.Negative(),
            Ga401KVector2.Zero,
            Ga401KVector3.Zero,
            Ga401KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2.Negative());
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            mv2.Negative(),
            Ga401KVector3.Zero,
            Ga401KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2.Negative());
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            mv2.Negative(),
            Ga401KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2.Negative());
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            Ga401KVector3.Zero,
            mv2.Negative(),
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector5 Subtract(Ga401KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga401KVector5()
        {
            Scalar12345 = Scalar12345 - mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401Multivector mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga401Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative(),
            Subtract(mv2.KVector5)
        );
    }
    
}

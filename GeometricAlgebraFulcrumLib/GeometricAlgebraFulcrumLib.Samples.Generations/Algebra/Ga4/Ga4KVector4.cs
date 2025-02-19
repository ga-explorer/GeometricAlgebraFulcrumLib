using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga4;

public sealed partial class Ga4KVector4
{
    public static Ga4KVector4 Zero { get; } = new Ga4KVector4();
    
    public static Ga4KVector4 E1234 { get; } = new Ga4KVector4() { Scalar1234 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector4 Create(params double[] scalarArray)
    {
        return new Ga4KVector4
        {
            Scalar1234 = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector4 operator +(Ga4KVector4 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector4 operator -(Ga4KVector4 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector4 operator *(Ga4KVector4 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector4 operator *(double mv1, Ga4KVector4 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector4 operator /(Ga4KVector4 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector4 mv1, Ga4KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector4 mv1, Ga4KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector4 mv1, Ga4KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector4 mv1, Ga4KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector4 mv1, Ga4KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector4 mv1, Ga4KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector4 mv1, Ga4KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector4 mv1, Ga4KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector4 mv1, Ga4Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector4 mv1, Ga4Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4KVector4 mv1, double mv2)
    {
        return Ga4Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(double mv1, Ga4KVector4 mv2)
    {
        return Ga4Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4KVector4 mv1, double mv2)
    {
        return Ga4Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(double mv1, Ga4KVector4 mv2)
    {
        return Ga4Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar1234 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4(double scalar)
    {
        Scalar1234 = scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar1234);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar1234 == 0d;
    
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
        
        scalarArray[15] = Scalar1234;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 Times(double mv2)
    {
        return new Ga4KVector4()
        {
            Scalar1234 = Scalar1234 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 Negative()
    {
        return new Ga4KVector4()
        {
            Scalar1234 = -Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 Conjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Dual()
    {
        return new Ga4KVector0()
               {
                   Scalar = Scalar1234
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 UnDual()
    {
        return new Ga4KVector0()
               {
                   Scalar = Scalar1234
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Dual(Ga4KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 UnDual(Ga4KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            mv2,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            mv2,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            Ga4KVector1.Zero,
            mv2,
            Ga4KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2);
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            mv2,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 Add(Ga4KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga4KVector4()
        {
            Scalar1234 = Scalar1234 + mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4Multivector mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga4Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            Add(mv2.KVector4)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            mv2.Negative(),
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            mv2.Negative(),
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            Ga4KVector1.Zero,
            mv2.Negative(),
            Ga4KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return Ga4Multivector.Create(mv2.Negative());
        
        return Ga4Multivector.Create(
            Ga4KVector0.Zero,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            mv2.Negative(),
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector4 Subtract(Ga4KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga4KVector4()
        {
            Scalar1234 = Scalar1234 - mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4Multivector mv2)
    {
        if (mv2.IsZero()) return Ga4Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga4Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            Subtract(mv2.KVector4)
        );
    }
    
}

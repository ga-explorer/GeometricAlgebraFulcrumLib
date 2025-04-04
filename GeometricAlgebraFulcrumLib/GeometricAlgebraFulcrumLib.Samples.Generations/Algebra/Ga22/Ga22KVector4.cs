using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga22;

public sealed partial class Ga22KVector4
{
    public static Ga22KVector4 Zero { get; } = new Ga22KVector4();
    
    public static Ga22KVector4 E1234 { get; } = new Ga22KVector4() { Scalar1234 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector4 Create(params double[] scalarArray)
    {
        return new Ga22KVector4
        {
            Scalar1234 = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector4 operator +(Ga22KVector4 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector4 operator -(Ga22KVector4 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector4 operator *(Ga22KVector4 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector4 operator *(double mv1, Ga22KVector4 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22KVector4 operator /(Ga22KVector4 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(Ga22KVector4 mv1, Ga22KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(Ga22KVector4 mv1, Ga22KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(Ga22KVector4 mv1, Ga22KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(Ga22KVector4 mv1, Ga22KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(Ga22KVector4 mv1, Ga22KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(Ga22KVector4 mv1, Ga22KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(Ga22KVector4 mv1, Ga22KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(Ga22KVector4 mv1, Ga22KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(Ga22KVector4 mv1, Ga22Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(Ga22KVector4 mv1, Ga22Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(Ga22KVector4 mv1, double mv2)
    {
        return Ga22Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator +(double mv1, Ga22KVector4 mv2)
    {
        return Ga22Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(Ga22KVector4 mv1, double mv2)
    {
        return Ga22Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga22Multivector operator -(double mv1, Ga22KVector4 mv2)
    {
        return Ga22Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar1234 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4(double scalar)
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
    public Ga22KVector4 Times(double mv2)
    {
        return new Ga22KVector4()
        {
            Scalar1234 = Scalar1234 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 Negative()
    {
        return new Ga22KVector4()
        {
            Scalar1234 = -Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 Conjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector0 Dual()
    {
        return new Ga22KVector0()
               {
                   Scalar = Scalar1234
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector0 UnDual()
    {
        return new Ga22KVector0()
               {
                   Scalar = Scalar1234
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector0 Dual(Ga22KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector0 UnDual(Ga22KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Add(Ga22KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2);
        
        return Ga22Multivector.Create(
            mv2,
            Ga22KVector1.Zero,
            Ga22KVector2.Zero,
            Ga22KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Add(Ga22KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2);
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            mv2,
            Ga22KVector2.Zero,
            Ga22KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Add(Ga22KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2);
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            Ga22KVector1.Zero,
            mv2,
            Ga22KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Add(Ga22KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2);
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            Ga22KVector1.Zero,
            Ga22KVector2.Zero,
            mv2,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 Add(Ga22KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga22KVector4()
        {
            Scalar1234 = Scalar1234 + mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Add(Ga22Multivector mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga22Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            Add(mv2.KVector4)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Subtract(Ga22KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2.Negative());
        
        return Ga22Multivector.Create(
            mv2.Negative(),
            Ga22KVector1.Zero,
            Ga22KVector2.Zero,
            Ga22KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Subtract(Ga22KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2.Negative());
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            mv2.Negative(),
            Ga22KVector2.Zero,
            Ga22KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Subtract(Ga22KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2.Negative());
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            Ga22KVector1.Zero,
            mv2.Negative(),
            Ga22KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Subtract(Ga22KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return Ga22Multivector.Create(mv2.Negative());
        
        return Ga22Multivector.Create(
            Ga22KVector0.Zero,
            Ga22KVector1.Zero,
            Ga22KVector2.Zero,
            mv2.Negative(),
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22KVector4 Subtract(Ga22KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga22KVector4()
        {
            Scalar1234 = Scalar1234 - mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga22Multivector Subtract(Ga22Multivector mv2)
    {
        if (mv2.IsZero()) return Ga22Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga22Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            Subtract(mv2.KVector4)
        );
    }
    
}

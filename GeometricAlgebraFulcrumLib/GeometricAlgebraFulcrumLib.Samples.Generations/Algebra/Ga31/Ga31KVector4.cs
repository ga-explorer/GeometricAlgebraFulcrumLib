using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga31;

public sealed partial class Ga31KVector4
{
    public static Ga31KVector4 Zero { get; } = new Ga31KVector4();
    
    public static Ga31KVector4 E1234 { get; } = new Ga31KVector4() { Scalar1234 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector4 Create(params double[] scalarArray)
    {
        return new Ga31KVector4
        {
            Scalar1234 = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector4 operator +(Ga31KVector4 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector4 operator -(Ga31KVector4 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector4 operator *(Ga31KVector4 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector4 operator *(double mv1, Ga31KVector4 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector4 operator /(Ga31KVector4 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31KVector4 mv1, Ga31KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31KVector4 mv1, Ga31KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31KVector4 mv1, Ga31KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31KVector4 mv1, Ga31KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31KVector4 mv1, Ga31KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31KVector4 mv1, Ga31KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31KVector4 mv1, Ga31KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31KVector4 mv1, Ga31KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31KVector4 mv1, Ga31Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31KVector4 mv1, Ga31Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31KVector4 mv1, double mv2)
    {
        return Ga31Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(double mv1, Ga31KVector4 mv2)
    {
        return Ga31Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31KVector4 mv1, double mv2)
    {
        return Ga31Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(double mv1, Ga31KVector4 mv2)
    {
        return Ga31Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar1234 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4(double scalar)
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
    public Ga31KVector4 Times(double mv2)
    {
        return new Ga31KVector4()
        {
            Scalar1234 = Scalar1234 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 Negative()
    {
        return new Ga31KVector4()
        {
            Scalar1234 = -Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 Conjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 Dual()
    {
        return new Ga31KVector0()
               {
                   Scalar = Scalar1234
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 UnDual()
    {
        return new Ga31KVector0()
               {
                   Scalar = -Scalar1234
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 Dual(Ga31KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 UnDual(Ga31KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Add(Ga31KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2);
        
        return Ga31Multivector.Create(
            mv2,
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Add(Ga31KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2);
        
        return Ga31Multivector.Create(
            Ga31KVector0.Zero,
            mv2,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Add(Ga31KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2);
        
        return Ga31Multivector.Create(
            Ga31KVector0.Zero,
            Ga31KVector1.Zero,
            mv2,
            Ga31KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Add(Ga31KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2);
        
        return Ga31Multivector.Create(
            Ga31KVector0.Zero,
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            mv2,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 Add(Ga31KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga31KVector4()
        {
            Scalar1234 = Scalar1234 + mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Add(Ga31Multivector mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga31Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            Add(mv2.KVector4)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Subtract(Ga31KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2.Negative());
        
        return Ga31Multivector.Create(
            mv2.Negative(),
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Subtract(Ga31KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2.Negative());
        
        return Ga31Multivector.Create(
            Ga31KVector0.Zero,
            mv2.Negative(),
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Subtract(Ga31KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2.Negative());
        
        return Ga31Multivector.Create(
            Ga31KVector0.Zero,
            Ga31KVector1.Zero,
            mv2.Negative(),
            Ga31KVector3.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Subtract(Ga31KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return Ga31Multivector.Create(mv2.Negative());
        
        return Ga31Multivector.Create(
            Ga31KVector0.Zero,
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            mv2.Negative(),
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector4 Subtract(Ga31KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga31KVector4()
        {
            Scalar1234 = Scalar1234 - mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Subtract(Ga31Multivector mv2)
    {
        if (mv2.IsZero()) return Ga31Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga31Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            Subtract(mv2.KVector4)
        );
    }
    
}

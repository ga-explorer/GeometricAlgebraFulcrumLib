using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga5;

public sealed partial class Ga5KVector5
{
    public static Ga5KVector5 Zero { get; } = new Ga5KVector5();
    
    public static Ga5KVector5 E12345 { get; } = new Ga5KVector5() { Scalar12345 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector5 Create(params double[] scalarArray)
    {
        return new Ga5KVector5
        {
            Scalar12345 = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector5 operator +(Ga5KVector5 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector5 operator -(Ga5KVector5 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector5 operator *(Ga5KVector5 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector5 operator *(double mv1, Ga5KVector5 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector5 operator /(Ga5KVector5 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector5 mv1, Ga5KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector5 mv1, Ga5KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector5 mv1, Ga5KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector5 mv1, Ga5KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector5 mv1, Ga5KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector5 mv1, Ga5KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector5 mv1, Ga5KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector5 mv1, Ga5KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector5 mv1, Ga5KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector5 mv1, Ga5KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector5 mv1, Ga5Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector5 mv1, Ga5Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector5 mv1, double mv2)
    {
        return Ga5Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(double mv1, Ga5KVector5 mv2)
    {
        return Ga5Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector5 mv1, double mv2)
    {
        return Ga5Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(double mv1, Ga5KVector5 mv2)
    {
        return Ga5Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar12345 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5(double scalar)
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
    public Ga5KVector5 Times(double mv2)
    {
        return new Ga5KVector5()
        {
            Scalar12345 = Scalar12345 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 Negative()
    {
        return new Ga5KVector5()
        {
            Scalar12345 = -Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 Conjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector0 Dual()
    {
        return new Ga5KVector0()
               {
                   Scalar = Scalar12345
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector0 UnDual()
    {
        return new Ga5KVector0()
               {
                   Scalar = Scalar12345
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector0 Dual(Ga5KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector0 UnDual(Ga5KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Add(Ga5KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2);
        
        return Ga5Multivector.Create(
            mv2,
            Ga5KVector1.Zero,
            Ga5KVector2.Zero,
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Add(Ga5KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2);
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            mv2,
            Ga5KVector2.Zero,
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Add(Ga5KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2);
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            Ga5KVector1.Zero,
            mv2,
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Add(Ga5KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2);
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            Ga5KVector1.Zero,
            Ga5KVector2.Zero,
            mv2,
            Ga5KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Add(Ga5KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2);
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            Ga5KVector1.Zero,
            Ga5KVector2.Zero,
            Ga5KVector3.Zero,
            mv2,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 Add(Ga5KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga5KVector5()
        {
            Scalar12345 = Scalar12345 + mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Add(Ga5Multivector mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga5Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4,
            Add(mv2.KVector5)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Subtract(Ga5KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2.Negative());
        
        return Ga5Multivector.Create(
            mv2.Negative(),
            Ga5KVector1.Zero,
            Ga5KVector2.Zero,
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Subtract(Ga5KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2.Negative());
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            mv2.Negative(),
            Ga5KVector2.Zero,
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Subtract(Ga5KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2.Negative());
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            Ga5KVector1.Zero,
            mv2.Negative(),
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Subtract(Ga5KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2.Negative());
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            Ga5KVector1.Zero,
            Ga5KVector2.Zero,
            mv2.Negative(),
            Ga5KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Subtract(Ga5KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2.Negative());
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            Ga5KVector1.Zero,
            Ga5KVector2.Zero,
            Ga5KVector3.Zero,
            mv2.Negative(),
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector5 Subtract(Ga5KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga5KVector5()
        {
            Scalar12345 = Scalar12345 - mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Subtract(Ga5Multivector mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga5Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative(),
            Subtract(mv2.KVector5)
        );
    }
    
}

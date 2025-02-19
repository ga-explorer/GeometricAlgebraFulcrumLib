using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga41;

public sealed partial class Ga41KVector5
{
    public static Ga41KVector5 Zero { get; } = new Ga41KVector5();
    
    public static Ga41KVector5 E12345 { get; } = new Ga41KVector5() { Scalar12345 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector5 Create(params double[] scalarArray)
    {
        return new Ga41KVector5
        {
            Scalar12345 = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector5 operator +(Ga41KVector5 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector5 operator -(Ga41KVector5 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector5 operator *(Ga41KVector5 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector5 operator *(double mv1, Ga41KVector5 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector5 operator /(Ga41KVector5 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector5 mv1, Ga41KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector5 mv1, Ga41KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector5 mv1, Ga41KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector5 mv1, Ga41KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector5 mv1, Ga41KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector5 mv1, Ga41KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector5 mv1, Ga41KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector5 mv1, Ga41KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector5 mv1, Ga41KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector5 mv1, Ga41KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector5 mv1, Ga41Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector5 mv1, Ga41Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector5 mv1, double mv2)
    {
        return Ga41Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(double mv1, Ga41KVector5 mv2)
    {
        return Ga41Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector5 mv1, double mv2)
    {
        return Ga41Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(double mv1, Ga41KVector5 mv2)
    {
        return Ga41Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar12345 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5(double scalar)
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
    public Ga41KVector5 Times(double mv2)
    {
        return new Ga41KVector5()
        {
            Scalar12345 = Scalar12345 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 Negative()
    {
        return new Ga41KVector5()
        {
            Scalar12345 = -Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 Conjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Dual()
    {
        return new Ga41KVector0()
               {
                   Scalar = Scalar12345
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 UnDual()
    {
        return new Ga41KVector0()
               {
                   Scalar = -Scalar12345
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Dual(Ga41KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 UnDual(Ga41KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            mv2,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            mv2,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            Ga41KVector1.Zero,
            mv2,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            mv2,
            Ga41KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            mv2,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 Add(Ga41KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga41KVector5()
        {
            Scalar12345 = Scalar12345 + mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41Multivector mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga41Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4,
            Add(mv2.KVector5)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            mv2.Negative(),
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            mv2.Negative(),
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            Ga41KVector1.Zero,
            mv2.Negative(),
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            mv2.Negative(),
            Ga41KVector4.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            mv2.Negative(),
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector5 Subtract(Ga41KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga41KVector5()
        {
            Scalar12345 = Scalar12345 - mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41Multivector mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga41Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative(),
            Subtract(mv2.KVector5)
        );
    }
    
}

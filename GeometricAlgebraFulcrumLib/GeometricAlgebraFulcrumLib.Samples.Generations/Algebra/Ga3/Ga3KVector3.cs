using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga3;

public sealed partial class Ga3KVector3
{
    public static Ga3KVector3 Zero { get; } = new Ga3KVector3();
    
    public static Ga3KVector3 E123 { get; } = new Ga3KVector3() { Scalar123 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 Create(params double[] scalarArray)
    {
        return new Ga3KVector3
        {
            Scalar123 = scalarArray[0]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 operator +(Ga3KVector3 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 operator -(Ga3KVector3 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 operator *(Ga3KVector3 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 operator *(double mv1, Ga3KVector3 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 operator /(Ga3KVector3 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector3 mv1, Ga3KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector3 mv1, Ga3KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector3 mv1, Ga3KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector3 mv1, Ga3KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector3 mv1, Ga3KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector3 mv1, Ga3KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector3 mv1, Ga3Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector3 mv1, Ga3Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(Ga3KVector3 mv1, double mv2)
    {
        return Ga3Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator +(double mv1, Ga3KVector3 mv2)
    {
        return Ga3Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(Ga3KVector3 mv1, double mv2)
    {
        return Ga3Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3Multivector operator -(double mv1, Ga3KVector3 mv2)
    {
        return Ga3Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar123 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3(double scalar)
    {
        Scalar123 = scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar123);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar123 == 0d;
    
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
        var scalarArray = new double[8];
        
        scalarArray[7] = Scalar123;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 Times(double mv2)
    {
        return new Ga3KVector3()
        {
            Scalar123 = Scalar123 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 Negative()
    {
        return new Ga3KVector3()
        {
            Scalar123 = -Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 Conjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector0 Dual()
    {
        return new Ga3KVector0()
               {
                   Scalar = Scalar123
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector0 UnDual()
    {
        return new Ga3KVector0()
               {
                   Scalar = -Scalar123
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector0 Dual(Ga3KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector0 UnDual(Ga3KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Add(Ga3KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2);
        
        return Ga3Multivector.Create(
            mv2,
            Ga3KVector1.Zero,
            Ga3KVector2.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Add(Ga3KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2);
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            mv2,
            Ga3KVector2.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Add(Ga3KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2);
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            Ga3KVector1.Zero,
            mv2,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 Add(Ga3KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga3KVector3()
        {
            Scalar123 = Scalar123 + mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Add(Ga3Multivector mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga3Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            Add(mv2.KVector3)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Subtract(Ga3KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2.Negative());
        
        return Ga3Multivector.Create(
            mv2.Negative(),
            Ga3KVector1.Zero,
            Ga3KVector2.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Subtract(Ga3KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2.Negative());
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            mv2.Negative(),
            Ga3KVector2.Zero,
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Subtract(Ga3KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return Ga3Multivector.Create(mv2.Negative());
        
        return Ga3Multivector.Create(
            Ga3KVector0.Zero,
            Ga3KVector1.Zero,
            mv2.Negative(),
            this
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3KVector3 Subtract(Ga3KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga3KVector3()
        {
            Scalar123 = Scalar123 - mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga3Multivector Subtract(Ga3Multivector mv2)
    {
        if (mv2.IsZero()) return Ga3Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga3Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            Subtract(mv2.KVector3)
        );
    }
    
}

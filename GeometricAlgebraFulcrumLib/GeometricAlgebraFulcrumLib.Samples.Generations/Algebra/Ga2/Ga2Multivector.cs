using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga2;

public sealed partial class Ga2Multivector
{
    public static Ga2Multivector Zero { get; } = Create(new double[4]);
    
    public static Ga2Multivector E => Create(Ga2KVector0.E);
    
    public static Ga2Multivector E1 => Create(Ga2KVector1.E1);
    
    public static Ga2Multivector E2 => Create(Ga2KVector1.E2);
    
    public static Ga2Multivector E12 => Create(Ga2KVector2.E12);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector Create(Ga2KVector0 kVector)
    {
        return new Ga2Multivector(
            kVector,
            Ga2KVector1.Zero,
            Ga2KVector2.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector Create(Ga2KVector0 kVector, double scalar)
    {
        return new Ga2Multivector(
            new Ga2KVector0 { Scalar = kVector.Scalar + scalar },
            Ga2KVector1.Zero,
            Ga2KVector2.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector Create(Ga2KVector1 kVector)
    {
        return new Ga2Multivector(
            Ga2KVector0.Zero,
            kVector,
            Ga2KVector2.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector Create(Ga2KVector1 kVector, double scalar)
    {
        return new Ga2Multivector(
            new Ga2KVector0 { Scalar = scalar },
            kVector,
            Ga2KVector2.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector Create(Ga2KVector2 kVector)
    {
        return new Ga2Multivector(
            Ga2KVector0.Zero,
            Ga2KVector1.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector Create(Ga2KVector2 kVector, double scalar)
    {
        return new Ga2Multivector(
            new Ga2KVector0 { Scalar = scalar },
            Ga2KVector1.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector Create(Ga2Multivector mv, double scalar)
    {
        return Create(
            new Ga2KVector0 { Scalar = mv.KVector0.Scalar + scalar },
            mv.KVector1,
            mv.KVector2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector Create(Ga2KVector0 kVector0, Ga2KVector1 kVector1, Ga2KVector2 kVector2)
    {
        return new Ga2Multivector(
            kVector0,
            kVector1,
            kVector2
        );
    }
    
    public static Ga2Multivector Create(params double[] scalarArray)
    {
        var kVector0 = new Ga2KVector0()
        {
            Scalar = scalarArray[0]
        };
        
        var kVector1 = new Ga2KVector1()
        {
            Scalar1 = scalarArray[1],
            Scalar2 = scalarArray[2]
        };
        
        var kVector2 = new Ga2KVector2()
        {
            Scalar12 = scalarArray[3]
        };
        
        return new Ga2Multivector(
            kVector0,
            kVector1,
            kVector2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2Multivector mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2Multivector mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator *(Ga2Multivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator *(double mv1, Ga2Multivector mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator /(Ga2Multivector mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2Multivector mv1, Ga2KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2Multivector mv1, Ga2KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2Multivector mv1, Ga2KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2Multivector mv1, Ga2KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2Multivector mv1, Ga2Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2Multivector mv1, Ga2Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(Ga2Multivector mv1, double mv2)
    {
        return Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator +(double mv1, Ga2Multivector mv2)
    {
        return Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(Ga2Multivector mv1, double mv2)
    {
        return Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2Multivector operator -(double mv1, Ga2Multivector mv2)
    {
        return Create(mv2.Negative(), mv1);
    }
    
    public Ga2KVector0 KVector0 { get; }
    
    public Ga2KVector1 KVector1 { get; }
    
    public Ga2KVector2 KVector2 { get; }
    
    public double Scalar => KVector0.Scalar;
    
    public double Scalar1 => KVector1.Scalar1;
    
    public double Scalar2 => KVector1.Scalar2;
    
    public double Scalar12 => KVector2.Scalar12;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Ga2Multivector(Ga2KVector0 kVector0, Ga2KVector1 kVector1, Ga2KVector2 kVector2)
    {
        KVector0 = kVector0;
        KVector1 = kVector1;
        KVector2 = kVector2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            KVector0.IsValid() &&
            KVector1.IsValid() &&
            KVector2.IsValid();
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            KVector0.IsZero() &&
            KVector1.IsZero() &&
            KVector2.IsZero();
    
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
        return new double[]
        {
            Scalar,
            Scalar1,
            Scalar2,
            Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetKVectorArrays()
    {
        return new double[][]
        {
            KVector0.GetKVectorArray(),
            KVector1.GetKVectorArray(),
            KVector2.GetKVectorArray()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Times(double mv2)
    {
        return new Ga2Multivector(
            KVector0 * mv2,
            KVector1 * mv2,
            KVector2 * mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Negative()
    {
        return new Ga2Multivector(
            KVector0.Negative(),
            KVector1.Negative(),
            KVector2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Reverse()
    {
        return new Ga2Multivector(
            KVector0,
            KVector1,
            KVector2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector GradeInvolution()
    {
        return new Ga2Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector CliffordConjugate()
    {
        return new Ga2Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Inverse()
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            1d / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector InverseTimes(double mv2)
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            mv2 / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Conjugate()
    {
        return new Ga2Multivector(
            KVector0.Conjugate(),
            KVector1.Conjugate(),
            KVector2.Conjugate()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector0 Dual(Ga2KVector0 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2KVector0 UnDual(Ga2KVector0 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Dual(Ga2KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector UnDual(Ga2KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Dual(Ga2KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector UnDual(Ga2KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Add(Ga2KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0.Add(mv2),
            KVector1,
            KVector2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Add(Ga2KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1.Add(mv2),
            KVector2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Add(Ga2KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2.Add(mv2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Add(Ga2Multivector mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return Create(
            KVector0.Add(mv2.KVector0),
            KVector1.Add(mv2.KVector1),
            KVector2.Add(mv2.KVector2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Subtract(Ga2KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0.Subtract(mv2),
            KVector1,
            KVector2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Subtract(Ga2KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1.Subtract(mv2),
            KVector2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Subtract(Ga2KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2.Subtract(mv2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga2Multivector Subtract(Ga2Multivector mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return Create(
            KVector0.Subtract(mv2.KVector0),
            KVector1.Subtract(mv2.KVector1),
            KVector2.Subtract(mv2.KVector2)
        );
    }
    
}

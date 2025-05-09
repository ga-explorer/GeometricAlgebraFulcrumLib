using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga21;

public sealed partial class Ga21Multivector
{
    public static Ga21Multivector Zero { get; } = Create(new double[8]);
    
    public static Ga21Multivector E => Create(Ga21KVector0.E);
    
    public static Ga21Multivector E1 => Create(Ga21KVector1.E1);
    
    public static Ga21Multivector E2 => Create(Ga21KVector1.E2);
    
    public static Ga21Multivector E3 => Create(Ga21KVector1.E3);
    
    public static Ga21Multivector E12 => Create(Ga21KVector2.E12);
    
    public static Ga21Multivector E13 => Create(Ga21KVector2.E13);
    
    public static Ga21Multivector E23 => Create(Ga21KVector2.E23);
    
    public static Ga21Multivector E123 => Create(Ga21KVector3.E123);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector Create(Ga21KVector0 kVector)
    {
        return new Ga21Multivector(
            kVector,
            Ga21KVector1.Zero,
            Ga21KVector2.Zero,
            Ga21KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector Create(Ga21KVector0 kVector, double scalar)
    {
        return new Ga21Multivector(
            new Ga21KVector0 { Scalar = kVector.Scalar + scalar },
            Ga21KVector1.Zero,
            Ga21KVector2.Zero,
            Ga21KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector Create(Ga21KVector1 kVector)
    {
        return new Ga21Multivector(
            Ga21KVector0.Zero,
            kVector,
            Ga21KVector2.Zero,
            Ga21KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector Create(Ga21KVector1 kVector, double scalar)
    {
        return new Ga21Multivector(
            new Ga21KVector0 { Scalar = scalar },
            kVector,
            Ga21KVector2.Zero,
            Ga21KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector Create(Ga21KVector2 kVector)
    {
        return new Ga21Multivector(
            Ga21KVector0.Zero,
            Ga21KVector1.Zero,
            kVector,
            Ga21KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector Create(Ga21KVector2 kVector, double scalar)
    {
        return new Ga21Multivector(
            new Ga21KVector0 { Scalar = scalar },
            Ga21KVector1.Zero,
            kVector,
            Ga21KVector3.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector Create(Ga21KVector3 kVector)
    {
        return new Ga21Multivector(
            Ga21KVector0.Zero,
            Ga21KVector1.Zero,
            Ga21KVector2.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector Create(Ga21KVector3 kVector, double scalar)
    {
        return new Ga21Multivector(
            new Ga21KVector0 { Scalar = scalar },
            Ga21KVector1.Zero,
            Ga21KVector2.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector Create(Ga21Multivector mv, double scalar)
    {
        return Create(
            new Ga21KVector0 { Scalar = mv.KVector0.Scalar + scalar },
            mv.KVector1,
            mv.KVector2,
            mv.KVector3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector Create(Ga21KVector0 kVector0, Ga21KVector1 kVector1, Ga21KVector2 kVector2, Ga21KVector3 kVector3)
    {
        return new Ga21Multivector(
            kVector0,
            kVector1,
            kVector2,
            kVector3
        );
    }
    
    public static Ga21Multivector Create(params double[] scalarArray)
    {
        var kVector0 = new Ga21KVector0()
        {
            Scalar = scalarArray[0]
        };
        
        var kVector1 = new Ga21KVector1()
        {
            Scalar1 = scalarArray[1],
            Scalar2 = scalarArray[2],
            Scalar3 = scalarArray[4]
        };
        
        var kVector2 = new Ga21KVector2()
        {
            Scalar12 = scalarArray[3],
            Scalar13 = scalarArray[5],
            Scalar23 = scalarArray[6]
        };
        
        var kVector3 = new Ga21KVector3()
        {
            Scalar123 = scalarArray[7]
        };
        
        return new Ga21Multivector(
            kVector0,
            kVector1,
            kVector2,
            kVector3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(Ga21Multivector mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(Ga21Multivector mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator *(Ga21Multivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator *(double mv1, Ga21Multivector mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator /(Ga21Multivector mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(Ga21Multivector mv1, Ga21KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(Ga21Multivector mv1, Ga21KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(Ga21Multivector mv1, Ga21KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(Ga21Multivector mv1, Ga21KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(Ga21Multivector mv1, Ga21KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(Ga21Multivector mv1, Ga21KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(Ga21Multivector mv1, Ga21Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(Ga21Multivector mv1, Ga21Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(Ga21Multivector mv1, double mv2)
    {
        return Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator +(double mv1, Ga21Multivector mv2)
    {
        return Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(Ga21Multivector mv1, double mv2)
    {
        return Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21Multivector operator -(double mv1, Ga21Multivector mv2)
    {
        return Create(mv2.Negative(), mv1);
    }
    
    public Ga21KVector0 KVector0 { get; }
    
    public Ga21KVector1 KVector1 { get; }
    
    public Ga21KVector2 KVector2 { get; }
    
    public Ga21KVector3 KVector3 { get; }
    
    public double Scalar => KVector0.Scalar;
    
    public double Scalar1 => KVector1.Scalar1;
    
    public double Scalar2 => KVector1.Scalar2;
    
    public double Scalar3 => KVector1.Scalar3;
    
    public double Scalar12 => KVector2.Scalar12;
    
    public double Scalar13 => KVector2.Scalar13;
    
    public double Scalar23 => KVector2.Scalar23;
    
    public double Scalar123 => KVector3.Scalar123;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Ga21Multivector(Ga21KVector0 kVector0, Ga21KVector1 kVector1, Ga21KVector2 kVector2, Ga21KVector3 kVector3)
    {
        KVector0 = kVector0;
        KVector1 = kVector1;
        KVector2 = kVector2;
        KVector3 = kVector3;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            KVector0.IsValid() &&
            KVector1.IsValid() &&
            KVector2.IsValid() &&
            KVector3.IsValid();
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            KVector0.IsZero() &&
            KVector1.IsZero() &&
            KVector2.IsZero() &&
            KVector3.IsZero();
    
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
            Scalar12,
            Scalar3,
            Scalar13,
            Scalar23,
            Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[][] GetKVectorArrays()
    {
        return new double[][]
        {
            KVector0.GetKVectorArray(),
            KVector1.GetKVectorArray(),
            KVector2.GetKVectorArray(),
            KVector3.GetKVectorArray()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Times(double mv2)
    {
        return new Ga21Multivector(
            KVector0 * mv2,
            KVector1 * mv2,
            KVector2 * mv2,
            KVector3 * mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Negative()
    {
        return new Ga21Multivector(
            KVector0.Negative(),
            KVector1.Negative(),
            KVector2.Negative(),
            KVector3.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Reverse()
    {
        return new Ga21Multivector(
            KVector0,
            KVector1,
            KVector2.Negative(),
            KVector3.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector GradeInvolution()
    {
        return new Ga21Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2,
            KVector3.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector CliffordConjugate()
    {
        return new Ga21Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2.Negative(),
            KVector3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Inverse()
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            1d / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector InverseTimes(double mv2)
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            mv2 / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Conjugate()
    {
        return new Ga21Multivector(
            KVector0.Conjugate(),
            KVector1.Conjugate(),
            KVector2.Conjugate(),
            KVector3.Conjugate()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector0 Dual(Ga21KVector0 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21KVector0 UnDual(Ga21KVector0 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Dual(Ga21KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector UnDual(Ga21KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Dual(Ga21KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector UnDual(Ga21KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Dual(Ga21KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector UnDual(Ga21KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Add(Ga21KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0.Add(mv2),
            KVector1,
            KVector2,
            KVector3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Add(Ga21KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1.Add(mv2),
            KVector2,
            KVector3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Add(Ga21KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2.Add(mv2),
            KVector3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Add(Ga21KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3.Add(mv2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Add(Ga21Multivector mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return Create(
            KVector0.Add(mv2.KVector0),
            KVector1.Add(mv2.KVector1),
            KVector2.Add(mv2.KVector2),
            KVector3.Add(mv2.KVector3)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Subtract(Ga21KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0.Subtract(mv2),
            KVector1,
            KVector2,
            KVector3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Subtract(Ga21KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1.Subtract(mv2),
            KVector2,
            KVector3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Subtract(Ga21KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2.Subtract(mv2),
            KVector3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Subtract(Ga21KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3.Subtract(mv2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga21Multivector Subtract(Ga21Multivector mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return Create(
            KVector0.Subtract(mv2.KVector0),
            KVector1.Subtract(mv2.KVector1),
            KVector2.Subtract(mv2.KVector2),
            KVector3.Subtract(mv2.KVector3)
        );
    }
    
}

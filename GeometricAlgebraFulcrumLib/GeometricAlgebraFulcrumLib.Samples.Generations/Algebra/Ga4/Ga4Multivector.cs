using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga4;

public sealed partial class Ga4Multivector
{
    public static Ga4Multivector Zero { get; } = Create(new double[16]);
    
    public static Ga4Multivector E => Create(Ga4KVector0.E);
    
    public static Ga4Multivector E1 => Create(Ga4KVector1.E1);
    
    public static Ga4Multivector E2 => Create(Ga4KVector1.E2);
    
    public static Ga4Multivector E3 => Create(Ga4KVector1.E3);
    
    public static Ga4Multivector E4 => Create(Ga4KVector1.E4);
    
    public static Ga4Multivector E12 => Create(Ga4KVector2.E12);
    
    public static Ga4Multivector E13 => Create(Ga4KVector2.E13);
    
    public static Ga4Multivector E23 => Create(Ga4KVector2.E23);
    
    public static Ga4Multivector E14 => Create(Ga4KVector2.E14);
    
    public static Ga4Multivector E24 => Create(Ga4KVector2.E24);
    
    public static Ga4Multivector E34 => Create(Ga4KVector2.E34);
    
    public static Ga4Multivector E123 => Create(Ga4KVector3.E123);
    
    public static Ga4Multivector E124 => Create(Ga4KVector3.E124);
    
    public static Ga4Multivector E134 => Create(Ga4KVector3.E134);
    
    public static Ga4Multivector E234 => Create(Ga4KVector3.E234);
    
    public static Ga4Multivector E1234 => Create(Ga4KVector4.E1234);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4KVector0 kVector)
    {
        return new Ga4Multivector(
            kVector,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4KVector0 kVector, double scalar)
    {
        return new Ga4Multivector(
            new Ga4KVector0 { Scalar = kVector.Scalar + scalar },
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4KVector1 kVector)
    {
        return new Ga4Multivector(
            Ga4KVector0.Zero,
            kVector,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4KVector1 kVector, double scalar)
    {
        return new Ga4Multivector(
            new Ga4KVector0 { Scalar = scalar },
            kVector,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4KVector2 kVector)
    {
        return new Ga4Multivector(
            Ga4KVector0.Zero,
            Ga4KVector1.Zero,
            kVector,
            Ga4KVector3.Zero,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4KVector2 kVector, double scalar)
    {
        return new Ga4Multivector(
            new Ga4KVector0 { Scalar = scalar },
            Ga4KVector1.Zero,
            kVector,
            Ga4KVector3.Zero,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4KVector3 kVector)
    {
        return new Ga4Multivector(
            Ga4KVector0.Zero,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            kVector,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4KVector3 kVector, double scalar)
    {
        return new Ga4Multivector(
            new Ga4KVector0 { Scalar = scalar },
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            kVector,
            Ga4KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4KVector4 kVector)
    {
        return new Ga4Multivector(
            Ga4KVector0.Zero,
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4KVector4 kVector, double scalar)
    {
        return new Ga4Multivector(
            new Ga4KVector0 { Scalar = scalar },
            Ga4KVector1.Zero,
            Ga4KVector2.Zero,
            Ga4KVector3.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4Multivector mv, double scalar)
    {
        return Create(
            new Ga4KVector0 { Scalar = mv.KVector0.Scalar + scalar },
            mv.KVector1,
            mv.KVector2,
            mv.KVector3,
            mv.KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector Create(Ga4KVector0 kVector0, Ga4KVector1 kVector1, Ga4KVector2 kVector2, Ga4KVector3 kVector3, Ga4KVector4 kVector4)
    {
        return new Ga4Multivector(
            kVector0,
            kVector1,
            kVector2,
            kVector3,
            kVector4
        );
    }
    
    public static Ga4Multivector Create(params double[] scalarArray)
    {
        var kVector0 = new Ga4KVector0()
        {
            Scalar = scalarArray[0]
        };
        
        var kVector1 = new Ga4KVector1()
        {
            Scalar1 = scalarArray[1],
            Scalar2 = scalarArray[2],
            Scalar3 = scalarArray[4],
            Scalar4 = scalarArray[8]
        };
        
        var kVector2 = new Ga4KVector2()
        {
            Scalar12 = scalarArray[3],
            Scalar13 = scalarArray[5],
            Scalar23 = scalarArray[6],
            Scalar14 = scalarArray[9],
            Scalar24 = scalarArray[10],
            Scalar34 = scalarArray[12]
        };
        
        var kVector3 = new Ga4KVector3()
        {
            Scalar123 = scalarArray[7],
            Scalar124 = scalarArray[11],
            Scalar134 = scalarArray[13],
            Scalar234 = scalarArray[14]
        };
        
        var kVector4 = new Ga4KVector4()
        {
            Scalar1234 = scalarArray[15]
        };
        
        return new Ga4Multivector(
            kVector0,
            kVector1,
            kVector2,
            kVector3,
            kVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4Multivector mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4Multivector mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator *(Ga4Multivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator *(double mv1, Ga4Multivector mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator /(Ga4Multivector mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4Multivector mv1, Ga4KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4Multivector mv1, Ga4KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4Multivector mv1, Ga4KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4Multivector mv1, Ga4KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4Multivector mv1, Ga4KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4Multivector mv1, Ga4KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4Multivector mv1, Ga4KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4Multivector mv1, Ga4KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4Multivector mv1, Ga4Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4Multivector mv1, Ga4Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(Ga4Multivector mv1, double mv2)
    {
        return Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator +(double mv1, Ga4Multivector mv2)
    {
        return Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(Ga4Multivector mv1, double mv2)
    {
        return Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4Multivector operator -(double mv1, Ga4Multivector mv2)
    {
        return Create(mv2.Negative(), mv1);
    }
    
    public Ga4KVector0 KVector0 { get; }
    
    public Ga4KVector1 KVector1 { get; }
    
    public Ga4KVector2 KVector2 { get; }
    
    public Ga4KVector3 KVector3 { get; }
    
    public Ga4KVector4 KVector4 { get; }
    
    public double Scalar => KVector0.Scalar;
    
    public double Scalar1 => KVector1.Scalar1;
    
    public double Scalar2 => KVector1.Scalar2;
    
    public double Scalar3 => KVector1.Scalar3;
    
    public double Scalar4 => KVector1.Scalar4;
    
    public double Scalar12 => KVector2.Scalar12;
    
    public double Scalar13 => KVector2.Scalar13;
    
    public double Scalar23 => KVector2.Scalar23;
    
    public double Scalar14 => KVector2.Scalar14;
    
    public double Scalar24 => KVector2.Scalar24;
    
    public double Scalar34 => KVector2.Scalar34;
    
    public double Scalar123 => KVector3.Scalar123;
    
    public double Scalar124 => KVector3.Scalar124;
    
    public double Scalar134 => KVector3.Scalar134;
    
    public double Scalar234 => KVector3.Scalar234;
    
    public double Scalar1234 => KVector4.Scalar1234;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Ga4Multivector(Ga4KVector0 kVector0, Ga4KVector1 kVector1, Ga4KVector2 kVector2, Ga4KVector3 kVector3, Ga4KVector4 kVector4)
    {
        KVector0 = kVector0;
        KVector1 = kVector1;
        KVector2 = kVector2;
        KVector3 = kVector3;
        KVector4 = kVector4;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            KVector0.IsValid() &&
            KVector1.IsValid() &&
            KVector2.IsValid() &&
            KVector3.IsValid() &&
            KVector4.IsValid();
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            KVector0.IsZero() &&
            KVector1.IsZero() &&
            KVector2.IsZero() &&
            KVector3.IsZero() &&
            KVector4.IsZero();
    
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
            Scalar123,
            Scalar4,
            Scalar14,
            Scalar24,
            Scalar124,
            Scalar34,
            Scalar134,
            Scalar234,
            Scalar1234
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
            KVector3.GetKVectorArray(),
            KVector4.GetKVectorArray()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Times(double mv2)
    {
        return new Ga4Multivector(
            KVector0 * mv2,
            KVector1 * mv2,
            KVector2 * mv2,
            KVector3 * mv2,
            KVector4 * mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Negative()
    {
        return new Ga4Multivector(
            KVector0.Negative(),
            KVector1.Negative(),
            KVector2.Negative(),
            KVector3.Negative(),
            KVector4.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Reverse()
    {
        return new Ga4Multivector(
            KVector0,
            KVector1,
            KVector2.Negative(),
            KVector3.Negative(),
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector GradeInvolution()
    {
        return new Ga4Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2,
            KVector3.Negative(),
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector CliffordConjugate()
    {
        return new Ga4Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2.Negative(),
            KVector3,
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Inverse()
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            1d / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector InverseTimes(double mv2)
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            mv2 / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Conjugate()
    {
        return new Ga4Multivector(
            KVector0.Conjugate(),
            KVector1.Conjugate(),
            KVector2.Conjugate(),
            KVector3.Conjugate(),
            KVector4.Conjugate()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 Dual(Ga4KVector0 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4KVector0 UnDual(Ga4KVector0 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Dual(Ga4KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector UnDual(Ga4KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Dual(Ga4KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector UnDual(Ga4KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Dual(Ga4KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector UnDual(Ga4KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Dual(Ga4KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector UnDual(Ga4KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0.Add(mv2),
            KVector1,
            KVector2,
            KVector3,
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1.Add(mv2),
            KVector2,
            KVector3,
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2.Add(mv2),
            KVector3,
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3.Add(mv2),
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4.Add(mv2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Add(Ga4Multivector mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return Create(
            KVector0.Add(mv2.KVector0),
            KVector1.Add(mv2.KVector1),
            KVector2.Add(mv2.KVector2),
            KVector3.Add(mv2.KVector3),
            KVector4.Add(mv2.KVector4)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0.Subtract(mv2),
            KVector1,
            KVector2,
            KVector3,
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1.Subtract(mv2),
            KVector2,
            KVector3,
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2.Subtract(mv2),
            KVector3,
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3.Subtract(mv2),
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4.Subtract(mv2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga4Multivector Subtract(Ga4Multivector mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return Create(
            KVector0.Subtract(mv2.KVector0),
            KVector1.Subtract(mv2.KVector1),
            KVector2.Subtract(mv2.KVector2),
            KVector3.Subtract(mv2.KVector3),
            KVector4.Subtract(mv2.KVector4)
        );
    }
    
}

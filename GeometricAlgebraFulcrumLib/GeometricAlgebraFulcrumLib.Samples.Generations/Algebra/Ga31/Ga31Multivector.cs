using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga31;

public sealed partial class Ga31Multivector
{
    public static Ga31Multivector Zero { get; } = Create(new double[16]);
    
    public static Ga31Multivector E => Create(Ga31KVector0.E);
    
    public static Ga31Multivector E1 => Create(Ga31KVector1.E1);
    
    public static Ga31Multivector E2 => Create(Ga31KVector1.E2);
    
    public static Ga31Multivector E3 => Create(Ga31KVector1.E3);
    
    public static Ga31Multivector E4 => Create(Ga31KVector1.E4);
    
    public static Ga31Multivector E12 => Create(Ga31KVector2.E12);
    
    public static Ga31Multivector E13 => Create(Ga31KVector2.E13);
    
    public static Ga31Multivector E23 => Create(Ga31KVector2.E23);
    
    public static Ga31Multivector E14 => Create(Ga31KVector2.E14);
    
    public static Ga31Multivector E24 => Create(Ga31KVector2.E24);
    
    public static Ga31Multivector E34 => Create(Ga31KVector2.E34);
    
    public static Ga31Multivector E123 => Create(Ga31KVector3.E123);
    
    public static Ga31Multivector E124 => Create(Ga31KVector3.E124);
    
    public static Ga31Multivector E134 => Create(Ga31KVector3.E134);
    
    public static Ga31Multivector E234 => Create(Ga31KVector3.E234);
    
    public static Ga31Multivector E1234 => Create(Ga31KVector4.E1234);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31KVector0 kVector)
    {
        return new Ga31Multivector(
            kVector,
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31KVector0 kVector, double scalar)
    {
        return new Ga31Multivector(
            new Ga31KVector0 { Scalar = kVector.Scalar + scalar },
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31KVector1 kVector)
    {
        return new Ga31Multivector(
            Ga31KVector0.Zero,
            kVector,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31KVector1 kVector, double scalar)
    {
        return new Ga31Multivector(
            new Ga31KVector0 { Scalar = scalar },
            kVector,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31KVector2 kVector)
    {
        return new Ga31Multivector(
            Ga31KVector0.Zero,
            Ga31KVector1.Zero,
            kVector,
            Ga31KVector3.Zero,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31KVector2 kVector, double scalar)
    {
        return new Ga31Multivector(
            new Ga31KVector0 { Scalar = scalar },
            Ga31KVector1.Zero,
            kVector,
            Ga31KVector3.Zero,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31KVector3 kVector)
    {
        return new Ga31Multivector(
            Ga31KVector0.Zero,
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            kVector,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31KVector3 kVector, double scalar)
    {
        return new Ga31Multivector(
            new Ga31KVector0 { Scalar = scalar },
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            kVector,
            Ga31KVector4.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31KVector4 kVector)
    {
        return new Ga31Multivector(
            Ga31KVector0.Zero,
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31KVector4 kVector, double scalar)
    {
        return new Ga31Multivector(
            new Ga31KVector0 { Scalar = scalar },
            Ga31KVector1.Zero,
            Ga31KVector2.Zero,
            Ga31KVector3.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31Multivector mv, double scalar)
    {
        return Create(
            new Ga31KVector0 { Scalar = mv.KVector0.Scalar + scalar },
            mv.KVector1,
            mv.KVector2,
            mv.KVector3,
            mv.KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector Create(Ga31KVector0 kVector0, Ga31KVector1 kVector1, Ga31KVector2 kVector2, Ga31KVector3 kVector3, Ga31KVector4 kVector4)
    {
        return new Ga31Multivector(
            kVector0,
            kVector1,
            kVector2,
            kVector3,
            kVector4
        );
    }
    
    public static Ga31Multivector Create(params double[] scalarArray)
    {
        var kVector0 = new Ga31KVector0()
        {
            Scalar = scalarArray[0]
        };
        
        var kVector1 = new Ga31KVector1()
        {
            Scalar1 = scalarArray[1],
            Scalar2 = scalarArray[2],
            Scalar3 = scalarArray[4],
            Scalar4 = scalarArray[8]
        };
        
        var kVector2 = new Ga31KVector2()
        {
            Scalar12 = scalarArray[3],
            Scalar13 = scalarArray[5],
            Scalar23 = scalarArray[6],
            Scalar14 = scalarArray[9],
            Scalar24 = scalarArray[10],
            Scalar34 = scalarArray[12]
        };
        
        var kVector3 = new Ga31KVector3()
        {
            Scalar123 = scalarArray[7],
            Scalar124 = scalarArray[11],
            Scalar134 = scalarArray[13],
            Scalar234 = scalarArray[14]
        };
        
        var kVector4 = new Ga31KVector4()
        {
            Scalar1234 = scalarArray[15]
        };
        
        return new Ga31Multivector(
            kVector0,
            kVector1,
            kVector2,
            kVector3,
            kVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31Multivector mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31Multivector mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator *(Ga31Multivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator *(double mv1, Ga31Multivector mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator /(Ga31Multivector mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31Multivector mv1, Ga31KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31Multivector mv1, Ga31KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31Multivector mv1, Ga31KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31Multivector mv1, Ga31KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31Multivector mv1, Ga31KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31Multivector mv1, Ga31KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31Multivector mv1, Ga31KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31Multivector mv1, Ga31KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31Multivector mv1, Ga31Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31Multivector mv1, Ga31Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(Ga31Multivector mv1, double mv2)
    {
        return Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator +(double mv1, Ga31Multivector mv2)
    {
        return Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(Ga31Multivector mv1, double mv2)
    {
        return Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31Multivector operator -(double mv1, Ga31Multivector mv2)
    {
        return Create(mv2.Negative(), mv1);
    }
    
    public Ga31KVector0 KVector0 { get; }
    
    public Ga31KVector1 KVector1 { get; }
    
    public Ga31KVector2 KVector2 { get; }
    
    public Ga31KVector3 KVector3 { get; }
    
    public Ga31KVector4 KVector4 { get; }
    
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
    private Ga31Multivector(Ga31KVector0 kVector0, Ga31KVector1 kVector1, Ga31KVector2 kVector2, Ga31KVector3 kVector3, Ga31KVector4 kVector4)
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
    public Ga31Multivector Times(double mv2)
    {
        return new Ga31Multivector(
            KVector0 * mv2,
            KVector1 * mv2,
            KVector2 * mv2,
            KVector3 * mv2,
            KVector4 * mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Negative()
    {
        return new Ga31Multivector(
            KVector0.Negative(),
            KVector1.Negative(),
            KVector2.Negative(),
            KVector3.Negative(),
            KVector4.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Reverse()
    {
        return new Ga31Multivector(
            KVector0,
            KVector1,
            KVector2.Negative(),
            KVector3.Negative(),
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector GradeInvolution()
    {
        return new Ga31Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2,
            KVector3.Negative(),
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector CliffordConjugate()
    {
        return new Ga31Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2.Negative(),
            KVector3,
            KVector4
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Inverse()
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            1d / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector InverseTimes(double mv2)
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            mv2 / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Conjugate()
    {
        return new Ga31Multivector(
            KVector0.Conjugate(),
            KVector1.Conjugate(),
            KVector2.Conjugate(),
            KVector3.Conjugate(),
            KVector4.Conjugate()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 Dual(Ga31KVector0 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31KVector0 UnDual(Ga31KVector0 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Dual(Ga31KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector UnDual(Ga31KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Dual(Ga31KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector UnDual(Ga31KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Dual(Ga31KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector UnDual(Ga31KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Dual(Ga31KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector UnDual(Ga31KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga31Multivector Add(Ga31KVector0 mv2)
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
    public Ga31Multivector Add(Ga31KVector1 mv2)
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
    public Ga31Multivector Add(Ga31KVector2 mv2)
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
    public Ga31Multivector Add(Ga31KVector3 mv2)
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
    public Ga31Multivector Add(Ga31KVector4 mv2)
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
    public Ga31Multivector Add(Ga31Multivector mv2)
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
    public Ga31Multivector Subtract(Ga31KVector0 mv2)
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
    public Ga31Multivector Subtract(Ga31KVector1 mv2)
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
    public Ga31Multivector Subtract(Ga31KVector2 mv2)
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
    public Ga31Multivector Subtract(Ga31KVector3 mv2)
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
    public Ga31Multivector Subtract(Ga31KVector4 mv2)
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
    public Ga31Multivector Subtract(Ga31Multivector mv2)
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

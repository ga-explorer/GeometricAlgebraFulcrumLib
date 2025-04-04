using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga41;

public sealed partial class Ga41Multivector
{
    public static Ga41Multivector Zero { get; } = Create(new double[32]);
    
    public static Ga41Multivector E => Create(Ga41KVector0.E);
    
    public static Ga41Multivector E1 => Create(Ga41KVector1.E1);
    
    public static Ga41Multivector E2 => Create(Ga41KVector1.E2);
    
    public static Ga41Multivector E3 => Create(Ga41KVector1.E3);
    
    public static Ga41Multivector E4 => Create(Ga41KVector1.E4);
    
    public static Ga41Multivector E5 => Create(Ga41KVector1.E5);
    
    public static Ga41Multivector E12 => Create(Ga41KVector2.E12);
    
    public static Ga41Multivector E13 => Create(Ga41KVector2.E13);
    
    public static Ga41Multivector E23 => Create(Ga41KVector2.E23);
    
    public static Ga41Multivector E14 => Create(Ga41KVector2.E14);
    
    public static Ga41Multivector E24 => Create(Ga41KVector2.E24);
    
    public static Ga41Multivector E34 => Create(Ga41KVector2.E34);
    
    public static Ga41Multivector E15 => Create(Ga41KVector2.E15);
    
    public static Ga41Multivector E25 => Create(Ga41KVector2.E25);
    
    public static Ga41Multivector E35 => Create(Ga41KVector2.E35);
    
    public static Ga41Multivector E45 => Create(Ga41KVector2.E45);
    
    public static Ga41Multivector E123 => Create(Ga41KVector3.E123);
    
    public static Ga41Multivector E124 => Create(Ga41KVector3.E124);
    
    public static Ga41Multivector E134 => Create(Ga41KVector3.E134);
    
    public static Ga41Multivector E234 => Create(Ga41KVector3.E234);
    
    public static Ga41Multivector E125 => Create(Ga41KVector3.E125);
    
    public static Ga41Multivector E135 => Create(Ga41KVector3.E135);
    
    public static Ga41Multivector E235 => Create(Ga41KVector3.E235);
    
    public static Ga41Multivector E145 => Create(Ga41KVector3.E145);
    
    public static Ga41Multivector E245 => Create(Ga41KVector3.E245);
    
    public static Ga41Multivector E345 => Create(Ga41KVector3.E345);
    
    public static Ga41Multivector E1234 => Create(Ga41KVector4.E1234);
    
    public static Ga41Multivector E1235 => Create(Ga41KVector4.E1235);
    
    public static Ga41Multivector E1245 => Create(Ga41KVector4.E1245);
    
    public static Ga41Multivector E1345 => Create(Ga41KVector4.E1345);
    
    public static Ga41Multivector E2345 => Create(Ga41KVector4.E2345);
    
    public static Ga41Multivector E12345 => Create(Ga41KVector5.E12345);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector0 kVector)
    {
        return new Ga41Multivector(
            kVector,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector0 kVector, double scalar)
    {
        return new Ga41Multivector(
            new Ga41KVector0 { Scalar = kVector.Scalar + scalar },
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector1 kVector)
    {
        return new Ga41Multivector(
            Ga41KVector0.Zero,
            kVector,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector1 kVector, double scalar)
    {
        return new Ga41Multivector(
            new Ga41KVector0 { Scalar = scalar },
            kVector,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector2 kVector)
    {
        return new Ga41Multivector(
            Ga41KVector0.Zero,
            Ga41KVector1.Zero,
            kVector,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector2 kVector, double scalar)
    {
        return new Ga41Multivector(
            new Ga41KVector0 { Scalar = scalar },
            Ga41KVector1.Zero,
            kVector,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector3 kVector)
    {
        return new Ga41Multivector(
            Ga41KVector0.Zero,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            kVector,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector3 kVector, double scalar)
    {
        return new Ga41Multivector(
            new Ga41KVector0 { Scalar = scalar },
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            kVector,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector4 kVector)
    {
        return new Ga41Multivector(
            Ga41KVector0.Zero,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            kVector,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector4 kVector, double scalar)
    {
        return new Ga41Multivector(
            new Ga41KVector0 { Scalar = scalar },
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            kVector,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector5 kVector)
    {
        return new Ga41Multivector(
            Ga41KVector0.Zero,
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector5 kVector, double scalar)
    {
        return new Ga41Multivector(
            new Ga41KVector0 { Scalar = scalar },
            Ga41KVector1.Zero,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            kVector
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41Multivector mv, double scalar)
    {
        return Create(
            new Ga41KVector0 { Scalar = mv.KVector0.Scalar + scalar },
            mv.KVector1,
            mv.KVector2,
            mv.KVector3,
            mv.KVector4,
            mv.KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector Create(Ga41KVector0 kVector0, Ga41KVector1 kVector1, Ga41KVector2 kVector2, Ga41KVector3 kVector3, Ga41KVector4 kVector4, Ga41KVector5 kVector5)
    {
        return new Ga41Multivector(
            kVector0,
            kVector1,
            kVector2,
            kVector3,
            kVector4,
            kVector5
        );
    }
    
    public static Ga41Multivector Create(params double[] scalarArray)
    {
        var kVector0 = new Ga41KVector0()
        {
            Scalar = scalarArray[0]
        };
        
        var kVector1 = new Ga41KVector1()
        {
            Scalar1 = scalarArray[1],
            Scalar2 = scalarArray[2],
            Scalar3 = scalarArray[4],
            Scalar4 = scalarArray[8],
            Scalar5 = scalarArray[16]
        };
        
        var kVector2 = new Ga41KVector2()
        {
            Scalar12 = scalarArray[3],
            Scalar13 = scalarArray[5],
            Scalar23 = scalarArray[6],
            Scalar14 = scalarArray[9],
            Scalar24 = scalarArray[10],
            Scalar34 = scalarArray[12],
            Scalar15 = scalarArray[17],
            Scalar25 = scalarArray[18],
            Scalar35 = scalarArray[20],
            Scalar45 = scalarArray[24]
        };
        
        var kVector3 = new Ga41KVector3()
        {
            Scalar123 = scalarArray[7],
            Scalar124 = scalarArray[11],
            Scalar134 = scalarArray[13],
            Scalar234 = scalarArray[14],
            Scalar125 = scalarArray[19],
            Scalar135 = scalarArray[21],
            Scalar235 = scalarArray[22],
            Scalar145 = scalarArray[25],
            Scalar245 = scalarArray[26],
            Scalar345 = scalarArray[28]
        };
        
        var kVector4 = new Ga41KVector4()
        {
            Scalar1234 = scalarArray[15],
            Scalar1235 = scalarArray[23],
            Scalar1245 = scalarArray[27],
            Scalar1345 = scalarArray[29],
            Scalar2345 = scalarArray[30]
        };
        
        var kVector5 = new Ga41KVector5()
        {
            Scalar12345 = scalarArray[31]
        };
        
        return new Ga41Multivector(
            kVector0,
            kVector1,
            kVector2,
            kVector3,
            kVector4,
            kVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41Multivector mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41Multivector mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator *(Ga41Multivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator *(double mv1, Ga41Multivector mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator /(Ga41Multivector mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41Multivector mv1, Ga41KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41Multivector mv1, Ga41KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41Multivector mv1, Ga41KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41Multivector mv1, Ga41KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41Multivector mv1, Ga41KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41Multivector mv1, Ga41KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41Multivector mv1, Ga41KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41Multivector mv1, Ga41KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41Multivector mv1, Ga41KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41Multivector mv1, Ga41KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41Multivector mv1, Ga41Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41Multivector mv1, Ga41Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41Multivector mv1, double mv2)
    {
        return Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(double mv1, Ga41Multivector mv2)
    {
        return Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41Multivector mv1, double mv2)
    {
        return Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(double mv1, Ga41Multivector mv2)
    {
        return Create(mv2.Negative(), mv1);
    }
    
    public Ga41KVector0 KVector0 { get; }
    
    public Ga41KVector1 KVector1 { get; }
    
    public Ga41KVector2 KVector2 { get; }
    
    public Ga41KVector3 KVector3 { get; }
    
    public Ga41KVector4 KVector4 { get; }
    
    public Ga41KVector5 KVector5 { get; }
    
    public double Scalar => KVector0.Scalar;
    
    public double Scalar1 => KVector1.Scalar1;
    
    public double Scalar2 => KVector1.Scalar2;
    
    public double Scalar3 => KVector1.Scalar3;
    
    public double Scalar4 => KVector1.Scalar4;
    
    public double Scalar5 => KVector1.Scalar5;
    
    public double Scalar12 => KVector2.Scalar12;
    
    public double Scalar13 => KVector2.Scalar13;
    
    public double Scalar23 => KVector2.Scalar23;
    
    public double Scalar14 => KVector2.Scalar14;
    
    public double Scalar24 => KVector2.Scalar24;
    
    public double Scalar34 => KVector2.Scalar34;
    
    public double Scalar15 => KVector2.Scalar15;
    
    public double Scalar25 => KVector2.Scalar25;
    
    public double Scalar35 => KVector2.Scalar35;
    
    public double Scalar45 => KVector2.Scalar45;
    
    public double Scalar123 => KVector3.Scalar123;
    
    public double Scalar124 => KVector3.Scalar124;
    
    public double Scalar134 => KVector3.Scalar134;
    
    public double Scalar234 => KVector3.Scalar234;
    
    public double Scalar125 => KVector3.Scalar125;
    
    public double Scalar135 => KVector3.Scalar135;
    
    public double Scalar235 => KVector3.Scalar235;
    
    public double Scalar145 => KVector3.Scalar145;
    
    public double Scalar245 => KVector3.Scalar245;
    
    public double Scalar345 => KVector3.Scalar345;
    
    public double Scalar1234 => KVector4.Scalar1234;
    
    public double Scalar1235 => KVector4.Scalar1235;
    
    public double Scalar1245 => KVector4.Scalar1245;
    
    public double Scalar1345 => KVector4.Scalar1345;
    
    public double Scalar2345 => KVector4.Scalar2345;
    
    public double Scalar12345 => KVector5.Scalar12345;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Ga41Multivector(Ga41KVector0 kVector0, Ga41KVector1 kVector1, Ga41KVector2 kVector2, Ga41KVector3 kVector3, Ga41KVector4 kVector4, Ga41KVector5 kVector5)
    {
        KVector0 = kVector0;
        KVector1 = kVector1;
        KVector2 = kVector2;
        KVector3 = kVector3;
        KVector4 = kVector4;
        KVector5 = kVector5;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            KVector0.IsValid() &&
            KVector1.IsValid() &&
            KVector2.IsValid() &&
            KVector3.IsValid() &&
            KVector4.IsValid() &&
            KVector5.IsValid();
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
            KVector4.IsZero() &&
            KVector5.IsZero();
    
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
            Scalar1234,
            Scalar5,
            Scalar15,
            Scalar25,
            Scalar125,
            Scalar35,
            Scalar135,
            Scalar235,
            Scalar1235,
            Scalar45,
            Scalar145,
            Scalar245,
            Scalar1245,
            Scalar345,
            Scalar1345,
            Scalar2345,
            Scalar12345
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
            KVector4.GetKVectorArray(),
            KVector5.GetKVectorArray()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Times(double mv2)
    {
        return new Ga41Multivector(
            KVector0 * mv2,
            KVector1 * mv2,
            KVector2 * mv2,
            KVector3 * mv2,
            KVector4 * mv2,
            KVector5 * mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Negative()
    {
        return new Ga41Multivector(
            KVector0.Negative(),
            KVector1.Negative(),
            KVector2.Negative(),
            KVector3.Negative(),
            KVector4.Negative(),
            KVector5.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Reverse()
    {
        return new Ga41Multivector(
            KVector0,
            KVector1,
            KVector2.Negative(),
            KVector3.Negative(),
            KVector4,
            KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector GradeInvolution()
    {
        return new Ga41Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2,
            KVector3.Negative(),
            KVector4,
            KVector5.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector CliffordConjugate()
    {
        return new Ga41Multivector(
            KVector0,
            KVector1.Negative(),
            KVector2.Negative(),
            KVector3,
            KVector4,
            KVector5.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Inverse()
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            1d / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector InverseTimes(double mv2)
    {
        var mvReverse = Reverse();
    
        return mvReverse.Times(
            mv2 / mvReverse.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Conjugate()
    {
        return new Ga41Multivector(
            KVector0.Conjugate(),
            KVector1.Conjugate(),
            KVector2.Conjugate(),
            KVector3.Conjugate(),
            KVector4.Conjugate(),
            KVector5.Conjugate()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Dual(Ga41KVector0 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 UnDual(Ga41KVector0 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Dual(Ga41KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector UnDual(Ga41KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Dual(Ga41KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector UnDual(Ga41KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Dual(Ga41KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector UnDual(Ga41KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Dual(Ga41KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector UnDual(Ga41KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Dual(Ga41KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector UnDual(Ga41KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0.Add(mv2),
            KVector1,
            KVector2,
            KVector3,
            KVector4,
            KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1.Add(mv2),
            KVector2,
            KVector3,
            KVector4,
            KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2.Add(mv2),
            KVector3,
            KVector4,
            KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3.Add(mv2),
            KVector4,
            KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4.Add(mv2),
            KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2);
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4,
            KVector5.Add(mv2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41Multivector mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return Create(
            KVector0.Add(mv2.KVector0),
            KVector1.Add(mv2.KVector1),
            KVector2.Add(mv2.KVector2),
            KVector3.Add(mv2.KVector3),
            KVector4.Add(mv2.KVector4),
            KVector5.Add(mv2.KVector5)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector0 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0.Subtract(mv2),
            KVector1,
            KVector2,
            KVector3,
            KVector4,
            KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1.Subtract(mv2),
            KVector2,
            KVector3,
            KVector4,
            KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2.Subtract(mv2),
            KVector3,
            KVector4,
            KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3.Subtract(mv2),
            KVector4,
            KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4.Subtract(mv2),
            KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return Create(mv2.Negative());
        
        return Create(
            KVector0,
            KVector1,
            KVector2,
            KVector3,
            KVector4,
            KVector5.Subtract(mv2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41Multivector mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return Create(
            KVector0.Subtract(mv2.KVector0),
            KVector1.Subtract(mv2.KVector1),
            KVector2.Subtract(mv2.KVector2),
            KVector3.Subtract(mv2.KVector3),
            KVector4.Subtract(mv2.KVector4),
            KVector5.Subtract(mv2.KVector5)
        );
    }
    
}

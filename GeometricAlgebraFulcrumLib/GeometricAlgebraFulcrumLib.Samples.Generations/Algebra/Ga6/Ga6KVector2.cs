using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga6;

public sealed partial class Ga6KVector2
{
    public static Ga6KVector2 Zero { get; } = new Ga6KVector2();
    
    public static Ga6KVector2 E12 { get; } = new Ga6KVector2() { Scalar12 = 1d };
    
    public static Ga6KVector2 E13 { get; } = new Ga6KVector2() { Scalar13 = 1d };
    
    public static Ga6KVector2 E23 { get; } = new Ga6KVector2() { Scalar23 = 1d };
    
    public static Ga6KVector2 E14 { get; } = new Ga6KVector2() { Scalar14 = 1d };
    
    public static Ga6KVector2 E24 { get; } = new Ga6KVector2() { Scalar24 = 1d };
    
    public static Ga6KVector2 E34 { get; } = new Ga6KVector2() { Scalar34 = 1d };
    
    public static Ga6KVector2 E15 { get; } = new Ga6KVector2() { Scalar15 = 1d };
    
    public static Ga6KVector2 E25 { get; } = new Ga6KVector2() { Scalar25 = 1d };
    
    public static Ga6KVector2 E35 { get; } = new Ga6KVector2() { Scalar35 = 1d };
    
    public static Ga6KVector2 E45 { get; } = new Ga6KVector2() { Scalar45 = 1d };
    
    public static Ga6KVector2 E16 { get; } = new Ga6KVector2() { Scalar16 = 1d };
    
    public static Ga6KVector2 E26 { get; } = new Ga6KVector2() { Scalar26 = 1d };
    
    public static Ga6KVector2 E36 { get; } = new Ga6KVector2() { Scalar36 = 1d };
    
    public static Ga6KVector2 E46 { get; } = new Ga6KVector2() { Scalar46 = 1d };
    
    public static Ga6KVector2 E56 { get; } = new Ga6KVector2() { Scalar56 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 Create(params double[] scalarArray)
    {
        return new Ga6KVector2
        {
            Scalar12 = scalarArray[0],
            Scalar13 = scalarArray[1],
            Scalar23 = scalarArray[2],
            Scalar14 = scalarArray[3],
            Scalar24 = scalarArray[4],
            Scalar34 = scalarArray[5],
            Scalar15 = scalarArray[6],
            Scalar25 = scalarArray[7],
            Scalar35 = scalarArray[8],
            Scalar45 = scalarArray[9],
            Scalar16 = scalarArray[10],
            Scalar26 = scalarArray[11],
            Scalar36 = scalarArray[12],
            Scalar46 = scalarArray[13],
            Scalar56 = scalarArray[14]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 operator +(Ga6KVector2 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 operator -(Ga6KVector2 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 operator *(Ga6KVector2 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 operator *(double mv1, Ga6KVector2 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 operator /(Ga6KVector2 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6KVector2 mv1, Ga6KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6KVector2 mv1, Ga6KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6KVector2 mv1, Ga6KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6KVector2 mv1, Ga6KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 operator +(Ga6KVector2 mv1, Ga6KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 operator -(Ga6KVector2 mv1, Ga6KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6KVector2 mv1, Ga6KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6KVector2 mv1, Ga6KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6KVector2 mv1, Ga6KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6KVector2 mv1, Ga6KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6KVector2 mv1, Ga6KVector5 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6KVector2 mv1, Ga6KVector5 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6KVector2 mv1, Ga6Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6KVector2 mv1, Ga6Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(Ga6KVector2 mv1, double mv2)
    {
        return Ga6Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator +(double mv1, Ga6KVector2 mv2)
    {
        return Ga6Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(Ga6KVector2 mv1, double mv2)
    {
        return Ga6Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6Multivector operator -(double mv1, Ga6KVector2 mv2)
    {
        return Ga6Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar12 { get; init; }
    
    public double Scalar13 { get; init; }
    
    public double Scalar23 { get; init; }
    
    public double Scalar14 { get; init; }
    
    public double Scalar24 { get; init; }
    
    public double Scalar34 { get; init; }
    
    public double Scalar15 { get; init; }
    
    public double Scalar25 { get; init; }
    
    public double Scalar35 { get; init; }
    
    public double Scalar45 { get; init; }
    
    public double Scalar16 { get; init; }
    
    public double Scalar26 { get; init; }
    
    public double Scalar36 { get; init; }
    
    public double Scalar46 { get; init; }
    
    public double Scalar56 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar12) &&
            !double.IsNaN(Scalar13) &&
            !double.IsNaN(Scalar23) &&
            !double.IsNaN(Scalar14) &&
            !double.IsNaN(Scalar24) &&
            !double.IsNaN(Scalar34) &&
            !double.IsNaN(Scalar15) &&
            !double.IsNaN(Scalar25) &&
            !double.IsNaN(Scalar35) &&
            !double.IsNaN(Scalar45) &&
            !double.IsNaN(Scalar16) &&
            !double.IsNaN(Scalar26) &&
            !double.IsNaN(Scalar36) &&
            !double.IsNaN(Scalar46) &&
            !double.IsNaN(Scalar56);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar12 == 0d &&
            Scalar13 == 0d &&
            Scalar23 == 0d &&
            Scalar14 == 0d &&
            Scalar24 == 0d &&
            Scalar34 == 0d &&
            Scalar15 == 0d &&
            Scalar25 == 0d &&
            Scalar35 == 0d &&
            Scalar45 == 0d &&
            Scalar16 == 0d &&
            Scalar26 == 0d &&
            Scalar36 == 0d &&
            Scalar46 == 0d &&
            Scalar56 == 0d;
    
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
        var scalarArray = new double[64];
        
        scalarArray[3] = Scalar12;
        scalarArray[5] = Scalar13;
        scalarArray[6] = Scalar23;
        scalarArray[9] = Scalar14;
        scalarArray[10] = Scalar24;
        scalarArray[12] = Scalar34;
        scalarArray[17] = Scalar15;
        scalarArray[18] = Scalar25;
        scalarArray[20] = Scalar35;
        scalarArray[24] = Scalar45;
        scalarArray[33] = Scalar16;
        scalarArray[34] = Scalar26;
        scalarArray[36] = Scalar36;
        scalarArray[40] = Scalar46;
        scalarArray[48] = Scalar56;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar12,
            Scalar13,
            Scalar23,
            Scalar14,
            Scalar24,
            Scalar34,
            Scalar15,
            Scalar25,
            Scalar35,
            Scalar45,
            Scalar16,
            Scalar26,
            Scalar36,
            Scalar46,
            Scalar56
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 Times(double mv2)
    {
        return new Ga6KVector2()
        {
            Scalar12 = Scalar12 * mv2,
            Scalar13 = Scalar13 * mv2,
            Scalar23 = Scalar23 * mv2,
            Scalar14 = Scalar14 * mv2,
            Scalar24 = Scalar24 * mv2,
            Scalar34 = Scalar34 * mv2,
            Scalar15 = Scalar15 * mv2,
            Scalar25 = Scalar25 * mv2,
            Scalar35 = Scalar35 * mv2,
            Scalar45 = Scalar45 * mv2,
            Scalar16 = Scalar16 * mv2,
            Scalar26 = Scalar26 * mv2,
            Scalar36 = Scalar36 * mv2,
            Scalar46 = Scalar46 * mv2,
            Scalar56 = Scalar56 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 Negative()
    {
        return new Ga6KVector2()
        {
            Scalar12 = -Scalar12,
            Scalar13 = -Scalar13,
            Scalar23 = -Scalar23,
            Scalar14 = -Scalar14,
            Scalar24 = -Scalar24,
            Scalar34 = -Scalar34,
            Scalar15 = -Scalar15,
            Scalar25 = -Scalar25,
            Scalar35 = -Scalar35,
            Scalar45 = -Scalar45,
            Scalar16 = -Scalar16,
            Scalar26 = -Scalar26,
            Scalar36 = -Scalar36,
            Scalar46 = -Scalar46,
            Scalar56 = -Scalar56
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 Conjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector4 Dual()
    {
        return new Ga6KVector4()
               {
                   Scalar1234 = Scalar56,
                   Scalar1235 = -Scalar46,
                   Scalar1245 = Scalar36,
                   Scalar1345 = -Scalar26,
                   Scalar2345 = Scalar16,
                   Scalar1236 = Scalar45,
                   Scalar1246 = -Scalar35,
                   Scalar1346 = Scalar25,
                   Scalar2346 = -Scalar15,
                   Scalar1256 = Scalar34,
                   Scalar1356 = -Scalar24,
                   Scalar2356 = Scalar14,
                   Scalar1456 = Scalar23,
                   Scalar2456 = -Scalar13,
                   Scalar3456 = Scalar12
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector4 UnDual()
    {
        return new Ga6KVector4()
               {
                   Scalar1234 = -Scalar56,
                   Scalar1235 = Scalar46,
                   Scalar1245 = -Scalar36,
                   Scalar1345 = Scalar26,
                   Scalar2345 = -Scalar16,
                   Scalar1236 = -Scalar45,
                   Scalar1246 = Scalar35,
                   Scalar1346 = -Scalar25,
                   Scalar2346 = Scalar15,
                   Scalar1256 = -Scalar34,
                   Scalar1356 = Scalar24,
                   Scalar2356 = -Scalar14,
                   Scalar1456 = -Scalar23,
                   Scalar2456 = Scalar13,
                   Scalar3456 = -Scalar12
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector0 Dual(Ga6KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector0 UnDual(Ga6KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector1 Dual(Ga6KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector1 UnDual(Ga6KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 Dual(Ga6KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 UnDual(Ga6KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector3 Dual(Ga6KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector3 UnDual(Ga6KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector4 Dual(Ga6KVector6 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector4 UnDual(Ga6KVector6 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2);
        
        return Ga6Multivector.Create(
            mv2,
            Ga6KVector1.Zero,
            this,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2);
        
        return Ga6Multivector.Create(
            Ga6KVector0.Zero,
            mv2,
            this,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 Add(Ga6KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga6KVector2()
        {
            Scalar12 = Scalar12 + mv2.Scalar12,
            Scalar13 = Scalar13 + mv2.Scalar13,
            Scalar23 = Scalar23 + mv2.Scalar23,
            Scalar14 = Scalar14 + mv2.Scalar14,
            Scalar24 = Scalar24 + mv2.Scalar24,
            Scalar34 = Scalar34 + mv2.Scalar34,
            Scalar15 = Scalar15 + mv2.Scalar15,
            Scalar25 = Scalar25 + mv2.Scalar25,
            Scalar35 = Scalar35 + mv2.Scalar35,
            Scalar45 = Scalar45 + mv2.Scalar45,
            Scalar16 = Scalar16 + mv2.Scalar16,
            Scalar26 = Scalar26 + mv2.Scalar26,
            Scalar36 = Scalar36 + mv2.Scalar36,
            Scalar46 = Scalar46 + mv2.Scalar46,
            Scalar56 = Scalar56 + mv2.Scalar56
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2);
        
        return Ga6Multivector.Create(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            this,
            mv2,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2);
        
        return Ga6Multivector.Create(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            this,
            Ga6KVector3.Zero,
            mv2,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2);
        
        return Ga6Multivector.Create(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            this,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            mv2,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6KVector6 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2);
        
        return Ga6Multivector.Create(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            this,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Add(Ga6Multivector mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga6Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            Add(mv2.KVector2),
            mv2.KVector3,
            mv2.KVector4,
            mv2.KVector5,
            mv2.KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2.Negative());
        
        return Ga6Multivector.Create(
            mv2.Negative(),
            Ga6KVector1.Zero,
            this,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2.Negative());
        
        return Ga6Multivector.Create(
            Ga6KVector0.Zero,
            mv2.Negative(),
            this,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6KVector2 Subtract(Ga6KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga6KVector2()
        {
            Scalar12 = Scalar12 - mv2.Scalar12,
            Scalar13 = Scalar13 - mv2.Scalar13,
            Scalar23 = Scalar23 - mv2.Scalar23,
            Scalar14 = Scalar14 - mv2.Scalar14,
            Scalar24 = Scalar24 - mv2.Scalar24,
            Scalar34 = Scalar34 - mv2.Scalar34,
            Scalar15 = Scalar15 - mv2.Scalar15,
            Scalar25 = Scalar25 - mv2.Scalar25,
            Scalar35 = Scalar35 - mv2.Scalar35,
            Scalar45 = Scalar45 - mv2.Scalar45,
            Scalar16 = Scalar16 - mv2.Scalar16,
            Scalar26 = Scalar26 - mv2.Scalar26,
            Scalar36 = Scalar36 - mv2.Scalar36,
            Scalar46 = Scalar46 - mv2.Scalar46,
            Scalar56 = Scalar56 - mv2.Scalar56
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2.Negative());
        
        return Ga6Multivector.Create(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            this,
            mv2.Negative(),
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2.Negative());
        
        return Ga6Multivector.Create(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            this,
            Ga6KVector3.Zero,
            mv2.Negative(),
            Ga6KVector5.Zero,
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2.Negative());
        
        return Ga6Multivector.Create(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            this,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            mv2.Negative(),
            Ga6KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6KVector6 mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return Ga6Multivector.Create(mv2.Negative());
        
        return Ga6Multivector.Create(
            Ga6KVector0.Zero,
            Ga6KVector1.Zero,
            this,
            Ga6KVector3.Zero,
            Ga6KVector4.Zero,
            Ga6KVector5.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga6Multivector Subtract(Ga6Multivector mv2)
    {
        if (mv2.IsZero()) return Ga6Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga6Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            Subtract(mv2.KVector2),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative(),
            mv2.KVector5.Negative(),
            mv2.KVector6.Negative()
        );
    }
    
}

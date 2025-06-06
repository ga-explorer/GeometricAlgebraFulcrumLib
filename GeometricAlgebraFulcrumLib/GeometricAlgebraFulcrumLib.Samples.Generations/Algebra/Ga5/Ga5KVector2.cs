using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga5;

public sealed partial class Ga5KVector2
{
    public static Ga5KVector2 Zero { get; } = new Ga5KVector2();
    
    public static Ga5KVector2 E12 { get; } = new Ga5KVector2() { Scalar12 = 1d };
    
    public static Ga5KVector2 E13 { get; } = new Ga5KVector2() { Scalar13 = 1d };
    
    public static Ga5KVector2 E23 { get; } = new Ga5KVector2() { Scalar23 = 1d };
    
    public static Ga5KVector2 E14 { get; } = new Ga5KVector2() { Scalar14 = 1d };
    
    public static Ga5KVector2 E24 { get; } = new Ga5KVector2() { Scalar24 = 1d };
    
    public static Ga5KVector2 E34 { get; } = new Ga5KVector2() { Scalar34 = 1d };
    
    public static Ga5KVector2 E15 { get; } = new Ga5KVector2() { Scalar15 = 1d };
    
    public static Ga5KVector2 E25 { get; } = new Ga5KVector2() { Scalar25 = 1d };
    
    public static Ga5KVector2 E35 { get; } = new Ga5KVector2() { Scalar35 = 1d };
    
    public static Ga5KVector2 E45 { get; } = new Ga5KVector2() { Scalar45 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector2 Create(params double[] scalarArray)
    {
        return new Ga5KVector2
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
            Scalar45 = scalarArray[9]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector2 operator +(Ga5KVector2 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector2 operator -(Ga5KVector2 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector2 operator *(Ga5KVector2 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector2 operator *(double mv1, Ga5KVector2 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector2 operator /(Ga5KVector2 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector2 mv1, Ga5KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector2 mv1, Ga5KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector2 mv1, Ga5KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector2 mv1, Ga5KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector2 operator +(Ga5KVector2 mv1, Ga5KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5KVector2 operator -(Ga5KVector2 mv1, Ga5KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector2 mv1, Ga5KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector2 mv1, Ga5KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector2 mv1, Ga5KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector2 mv1, Ga5KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector2 mv1, Ga5Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector2 mv1, Ga5Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(Ga5KVector2 mv1, double mv2)
    {
        return Ga5Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator +(double mv1, Ga5KVector2 mv2)
    {
        return Ga5Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(Ga5KVector2 mv1, double mv2)
    {
        return Ga5Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga5Multivector operator -(double mv1, Ga5KVector2 mv2)
    {
        return Ga5Multivector.Create(mv2.Negative(), mv1);
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2()
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
            !double.IsNaN(Scalar45);
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
            Scalar45 == 0d;
    
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
            Scalar45
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 Times(double mv2)
    {
        return new Ga5KVector2()
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
            Scalar45 = Scalar45 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 Negative()
    {
        return new Ga5KVector2()
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
            Scalar45 = -Scalar45
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 Conjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector3 Dual()
    {
        return new Ga5KVector3()
               {
                   Scalar123 = -Scalar45,
                   Scalar124 = Scalar35,
                   Scalar134 = -Scalar25,
                   Scalar234 = Scalar15,
                   Scalar125 = -Scalar34,
                   Scalar135 = Scalar24,
                   Scalar235 = -Scalar14,
                   Scalar145 = -Scalar23,
                   Scalar245 = Scalar13,
                   Scalar345 = -Scalar12
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector3 UnDual()
    {
        return new Ga5KVector3()
               {
                   Scalar123 = -Scalar45,
                   Scalar124 = Scalar35,
                   Scalar134 = -Scalar25,
                   Scalar234 = Scalar15,
                   Scalar125 = -Scalar34,
                   Scalar135 = Scalar24,
                   Scalar235 = -Scalar14,
                   Scalar145 = -Scalar23,
                   Scalar245 = Scalar13,
                   Scalar345 = -Scalar12
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector0 Dual(Ga5KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector0 UnDual(Ga5KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector1 Dual(Ga5KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector1 UnDual(Ga5KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 Dual(Ga5KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 UnDual(Ga5KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector3 Dual(Ga5KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector3 UnDual(Ga5KVector5 kv2)
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
            this,
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            Ga5KVector5.Zero
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
            this,
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            Ga5KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 Add(Ga5KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga5KVector2()
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
            Scalar45 = Scalar45 + mv2.Scalar45
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Add(Ga5KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2);
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            Ga5KVector1.Zero,
            this,
            mv2,
            Ga5KVector4.Zero,
            Ga5KVector5.Zero
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
            this,
            Ga5KVector3.Zero,
            mv2,
            Ga5KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Add(Ga5KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2);
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            Ga5KVector1.Zero,
            this,
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Add(Ga5Multivector mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga5Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            Add(mv2.KVector2),
            mv2.KVector3,
            mv2.KVector4,
            mv2.KVector5
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
            this,
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            Ga5KVector5.Zero
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
            this,
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            Ga5KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5KVector2 Subtract(Ga5KVector2 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga5KVector2()
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
            Scalar45 = Scalar45 - mv2.Scalar45
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Subtract(Ga5KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2.Negative());
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            Ga5KVector1.Zero,
            this,
            mv2.Negative(),
            Ga5KVector4.Zero,
            Ga5KVector5.Zero
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
            this,
            Ga5KVector3.Zero,
            mv2.Negative(),
            Ga5KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Subtract(Ga5KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return Ga5Multivector.Create(mv2.Negative());
        
        return Ga5Multivector.Create(
            Ga5KVector0.Zero,
            Ga5KVector1.Zero,
            this,
            Ga5KVector3.Zero,
            Ga5KVector4.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga5Multivector Subtract(Ga5Multivector mv2)
    {
        if (mv2.IsZero()) return Ga5Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga5Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            Subtract(mv2.KVector2),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative(),
            mv2.KVector5.Negative()
        );
    }
    
}

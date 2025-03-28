using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga401;

public sealed partial class Ga401KVector3
{
    public static Ga401KVector3 Zero { get; } = new Ga401KVector3();
    
    public static Ga401KVector3 E123 { get; } = new Ga401KVector3() { Scalar123 = 1d };
    
    public static Ga401KVector3 E124 { get; } = new Ga401KVector3() { Scalar124 = 1d };
    
    public static Ga401KVector3 E134 { get; } = new Ga401KVector3() { Scalar134 = 1d };
    
    public static Ga401KVector3 E234 { get; } = new Ga401KVector3() { Scalar234 = 1d };
    
    public static Ga401KVector3 E125 { get; } = new Ga401KVector3() { Scalar125 = 1d };
    
    public static Ga401KVector3 E135 { get; } = new Ga401KVector3() { Scalar135 = 1d };
    
    public static Ga401KVector3 E235 { get; } = new Ga401KVector3() { Scalar235 = 1d };
    
    public static Ga401KVector3 E145 { get; } = new Ga401KVector3() { Scalar145 = 1d };
    
    public static Ga401KVector3 E245 { get; } = new Ga401KVector3() { Scalar245 = 1d };
    
    public static Ga401KVector3 E345 { get; } = new Ga401KVector3() { Scalar345 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 Create(params double[] scalarArray)
    {
        return new Ga401KVector3
        {
            Scalar123 = scalarArray[0],
            Scalar124 = scalarArray[1],
            Scalar134 = scalarArray[2],
            Scalar234 = scalarArray[3],
            Scalar125 = scalarArray[4],
            Scalar135 = scalarArray[5],
            Scalar235 = scalarArray[6],
            Scalar145 = scalarArray[7],
            Scalar245 = scalarArray[8],
            Scalar345 = scalarArray[9]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 operator +(Ga401KVector3 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 operator -(Ga401KVector3 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 operator *(Ga401KVector3 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 operator *(double mv1, Ga401KVector3 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 operator /(Ga401KVector3 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector3 mv1, Ga401KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector3 mv1, Ga401KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector3 mv1, Ga401KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector3 mv1, Ga401KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector3 mv1, Ga401KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector3 mv1, Ga401KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 operator +(Ga401KVector3 mv1, Ga401KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 operator -(Ga401KVector3 mv1, Ga401KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector3 mv1, Ga401KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector3 mv1, Ga401KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector3 mv1, Ga401Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector3 mv1, Ga401Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector3 mv1, double mv2)
    {
        return Ga401Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(double mv1, Ga401KVector3 mv2)
    {
        return Ga401Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector3 mv1, double mv2)
    {
        return Ga401Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(double mv1, Ga401KVector3 mv2)
    {
        return Ga401Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar123 { get; init; }
    
    public double Scalar124 { get; init; }
    
    public double Scalar134 { get; init; }
    
    public double Scalar234 { get; init; }
    
    public double Scalar125 { get; init; }
    
    public double Scalar135 { get; init; }
    
    public double Scalar235 { get; init; }
    
    public double Scalar145 { get; init; }
    
    public double Scalar245 { get; init; }
    
    public double Scalar345 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar123) &&
            !double.IsNaN(Scalar124) &&
            !double.IsNaN(Scalar134) &&
            !double.IsNaN(Scalar234) &&
            !double.IsNaN(Scalar125) &&
            !double.IsNaN(Scalar135) &&
            !double.IsNaN(Scalar235) &&
            !double.IsNaN(Scalar145) &&
            !double.IsNaN(Scalar245) &&
            !double.IsNaN(Scalar345);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar123 == 0d &&
            Scalar124 == 0d &&
            Scalar134 == 0d &&
            Scalar234 == 0d &&
            Scalar125 == 0d &&
            Scalar135 == 0d &&
            Scalar235 == 0d &&
            Scalar145 == 0d &&
            Scalar245 == 0d &&
            Scalar345 == 0d;
    
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
        
        scalarArray[7] = Scalar123;
        scalarArray[11] = Scalar124;
        scalarArray[13] = Scalar134;
        scalarArray[14] = Scalar234;
        scalarArray[19] = Scalar125;
        scalarArray[21] = Scalar135;
        scalarArray[22] = Scalar235;
        scalarArray[25] = Scalar145;
        scalarArray[26] = Scalar245;
        scalarArray[28] = Scalar345;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar123,
            Scalar124,
            Scalar134,
            Scalar234,
            Scalar125,
            Scalar135,
            Scalar235,
            Scalar145,
            Scalar245,
            Scalar345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 Times(double mv2)
    {
        return new Ga401KVector3()
        {
            Scalar123 = Scalar123 * mv2,
            Scalar124 = Scalar124 * mv2,
            Scalar134 = Scalar134 * mv2,
            Scalar234 = Scalar234 * mv2,
            Scalar125 = Scalar125 * mv2,
            Scalar135 = Scalar135 * mv2,
            Scalar235 = Scalar235 * mv2,
            Scalar145 = Scalar145 * mv2,
            Scalar245 = Scalar245 * mv2,
            Scalar345 = Scalar345 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 Negative()
    {
        return new Ga401KVector3()
        {
            Scalar123 = -Scalar123,
            Scalar124 = -Scalar124,
            Scalar134 = -Scalar134,
            Scalar234 = -Scalar234,
            Scalar125 = -Scalar125,
            Scalar135 = -Scalar135,
            Scalar235 = -Scalar235,
            Scalar145 = -Scalar145,
            Scalar245 = -Scalar245,
            Scalar345 = -Scalar345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 Conjugate()
    {
        return new Ga401KVector3()
               {
                   Scalar234 = -Scalar234,
                   Scalar235 = -Scalar235,
                   Scalar245 = -Scalar245,
                   Scalar345 = -Scalar345
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector2 UnDual()
    {
        return new Ga401KVector2()
               {
                   Scalar12 = -Scalar345,
                   Scalar13 = Scalar245,
                   Scalar14 = -Scalar235,
                   Scalar15 = Scalar234
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector0 Dual(Ga401KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector0 UnDual(Ga401KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector1 Dual(Ga401KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector1 UnDual(Ga401KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector2 Dual(Ga401KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector2 UnDual(Ga401KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2);
        
        return Ga401Multivector.Create(
            mv2,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            this,
            Ga401KVector4.Zero,
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2);
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            mv2,
            Ga401KVector2.Zero,
            this,
            Ga401KVector4.Zero,
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2);
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            mv2,
            this,
            Ga401KVector4.Zero,
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 Add(Ga401KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga401KVector3()
        {
            Scalar123 = Scalar123 + mv2.Scalar123,
            Scalar124 = Scalar124 + mv2.Scalar124,
            Scalar134 = Scalar134 + mv2.Scalar134,
            Scalar234 = Scalar234 + mv2.Scalar234,
            Scalar125 = Scalar125 + mv2.Scalar125,
            Scalar135 = Scalar135 + mv2.Scalar135,
            Scalar235 = Scalar235 + mv2.Scalar235,
            Scalar145 = Scalar145 + mv2.Scalar145,
            Scalar245 = Scalar245 + mv2.Scalar245,
            Scalar345 = Scalar345 + mv2.Scalar345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2);
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            this,
            mv2,
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2);
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            this,
            Ga401KVector4.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401Multivector mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga401Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            Add(mv2.KVector3),
            mv2.KVector4,
            mv2.KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2.Negative());
        
        return Ga401Multivector.Create(
            mv2.Negative(),
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            this,
            Ga401KVector4.Zero,
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2.Negative());
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            mv2.Negative(),
            Ga401KVector2.Zero,
            this,
            Ga401KVector4.Zero,
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2.Negative());
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            mv2.Negative(),
            this,
            Ga401KVector4.Zero,
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector3 Subtract(Ga401KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga401KVector3()
        {
            Scalar123 = Scalar123 - mv2.Scalar123,
            Scalar124 = Scalar124 - mv2.Scalar124,
            Scalar134 = Scalar134 - mv2.Scalar134,
            Scalar234 = Scalar234 - mv2.Scalar234,
            Scalar125 = Scalar125 - mv2.Scalar125,
            Scalar135 = Scalar135 - mv2.Scalar135,
            Scalar235 = Scalar235 - mv2.Scalar235,
            Scalar145 = Scalar145 - mv2.Scalar145,
            Scalar245 = Scalar245 - mv2.Scalar245,
            Scalar345 = Scalar345 - mv2.Scalar345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2.Negative());
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            this,
            mv2.Negative(),
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2.Negative());
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            this,
            Ga401KVector4.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401Multivector mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga401Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            Subtract(mv2.KVector3),
            mv2.KVector4.Negative(),
            mv2.KVector5.Negative()
        );
    }
    
}

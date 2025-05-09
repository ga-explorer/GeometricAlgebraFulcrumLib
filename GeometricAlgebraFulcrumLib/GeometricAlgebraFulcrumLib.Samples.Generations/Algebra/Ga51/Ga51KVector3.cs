using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga51;

public sealed partial class Ga51KVector3
{
    public static Ga51KVector3 Zero { get; } = new Ga51KVector3();
    
    public static Ga51KVector3 E123 { get; } = new Ga51KVector3() { Scalar123 = 1d };
    
    public static Ga51KVector3 E124 { get; } = new Ga51KVector3() { Scalar124 = 1d };
    
    public static Ga51KVector3 E134 { get; } = new Ga51KVector3() { Scalar134 = 1d };
    
    public static Ga51KVector3 E234 { get; } = new Ga51KVector3() { Scalar234 = 1d };
    
    public static Ga51KVector3 E125 { get; } = new Ga51KVector3() { Scalar125 = 1d };
    
    public static Ga51KVector3 E135 { get; } = new Ga51KVector3() { Scalar135 = 1d };
    
    public static Ga51KVector3 E235 { get; } = new Ga51KVector3() { Scalar235 = 1d };
    
    public static Ga51KVector3 E145 { get; } = new Ga51KVector3() { Scalar145 = 1d };
    
    public static Ga51KVector3 E245 { get; } = new Ga51KVector3() { Scalar245 = 1d };
    
    public static Ga51KVector3 E345 { get; } = new Ga51KVector3() { Scalar345 = 1d };
    
    public static Ga51KVector3 E126 { get; } = new Ga51KVector3() { Scalar126 = 1d };
    
    public static Ga51KVector3 E136 { get; } = new Ga51KVector3() { Scalar136 = 1d };
    
    public static Ga51KVector3 E236 { get; } = new Ga51KVector3() { Scalar236 = 1d };
    
    public static Ga51KVector3 E146 { get; } = new Ga51KVector3() { Scalar146 = 1d };
    
    public static Ga51KVector3 E246 { get; } = new Ga51KVector3() { Scalar246 = 1d };
    
    public static Ga51KVector3 E346 { get; } = new Ga51KVector3() { Scalar346 = 1d };
    
    public static Ga51KVector3 E156 { get; } = new Ga51KVector3() { Scalar156 = 1d };
    
    public static Ga51KVector3 E256 { get; } = new Ga51KVector3() { Scalar256 = 1d };
    
    public static Ga51KVector3 E356 { get; } = new Ga51KVector3() { Scalar356 = 1d };
    
    public static Ga51KVector3 E456 { get; } = new Ga51KVector3() { Scalar456 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 Create(params double[] scalarArray)
    {
        return new Ga51KVector3
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
            Scalar345 = scalarArray[9],
            Scalar126 = scalarArray[10],
            Scalar136 = scalarArray[11],
            Scalar236 = scalarArray[12],
            Scalar146 = scalarArray[13],
            Scalar246 = scalarArray[14],
            Scalar346 = scalarArray[15],
            Scalar156 = scalarArray[16],
            Scalar256 = scalarArray[17],
            Scalar356 = scalarArray[18],
            Scalar456 = scalarArray[19]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 operator +(Ga51KVector3 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 operator -(Ga51KVector3 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 operator *(Ga51KVector3 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 operator *(double mv1, Ga51KVector3 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 operator /(Ga51KVector3 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector3 mv1, Ga51KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector3 mv1, Ga51KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector3 mv1, Ga51KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector3 mv1, Ga51KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector3 mv1, Ga51KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector3 mv1, Ga51KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 operator +(Ga51KVector3 mv1, Ga51KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 operator -(Ga51KVector3 mv1, Ga51KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector3 mv1, Ga51KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector3 mv1, Ga51KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector3 mv1, Ga51KVector5 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector3 mv1, Ga51KVector5 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector3 mv1, Ga51Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector3 mv1, Ga51Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector3 mv1, double mv2)
    {
        return Ga51Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(double mv1, Ga51KVector3 mv2)
    {
        return Ga51Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector3 mv1, double mv2)
    {
        return Ga51Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(double mv1, Ga51KVector3 mv2)
    {
        return Ga51Multivector.Create(mv2.Negative(), mv1);
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
    
    public double Scalar126 { get; init; }
    
    public double Scalar136 { get; init; }
    
    public double Scalar236 { get; init; }
    
    public double Scalar146 { get; init; }
    
    public double Scalar246 { get; init; }
    
    public double Scalar346 { get; init; }
    
    public double Scalar156 { get; init; }
    
    public double Scalar256 { get; init; }
    
    public double Scalar356 { get; init; }
    
    public double Scalar456 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3()
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
            !double.IsNaN(Scalar345) &&
            !double.IsNaN(Scalar126) &&
            !double.IsNaN(Scalar136) &&
            !double.IsNaN(Scalar236) &&
            !double.IsNaN(Scalar146) &&
            !double.IsNaN(Scalar246) &&
            !double.IsNaN(Scalar346) &&
            !double.IsNaN(Scalar156) &&
            !double.IsNaN(Scalar256) &&
            !double.IsNaN(Scalar356) &&
            !double.IsNaN(Scalar456);
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
            Scalar345 == 0d &&
            Scalar126 == 0d &&
            Scalar136 == 0d &&
            Scalar236 == 0d &&
            Scalar146 == 0d &&
            Scalar246 == 0d &&
            Scalar346 == 0d &&
            Scalar156 == 0d &&
            Scalar256 == 0d &&
            Scalar356 == 0d &&
            Scalar456 == 0d;
    
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
        scalarArray[35] = Scalar126;
        scalarArray[37] = Scalar136;
        scalarArray[38] = Scalar236;
        scalarArray[41] = Scalar146;
        scalarArray[42] = Scalar246;
        scalarArray[44] = Scalar346;
        scalarArray[49] = Scalar156;
        scalarArray[50] = Scalar256;
        scalarArray[52] = Scalar356;
        scalarArray[56] = Scalar456;
        
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
            Scalar345,
            Scalar126,
            Scalar136,
            Scalar236,
            Scalar146,
            Scalar246,
            Scalar346,
            Scalar156,
            Scalar256,
            Scalar356,
            Scalar456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 Times(double mv2)
    {
        return new Ga51KVector3()
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
            Scalar345 = Scalar345 * mv2,
            Scalar126 = Scalar126 * mv2,
            Scalar136 = Scalar136 * mv2,
            Scalar236 = Scalar236 * mv2,
            Scalar146 = Scalar146 * mv2,
            Scalar246 = Scalar246 * mv2,
            Scalar346 = Scalar346 * mv2,
            Scalar156 = Scalar156 * mv2,
            Scalar256 = Scalar256 * mv2,
            Scalar356 = Scalar356 * mv2,
            Scalar456 = Scalar456 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 Negative()
    {
        return new Ga51KVector3()
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
            Scalar345 = -Scalar345,
            Scalar126 = -Scalar126,
            Scalar136 = -Scalar136,
            Scalar236 = -Scalar236,
            Scalar146 = -Scalar146,
            Scalar246 = -Scalar246,
            Scalar346 = -Scalar346,
            Scalar156 = -Scalar156,
            Scalar256 = -Scalar256,
            Scalar356 = -Scalar356,
            Scalar456 = -Scalar456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 Reverse()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 Inverse()
    {
        return Times(-1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 InverseTimes(double mv2)
    {
        return Times(-mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 Conjugate()
    {
        return new Ga51KVector3()
               {
                   Scalar123 = Scalar123,
                   Scalar124 = Scalar124,
                   Scalar134 = Scalar134,
                   Scalar234 = -Scalar234,
                   Scalar125 = Scalar125,
                   Scalar135 = Scalar135,
                   Scalar235 = -Scalar235,
                   Scalar145 = Scalar145,
                   Scalar245 = -Scalar245,
                   Scalar345 = -Scalar345,
                   Scalar126 = Scalar126,
                   Scalar136 = Scalar136,
                   Scalar236 = -Scalar236,
                   Scalar146 = Scalar146,
                   Scalar246 = -Scalar246,
                   Scalar346 = -Scalar346,
                   Scalar156 = Scalar156,
                   Scalar256 = -Scalar256,
                   Scalar356 = -Scalar356,
                   Scalar456 = -Scalar456
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 Dual()
    {
        return new Ga51KVector3()
               {
                   Scalar123 = Scalar456,
                   Scalar124 = -Scalar356,
                   Scalar134 = Scalar256,
                   Scalar234 = Scalar156,
                   Scalar125 = Scalar346,
                   Scalar135 = -Scalar246,
                   Scalar235 = -Scalar146,
                   Scalar145 = Scalar236,
                   Scalar245 = Scalar136,
                   Scalar345 = -Scalar126,
                   Scalar126 = -Scalar345,
                   Scalar136 = Scalar245,
                   Scalar236 = Scalar145,
                   Scalar146 = -Scalar235,
                   Scalar246 = -Scalar135,
                   Scalar346 = Scalar125,
                   Scalar156 = Scalar234,
                   Scalar256 = Scalar134,
                   Scalar356 = -Scalar124,
                   Scalar456 = Scalar123
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 UnDual()
    {
        return new Ga51KVector3()
               {
                   Scalar123 = Scalar456,
                   Scalar124 = -Scalar356,
                   Scalar134 = Scalar256,
                   Scalar234 = Scalar156,
                   Scalar125 = Scalar346,
                   Scalar135 = -Scalar246,
                   Scalar235 = -Scalar146,
                   Scalar145 = Scalar236,
                   Scalar245 = Scalar136,
                   Scalar345 = -Scalar126,
                   Scalar126 = -Scalar345,
                   Scalar136 = Scalar245,
                   Scalar236 = Scalar145,
                   Scalar146 = -Scalar235,
                   Scalar246 = -Scalar135,
                   Scalar346 = Scalar125,
                   Scalar156 = Scalar234,
                   Scalar256 = Scalar134,
                   Scalar356 = -Scalar124,
                   Scalar456 = Scalar123
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector0 Dual(Ga51KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector0 UnDual(Ga51KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector1 Dual(Ga51KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector1 UnDual(Ga51KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector2 Dual(Ga51KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector2 UnDual(Ga51KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 Dual(Ga51KVector6 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 UnDual(Ga51KVector6 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Add(Ga51KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2);
        
        return Ga51Multivector.Create(
            mv2,
            Ga51KVector1.Zero,
            Ga51KVector2.Zero,
            this,
            Ga51KVector4.Zero,
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Add(Ga51KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2);
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            mv2,
            Ga51KVector2.Zero,
            this,
            Ga51KVector4.Zero,
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Add(Ga51KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2);
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            Ga51KVector1.Zero,
            mv2,
            this,
            Ga51KVector4.Zero,
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 Add(Ga51KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga51KVector3()
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
            Scalar345 = Scalar345 + mv2.Scalar345,
            Scalar126 = Scalar126 + mv2.Scalar126,
            Scalar136 = Scalar136 + mv2.Scalar136,
            Scalar236 = Scalar236 + mv2.Scalar236,
            Scalar146 = Scalar146 + mv2.Scalar146,
            Scalar246 = Scalar246 + mv2.Scalar246,
            Scalar346 = Scalar346 + mv2.Scalar346,
            Scalar156 = Scalar156 + mv2.Scalar156,
            Scalar256 = Scalar256 + mv2.Scalar256,
            Scalar356 = Scalar356 + mv2.Scalar356,
            Scalar456 = Scalar456 + mv2.Scalar456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Add(Ga51KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2);
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            Ga51KVector1.Zero,
            Ga51KVector2.Zero,
            this,
            mv2,
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Add(Ga51KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2);
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            Ga51KVector1.Zero,
            Ga51KVector2.Zero,
            this,
            Ga51KVector4.Zero,
            mv2,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Add(Ga51KVector6 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2);
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            Ga51KVector1.Zero,
            Ga51KVector2.Zero,
            this,
            Ga51KVector4.Zero,
            Ga51KVector5.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Add(Ga51Multivector mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga51Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            Add(mv2.KVector3),
            mv2.KVector4,
            mv2.KVector5,
            mv2.KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Subtract(Ga51KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2.Negative());
        
        return Ga51Multivector.Create(
            mv2.Negative(),
            Ga51KVector1.Zero,
            Ga51KVector2.Zero,
            this,
            Ga51KVector4.Zero,
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Subtract(Ga51KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2.Negative());
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            mv2.Negative(),
            Ga51KVector2.Zero,
            this,
            Ga51KVector4.Zero,
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Subtract(Ga51KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2.Negative());
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            Ga51KVector1.Zero,
            mv2.Negative(),
            this,
            Ga51KVector4.Zero,
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector3 Subtract(Ga51KVector3 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga51KVector3()
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
            Scalar345 = Scalar345 - mv2.Scalar345,
            Scalar126 = Scalar126 - mv2.Scalar126,
            Scalar136 = Scalar136 - mv2.Scalar136,
            Scalar236 = Scalar236 - mv2.Scalar236,
            Scalar146 = Scalar146 - mv2.Scalar146,
            Scalar246 = Scalar246 - mv2.Scalar246,
            Scalar346 = Scalar346 - mv2.Scalar346,
            Scalar156 = Scalar156 - mv2.Scalar156,
            Scalar256 = Scalar256 - mv2.Scalar256,
            Scalar356 = Scalar356 - mv2.Scalar356,
            Scalar456 = Scalar456 - mv2.Scalar456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Subtract(Ga51KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2.Negative());
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            Ga51KVector1.Zero,
            Ga51KVector2.Zero,
            this,
            mv2.Negative(),
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Subtract(Ga51KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2.Negative());
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            Ga51KVector1.Zero,
            Ga51KVector2.Zero,
            this,
            Ga51KVector4.Zero,
            mv2.Negative(),
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Subtract(Ga51KVector6 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2.Negative());
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            Ga51KVector1.Zero,
            Ga51KVector2.Zero,
            this,
            Ga51KVector4.Zero,
            Ga51KVector5.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Subtract(Ga51Multivector mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga51Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            Subtract(mv2.KVector3),
            mv2.KVector4.Negative(),
            mv2.KVector5.Negative(),
            mv2.KVector6.Negative()
        );
    }
    
}

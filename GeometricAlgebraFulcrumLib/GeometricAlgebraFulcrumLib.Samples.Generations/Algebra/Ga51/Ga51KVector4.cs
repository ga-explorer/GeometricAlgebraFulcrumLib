using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga51;

public sealed partial class Ga51KVector4
{
    public static Ga51KVector4 Zero { get; } = new Ga51KVector4();
    
    public static Ga51KVector4 E1234 { get; } = new Ga51KVector4() { Scalar1234 = 1d };
    
    public static Ga51KVector4 E1235 { get; } = new Ga51KVector4() { Scalar1235 = 1d };
    
    public static Ga51KVector4 E1245 { get; } = new Ga51KVector4() { Scalar1245 = 1d };
    
    public static Ga51KVector4 E1345 { get; } = new Ga51KVector4() { Scalar1345 = 1d };
    
    public static Ga51KVector4 E2345 { get; } = new Ga51KVector4() { Scalar2345 = 1d };
    
    public static Ga51KVector4 E1236 { get; } = new Ga51KVector4() { Scalar1236 = 1d };
    
    public static Ga51KVector4 E1246 { get; } = new Ga51KVector4() { Scalar1246 = 1d };
    
    public static Ga51KVector4 E1346 { get; } = new Ga51KVector4() { Scalar1346 = 1d };
    
    public static Ga51KVector4 E2346 { get; } = new Ga51KVector4() { Scalar2346 = 1d };
    
    public static Ga51KVector4 E1256 { get; } = new Ga51KVector4() { Scalar1256 = 1d };
    
    public static Ga51KVector4 E1356 { get; } = new Ga51KVector4() { Scalar1356 = 1d };
    
    public static Ga51KVector4 E2356 { get; } = new Ga51KVector4() { Scalar2356 = 1d };
    
    public static Ga51KVector4 E1456 { get; } = new Ga51KVector4() { Scalar1456 = 1d };
    
    public static Ga51KVector4 E2456 { get; } = new Ga51KVector4() { Scalar2456 = 1d };
    
    public static Ga51KVector4 E3456 { get; } = new Ga51KVector4() { Scalar3456 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector4 Create(params double[] scalarArray)
    {
        return new Ga51KVector4
        {
            Scalar1234 = scalarArray[0],
            Scalar1235 = scalarArray[1],
            Scalar1245 = scalarArray[2],
            Scalar1345 = scalarArray[3],
            Scalar2345 = scalarArray[4],
            Scalar1236 = scalarArray[5],
            Scalar1246 = scalarArray[6],
            Scalar1346 = scalarArray[7],
            Scalar2346 = scalarArray[8],
            Scalar1256 = scalarArray[9],
            Scalar1356 = scalarArray[10],
            Scalar2356 = scalarArray[11],
            Scalar1456 = scalarArray[12],
            Scalar2456 = scalarArray[13],
            Scalar3456 = scalarArray[14]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector4 operator +(Ga51KVector4 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector4 operator -(Ga51KVector4 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector4 operator *(Ga51KVector4 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector4 operator *(double mv1, Ga51KVector4 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector4 operator /(Ga51KVector4 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector4 mv1, Ga51KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector4 mv1, Ga51KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector4 mv1, Ga51KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector4 mv1, Ga51KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector4 mv1, Ga51KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector4 mv1, Ga51KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector4 mv1, Ga51KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector4 mv1, Ga51KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector4 operator +(Ga51KVector4 mv1, Ga51KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector4 operator -(Ga51KVector4 mv1, Ga51KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector4 mv1, Ga51KVector5 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector4 mv1, Ga51KVector5 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector4 mv1, Ga51Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector4 mv1, Ga51Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(Ga51KVector4 mv1, double mv2)
    {
        return Ga51Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator +(double mv1, Ga51KVector4 mv2)
    {
        return Ga51Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(Ga51KVector4 mv1, double mv2)
    {
        return Ga51Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51Multivector operator -(double mv1, Ga51KVector4 mv2)
    {
        return Ga51Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar1234 { get; init; }
    
    public double Scalar1235 { get; init; }
    
    public double Scalar1245 { get; init; }
    
    public double Scalar1345 { get; init; }
    
    public double Scalar2345 { get; init; }
    
    public double Scalar1236 { get; init; }
    
    public double Scalar1246 { get; init; }
    
    public double Scalar1346 { get; init; }
    
    public double Scalar2346 { get; init; }
    
    public double Scalar1256 { get; init; }
    
    public double Scalar1356 { get; init; }
    
    public double Scalar2356 { get; init; }
    
    public double Scalar1456 { get; init; }
    
    public double Scalar2456 { get; init; }
    
    public double Scalar3456 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar1234) &&
            !double.IsNaN(Scalar1235) &&
            !double.IsNaN(Scalar1245) &&
            !double.IsNaN(Scalar1345) &&
            !double.IsNaN(Scalar2345) &&
            !double.IsNaN(Scalar1236) &&
            !double.IsNaN(Scalar1246) &&
            !double.IsNaN(Scalar1346) &&
            !double.IsNaN(Scalar2346) &&
            !double.IsNaN(Scalar1256) &&
            !double.IsNaN(Scalar1356) &&
            !double.IsNaN(Scalar2356) &&
            !double.IsNaN(Scalar1456) &&
            !double.IsNaN(Scalar2456) &&
            !double.IsNaN(Scalar3456);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar1234 == 0d &&
            Scalar1235 == 0d &&
            Scalar1245 == 0d &&
            Scalar1345 == 0d &&
            Scalar2345 == 0d &&
            Scalar1236 == 0d &&
            Scalar1246 == 0d &&
            Scalar1346 == 0d &&
            Scalar2346 == 0d &&
            Scalar1256 == 0d &&
            Scalar1356 == 0d &&
            Scalar2356 == 0d &&
            Scalar1456 == 0d &&
            Scalar2456 == 0d &&
            Scalar3456 == 0d;
    
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
        
        scalarArray[15] = Scalar1234;
        scalarArray[23] = Scalar1235;
        scalarArray[27] = Scalar1245;
        scalarArray[29] = Scalar1345;
        scalarArray[30] = Scalar2345;
        scalarArray[39] = Scalar1236;
        scalarArray[43] = Scalar1246;
        scalarArray[45] = Scalar1346;
        scalarArray[46] = Scalar2346;
        scalarArray[51] = Scalar1256;
        scalarArray[53] = Scalar1356;
        scalarArray[54] = Scalar2356;
        scalarArray[57] = Scalar1456;
        scalarArray[58] = Scalar2456;
        scalarArray[60] = Scalar3456;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar1234,
            Scalar1235,
            Scalar1245,
            Scalar1345,
            Scalar2345,
            Scalar1236,
            Scalar1246,
            Scalar1346,
            Scalar2346,
            Scalar1256,
            Scalar1356,
            Scalar2356,
            Scalar1456,
            Scalar2456,
            Scalar3456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 Times(double mv2)
    {
        return new Ga51KVector4()
        {
            Scalar1234 = Scalar1234 * mv2,
            Scalar1235 = Scalar1235 * mv2,
            Scalar1245 = Scalar1245 * mv2,
            Scalar1345 = Scalar1345 * mv2,
            Scalar2345 = Scalar2345 * mv2,
            Scalar1236 = Scalar1236 * mv2,
            Scalar1246 = Scalar1246 * mv2,
            Scalar1346 = Scalar1346 * mv2,
            Scalar2346 = Scalar2346 * mv2,
            Scalar1256 = Scalar1256 * mv2,
            Scalar1356 = Scalar1356 * mv2,
            Scalar2356 = Scalar2356 * mv2,
            Scalar1456 = Scalar1456 * mv2,
            Scalar2456 = Scalar2456 * mv2,
            Scalar3456 = Scalar3456 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 Negative()
    {
        return new Ga51KVector4()
        {
            Scalar1234 = -Scalar1234,
            Scalar1235 = -Scalar1235,
            Scalar1245 = -Scalar1245,
            Scalar1345 = -Scalar1345,
            Scalar2345 = -Scalar2345,
            Scalar1236 = -Scalar1236,
            Scalar1246 = -Scalar1246,
            Scalar1346 = -Scalar1346,
            Scalar2346 = -Scalar2346,
            Scalar1256 = -Scalar1256,
            Scalar1356 = -Scalar1356,
            Scalar2356 = -Scalar2356,
            Scalar1456 = -Scalar1456,
            Scalar2456 = -Scalar2456,
            Scalar3456 = -Scalar3456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 Conjugate()
    {
        return new Ga51KVector4()
               {
                   Scalar1234 = -Scalar1234,
                   Scalar1235 = -Scalar1235,
                   Scalar1245 = -Scalar1245,
                   Scalar1345 = -Scalar1345,
                   Scalar2345 = Scalar2345,
                   Scalar1236 = -Scalar1236,
                   Scalar1246 = -Scalar1246,
                   Scalar1346 = -Scalar1346,
                   Scalar2346 = Scalar2346,
                   Scalar1256 = -Scalar1256,
                   Scalar1356 = -Scalar1356,
                   Scalar2356 = Scalar2356,
                   Scalar1456 = -Scalar1456,
                   Scalar2456 = Scalar2456,
                   Scalar3456 = Scalar3456
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector2 Dual()
    {
        return new Ga51KVector2()
               {
                   Scalar12 = Scalar3456,
                   Scalar13 = -Scalar2456,
                   Scalar23 = -Scalar1456,
                   Scalar14 = Scalar2356,
                   Scalar24 = Scalar1356,
                   Scalar34 = -Scalar1256,
                   Scalar15 = -Scalar2346,
                   Scalar25 = -Scalar1346,
                   Scalar35 = Scalar1246,
                   Scalar45 = -Scalar1236,
                   Scalar16 = Scalar2345,
                   Scalar26 = Scalar1345,
                   Scalar36 = -Scalar1245,
                   Scalar46 = Scalar1235,
                   Scalar56 = -Scalar1234
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector2 UnDual()
    {
        return new Ga51KVector2()
               {
                   Scalar12 = Scalar3456,
                   Scalar13 = -Scalar2456,
                   Scalar23 = -Scalar1456,
                   Scalar14 = Scalar2356,
                   Scalar24 = Scalar1356,
                   Scalar34 = -Scalar1256,
                   Scalar15 = -Scalar2346,
                   Scalar25 = -Scalar1346,
                   Scalar35 = Scalar1246,
                   Scalar45 = -Scalar1236,
                   Scalar16 = Scalar2345,
                   Scalar26 = Scalar1345,
                   Scalar36 = -Scalar1245,
                   Scalar46 = Scalar1235,
                   Scalar56 = -Scalar1234
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector0 Dual(Ga51KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector0 UnDual(Ga51KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector1 Dual(Ga51KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector1 UnDual(Ga51KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector2 Dual(Ga51KVector6 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector2 UnDual(Ga51KVector6 kv2)
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
            Ga51KVector3.Zero,
            this,
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
            Ga51KVector3.Zero,
            this,
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
            Ga51KVector3.Zero,
            this,
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Add(Ga51KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2);
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            Ga51KVector1.Zero,
            Ga51KVector2.Zero,
            mv2,
            this,
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 Add(Ga51KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga51KVector4()
        {
            Scalar1234 = Scalar1234 + mv2.Scalar1234,
            Scalar1235 = Scalar1235 + mv2.Scalar1235,
            Scalar1245 = Scalar1245 + mv2.Scalar1245,
            Scalar1345 = Scalar1345 + mv2.Scalar1345,
            Scalar2345 = Scalar2345 + mv2.Scalar2345,
            Scalar1236 = Scalar1236 + mv2.Scalar1236,
            Scalar1246 = Scalar1246 + mv2.Scalar1246,
            Scalar1346 = Scalar1346 + mv2.Scalar1346,
            Scalar2346 = Scalar2346 + mv2.Scalar2346,
            Scalar1256 = Scalar1256 + mv2.Scalar1256,
            Scalar1356 = Scalar1356 + mv2.Scalar1356,
            Scalar2356 = Scalar2356 + mv2.Scalar2356,
            Scalar1456 = Scalar1456 + mv2.Scalar1456,
            Scalar2456 = Scalar2456 + mv2.Scalar2456,
            Scalar3456 = Scalar3456 + mv2.Scalar3456
        };
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
            Ga51KVector3.Zero,
            this,
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
            Ga51KVector3.Zero,
            this,
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
            mv2.KVector3,
            Add(mv2.KVector4),
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
            Ga51KVector3.Zero,
            this,
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
            Ga51KVector3.Zero,
            this,
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
            Ga51KVector3.Zero,
            this,
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51Multivector Subtract(Ga51KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga51Multivector.Create(this);
        if (IsZero()) return Ga51Multivector.Create(mv2.Negative());
        
        return Ga51Multivector.Create(
            Ga51KVector0.Zero,
            Ga51KVector1.Zero,
            Ga51KVector2.Zero,
            mv2.Negative(),
            this,
            Ga51KVector5.Zero,
            Ga51KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51KVector4 Subtract(Ga51KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga51KVector4()
        {
            Scalar1234 = Scalar1234 - mv2.Scalar1234,
            Scalar1235 = Scalar1235 - mv2.Scalar1235,
            Scalar1245 = Scalar1245 - mv2.Scalar1245,
            Scalar1345 = Scalar1345 - mv2.Scalar1345,
            Scalar2345 = Scalar2345 - mv2.Scalar2345,
            Scalar1236 = Scalar1236 - mv2.Scalar1236,
            Scalar1246 = Scalar1246 - mv2.Scalar1246,
            Scalar1346 = Scalar1346 - mv2.Scalar1346,
            Scalar2346 = Scalar2346 - mv2.Scalar2346,
            Scalar1256 = Scalar1256 - mv2.Scalar1256,
            Scalar1356 = Scalar1356 - mv2.Scalar1356,
            Scalar2356 = Scalar2356 - mv2.Scalar2356,
            Scalar1456 = Scalar1456 - mv2.Scalar1456,
            Scalar2456 = Scalar2456 - mv2.Scalar2456,
            Scalar3456 = Scalar3456 - mv2.Scalar3456
        };
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
            Ga51KVector3.Zero,
            this,
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
            Ga51KVector3.Zero,
            this,
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
            mv2.KVector3.Negative(),
            Subtract(mv2.KVector4),
            mv2.KVector5.Negative(),
            mv2.KVector6.Negative()
        );
    }
    
}

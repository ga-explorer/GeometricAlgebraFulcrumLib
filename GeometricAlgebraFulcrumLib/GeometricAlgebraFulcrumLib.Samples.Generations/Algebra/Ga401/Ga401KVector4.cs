using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga401;

public sealed partial class Ga401KVector4
{
    public static Ga401KVector4 Zero { get; } = new Ga401KVector4();
    
    public static Ga401KVector4 E1234 { get; } = new Ga401KVector4() { Scalar1234 = 1d };
    
    public static Ga401KVector4 E1235 { get; } = new Ga401KVector4() { Scalar1235 = 1d };
    
    public static Ga401KVector4 E1245 { get; } = new Ga401KVector4() { Scalar1245 = 1d };
    
    public static Ga401KVector4 E1345 { get; } = new Ga401KVector4() { Scalar1345 = 1d };
    
    public static Ga401KVector4 E2345 { get; } = new Ga401KVector4() { Scalar2345 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 Create(params double[] scalarArray)
    {
        return new Ga401KVector4
        {
            Scalar1234 = scalarArray[0],
            Scalar1235 = scalarArray[1],
            Scalar1245 = scalarArray[2],
            Scalar1345 = scalarArray[3],
            Scalar2345 = scalarArray[4]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 operator +(Ga401KVector4 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 operator -(Ga401KVector4 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 operator *(Ga401KVector4 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 operator *(double mv1, Ga401KVector4 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 operator /(Ga401KVector4 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector4 mv1, Ga401KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector4 mv1, Ga401KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector4 mv1, Ga401KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector4 mv1, Ga401KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector4 mv1, Ga401KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector4 mv1, Ga401KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector4 mv1, Ga401KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector4 mv1, Ga401KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 operator +(Ga401KVector4 mv1, Ga401KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 operator -(Ga401KVector4 mv1, Ga401KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector4 mv1, Ga401Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector4 mv1, Ga401Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(Ga401KVector4 mv1, double mv2)
    {
        return Ga401Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator +(double mv1, Ga401KVector4 mv2)
    {
        return Ga401Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(Ga401KVector4 mv1, double mv2)
    {
        return Ga401Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector operator -(double mv1, Ga401KVector4 mv2)
    {
        return Ga401Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar1234 { get; init; }
    
    public double Scalar1235 { get; init; }
    
    public double Scalar1245 { get; init; }
    
    public double Scalar1345 { get; init; }
    
    public double Scalar2345 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4()
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
            !double.IsNaN(Scalar2345);
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
            Scalar2345 == 0d;
    
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
        
        scalarArray[15] = Scalar1234;
        scalarArray[23] = Scalar1235;
        scalarArray[27] = Scalar1245;
        scalarArray[29] = Scalar1345;
        scalarArray[30] = Scalar2345;
        
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
            Scalar2345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 Times(double mv2)
    {
        return new Ga401KVector4()
        {
            Scalar1234 = Scalar1234 * mv2,
            Scalar1235 = Scalar1235 * mv2,
            Scalar1245 = Scalar1245 * mv2,
            Scalar1345 = Scalar1345 * mv2,
            Scalar2345 = Scalar2345 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 Negative()
    {
        return new Ga401KVector4()
        {
            Scalar1234 = -Scalar1234,
            Scalar1235 = -Scalar1235,
            Scalar1245 = -Scalar1245,
            Scalar1345 = -Scalar1345,
            Scalar2345 = -Scalar2345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 CliffordConjugate()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 Conjugate()
    {
        return new Ga401KVector4()
               {
                   Scalar2345 = Scalar2345
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector1 UnDual()
    {
        return new Ga401KVector1()
               {
                   Scalar1 = Scalar2345
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector0 Dual(Ga401KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector0 UnDual(Ga401KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector1 Dual(Ga401KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector1 UnDual(Ga401KVector5 kv2)
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
            Ga401KVector3.Zero,
            this,
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
            Ga401KVector3.Zero,
            this,
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
            Ga401KVector3.Zero,
            this,
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Add(Ga401KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2);
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            mv2,
            this,
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 Add(Ga401KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga401KVector4()
        {
            Scalar1234 = Scalar1234 + mv2.Scalar1234,
            Scalar1235 = Scalar1235 + mv2.Scalar1235,
            Scalar1245 = Scalar1245 + mv2.Scalar1245,
            Scalar1345 = Scalar1345 + mv2.Scalar1345,
            Scalar2345 = Scalar2345 + mv2.Scalar2345
        };
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
            Ga401KVector3.Zero,
            this,
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
            mv2.KVector3,
            Add(mv2.KVector4),
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
            Ga401KVector3.Zero,
            this,
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
            Ga401KVector3.Zero,
            this,
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
            Ga401KVector3.Zero,
            this,
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401Multivector Subtract(Ga401KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga401Multivector.Create(this);
        if (IsZero()) return Ga401Multivector.Create(mv2.Negative());
        
        return Ga401Multivector.Create(
            Ga401KVector0.Zero,
            Ga401KVector1.Zero,
            Ga401KVector2.Zero,
            mv2.Negative(),
            this,
            Ga401KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga401KVector4 Subtract(Ga401KVector4 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga401KVector4()
        {
            Scalar1234 = Scalar1234 - mv2.Scalar1234,
            Scalar1235 = Scalar1235 - mv2.Scalar1235,
            Scalar1245 = Scalar1245 - mv2.Scalar1245,
            Scalar1345 = Scalar1345 - mv2.Scalar1345,
            Scalar2345 = Scalar2345 - mv2.Scalar2345
        };
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
            Ga401KVector3.Zero,
            this,
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
            mv2.KVector3.Negative(),
            Subtract(mv2.KVector4),
            mv2.KVector5.Negative()
        );
    }
    
}

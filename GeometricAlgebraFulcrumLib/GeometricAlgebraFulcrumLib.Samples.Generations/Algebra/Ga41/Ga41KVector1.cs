using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga41;

public sealed partial class Ga41KVector1
{
    public static Ga41KVector1 Zero { get; } = new Ga41KVector1();
    
    public static Ga41KVector1 E1 { get; } = new Ga41KVector1() { Scalar1 = 1d };
    
    public static Ga41KVector1 E2 { get; } = new Ga41KVector1() { Scalar2 = 1d };
    
    public static Ga41KVector1 E3 { get; } = new Ga41KVector1() { Scalar3 = 1d };
    
    public static Ga41KVector1 E4 { get; } = new Ga41KVector1() { Scalar4 = 1d };
    
    public static Ga41KVector1 E5 { get; } = new Ga41KVector1() { Scalar5 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 Create(params double[] scalarArray)
    {
        return new Ga41KVector1
        {
            Scalar1 = scalarArray[0],
            Scalar2 = scalarArray[1],
            Scalar3 = scalarArray[2],
            Scalar4 = scalarArray[3],
            Scalar5 = scalarArray[4]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 operator +(Ga41KVector1 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 operator -(Ga41KVector1 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 operator *(Ga41KVector1 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 operator *(double mv1, Ga41KVector1 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 operator /(Ga41KVector1 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector1 mv1, Ga41KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector1 mv1, Ga41KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 operator +(Ga41KVector1 mv1, Ga41KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 operator -(Ga41KVector1 mv1, Ga41KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector1 mv1, Ga41KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector1 mv1, Ga41KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector1 mv1, Ga41KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector1 mv1, Ga41KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector1 mv1, Ga41KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector1 mv1, Ga41KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector1 mv1, Ga41Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector1 mv1, Ga41Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(Ga41KVector1 mv1, double mv2)
    {
        return Ga41Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator +(double mv1, Ga41KVector1 mv2)
    {
        return Ga41Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(Ga41KVector1 mv1, double mv2)
    {
        return Ga41Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41Multivector operator -(double mv1, Ga41KVector1 mv2)
    {
        return Ga41Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar1 { get; init; }
    
    public double Scalar2 { get; init; }
    
    public double Scalar3 { get; init; }
    
    public double Scalar4 { get; init; }
    
    public double Scalar5 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar1) &&
            !double.IsNaN(Scalar2) &&
            !double.IsNaN(Scalar3) &&
            !double.IsNaN(Scalar4) &&
            !double.IsNaN(Scalar5);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar1 == 0d &&
            Scalar2 == 0d &&
            Scalar3 == 0d &&
            Scalar4 == 0d &&
            Scalar5 == 0d;
    
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
        
        scalarArray[1] = Scalar1;
        scalarArray[2] = Scalar2;
        scalarArray[4] = Scalar3;
        scalarArray[8] = Scalar4;
        scalarArray[16] = Scalar5;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar1,
            Scalar2,
            Scalar3,
            Scalar4,
            Scalar5
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 Times(double mv2)
    {
        return new Ga41KVector1()
        {
            Scalar1 = Scalar1 * mv2,
            Scalar2 = Scalar2 * mv2,
            Scalar3 = Scalar3 * mv2,
            Scalar4 = Scalar4 * mv2,
            Scalar5 = Scalar5 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 Negative()
    {
        return new Ga41KVector1()
        {
            Scalar1 = -Scalar1,
            Scalar2 = -Scalar2,
            Scalar3 = -Scalar3,
            Scalar4 = -Scalar4,
            Scalar5 = -Scalar5
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 Conjugate()
    {
        return new Ga41KVector1()
               {
                   Scalar1 = -Scalar1,
                   Scalar2 = Scalar2,
                   Scalar3 = Scalar3,
                   Scalar4 = Scalar4,
                   Scalar5 = Scalar5
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector4 Dual()
    {
        return new Ga41KVector4()
               {
                   Scalar1234 = -Scalar5,
                   Scalar1235 = Scalar4,
                   Scalar1245 = -Scalar3,
                   Scalar1345 = Scalar2,
                   Scalar2345 = Scalar1
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector4 UnDual()
    {
        return new Ga41KVector4()
               {
                   Scalar1234 = Scalar5,
                   Scalar1235 = -Scalar4,
                   Scalar1245 = Scalar3,
                   Scalar1345 = -Scalar2,
                   Scalar2345 = -Scalar1
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 Dual(Ga41KVector1 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector0 UnDual(Ga41KVector1 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 Dual(Ga41KVector2 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 UnDual(Ga41KVector2 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector2 Dual(Ga41KVector3 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector2 UnDual(Ga41KVector3 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector3 Dual(Ga41KVector4 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector3 UnDual(Ga41KVector4 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector4 Dual(Ga41KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector4 UnDual(Ga41KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            mv2,
            this,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 Add(Ga41KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga41KVector1()
        {
            Scalar1 = Scalar1 + mv2.Scalar1,
            Scalar2 = Scalar2 + mv2.Scalar2,
            Scalar3 = Scalar3 + mv2.Scalar3,
            Scalar4 = Scalar4 + mv2.Scalar4,
            Scalar5 = Scalar5 + mv2.Scalar5
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            this,
            mv2,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            this,
            Ga41KVector2.Zero,
            mv2,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            this,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            mv2,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2);
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            this,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Add(Ga41Multivector mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga41Multivector.Create(
            mv2.KVector0,
            Add(mv2.KVector1),
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4,
            mv2.KVector5
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            mv2.Negative(),
            this,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41KVector1 Subtract(Ga41KVector1 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga41KVector1()
        {
            Scalar1 = Scalar1 - mv2.Scalar1,
            Scalar2 = Scalar2 - mv2.Scalar2,
            Scalar3 = Scalar3 - mv2.Scalar3,
            Scalar4 = Scalar4 - mv2.Scalar4,
            Scalar5 = Scalar5 - mv2.Scalar5
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            this,
            mv2.Negative(),
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            this,
            Ga41KVector2.Zero,
            mv2.Negative(),
            Ga41KVector4.Zero,
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            this,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            mv2.Negative(),
            Ga41KVector5.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41KVector5 mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return Ga41Multivector.Create(mv2.Negative());
        
        return Ga41Multivector.Create(
            Ga41KVector0.Zero,
            this,
            Ga41KVector2.Zero,
            Ga41KVector3.Zero,
            Ga41KVector4.Zero,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga41Multivector Subtract(Ga41Multivector mv2)
    {
        if (mv2.IsZero()) return Ga41Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga41Multivector.Create(
            mv2.KVector0.Negative(),
            Subtract(mv2.KVector1),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative(),
            mv2.KVector5.Negative()
        );
    }
    
}

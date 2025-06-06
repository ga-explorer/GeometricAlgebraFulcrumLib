using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga33;

public sealed partial class Ga33KVector5
{
    public static Ga33KVector5 Zero { get; } = new Ga33KVector5();
    
    public static Ga33KVector5 E12345 { get; } = new Ga33KVector5() { Scalar12345 = 1d };
    
    public static Ga33KVector5 E12346 { get; } = new Ga33KVector5() { Scalar12346 = 1d };
    
    public static Ga33KVector5 E12356 { get; } = new Ga33KVector5() { Scalar12356 = 1d };
    
    public static Ga33KVector5 E12456 { get; } = new Ga33KVector5() { Scalar12456 = 1d };
    
    public static Ga33KVector5 E13456 { get; } = new Ga33KVector5() { Scalar13456 = 1d };
    
    public static Ga33KVector5 E23456 { get; } = new Ga33KVector5() { Scalar23456 = 1d };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector5 Create(params double[] scalarArray)
    {
        return new Ga33KVector5
        {
            Scalar12345 = scalarArray[0],
            Scalar12346 = scalarArray[1],
            Scalar12356 = scalarArray[2],
            Scalar12456 = scalarArray[3],
            Scalar13456 = scalarArray[4],
            Scalar23456 = scalarArray[5]
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector5 operator +(Ga33KVector5 mv)
    {
        return mv;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector5 operator -(Ga33KVector5 mv)
    {
        return mv.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector5 operator *(Ga33KVector5 mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector5 operator *(double mv1, Ga33KVector5 mv2)
    {
        return mv2.Times(mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector5 operator /(Ga33KVector5 mv1, double mv2)
    {
        return mv1.Times(1d / mv2);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector5 mv1, Ga33KVector0 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector5 mv1, Ga33KVector0 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector5 mv1, Ga33KVector1 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector5 mv1, Ga33KVector1 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector5 mv1, Ga33KVector2 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector5 mv1, Ga33KVector2 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector5 mv1, Ga33KVector3 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector5 mv1, Ga33KVector3 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector5 mv1, Ga33KVector4 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector5 mv1, Ga33KVector4 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector5 operator +(Ga33KVector5 mv1, Ga33KVector5 mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33KVector5 operator -(Ga33KVector5 mv1, Ga33KVector5 mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector5 mv1, Ga33Multivector mv2)
    {
        return mv1.Add(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector5 mv1, Ga33Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(Ga33KVector5 mv1, double mv2)
    {
        return Ga33Multivector.Create(mv1, mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator +(double mv1, Ga33KVector5 mv2)
    {
        return Ga33Multivector.Create(mv2, mv1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(Ga33KVector5 mv1, double mv2)
    {
        return Ga33Multivector.Create(mv1, -mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga33Multivector operator -(double mv1, Ga33KVector5 mv2)
    {
        return Ga33Multivector.Create(mv2.Negative(), mv1);
    }
    
    public double Scalar12345 { get; init; }
    
    public double Scalar12346 { get; init; }
    
    public double Scalar12356 { get; init; }
    
    public double Scalar12456 { get; init; }
    
    public double Scalar13456 { get; init; }
    
    public double Scalar23456 { get; init; }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5()
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return
            !double.IsNaN(Scalar12345) &&
            !double.IsNaN(Scalar12346) &&
            !double.IsNaN(Scalar12356) &&
            !double.IsNaN(Scalar12456) &&
            !double.IsNaN(Scalar13456) &&
            !double.IsNaN(Scalar23456);
    }
    
    private bool? _isZero;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        _isZero ??= 
            Scalar12345 == 0d &&
            Scalar12346 == 0d &&
            Scalar12356 == 0d &&
            Scalar12456 == 0d &&
            Scalar13456 == 0d &&
            Scalar23456 == 0d;
    
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
        
        scalarArray[31] = Scalar12345;
        scalarArray[47] = Scalar12346;
        scalarArray[55] = Scalar12356;
        scalarArray[59] = Scalar12456;
        scalarArray[61] = Scalar13456;
        scalarArray[62] = Scalar23456;
        
        return scalarArray;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetKVectorArray()
    {
        return new double[]
        {
            Scalar12345,
            Scalar12346,
            Scalar12356,
            Scalar12456,
            Scalar13456,
            Scalar23456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 Times(double mv2)
    {
        return new Ga33KVector5()
        {
            Scalar12345 = Scalar12345 * mv2,
            Scalar12346 = Scalar12346 * mv2,
            Scalar12356 = Scalar12356 * mv2,
            Scalar12456 = Scalar12456 * mv2,
            Scalar13456 = Scalar13456 * mv2,
            Scalar23456 = Scalar23456 * mv2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 Divide(double mv2)
    {
        return Times(1d / mv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 DivideByNorm()
    {
        return Times(1d / this.Norm());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 DivideByNormSquared()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 DivideBySpSquared()
    {
        return Times(1d / this.SpSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 Negative()
    {
        return new Ga33KVector5()
        {
            Scalar12345 = -Scalar12345,
            Scalar12346 = -Scalar12346,
            Scalar12356 = -Scalar12356,
            Scalar12456 = -Scalar12456,
            Scalar13456 = -Scalar13456,
            Scalar23456 = -Scalar23456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 Reverse()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 GradeInvolution()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 CliffordConjugate()
    {
        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 Inverse()
    {
        return Times(1d / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 InverseTimes(double mv2)
    {
        return Times(mv2 / this.NormSquared());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 PseudoInverse()
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            1d / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 PseudoInverseTimes(double mv2)
    {
        var conjugate = Conjugate();
    
        return conjugate.Times(
            mv2 / conjugate.Sp(this).Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 Conjugate()
    {
        return new Ga33KVector5()
               {
                   Scalar12345 = -Scalar12345,
                   Scalar12346 = -Scalar12346,
                   Scalar12356 = -Scalar12356,
                   Scalar12456 = Scalar12456,
                   Scalar13456 = Scalar13456,
                   Scalar23456 = Scalar23456
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector1 Dual()
    {
        return new Ga33KVector1()
               {
                   Scalar1 = -Scalar23456,
                   Scalar2 = Scalar13456,
                   Scalar3 = -Scalar12456,
                   Scalar4 = -Scalar12356,
                   Scalar5 = Scalar12346,
                   Scalar6 = -Scalar12345
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector1 UnDual()
    {
        return new Ga33KVector1()
               {
                   Scalar1 = -Scalar23456,
                   Scalar2 = Scalar13456,
                   Scalar3 = -Scalar12456,
                   Scalar4 = -Scalar12356,
                   Scalar5 = Scalar12346,
                   Scalar6 = -Scalar12345
               };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 Dual(Ga33KVector5 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector0 UnDual(Ga33KVector5 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector1 Dual(Ga33KVector6 kv2)
    {
        return this.Lcp(kv2.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector1 UnDual(Ga33KVector6 kv2)
    {
        return this.Lcp(kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            mv2,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            this,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            mv2,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            this,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            mv2,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            this,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            mv2,
            Ga33KVector4.Zero,
            this,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            mv2,
            this,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 Add(Ga33KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2;
        
        return new Ga33KVector5()
        {
            Scalar12345 = Scalar12345 + mv2.Scalar12345,
            Scalar12346 = Scalar12346 + mv2.Scalar12346,
            Scalar12356 = Scalar12356 + mv2.Scalar12356,
            Scalar12456 = Scalar12456 + mv2.Scalar12456,
            Scalar13456 = Scalar13456 + mv2.Scalar13456,
            Scalar23456 = Scalar23456 + mv2.Scalar23456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33KVector6 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2);
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            this,
            mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Add(Ga33Multivector mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return mv2;
        
        return Ga33Multivector.Create(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3,
            mv2.KVector4,
            Add(mv2.KVector5),
            mv2.KVector6
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector0 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            mv2.Negative(),
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            this,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector1 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            mv2.Negative(),
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            this,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector2 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            mv2.Negative(),
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            this,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector3 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            mv2.Negative(),
            Ga33KVector4.Zero,
            this,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector4 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            mv2.Negative(),
            this,
            Ga33KVector6.Zero
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33KVector5 Subtract(Ga33KVector5 mv2)
    {
        if (mv2.IsZero()) return this;
        if (IsZero()) return mv2.Negative();
        
        return new Ga33KVector5()
        {
            Scalar12345 = Scalar12345 - mv2.Scalar12345,
            Scalar12346 = Scalar12346 - mv2.Scalar12346,
            Scalar12356 = Scalar12356 - mv2.Scalar12356,
            Scalar12456 = Scalar12456 - mv2.Scalar12456,
            Scalar13456 = Scalar13456 - mv2.Scalar13456,
            Scalar23456 = Scalar23456 - mv2.Scalar23456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33KVector6 mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return Ga33Multivector.Create(mv2.Negative());
        
        return Ga33Multivector.Create(
            Ga33KVector0.Zero,
            Ga33KVector1.Zero,
            Ga33KVector2.Zero,
            Ga33KVector3.Zero,
            Ga33KVector4.Zero,
            this,
            mv2.Negative()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga33Multivector Subtract(Ga33Multivector mv2)
    {
        if (mv2.IsZero()) return Ga33Multivector.Create(this);
        if (IsZero()) return mv2.Negative();
        
        return Ga33Multivector.Create(
            mv2.KVector0.Negative(),
            mv2.KVector1.Negative(),
            mv2.KVector2.Negative(),
            mv2.KVector3.Negative(),
            mv2.KVector4.Negative(),
            Subtract(mv2.KVector5),
            mv2.KVector6.Negative()
        );
    }
    
}

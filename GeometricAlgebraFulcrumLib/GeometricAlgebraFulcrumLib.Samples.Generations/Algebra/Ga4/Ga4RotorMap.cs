using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga4;

public static class Ga4RotorMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 MapUsingRotor(this Ga4KVector0 mv1, Ga4KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector0.Zero;
        
        return new Ga4KVector0
        {
            Scalar = mv2.Scalar * mv1.Scalar * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 MapUsingRotor(this Ga4KVector0 mv1, Ga4KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector0.Zero;
        
        return new Ga4KVector0
        {
            Scalar = mv2.Scalar12 * mv1.Scalar * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar * mv2.Scalar23 + mv2.Scalar14 * mv1.Scalar * mv2.Scalar14 + mv2.Scalar24 * mv1.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar * mv2.Scalar34
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector0 MapUsingRotor(this Ga4KVector0 mv1, Ga4KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector0.Zero;
        
        return new Ga4KVector0
        {
            Scalar = mv2.Scalar1234 * mv1.Scalar * mv2.Scalar1234
        };
    }
    
    public static Ga4KVector0 MapUsingRotor(this Ga4KVector0 mv1, Ga4Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv2.KVector0.Scalar * mv1.Scalar * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar += mv2.KVector2.Scalar12 * mv1.Scalar * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar13 * mv1.Scalar * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.Scalar * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar14 * mv1.Scalar * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar24 * mv1.Scalar * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar += mv2.KVector4.Scalar1234 * mv1.Scalar * mv2.KVector4.Scalar1234;
        }
        
        return new Ga4KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector1 MapUsingRotor(this Ga4KVector1 mv1, Ga4KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector1.Zero;
        
        return new Ga4KVector1
        {
            Scalar1 = mv2.Scalar * mv1.Scalar1 * mv2.Scalar,
            Scalar2 = mv2.Scalar * mv1.Scalar2 * mv2.Scalar,
            Scalar3 = mv2.Scalar * mv1.Scalar3 * mv2.Scalar,
            Scalar4 = mv2.Scalar * mv1.Scalar4 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector1 MapUsingRotor(this Ga4KVector1 mv1, Ga4KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector1.Zero;
        
        return new Ga4KVector1
        {
            Scalar1 = mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar4 * mv2.Scalar24 + mv2.Scalar13 * mv1.Scalar1 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar4 * mv2.Scalar34 + mv2.Scalar14 * mv1.Scalar1 * mv2.Scalar14 + mv2.Scalar24 * mv1.Scalar1 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar1 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar2 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar3 * mv2.Scalar34,
            Scalar2 = mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar2 * mv2.Scalar24 - mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar13 - mv2.Scalar14 * mv1.Scalar2 * mv2.Scalar14 - 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.Scalar4 * mv2.Scalar14 + 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar34 + mv2.Scalar34 * mv1.Scalar2 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar34,
            Scalar3 = mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar3 * mv2.Scalar13 - mv2.Scalar14 * mv1.Scalar3 * mv2.Scalar14 - 2 * mv2.Scalar13 * mv1.Scalar4 * mv2.Scalar14 + mv2.Scalar34 * mv1.Scalar3 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar24,
            Scalar4 = mv2.Scalar12 * mv1.Scalar4 * mv2.Scalar12 - mv2.Scalar34 * mv1.Scalar4 * mv2.Scalar34 + mv2.Scalar13 * mv1.Scalar4 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar23 - mv2.Scalar14 * mv1.Scalar4 * mv2.Scalar14 - mv2.Scalar24 * mv1.Scalar4 * mv2.Scalar24
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector1 MapUsingRotor(this Ga4KVector1 mv1, Ga4KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector1.Zero;
        
        return new Ga4KVector1
        {
            Scalar1 = -mv2.Scalar1234 * mv1.Scalar1 * mv2.Scalar1234,
            Scalar2 = -mv2.Scalar1234 * mv1.Scalar2 * mv2.Scalar1234,
            Scalar3 = -mv2.Scalar1234 * mv1.Scalar3 * mv2.Scalar1234,
            Scalar4 = -mv2.Scalar1234 * mv1.Scalar4 * mv2.Scalar1234
        };
    }
    
    public static Ga4KVector1 MapUsingRotor(this Ga4KVector1 mv1, Ga4Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector1.Zero;
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        var tempScalar2 = 0d;
        var tempScalar3 = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar0 += mv2.KVector0.Scalar * mv1.Scalar1 * mv2.KVector0.Scalar;
            tempScalar1 += mv2.KVector0.Scalar * mv1.Scalar2 * mv2.KVector0.Scalar;
            tempScalar2 += mv2.KVector0.Scalar * mv1.Scalar3 * mv2.KVector0.Scalar;
            tempScalar3 += mv2.KVector0.Scalar * mv1.Scalar4 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector0.Scalar * mv1.Scalar2 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.Scalar3 * mv2.KVector2.Scalar13 + 2 * mv2.KVector0.Scalar * mv1.Scalar4 * mv2.KVector2.Scalar14;
            tempScalar1 += -2 * mv2.KVector0.Scalar * mv1.Scalar1 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.Scalar3 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.Scalar4 * mv2.KVector2.Scalar24;
            tempScalar2 += -2 * mv2.KVector0.Scalar * mv1.Scalar1 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.Scalar4 * mv2.KVector2.Scalar34;
            tempScalar3 += -2 * mv2.KVector0.Scalar * mv1.Scalar1 * mv2.KVector2.Scalar14 - 2 * mv2.KVector0.Scalar * mv1.Scalar2 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.Scalar3 * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar0 += mv2.KVector2.Scalar12 * mv1.Scalar1 * mv2.KVector2.Scalar12 + 2 * mv2.KVector2.Scalar12 * mv1.Scalar3 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar12 * mv1.Scalar4 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar13 * mv1.Scalar1 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.Scalar1 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.Scalar4 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar14 * mv1.Scalar1 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar24 * mv1.Scalar1 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar1 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar14 * mv1.Scalar2 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar14 * mv1.Scalar3 * mv2.KVector2.Scalar34;
            tempScalar1 += -mv2.KVector2.Scalar12 * mv1.Scalar2 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.Scalar2 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar14 * mv1.Scalar2 * mv2.KVector2.Scalar14 - 2 * mv2.KVector2.Scalar12 * mv1.Scalar3 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.Scalar4 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar23 * mv1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar23 * mv1.Scalar4 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.Scalar2 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar2 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar24 * mv1.Scalar3 * mv2.KVector2.Scalar34;
            tempScalar2 += mv2.KVector2.Scalar12 * mv1.Scalar3 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.Scalar3 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar14 * mv1.Scalar3 * mv2.KVector2.Scalar14 - 2 * mv2.KVector2.Scalar13 * mv1.Scalar4 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar23 * mv1.Scalar3 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.Scalar3 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar4 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar3 * mv2.KVector2.Scalar34;
            tempScalar3 += mv2.KVector2.Scalar12 * mv1.Scalar4 * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar13 * mv1.Scalar4 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.Scalar4 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar14 * mv1.Scalar4 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar24 * mv1.Scalar4 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.Scalar4 * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector2.IsZero() && !mv2.KVector4.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector2.Scalar34 * mv1.Scalar2 * mv2.KVector4.Scalar1234 + 2 * mv2.KVector2.Scalar23 * mv1.Scalar4 * mv2.KVector4.Scalar1234;
            tempScalar1 += -2 * mv2.KVector2.Scalar13 * mv1.Scalar4 * mv2.KVector4.Scalar1234;
            tempScalar2 += 2 * mv2.KVector2.Scalar12 * mv1.Scalar4 * mv2.KVector4.Scalar1234 - 2 * mv2.KVector2.Scalar14 * mv1.Scalar2 * mv2.KVector4.Scalar1234;
            tempScalar3 += 2 * mv2.KVector2.Scalar13 * mv1.Scalar2 * mv2.KVector4.Scalar1234;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar0 += -mv2.KVector4.Scalar1234 * mv1.Scalar1 * mv2.KVector4.Scalar1234;
            tempScalar1 += -mv2.KVector4.Scalar1234 * mv1.Scalar2 * mv2.KVector4.Scalar1234;
            tempScalar2 += -mv2.KVector4.Scalar1234 * mv1.Scalar3 * mv2.KVector4.Scalar1234;
            tempScalar3 += -mv2.KVector4.Scalar1234 * mv1.Scalar4 * mv2.KVector4.Scalar1234;
        }
        
        return new Ga4KVector1
        {
            Scalar1 = tempScalar0,
            Scalar2 = tempScalar1,
            Scalar3 = tempScalar2,
            Scalar4 = tempScalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector2 MapUsingRotor(this Ga4KVector2 mv1, Ga4KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector2.Zero;
        
        return new Ga4KVector2
        {
            Scalar12 = mv2.Scalar * mv1.Scalar12 * mv2.Scalar,
            Scalar13 = mv2.Scalar * mv1.Scalar13 * mv2.Scalar,
            Scalar23 = mv2.Scalar * mv1.Scalar23 * mv2.Scalar,
            Scalar14 = mv2.Scalar * mv1.Scalar14 * mv2.Scalar,
            Scalar24 = mv2.Scalar * mv1.Scalar24 * mv2.Scalar,
            Scalar34 = mv2.Scalar * mv1.Scalar34 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector2 MapUsingRotor(this Ga4KVector2 mv1, Ga4KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector2.Zero;
        
        return new Ga4KVector2
        {
            Scalar12 = -mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar12 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.Scalar14 * mv2.Scalar14 + mv2.Scalar24 * mv1.Scalar12 * mv2.Scalar24 + mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23 - mv2.Scalar14 * mv1.Scalar12 * mv2.Scalar14 + 2 * mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar34 + mv2.Scalar34 * mv1.Scalar12 * mv2.Scalar34,
            Scalar13 = mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.Scalar24 * mv2.Scalar34 - mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23 - mv2.Scalar14 * mv1.Scalar13 * mv2.Scalar14 - 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.Scalar14 * mv2.Scalar14 - 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar13 * mv2.Scalar34 - 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar14 - mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar24,
            Scalar23 = -2 * mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.Scalar12 * mv2.Scalar34 - mv2.Scalar12 * mv1.Scalar23 * mv2.Scalar12 - mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 + mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar14 + mv2.Scalar14 * mv1.Scalar23 * mv2.Scalar14 - mv2.Scalar24 * mv1.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar23 * mv2.Scalar34,
            Scalar14 = mv2.Scalar12 * mv1.Scalar14 * mv2.Scalar12 - mv2.Scalar14 * mv1.Scalar14 * mv2.Scalar14 + 2 * mv2.Scalar12 * mv1.Scalar34 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.Scalar34 * mv2.Scalar34 + mv2.Scalar13 * mv1.Scalar14 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar14 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar14 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar24 * mv2.Scalar24,
            Scalar24 = -2 * mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar34 - mv2.Scalar12 * mv1.Scalar24 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar13 - mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.Scalar34 * mv2.Scalar13 - 2 * mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.Scalar12 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar24 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar14 + mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar23 + mv2.Scalar14 * mv1.Scalar24 * mv2.Scalar14,
            Scalar34 = 2 * mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar24 + 2 * mv2.Scalar12 * mv1.Scalar23 * mv2.Scalar14 + mv2.Scalar12 * mv1.Scalar34 * mv2.Scalar12 - 2 * mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar34 - mv2.Scalar13 * mv1.Scalar34 * mv2.Scalar13 + mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar34 * mv2.Scalar23 + mv2.Scalar14 * mv1.Scalar34 * mv2.Scalar14 - mv2.Scalar34 * mv1.Scalar34 * mv2.Scalar34
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector2 MapUsingRotor(this Ga4KVector2 mv1, Ga4KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector2.Zero;
        
        return new Ga4KVector2
        {
            Scalar12 = mv2.Scalar1234 * mv1.Scalar12 * mv2.Scalar1234,
            Scalar13 = mv2.Scalar1234 * mv1.Scalar13 * mv2.Scalar1234,
            Scalar23 = mv2.Scalar1234 * mv1.Scalar23 * mv2.Scalar1234,
            Scalar14 = mv2.Scalar1234 * mv1.Scalar14 * mv2.Scalar1234,
            Scalar24 = mv2.Scalar1234 * mv1.Scalar24 * mv2.Scalar1234,
            Scalar34 = mv2.Scalar1234 * mv1.Scalar34 * mv2.Scalar1234
        };
    }
    
    public static Ga4KVector2 MapUsingRotor(this Ga4KVector2 mv1, Ga4Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector2.Zero;
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        var tempScalar2 = 0d;
        var tempScalar3 = 0d;
        var tempScalar4 = 0d;
        var tempScalar5 = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar0 += mv2.KVector0.Scalar * mv1.Scalar12 * mv2.KVector0.Scalar;
            tempScalar1 += mv2.KVector0.Scalar * mv1.Scalar13 * mv2.KVector0.Scalar;
            tempScalar2 += mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector0.Scalar;
            tempScalar3 += mv2.KVector0.Scalar * mv1.Scalar14 * mv2.KVector0.Scalar;
            tempScalar4 += mv2.KVector0.Scalar * mv1.Scalar24 * mv2.KVector0.Scalar;
            tempScalar5 += mv2.KVector0.Scalar * mv1.Scalar34 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector0.Scalar * mv1.Scalar13 * mv2.KVector2.Scalar23 - 2 * mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector2.Scalar13 + 2 * mv2.KVector0.Scalar * mv1.Scalar14 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.Scalar24 * mv2.KVector2.Scalar14;
            tempScalar1 += -2 * mv2.KVector0.Scalar * mv1.Scalar12 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.Scalar14 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.Scalar34 * mv2.KVector2.Scalar14;
            tempScalar2 += 2 * mv2.KVector0.Scalar * mv1.Scalar12 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.Scalar13 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.Scalar24 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.Scalar34 * mv2.KVector2.Scalar24;
            tempScalar3 += -2 * mv2.KVector0.Scalar * mv1.Scalar12 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.Scalar13 * mv2.KVector2.Scalar34 + 2 * mv2.KVector0.Scalar * mv1.Scalar24 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.Scalar34 * mv2.KVector2.Scalar13;
            tempScalar4 += 2 * mv2.KVector0.Scalar * mv1.Scalar12 * mv2.KVector2.Scalar14 - 2 * mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.Scalar14 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.Scalar34 * mv2.KVector2.Scalar23;
            tempScalar5 += 2 * mv2.KVector0.Scalar * mv1.Scalar13 * mv2.KVector2.Scalar14 + 2 * mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.Scalar14 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.Scalar24 * mv2.KVector2.Scalar23;
        }
        
        if (!mv2.KVector0.IsZero() && !mv2.KVector4.IsZero())
        {
            tempScalar0 += -2 * mv2.KVector0.Scalar * mv1.Scalar34 * mv2.KVector4.Scalar1234;
            tempScalar1 += 2 * mv2.KVector0.Scalar * mv1.Scalar24 * mv2.KVector4.Scalar1234;
            tempScalar2 += -2 * mv2.KVector0.Scalar * mv1.Scalar14 * mv2.KVector4.Scalar1234;
            tempScalar3 += -2 * mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector4.Scalar1234;
            tempScalar4 += 2 * mv2.KVector0.Scalar * mv1.Scalar13 * mv2.KVector4.Scalar1234;
            tempScalar5 += -2 * mv2.KVector0.Scalar * mv1.Scalar12 * mv2.KVector4.Scalar1234;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar0 += -mv2.KVector2.Scalar12 * mv1.Scalar12 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.Scalar12 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.Scalar13 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.Scalar14 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar24 * mv1.Scalar12 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar23 * mv1.Scalar12 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar14 * mv1.Scalar12 * mv2.KVector2.Scalar14 + 2 * mv2.KVector2.Scalar24 * mv1.Scalar13 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar23 * mv1.Scalar14 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar34 * mv1.Scalar12 * mv2.KVector2.Scalar34;
            tempScalar1 += mv2.KVector2.Scalar12 * mv1.Scalar13 * mv2.KVector2.Scalar12 + 2 * mv2.KVector2.Scalar12 * mv1.Scalar24 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar13 * mv1.Scalar13 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.Scalar13 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar14 * mv1.Scalar13 * mv2.KVector2.Scalar14 - 2 * mv2.KVector2.Scalar13 * mv1.Scalar23 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar14 * mv1.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar13 * mv1.Scalar14 * mv2.KVector2.Scalar14 - 2 * mv2.KVector2.Scalar13 * mv1.Scalar24 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar13 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar14 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar24 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar24 * mv1.Scalar13 * mv2.KVector2.Scalar24;
            tempScalar2 += -2 * mv2.KVector2.Scalar12 * mv1.Scalar12 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar14 * mv1.Scalar12 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar12 * mv1.Scalar23 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar23 * mv1.Scalar23 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar13 * mv1.Scalar23 * mv2.KVector2.Scalar13 + 2 * mv2.KVector2.Scalar13 * mv1.Scalar24 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar14 * mv1.Scalar23 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar24 * mv1.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar24 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar23 * mv2.KVector2.Scalar34;
            tempScalar3 += mv2.KVector2.Scalar12 * mv1.Scalar14 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar14 * mv1.Scalar14 * mv2.KVector2.Scalar14 + 2 * mv2.KVector2.Scalar12 * mv1.Scalar34 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar14 * mv1.Scalar34 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar13 * mv1.Scalar14 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.Scalar14 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.Scalar24 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.Scalar14 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.Scalar14 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar14 * mv1.Scalar24 * mv2.KVector2.Scalar24;
            tempScalar4 += -2 * mv2.KVector2.Scalar12 * mv1.Scalar12 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar12 * mv1.Scalar13 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar12 * mv1.Scalar24 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.Scalar24 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar24 * mv1.Scalar24 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar12 * mv1.Scalar34 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar24 * mv1.Scalar34 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar13 * mv1.Scalar12 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar13 * mv1.Scalar13 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar23 * mv1.Scalar13 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar23 * mv1.Scalar24 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar14 * mv1.Scalar24 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar34 * mv1.Scalar24 * mv2.KVector2.Scalar34;
            tempScalar5 += 2 * mv2.KVector2.Scalar12 * mv1.Scalar13 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar12 * mv1.Scalar23 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar12 * mv1.Scalar34 * mv2.KVector2.Scalar12 - 2 * mv2.KVector2.Scalar13 * mv1.Scalar13 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar13 * mv1.Scalar34 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar24 * mv1.Scalar34 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar23 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.Scalar34 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar14 * mv1.Scalar34 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar34 * mv1.Scalar34 * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector2.IsZero() && !mv2.KVector4.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector2.Scalar23 * mv1.Scalar24 * mv2.KVector4.Scalar1234 - 2 * mv2.KVector2.Scalar14 * mv1.Scalar13 * mv2.KVector4.Scalar1234;
            tempScalar2 += 2 * mv2.KVector2.Scalar34 * mv1.Scalar13 * mv2.KVector4.Scalar1234 - 2 * mv2.KVector2.Scalar12 * mv1.Scalar24 * mv2.KVector4.Scalar1234;
            tempScalar3 += 2 * mv2.KVector2.Scalar12 * mv1.Scalar13 * mv2.KVector4.Scalar1234 - 2 * mv2.KVector2.Scalar34 * mv1.Scalar24 * mv2.KVector4.Scalar1234;
            tempScalar5 += -2 * mv2.KVector2.Scalar23 * mv1.Scalar13 * mv2.KVector4.Scalar1234 + 2 * mv2.KVector2.Scalar14 * mv1.Scalar24 * mv2.KVector4.Scalar1234;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar0 += mv2.KVector4.Scalar1234 * mv1.Scalar12 * mv2.KVector4.Scalar1234;
            tempScalar1 += mv2.KVector4.Scalar1234 * mv1.Scalar13 * mv2.KVector4.Scalar1234;
            tempScalar2 += mv2.KVector4.Scalar1234 * mv1.Scalar23 * mv2.KVector4.Scalar1234;
            tempScalar3 += mv2.KVector4.Scalar1234 * mv1.Scalar14 * mv2.KVector4.Scalar1234;
            tempScalar4 += mv2.KVector4.Scalar1234 * mv1.Scalar24 * mv2.KVector4.Scalar1234;
            tempScalar5 += mv2.KVector4.Scalar1234 * mv1.Scalar34 * mv2.KVector4.Scalar1234;
        }
        
        return new Ga4KVector2
        {
            Scalar12 = tempScalar0,
            Scalar13 = tempScalar1,
            Scalar23 = tempScalar2,
            Scalar14 = tempScalar3,
            Scalar24 = tempScalar4,
            Scalar34 = tempScalar5
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector3 MapUsingRotor(this Ga4KVector3 mv1, Ga4KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector3.Zero;
        
        return new Ga4KVector3
        {
            Scalar123 = mv2.Scalar * mv1.Scalar123 * mv2.Scalar,
            Scalar124 = mv2.Scalar * mv1.Scalar124 * mv2.Scalar,
            Scalar134 = mv2.Scalar * mv1.Scalar134 * mv2.Scalar,
            Scalar234 = mv2.Scalar * mv1.Scalar234 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector3 MapUsingRotor(this Ga4KVector3 mv1, Ga4KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector3.Zero;
        
        return new Ga4KVector3
        {
            Scalar123 = -mv2.Scalar12 * mv1.Scalar123 * mv2.Scalar12 - 2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar24 + mv2.Scalar13 * mv1.Scalar123 * mv2.Scalar13 + mv2.Scalar14 * mv1.Scalar123 * mv2.Scalar14 + mv2.Scalar34 * mv1.Scalar123 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.Scalar124 * mv2.Scalar14 + 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar24,
            Scalar124 = -mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar24 - mv2.Scalar12 * mv1.Scalar124 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar124 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.Scalar134 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar23 + mv2.Scalar14 * mv1.Scalar124 * mv2.Scalar14 - mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar34,
            Scalar134 = 2 * mv2.Scalar12 * mv1.Scalar123 * mv2.Scalar14 + mv2.Scalar12 * mv1.Scalar134 * mv2.Scalar12 + mv2.Scalar14 * mv1.Scalar134 * mv2.Scalar14 - mv2.Scalar13 * mv1.Scalar134 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar34 - mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar34 + mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar24,
            Scalar234 = -2 * mv2.Scalar12 * mv1.Scalar124 * mv2.Scalar23 - mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar12 - mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar23 + mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar13 - 2 * mv2.Scalar14 * mv1.Scalar124 * mv2.Scalar34 - mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar14 - mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar34 + mv2.Scalar24 * mv1.Scalar234 * mv2.Scalar24
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector3 MapUsingRotor(this Ga4KVector3 mv1, Ga4KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector3.Zero;
        
        return new Ga4KVector3
        {
            Scalar123 = -mv2.Scalar1234 * mv1.Scalar123 * mv2.Scalar1234,
            Scalar124 = -mv2.Scalar1234 * mv1.Scalar124 * mv2.Scalar1234,
            Scalar134 = -mv2.Scalar1234 * mv1.Scalar134 * mv2.Scalar1234,
            Scalar234 = -mv2.Scalar1234 * mv1.Scalar234 * mv2.Scalar1234
        };
    }
    
    public static Ga4KVector3 MapUsingRotor(this Ga4KVector3 mv1, Ga4Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector3.Zero;
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        var tempScalar2 = 0d;
        var tempScalar3 = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar0 += mv2.KVector0.Scalar * mv1.Scalar123 * mv2.KVector0.Scalar;
            tempScalar1 += mv2.KVector0.Scalar * mv1.Scalar124 * mv2.KVector0.Scalar;
            tempScalar2 += mv2.KVector0.Scalar * mv1.Scalar134 * mv2.KVector0.Scalar;
            tempScalar3 += mv2.KVector0.Scalar * mv1.Scalar234 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector0.Scalar * mv1.Scalar124 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.Scalar134 * mv2.KVector2.Scalar24 + 2 * mv2.KVector0.Scalar * mv1.Scalar234 * mv2.KVector2.Scalar14;
            tempScalar1 += -2 * mv2.KVector0.Scalar * mv1.Scalar123 * mv2.KVector2.Scalar34 + 2 * mv2.KVector0.Scalar * mv1.Scalar134 * mv2.KVector2.Scalar23 - 2 * mv2.KVector0.Scalar * mv1.Scalar234 * mv2.KVector2.Scalar13;
            tempScalar2 += 2 * mv2.KVector0.Scalar * mv1.Scalar123 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.Scalar124 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.Scalar234 * mv2.KVector2.Scalar12;
            tempScalar3 += -2 * mv2.KVector0.Scalar * mv1.Scalar123 * mv2.KVector2.Scalar14 + 2 * mv2.KVector0.Scalar * mv1.Scalar124 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.Scalar134 * mv2.KVector2.Scalar12;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar0 += -mv2.KVector2.Scalar12 * mv1.Scalar123 * mv2.KVector2.Scalar12 - 2 * mv2.KVector2.Scalar12 * mv1.Scalar234 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar13 * mv1.Scalar123 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar14 * mv1.Scalar123 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar34 * mv1.Scalar123 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar13 * mv1.Scalar124 * mv2.KVector2.Scalar14 + 2 * mv2.KVector2.Scalar13 * mv1.Scalar234 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.Scalar123 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.Scalar123 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar124 * mv2.KVector2.Scalar24;
            tempScalar1 += -mv2.KVector2.Scalar24 * mv1.Scalar124 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar12 * mv1.Scalar124 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.Scalar124 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.Scalar134 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.Scalar124 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar14 * mv1.Scalar124 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar34 * mv1.Scalar124 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar24 * mv1.Scalar134 * mv2.KVector2.Scalar34;
            tempScalar2 += 2 * mv2.KVector2.Scalar12 * mv1.Scalar123 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar12 * mv1.Scalar134 * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar14 * mv1.Scalar134 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar13 * mv1.Scalar134 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.Scalar134 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar13 * mv1.Scalar234 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar123 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar34 * mv1.Scalar134 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.Scalar134 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar14 * mv1.Scalar234 * mv2.KVector2.Scalar24;
            tempScalar3 += -2 * mv2.KVector2.Scalar12 * mv1.Scalar124 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar12 * mv1.Scalar234 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar23 * mv1.Scalar234 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar13 * mv1.Scalar234 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar14 * mv1.Scalar124 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar14 * mv1.Scalar234 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar34 * mv1.Scalar234 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.Scalar234 * mv2.KVector2.Scalar24;
        }
        
        if (!mv2.KVector2.IsZero() && !mv2.KVector4.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector2.Scalar13 * mv1.Scalar134 * mv2.KVector4.Scalar1234;
            tempScalar1 += -2 * mv2.KVector2.Scalar12 * mv1.Scalar123 * mv2.KVector4.Scalar1234 + 2 * mv2.KVector2.Scalar14 * mv1.Scalar134 * mv2.KVector4.Scalar1234;
            tempScalar2 += -2 * mv2.KVector2.Scalar13 * mv1.Scalar123 * mv2.KVector4.Scalar1234;
            tempScalar3 += -2 * mv2.KVector2.Scalar23 * mv1.Scalar123 * mv2.KVector4.Scalar1234 - 2 * mv2.KVector2.Scalar34 * mv1.Scalar134 * mv2.KVector4.Scalar1234;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar0 += -mv2.KVector4.Scalar1234 * mv1.Scalar123 * mv2.KVector4.Scalar1234;
            tempScalar1 += -mv2.KVector4.Scalar1234 * mv1.Scalar124 * mv2.KVector4.Scalar1234;
            tempScalar2 += -mv2.KVector4.Scalar1234 * mv1.Scalar134 * mv2.KVector4.Scalar1234;
            tempScalar3 += -mv2.KVector4.Scalar1234 * mv1.Scalar234 * mv2.KVector4.Scalar1234;
        }
        
        return new Ga4KVector3
        {
            Scalar123 = tempScalar0,
            Scalar124 = tempScalar1,
            Scalar134 = tempScalar2,
            Scalar234 = tempScalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector4 MapUsingRotor(this Ga4KVector4 mv1, Ga4KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector4.Zero;
        
        return new Ga4KVector4
        {
            Scalar1234 = mv2.Scalar * mv1.Scalar1234 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector4 MapUsingRotor(this Ga4KVector4 mv1, Ga4KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector4.Zero;
        
        return new Ga4KVector4
        {
            Scalar1234 = -mv2.Scalar12 * mv1.Scalar1234 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar1234 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar23 - mv2.Scalar14 * mv1.Scalar1234 * mv2.Scalar14 + mv2.Scalar24 * mv1.Scalar1234 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar34
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga4KVector4 MapUsingRotor(this Ga4KVector4 mv1, Ga4KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector4.Zero;
        
        return new Ga4KVector4
        {
            Scalar1234 = mv2.Scalar1234 * mv1.Scalar1234 * mv2.Scalar1234
        };
    }
    
    public static Ga4KVector4 MapUsingRotor(this Ga4KVector4 mv1, Ga4Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4KVector4.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv2.KVector0.Scalar * mv1.Scalar1234 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar += -mv2.KVector2.Scalar12 * mv1.Scalar1234 * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar13 * mv1.Scalar1234 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.Scalar1234 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar14 * mv1.Scalar1234 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar24 * mv1.Scalar1234 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.Scalar1234 * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar += mv2.KVector4.Scalar1234 * mv1.Scalar1234 * mv2.KVector4.Scalar1234;
        }
        
        return new Ga4KVector4
        {
            Scalar1234 = tempScalar
        };
    }
    
    public static Ga4Multivector MapUsingRotor(this Ga4Multivector mv1, Ga4KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += mv2.Scalar * mv1.KVector0.Scalar * mv2.Scalar;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += mv2.Scalar * mv1.KVector1.Scalar1 * mv2.Scalar;
            tempScalar[2] += mv2.Scalar * mv1.KVector1.Scalar2 * mv2.Scalar;
            tempScalar[4] += mv2.Scalar * mv1.KVector1.Scalar3 * mv2.Scalar;
            tempScalar[8] += mv2.Scalar * mv1.KVector1.Scalar4 * mv2.Scalar;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += mv2.Scalar * mv1.KVector2.Scalar12 * mv2.Scalar;
            tempScalar[5] += mv2.Scalar * mv1.KVector2.Scalar13 * mv2.Scalar;
            tempScalar[6] += mv2.Scalar * mv1.KVector2.Scalar23 * mv2.Scalar;
            tempScalar[9] += mv2.Scalar * mv1.KVector2.Scalar14 * mv2.Scalar;
            tempScalar[10] += mv2.Scalar * mv1.KVector2.Scalar24 * mv2.Scalar;
            tempScalar[12] += mv2.Scalar * mv1.KVector2.Scalar34 * mv2.Scalar;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += mv2.Scalar * mv1.KVector3.Scalar123 * mv2.Scalar;
            tempScalar[11] += mv2.Scalar * mv1.KVector3.Scalar124 * mv2.Scalar;
            tempScalar[13] += mv2.Scalar * mv1.KVector3.Scalar134 * mv2.Scalar;
            tempScalar[14] += mv2.Scalar * mv1.KVector3.Scalar234 * mv2.Scalar;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += mv2.Scalar * mv1.KVector4.Scalar1234 * mv2.Scalar;
        }
        
        return Ga4Multivector.Create(tempScalar);
    }
    
    public static Ga4Multivector MapUsingRotor(this Ga4Multivector mv1, Ga4KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += mv2.Scalar12 * mv1.KVector0.Scalar * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector0.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23 + mv2.Scalar14 * mv1.KVector0.Scalar * mv2.Scalar14 + mv2.Scalar24 * mv1.KVector0.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector0.Scalar * mv2.Scalar34;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar4 * mv2.Scalar24 + mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar4 * mv2.Scalar34 + mv2.Scalar14 * mv1.KVector1.Scalar1 * mv2.Scalar14 + mv2.Scalar24 * mv1.KVector1.Scalar1 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector1.Scalar1 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar2 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar3 * mv2.Scalar34;
            tempScalar[2] += mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector1.Scalar2 * mv2.Scalar24 - mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar13 - mv2.Scalar14 * mv1.KVector1.Scalar2 * mv2.Scalar14 - 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.KVector1.Scalar4 * mv2.Scalar14 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar34 + mv2.Scalar34 * mv1.KVector1.Scalar2 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar34;
            tempScalar[4] += mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector1.Scalar3 * mv2.Scalar13 - mv2.Scalar14 * mv1.KVector1.Scalar3 * mv2.Scalar14 - 2 * mv2.Scalar13 * mv1.KVector1.Scalar4 * mv2.Scalar14 + mv2.Scalar34 * mv1.KVector1.Scalar3 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar24;
            tempScalar[8] += -mv2.Scalar34 * mv1.KVector1.Scalar4 * mv2.Scalar34 - mv2.Scalar24 * mv1.KVector1.Scalar4 * mv2.Scalar24 + mv2.Scalar12 * mv1.KVector1.Scalar4 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector1.Scalar4 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar23 - mv2.Scalar14 * mv1.KVector1.Scalar4 * mv2.Scalar14;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += -mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar12 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar14 * mv2.Scalar14 + mv2.Scalar24 * mv1.KVector2.Scalar12 * mv2.Scalar24 + mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23 - mv2.Scalar14 * mv1.KVector2.Scalar12 * mv2.Scalar14 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar34 + mv2.Scalar34 * mv1.KVector2.Scalar12 * mv2.Scalar34;
            tempScalar[5] += mv2.Scalar34 * mv1.KVector2.Scalar13 * mv2.Scalar34 + mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar24 * mv2.Scalar34 - mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23 - mv2.Scalar14 * mv1.KVector2.Scalar13 * mv2.Scalar14 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar14 * mv2.Scalar14 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar14 - mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar24;
            tempScalar[6] += -2 * mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar12 * mv2.Scalar34 - mv2.Scalar12 * mv1.KVector2.Scalar23 * mv2.Scalar12 - mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 + mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar14 + mv2.Scalar14 * mv1.KVector2.Scalar23 * mv2.Scalar14 - mv2.Scalar24 * mv1.KVector2.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector2.Scalar23 * mv2.Scalar34;
            tempScalar[9] += mv2.Scalar12 * mv1.KVector2.Scalar14 * mv2.Scalar12 - mv2.Scalar14 * mv1.KVector2.Scalar14 * mv2.Scalar14 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar34 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar34 * mv2.Scalar34 + mv2.Scalar13 * mv1.KVector2.Scalar14 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector2.Scalar14 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector2.Scalar14 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar24 * mv2.Scalar24;
            tempScalar[10] += -2 * mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar34 - mv2.Scalar12 * mv1.KVector2.Scalar24 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar13 - mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar34 * mv2.Scalar13 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar12 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector2.Scalar24 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar14 + mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar23 + mv2.Scalar14 * mv1.KVector2.Scalar24 * mv2.Scalar14;
            tempScalar[12] += 2 * mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar24 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar23 * mv2.Scalar14 + mv2.Scalar12 * mv1.KVector2.Scalar34 * mv2.Scalar12 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar34 - mv2.Scalar13 * mv1.KVector2.Scalar34 * mv2.Scalar13 + mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector2.Scalar34 * mv2.Scalar23 + mv2.Scalar14 * mv1.KVector2.Scalar34 * mv2.Scalar14 - mv2.Scalar34 * mv1.KVector2.Scalar34 * mv2.Scalar34;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += -mv2.Scalar12 * mv1.KVector3.Scalar123 * mv2.Scalar12 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar24 + mv2.Scalar13 * mv1.KVector3.Scalar123 * mv2.Scalar13 + mv2.Scalar14 * mv1.KVector3.Scalar123 * mv2.Scalar14 + mv2.Scalar34 * mv1.KVector3.Scalar123 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar124 * mv2.Scalar14 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar24;
            tempScalar[11] += -mv2.Scalar12 * mv1.KVector3.Scalar124 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector3.Scalar124 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar134 * mv2.Scalar13 - mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar24 + mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar23 + mv2.Scalar14 * mv1.KVector3.Scalar124 * mv2.Scalar14 - mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar34;
            tempScalar[13] += 2 * mv2.Scalar12 * mv1.KVector3.Scalar123 * mv2.Scalar14 + mv2.Scalar12 * mv1.KVector3.Scalar134 * mv2.Scalar12 + mv2.Scalar14 * mv1.KVector3.Scalar134 * mv2.Scalar14 - mv2.Scalar13 * mv1.KVector3.Scalar134 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar34 - mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar24;
            tempScalar[14] += -2 * mv2.Scalar12 * mv1.KVector3.Scalar124 * mv2.Scalar23 - mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar12 - mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar23 + mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar13 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar124 * mv2.Scalar34 - mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar14 - mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector3.Scalar234 * mv2.Scalar24;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += -mv2.Scalar12 * mv1.KVector4.Scalar1234 * mv2.Scalar12 - mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar34 + mv2.Scalar13 * mv1.KVector4.Scalar1234 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar23 - mv2.Scalar14 * mv1.KVector4.Scalar1234 * mv2.Scalar14 + mv2.Scalar24 * mv1.KVector4.Scalar1234 * mv2.Scalar24;
        }
        
        return Ga4Multivector.Create(tempScalar);
    }
    
    public static Ga4Multivector MapUsingRotor(this Ga4Multivector mv1, Ga4KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += mv2.Scalar1234 * mv1.KVector0.Scalar * mv2.Scalar1234;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += -mv2.Scalar1234 * mv1.KVector1.Scalar1 * mv2.Scalar1234;
            tempScalar[2] += -mv2.Scalar1234 * mv1.KVector1.Scalar2 * mv2.Scalar1234;
            tempScalar[4] += -mv2.Scalar1234 * mv1.KVector1.Scalar3 * mv2.Scalar1234;
            tempScalar[8] += -mv2.Scalar1234 * mv1.KVector1.Scalar4 * mv2.Scalar1234;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += mv2.Scalar1234 * mv1.KVector2.Scalar12 * mv2.Scalar1234;
            tempScalar[5] += mv2.Scalar1234 * mv1.KVector2.Scalar13 * mv2.Scalar1234;
            tempScalar[6] += mv2.Scalar1234 * mv1.KVector2.Scalar23 * mv2.Scalar1234;
            tempScalar[9] += mv2.Scalar1234 * mv1.KVector2.Scalar14 * mv2.Scalar1234;
            tempScalar[10] += mv2.Scalar1234 * mv1.KVector2.Scalar24 * mv2.Scalar1234;
            tempScalar[12] += mv2.Scalar1234 * mv1.KVector2.Scalar34 * mv2.Scalar1234;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += -mv2.Scalar1234 * mv1.KVector3.Scalar123 * mv2.Scalar1234;
            tempScalar[11] += -mv2.Scalar1234 * mv1.KVector3.Scalar124 * mv2.Scalar1234;
            tempScalar[13] += -mv2.Scalar1234 * mv1.KVector3.Scalar134 * mv2.Scalar1234;
            tempScalar[14] += -mv2.Scalar1234 * mv1.KVector3.Scalar234 * mv2.Scalar1234;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += mv2.Scalar1234 * mv1.KVector4.Scalar1234 * mv2.Scalar1234;
        }
        
        return Ga4Multivector.Create(tempScalar);
    }
    
    public static Ga4Multivector MapUsingRotor(this Ga4Multivector mv1, Ga4Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga4Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[0] += mv2.KVector0.Scalar * mv1.KVector0.Scalar * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[0] += mv2.KVector2.Scalar12 * mv1.KVector0.Scalar * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar13 * mv1.KVector0.Scalar * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.KVector0.Scalar * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar14 * mv1.KVector0.Scalar * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar24 * mv1.KVector0.Scalar * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.KVector0.Scalar * mv2.KVector2.Scalar34;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[0] += mv2.KVector4.Scalar1234 * mv1.KVector0.Scalar * mv2.KVector4.Scalar1234;
            }
            
        }
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[1] += mv2.KVector0.Scalar * mv1.KVector1.Scalar1 * mv2.KVector0.Scalar;
                tempScalar[2] += mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector0.Scalar;
                tempScalar[4] += mv2.KVector0.Scalar * mv1.KVector1.Scalar3 * mv2.KVector0.Scalar;
                tempScalar[8] += mv2.KVector0.Scalar * mv1.KVector1.Scalar4 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
            {
                tempScalar[1] += 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13 + 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar14;
                tempScalar[2] += -2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24;
                tempScalar[4] += -2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34;
                tempScalar[8] += -2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar14 - 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += mv2.KVector2.Scalar12 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar12 + 2 * mv2.KVector2.Scalar12 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar12 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar13 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar14 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar24 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar14 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar14 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34;
                tempScalar[2] += -mv2.KVector2.Scalar12 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar14 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar14 - 2 * mv2.KVector2.Scalar12 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar23 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar23 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar24 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34;
                tempScalar[4] += mv2.KVector2.Scalar12 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar14 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar14 - 2 * mv2.KVector2.Scalar13 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar34 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24;
                tempScalar[8] += -mv2.KVector2.Scalar24 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar12 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar13 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar14 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar34 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34;
            }
            
            if (!mv2.KVector2.IsZero() && !mv2.KVector4.IsZero())
            {
                tempScalar[1] += 2 * mv2.KVector2.Scalar34 * mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1234 + 2 * mv2.KVector2.Scalar23 * mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1234;
                tempScalar[2] += -2 * mv2.KVector2.Scalar13 * mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1234;
                tempScalar[4] += 2 * mv2.KVector2.Scalar12 * mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1234 - 2 * mv2.KVector2.Scalar14 * mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1234;
                tempScalar[8] += 2 * mv2.KVector2.Scalar13 * mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1234;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[1] += -mv2.KVector4.Scalar1234 * mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1234;
                tempScalar[2] += -mv2.KVector4.Scalar1234 * mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1234;
                tempScalar[4] += -mv2.KVector4.Scalar1234 * mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1234;
                tempScalar[8] += -mv2.KVector4.Scalar1234 * mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1234;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[3] += mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector0.Scalar;
                tempScalar[5] += mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector0.Scalar;
                tempScalar[6] += mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector0.Scalar;
                tempScalar[9] += mv2.KVector0.Scalar * mv1.KVector2.Scalar14 * mv2.KVector0.Scalar;
                tempScalar[10] += mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector0.Scalar;
                tempScalar[12] += mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
            {
                tempScalar[3] += 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar23 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar13 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar14;
                tempScalar[5] += -2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar14;
                tempScalar[6] += 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar24;
                tempScalar[9] += -2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar34 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar13;
                tempScalar[10] += 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar14 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar23;
                tempScalar[12] += 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar14 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar23;
            }
            
            if (!mv2.KVector0.IsZero() && !mv2.KVector4.IsZero())
            {
                tempScalar[3] += -2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1234;
                tempScalar[5] += 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1234;
                tempScalar[6] += -2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1234;
                tempScalar[9] += -2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1234;
                tempScalar[10] += 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1234;
                tempScalar[12] += -2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1234;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += -mv2.KVector2.Scalar12 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar24 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar23 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar14 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar14 + 2 * mv2.KVector2.Scalar24 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar34 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar34;
                tempScalar[5] += mv2.KVector2.Scalar34 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar12 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar12 + 2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar13 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar14 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar14 - 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar14 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar14 - 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar24 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar24;
                tempScalar[6] += -2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar14 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar12 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar23 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar13 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar13 + 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar14 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar24 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar34;
                tempScalar[9] += mv2.KVector2.Scalar12 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar14 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar14 + 2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar14 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar13 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar14 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24;
                tempScalar[10] += -2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar12 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar24 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar24 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar23 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar14 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar34 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar34;
                tempScalar[12] += 2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar12 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar12 - 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar13 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar24 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar14 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar34 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar34;
            }
            
            if (!mv2.KVector2.IsZero() && !mv2.KVector4.IsZero())
            {
                tempScalar[3] += 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1234 - 2 * mv2.KVector2.Scalar14 * mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1234;
                tempScalar[6] += -2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1234 + 2 * mv2.KVector2.Scalar34 * mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1234;
                tempScalar[9] += 2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1234 - 2 * mv2.KVector2.Scalar34 * mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1234;
                tempScalar[12] += -2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1234 + 2 * mv2.KVector2.Scalar14 * mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1234;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[3] += mv2.KVector4.Scalar1234 * mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1234;
                tempScalar[5] += mv2.KVector4.Scalar1234 * mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1234;
                tempScalar[6] += mv2.KVector4.Scalar1234 * mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1234;
                tempScalar[9] += mv2.KVector4.Scalar1234 * mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1234;
                tempScalar[10] += mv2.KVector4.Scalar1234 * mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1234;
                tempScalar[12] += mv2.KVector4.Scalar1234 * mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1234;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[7] += mv2.KVector0.Scalar * mv1.KVector3.Scalar123 * mv2.KVector0.Scalar;
                tempScalar[11] += mv2.KVector0.Scalar * mv1.KVector3.Scalar124 * mv2.KVector0.Scalar;
                tempScalar[13] += mv2.KVector0.Scalar * mv1.KVector3.Scalar134 * mv2.KVector0.Scalar;
                tempScalar[14] += mv2.KVector0.Scalar * mv1.KVector3.Scalar234 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
            {
                tempScalar[7] += 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar24 + 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar14;
                tempScalar[11] += -2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar34 + 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar23 - 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar13;
                tempScalar[13] += 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar12;
                tempScalar[14] += -2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar14 + 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar12;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[7] += -mv2.KVector2.Scalar12 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar12 - 2 * mv2.KVector2.Scalar12 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar13 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar14 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar34 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar13 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar14 + 2 * mv2.KVector2.Scalar13 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar24;
                tempScalar[11] += -mv2.KVector2.Scalar12 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar14 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar24 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar24 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar34;
                tempScalar[13] += 2 * mv2.KVector2.Scalar12 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar12 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar14 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar13 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar13 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar23 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar34 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar14 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar24;
                tempScalar[14] += -2 * mv2.KVector2.Scalar12 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar12 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar23 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar13 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar14 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar14 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar14 - mv2.KVector2.Scalar34 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar24;
            }
            
            if (!mv2.KVector2.IsZero() && !mv2.KVector4.IsZero())
            {
                tempScalar[7] += 2 * mv2.KVector2.Scalar13 * mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1234;
                tempScalar[11] += -2 * mv2.KVector2.Scalar12 * mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1234 + 2 * mv2.KVector2.Scalar14 * mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1234;
                tempScalar[13] += -2 * mv2.KVector2.Scalar13 * mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1234;
                tempScalar[14] += -2 * mv2.KVector2.Scalar34 * mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1234 - 2 * mv2.KVector2.Scalar23 * mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1234;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[7] += -mv2.KVector4.Scalar1234 * mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1234;
                tempScalar[11] += -mv2.KVector4.Scalar1234 * mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1234;
                tempScalar[13] += -mv2.KVector4.Scalar1234 * mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1234;
                tempScalar[14] += -mv2.KVector4.Scalar1234 * mv1.KVector3.Scalar234 * mv2.KVector4.Scalar1234;
            }
            
        }
        
        if (!mv1.KVector4.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[15] += mv2.KVector0.Scalar * mv1.KVector4.Scalar1234 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[15] += -mv2.KVector2.Scalar12 * mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar13 * mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar14 * mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar24 * mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar34;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[15] += mv2.KVector4.Scalar1234 * mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1234;
            }
            
        }
        
        return Ga4Multivector.Create(tempScalar);
    }
    
}

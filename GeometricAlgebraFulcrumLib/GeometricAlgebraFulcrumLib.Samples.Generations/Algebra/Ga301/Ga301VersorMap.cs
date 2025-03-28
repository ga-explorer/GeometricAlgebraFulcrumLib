using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga301;

public static class Ga301VersorMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 MapUsingVersor(this Ga301KVector0 mv1, Ga301KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar * mv1.Scalar * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 MapUsingVersor(this Ga301KVector0 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 MapUsingVersor(this Ga301KVector0 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar23 * mv1.Scalar * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 MapUsingVersor(this Ga301KVector0 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar234 * mv1.Scalar * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 MapUsingVersor(this Ga301KVector0 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector0.Zero;
    }
    
    public static Ga301KVector0 MapUsingEvenVersor(this Ga301KVector0 mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv2.KVector0.Scalar * mv1.Scalar * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar += mv2.KVector2.Scalar23 * mv1.Scalar * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar24 * mv1.Scalar * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar * mv2.KVector2.Scalar34;
        }
        
        return new Ga301KVector0
        {
            Scalar = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    public static Ga301KVector0 MapUsingOddVersor(this Ga301KVector0 mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar += mv2.KVector1.Scalar2 * mv1.Scalar * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar3 * mv1.Scalar * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar4 * mv1.Scalar * mv2.KVector1.Scalar4;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar += mv2.KVector3.Scalar234 * mv1.Scalar * mv2.KVector3.Scalar234;
        }
        
        return new Ga301KVector0
        {
            Scalar = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 MapUsingVersor(this Ga301KVector1 mv1, Ga301KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector1
        {
            Scalar1 = (mv2.Scalar * mv1.Scalar1 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar * mv1.Scalar2 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar * mv1.Scalar3 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar4 = (mv2.Scalar * mv1.Scalar4 * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 MapUsingVersor(this Ga301KVector1 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector1
        {
            Scalar1 = (-2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar4 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar1 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar2 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar3 = (-2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar4 + mv2.Scalar4 * mv1.Scalar3 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar4 = (-2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar4 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 MapUsingVersor(this Ga301KVector1 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector1
        {
            Scalar1 = (2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar4 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar4 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.Scalar2 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar3 * mv2.Scalar34 + mv2.Scalar24 * mv1.Scalar1 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar1 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar34 + mv2.Scalar24 * mv1.Scalar2 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar2 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar34 * mv1.Scalar3 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar24) * mv2NormSquaredInv,
            Scalar4 = (mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar4 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar4 * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 MapUsingVersor(this Ga301KVector1 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector1
        {
            Scalar1 = (2 * mv2.Scalar123 * mv1.Scalar4 * mv2.Scalar234 + 2 * mv2.Scalar124 * mv1.Scalar3 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.Scalar2 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar1 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar234 * mv1.Scalar2 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar234 * mv1.Scalar3 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar4 = (mv2.Scalar234 * mv1.Scalar4 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 MapUsingVersor(this Ga301KVector1 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector1.Zero;
    }
    
    public static Ga301KVector1 MapUsingEvenVersor(this Ga301KVector1 mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
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
            tempScalar1 += 2 * mv2.KVector0.Scalar * mv1.Scalar3 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.Scalar4 * mv2.KVector2.Scalar24;
            tempScalar2 += -2 * mv2.KVector0.Scalar * mv1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.Scalar4 * mv2.KVector2.Scalar34;
            tempScalar3 += -2 * mv2.KVector0.Scalar * mv1.Scalar2 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.Scalar3 * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector2.Scalar12 * mv1.Scalar3 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar12 * mv1.Scalar4 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar13 * mv1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.Scalar4 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar23 * mv1.Scalar1 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar14 * mv1.Scalar2 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar14 * mv1.Scalar3 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.Scalar1 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar1 * mv2.KVector2.Scalar34;
            tempScalar1 += mv2.KVector2.Scalar23 * mv1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar23 * mv1.Scalar4 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.Scalar2 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar2 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar24 * mv1.Scalar3 * mv2.KVector2.Scalar34;
            tempScalar2 += -mv2.KVector2.Scalar23 * mv1.Scalar3 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.Scalar3 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar4 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar3 * mv2.KVector2.Scalar34;
            tempScalar3 += mv2.KVector2.Scalar23 * mv1.Scalar4 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.Scalar4 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.Scalar4 * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector2.IsZero() && !mv2.KVector4.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector2.Scalar34 * mv1.Scalar2 * mv2.KVector4.Scalar1234 + 2 * mv2.KVector2.Scalar23 * mv1.Scalar4 * mv2.KVector4.Scalar1234;
        }
        
        return new Ga301KVector1
        {
            Scalar1 = (tempScalar0) * mv2NormSquaredInv,
            Scalar2 = (tempScalar1) * mv2NormSquaredInv,
            Scalar3 = (tempScalar2) * mv2NormSquaredInv,
            Scalar4 = (tempScalar3) * mv2NormSquaredInv
        };
    }
    
    public static Ga301KVector1 MapUsingOddVersor(this Ga301KVector1 mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        var tempScalar2 = 0d;
        var tempScalar3 = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar0 += -2 * mv2.KVector1.Scalar1 * mv1.Scalar2 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar1 * mv1.Scalar3 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar1 * mv1.Scalar4 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar2 * mv1.Scalar1 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar3 * mv1.Scalar1 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar4 * mv1.Scalar1 * mv2.KVector1.Scalar4;
            tempScalar1 += -mv2.KVector1.Scalar2 * mv1.Scalar2 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar3 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar4 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.Scalar2 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar4 * mv1.Scalar2 * mv2.KVector1.Scalar4;
            tempScalar2 += -2 * mv2.KVector1.Scalar2 * mv1.Scalar2 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.Scalar3 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.Scalar3 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar4 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar4 * mv1.Scalar3 * mv2.KVector1.Scalar4;
            tempScalar3 += -2 * mv2.KVector1.Scalar2 * mv1.Scalar2 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar2 * mv1.Scalar4 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar3 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.Scalar4 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.Scalar4 * mv2.KVector1.Scalar4;
        }
        
        if (!mv2.KVector1.IsZero() && !mv2.KVector3.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector1.Scalar3 * mv1.Scalar2 * mv2.KVector3.Scalar123 + 2 * mv2.KVector1.Scalar4 * mv1.Scalar2 * mv2.KVector3.Scalar124 + 2 * mv2.KVector1.Scalar4 * mv1.Scalar3 * mv2.KVector3.Scalar134;
            tempScalar1 += 2 * mv2.KVector1.Scalar4 * mv1.Scalar3 * mv2.KVector3.Scalar234;
            tempScalar2 += -2 * mv2.KVector1.Scalar4 * mv1.Scalar2 * mv2.KVector3.Scalar234;
            tempScalar3 += 2 * mv2.KVector1.Scalar3 * mv1.Scalar2 * mv2.KVector3.Scalar234;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector3.Scalar123 * mv1.Scalar4 * mv2.KVector3.Scalar234 + 2 * mv2.KVector3.Scalar134 * mv1.Scalar2 * mv2.KVector3.Scalar234 + mv2.KVector3.Scalar234 * mv1.Scalar1 * mv2.KVector3.Scalar234 + 2 * mv2.KVector3.Scalar124 * mv1.Scalar3 * mv2.KVector3.Scalar234;
            tempScalar1 += mv2.KVector3.Scalar234 * mv1.Scalar2 * mv2.KVector3.Scalar234;
            tempScalar2 += -mv2.KVector3.Scalar234 * mv1.Scalar3 * mv2.KVector3.Scalar234;
            tempScalar3 += mv2.KVector3.Scalar234 * mv1.Scalar4 * mv2.KVector3.Scalar234;
        }
        
        return new Ga301KVector1
        {
            Scalar1 = (tempScalar0) * mv2NormSquaredInv,
            Scalar2 = (tempScalar1) * mv2NormSquaredInv,
            Scalar3 = (tempScalar2) * mv2NormSquaredInv,
            Scalar4 = (tempScalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 MapUsingVersor(this Ga301KVector2 mv1, Ga301KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector2
        {
            Scalar12 = (mv2.Scalar * mv1.Scalar12 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar * mv1.Scalar13 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar * mv1.Scalar23 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar14 = (mv2.Scalar * mv1.Scalar14 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar * mv1.Scalar24 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar34 = (mv2.Scalar * mv1.Scalar34 * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 MapUsingVersor(this Ga301KVector2 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector2
        {
            Scalar12 = (2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar12 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar13 = (-2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar4 + mv2.Scalar4 * mv1.Scalar13 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar4 + mv2.Scalar4 * mv1.Scalar23 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar14 = (-2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar14 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar24 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar34 = (2 * mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar34 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar34 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 MapUsingVersor(this Ga301KVector2 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector2
        {
            Scalar12 = (mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar34 + mv2.Scalar24 * mv1.Scalar12 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar12 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar12 * mv1.Scalar24 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar13 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar14) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar23 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar14 = (-mv2.Scalar34 * mv1.Scalar14 * mv2.Scalar34 + 2 * mv2.Scalar12 * mv1.Scalar34 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar14 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar23 + mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar24 * mv2.Scalar24) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar24 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar34 = (mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar34 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar34 * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 MapUsingVersor(this Ga301KVector2 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector2
        {
            Scalar12 = (mv2.Scalar234 * mv1.Scalar12 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar13 = (-2 * mv2.Scalar134 * mv1.Scalar23 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar13 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar234 * mv1.Scalar23 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar14 = (2 * mv2.Scalar124 * mv1.Scalar34 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.Scalar24 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar14 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar234 * mv1.Scalar24 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar34 = (-mv2.Scalar234 * mv1.Scalar34 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 MapUsingVersor(this Ga301KVector2 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector2.Zero;
    }
    
    public static Ga301KVector2 MapUsingEvenVersor(this Ga301KVector2 mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
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
            tempScalar2 += 2 * mv2.KVector0.Scalar * mv1.Scalar24 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.Scalar34 * mv2.KVector2.Scalar24;
            tempScalar3 += -2 * mv2.KVector0.Scalar * mv1.Scalar12 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.Scalar13 * mv2.KVector2.Scalar34 + 2 * mv2.KVector0.Scalar * mv1.Scalar24 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.Scalar34 * mv2.KVector2.Scalar13;
            tempScalar4 += -2 * mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector2.Scalar34 + 2 * mv2.KVector0.Scalar * mv1.Scalar34 * mv2.KVector2.Scalar23;
            tempScalar5 += 2 * mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.Scalar24 * mv2.KVector2.Scalar23;
        }
        
        if (!mv2.KVector0.IsZero() && !mv2.KVector4.IsZero())
        {
            tempScalar0 += -2 * mv2.KVector0.Scalar * mv1.Scalar34 * mv2.KVector4.Scalar1234;
            tempScalar1 += 2 * mv2.KVector0.Scalar * mv1.Scalar24 * mv2.KVector4.Scalar1234;
            tempScalar3 += -2 * mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector4.Scalar1234;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar0 += mv2.KVector2.Scalar23 * mv1.Scalar12 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar23 * mv1.Scalar14 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.Scalar12 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar12 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar24 * mv1.Scalar13 * mv2.KVector2.Scalar34;
            tempScalar1 += 2 * mv2.KVector2.Scalar12 * mv1.Scalar24 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar13 * mv1.Scalar23 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar13 * mv1.Scalar24 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar13 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.Scalar13 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.Scalar13 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar14 * mv1.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar14 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar24 * mv2.KVector2.Scalar14;
            tempScalar2 += -mv2.KVector2.Scalar23 * mv1.Scalar23 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar24 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar23 * mv2.KVector2.Scalar34;
            tempScalar3 += 2 * mv2.KVector2.Scalar12 * mv1.Scalar34 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.Scalar24 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar23 * mv1.Scalar14 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar14 * mv1.Scalar34 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar14 * mv1.Scalar24 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar24 * mv1.Scalar14 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.Scalar14 * mv2.KVector2.Scalar34;
            tempScalar4 += -mv2.KVector2.Scalar24 * mv1.Scalar24 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar24 * mv1.Scalar34 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar23 * mv1.Scalar24 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar34 * mv1.Scalar24 * mv2.KVector2.Scalar34;
            tempScalar5 += mv2.KVector2.Scalar24 * mv1.Scalar34 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar23 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.Scalar34 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar34 * mv1.Scalar34 * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector2.IsZero() && !mv2.KVector4.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector2.Scalar23 * mv1.Scalar24 * mv2.KVector4.Scalar1234;
            tempScalar3 += -2 * mv2.KVector2.Scalar34 * mv1.Scalar24 * mv2.KVector4.Scalar1234;
        }
        
        return new Ga301KVector2
        {
            Scalar12 = (tempScalar0) * mv2NormSquaredInv,
            Scalar13 = (tempScalar1) * mv2NormSquaredInv,
            Scalar23 = (tempScalar2) * mv2NormSquaredInv,
            Scalar14 = (tempScalar3) * mv2NormSquaredInv,
            Scalar24 = (tempScalar4) * mv2NormSquaredInv,
            Scalar34 = (tempScalar5) * mv2NormSquaredInv
        };
    }
    
    public static Ga301KVector2 MapUsingOddVersor(this Ga301KVector2 mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        var tempScalar2 = 0d;
        var tempScalar3 = 0d;
        var tempScalar4 = 0d;
        var tempScalar5 = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector1.Scalar1 * mv1.Scalar23 * mv2.KVector1.Scalar3 + 2 * mv2.KVector1.Scalar1 * mv1.Scalar24 * mv2.KVector1.Scalar4 - mv2.KVector1.Scalar2 * mv1.Scalar12 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar13 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar14 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.Scalar12 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar4 * mv1.Scalar12 * mv2.KVector1.Scalar4;
            tempScalar1 += -2 * mv2.KVector1.Scalar1 * mv1.Scalar23 * mv2.KVector1.Scalar2 + 2 * mv2.KVector1.Scalar1 * mv1.Scalar34 * mv2.KVector1.Scalar4 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar12 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.Scalar13 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.Scalar13 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar14 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar4 * mv1.Scalar13 * mv2.KVector1.Scalar4;
            tempScalar2 += -mv2.KVector1.Scalar2 * mv1.Scalar23 * mv2.KVector1.Scalar2 + 2 * mv2.KVector1.Scalar2 * mv1.Scalar34 * mv2.KVector1.Scalar4 - mv2.KVector1.Scalar3 * mv1.Scalar23 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar24 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar4 * mv1.Scalar23 * mv2.KVector1.Scalar4;
            tempScalar3 += -2 * mv2.KVector1.Scalar1 * mv1.Scalar24 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar1 * mv1.Scalar34 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar12 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar2 * mv1.Scalar14 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar13 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.Scalar14 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.Scalar14 * mv2.KVector1.Scalar4;
            tempScalar4 += -mv2.KVector1.Scalar2 * mv1.Scalar24 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar34 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar23 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.Scalar24 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.Scalar24 * mv2.KVector1.Scalar4;
            tempScalar5 += 2 * mv2.KVector1.Scalar2 * mv1.Scalar23 * mv2.KVector1.Scalar4 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar24 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.Scalar34 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.Scalar34 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.Scalar34 * mv2.KVector1.Scalar4;
        }
        
        if (!mv2.KVector1.IsZero() && !mv2.KVector3.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector1.Scalar1 * mv1.Scalar34 * mv2.KVector3.Scalar234 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar23 * mv2.KVector3.Scalar123 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar24 * mv2.KVector3.Scalar124 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar34 * mv2.KVector3.Scalar134 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar34 * mv2.KVector3.Scalar124 + 2 * mv2.KVector1.Scalar4 * mv1.Scalar13 * mv2.KVector3.Scalar234 - 2 * mv2.KVector1.Scalar4 * mv1.Scalar23 * mv2.KVector3.Scalar134;
            tempScalar1 += -2 * mv2.KVector1.Scalar1 * mv1.Scalar24 * mv2.KVector3.Scalar234 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar24 * mv2.KVector3.Scalar134 + 2 * mv2.KVector1.Scalar2 * mv1.Scalar34 * mv2.KVector3.Scalar124 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar34 * mv2.KVector3.Scalar134 - 2 * mv2.KVector1.Scalar4 * mv1.Scalar12 * mv2.KVector3.Scalar234 + 2 * mv2.KVector1.Scalar4 * mv1.Scalar23 * mv2.KVector3.Scalar124;
            tempScalar2 += -2 * mv2.KVector1.Scalar2 * mv1.Scalar24 * mv2.KVector3.Scalar234 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar34 * mv2.KVector3.Scalar234;
            tempScalar3 += 2 * mv2.KVector1.Scalar1 * mv1.Scalar23 * mv2.KVector3.Scalar234 + 2 * mv2.KVector1.Scalar2 * mv1.Scalar23 * mv2.KVector3.Scalar134 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar34 * mv2.KVector3.Scalar123 + 2 * mv2.KVector1.Scalar3 * mv1.Scalar12 * mv2.KVector3.Scalar234;
            tempScalar4 += 2 * mv2.KVector1.Scalar2 * mv1.Scalar23 * mv2.KVector3.Scalar234;
        }
        
        if (!mv2.KVector3.IsZero() && !mv2.KVector1.IsZero())
        {
            tempScalar3 += -2 * mv2.KVector3.Scalar123 * mv1.Scalar23 * mv2.KVector1.Scalar4;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar0 += mv2.KVector3.Scalar234 * mv1.Scalar12 * mv2.KVector3.Scalar234;
            tempScalar1 += -mv2.KVector3.Scalar234 * mv1.Scalar13 * mv2.KVector3.Scalar234 - 2 * mv2.KVector3.Scalar134 * mv1.Scalar23 * mv2.KVector3.Scalar234;
            tempScalar2 += -mv2.KVector3.Scalar234 * mv1.Scalar23 * mv2.KVector3.Scalar234;
            tempScalar3 += mv2.KVector3.Scalar234 * mv1.Scalar14 * mv2.KVector3.Scalar234 + 2 * mv2.KVector3.Scalar134 * mv1.Scalar24 * mv2.KVector3.Scalar234 + 2 * mv2.KVector3.Scalar124 * mv1.Scalar34 * mv2.KVector3.Scalar234;
            tempScalar4 += mv2.KVector3.Scalar234 * mv1.Scalar24 * mv2.KVector3.Scalar234;
            tempScalar5 += -mv2.KVector3.Scalar234 * mv1.Scalar34 * mv2.KVector3.Scalar234;
        }
        
        return new Ga301KVector2
        {
            Scalar12 = (tempScalar0) * mv2NormSquaredInv,
            Scalar13 = (tempScalar1) * mv2NormSquaredInv,
            Scalar23 = (tempScalar2) * mv2NormSquaredInv,
            Scalar14 = (tempScalar3) * mv2NormSquaredInv,
            Scalar24 = (tempScalar4) * mv2NormSquaredInv,
            Scalar34 = (tempScalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 MapUsingVersor(this Ga301KVector3 mv1, Ga301KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector3
        {
            Scalar123 = (mv2.Scalar * mv1.Scalar123 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar124 = (mv2.Scalar * mv1.Scalar124 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar134 = (mv2.Scalar * mv1.Scalar134 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar * mv1.Scalar234 * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 MapUsingVersor(this Ga301KVector3 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector3
        {
            Scalar123 = (-2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar4 + mv2.Scalar4 * mv1.Scalar123 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar124 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar124 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar134 = (-2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar134 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar134 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar2 * mv1.Scalar234 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar234 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar234 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 MapUsingVersor(this Ga301KVector3 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector3
        {
            Scalar123 = (-2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar123 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar124 = (mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar134 = (-2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar24 + mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar24) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar34 + mv2.Scalar24 * mv1.Scalar234 * mv2.Scalar24) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 MapUsingVersor(this Ga301KVector3 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector3
        {
            Scalar123 = (-2 * mv2.Scalar123 * mv1.Scalar234 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar123 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar124 = (mv2.Scalar234 * mv1.Scalar124 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar134 = (-2 * mv2.Scalar134 * mv1.Scalar234 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar134 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 MapUsingVersor(this Ga301KVector3 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector3.Zero;
    }
    
    public static Ga301KVector3 MapUsingEvenVersor(this Ga301KVector3 mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
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
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar0 += -2 * mv2.KVector2.Scalar12 * mv1.Scalar234 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar13 * mv1.Scalar234 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.Scalar123 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.Scalar123 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar124 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.Scalar123 * mv2.KVector2.Scalar34;
            tempScalar1 += mv2.KVector2.Scalar23 * mv1.Scalar124 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.Scalar124 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.Scalar124 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar24 * mv1.Scalar134 * mv2.KVector2.Scalar34;
            tempScalar2 += -2 * mv2.KVector2.Scalar13 * mv1.Scalar234 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar23 * mv1.Scalar123 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.Scalar134 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar34 * mv1.Scalar134 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar14 * mv1.Scalar234 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar24 * mv1.Scalar134 * mv2.KVector2.Scalar24;
            tempScalar3 += -mv2.KVector2.Scalar23 * mv1.Scalar234 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar34 * mv1.Scalar234 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.Scalar234 * mv2.KVector2.Scalar24;
        }
        
        return new Ga301KVector3
        {
            Scalar123 = (tempScalar0) * mv2NormSquaredInv,
            Scalar124 = (tempScalar1) * mv2NormSquaredInv,
            Scalar134 = (tempScalar2) * mv2NormSquaredInv,
            Scalar234 = (tempScalar3) * mv2NormSquaredInv
        };
    }
    
    public static Ga301KVector3 MapUsingOddVersor(this Ga301KVector3 mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        var tempScalar2 = 0d;
        var tempScalar3 = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar0 += -2 * mv2.KVector1.Scalar1 * mv1.Scalar234 * mv2.KVector1.Scalar4 - mv2.KVector1.Scalar2 * mv1.Scalar123 * mv2.KVector1.Scalar2 + 2 * mv2.KVector1.Scalar2 * mv1.Scalar134 * mv2.KVector1.Scalar4 - mv2.KVector1.Scalar3 * mv1.Scalar123 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar124 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar4 * mv1.Scalar123 * mv2.KVector1.Scalar4;
            tempScalar1 += 2 * mv2.KVector1.Scalar1 * mv1.Scalar234 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar2 * mv1.Scalar124 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar134 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar123 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.Scalar124 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.Scalar124 * mv2.KVector1.Scalar4;
            tempScalar2 += -2 * mv2.KVector1.Scalar1 * mv1.Scalar234 * mv2.KVector1.Scalar2 + 2 * mv2.KVector1.Scalar2 * mv1.Scalar123 * mv2.KVector1.Scalar4 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar124 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.Scalar134 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.Scalar134 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.Scalar134 * mv2.KVector1.Scalar4;
            tempScalar3 += -mv2.KVector1.Scalar2 * mv1.Scalar234 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.Scalar234 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.Scalar234 * mv2.KVector1.Scalar4;
        }
        
        if (!mv2.KVector1.IsZero() && !mv2.KVector3.IsZero())
        {
            tempScalar0 += -2 * mv2.KVector1.Scalar2 * mv1.Scalar124 * mv2.KVector3.Scalar234 - 2 * mv2.KVector1.Scalar3 * mv1.Scalar134 * mv2.KVector3.Scalar234 + 2 * mv2.KVector1.Scalar3 * mv1.Scalar234 * mv2.KVector3.Scalar134;
            tempScalar1 += 2 * mv2.KVector1.Scalar2 * mv1.Scalar123 * mv2.KVector3.Scalar234;
            tempScalar2 += -2 * mv2.KVector1.Scalar3 * mv1.Scalar234 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar0 += -2 * mv2.KVector3.Scalar123 * mv1.Scalar234 * mv2.KVector3.Scalar234 - mv2.KVector3.Scalar234 * mv1.Scalar123 * mv2.KVector3.Scalar234;
            tempScalar1 += mv2.KVector3.Scalar234 * mv1.Scalar124 * mv2.KVector3.Scalar234;
            tempScalar2 += -mv2.KVector3.Scalar234 * mv1.Scalar134 * mv2.KVector3.Scalar234 - 2 * mv2.KVector3.Scalar134 * mv1.Scalar234 * mv2.KVector3.Scalar234;
            tempScalar3 += -mv2.KVector3.Scalar234 * mv1.Scalar234 * mv2.KVector3.Scalar234;
        }
        
        return new Ga301KVector3
        {
            Scalar123 = (tempScalar0) * mv2NormSquaredInv,
            Scalar124 = (tempScalar1) * mv2NormSquaredInv,
            Scalar134 = (tempScalar2) * mv2NormSquaredInv,
            Scalar234 = (tempScalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 MapUsingVersor(this Ga301KVector4 mv1, Ga301KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (mv2.Scalar * mv1.Scalar1234 * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 MapUsingVersor(this Ga301KVector4 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (-mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 MapUsingVersor(this Ga301KVector4 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (-mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar1234 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 MapUsingVersor(this Ga301KVector4 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (-mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 MapUsingVersor(this Ga301KVector4 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector4.Zero;
    }
    
    public static Ga301KVector4 MapUsingEvenVersor(this Ga301KVector4 mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv2.KVector0.Scalar * mv1.Scalar1234 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar += -mv2.KVector2.Scalar23 * mv1.Scalar1234 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar24 * mv1.Scalar1234 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.Scalar1234 * mv2.KVector2.Scalar34;
        }
        
        return new Ga301KVector4
        {
            Scalar1234 = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    public static Ga301KVector4 MapUsingOddVersor(this Ga301KVector4 mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar += -mv2.KVector1.Scalar2 * mv1.Scalar1234 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.Scalar1234 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.Scalar1234 * mv2.KVector1.Scalar4;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar += -mv2.KVector3.Scalar234 * mv1.Scalar1234 * mv2.KVector3.Scalar234;
        }
        
        return new Ga301KVector4
        {
            Scalar1234 = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    public static Ga301Multivector MapUsingVersor(this Ga301Multivector mv1, Ga301KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar * mv1.KVector0.Scalar * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar * mv1.KVector1.Scalar1 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar * mv1.KVector1.Scalar2 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar * mv1.KVector1.Scalar3 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[8] += (mv2.Scalar * mv1.KVector1.Scalar4 * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar * mv1.KVector2.Scalar12 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar * mv1.KVector2.Scalar13 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar * mv1.KVector2.Scalar23 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[9] += (mv2.Scalar * mv1.KVector2.Scalar14 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar * mv1.KVector2.Scalar24 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[12] += (mv2.Scalar * mv1.KVector2.Scalar34 * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar * mv1.KVector3.Scalar123 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[11] += (mv2.Scalar * mv1.KVector3.Scalar124 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[13] += (mv2.Scalar * mv1.KVector3.Scalar134 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar * mv1.KVector3.Scalar234 * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (mv2.Scalar * mv1.KVector4.Scalar1234 * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    public static Ga301Multivector MapUsingVersor(this Ga301Multivector mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector0.Scalar * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector1.Scalar4 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector1.Scalar1 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector1.Scalar2 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[4] += (-2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar4 + mv2.Scalar4 * mv1.KVector1.Scalar3 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[8] += (-2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar4 * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar12 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[5] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar4 + mv2.Scalar4 * mv1.KVector2.Scalar13 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar4 + mv2.Scalar4 * mv1.KVector2.Scalar23 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[9] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar14 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar24 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[12] += (2 * mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar34 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar34 * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar4 + mv2.Scalar4 * mv1.KVector3.Scalar123 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[11] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector3.Scalar124 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[13] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector3.Scalar134 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector3.Scalar134 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar2 * mv1.KVector3.Scalar234 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector3.Scalar234 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector3.Scalar234 * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    public static Ga301Multivector MapUsingVersor(this Ga301Multivector mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector0.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector0.Scalar * mv2.Scalar34) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar4 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar4 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar2 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar3 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector1.Scalar1 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector1.Scalar1 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector1.Scalar2 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector1.Scalar2 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar34 * mv1.KVector1.Scalar3 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar24) * mv2NormSquaredInv;
            tempScalar[8] += (-mv2.Scalar24 * mv1.KVector1.Scalar4 * mv2.Scalar24 + mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector1.Scalar4 * mv2.Scalar34) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector2.Scalar12 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector2.Scalar12 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar24 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector2.Scalar13 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar14) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector2.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector2.Scalar23 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar34 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar23 + mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar24 * mv2.Scalar24 - mv2.Scalar24 * mv1.KVector2.Scalar14 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector2.Scalar14 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector2.Scalar24 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[12] += (mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector2.Scalar34 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector2.Scalar34 * mv2.Scalar34) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar34 * mv1.KVector3.Scalar123 * mv2.Scalar34 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar24) * mv2NormSquaredInv;
            tempScalar[11] += (mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[13] += (-2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar24 + mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar24) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector3.Scalar234 * mv2.Scalar24) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector4.Scalar1234 * mv2.Scalar24) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    public static Ga301Multivector MapUsingVersor(this Ga301Multivector mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar234 * mv1.KVector0.Scalar * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar123 * mv1.KVector1.Scalar4 * mv2.Scalar234 + 2 * mv2.Scalar124 * mv1.KVector1.Scalar3 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.KVector1.Scalar2 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector1.Scalar1 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar234 * mv1.KVector1.Scalar2 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar234 * mv1.KVector1.Scalar3 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[8] += (mv2.Scalar234 * mv1.KVector1.Scalar4 * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar234 * mv1.KVector2.Scalar12 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[5] += (-2 * mv2.Scalar134 * mv1.KVector2.Scalar23 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector2.Scalar13 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar234 * mv1.KVector2.Scalar23 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar124 * mv1.KVector2.Scalar34 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.KVector2.Scalar24 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector2.Scalar14 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar234 * mv1.KVector2.Scalar24 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[12] += (-mv2.Scalar234 * mv1.KVector2.Scalar34 * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-2 * mv2.Scalar123 * mv1.KVector3.Scalar234 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector3.Scalar123 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[11] += (mv2.Scalar234 * mv1.KVector3.Scalar124 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[13] += (-2 * mv2.Scalar134 * mv1.KVector3.Scalar234 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector3.Scalar134 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector MapUsingVersor(this Ga301Multivector mv1, Ga301KVector4 mv2)
    {
        return Ga301Multivector.Zero;
    }
    
    public static Ga301Multivector MapUsingEvenVersor(this Ga301Multivector mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[0] += (mv2.KVector0.Scalar * mv1.KVector0.Scalar * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[0] += (mv2.KVector2.Scalar23 * mv1.KVector0.Scalar * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar24 * mv1.KVector0.Scalar * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.KVector0.Scalar * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[1] += (mv2.KVector0.Scalar * mv1.KVector1.Scalar1 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[2] += (mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[4] += (mv2.KVector0.Scalar * mv1.KVector1.Scalar3 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[8] += (mv2.KVector0.Scalar * mv1.KVector1.Scalar4 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
            {
                tempScalar[1] += (2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13 + 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar14) * mv2NormSquaredInv;
                tempScalar[2] += (2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24) * mv2NormSquaredInv;
                tempScalar[4] += (-2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
                tempScalar[8] += (-2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += (2 * mv2.KVector2.Scalar12 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar12 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar13 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar23 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar14 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar14 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
                tempScalar[2] += (mv2.KVector2.Scalar23 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar23 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar24 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
                tempScalar[4] += (mv2.KVector2.Scalar34 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24) * mv2NormSquaredInv;
                tempScalar[8] += (mv2.KVector2.Scalar23 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero() && !mv2.KVector4.IsZero())
            {
                tempScalar[1] += (2 * mv2.KVector2.Scalar34 * mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1234 + 2 * mv2.KVector2.Scalar23 * mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1234) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[3] += (mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[5] += (mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[6] += (mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[9] += (mv2.KVector0.Scalar * mv1.KVector2.Scalar14 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[10] += (mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[12] += (mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
            {
                tempScalar[3] += (2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar23 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar13 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar14) * mv2NormSquaredInv;
                tempScalar[5] += (-2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar14) * mv2NormSquaredInv;
                tempScalar[6] += (2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar24) * mv2NormSquaredInv;
                tempScalar[9] += (-2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar34 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar13) * mv2NormSquaredInv;
                tempScalar[10] += (-2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar34 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar23) * mv2NormSquaredInv;
                tempScalar[12] += (2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar23) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector0.IsZero() && !mv2.KVector4.IsZero())
            {
                tempScalar[3] += (-2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1234) * mv2NormSquaredInv;
                tempScalar[5] += (2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1234) * mv2NormSquaredInv;
                tempScalar[9] += (-2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1234) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += (mv2.KVector2.Scalar23 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar24 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
                tempScalar[5] += (2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar23 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar14 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar14 + mv2.KVector2.Scalar34 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
                tempScalar[6] += (-mv2.KVector2.Scalar23 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
                tempScalar[9] += (2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar23 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar14 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar14 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar24 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.KVector2.Scalar14 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
                tempScalar[10] += (-mv2.KVector2.Scalar24 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar24 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar23 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar34 * mv1.KVector2.Scalar24 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
                tempScalar[12] += (mv2.KVector2.Scalar24 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar34 * mv1.KVector2.Scalar34 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero() && !mv2.KVector4.IsZero())
            {
                tempScalar[3] += (2 * mv2.KVector2.Scalar23 * mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1234) * mv2NormSquaredInv;
                tempScalar[9] += (-2 * mv2.KVector2.Scalar34 * mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1234) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[7] += (mv2.KVector0.Scalar * mv1.KVector3.Scalar123 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[11] += (mv2.KVector0.Scalar * mv1.KVector3.Scalar124 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[13] += (mv2.KVector0.Scalar * mv1.KVector3.Scalar134 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[14] += (mv2.KVector0.Scalar * mv1.KVector3.Scalar234 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
            {
                tempScalar[7] += (2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar34 - 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar24 + 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar14) * mv2NormSquaredInv;
                tempScalar[11] += (-2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar34 + 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar23 - 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar13) * mv2NormSquaredInv;
                tempScalar[13] += (2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar24 - 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar12) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[7] += (-2 * mv2.KVector2.Scalar12 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar24 + 2 * mv2.KVector2.Scalar13 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar24 - 2 * mv2.KVector2.Scalar23 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar34 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
                tempScalar[11] += (mv2.KVector2.Scalar23 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar24 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar24 - mv2.KVector2.Scalar34 * mv1.KVector3.Scalar124 * mv2.KVector2.Scalar34 - 2 * mv2.KVector2.Scalar24 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar34) * mv2NormSquaredInv;
                tempScalar[13] += (-2 * mv2.KVector2.Scalar13 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar23 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar34 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar34 + 2 * mv2.KVector2.Scalar14 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar24 + mv2.KVector2.Scalar24 * mv1.KVector3.Scalar134 * mv2.KVector2.Scalar24) * mv2NormSquaredInv;
                tempScalar[14] += (-mv2.KVector2.Scalar23 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar34 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar34 + mv2.KVector2.Scalar24 * mv1.KVector3.Scalar234 * mv2.KVector2.Scalar24) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector4.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[15] += (mv2.KVector0.Scalar * mv1.KVector4.Scalar1234 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[15] += (-mv2.KVector2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar34 - mv2.KVector2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar24 * mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar24) * mv2NormSquaredInv;
            }
            
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    public static Ga301Multivector MapUsingOddVersor(this Ga301Multivector mv1, Ga301Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[0] += (mv2.KVector1.Scalar2 * mv1.KVector0.Scalar * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar3 * mv1.KVector0.Scalar * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar4 * mv1.KVector0.Scalar * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[0] += (mv2.KVector3.Scalar234 * mv1.KVector0.Scalar * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[1] += (-2 * mv2.KVector1.Scalar1 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar1 * mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar1 * mv1.KVector1.Scalar4 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar2 * mv1.KVector1.Scalar1 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar3 * mv1.KVector1.Scalar1 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar4 * mv1.KVector1.Scalar1 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
                tempScalar[2] += (-mv2.KVector1.Scalar2 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar2 * mv1.KVector1.Scalar4 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar4 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
                tempScalar[4] += (-2 * mv2.KVector1.Scalar2 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.KVector1.Scalar3 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.KVector1.Scalar4 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar4 * mv1.KVector1.Scalar3 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
                tempScalar[8] += (-2 * mv2.KVector1.Scalar2 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar2 * mv1.KVector1.Scalar4 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar3 * mv1.KVector1.Scalar3 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.KVector1.Scalar4 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.KVector1.Scalar4 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector1.IsZero() && !mv2.KVector3.IsZero())
            {
                tempScalar[1] += (2 * mv2.KVector1.Scalar3 * mv1.KVector1.Scalar2 * mv2.KVector3.Scalar123 + 2 * mv2.KVector1.Scalar4 * mv1.KVector1.Scalar2 * mv2.KVector3.Scalar124 + 2 * mv2.KVector1.Scalar4 * mv1.KVector1.Scalar3 * mv2.KVector3.Scalar134) * mv2NormSquaredInv;
                tempScalar[2] += (2 * mv2.KVector1.Scalar4 * mv1.KVector1.Scalar3 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[4] += (-2 * mv2.KVector1.Scalar4 * mv1.KVector1.Scalar2 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[8] += (2 * mv2.KVector1.Scalar3 * mv1.KVector1.Scalar2 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[1] += (2 * mv2.KVector3.Scalar123 * mv1.KVector1.Scalar4 * mv2.KVector3.Scalar234 + 2 * mv2.KVector3.Scalar134 * mv1.KVector1.Scalar2 * mv2.KVector3.Scalar234 + mv2.KVector3.Scalar234 * mv1.KVector1.Scalar1 * mv2.KVector3.Scalar234 + 2 * mv2.KVector3.Scalar124 * mv1.KVector1.Scalar3 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[2] += (mv2.KVector3.Scalar234 * mv1.KVector1.Scalar2 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[4] += (-mv2.KVector3.Scalar234 * mv1.KVector1.Scalar3 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[8] += (mv2.KVector3.Scalar234 * mv1.KVector1.Scalar4 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[3] += (2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar3 + 2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar24 * mv2.KVector1.Scalar4 - mv2.KVector1.Scalar2 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar13 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar14 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar4 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
                tempScalar[5] += (-2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar2 + 2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar34 * mv2.KVector1.Scalar4 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.KVector2.Scalar13 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.KVector2.Scalar13 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.KVector2.Scalar14 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar4 * mv1.KVector2.Scalar13 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
                tempScalar[6] += (-mv2.KVector1.Scalar2 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar2 + 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar34 * mv2.KVector1.Scalar4 - mv2.KVector1.Scalar3 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.KVector2.Scalar24 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar4 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
                tempScalar[9] += (-2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar24 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar34 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar2 * mv1.KVector2.Scalar14 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar3 * mv1.KVector2.Scalar13 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.KVector2.Scalar14 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.KVector2.Scalar14 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
                tempScalar[10] += (-mv2.KVector1.Scalar2 * mv1.KVector2.Scalar24 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar34 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.KVector2.Scalar24 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.KVector2.Scalar24 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
                tempScalar[12] += (2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar4 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar24 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.KVector2.Scalar34 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.KVector2.Scalar34 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.KVector2.Scalar34 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector1.IsZero() && !mv2.KVector3.IsZero())
            {
                tempScalar[3] += (2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar34 * mv2.KVector3.Scalar234 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar23 * mv2.KVector3.Scalar123 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar24 * mv2.KVector3.Scalar124 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar34 * mv2.KVector3.Scalar134 - 2 * mv2.KVector1.Scalar3 * mv1.KVector2.Scalar34 * mv2.KVector3.Scalar124 + 2 * mv2.KVector1.Scalar4 * mv1.KVector2.Scalar13 * mv2.KVector3.Scalar234 - 2 * mv2.KVector1.Scalar4 * mv1.KVector2.Scalar23 * mv2.KVector3.Scalar134) * mv2NormSquaredInv;
                tempScalar[5] += (-2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar24 * mv2.KVector3.Scalar234 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar24 * mv2.KVector3.Scalar134 + 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar34 * mv2.KVector3.Scalar124 - 2 * mv2.KVector1.Scalar3 * mv1.KVector2.Scalar34 * mv2.KVector3.Scalar134 - 2 * mv2.KVector1.Scalar4 * mv1.KVector2.Scalar12 * mv2.KVector3.Scalar234 + 2 * mv2.KVector1.Scalar4 * mv1.KVector2.Scalar23 * mv2.KVector3.Scalar124) * mv2NormSquaredInv;
                tempScalar[6] += (-2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar24 * mv2.KVector3.Scalar234 - 2 * mv2.KVector1.Scalar3 * mv1.KVector2.Scalar34 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[9] += (2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar23 * mv2.KVector3.Scalar234 + 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar23 * mv2.KVector3.Scalar134 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar34 * mv2.KVector3.Scalar123 + 2 * mv2.KVector1.Scalar3 * mv1.KVector2.Scalar12 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[10] += (2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar23 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector3.IsZero() && !mv2.KVector1.IsZero())
            {
                tempScalar[9] += (-2 * mv2.KVector3.Scalar123 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[3] += (mv2.KVector3.Scalar234 * mv1.KVector2.Scalar12 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[5] += (-mv2.KVector3.Scalar234 * mv1.KVector2.Scalar13 * mv2.KVector3.Scalar234 - 2 * mv2.KVector3.Scalar134 * mv1.KVector2.Scalar23 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[6] += (-mv2.KVector3.Scalar234 * mv1.KVector2.Scalar23 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[9] += (mv2.KVector3.Scalar234 * mv1.KVector2.Scalar14 * mv2.KVector3.Scalar234 + 2 * mv2.KVector3.Scalar134 * mv1.KVector2.Scalar24 * mv2.KVector3.Scalar234 + 2 * mv2.KVector3.Scalar124 * mv1.KVector2.Scalar34 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[10] += (mv2.KVector3.Scalar234 * mv1.KVector2.Scalar24 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[12] += (-mv2.KVector3.Scalar234 * mv1.KVector2.Scalar34 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[7] += (-2 * mv2.KVector1.Scalar1 * mv1.KVector3.Scalar234 * mv2.KVector1.Scalar4 - mv2.KVector1.Scalar2 * mv1.KVector3.Scalar123 * mv2.KVector1.Scalar2 + 2 * mv2.KVector1.Scalar2 * mv1.KVector3.Scalar134 * mv2.KVector1.Scalar4 - mv2.KVector1.Scalar3 * mv1.KVector3.Scalar123 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.KVector3.Scalar124 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar4 * mv1.KVector3.Scalar123 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
                tempScalar[11] += (2 * mv2.KVector1.Scalar1 * mv1.KVector3.Scalar234 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar2 * mv1.KVector3.Scalar124 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.KVector3.Scalar134 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar3 * mv1.KVector3.Scalar123 * mv2.KVector1.Scalar4 + mv2.KVector1.Scalar3 * mv1.KVector3.Scalar124 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.KVector3.Scalar124 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
                tempScalar[13] += (-2 * mv2.KVector1.Scalar1 * mv1.KVector3.Scalar234 * mv2.KVector1.Scalar2 + 2 * mv2.KVector1.Scalar2 * mv1.KVector3.Scalar123 * mv2.KVector1.Scalar4 - 2 * mv2.KVector1.Scalar2 * mv1.KVector3.Scalar124 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.KVector3.Scalar134 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.KVector3.Scalar134 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.KVector3.Scalar134 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
                tempScalar[14] += (-mv2.KVector1.Scalar2 * mv1.KVector3.Scalar234 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.KVector3.Scalar234 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.KVector3.Scalar234 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector1.IsZero() && !mv2.KVector3.IsZero())
            {
                tempScalar[7] += (-2 * mv2.KVector1.Scalar2 * mv1.KVector3.Scalar124 * mv2.KVector3.Scalar234 - 2 * mv2.KVector1.Scalar3 * mv1.KVector3.Scalar134 * mv2.KVector3.Scalar234 + 2 * mv2.KVector1.Scalar3 * mv1.KVector3.Scalar234 * mv2.KVector3.Scalar134) * mv2NormSquaredInv;
                tempScalar[11] += (2 * mv2.KVector1.Scalar2 * mv1.KVector3.Scalar123 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[13] += (-2 * mv2.KVector1.Scalar3 * mv1.KVector3.Scalar234 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[7] += (-2 * mv2.KVector3.Scalar123 * mv1.KVector3.Scalar234 * mv2.KVector3.Scalar234 - mv2.KVector3.Scalar234 * mv1.KVector3.Scalar123 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[11] += (mv2.KVector3.Scalar234 * mv1.KVector3.Scalar124 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[13] += (-mv2.KVector3.Scalar234 * mv1.KVector3.Scalar134 * mv2.KVector3.Scalar234 - 2 * mv2.KVector3.Scalar134 * mv1.KVector3.Scalar234 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
                tempScalar[14] += (-mv2.KVector3.Scalar234 * mv1.KVector3.Scalar234 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector4.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[15] += (-mv2.KVector1.Scalar2 * mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar4 * mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar4) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[15] += (-mv2.KVector3.Scalar234 * mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar234) * mv2NormSquaredInv;
            }
            
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
}

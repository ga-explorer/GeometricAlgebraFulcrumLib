using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga3;

public static class Ga3VersorMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 MapUsingVersor(this Ga3KVector0 mv1, Ga3KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar * mv1.Scalar * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 MapUsingVersor(this Ga3KVector0 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 MapUsingVersor(this Ga3KVector0 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar12 * mv1.Scalar * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 MapUsingVersor(this Ga3KVector0 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar123 * mv1.Scalar * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga3KVector0 MapUsingEvenVersor(this Ga3KVector0 mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv2.KVector0.Scalar * mv1.Scalar * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar += mv2.KVector2.Scalar12 * mv1.Scalar * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar13 * mv1.Scalar * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.Scalar * mv2.KVector2.Scalar23;
        }
        
        return new Ga3KVector0
        {
            Scalar = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    public static Ga3KVector0 MapUsingOddVersor(this Ga3KVector0 mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar += mv2.KVector1.Scalar1 * mv1.Scalar * mv2.KVector1.Scalar1 + mv2.KVector1.Scalar2 * mv1.Scalar * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar3 * mv1.Scalar * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar += mv2.KVector3.Scalar123 * mv1.Scalar * mv2.KVector3.Scalar123;
        }
        
        return new Ga3KVector0
        {
            Scalar = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 MapUsingVersor(this Ga3KVector1 mv1, Ga3KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector1
        {
            Scalar1 = (mv2.Scalar * mv1.Scalar1 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar * mv1.Scalar2 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar * mv1.Scalar3 * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 MapUsingVersor(this Ga3KVector1 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector1
        {
            Scalar1 = (-mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar2 = (-2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 + mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar3 = (-2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar3 + mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar1 - 2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 MapUsingVersor(this Ga3KVector1 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector1
        {
            Scalar1 = (mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 + mv2.Scalar13 * mv1.Scalar1 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23 - mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar13) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar3 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 MapUsingVersor(this Ga3KVector1 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector1
        {
            Scalar1 = (mv2.Scalar123 * mv1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar123 * mv1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar123 * mv1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga3KVector1 MapUsingEvenVersor(this Ga3KVector1 mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        var tempScalar2 = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar0 += mv2.KVector0.Scalar * mv1.Scalar1 * mv2.KVector0.Scalar;
            tempScalar1 += mv2.KVector0.Scalar * mv1.Scalar2 * mv2.KVector0.Scalar;
            tempScalar2 += mv2.KVector0.Scalar * mv1.Scalar3 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector0.Scalar * mv1.Scalar2 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.Scalar3 * mv2.KVector2.Scalar13;
            tempScalar1 += -2 * mv2.KVector0.Scalar * mv1.Scalar1 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.Scalar3 * mv2.KVector2.Scalar23;
            tempScalar2 += -2 * mv2.KVector0.Scalar * mv1.Scalar1 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.Scalar2 * mv2.KVector2.Scalar23;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar0 += mv2.KVector2.Scalar12 * mv1.Scalar1 * mv2.KVector2.Scalar12 + 2 * mv2.KVector2.Scalar12 * mv1.Scalar3 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar13 * mv1.Scalar1 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.Scalar1 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.Scalar2 * mv2.KVector2.Scalar23;
            tempScalar1 += mv2.KVector2.Scalar23 * mv1.Scalar2 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar12 * mv1.Scalar2 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.Scalar2 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.Scalar3 * mv2.KVector2.Scalar13;
            tempScalar2 += mv2.KVector2.Scalar12 * mv1.Scalar3 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.Scalar3 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.Scalar3 * mv2.KVector2.Scalar23;
        }
        
        return new Ga3KVector1
        {
            Scalar1 = (tempScalar0) * mv2NormSquaredInv,
            Scalar2 = (tempScalar1) * mv2NormSquaredInv,
            Scalar3 = (tempScalar2) * mv2NormSquaredInv
        };
    }
    
    public static Ga3KVector1 MapUsingOddVersor(this Ga3KVector1 mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        var tempScalar2 = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar0 += -mv2.KVector1.Scalar1 * mv1.Scalar1 * mv2.KVector1.Scalar1 - 2 * mv2.KVector1.Scalar1 * mv1.Scalar2 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar1 * mv1.Scalar3 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.Scalar1 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar3 * mv1.Scalar1 * mv2.KVector1.Scalar3;
            tempScalar1 += -2 * mv2.KVector1.Scalar1 * mv1.Scalar1 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar1 * mv1.Scalar2 * mv2.KVector1.Scalar1 - mv2.KVector1.Scalar2 * mv1.Scalar2 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar3 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar3 * mv1.Scalar2 * mv2.KVector1.Scalar3;
            tempScalar2 += -2 * mv2.KVector1.Scalar1 * mv1.Scalar1 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar1 * mv1.Scalar3 * mv2.KVector1.Scalar1 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar2 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.Scalar3 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.Scalar3 * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector1.IsZero() && !mv2.KVector3.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector1.Scalar3 * mv1.Scalar2 * mv2.KVector3.Scalar123;
            tempScalar1 += -2 * mv2.KVector1.Scalar3 * mv1.Scalar1 * mv2.KVector3.Scalar123;
            tempScalar2 += 2 * mv2.KVector1.Scalar2 * mv1.Scalar1 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar0 += mv2.KVector3.Scalar123 * mv1.Scalar1 * mv2.KVector3.Scalar123;
            tempScalar1 += -mv2.KVector3.Scalar123 * mv1.Scalar2 * mv2.KVector3.Scalar123;
            tempScalar2 += mv2.KVector3.Scalar123 * mv1.Scalar3 * mv2.KVector3.Scalar123;
        }
        
        return new Ga3KVector1
        {
            Scalar1 = (tempScalar0) * mv2NormSquaredInv,
            Scalar2 = (tempScalar1) * mv2NormSquaredInv,
            Scalar3 = (tempScalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 MapUsingVersor(this Ga3KVector2 mv1, Ga3KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector2
        {
            Scalar12 = (mv2.Scalar * mv1.Scalar12 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar * mv1.Scalar13 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar * mv1.Scalar23 * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 MapUsingVersor(this Ga3KVector2 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector2
        {
            Scalar12 = (-mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 + mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar13 = (-mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar23 = (2 * mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar2 + mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 MapUsingVersor(this Ga3KVector2 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector2
        {
            Scalar12 = (-mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar12 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar23 = (-2 * mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar23 - mv2.Scalar12 * mv1.Scalar23 * mv2.Scalar12 - mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 + mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 MapUsingVersor(this Ga3KVector2 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector2
        {
            Scalar12 = (-mv2.Scalar123 * mv1.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar123 * mv1.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar123 * mv1.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga3KVector2 MapUsingEvenVersor(this Ga3KVector2 mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        var tempScalar2 = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar0 += mv2.KVector0.Scalar * mv1.Scalar12 * mv2.KVector0.Scalar;
            tempScalar1 += mv2.KVector0.Scalar * mv1.Scalar13 * mv2.KVector0.Scalar;
            tempScalar2 += mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector0.Scalar * mv1.Scalar13 * mv2.KVector2.Scalar23 - 2 * mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector2.Scalar13;
            tempScalar1 += -2 * mv2.KVector0.Scalar * mv1.Scalar12 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.Scalar23 * mv2.KVector2.Scalar12;
            tempScalar2 += 2 * mv2.KVector0.Scalar * mv1.Scalar12 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.Scalar13 * mv2.KVector2.Scalar12;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar0 += -mv2.KVector2.Scalar12 * mv1.Scalar12 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.Scalar12 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.Scalar13 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.Scalar12 * mv2.KVector2.Scalar23;
            tempScalar1 += mv2.KVector2.Scalar12 * mv1.Scalar13 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.Scalar13 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.Scalar13 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar13 * mv1.Scalar23 * mv2.KVector2.Scalar23;
            tempScalar2 += -2 * mv2.KVector2.Scalar12 * mv1.Scalar12 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar12 * mv1.Scalar23 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar23 * mv1.Scalar23 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar13 * mv1.Scalar23 * mv2.KVector2.Scalar13;
        }
        
        return new Ga3KVector2
        {
            Scalar12 = (tempScalar0) * mv2NormSquaredInv,
            Scalar13 = (tempScalar1) * mv2NormSquaredInv,
            Scalar23 = (tempScalar2) * mv2NormSquaredInv
        };
    }
    
    public static Ga3KVector2 MapUsingOddVersor(this Ga3KVector2 mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        var tempScalar2 = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar0 += -mv2.KVector1.Scalar1 * mv1.Scalar12 * mv2.KVector1.Scalar1 + 2 * mv2.KVector1.Scalar1 * mv1.Scalar23 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar2 * mv1.Scalar12 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar13 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar3 * mv1.Scalar12 * mv2.KVector1.Scalar3;
            tempScalar1 += -mv2.KVector1.Scalar1 * mv1.Scalar13 * mv2.KVector1.Scalar1 - 2 * mv2.KVector1.Scalar1 * mv1.Scalar23 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar12 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.Scalar13 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.Scalar13 * mv2.KVector1.Scalar3;
            tempScalar2 += 2 * mv2.KVector1.Scalar1 * mv1.Scalar12 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar1 * mv1.Scalar13 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar1 * mv1.Scalar23 * mv2.KVector1.Scalar1 - mv2.KVector1.Scalar2 * mv1.Scalar23 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.Scalar23 * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector1.IsZero() && !mv2.KVector3.IsZero())
        {
            tempScalar0 += -2 * mv2.KVector1.Scalar1 * mv1.Scalar13 * mv2.KVector3.Scalar123 - 2 * mv2.KVector1.Scalar2 * mv1.Scalar23 * mv2.KVector3.Scalar123;
            tempScalar1 += 2 * mv2.KVector1.Scalar1 * mv1.Scalar12 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar0 += -mv2.KVector3.Scalar123 * mv1.Scalar12 * mv2.KVector3.Scalar123;
            tempScalar1 += mv2.KVector3.Scalar123 * mv1.Scalar13 * mv2.KVector3.Scalar123;
            tempScalar2 += -mv2.KVector3.Scalar123 * mv1.Scalar23 * mv2.KVector3.Scalar123;
        }
        
        return new Ga3KVector2
        {
            Scalar12 = (tempScalar0) * mv2NormSquaredInv,
            Scalar13 = (tempScalar1) * mv2NormSquaredInv,
            Scalar23 = (tempScalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 MapUsingVersor(this Ga3KVector3 mv1, Ga3KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (mv2.Scalar * mv1.Scalar123 * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 MapUsingVersor(this Ga3KVector3 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (-mv2.Scalar1 * mv1.Scalar123 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 MapUsingVersor(this Ga3KVector3 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (-mv2.Scalar12 * mv1.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 MapUsingVersor(this Ga3KVector3 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (-mv2.Scalar123 * mv1.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga3KVector3 MapUsingEvenVersor(this Ga3KVector3 mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv2.KVector0.Scalar * mv1.Scalar123 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar += -mv2.KVector2.Scalar12 * mv1.Scalar123 * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar13 * mv1.Scalar123 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.Scalar123 * mv2.KVector2.Scalar23;
        }
        
        return new Ga3KVector3
        {
            Scalar123 = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    public static Ga3KVector3 MapUsingOddVersor(this Ga3KVector3 mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar += -mv2.KVector1.Scalar1 * mv1.Scalar123 * mv2.KVector1.Scalar1 - mv2.KVector1.Scalar2 * mv1.Scalar123 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.Scalar123 * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar += -mv2.KVector3.Scalar123 * mv1.Scalar123 * mv2.KVector3.Scalar123;
        }
        
        return new Ga3KVector3
        {
            Scalar123 = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    public static Ga3Multivector MapUsingVersor(this Ga3Multivector mv1, Ga3KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar * mv1.KVector0.Scalar * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar * mv1.KVector1.Scalar1 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar * mv1.KVector1.Scalar2 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar * mv1.KVector1.Scalar3 * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar * mv1.KVector2.Scalar12 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar * mv1.KVector2.Scalar13 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar * mv1.KVector2.Scalar23 * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar * mv1.KVector3.Scalar123 * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    public static Ga3Multivector MapUsingVersor(this Ga3Multivector mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar1 * mv1.KVector0.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[2] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 + mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[4] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar3 + mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar1 - 2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 + mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[5] += (-mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[6] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar2 + mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar1 * mv1.KVector3.Scalar123 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    public static Ga3Multivector MapUsingVersor(this Ga3Multivector mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar12 * mv1.KVector0.Scalar * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector0.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 + mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23 - mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar13) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector1.Scalar3 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar12 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[6] += (-2 * mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar23 - mv2.Scalar12 * mv1.KVector2.Scalar23 * mv2.Scalar12 - mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 + mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar12 * mv1.KVector3.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector3.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    public static Ga3Multivector MapUsingVersor(this Ga3Multivector mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar123 * mv1.KVector0.Scalar * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar123 * mv1.KVector1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar123 * mv1.KVector1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar123 * mv1.KVector1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar123 * mv1.KVector2.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar123 * mv1.KVector2.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar123 * mv1.KVector2.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar123 * mv1.KVector3.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    public static Ga3Multivector MapUsingEvenVersor(this Ga3Multivector mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[0] += (mv2.KVector0.Scalar * mv1.KVector0.Scalar * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[0] += (mv2.KVector2.Scalar12 * mv1.KVector0.Scalar * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar13 * mv1.KVector0.Scalar * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.KVector0.Scalar * mv2.KVector2.Scalar23) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[1] += (mv2.KVector0.Scalar * mv1.KVector1.Scalar1 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[2] += (mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[4] += (mv2.KVector0.Scalar * mv1.KVector1.Scalar3 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
            {
                tempScalar[1] += (2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13) * mv2NormSquaredInv;
                tempScalar[2] += (-2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar12 + 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23) * mv2NormSquaredInv;
                tempScalar[4] += (-2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += (mv2.KVector2.Scalar12 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar12 + 2 * mv2.KVector2.Scalar12 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar13 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar23 + 2 * mv2.KVector2.Scalar13 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23) * mv2NormSquaredInv;
                tempScalar[2] += (mv2.KVector2.Scalar23 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar12 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13) * mv2NormSquaredInv;
                tempScalar[4] += (mv2.KVector2.Scalar12 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[3] += (mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[5] += (mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[6] += (mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
            {
                tempScalar[3] += (2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar23 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar13) * mv2NormSquaredInv;
                tempScalar[5] += (-2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23 + 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar12) * mv2NormSquaredInv;
                tempScalar[6] += (2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar13 - 2 * mv2.KVector0.Scalar * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar12) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += (-mv2.KVector2.Scalar12 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar13 - 2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar13 + mv2.KVector2.Scalar23 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23) * mv2NormSquaredInv;
                tempScalar[5] += (mv2.KVector2.Scalar12 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar13 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.KVector2.Scalar13 * mv2.KVector2.Scalar23 - 2 * mv2.KVector2.Scalar13 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23) * mv2NormSquaredInv;
                tempScalar[6] += (-2 * mv2.KVector2.Scalar12 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23 - mv2.KVector2.Scalar12 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar12 - mv2.KVector2.Scalar23 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23 + mv2.KVector2.Scalar13 * mv1.KVector2.Scalar23 * mv2.KVector2.Scalar13) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[7] += (mv2.KVector0.Scalar * mv1.KVector3.Scalar123 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[7] += (-mv2.KVector2.Scalar12 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar12 + mv2.KVector2.Scalar13 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar13 - mv2.KVector2.Scalar23 * mv1.KVector3.Scalar123 * mv2.KVector2.Scalar23) * mv2NormSquaredInv;
            }
            
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    public static Ga3Multivector MapUsingOddVersor(this Ga3Multivector mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[0] += (mv2.KVector1.Scalar1 * mv1.KVector0.Scalar * mv2.KVector1.Scalar1 + mv2.KVector1.Scalar2 * mv1.KVector0.Scalar * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar3 * mv1.KVector0.Scalar * mv2.KVector1.Scalar3) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[0] += (mv2.KVector3.Scalar123 * mv1.KVector0.Scalar * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[1] += (-mv2.KVector1.Scalar1 * mv1.KVector1.Scalar1 * mv2.KVector1.Scalar1 - 2 * mv2.KVector1.Scalar1 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar1 * mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.KVector1.Scalar1 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar3 * mv1.KVector1.Scalar1 * mv2.KVector1.Scalar3) * mv2NormSquaredInv;
                tempScalar[2] += (-2 * mv2.KVector1.Scalar1 * mv1.KVector1.Scalar1 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar1 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar1 - mv2.KVector1.Scalar2 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar3 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar3) * mv2NormSquaredInv;
                tempScalar[4] += (-2 * mv2.KVector1.Scalar1 * mv1.KVector1.Scalar1 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar1 * mv1.KVector1.Scalar3 * mv2.KVector1.Scalar1 - 2 * mv2.KVector1.Scalar2 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.KVector1.Scalar3 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector1.IsZero() && !mv2.KVector3.IsZero())
            {
                tempScalar[1] += (2 * mv2.KVector1.Scalar3 * mv1.KVector1.Scalar2 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
                tempScalar[2] += (-2 * mv2.KVector1.Scalar3 * mv1.KVector1.Scalar1 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
                tempScalar[4] += (2 * mv2.KVector1.Scalar2 * mv1.KVector1.Scalar1 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[1] += (mv2.KVector3.Scalar123 * mv1.KVector1.Scalar1 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
                tempScalar[2] += (-mv2.KVector3.Scalar123 * mv1.KVector1.Scalar2 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
                tempScalar[4] += (mv2.KVector3.Scalar123 * mv1.KVector1.Scalar3 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[3] += (-mv2.KVector1.Scalar1 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar1 + 2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar3 - mv2.KVector1.Scalar2 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar13 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar3 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar3) * mv2NormSquaredInv;
                tempScalar[5] += (-mv2.KVector1.Scalar1 * mv1.KVector2.Scalar13 * mv2.KVector1.Scalar1 - 2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar2 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar3 + mv2.KVector1.Scalar2 * mv1.KVector2.Scalar13 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.KVector2.Scalar13 * mv2.KVector1.Scalar3) * mv2NormSquaredInv;
                tempScalar[6] += (2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar3 - 2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar13 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar1 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar1 - mv2.KVector1.Scalar2 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.KVector2.Scalar23 * mv2.KVector1.Scalar3) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector1.IsZero() && !mv2.KVector3.IsZero())
            {
                tempScalar[3] += (-2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar13 * mv2.KVector3.Scalar123 - 2 * mv2.KVector1.Scalar2 * mv1.KVector2.Scalar23 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
                tempScalar[5] += (2 * mv2.KVector1.Scalar1 * mv1.KVector2.Scalar12 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[3] += (-mv2.KVector3.Scalar123 * mv1.KVector2.Scalar12 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
                tempScalar[5] += (mv2.KVector3.Scalar123 * mv1.KVector2.Scalar13 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
                tempScalar[6] += (-mv2.KVector3.Scalar123 * mv1.KVector2.Scalar23 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[7] += (-mv2.KVector1.Scalar1 * mv1.KVector3.Scalar123 * mv2.KVector1.Scalar1 - mv2.KVector1.Scalar2 * mv1.KVector3.Scalar123 * mv2.KVector1.Scalar2 - mv2.KVector1.Scalar3 * mv1.KVector3.Scalar123 * mv2.KVector1.Scalar3) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[7] += (-mv2.KVector3.Scalar123 * mv1.KVector3.Scalar123 * mv2.KVector3.Scalar123) * mv2NormSquaredInv;
            }
            
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
}

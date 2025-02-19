using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga3;

public static class Ga3Reflection
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDirectOnDirect(this Ga3KVector0 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDirectOnDirect(this Ga3KVector0 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar12 * mv1.Scalar * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDirectOnDirect(this Ga3KVector0 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar123 * mv1.Scalar * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 ReflectDirectOnDirect(this Ga3KVector1 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector1
        {
            Scalar1 = (mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar2 = (2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar3 = (2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar3 - mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 ReflectDirectOnDirect(this Ga3KVector1 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector1
        {
            Scalar1 = (-mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12 - 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 - mv2.Scalar13 * mv1.Scalar1 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23 + mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar13 + 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar13) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar3 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 ReflectDirectOnDirect(this Ga3KVector1 mv1, Ga3KVector3 mv2)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 ReflectDirectOnDirect(this Ga3KVector2 mv1, Ga3KVector1 mv2)
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
    public static Ga3KVector2 ReflectDirectOnDirect(this Ga3KVector2 mv1, Ga3KVector2 mv2)
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
    public static Ga3KVector2 ReflectDirectOnDirect(this Ga3KVector2 mv1, Ga3KVector3 mv2)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDirectOnDirect(this Ga3KVector3 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (mv2.Scalar1 * mv1.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDirectOnDirect(this Ga3KVector3 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (mv2.Scalar12 * mv1.Scalar123 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar123 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDirectOnDirect(this Ga3KVector3 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (-mv2.Scalar123 * mv1.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga3Multivector ReflectDirectOnDirect(this Ga3Multivector mv1, Ga3KVector1 mv2)
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
            tempScalar[1] += (mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[2] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[4] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar3 - mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 + mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[5] += (-mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[6] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar2 + mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar1 * mv1.KVector3.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    public static Ga3Multivector ReflectDirectOnDirect(this Ga3Multivector mv1, Ga3KVector2 mv2)
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
            tempScalar[1] += (-mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12 - 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 - mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23 + mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar13 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar13) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector1.Scalar3 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar12 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[6] += (-2 * mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar23 - mv2.Scalar12 * mv1.KVector2.Scalar23 * mv2.Scalar12 - mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 + mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar12 * mv1.KVector3.Scalar123 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector3.Scalar123 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    public static Ga3Multivector ReflectDirectOnDirect(this Ga3Multivector mv1, Ga3KVector3 mv2)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDirectOnDual(this Ga3KVector0 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDirectOnDual(this Ga3KVector0 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar12 * mv1.Scalar * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDirectOnDual(this Ga3KVector0 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar123 * mv1.Scalar * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 ReflectDirectOnDual(this Ga3KVector1 mv1, Ga3KVector1 mv2)
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
    public static Ga3KVector1 ReflectDirectOnDual(this Ga3KVector1 mv1, Ga3KVector2 mv2)
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
    public static Ga3KVector1 ReflectDirectOnDual(this Ga3KVector1 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector1
        {
            Scalar1 = (-mv2.Scalar123 * mv1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar123 * mv1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar123 * mv1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 ReflectDirectOnDual(this Ga3KVector2 mv1, Ga3KVector1 mv2)
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
    public static Ga3KVector2 ReflectDirectOnDual(this Ga3KVector2 mv1, Ga3KVector2 mv2)
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
    public static Ga3KVector2 ReflectDirectOnDual(this Ga3KVector2 mv1, Ga3KVector3 mv2)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDirectOnDual(this Ga3KVector3 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (-mv2.Scalar1 * mv1.Scalar123 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDirectOnDual(this Ga3KVector3 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (-mv2.Scalar12 * mv1.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDirectOnDual(this Ga3KVector3 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (mv2.Scalar123 * mv1.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga3Multivector ReflectDirectOnDual(this Ga3Multivector mv1, Ga3KVector1 mv2)
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
    
    public static Ga3Multivector ReflectDirectOnDual(this Ga3Multivector mv1, Ga3KVector2 mv2)
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
    
    public static Ga3Multivector ReflectDirectOnDual(this Ga3Multivector mv1, Ga3KVector3 mv2)
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
            tempScalar[1] += (-mv2.Scalar123 * mv1.KVector1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar123 * mv1.KVector1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar123 * mv1.KVector1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar123 * mv1.KVector2.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar123 * mv1.KVector2.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar123 * mv1.KVector2.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar123 * mv1.KVector3.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDualOnDirect(this Ga3KVector0 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDualOnDirect(this Ga3KVector0 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (-mv2.Scalar12 * mv1.Scalar * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDualOnDirect(this Ga3KVector0 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar123 * mv1.Scalar * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 ReflectDualOnDirect(this Ga3KVector1 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector1
        {
            Scalar1 = (mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar2 = (2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar3 = (2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar3 - mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 ReflectDualOnDirect(this Ga3KVector1 mv1, Ga3KVector2 mv2)
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
    public static Ga3KVector1 ReflectDualOnDirect(this Ga3KVector1 mv1, Ga3KVector3 mv2)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 ReflectDualOnDirect(this Ga3KVector2 mv1, Ga3KVector1 mv2)
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
    public static Ga3KVector2 ReflectDualOnDirect(this Ga3KVector2 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector2
        {
            Scalar12 = (mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar12 * mv2.Scalar13 + 2 * mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar13 = (-mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar23 = (2 * mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar23 + mv2.Scalar12 * mv1.Scalar23 * mv2.Scalar12 + mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 - mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 ReflectDualOnDirect(this Ga3KVector2 mv1, Ga3KVector3 mv2)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDualOnDirect(this Ga3KVector3 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (mv2.Scalar1 * mv1.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDualOnDirect(this Ga3KVector3 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (-mv2.Scalar12 * mv1.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDualOnDirect(this Ga3KVector3 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (-mv2.Scalar123 * mv1.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga3Multivector ReflectDualOnDirect(this Ga3Multivector mv1, Ga3KVector1 mv2)
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
            tempScalar[1] += (mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[2] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[4] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar3 - mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 + mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[5] += (-mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[6] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar2 + mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar1 * mv1.KVector3.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    public static Ga3Multivector ReflectDualOnDirect(this Ga3Multivector mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar12 * mv1.KVector0.Scalar * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector0.Scalar * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 + mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23 - mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar13) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector1.Scalar3 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector2.Scalar12 * mv2.Scalar13 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[5] += (-mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[6] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar23 + mv2.Scalar12 * mv1.KVector2.Scalar23 * mv2.Scalar12 + mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 - mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar12 * mv1.KVector3.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector3.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    public static Ga3Multivector ReflectDualOnDirect(this Ga3Multivector mv1, Ga3KVector3 mv2)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDualOnDual(this Ga3KVector0 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (-mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDualOnDual(this Ga3KVector0 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (mv2.Scalar12 * mv1.Scalar * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 ReflectDualOnDual(this Ga3KVector0 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector0
        {
            Scalar = (-mv2.Scalar123 * mv1.Scalar * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 ReflectDualOnDual(this Ga3KVector1 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector1
        {
            Scalar1 = (mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar2 = (2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar3 = (2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar3 - mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 ReflectDualOnDual(this Ga3KVector1 mv1, Ga3KVector2 mv2)
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
    public static Ga3KVector1 ReflectDualOnDual(this Ga3KVector1 mv1, Ga3KVector3 mv2)
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 ReflectDualOnDual(this Ga3KVector2 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector2
        {
            Scalar12 = (mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 - mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar23 = (-2 * mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 ReflectDualOnDual(this Ga3KVector2 mv1, Ga3KVector2 mv2)
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
    public static Ga3KVector2 ReflectDualOnDual(this Ga3KVector2 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector2
        {
            Scalar12 = (mv2.Scalar123 * mv1.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar13 = (-mv2.Scalar123 * mv1.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar123 * mv1.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDualOnDual(this Ga3KVector3 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (mv2.Scalar1 * mv1.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDualOnDual(this Ga3KVector3 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (-mv2.Scalar12 * mv1.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector3 ReflectDualOnDual(this Ga3KVector3 mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga3KVector3
        {
            Scalar123 = (-mv2.Scalar123 * mv1.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga3Multivector ReflectDualOnDual(this Ga3Multivector mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar1 * mv1.KVector0.Scalar * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[2] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[4] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar3 - mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 - mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[6] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar1 * mv1.KVector3.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    public static Ga3Multivector ReflectDualOnDual(this Ga3Multivector mv1, Ga3KVector2 mv2)
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
    
    public static Ga3Multivector ReflectDualOnDual(this Ga3Multivector mv1, Ga3KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar123 * mv1.KVector0.Scalar * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar123 * mv1.KVector1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar123 * mv1.KVector1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar123 * mv1.KVector1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar123 * mv1.KVector2.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[5] += (-mv2.Scalar123 * mv1.KVector2.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar123 * mv1.KVector2.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar123 * mv1.KVector3.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
}

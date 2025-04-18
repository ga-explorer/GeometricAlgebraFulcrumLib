using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga21;

public static class Ga21Reflection
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDirectOnDirect(this Ga21KVector0 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (-mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDirectOnDirect(this Ga21KVector0 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (-mv2.Scalar12 * mv1.Scalar * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDirectOnDirect(this Ga21KVector0 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (-mv2.Scalar123 * mv1.Scalar * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDirectOnDirect(this Ga21KVector1 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (-mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar2 = (-2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar3 = (-2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar3 + mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDirectOnDirect(this Ga21KVector1 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (-mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12 - 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 - mv2.Scalar13 * mv1.Scalar1 * mv2.Scalar13 - 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 - mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar13 - 2 * mv2.Scalar13 * mv1.Scalar1 * mv2.Scalar23 - mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar3 = (2 * mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar23 + mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar3 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDirectOnDirect(this Ga21KVector1 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (mv2.Scalar123 * mv1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar123 * mv1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar123 * mv1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDirectOnDirect(this Ga21KVector2 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 + mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar23 = (-2 * mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDirectOnDirect(this Ga21KVector2 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (-mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar12 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar13) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar13 - 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23 - mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar12 * mv1.Scalar23 * mv2.Scalar12 - mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar23 - mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDirectOnDirect(this Ga21KVector2 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (-mv2.Scalar123 * mv1.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar123 * mv1.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar123 * mv1.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDirectOnDirect(this Ga21KVector3 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (-mv2.Scalar1 * mv1.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDirectOnDirect(this Ga21KVector3 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (mv2.Scalar12 * mv1.Scalar123 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar123 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDirectOnDirect(this Ga21KVector3 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (-mv2.Scalar123 * mv1.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga21Multivector ReflectDirectOnDirect(this Ga21Multivector mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar1 * mv1.KVector0.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[2] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[4] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar3 + mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 + mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[6] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar1 * mv1.KVector3.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    public static Ga21Multivector ReflectDirectOnDirect(this Ga21Multivector mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar12 * mv1.KVector0.Scalar * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector0.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12 - 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 - mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar13 - 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 - mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar13 - 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar13 - 2 * mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar23 - mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[4] += (2 * mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar23 + mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector1.Scalar3 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar12 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar13) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar13 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23 - mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar12 * mv1.KVector2.Scalar23 * mv2.Scalar12 - mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar23 - mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar12 * mv1.KVector3.Scalar123 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector3.Scalar123 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    public static Ga21Multivector ReflectDirectOnDirect(this Ga21Multivector mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar123 * mv1.KVector0.Scalar * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar123 * mv1.KVector1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar123 * mv1.KVector1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar123 * mv1.KVector1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar123 * mv1.KVector2.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar123 * mv1.KVector2.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar123 * mv1.KVector2.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar123 * mv1.KVector3.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDirectOnDual(this Ga21KVector0 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (-mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDirectOnDual(this Ga21KVector0 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (-mv2.Scalar12 * mv1.Scalar * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDirectOnDual(this Ga21KVector0 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (-mv2.Scalar123 * mv1.Scalar * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDirectOnDual(this Ga21KVector1 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar2 = (2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 + mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar3 = (2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar3 - mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar1 - 2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDirectOnDual(this Ga21KVector1 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 + mv2.Scalar13 * mv1.Scalar1 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 + mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar13 + 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.Scalar1 * mv2.Scalar23 + mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar3 = (-2 * mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar23 - mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar3 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDirectOnDual(this Ga21KVector1 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (-mv2.Scalar123 * mv1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar123 * mv1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar123 * mv1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDirectOnDual(this Ga21KVector2 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 + mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar23 = (-2 * mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDirectOnDual(this Ga21KVector2 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (-mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar12 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar13) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar13 - 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23 - mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar12 * mv1.Scalar23 * mv2.Scalar12 - mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar23 - mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDirectOnDual(this Ga21KVector2 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (-mv2.Scalar123 * mv1.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar123 * mv1.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar123 * mv1.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDirectOnDual(this Ga21KVector3 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (mv2.Scalar1 * mv1.Scalar123 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDirectOnDual(this Ga21KVector3 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (-mv2.Scalar12 * mv1.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDirectOnDual(this Ga21KVector3 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (mv2.Scalar123 * mv1.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga21Multivector ReflectDirectOnDual(this Ga21Multivector mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar1 * mv1.KVector0.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[2] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 + mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[4] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar3 - mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar1 - 2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 + mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[6] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar1 * mv1.KVector3.Scalar123 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    public static Ga21Multivector ReflectDirectOnDual(this Ga21Multivector mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar12 * mv1.KVector0.Scalar * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector0.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 + mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 + mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar13 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar23 + mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[4] += (-2 * mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar23 - mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector1.Scalar3 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar12 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar13) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar13 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23 - mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar12 * mv1.KVector2.Scalar23 * mv2.Scalar12 - mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar23 - mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar12 * mv1.KVector3.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector3.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    public static Ga21Multivector ReflectDirectOnDual(this Ga21Multivector mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar123 * mv1.KVector0.Scalar * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-mv2.Scalar123 * mv1.KVector1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar123 * mv1.KVector1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar123 * mv1.KVector1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar123 * mv1.KVector2.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar123 * mv1.KVector2.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar123 * mv1.KVector2.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar123 * mv1.KVector3.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDualOnDirect(this Ga21KVector0 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (-mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDualOnDirect(this Ga21KVector0 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (mv2.Scalar12 * mv1.Scalar * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDualOnDirect(this Ga21KVector0 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (-mv2.Scalar123 * mv1.Scalar * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDualOnDirect(this Ga21KVector1 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (-mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar2 = (-2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar3 = (-2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar3 + mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDualOnDirect(this Ga21KVector1 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 + mv2.Scalar13 * mv1.Scalar1 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 + mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar13 + 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.Scalar1 * mv2.Scalar23 + mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar3 = (-2 * mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar23 - mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar3 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDualOnDirect(this Ga21KVector1 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (mv2.Scalar123 * mv1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar123 * mv1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar123 * mv1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDualOnDirect(this Ga21KVector2 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 + mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar23 = (-2 * mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDualOnDirect(this Ga21KVector2 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar12 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar13) * mv2NormSquaredInv,
            Scalar13 = (-mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23 + mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar12 * mv1.Scalar23 * mv2.Scalar12 + mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar23 + mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDualOnDirect(this Ga21KVector2 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (-mv2.Scalar123 * mv1.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar123 * mv1.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar123 * mv1.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDualOnDirect(this Ga21KVector3 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (-mv2.Scalar1 * mv1.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDualOnDirect(this Ga21KVector3 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (-mv2.Scalar12 * mv1.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDualOnDirect(this Ga21KVector3 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (-mv2.Scalar123 * mv1.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga21Multivector ReflectDualOnDirect(this Ga21Multivector mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar1 * mv1.KVector0.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[2] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[4] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar3 + mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 + mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[6] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar1 * mv1.KVector3.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    public static Ga21Multivector ReflectDualOnDirect(this Ga21Multivector mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar12 * mv1.KVector0.Scalar * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector0.Scalar * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 + mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 + mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar13 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar23 + mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[4] += (-2 * mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar23 - mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector1.Scalar3 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector2.Scalar12 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar13) * mv2NormSquaredInv;
            tempScalar[5] += (-mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23 + mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar12 * mv1.KVector2.Scalar23 * mv2.Scalar12 + mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar23 + mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar12 * mv1.KVector3.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector3.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    public static Ga21Multivector ReflectDualOnDirect(this Ga21Multivector mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar123 * mv1.KVector0.Scalar * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar123 * mv1.KVector1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar123 * mv1.KVector1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar123 * mv1.KVector1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar123 * mv1.KVector2.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar123 * mv1.KVector2.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar123 * mv1.KVector2.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar123 * mv1.KVector3.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDualOnDual(this Ga21KVector0 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDualOnDual(this Ga21KVector0 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (-mv2.Scalar12 * mv1.Scalar * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 ReflectDualOnDual(this Ga21KVector0 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector0
        {
            Scalar = (mv2.Scalar123 * mv1.Scalar * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDualOnDual(this Ga21KVector1 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (-mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar2 = (-2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar3 = (-2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar3 + mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDualOnDual(this Ga21KVector1 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 + mv2.Scalar13 * mv1.Scalar1 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 + mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar13 + 2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.Scalar1 * mv2.Scalar23 + mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar3 = (-2 * mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar23 - mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar3 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 ReflectDualOnDual(this Ga21KVector1 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector1
        {
            Scalar1 = (mv2.Scalar123 * mv1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar123 * mv1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar123 * mv1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDualOnDual(this Ga21KVector2 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (-mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 - mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar13 = (-mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv,
            Scalar23 = (2 * mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar13 * mv2.Scalar2 + mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDualOnDual(this Ga21KVector2 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (-mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar12 * mv2.Scalar13 + mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar13) * mv2NormSquaredInv,
            Scalar13 = (mv2.Scalar12 * mv1.Scalar13 * mv2.Scalar12 - mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar13 - 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23 - mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar12 * mv1.Scalar23 * mv2.Scalar12 - mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar13 * mv2.Scalar23 - mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 ReflectDualOnDual(this Ga21KVector2 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector2
        {
            Scalar12 = (mv2.Scalar123 * mv1.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar13 = (-mv2.Scalar123 * mv1.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar123 * mv1.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDualOnDual(this Ga21KVector3 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (-mv2.Scalar1 * mv1.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDualOnDual(this Ga21KVector3 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (-mv2.Scalar12 * mv1.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 ReflectDualOnDual(this Ga21KVector3 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga21KVector3
        {
            Scalar123 = (-mv2.Scalar123 * mv1.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv
        };
    }
    
    public static Ga21Multivector ReflectDualOnDual(this Ga21Multivector mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar1 * mv1.KVector0.Scalar * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[2] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 - mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[4] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar3 + mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar1 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 - mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[5] += (-mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3) * mv2NormSquaredInv;
            tempScalar[6] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar13 * mv2.Scalar2 + mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar1 * mv1.KVector3.Scalar123 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    public static Ga21Multivector ReflectDualOnDual(this Ga21Multivector mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar12 * mv1.KVector0.Scalar * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector0.Scalar * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 + mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 + mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar13 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar13 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar1 * mv2.Scalar23 + mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[4] += (-2 * mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar23 - mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector1.Scalar3 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar12 * mv2.Scalar13 + mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar13) * mv2NormSquaredInv;
            tempScalar[5] += (mv2.Scalar12 * mv1.KVector2.Scalar13 * mv2.Scalar12 - mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar13 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23 - mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar12 * mv1.KVector2.Scalar23 * mv2.Scalar12 - mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar13 * mv2.Scalar23 - mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar13) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar12 * mv1.KVector3.Scalar123 * mv2.Scalar12 + mv2.Scalar13 * mv1.KVector3.Scalar123 * mv2.Scalar13 - mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    public static Ga21Multivector ReflectDualOnDual(this Ga21Multivector mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar123 * mv1.KVector0.Scalar * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar123 * mv1.KVector1.Scalar1 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar123 * mv1.KVector1.Scalar2 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar123 * mv1.KVector1.Scalar3 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar123 * mv1.KVector2.Scalar12 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[5] += (-mv2.Scalar123 * mv1.KVector2.Scalar13 * mv2.Scalar123) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar123 * mv1.KVector2.Scalar23 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar123 * mv1.KVector3.Scalar123 * mv2.Scalar123) * mv2NormSquaredInv;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
}

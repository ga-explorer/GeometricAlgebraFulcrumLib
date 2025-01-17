using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga2;

public static class Ga2Reflection
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 ReflectDirectOnDirect(this Ga2KVector0 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector0
        {
            Scalar = (mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 ReflectDirectOnDirect(this Ga2KVector0 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector0
        {
            Scalar = (mv2.Scalar12 * mv1.Scalar * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 ReflectDirectOnDirect(this Ga2KVector1 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector1
        {
            Scalar1 = (mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2) * mv2NormSquaredInv,
            Scalar2 = (2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 ReflectDirectOnDirect(this Ga2KVector1 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector1
        {
            Scalar1 = (-mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 ReflectDirectOnDirect(this Ga2KVector2 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector2
        {
            Scalar12 = (-mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 ReflectDirectOnDirect(this Ga2KVector2 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector2
        {
            Scalar12 = (-mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    public static Ga2Multivector ReflectDirectOnDirect(this Ga2Multivector mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar1 * mv1.KVector0.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2) * mv2NormSquaredInv;
            tempScalar[2] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    public static Ga2Multivector ReflectDirectOnDirect(this Ga2Multivector mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar12 * mv1.KVector0.Scalar * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 ReflectDirectOnDual(this Ga2KVector0 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector0
        {
            Scalar = (mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 ReflectDirectOnDual(this Ga2KVector0 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector0
        {
            Scalar = (mv2.Scalar12 * mv1.Scalar * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 ReflectDirectOnDual(this Ga2KVector1 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector1
        {
            Scalar1 = (-mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2) * mv2NormSquaredInv,
            Scalar2 = (-2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 ReflectDirectOnDual(this Ga2KVector1 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector1
        {
            Scalar1 = (mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 ReflectDirectOnDual(this Ga2KVector2 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector2
        {
            Scalar12 = (-mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 ReflectDirectOnDual(this Ga2KVector2 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector2
        {
            Scalar12 = (-mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    public static Ga2Multivector ReflectDirectOnDual(this Ga2Multivector mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar1 * mv1.KVector0.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2) * mv2NormSquaredInv;
            tempScalar[2] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    public static Ga2Multivector ReflectDirectOnDual(this Ga2Multivector mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar12 * mv1.KVector0.Scalar * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 ReflectDualOnDirect(this Ga2KVector0 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector0
        {
            Scalar = (-mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 ReflectDualOnDirect(this Ga2KVector0 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector0
        {
            Scalar = (mv2.Scalar12 * mv1.Scalar * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 ReflectDualOnDirect(this Ga2KVector1 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector1
        {
            Scalar1 = (-mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2) * mv2NormSquaredInv,
            Scalar2 = (-2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 ReflectDualOnDirect(this Ga2KVector1 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector1
        {
            Scalar1 = (-mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 ReflectDualOnDirect(this Ga2KVector2 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector2
        {
            Scalar12 = (mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 ReflectDualOnDirect(this Ga2KVector2 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector2
        {
            Scalar12 = (-mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    public static Ga2Multivector ReflectDualOnDirect(this Ga2Multivector mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar1 * mv1.KVector0.Scalar * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 - 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2) * mv2NormSquaredInv;
            tempScalar[2] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 + mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    public static Ga2Multivector ReflectDualOnDirect(this Ga2Multivector mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar12 * mv1.KVector0.Scalar * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 ReflectDualOnDual(this Ga2KVector0 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector0
        {
            Scalar = (-mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 ReflectDualOnDual(this Ga2KVector0 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector0
        {
            Scalar = (mv2.Scalar12 * mv1.Scalar * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 ReflectDualOnDual(this Ga2KVector1 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector1
        {
            Scalar1 = (mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2) * mv2NormSquaredInv,
            Scalar2 = (2 * mv2.Scalar1 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 ReflectDualOnDual(this Ga2KVector1 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector1
        {
            Scalar1 = (mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 ReflectDualOnDual(this Ga2KVector2 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector2
        {
            Scalar12 = (mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 ReflectDualOnDual(this Ga2KVector2 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector2
        {
            Scalar12 = (-mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    public static Ga2Multivector ReflectDualOnDual(this Ga2Multivector mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar1 * mv1.KVector0.Scalar * mv2.Scalar1 - mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar1 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2) * mv2NormSquaredInv;
            tempScalar[2] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar1 * mv1.KVector2.Scalar12 * mv2.Scalar1 + mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2) * mv2NormSquaredInv;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    public static Ga2Multivector ReflectDualOnDual(this Ga2Multivector mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar12 * mv1.KVector0.Scalar * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar12 * mv1.KVector1.Scalar1 * mv2.Scalar12) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar12 * mv1.KVector1.Scalar2 * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar12 * mv1.KVector2.Scalar12 * mv2.Scalar12) * mv2NormSquaredInv;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
}

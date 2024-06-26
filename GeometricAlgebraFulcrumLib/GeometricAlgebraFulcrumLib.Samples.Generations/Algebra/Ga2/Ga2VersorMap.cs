using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga2;

public static class Ga2VersorMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 MapUsingVersor(this Ga2KVector0 mv1, Ga2KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector0
        {
            Scalar = (mv2.Scalar * mv1.Scalar * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 MapUsingVersor(this Ga2KVector0 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector0
        {
            Scalar = (mv2.Scalar1 * mv1.Scalar * mv2.Scalar1 + mv2.Scalar2 * mv1.Scalar * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 MapUsingVersor(this Ga2KVector0 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector0
        {
            Scalar = (mv2.Scalar12 * mv1.Scalar * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    public static Ga2KVector0 MapUsingEvenVersor(this Ga2KVector0 mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv2.KVector0.Scalar * mv1.Scalar * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar += mv2.KVector2.Scalar12 * mv1.Scalar * mv2.KVector2.Scalar12;
        }
        
        return new Ga2KVector0
        {
            Scalar = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    public static Ga2KVector0 MapUsingOddVersor(this Ga2KVector0 mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar += mv2.KVector1.Scalar1 * mv1.Scalar * mv2.KVector1.Scalar1 + mv2.KVector1.Scalar2 * mv1.Scalar * mv2.KVector1.Scalar2;
        }
        
        return new Ga2KVector0
        {
            Scalar = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 MapUsingVersor(this Ga2KVector1 mv1, Ga2KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector1
        {
            Scalar1 = (mv2.Scalar * mv1.Scalar1 * mv2.Scalar) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar * mv1.Scalar2 * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 MapUsingVersor(this Ga2KVector1 mv1, Ga2KVector1 mv2)
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
    public static Ga2KVector1 MapUsingVersor(this Ga2KVector1 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector1
        {
            Scalar1 = (mv2.Scalar12 * mv1.Scalar1 * mv2.Scalar12) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar12 * mv1.Scalar2 * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    public static Ga2KVector1 MapUsingEvenVersor(this Ga2KVector1 mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar0 += mv2.KVector0.Scalar * mv1.Scalar1 * mv2.KVector0.Scalar;
            tempScalar1 += mv2.KVector0.Scalar * mv1.Scalar2 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
        {
            tempScalar0 += 2 * mv2.KVector0.Scalar * mv1.Scalar2 * mv2.KVector2.Scalar12;
            tempScalar1 += -2 * mv2.KVector0.Scalar * mv1.Scalar1 * mv2.KVector2.Scalar12;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar0 += mv2.KVector2.Scalar12 * mv1.Scalar1 * mv2.KVector2.Scalar12;
            tempScalar1 += -mv2.KVector2.Scalar12 * mv1.Scalar2 * mv2.KVector2.Scalar12;
        }
        
        return new Ga2KVector1
        {
            Scalar1 = (tempScalar0) * mv2NormSquaredInv,
            Scalar2 = (tempScalar1) * mv2NormSquaredInv
        };
    }
    
    public static Ga2KVector1 MapUsingOddVersor(this Ga2KVector1 mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar0 = 0d;
        var tempScalar1 = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar0 += -mv2.KVector1.Scalar1 * mv1.Scalar1 * mv2.KVector1.Scalar1 - 2 * mv2.KVector1.Scalar1 * mv1.Scalar2 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar2 * mv1.Scalar1 * mv2.KVector1.Scalar2;
            tempScalar1 += -2 * mv2.KVector1.Scalar1 * mv1.Scalar1 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar1 * mv1.Scalar2 * mv2.KVector1.Scalar1 - mv2.KVector1.Scalar2 * mv1.Scalar2 * mv2.KVector1.Scalar2;
        }
        
        return new Ga2KVector1
        {
            Scalar1 = (tempScalar0) * mv2NormSquaredInv,
            Scalar2 = (tempScalar1) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 MapUsingVersor(this Ga2KVector2 mv1, Ga2KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector2
        {
            Scalar12 = (mv2.Scalar * mv1.Scalar12 * mv2.Scalar) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 MapUsingVersor(this Ga2KVector2 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector2
        {
            Scalar12 = (-mv2.Scalar1 * mv1.Scalar12 * mv2.Scalar1 - mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 MapUsingVersor(this Ga2KVector2 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga2KVector2
        {
            Scalar12 = (-mv2.Scalar12 * mv1.Scalar12 * mv2.Scalar12) * mv2NormSquaredInv
        };
    }
    
    public static Ga2KVector2 MapUsingEvenVersor(this Ga2KVector2 mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv2.KVector0.Scalar * mv1.Scalar12 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar += -mv2.KVector2.Scalar12 * mv1.Scalar12 * mv2.KVector2.Scalar12;
        }
        
        return new Ga2KVector2
        {
            Scalar12 = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    public static Ga2KVector2 MapUsingOddVersor(this Ga2KVector2 mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar += -mv2.KVector1.Scalar1 * mv1.Scalar12 * mv2.KVector1.Scalar1 - mv2.KVector1.Scalar2 * mv1.Scalar12 * mv2.KVector1.Scalar2;
        }
        
        return new Ga2KVector2
        {
            Scalar12 = (tempScalar) * mv2NormSquaredInv
        };
    }
    
    public static Ga2Multivector MapUsingVersor(this Ga2Multivector mv1, Ga2KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar * mv1.KVector0.Scalar * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (mv2.Scalar * mv1.KVector1.Scalar1 * mv2.Scalar) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar * mv1.KVector1.Scalar2 * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar * mv1.KVector2.Scalar12 * mv2.Scalar) * mv2NormSquaredInv;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    public static Ga2Multivector MapUsingVersor(this Ga2Multivector mv1, Ga2KVector1 mv2)
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
    
    public static Ga2Multivector MapUsingVersor(this Ga2Multivector mv1, Ga2KVector2 mv2)
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
    
    public static Ga2Multivector MapUsingEvenVersor(this Ga2Multivector mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[0] += (mv2.KVector0.Scalar * mv1.KVector0.Scalar * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[0] += (mv2.KVector2.Scalar12 * mv1.KVector0.Scalar * mv2.KVector2.Scalar12) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[1] += (mv2.KVector0.Scalar * mv1.KVector1.Scalar1 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
                tempScalar[2] += (mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector0.IsZero() && !mv2.KVector2.IsZero())
            {
                tempScalar[1] += (2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12) * mv2NormSquaredInv;
                tempScalar[2] += (-2 * mv2.KVector0.Scalar * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar12) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += (mv2.KVector2.Scalar12 * mv1.KVector1.Scalar1 * mv2.KVector2.Scalar12) * mv2NormSquaredInv;
                tempScalar[2] += (-mv2.KVector2.Scalar12 * mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[3] += (mv2.KVector0.Scalar * mv1.KVector2.Scalar12 * mv2.KVector0.Scalar) * mv2NormSquaredInv;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += (-mv2.KVector2.Scalar12 * mv1.KVector2.Scalar12 * mv2.KVector2.Scalar12) * mv2NormSquaredInv;
            }
            
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    public static Ga2Multivector MapUsingOddVersor(this Ga2Multivector mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[0] += (mv2.KVector1.Scalar1 * mv1.KVector0.Scalar * mv2.KVector1.Scalar1 + mv2.KVector1.Scalar2 * mv1.KVector0.Scalar * mv2.KVector1.Scalar2) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[1] += (-mv2.KVector1.Scalar1 * mv1.KVector1.Scalar1 * mv2.KVector1.Scalar1 - 2 * mv2.KVector1.Scalar1 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar2 * mv1.KVector1.Scalar1 * mv2.KVector1.Scalar2) * mv2NormSquaredInv;
                tempScalar[2] += (-2 * mv2.KVector1.Scalar1 * mv1.KVector1.Scalar1 * mv2.KVector1.Scalar2 + mv2.KVector1.Scalar1 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar1 - mv2.KVector1.Scalar2 * mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2) * mv2NormSquaredInv;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[3] += (-mv2.KVector1.Scalar1 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar1 - mv2.KVector1.Scalar2 * mv1.KVector2.Scalar12 * mv2.KVector1.Scalar2) * mv2NormSquaredInv;
            }
            
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
}

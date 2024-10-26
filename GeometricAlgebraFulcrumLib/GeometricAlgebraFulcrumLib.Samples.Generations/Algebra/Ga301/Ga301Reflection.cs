using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga301;

public static class Ga301Reflection
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDirectOnDirect(this Ga301KVector0 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDirectOnDirect(this Ga301KVector0 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar23 * mv1.Scalar * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDirectOnDirect(this Ga301KVector0 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar234 * mv1.Scalar * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDirectOnDirect(this Ga301KVector0 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 ReflectDirectOnDirect(this Ga301KVector1 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector1
        {
            Scalar1 = (2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar4 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar2 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar3 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar4 - mv2.Scalar4 * mv1.Scalar3 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar4 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar4 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 ReflectDirectOnDirect(this Ga301KVector1 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector1
        {
            Scalar1 = (-2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar4 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.Scalar2 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.Scalar3 * mv2.Scalar34 - mv2.Scalar24 * mv1.Scalar1 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar34 - mv2.Scalar24 * mv1.Scalar2 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar2 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar34 * mv1.Scalar3 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar24) * mv2NormSquaredInv,
            Scalar4 = (-mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar4 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar4 * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 ReflectDirectOnDirect(this Ga301KVector1 mv1, Ga301KVector3 mv2)
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
    public static Ga301KVector1 ReflectDirectOnDirect(this Ga301KVector1 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector1.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 ReflectDirectOnDirect(this Ga301KVector2 mv1, Ga301KVector1 mv2)
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
    public static Ga301KVector2 ReflectDirectOnDirect(this Ga301KVector2 mv1, Ga301KVector2 mv2)
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
    public static Ga301KVector2 ReflectDirectOnDirect(this Ga301KVector2 mv1, Ga301KVector3 mv2)
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
    public static Ga301KVector2 ReflectDirectOnDirect(this Ga301KVector2 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector2.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 ReflectDirectOnDirect(this Ga301KVector3 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector3
        {
            Scalar123 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar4 - mv2.Scalar4 * mv1.Scalar123 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar124 = (-2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar124 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar134 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar134 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar2 * mv1.Scalar234 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar234 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar234 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 ReflectDirectOnDirect(this Ga301KVector3 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector3
        {
            Scalar123 = (2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar123 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar124 = (-mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar23 + mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar24 - mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar24) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar23 + mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar34 - mv2.Scalar24 * mv1.Scalar234 * mv2.Scalar24) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 ReflectDirectOnDirect(this Ga301KVector3 mv1, Ga301KVector3 mv2)
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
    public static Ga301KVector3 ReflectDirectOnDirect(this Ga301KVector3 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector3.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDirectOnDirect(this Ga301KVector4 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (-mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDirectOnDirect(this Ga301KVector4 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (-mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar1234 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDirectOnDirect(this Ga301KVector4 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (-mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDirectOnDirect(this Ga301KVector4 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector4.Zero;
    }
    
    public static Ga301Multivector ReflectDirectOnDirect(this Ga301Multivector mv1, Ga301KVector1 mv2)
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
            tempScalar[1] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar4 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar1 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar2 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[4] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar4 - mv2.Scalar4 * mv1.KVector1.Scalar3 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[8] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector1.Scalar4 * mv2.Scalar4) * mv2NormSquaredInv;
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
            tempScalar[7] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar4 - mv2.Scalar4 * mv1.KVector3.Scalar123 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[11] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar124 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar134 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar134 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar2 * mv1.KVector3.Scalar234 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar234 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar234 * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    public static Ga301Multivector ReflectDirectOnDirect(this Ga301Multivector mv1, Ga301KVector2 mv2)
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
            tempScalar[1] += (-2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.KVector1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector1.Scalar4 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.KVector1.Scalar2 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.KVector1.Scalar3 * mv2.Scalar34 - mv2.Scalar24 * mv1.KVector1.Scalar1 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector1.Scalar1 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar34 - mv2.Scalar24 * mv1.KVector1.Scalar2 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector1.Scalar2 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar34 * mv1.KVector1.Scalar3 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar24) * mv2NormSquaredInv;
            tempScalar[8] += (mv2.Scalar24 * mv1.KVector1.Scalar4 * mv2.Scalar24 - mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar23 + mv2.Scalar34 * mv1.KVector1.Scalar4 * mv2.Scalar34) * mv2NormSquaredInv;
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
            tempScalar[7] += (-mv2.Scalar34 * mv1.KVector3.Scalar123 * mv2.Scalar34 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar24) * mv2NormSquaredInv;
            tempScalar[11] += (-mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar23 + mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar24 - mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar24) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar23 + mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar34 - mv2.Scalar24 * mv1.KVector3.Scalar234 * mv2.Scalar24) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector4.Scalar1234 * mv2.Scalar24) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    public static Ga301Multivector ReflectDirectOnDirect(this Ga301Multivector mv1, Ga301KVector3 mv2)
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
    public static Ga301Multivector ReflectDirectOnDirect(this Ga301Multivector mv1, Ga301KVector4 mv2)
    {
        return Ga301Multivector.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDirectOnDual(this Ga301KVector0 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDirectOnDual(this Ga301KVector0 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar23 * mv1.Scalar * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDirectOnDual(this Ga301KVector0 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar234 * mv1.Scalar * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDirectOnDual(this Ga301KVector0 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 ReflectDirectOnDual(this Ga301KVector1 mv1, Ga301KVector1 mv2)
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
    public static Ga301KVector1 ReflectDirectOnDual(this Ga301KVector1 mv1, Ga301KVector2 mv2)
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
    public static Ga301KVector1 ReflectDirectOnDual(this Ga301KVector1 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector1
        {
            Scalar1 = (-2 * mv2.Scalar123 * mv1.Scalar4 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar2 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar1 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar234 * mv1.Scalar2 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar234 * mv1.Scalar3 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar4 = (-mv2.Scalar234 * mv1.Scalar4 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 ReflectDirectOnDual(this Ga301KVector1 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector1.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 ReflectDirectOnDual(this Ga301KVector2 mv1, Ga301KVector1 mv2)
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
    public static Ga301KVector2 ReflectDirectOnDual(this Ga301KVector2 mv1, Ga301KVector2 mv2)
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
    public static Ga301KVector2 ReflectDirectOnDual(this Ga301KVector2 mv1, Ga301KVector3 mv2)
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
    public static Ga301KVector2 ReflectDirectOnDual(this Ga301KVector2 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector2.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 ReflectDirectOnDual(this Ga301KVector3 mv1, Ga301KVector1 mv2)
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
    public static Ga301KVector3 ReflectDirectOnDual(this Ga301KVector3 mv1, Ga301KVector2 mv2)
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
    public static Ga301KVector3 ReflectDirectOnDual(this Ga301KVector3 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector3
        {
            Scalar123 = (2 * mv2.Scalar123 * mv1.Scalar234 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar123 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar124 = (-mv2.Scalar234 * mv1.Scalar124 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar134 * mv1.Scalar234 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar134 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 ReflectDirectOnDual(this Ga301KVector3 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector3.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDirectOnDual(this Ga301KVector4 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (-mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDirectOnDual(this Ga301KVector4 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (-mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar1234 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDirectOnDual(this Ga301KVector4 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (-mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDirectOnDual(this Ga301KVector4 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector4.Zero;
    }
    
    public static Ga301Multivector ReflectDirectOnDual(this Ga301Multivector mv1, Ga301KVector1 mv2)
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
    
    public static Ga301Multivector ReflectDirectOnDual(this Ga301Multivector mv1, Ga301KVector2 mv2)
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
    
    public static Ga301Multivector ReflectDirectOnDual(this Ga301Multivector mv1, Ga301KVector3 mv2)
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
            tempScalar[1] += (-2 * mv2.Scalar123 * mv1.KVector1.Scalar4 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.KVector1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector1.Scalar2 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector1.Scalar1 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar234 * mv1.KVector1.Scalar2 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar234 * mv1.KVector1.Scalar3 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[8] += (-mv2.Scalar234 * mv1.KVector1.Scalar4 * mv2.Scalar234) * mv2NormSquaredInv;
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
            tempScalar[7] += (2 * mv2.Scalar123 * mv1.KVector3.Scalar234 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector3.Scalar123 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[11] += (-mv2.Scalar234 * mv1.KVector3.Scalar124 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar134 * mv1.KVector3.Scalar234 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector3.Scalar134 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector ReflectDirectOnDual(this Ga301Multivector mv1, Ga301KVector4 mv2)
    {
        return Ga301Multivector.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDualOnDirect(this Ga301KVector0 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (-mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDualOnDirect(this Ga301KVector0 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar23 * mv1.Scalar * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDualOnDirect(this Ga301KVector0 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (-mv2.Scalar234 * mv1.Scalar * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDualOnDirect(this Ga301KVector0 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 ReflectDualOnDirect(this Ga301KVector1 mv1, Ga301KVector1 mv2)
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
    public static Ga301KVector1 ReflectDualOnDirect(this Ga301KVector1 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector1
        {
            Scalar1 = (-2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar4 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.Scalar2 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.Scalar3 * mv2.Scalar34 - mv2.Scalar24 * mv1.Scalar1 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar34 - mv2.Scalar24 * mv1.Scalar2 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar2 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar34 * mv1.Scalar3 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar24) * mv2NormSquaredInv,
            Scalar4 = (-mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar4 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar4 * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 ReflectDualOnDirect(this Ga301KVector1 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector1
        {
            Scalar1 = (-2 * mv2.Scalar123 * mv1.Scalar4 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar2 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar1 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar234 * mv1.Scalar2 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar234 * mv1.Scalar3 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar4 = (-mv2.Scalar234 * mv1.Scalar4 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 ReflectDualOnDirect(this Ga301KVector1 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector1.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 ReflectDualOnDirect(this Ga301KVector2 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector2
        {
            Scalar12 = (-2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar12 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar4 - mv2.Scalar4 * mv1.Scalar13 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar4 - mv2.Scalar4 * mv1.Scalar23 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar14 = (2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar14 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar24 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar34 = (-2 * mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar34 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar34 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 ReflectDualOnDirect(this Ga301KVector2 mv1, Ga301KVector2 mv2)
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
    public static Ga301KVector2 ReflectDualOnDirect(this Ga301KVector2 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector2
        {
            Scalar12 = (-mv2.Scalar234 * mv1.Scalar12 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar134 * mv1.Scalar23 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar13 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar234 * mv1.Scalar23 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar14 = (-2 * mv2.Scalar124 * mv1.Scalar34 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar24 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar14 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar234 * mv1.Scalar24 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar34 = (mv2.Scalar234 * mv1.Scalar34 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 ReflectDualOnDirect(this Ga301KVector2 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector2.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 ReflectDualOnDirect(this Ga301KVector3 mv1, Ga301KVector1 mv2)
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
    public static Ga301KVector3 ReflectDualOnDirect(this Ga301KVector3 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector3
        {
            Scalar123 = (2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar123 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar124 = (-mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar23 + mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar24 - mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar24) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar23 + mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar34 - mv2.Scalar24 * mv1.Scalar234 * mv2.Scalar24) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 ReflectDualOnDirect(this Ga301KVector3 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector3
        {
            Scalar123 = (2 * mv2.Scalar123 * mv1.Scalar234 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar123 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar124 = (-mv2.Scalar234 * mv1.Scalar124 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar134 * mv1.Scalar234 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar134 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 ReflectDualOnDirect(this Ga301KVector3 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector3.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDualOnDirect(this Ga301KVector4 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDualOnDirect(this Ga301KVector4 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (-mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar1234 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDualOnDirect(this Ga301KVector4 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDualOnDirect(this Ga301KVector4 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector4.Zero;
    }
    
    public static Ga301Multivector ReflectDualOnDirect(this Ga301Multivector mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector0.Scalar * mv2.Scalar4) * mv2NormSquaredInv;
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
            tempScalar[3] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar12 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar4 - mv2.Scalar4 * mv1.KVector2.Scalar13 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar4 - mv2.Scalar4 * mv1.KVector2.Scalar23 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar14 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar24 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[12] += (-2 * mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector2.Scalar34 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar34 * mv2.Scalar4) * mv2NormSquaredInv;
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
            tempScalar[15] += (mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    public static Ga301Multivector ReflectDualOnDirect(this Ga301Multivector mv1, Ga301KVector2 mv2)
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
            tempScalar[1] += (-2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.KVector1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector1.Scalar4 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.KVector1.Scalar2 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.KVector1.Scalar3 * mv2.Scalar34 - mv2.Scalar24 * mv1.KVector1.Scalar1 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector1.Scalar1 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar34 - mv2.Scalar24 * mv1.KVector1.Scalar2 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector1.Scalar2 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar34 * mv1.KVector1.Scalar3 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar24) * mv2NormSquaredInv;
            tempScalar[8] += (mv2.Scalar24 * mv1.KVector1.Scalar4 * mv2.Scalar24 - mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar23 + mv2.Scalar34 * mv1.KVector1.Scalar4 * mv2.Scalar34) * mv2NormSquaredInv;
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
            tempScalar[7] += (-mv2.Scalar34 * mv1.KVector3.Scalar123 * mv2.Scalar34 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar24) * mv2NormSquaredInv;
            tempScalar[11] += (-mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar23 + mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar24 - mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar24) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar23 + mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar34 - mv2.Scalar24 * mv1.KVector3.Scalar234 * mv2.Scalar24) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector4.Scalar1234 * mv2.Scalar24) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    public static Ga301Multivector ReflectDualOnDirect(this Ga301Multivector mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar234 * mv1.KVector0.Scalar * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-2 * mv2.Scalar123 * mv1.KVector1.Scalar4 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.KVector1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector1.Scalar2 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector1.Scalar1 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar234 * mv1.KVector1.Scalar2 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar234 * mv1.KVector1.Scalar3 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[8] += (-mv2.Scalar234 * mv1.KVector1.Scalar4 * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar234 * mv1.KVector2.Scalar12 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar134 * mv1.KVector2.Scalar23 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector2.Scalar13 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar234 * mv1.KVector2.Scalar23 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[9] += (-2 * mv2.Scalar124 * mv1.KVector2.Scalar34 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar24 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector2.Scalar14 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar234 * mv1.KVector2.Scalar24 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[12] += (mv2.Scalar234 * mv1.KVector2.Scalar34 * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (2 * mv2.Scalar123 * mv1.KVector3.Scalar234 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector3.Scalar123 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[11] += (-mv2.Scalar234 * mv1.KVector3.Scalar124 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar134 * mv1.KVector3.Scalar234 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector3.Scalar134 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector ReflectDualOnDirect(this Ga301Multivector mv1, Ga301KVector4 mv2)
    {
        return Ga301Multivector.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDualOnDual(this Ga301KVector0 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (-mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDualOnDual(this Ga301KVector0 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (mv2.Scalar23 * mv1.Scalar * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDualOnDual(this Ga301KVector0 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector0
        {
            Scalar = (-mv2.Scalar234 * mv1.Scalar * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector0 ReflectDualOnDual(this Ga301KVector0 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 ReflectDualOnDual(this Ga301KVector1 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector1
        {
            Scalar1 = (2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar4 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar2 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar3 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar4 - mv2.Scalar4 * mv1.Scalar3 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar4 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar4 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector1 ReflectDualOnDual(this Ga301KVector1 mv1, Ga301KVector2 mv2)
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
    public static Ga301KVector1 ReflectDualOnDual(this Ga301KVector1 mv1, Ga301KVector3 mv2)
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
    public static Ga301KVector1 ReflectDualOnDual(this Ga301KVector1 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector1.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 ReflectDualOnDual(this Ga301KVector2 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector2
        {
            Scalar12 = (-2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar12 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar4 - mv2.Scalar4 * mv1.Scalar13 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar4 - mv2.Scalar4 * mv1.Scalar23 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar14 = (2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar14 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar24 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar34 = (-2 * mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar34 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar34 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 ReflectDualOnDual(this Ga301KVector2 mv1, Ga301KVector2 mv2)
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
    public static Ga301KVector2 ReflectDualOnDual(this Ga301KVector2 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector2
        {
            Scalar12 = (-mv2.Scalar234 * mv1.Scalar12 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar134 * mv1.Scalar23 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar13 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar234 * mv1.Scalar23 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar14 = (-2 * mv2.Scalar124 * mv1.Scalar34 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar24 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar14 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar234 * mv1.Scalar24 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar34 = (mv2.Scalar234 * mv1.Scalar34 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector2 ReflectDualOnDual(this Ga301KVector2 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector2.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 ReflectDualOnDual(this Ga301KVector3 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector3
        {
            Scalar123 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar4 - mv2.Scalar4 * mv1.Scalar123 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar124 = (-2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar124 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar134 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar134 * mv2.Scalar4) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar2 * mv1.Scalar234 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar234 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar234 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector3 ReflectDualOnDual(this Ga301KVector3 mv1, Ga301KVector2 mv2)
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
    public static Ga301KVector3 ReflectDualOnDual(this Ga301KVector3 mv1, Ga301KVector3 mv2)
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
    public static Ga301KVector3 ReflectDualOnDual(this Ga301KVector3 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector3.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDualOnDual(this Ga301KVector4 mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar4) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDualOnDual(this Ga301KVector4 mv1, Ga301KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (-mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar1234 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar34) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDualOnDual(this Ga301KVector4 mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga301KVector4
        {
            Scalar1234 = (mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar234) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301KVector4 ReflectDualOnDual(this Ga301KVector4 mv1, Ga301KVector4 mv2)
    {
        return Ga301KVector4.Zero;
    }
    
    public static Ga301Multivector ReflectDualOnDual(this Ga301Multivector mv1, Ga301KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector0.Scalar * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar4 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar1 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar2 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[4] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar4 - mv2.Scalar4 * mv1.KVector1.Scalar3 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[8] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector1.Scalar4 * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar12 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar4 - mv2.Scalar4 * mv1.KVector2.Scalar13 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar4 - mv2.Scalar4 * mv1.KVector2.Scalar23 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar14 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar24 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[12] += (-2 * mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector2.Scalar34 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar34 * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar4 - mv2.Scalar4 * mv1.KVector3.Scalar123 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[11] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar124 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar134 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar134 * mv2.Scalar4) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar2 * mv1.KVector3.Scalar234 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar234 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar234 * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar4) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    public static Ga301Multivector ReflectDualOnDual(this Ga301Multivector mv1, Ga301KVector2 mv2)
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
    
    public static Ga301Multivector ReflectDualOnDual(this Ga301Multivector mv1, Ga301KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga301Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar234 * mv1.KVector0.Scalar * mv2.Scalar234) * mv2NormSquaredInv;
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
            tempScalar[3] += (-mv2.Scalar234 * mv1.KVector2.Scalar12 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar134 * mv1.KVector2.Scalar23 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector2.Scalar13 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar234 * mv1.KVector2.Scalar23 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[9] += (-2 * mv2.Scalar124 * mv1.KVector2.Scalar34 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar24 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector2.Scalar14 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar234 * mv1.KVector2.Scalar24 * mv2.Scalar234) * mv2NormSquaredInv;
            tempScalar[12] += (mv2.Scalar234 * mv1.KVector2.Scalar34 * mv2.Scalar234) * mv2NormSquaredInv;
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
            tempScalar[15] += (mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar234) * mv2NormSquaredInv;
        }
        
        return Ga301Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga301Multivector ReflectDualOnDual(this Ga301Multivector mv1, Ga301KVector4 mv2)
    {
        return Ga301Multivector.Zero;
    }
    
}

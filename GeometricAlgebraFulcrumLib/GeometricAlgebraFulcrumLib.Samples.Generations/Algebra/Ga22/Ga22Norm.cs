using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga22;

public static class Ga22Norm
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga22KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga22KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar1 * mv.Scalar1 - mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3 + mv.Scalar4 * mv.Scalar4;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga22KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar12 * mv.Scalar12 + mv.Scalar13 * mv.Scalar13 + mv.Scalar23 * mv.Scalar23 + mv.Scalar14 * mv.Scalar14 + mv.Scalar24 * mv.Scalar24 - mv.Scalar34 * mv.Scalar34;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga22KVector3 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar123 * mv.Scalar123 - mv.Scalar124 * mv.Scalar124 + mv.Scalar134 * mv.Scalar134 + mv.Scalar234 * mv.Scalar234;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga22KVector4 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar1234 * mv.Scalar1234;
    }
    
    public static double SpSquared(this Ga22Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += -mv.KVector1.Scalar1 * mv.KVector1.Scalar1 - mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3 + mv.KVector1.Scalar4 * mv.KVector1.Scalar4;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += -mv.KVector2.Scalar12 * mv.KVector2.Scalar12 + mv.KVector2.Scalar13 * mv.KVector2.Scalar13 + mv.KVector2.Scalar23 * mv.KVector2.Scalar23 + mv.KVector2.Scalar14 * mv.KVector2.Scalar14 + mv.KVector2.Scalar24 * mv.KVector2.Scalar24 - mv.KVector2.Scalar34 * mv.KVector2.Scalar34;
        }
        
        if (!mv.KVector3.IsZero())
        {
            normSquared += -mv.KVector3.Scalar123 * mv.KVector3.Scalar123 - mv.KVector3.Scalar124 * mv.KVector3.Scalar124 + mv.KVector3.Scalar134 * mv.KVector3.Scalar134 + mv.KVector3.Scalar234 * mv.KVector3.Scalar234;
        }
        
        if (!mv.KVector4.IsZero())
        {
            normSquared += mv.KVector4.Scalar1234 * mv.KVector4.Scalar1234;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga22KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga22KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar1 * mv.Scalar1 - mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3 + mv.Scalar4 * mv.Scalar4;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga22KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar12 * mv.Scalar12 - mv.Scalar13 * mv.Scalar13 - mv.Scalar23 * mv.Scalar23 - mv.Scalar14 * mv.Scalar14 - mv.Scalar24 * mv.Scalar24 + mv.Scalar34 * mv.Scalar34;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga22KVector3 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar123 * mv.Scalar123 + mv.Scalar124 * mv.Scalar124 - mv.Scalar134 * mv.Scalar134 - mv.Scalar234 * mv.Scalar234;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga22KVector4 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar1234 * mv.Scalar1234;
    }
    
    public static double NormSquared(this Ga22Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += -mv.KVector1.Scalar1 * mv.KVector1.Scalar1 - mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3 + mv.KVector1.Scalar4 * mv.KVector1.Scalar4;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += mv.KVector2.Scalar12 * mv.KVector2.Scalar12 - mv.KVector2.Scalar13 * mv.KVector2.Scalar13 - mv.KVector2.Scalar23 * mv.KVector2.Scalar23 - mv.KVector2.Scalar14 * mv.KVector2.Scalar14 - mv.KVector2.Scalar24 * mv.KVector2.Scalar24 + mv.KVector2.Scalar34 * mv.KVector2.Scalar34;
        }
        
        if (!mv.KVector3.IsZero())
        {
            normSquared += mv.KVector3.Scalar123 * mv.KVector3.Scalar123 + mv.KVector3.Scalar124 * mv.KVector3.Scalar124 - mv.KVector3.Scalar134 * mv.KVector3.Scalar134 - mv.KVector3.Scalar234 * mv.KVector3.Scalar234;
        }
        
        if (!mv.KVector4.IsZero())
        {
            normSquared += mv.KVector4.Scalar1234 * mv.KVector4.Scalar1234;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga22KVector0 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga22KVector1 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga22KVector2 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga22KVector3 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga22KVector4 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga22Multivector mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
}

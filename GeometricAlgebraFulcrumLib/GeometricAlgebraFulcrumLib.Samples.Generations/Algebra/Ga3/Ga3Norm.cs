using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga3;

public static class Ga3Norm
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga3KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga3KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar1 * mv.Scalar1 + mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga3KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar12 * mv.Scalar12 - mv.Scalar13 * mv.Scalar13 - mv.Scalar23 * mv.Scalar23;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga3KVector3 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar123 * mv.Scalar123;
    }
    
    public static double SpSquared(this Ga3Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += mv.KVector1.Scalar1 * mv.KVector1.Scalar1 + mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += -mv.KVector2.Scalar12 * mv.KVector2.Scalar12 - mv.KVector2.Scalar13 * mv.KVector2.Scalar13 - mv.KVector2.Scalar23 * mv.KVector2.Scalar23;
        }
        
        if (!mv.KVector3.IsZero())
        {
            normSquared += -mv.KVector3.Scalar123 * mv.KVector3.Scalar123;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga3KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga3KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar1 * mv.Scalar1 + mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga3KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar12 * mv.Scalar12 + mv.Scalar13 * mv.Scalar13 + mv.Scalar23 * mv.Scalar23;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga3KVector3 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar123 * mv.Scalar123;
    }
    
    public static double NormSquared(this Ga3Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += mv.KVector1.Scalar1 * mv.KVector1.Scalar1 + mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += mv.KVector2.Scalar12 * mv.KVector2.Scalar12 + mv.KVector2.Scalar13 * mv.KVector2.Scalar13 + mv.KVector2.Scalar23 * mv.KVector2.Scalar23;
        }
        
        if (!mv.KVector3.IsZero())
        {
            normSquared += mv.KVector3.Scalar123 * mv.KVector3.Scalar123;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga3KVector0 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga3KVector1 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga3KVector2 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga3KVector3 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga3Multivector mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
}

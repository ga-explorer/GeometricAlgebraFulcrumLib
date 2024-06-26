using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga201;

public static class Ga201Norm
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga201KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga201KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga201KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar23 * mv.Scalar23;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga201KVector3 mv)
    {
        return 0d;
    }
    
    public static double SpSquared(this Ga201Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += -mv.KVector2.Scalar23 * mv.KVector2.Scalar23;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga201KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga201KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga201KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar23 * mv.Scalar23;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga201KVector3 mv)
    {
        return 0d;
    }
    
    public static double NormSquared(this Ga201Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += mv.KVector2.Scalar23 * mv.KVector2.Scalar23;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga201KVector0 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga201KVector1 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga201KVector2 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga201KVector3 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga201Multivector mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
}

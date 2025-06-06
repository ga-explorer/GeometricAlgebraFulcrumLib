using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga501;

public static class Ga501Norm
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga501KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga501KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3 + mv.Scalar4 * mv.Scalar4 + mv.Scalar5 * mv.Scalar5 + mv.Scalar6 * mv.Scalar6;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga501KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar23 * mv.Scalar23 - mv.Scalar24 * mv.Scalar24 - mv.Scalar34 * mv.Scalar34 - mv.Scalar25 * mv.Scalar25 - mv.Scalar35 * mv.Scalar35 - mv.Scalar45 * mv.Scalar45 - mv.Scalar26 * mv.Scalar26 - mv.Scalar36 * mv.Scalar36 - mv.Scalar46 * mv.Scalar46 - mv.Scalar56 * mv.Scalar56;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga501KVector3 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar234 * mv.Scalar234 - mv.Scalar235 * mv.Scalar235 - mv.Scalar245 * mv.Scalar245 - mv.Scalar345 * mv.Scalar345 - mv.Scalar236 * mv.Scalar236 - mv.Scalar246 * mv.Scalar246 - mv.Scalar346 * mv.Scalar346 - mv.Scalar256 * mv.Scalar256 - mv.Scalar356 * mv.Scalar356 - mv.Scalar456 * mv.Scalar456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga501KVector4 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar2345 * mv.Scalar2345 + mv.Scalar2346 * mv.Scalar2346 + mv.Scalar2356 * mv.Scalar2356 + mv.Scalar2456 * mv.Scalar2456 + mv.Scalar3456 * mv.Scalar3456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga501KVector5 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar23456 * mv.Scalar23456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga501KVector6 mv)
    {
        return 0d;
    }
    
    public static double SpSquared(this Ga501Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3 + mv.KVector1.Scalar4 * mv.KVector1.Scalar4 + mv.KVector1.Scalar5 * mv.KVector1.Scalar5 + mv.KVector1.Scalar6 * mv.KVector1.Scalar6;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += -mv.KVector2.Scalar23 * mv.KVector2.Scalar23 - mv.KVector2.Scalar24 * mv.KVector2.Scalar24 - mv.KVector2.Scalar34 * mv.KVector2.Scalar34 - mv.KVector2.Scalar25 * mv.KVector2.Scalar25 - mv.KVector2.Scalar35 * mv.KVector2.Scalar35 - mv.KVector2.Scalar45 * mv.KVector2.Scalar45 - mv.KVector2.Scalar26 * mv.KVector2.Scalar26 - mv.KVector2.Scalar36 * mv.KVector2.Scalar36 - mv.KVector2.Scalar46 * mv.KVector2.Scalar46 - mv.KVector2.Scalar56 * mv.KVector2.Scalar56;
        }
        
        if (!mv.KVector3.IsZero())
        {
            normSquared += -mv.KVector3.Scalar234 * mv.KVector3.Scalar234 - mv.KVector3.Scalar235 * mv.KVector3.Scalar235 - mv.KVector3.Scalar245 * mv.KVector3.Scalar245 - mv.KVector3.Scalar345 * mv.KVector3.Scalar345 - mv.KVector3.Scalar236 * mv.KVector3.Scalar236 - mv.KVector3.Scalar246 * mv.KVector3.Scalar246 - mv.KVector3.Scalar346 * mv.KVector3.Scalar346 - mv.KVector3.Scalar256 * mv.KVector3.Scalar256 - mv.KVector3.Scalar356 * mv.KVector3.Scalar356 - mv.KVector3.Scalar456 * mv.KVector3.Scalar456;
        }
        
        if (!mv.KVector4.IsZero())
        {
            normSquared += mv.KVector4.Scalar2345 * mv.KVector4.Scalar2345 + mv.KVector4.Scalar2346 * mv.KVector4.Scalar2346 + mv.KVector4.Scalar2356 * mv.KVector4.Scalar2356 + mv.KVector4.Scalar2456 * mv.KVector4.Scalar2456 + mv.KVector4.Scalar3456 * mv.KVector4.Scalar3456;
        }
        
        if (!mv.KVector5.IsZero())
        {
            normSquared += mv.KVector5.Scalar23456 * mv.KVector5.Scalar23456;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga501KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga501KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3 + mv.Scalar4 * mv.Scalar4 + mv.Scalar5 * mv.Scalar5 + mv.Scalar6 * mv.Scalar6;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga501KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar23 * mv.Scalar23 + mv.Scalar24 * mv.Scalar24 + mv.Scalar34 * mv.Scalar34 + mv.Scalar25 * mv.Scalar25 + mv.Scalar35 * mv.Scalar35 + mv.Scalar45 * mv.Scalar45 + mv.Scalar26 * mv.Scalar26 + mv.Scalar36 * mv.Scalar36 + mv.Scalar46 * mv.Scalar46 + mv.Scalar56 * mv.Scalar56;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga501KVector3 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar234 * mv.Scalar234 + mv.Scalar235 * mv.Scalar235 + mv.Scalar245 * mv.Scalar245 + mv.Scalar345 * mv.Scalar345 + mv.Scalar236 * mv.Scalar236 + mv.Scalar246 * mv.Scalar246 + mv.Scalar346 * mv.Scalar346 + mv.Scalar256 * mv.Scalar256 + mv.Scalar356 * mv.Scalar356 + mv.Scalar456 * mv.Scalar456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga501KVector4 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar2345 * mv.Scalar2345 + mv.Scalar2346 * mv.Scalar2346 + mv.Scalar2356 * mv.Scalar2356 + mv.Scalar2456 * mv.Scalar2456 + mv.Scalar3456 * mv.Scalar3456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga501KVector5 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar23456 * mv.Scalar23456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga501KVector6 mv)
    {
        return 0d;
    }
    
    public static double NormSquared(this Ga501Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3 + mv.KVector1.Scalar4 * mv.KVector1.Scalar4 + mv.KVector1.Scalar5 * mv.KVector1.Scalar5 + mv.KVector1.Scalar6 * mv.KVector1.Scalar6;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += mv.KVector2.Scalar23 * mv.KVector2.Scalar23 + mv.KVector2.Scalar24 * mv.KVector2.Scalar24 + mv.KVector2.Scalar34 * mv.KVector2.Scalar34 + mv.KVector2.Scalar25 * mv.KVector2.Scalar25 + mv.KVector2.Scalar35 * mv.KVector2.Scalar35 + mv.KVector2.Scalar45 * mv.KVector2.Scalar45 + mv.KVector2.Scalar26 * mv.KVector2.Scalar26 + mv.KVector2.Scalar36 * mv.KVector2.Scalar36 + mv.KVector2.Scalar46 * mv.KVector2.Scalar46 + mv.KVector2.Scalar56 * mv.KVector2.Scalar56;
        }
        
        if (!mv.KVector3.IsZero())
        {
            normSquared += mv.KVector3.Scalar234 * mv.KVector3.Scalar234 + mv.KVector3.Scalar235 * mv.KVector3.Scalar235 + mv.KVector3.Scalar245 * mv.KVector3.Scalar245 + mv.KVector3.Scalar345 * mv.KVector3.Scalar345 + mv.KVector3.Scalar236 * mv.KVector3.Scalar236 + mv.KVector3.Scalar246 * mv.KVector3.Scalar246 + mv.KVector3.Scalar346 * mv.KVector3.Scalar346 + mv.KVector3.Scalar256 * mv.KVector3.Scalar256 + mv.KVector3.Scalar356 * mv.KVector3.Scalar356 + mv.KVector3.Scalar456 * mv.KVector3.Scalar456;
        }
        
        if (!mv.KVector4.IsZero())
        {
            normSquared += mv.KVector4.Scalar2345 * mv.KVector4.Scalar2345 + mv.KVector4.Scalar2346 * mv.KVector4.Scalar2346 + mv.KVector4.Scalar2356 * mv.KVector4.Scalar2356 + mv.KVector4.Scalar2456 * mv.KVector4.Scalar2456 + mv.KVector4.Scalar3456 * mv.KVector4.Scalar3456;
        }
        
        if (!mv.KVector5.IsZero())
        {
            normSquared += mv.KVector5.Scalar23456 * mv.KVector5.Scalar23456;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga501KVector0 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga501KVector1 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga501KVector2 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga501KVector3 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga501KVector4 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga501KVector5 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga501KVector6 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga501Multivector mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
}

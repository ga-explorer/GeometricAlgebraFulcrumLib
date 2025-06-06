using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga51;

public static class Ga51Norm
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga51KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga51KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar1 * mv.Scalar1 + mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3 + mv.Scalar4 * mv.Scalar4 + mv.Scalar5 * mv.Scalar5 + mv.Scalar6 * mv.Scalar6;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga51KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar12 * mv.Scalar12 + mv.Scalar13 * mv.Scalar13 - mv.Scalar23 * mv.Scalar23 + mv.Scalar14 * mv.Scalar14 - mv.Scalar24 * mv.Scalar24 - mv.Scalar34 * mv.Scalar34 + mv.Scalar15 * mv.Scalar15 - mv.Scalar25 * mv.Scalar25 - mv.Scalar35 * mv.Scalar35 - mv.Scalar45 * mv.Scalar45 + mv.Scalar16 * mv.Scalar16 - mv.Scalar26 * mv.Scalar26 - mv.Scalar36 * mv.Scalar36 - mv.Scalar46 * mv.Scalar46 - mv.Scalar56 * mv.Scalar56;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga51KVector3 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar123 * mv.Scalar123 + mv.Scalar124 * mv.Scalar124 + mv.Scalar134 * mv.Scalar134 - mv.Scalar234 * mv.Scalar234 + mv.Scalar125 * mv.Scalar125 + mv.Scalar135 * mv.Scalar135 - mv.Scalar235 * mv.Scalar235 + mv.Scalar145 * mv.Scalar145 - mv.Scalar245 * mv.Scalar245 - mv.Scalar345 * mv.Scalar345 + mv.Scalar126 * mv.Scalar126 + mv.Scalar136 * mv.Scalar136 - mv.Scalar236 * mv.Scalar236 + mv.Scalar146 * mv.Scalar146 - mv.Scalar246 * mv.Scalar246 - mv.Scalar346 * mv.Scalar346 + mv.Scalar156 * mv.Scalar156 - mv.Scalar256 * mv.Scalar256 - mv.Scalar356 * mv.Scalar356 - mv.Scalar456 * mv.Scalar456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga51KVector4 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar1234 * mv.Scalar1234 - mv.Scalar1235 * mv.Scalar1235 - mv.Scalar1245 * mv.Scalar1245 - mv.Scalar1345 * mv.Scalar1345 + mv.Scalar2345 * mv.Scalar2345 - mv.Scalar1236 * mv.Scalar1236 - mv.Scalar1246 * mv.Scalar1246 - mv.Scalar1346 * mv.Scalar1346 + mv.Scalar2346 * mv.Scalar2346 - mv.Scalar1256 * mv.Scalar1256 - mv.Scalar1356 * mv.Scalar1356 + mv.Scalar2356 * mv.Scalar2356 - mv.Scalar1456 * mv.Scalar1456 + mv.Scalar2456 * mv.Scalar2456 + mv.Scalar3456 * mv.Scalar3456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga51KVector5 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar12345 * mv.Scalar12345 - mv.Scalar12346 * mv.Scalar12346 - mv.Scalar12356 * mv.Scalar12356 - mv.Scalar12456 * mv.Scalar12456 - mv.Scalar13456 * mv.Scalar13456 + mv.Scalar23456 * mv.Scalar23456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga51KVector6 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar123456 * mv.Scalar123456;
    }
    
    public static double SpSquared(this Ga51Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += -mv.KVector1.Scalar1 * mv.KVector1.Scalar1 + mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3 + mv.KVector1.Scalar4 * mv.KVector1.Scalar4 + mv.KVector1.Scalar5 * mv.KVector1.Scalar5 + mv.KVector1.Scalar6 * mv.KVector1.Scalar6;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += mv.KVector2.Scalar12 * mv.KVector2.Scalar12 + mv.KVector2.Scalar13 * mv.KVector2.Scalar13 - mv.KVector2.Scalar23 * mv.KVector2.Scalar23 + mv.KVector2.Scalar14 * mv.KVector2.Scalar14 - mv.KVector2.Scalar24 * mv.KVector2.Scalar24 - mv.KVector2.Scalar34 * mv.KVector2.Scalar34 + mv.KVector2.Scalar15 * mv.KVector2.Scalar15 - mv.KVector2.Scalar25 * mv.KVector2.Scalar25 - mv.KVector2.Scalar35 * mv.KVector2.Scalar35 - mv.KVector2.Scalar45 * mv.KVector2.Scalar45 + mv.KVector2.Scalar16 * mv.KVector2.Scalar16 - mv.KVector2.Scalar26 * mv.KVector2.Scalar26 - mv.KVector2.Scalar36 * mv.KVector2.Scalar36 - mv.KVector2.Scalar46 * mv.KVector2.Scalar46 - mv.KVector2.Scalar56 * mv.KVector2.Scalar56;
        }
        
        if (!mv.KVector3.IsZero())
        {
            normSquared += mv.KVector3.Scalar123 * mv.KVector3.Scalar123 + mv.KVector3.Scalar124 * mv.KVector3.Scalar124 + mv.KVector3.Scalar134 * mv.KVector3.Scalar134 - mv.KVector3.Scalar234 * mv.KVector3.Scalar234 + mv.KVector3.Scalar125 * mv.KVector3.Scalar125 + mv.KVector3.Scalar135 * mv.KVector3.Scalar135 - mv.KVector3.Scalar235 * mv.KVector3.Scalar235 + mv.KVector3.Scalar145 * mv.KVector3.Scalar145 - mv.KVector3.Scalar245 * mv.KVector3.Scalar245 - mv.KVector3.Scalar345 * mv.KVector3.Scalar345 + mv.KVector3.Scalar126 * mv.KVector3.Scalar126 + mv.KVector3.Scalar136 * mv.KVector3.Scalar136 - mv.KVector3.Scalar236 * mv.KVector3.Scalar236 + mv.KVector3.Scalar146 * mv.KVector3.Scalar146 - mv.KVector3.Scalar246 * mv.KVector3.Scalar246 - mv.KVector3.Scalar346 * mv.KVector3.Scalar346 + mv.KVector3.Scalar156 * mv.KVector3.Scalar156 - mv.KVector3.Scalar256 * mv.KVector3.Scalar256 - mv.KVector3.Scalar356 * mv.KVector3.Scalar356 - mv.KVector3.Scalar456 * mv.KVector3.Scalar456;
        }
        
        if (!mv.KVector4.IsZero())
        {
            normSquared += -mv.KVector4.Scalar1234 * mv.KVector4.Scalar1234 - mv.KVector4.Scalar1235 * mv.KVector4.Scalar1235 - mv.KVector4.Scalar1245 * mv.KVector4.Scalar1245 - mv.KVector4.Scalar1345 * mv.KVector4.Scalar1345 + mv.KVector4.Scalar2345 * mv.KVector4.Scalar2345 - mv.KVector4.Scalar1236 * mv.KVector4.Scalar1236 - mv.KVector4.Scalar1246 * mv.KVector4.Scalar1246 - mv.KVector4.Scalar1346 * mv.KVector4.Scalar1346 + mv.KVector4.Scalar2346 * mv.KVector4.Scalar2346 - mv.KVector4.Scalar1256 * mv.KVector4.Scalar1256 - mv.KVector4.Scalar1356 * mv.KVector4.Scalar1356 + mv.KVector4.Scalar2356 * mv.KVector4.Scalar2356 - mv.KVector4.Scalar1456 * mv.KVector4.Scalar1456 + mv.KVector4.Scalar2456 * mv.KVector4.Scalar2456 + mv.KVector4.Scalar3456 * mv.KVector4.Scalar3456;
        }
        
        if (!mv.KVector5.IsZero())
        {
            normSquared += -mv.KVector5.Scalar12345 * mv.KVector5.Scalar12345 - mv.KVector5.Scalar12346 * mv.KVector5.Scalar12346 - mv.KVector5.Scalar12356 * mv.KVector5.Scalar12356 - mv.KVector5.Scalar12456 * mv.KVector5.Scalar12456 - mv.KVector5.Scalar13456 * mv.KVector5.Scalar13456 + mv.KVector5.Scalar23456 * mv.KVector5.Scalar23456;
        }
        
        if (!mv.KVector6.IsZero())
        {
            normSquared += mv.KVector6.Scalar123456 * mv.KVector6.Scalar123456;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga51KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga51KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar1 * mv.Scalar1 + mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3 + mv.Scalar4 * mv.Scalar4 + mv.Scalar5 * mv.Scalar5 + mv.Scalar6 * mv.Scalar6;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga51KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar12 * mv.Scalar12 - mv.Scalar13 * mv.Scalar13 + mv.Scalar23 * mv.Scalar23 - mv.Scalar14 * mv.Scalar14 + mv.Scalar24 * mv.Scalar24 + mv.Scalar34 * mv.Scalar34 - mv.Scalar15 * mv.Scalar15 + mv.Scalar25 * mv.Scalar25 + mv.Scalar35 * mv.Scalar35 + mv.Scalar45 * mv.Scalar45 - mv.Scalar16 * mv.Scalar16 + mv.Scalar26 * mv.Scalar26 + mv.Scalar36 * mv.Scalar36 + mv.Scalar46 * mv.Scalar46 + mv.Scalar56 * mv.Scalar56;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga51KVector3 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar123 * mv.Scalar123 - mv.Scalar124 * mv.Scalar124 - mv.Scalar134 * mv.Scalar134 + mv.Scalar234 * mv.Scalar234 - mv.Scalar125 * mv.Scalar125 - mv.Scalar135 * mv.Scalar135 + mv.Scalar235 * mv.Scalar235 - mv.Scalar145 * mv.Scalar145 + mv.Scalar245 * mv.Scalar245 + mv.Scalar345 * mv.Scalar345 - mv.Scalar126 * mv.Scalar126 - mv.Scalar136 * mv.Scalar136 + mv.Scalar236 * mv.Scalar236 - mv.Scalar146 * mv.Scalar146 + mv.Scalar246 * mv.Scalar246 + mv.Scalar346 * mv.Scalar346 - mv.Scalar156 * mv.Scalar156 + mv.Scalar256 * mv.Scalar256 + mv.Scalar356 * mv.Scalar356 + mv.Scalar456 * mv.Scalar456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga51KVector4 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar1234 * mv.Scalar1234 - mv.Scalar1235 * mv.Scalar1235 - mv.Scalar1245 * mv.Scalar1245 - mv.Scalar1345 * mv.Scalar1345 + mv.Scalar2345 * mv.Scalar2345 - mv.Scalar1236 * mv.Scalar1236 - mv.Scalar1246 * mv.Scalar1246 - mv.Scalar1346 * mv.Scalar1346 + mv.Scalar2346 * mv.Scalar2346 - mv.Scalar1256 * mv.Scalar1256 - mv.Scalar1356 * mv.Scalar1356 + mv.Scalar2356 * mv.Scalar2356 - mv.Scalar1456 * mv.Scalar1456 + mv.Scalar2456 * mv.Scalar2456 + mv.Scalar3456 * mv.Scalar3456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga51KVector5 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar12345 * mv.Scalar12345 - mv.Scalar12346 * mv.Scalar12346 - mv.Scalar12356 * mv.Scalar12356 - mv.Scalar12456 * mv.Scalar12456 - mv.Scalar13456 * mv.Scalar13456 + mv.Scalar23456 * mv.Scalar23456;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga51KVector6 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar123456 * mv.Scalar123456;
    }
    
    public static double NormSquared(this Ga51Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += -mv.KVector1.Scalar1 * mv.KVector1.Scalar1 + mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3 + mv.KVector1.Scalar4 * mv.KVector1.Scalar4 + mv.KVector1.Scalar5 * mv.KVector1.Scalar5 + mv.KVector1.Scalar6 * mv.KVector1.Scalar6;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += -mv.KVector2.Scalar12 * mv.KVector2.Scalar12 - mv.KVector2.Scalar13 * mv.KVector2.Scalar13 + mv.KVector2.Scalar23 * mv.KVector2.Scalar23 - mv.KVector2.Scalar14 * mv.KVector2.Scalar14 + mv.KVector2.Scalar24 * mv.KVector2.Scalar24 + mv.KVector2.Scalar34 * mv.KVector2.Scalar34 - mv.KVector2.Scalar15 * mv.KVector2.Scalar15 + mv.KVector2.Scalar25 * mv.KVector2.Scalar25 + mv.KVector2.Scalar35 * mv.KVector2.Scalar35 + mv.KVector2.Scalar45 * mv.KVector2.Scalar45 - mv.KVector2.Scalar16 * mv.KVector2.Scalar16 + mv.KVector2.Scalar26 * mv.KVector2.Scalar26 + mv.KVector2.Scalar36 * mv.KVector2.Scalar36 + mv.KVector2.Scalar46 * mv.KVector2.Scalar46 + mv.KVector2.Scalar56 * mv.KVector2.Scalar56;
        }
        
        if (!mv.KVector3.IsZero())
        {
            normSquared += -mv.KVector3.Scalar123 * mv.KVector3.Scalar123 - mv.KVector3.Scalar124 * mv.KVector3.Scalar124 - mv.KVector3.Scalar134 * mv.KVector3.Scalar134 + mv.KVector3.Scalar234 * mv.KVector3.Scalar234 - mv.KVector3.Scalar125 * mv.KVector3.Scalar125 - mv.KVector3.Scalar135 * mv.KVector3.Scalar135 + mv.KVector3.Scalar235 * mv.KVector3.Scalar235 - mv.KVector3.Scalar145 * mv.KVector3.Scalar145 + mv.KVector3.Scalar245 * mv.KVector3.Scalar245 + mv.KVector3.Scalar345 * mv.KVector3.Scalar345 - mv.KVector3.Scalar126 * mv.KVector3.Scalar126 - mv.KVector3.Scalar136 * mv.KVector3.Scalar136 + mv.KVector3.Scalar236 * mv.KVector3.Scalar236 - mv.KVector3.Scalar146 * mv.KVector3.Scalar146 + mv.KVector3.Scalar246 * mv.KVector3.Scalar246 + mv.KVector3.Scalar346 * mv.KVector3.Scalar346 - mv.KVector3.Scalar156 * mv.KVector3.Scalar156 + mv.KVector3.Scalar256 * mv.KVector3.Scalar256 + mv.KVector3.Scalar356 * mv.KVector3.Scalar356 + mv.KVector3.Scalar456 * mv.KVector3.Scalar456;
        }
        
        if (!mv.KVector4.IsZero())
        {
            normSquared += -mv.KVector4.Scalar1234 * mv.KVector4.Scalar1234 - mv.KVector4.Scalar1235 * mv.KVector4.Scalar1235 - mv.KVector4.Scalar1245 * mv.KVector4.Scalar1245 - mv.KVector4.Scalar1345 * mv.KVector4.Scalar1345 + mv.KVector4.Scalar2345 * mv.KVector4.Scalar2345 - mv.KVector4.Scalar1236 * mv.KVector4.Scalar1236 - mv.KVector4.Scalar1246 * mv.KVector4.Scalar1246 - mv.KVector4.Scalar1346 * mv.KVector4.Scalar1346 + mv.KVector4.Scalar2346 * mv.KVector4.Scalar2346 - mv.KVector4.Scalar1256 * mv.KVector4.Scalar1256 - mv.KVector4.Scalar1356 * mv.KVector4.Scalar1356 + mv.KVector4.Scalar2356 * mv.KVector4.Scalar2356 - mv.KVector4.Scalar1456 * mv.KVector4.Scalar1456 + mv.KVector4.Scalar2456 * mv.KVector4.Scalar2456 + mv.KVector4.Scalar3456 * mv.KVector4.Scalar3456;
        }
        
        if (!mv.KVector5.IsZero())
        {
            normSquared += -mv.KVector5.Scalar12345 * mv.KVector5.Scalar12345 - mv.KVector5.Scalar12346 * mv.KVector5.Scalar12346 - mv.KVector5.Scalar12356 * mv.KVector5.Scalar12356 - mv.KVector5.Scalar12456 * mv.KVector5.Scalar12456 - mv.KVector5.Scalar13456 * mv.KVector5.Scalar13456 + mv.KVector5.Scalar23456 * mv.KVector5.Scalar23456;
        }
        
        if (!mv.KVector6.IsZero())
        {
            normSquared += -mv.KVector6.Scalar123456 * mv.KVector6.Scalar123456;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga51KVector0 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga51KVector1 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga51KVector2 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga51KVector3 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga51KVector4 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga51KVector5 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga51KVector6 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga51Multivector mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
}

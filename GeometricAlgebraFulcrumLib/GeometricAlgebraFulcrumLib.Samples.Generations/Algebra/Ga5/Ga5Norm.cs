using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga5;

public static class Ga5Norm
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga5KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga5KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar1 * mv.Scalar1 + mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3 + mv.Scalar4 * mv.Scalar4 + mv.Scalar5 * mv.Scalar5;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga5KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar12 * mv.Scalar12 - mv.Scalar13 * mv.Scalar13 - mv.Scalar23 * mv.Scalar23 - mv.Scalar14 * mv.Scalar14 - mv.Scalar24 * mv.Scalar24 - mv.Scalar34 * mv.Scalar34 - mv.Scalar15 * mv.Scalar15 - mv.Scalar25 * mv.Scalar25 - mv.Scalar35 * mv.Scalar35 - mv.Scalar45 * mv.Scalar45;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga5KVector3 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return -mv.Scalar123 * mv.Scalar123 - mv.Scalar124 * mv.Scalar124 - mv.Scalar134 * mv.Scalar134 - mv.Scalar234 * mv.Scalar234 - mv.Scalar125 * mv.Scalar125 - mv.Scalar135 * mv.Scalar135 - mv.Scalar235 * mv.Scalar235 - mv.Scalar145 * mv.Scalar145 - mv.Scalar245 * mv.Scalar245 - mv.Scalar345 * mv.Scalar345;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga5KVector4 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar1234 * mv.Scalar1234 + mv.Scalar1235 * mv.Scalar1235 + mv.Scalar1245 * mv.Scalar1245 + mv.Scalar1345 * mv.Scalar1345 + mv.Scalar2345 * mv.Scalar2345;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SpSquared(this Ga5KVector5 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar12345 * mv.Scalar12345;
    }
    
    public static double SpSquared(this Ga5Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += mv.KVector1.Scalar1 * mv.KVector1.Scalar1 + mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3 + mv.KVector1.Scalar4 * mv.KVector1.Scalar4 + mv.KVector1.Scalar5 * mv.KVector1.Scalar5;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += -mv.KVector2.Scalar12 * mv.KVector2.Scalar12 - mv.KVector2.Scalar13 * mv.KVector2.Scalar13 - mv.KVector2.Scalar23 * mv.KVector2.Scalar23 - mv.KVector2.Scalar14 * mv.KVector2.Scalar14 - mv.KVector2.Scalar24 * mv.KVector2.Scalar24 - mv.KVector2.Scalar34 * mv.KVector2.Scalar34 - mv.KVector2.Scalar15 * mv.KVector2.Scalar15 - mv.KVector2.Scalar25 * mv.KVector2.Scalar25 - mv.KVector2.Scalar35 * mv.KVector2.Scalar35 - mv.KVector2.Scalar45 * mv.KVector2.Scalar45;
        }
        
        if (!mv.KVector3.IsZero())
        {
            normSquared += -mv.KVector3.Scalar123 * mv.KVector3.Scalar123 - mv.KVector3.Scalar124 * mv.KVector3.Scalar124 - mv.KVector3.Scalar134 * mv.KVector3.Scalar134 - mv.KVector3.Scalar234 * mv.KVector3.Scalar234 - mv.KVector3.Scalar125 * mv.KVector3.Scalar125 - mv.KVector3.Scalar135 * mv.KVector3.Scalar135 - mv.KVector3.Scalar235 * mv.KVector3.Scalar235 - mv.KVector3.Scalar145 * mv.KVector3.Scalar145 - mv.KVector3.Scalar245 * mv.KVector3.Scalar245 - mv.KVector3.Scalar345 * mv.KVector3.Scalar345;
        }
        
        if (!mv.KVector4.IsZero())
        {
            normSquared += mv.KVector4.Scalar1234 * mv.KVector4.Scalar1234 + mv.KVector4.Scalar1235 * mv.KVector4.Scalar1235 + mv.KVector4.Scalar1245 * mv.KVector4.Scalar1245 + mv.KVector4.Scalar1345 * mv.KVector4.Scalar1345 + mv.KVector4.Scalar2345 * mv.KVector4.Scalar2345;
        }
        
        if (!mv.KVector5.IsZero())
        {
            normSquared += mv.KVector5.Scalar12345 * mv.KVector5.Scalar12345;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga5KVector0 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar * mv.Scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga5KVector1 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar1 * mv.Scalar1 + mv.Scalar2 * mv.Scalar2 + mv.Scalar3 * mv.Scalar3 + mv.Scalar4 * mv.Scalar4 + mv.Scalar5 * mv.Scalar5;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga5KVector2 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar12 * mv.Scalar12 + mv.Scalar13 * mv.Scalar13 + mv.Scalar23 * mv.Scalar23 + mv.Scalar14 * mv.Scalar14 + mv.Scalar24 * mv.Scalar24 + mv.Scalar34 * mv.Scalar34 + mv.Scalar15 * mv.Scalar15 + mv.Scalar25 * mv.Scalar25 + mv.Scalar35 * mv.Scalar35 + mv.Scalar45 * mv.Scalar45;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga5KVector3 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar123 * mv.Scalar123 + mv.Scalar124 * mv.Scalar124 + mv.Scalar134 * mv.Scalar134 + mv.Scalar234 * mv.Scalar234 + mv.Scalar125 * mv.Scalar125 + mv.Scalar135 * mv.Scalar135 + mv.Scalar235 * mv.Scalar235 + mv.Scalar145 * mv.Scalar145 + mv.Scalar245 * mv.Scalar245 + mv.Scalar345 * mv.Scalar345;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga5KVector4 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar1234 * mv.Scalar1234 + mv.Scalar1235 * mv.Scalar1235 + mv.Scalar1245 * mv.Scalar1245 + mv.Scalar1345 * mv.Scalar1345 + mv.Scalar2345 * mv.Scalar2345;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NormSquared(this Ga5KVector5 mv)
    {
        if (mv.IsZero()) return 0d;
        
        return mv.Scalar12345 * mv.Scalar12345;
    }
    
    public static double NormSquared(this Ga5Multivector mv)
    {
        if (mv.IsZero()) return 0d;
        
        var normSquared = 0d;
        
        if (!mv.KVector0.IsZero())
        {
            normSquared += mv.KVector0.Scalar * mv.KVector0.Scalar;
        }
        
        if (!mv.KVector1.IsZero())
        {
            normSquared += mv.KVector1.Scalar1 * mv.KVector1.Scalar1 + mv.KVector1.Scalar2 * mv.KVector1.Scalar2 + mv.KVector1.Scalar3 * mv.KVector1.Scalar3 + mv.KVector1.Scalar4 * mv.KVector1.Scalar4 + mv.KVector1.Scalar5 * mv.KVector1.Scalar5;
        }
        
        if (!mv.KVector2.IsZero())
        {
            normSquared += mv.KVector2.Scalar12 * mv.KVector2.Scalar12 + mv.KVector2.Scalar13 * mv.KVector2.Scalar13 + mv.KVector2.Scalar23 * mv.KVector2.Scalar23 + mv.KVector2.Scalar14 * mv.KVector2.Scalar14 + mv.KVector2.Scalar24 * mv.KVector2.Scalar24 + mv.KVector2.Scalar34 * mv.KVector2.Scalar34 + mv.KVector2.Scalar15 * mv.KVector2.Scalar15 + mv.KVector2.Scalar25 * mv.KVector2.Scalar25 + mv.KVector2.Scalar35 * mv.KVector2.Scalar35 + mv.KVector2.Scalar45 * mv.KVector2.Scalar45;
        }
        
        if (!mv.KVector3.IsZero())
        {
            normSquared += mv.KVector3.Scalar123 * mv.KVector3.Scalar123 + mv.KVector3.Scalar124 * mv.KVector3.Scalar124 + mv.KVector3.Scalar134 * mv.KVector3.Scalar134 + mv.KVector3.Scalar234 * mv.KVector3.Scalar234 + mv.KVector3.Scalar125 * mv.KVector3.Scalar125 + mv.KVector3.Scalar135 * mv.KVector3.Scalar135 + mv.KVector3.Scalar235 * mv.KVector3.Scalar235 + mv.KVector3.Scalar145 * mv.KVector3.Scalar145 + mv.KVector3.Scalar245 * mv.KVector3.Scalar245 + mv.KVector3.Scalar345 * mv.KVector3.Scalar345;
        }
        
        if (!mv.KVector4.IsZero())
        {
            normSquared += mv.KVector4.Scalar1234 * mv.KVector4.Scalar1234 + mv.KVector4.Scalar1235 * mv.KVector4.Scalar1235 + mv.KVector4.Scalar1245 * mv.KVector4.Scalar1245 + mv.KVector4.Scalar1345 * mv.KVector4.Scalar1345 + mv.KVector4.Scalar2345 * mv.KVector4.Scalar2345;
        }
        
        if (!mv.KVector5.IsZero())
        {
            normSquared += mv.KVector5.Scalar12345 * mv.KVector5.Scalar12345;
        }
        
        return normSquared;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga5KVector0 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga5KVector1 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga5KVector2 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga5KVector3 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga5KVector4 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga5KVector5 mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Norm(this Ga5Multivector mv)
    {
        return Math.Sqrt(Math.Abs(mv.NormSquared()));
    }
    
}

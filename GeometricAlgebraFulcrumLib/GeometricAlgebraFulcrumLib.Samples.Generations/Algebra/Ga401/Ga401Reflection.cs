using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga401;

public static class Ga401Reflection
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDirectOnDirect(this Ga401KVector0 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDirectOnDirect(this Ga401KVector0 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar23 * mv1.Scalar * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDirectOnDirect(this Ga401KVector0 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar234 * mv1.Scalar * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDirectOnDirect(this Ga401KVector0 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar2345 * mv1.Scalar * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDirectOnDirect(this Ga401KVector0 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDirectOnDirect(this Ga401KVector1 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar2 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar2 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar3 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar3 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar3 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar4 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar4 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar5 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar5 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar5 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.Scalar4 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar5 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar5 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDirectOnDirect(this Ga401KVector1 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (-2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.Scalar5 * mv2.Scalar25 - 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar4 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.Scalar5 * mv2.Scalar35 - mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.Scalar2 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.Scalar3 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar5 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar1 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.Scalar2 * mv2.Scalar25 - 2 * mv2.Scalar15 * mv1.Scalar3 * mv2.Scalar35 - 2 * mv2.Scalar15 * mv1.Scalar4 * mv2.Scalar45 - mv2.Scalar25 * mv1.Scalar1 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar1 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar1 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar34 - 2 * mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar35 - mv2.Scalar24 * mv1.Scalar2 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar2 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar45 - mv2.Scalar25 * mv1.Scalar2 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar2 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar2 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.Scalar3 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar34 * mv1.Scalar3 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar3 * mv2.Scalar35 + mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar24 + mv2.Scalar25 * mv1.Scalar3 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar25 - 2 * mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar3 * mv2.Scalar45 - 2 * mv2.Scalar35 * mv1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar4 = (-mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar4 * mv2.Scalar24 + mv2.Scalar25 * mv1.Scalar4 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar4 * mv2.Scalar45 + mv2.Scalar34 * mv1.Scalar4 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar4 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar5 = (-mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar23 + mv2.Scalar45 * mv1.Scalar5 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar5 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDirectOnDirect(this Ga401KVector1 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar123 * mv1.Scalar4 * mv2.Scalar234 + 2 * mv2.Scalar123 * mv1.Scalar5 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.Scalar3 * mv2.Scalar234 + 2 * mv2.Scalar124 * mv1.Scalar5 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.Scalar5 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar1 * mv2.Scalar234 + 2 * mv2.Scalar125 * mv1.Scalar3 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.Scalar4 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar4 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar1 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.Scalar2 * mv2.Scalar245 + 2 * mv2.Scalar145 * mv1.Scalar3 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar1 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar1 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar234 * mv1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.Scalar3 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar2 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar2 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar234 * mv1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar3 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar3 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar3 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar4 = (-mv2.Scalar345 * mv1.Scalar4 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar4 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar5 = (-mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar5 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar5 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar5 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDirectOnDirect(this Ga401KVector1 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (-2 * mv2.Scalar1234 * mv1.Scalar5 * mv2.Scalar2345 + 2 * mv2.Scalar1235 * mv1.Scalar4 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar3 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar2 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar2345 * mv1.Scalar2 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar2345 * mv1.Scalar3 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar4 = (mv2.Scalar2345 * mv1.Scalar4 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar5 = (mv2.Scalar2345 * mv1.Scalar5 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDirectOnDirect(this Ga401KVector1 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector1.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDirectOnDirect(this Ga401KVector2 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar25 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar15 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar12 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar12 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar13 = (-2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar35 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar15 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar13 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar13 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar25 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar23 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar23 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar14 = (-2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar15 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar14 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar25 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar24 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar34 = (2 * mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar35 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar34 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar15 = (-2 * mv2.Scalar1 * mv1.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar15 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar15 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.Scalar14 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar15 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar15 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar25 = (-mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar25 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.Scalar24 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar25 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar25 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar35 = (2 * mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar34 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar35 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar35 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar45 = (2 * mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar34 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar35 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar45 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar45 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDirectOnDirect(this Ga401KVector2 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (2 * mv2.Scalar25 * mv1.Scalar14 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar12 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar12 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar12 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar12 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar12 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.Scalar13 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar35 * mv1.Scalar14 * mv2.Scalar45 + 2 * mv2.Scalar12 * mv1.Scalar24 * mv2.Scalar34 + 2 * mv2.Scalar12 * mv1.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.Scalar25 * mv2.Scalar25 + mv2.Scalar34 * mv1.Scalar13 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar13 * mv2.Scalar35 - mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar13 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.Scalar23 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar14 - 2 * mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar15 + mv2.Scalar45 * mv1.Scalar13 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar23 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar23 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar25 + mv2.Scalar34 * mv1.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar35 * mv1.Scalar24 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar23 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar14 = (2 * mv2.Scalar12 * mv1.Scalar34 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar25 * mv2.Scalar45 + 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.Scalar34 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.Scalar35 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.Scalar35 * mv2.Scalar35 - mv2.Scalar24 * mv1.Scalar14 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar14 * mv2.Scalar25 - 2 * mv2.Scalar15 * mv1.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar15 + mv2.Scalar45 * mv1.Scalar14 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar14 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar14 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar15) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar35 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar24 * mv2.Scalar35 - mv2.Scalar34 * mv1.Scalar24 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar24 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar34 = (mv2.Scalar25 * mv1.Scalar34 * mv2.Scalar25 + mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.Scalar23 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar34 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar34 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar34 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar15 = (2 * mv2.Scalar12 * mv1.Scalar35 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar45 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.Scalar25 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar45 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.Scalar35 * mv2.Scalar35 + 2 * mv2.Scalar14 * mv1.Scalar25 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar35 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar15 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.Scalar45 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar15 * mv2.Scalar45 - 2 * mv2.Scalar15 * mv1.Scalar25 * mv2.Scalar25 - mv2.Scalar25 * mv1.Scalar15 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar25 = (-mv2.Scalar35 * mv1.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.Scalar35 * mv2.Scalar35 + mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar25 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.Scalar45 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar34 - mv2.Scalar45 * mv1.Scalar25 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar35 = (2 * mv2.Scalar34 * mv1.Scalar24 * mv2.Scalar25 + mv2.Scalar25 * mv1.Scalar35 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar35 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar35 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar24 - mv2.Scalar35 * mv1.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.Scalar45 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.Scalar23 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar35 + mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar45 = (2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar34 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar23 - 2 * mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar45 * mv2.Scalar24 + mv2.Scalar35 * mv1.Scalar45 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar34 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar45 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar45 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar45 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDirectOnDirect(this Ga401KVector2 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (mv2.Scalar234 * mv1.Scalar12 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.Scalar13 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar12 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar12 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar12 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar124 * mv1.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.Scalar23 * mv2.Scalar234 - 2 * mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar145 - 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar245 - mv2.Scalar234 * mv1.Scalar13 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar145 + 2 * mv2.Scalar125 * mv1.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.Scalar23 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar24 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar13 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar13 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar13 * mv2.Scalar245 - 2 * mv2.Scalar145 * mv1.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar234 * mv1.Scalar23 * mv2.Scalar234 + mv2.Scalar345 * mv1.Scalar23 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar245 - mv2.Scalar235 * mv1.Scalar23 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar14 = (-2 * mv2.Scalar123 * mv1.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.Scalar35 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.Scalar34 * mv2.Scalar234 - 2 * mv2.Scalar145 * mv1.Scalar34 * mv2.Scalar345 + 2 * mv2.Scalar124 * mv1.Scalar35 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.Scalar24 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar235 + mv2.Scalar234 * mv1.Scalar14 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.Scalar34 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar135 + 2 * mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar125 - mv2.Scalar345 * mv1.Scalar14 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar14 * mv2.Scalar245 - 2 * mv2.Scalar145 * mv1.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar234 * mv1.Scalar24 * mv2.Scalar234 - mv2.Scalar345 * mv1.Scalar24 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar24 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar34 = (-mv2.Scalar234 * mv1.Scalar34 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar34 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar34 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar23 * mv2.Scalar345 - mv2.Scalar345 * mv1.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar15 = (mv2.Scalar245 * mv1.Scalar15 * mv2.Scalar245 - 2 * mv2.Scalar123 * mv1.Scalar45 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.Scalar35 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar234 + 2 * mv2.Scalar145 * mv1.Scalar35 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar45 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.Scalar35 * mv2.Scalar235 + mv2.Scalar345 * mv1.Scalar15 * mv2.Scalar345 + 2 * mv2.Scalar135 * mv1.Scalar25 * mv2.Scalar235 + mv2.Scalar235 * mv1.Scalar15 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.Scalar25 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar25 = (mv2.Scalar235 * mv1.Scalar25 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar25 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar25 * mv2.Scalar345 + 2 * mv2.Scalar245 * mv1.Scalar35 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar35 = (-mv2.Scalar235 * mv1.Scalar35 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.Scalar23 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar234 + mv2.Scalar345 * mv1.Scalar35 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar35 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar45 = (mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar34 * mv2.Scalar245 - mv2.Scalar234 * mv1.Scalar45 * mv2.Scalar234 - mv2.Scalar245 * mv1.Scalar45 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar45 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDirectOnDirect(this Ga401KVector2 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (2 * mv2.Scalar1234 * mv1.Scalar25 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar24 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar12 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar1234 * mv1.Scalar35 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar13 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar2345 * mv1.Scalar23 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar14 = (2 * mv2.Scalar1234 * mv1.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar24 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar14 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar2345 * mv1.Scalar24 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar34 = (mv2.Scalar2345 * mv1.Scalar34 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar15 = (2 * mv2.Scalar1235 * mv1.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar35 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar25 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar15 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar25 = (mv2.Scalar2345 * mv1.Scalar25 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar35 = (mv2.Scalar2345 * mv1.Scalar35 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar45 = (mv2.Scalar2345 * mv1.Scalar45 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDirectOnDirect(this Ga401KVector2 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector2.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDirectOnDirect(this Ga401KVector3 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar125 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar123 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar123 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar124 = (-2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar125 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar124 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar134 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar135 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar134 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar2 * mv1.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar234 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar235 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar234 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar125 = (-2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar125 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.Scalar124 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar125 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar125 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar135 = (2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar134 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar135 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar135 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar235 = (mv2.Scalar2 * mv1.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar235 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar234 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar235 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar235 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar145 = (2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar134 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar135 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar145 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar145 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar245 = (mv2.Scalar2 * mv1.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar234 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar235 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar245 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar245 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar345 = (2 * mv2.Scalar2 * mv1.Scalar234 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar235 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar245 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar345 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar345 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDirectOnDirect(this Ga401KVector3 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar24 + 2 * mv2.Scalar12 * mv1.Scalar235 * mv2.Scalar25 - 2 * mv2.Scalar15 * mv1.Scalar234 * mv2.Scalar45 - 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.Scalar235 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar245 * mv2.Scalar15 - 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar125 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar45 - 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar35 - mv2.Scalar35 * mv1.Scalar123 * mv2.Scalar35 - mv2.Scalar34 * mv1.Scalar123 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar123 * mv2.Scalar25 - 2 * mv2.Scalar35 * mv1.Scalar124 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar123 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar124 = (2 * mv2.Scalar12 * mv1.Scalar245 * mv2.Scalar25 + 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.Scalar345 * mv2.Scalar25 - mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar135 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar15 - 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar124 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.Scalar134 * mv2.Scalar35 + 2 * mv2.Scalar24 * mv1.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar124 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar124 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar134 = (-2 * mv2.Scalar12 * mv1.Scalar235 * mv2.Scalar45 - 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar23 - 2 * mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar15 + 2 * mv2.Scalar13 * mv1.Scalar345 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.Scalar123 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar23 + mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.Scalar345 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.Scalar234 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar25 - mv2.Scalar25 * mv1.Scalar134 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar134 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar134 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar23 + mv2.Scalar35 * mv1.Scalar234 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar35 + mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar34 - mv2.Scalar24 * mv1.Scalar234 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar234 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar234 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar235 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar125 = (2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar125 * mv2.Scalar23 + mv2.Scalar25 * mv1.Scalar125 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.Scalar145 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar125 * mv2.Scalar35 + 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar35 - mv2.Scalar24 * mv1.Scalar125 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar15 + mv2.Scalar45 * mv1.Scalar125 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.Scalar135 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar135 = (-2 * mv2.Scalar15 * mv1.Scalar235 * mv2.Scalar25 - 2 * mv2.Scalar12 * mv1.Scalar245 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.Scalar235 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar135 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar24 + mv2.Scalar35 * mv1.Scalar135 * mv2.Scalar35 + 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar24 + 2 * mv2.Scalar35 * mv1.Scalar145 * mv2.Scalar45 + 2 * mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar14 + 2 * mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar135 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar25 - mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar135 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar235 = (mv2.Scalar23 * mv1.Scalar235 * mv2.Scalar23 + 2 * mv2.Scalar35 * mv1.Scalar245 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar235 * mv2.Scalar34 + mv2.Scalar45 * mv1.Scalar235 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar235 * mv2.Scalar35 - mv2.Scalar25 * mv1.Scalar235 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar145 = (-2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar35 - 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar25 - 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar15 - mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.Scalar245 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.Scalar345 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar24 + 2 * mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar145 * mv2.Scalar24 - mv2.Scalar35 * mv1.Scalar145 * mv2.Scalar35 - 2 * mv2.Scalar15 * mv1.Scalar345 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar45 + mv2.Scalar34 * mv1.Scalar145 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar145 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar145 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar245 = (mv2.Scalar24 * mv1.Scalar245 * mv2.Scalar24 + 2 * mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar245 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar25 - mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar245 * mv2.Scalar25 + mv2.Scalar34 * mv1.Scalar245 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar45 + mv2.Scalar45 * mv1.Scalar245 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.Scalar345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar345 = (-mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar235 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar23 + mv2.Scalar34 * mv1.Scalar345 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar345 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar345 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.Scalar235 * mv2.Scalar45 + mv2.Scalar45 * mv1.Scalar345 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDirectOnDirect(this Ga401KVector3 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (-2 * mv2.Scalar123 * mv1.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar123 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar123 * mv1.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.Scalar245 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar345 + mv2.Scalar345 * mv1.Scalar123 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar123 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.Scalar245 * mv2.Scalar125 - 2 * mv2.Scalar125 * mv1.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar234 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar123 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar123 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar124 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar124 = (2 * mv2.Scalar123 * mv1.Scalar245 * mv2.Scalar235 - 2 * mv2.Scalar124 * mv1.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.Scalar345 * mv2.Scalar245 - mv2.Scalar245 * mv1.Scalar124 * mv2.Scalar245 + mv2.Scalar234 * mv1.Scalar124 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar124 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar145 - mv2.Scalar345 * mv1.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar245 * mv1.Scalar134 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar134 = (-2 * mv2.Scalar123 * mv1.Scalar235 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.Scalar345 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar134 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar134 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar234 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar135 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar135 - 2 * mv2.Scalar235 * mv1.Scalar123 * mv2.Scalar345 - mv2.Scalar345 * mv1.Scalar134 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.Scalar234 * mv2.Scalar245 + mv2.Scalar245 * mv1.Scalar134 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar234 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar234 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar125 = (2 * mv2.Scalar123 * mv1.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.Scalar234 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar125 + mv2.Scalar235 * mv1.Scalar125 * mv2.Scalar235 - mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar234 + 2 * mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar125 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar125 * mv2.Scalar345 + 2 * mv2.Scalar245 * mv1.Scalar135 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar135 = (-2 * mv2.Scalar145 * mv1.Scalar235 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.Scalar123 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar135 * mv2.Scalar234 + mv2.Scalar345 * mv1.Scalar135 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar245 * mv2.Scalar245 - mv2.Scalar235 * mv1.Scalar135 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar135 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar145) * mv2NormSquaredInv,
            Scalar235 = (mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar235 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar235 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar145 = (-2 * mv2.Scalar124 * mv1.Scalar234 * mv2.Scalar345 - 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar234 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar134 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar145 - mv2.Scalar234 * mv1.Scalar145 * mv2.Scalar234 - mv2.Scalar245 * mv1.Scalar145 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar145 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar125 * mv1.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar245 * mv2.Scalar235 + mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar235 - 2 * mv2.Scalar145 * mv1.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar245 = (-mv2.Scalar234 * mv1.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar245 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.Scalar345 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar235) * mv2NormSquaredInv,
            Scalar345 = (mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar345 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar235 * mv2.Scalar345 - mv2.Scalar345 * mv1.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDirectOnDirect(this Ga401KVector3 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (-2 * mv2.Scalar1234 * mv1.Scalar235 * mv2.Scalar2345 + 2 * mv2.Scalar1235 * mv1.Scalar234 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar123 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar124 = (-2 * mv2.Scalar1234 * mv1.Scalar245 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.Scalar234 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar124 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar134 = (-2 * mv2.Scalar1234 * mv1.Scalar345 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar234 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar134 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar2345 * mv1.Scalar234 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar125 = (-2 * mv2.Scalar1235 * mv1.Scalar245 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.Scalar235 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar125 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar135 = (-2 * mv2.Scalar1235 * mv1.Scalar345 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar235 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar135 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar235 = (mv2.Scalar2345 * mv1.Scalar235 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar145 = (-2 * mv2.Scalar1245 * mv1.Scalar345 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar245 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar145 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar245 = (mv2.Scalar2345 * mv1.Scalar245 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar345 = (mv2.Scalar2345 * mv1.Scalar345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDirectOnDirect(this Ga401KVector3 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector3.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDirectOnDirect(this Ga401KVector4 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar1235 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar1234 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1235 = (-2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar1235 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar1235 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar1235 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1235 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1245 = (2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar1245 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar1235 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1245 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1245 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1345 = (-2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar1235 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar1245 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1345 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1345 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1345 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar2345 = (-mv2.Scalar2 * mv1.Scalar2345 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar2345 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar2345 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar2345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDirectOnDirect(this Ga401KVector4 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (-mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar1345 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar1234 * mv2.Scalar24 + mv2.Scalar25 * mv1.Scalar1234 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar1234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.Scalar1235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar1234 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar1235 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar1235 = (-2 * mv2.Scalar12 * mv1.Scalar2345 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.Scalar2345 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar1235 * mv2.Scalar35 - mv2.Scalar23 * mv1.Scalar1235 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar1235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar1245 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar1235 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.Scalar2345 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar1235 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar1235 * mv2.Scalar45 - 2 * mv2.Scalar35 * mv1.Scalar1245 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar1245 = (2 * mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar1245 * mv2.Scalar23 + mv2.Scalar25 * mv1.Scalar1245 * mv2.Scalar25 - mv2.Scalar24 * mv1.Scalar1245 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1245 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar1245 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar1245 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar1345 = (-2 * mv2.Scalar13 * mv1.Scalar2345 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar1235 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar1345 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar1345 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar2345 * mv2.Scalar24 + mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.Scalar2345 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.Scalar1235 * mv2.Scalar45 - mv2.Scalar25 * mv1.Scalar1345 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar1345 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar2345 = (-mv2.Scalar23 * mv1.Scalar2345 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar2345 * mv2.Scalar25 - mv2.Scalar34 * mv1.Scalar2345 * mv2.Scalar34 - mv2.Scalar45 * mv1.Scalar2345 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar2345 * mv2.Scalar24 + mv2.Scalar35 * mv1.Scalar2345 * mv2.Scalar35) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDirectOnDirect(this Ga401KVector4 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (-mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar1234 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar1234 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar1235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar1345 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar1234 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar1235 = (2 * mv2.Scalar123 * mv1.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar1235 * mv2.Scalar234 - 2 * mv2.Scalar125 * mv1.Scalar2345 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar2345 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar1235 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar1235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar1245 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar1235 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar1245 = (-2 * mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar245 - mv2.Scalar234 * mv1.Scalar1245 * mv2.Scalar234 - mv2.Scalar245 * mv1.Scalar1245 * mv2.Scalar245 + mv2.Scalar235 * mv1.Scalar1245 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar1245 * mv2.Scalar345 - 2 * mv2.Scalar245 * mv1.Scalar1345 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar1345 = (2 * mv2.Scalar145 * mv1.Scalar2345 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar1345 * mv2.Scalar234 - 2 * mv2.Scalar135 * mv1.Scalar2345 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.Scalar1235 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar1345 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar1345 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar1345 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar2345 = (mv2.Scalar234 * mv1.Scalar2345 * mv2.Scalar234 + mv2.Scalar245 * mv1.Scalar2345 * mv2.Scalar245 - mv2.Scalar235 * mv1.Scalar2345 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar2345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDirectOnDirect(this Ga401KVector4 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (2 * mv2.Scalar1234 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1234 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1235 = (2 * mv2.Scalar1235 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1235 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1245 = (2 * mv2.Scalar1245 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1245 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1345 = (2 * mv2.Scalar1345 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1345 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar2345 = (mv2.Scalar2345 * mv1.Scalar2345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDirectOnDirect(this Ga401KVector4 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector4.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDirectOnDirect(this Ga401KVector5 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (mv2.Scalar2 * mv1.Scalar12345 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar12345 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar12345 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar12345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDirectOnDirect(this Ga401KVector5 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (mv2.Scalar23 * mv1.Scalar12345 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar12345 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar12345 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar12345 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar12345 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar12345 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDirectOnDirect(this Ga401KVector5 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (mv2.Scalar234 * mv1.Scalar12345 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar12345 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar12345 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar12345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDirectOnDirect(this Ga401KVector5 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (-mv2.Scalar2345 * mv1.Scalar12345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDirectOnDirect(this Ga401KVector5 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector5.Zero;
    }
    
    public static Ga401Multivector ReflectDirectOnDirect(this Ga401Multivector mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector0.Scalar * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector0.Scalar * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar1 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector1.Scalar1 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar2 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector1.Scalar2 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[4] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector1.Scalar3 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector1.Scalar3 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[8] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector1.Scalar4 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[16] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector1.Scalar5 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector1.Scalar5 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.KVector1.Scalar4 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector1.Scalar5 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector1.Scalar5 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar25 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar15 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar12 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar12 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[5] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar35 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar15 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar13 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar13 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar25 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar23 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar23 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[9] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar15 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector2.Scalar14 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar25 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector2.Scalar24 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[12] += (2 * mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector2.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar35 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector2.Scalar34 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[17] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector2.Scalar15 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector2.Scalar15 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar14 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar15 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar15 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[18] += (-mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector2.Scalar25 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar24 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar25 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar25 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[20] += (2 * mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar34 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar35 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar35 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[24] += (2 * mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar34 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar35 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar45 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar45 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar125 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar123 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector3.Scalar123 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[11] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar125 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector3.Scalar124 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector3.Scalar134 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar135 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector3.Scalar134 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar2 * mv1.KVector3.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector3.Scalar234 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector3.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar235 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector3.Scalar234 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[19] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector3.Scalar125 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar124 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar125 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar125 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[21] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar134 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar135 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar135 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[22] += (mv2.Scalar2 * mv1.KVector3.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector3.Scalar235 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar234 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar235 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar235 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[25] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar134 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar135 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar145 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar145 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[26] += (mv2.Scalar2 * mv1.KVector3.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar234 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar235 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar245 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar245 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[28] += (2 * mv2.Scalar2 * mv1.KVector3.Scalar234 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar235 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar245 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar345 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar345 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector4.Scalar1235 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector4.Scalar1234 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[23] += (-2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector4.Scalar1235 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector4.Scalar1235 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector4.Scalar1235 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar1235 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[27] += (2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector4.Scalar1245 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector4.Scalar1235 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector4.Scalar1245 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar1245 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[29] += (-2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector4.Scalar1235 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1245 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector4.Scalar1345 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector4.Scalar1345 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar1345 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[30] += (-mv2.Scalar2 * mv1.KVector4.Scalar2345 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector4.Scalar2345 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector4.Scalar2345 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar2345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (mv2.Scalar2 * mv1.KVector5.Scalar12345 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector5.Scalar12345 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector5.Scalar12345 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector5.Scalar12345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDirectOnDirect(this Ga401Multivector mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23 + mv2.Scalar45 * mv1.KVector0.Scalar * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector0.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector0.Scalar * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector0.Scalar * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector0.Scalar * mv2.Scalar35) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.KVector1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.KVector1.Scalar5 * mv2.Scalar25 - 2 * mv2.Scalar15 * mv1.KVector1.Scalar2 * mv2.Scalar25 - 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector1.Scalar4 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.KVector1.Scalar5 * mv2.Scalar35 - mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.KVector1.Scalar2 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.KVector1.Scalar3 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector1.Scalar5 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector1.Scalar1 * mv2.Scalar24 - mv2.Scalar45 * mv1.KVector1.Scalar1 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector1.Scalar1 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.KVector1.Scalar3 * mv2.Scalar35 - 2 * mv2.Scalar15 * mv1.KVector1.Scalar4 * mv2.Scalar45 - mv2.Scalar25 * mv1.KVector1.Scalar1 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector1.Scalar1 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[2] += (-2 * mv2.Scalar25 * mv1.KVector1.Scalar4 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar34 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar35 - mv2.Scalar24 * mv1.KVector1.Scalar2 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector1.Scalar2 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar45 - mv2.Scalar25 * mv1.KVector1.Scalar2 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector1.Scalar2 * mv2.Scalar35 - mv2.Scalar45 * mv1.KVector1.Scalar2 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.KVector1.Scalar3 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar45 * mv1.KVector1.Scalar3 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector1.Scalar3 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector1.Scalar3 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar24 + mv2.Scalar25 * mv1.KVector1.Scalar3 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar25 - 2 * mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar45 - 2 * mv2.Scalar35 * mv1.KVector1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[8] += (-mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector1.Scalar4 * mv2.Scalar24 + mv2.Scalar25 * mv1.KVector1.Scalar4 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector1.Scalar4 * mv2.Scalar45 + mv2.Scalar34 * mv1.KVector1.Scalar4 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector1.Scalar4 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[16] += (mv2.Scalar45 * mv1.KVector1.Scalar5 * mv2.Scalar45 + mv2.Scalar35 * mv1.KVector1.Scalar5 * mv2.Scalar35 - mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar23 + mv2.Scalar25 * mv1.KVector1.Scalar5 * mv2.Scalar25 - mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar34) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector2.Scalar12 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector2.Scalar12 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector2.Scalar12 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector2.Scalar12 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector2.Scalar12 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar13 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar14 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar24 * mv2.Scalar34 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar25 * mv2.Scalar25 - mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector2.Scalar13 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar23 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar14 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar15 + mv2.Scalar34 * mv1.KVector2.Scalar13 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar45 + mv2.Scalar35 * mv1.KVector2.Scalar13 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector2.Scalar13 * mv2.Scalar45 + 2 * mv2.Scalar35 * mv1.KVector2.Scalar14 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector2.Scalar23 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector2.Scalar23 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector2.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar25 + mv2.Scalar34 * mv1.KVector2.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar35 * mv1.KVector2.Scalar24 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar45 + mv2.Scalar35 * mv1.KVector2.Scalar23 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar34 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar25 * mv2.Scalar45 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar34 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar35 * mv2.Scalar45 + mv2.Scalar45 * mv1.KVector2.Scalar14 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar35 * mv2.Scalar35 - mv2.Scalar24 * mv1.KVector2.Scalar14 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector2.Scalar14 * mv2.Scalar25 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar15 - mv2.Scalar34 * mv1.KVector2.Scalar14 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector2.Scalar14 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar15) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar35 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector2.Scalar24 * mv2.Scalar35 - mv2.Scalar34 * mv1.KVector2.Scalar24 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector2.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector2.Scalar24 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[12] += (mv2.Scalar25 * mv1.KVector2.Scalar34 * mv2.Scalar25 + mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar23 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector2.Scalar34 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector2.Scalar34 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector2.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector2.Scalar34 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[17] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar35 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar45 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar25 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar45 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector2.Scalar15 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar35 * mv2.Scalar35 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar25 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar35 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector2.Scalar15 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar45 * mv2.Scalar45 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar25 * mv2.Scalar25 - mv2.Scalar25 * mv1.KVector2.Scalar15 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[18] += (-mv2.Scalar35 * mv1.KVector2.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar35 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector2.Scalar25 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar45 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector2.Scalar25 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[20] += (2 * mv2.Scalar34 * mv1.KVector2.Scalar24 * mv2.Scalar25 + mv2.Scalar25 * mv1.KVector2.Scalar35 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector2.Scalar35 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector2.Scalar35 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector2.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.KVector2.Scalar45 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar23 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[24] += (2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar34 * mv2.Scalar25 + mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar23 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector2.Scalar45 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector2.Scalar45 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar34 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector2.Scalar45 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector2.Scalar45 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector2.Scalar45 * mv2.Scalar45) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-mv2.Scalar45 * mv1.KVector3.Scalar123 * mv2.Scalar45 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar24 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar235 * mv2.Scalar25 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar235 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar245 * mv2.Scalar15 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar125 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar45 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar35 - mv2.Scalar35 * mv1.KVector3.Scalar123 * mv2.Scalar35 - mv2.Scalar34 * mv1.KVector3.Scalar123 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector3.Scalar123 * mv2.Scalar25 - 2 * mv2.Scalar35 * mv1.KVector3.Scalar124 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar45 - 2 * mv2.Scalar15 * mv1.KVector3.Scalar234 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[11] += (-mv2.Scalar45 * mv1.KVector3.Scalar124 * mv2.Scalar45 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar245 * mv2.Scalar25 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar345 * mv2.Scalar25 - mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar135 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar15 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector3.Scalar124 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.KVector3.Scalar134 * mv2.Scalar35 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector3.Scalar124 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[13] += (-2 * mv2.Scalar12 * mv1.KVector3.Scalar235 * mv2.Scalar45 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar23 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar15 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar345 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar123 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar23 + mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar345 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.KVector3.Scalar234 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar25 - mv2.Scalar25 * mv1.KVector3.Scalar134 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector3.Scalar134 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar35 - mv2.Scalar45 * mv1.KVector3.Scalar134 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar34 - mv2.Scalar24 * mv1.KVector3.Scalar234 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector3.Scalar234 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector3.Scalar234 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar235 * mv2.Scalar35 + mv2.Scalar35 * mv1.KVector3.Scalar234 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[19] += (2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector3.Scalar125 * mv2.Scalar23 + mv2.Scalar25 * mv1.KVector3.Scalar125 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.KVector3.Scalar145 * mv2.Scalar45 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar35 - mv2.Scalar24 * mv1.KVector3.Scalar125 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar15 + mv2.Scalar35 * mv1.KVector3.Scalar125 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector3.Scalar125 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.KVector3.Scalar135 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[21] += (mv2.Scalar45 * mv1.KVector3.Scalar135 * mv2.Scalar45 - 2 * mv2.Scalar15 * mv1.KVector3.Scalar235 * mv2.Scalar25 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar245 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar235 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector3.Scalar135 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector3.Scalar135 * mv2.Scalar35 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar24 + 2 * mv2.Scalar35 * mv1.KVector3.Scalar145 * mv2.Scalar45 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar14 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar25 - mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector3.Scalar135 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[22] += (mv2.Scalar23 * mv1.KVector3.Scalar235 * mv2.Scalar23 + 2 * mv2.Scalar35 * mv1.KVector3.Scalar245 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector3.Scalar235 * mv2.Scalar34 + mv2.Scalar45 * mv1.KVector3.Scalar235 * mv2.Scalar45 + mv2.Scalar35 * mv1.KVector3.Scalar235 * mv2.Scalar35 - mv2.Scalar25 * mv1.KVector3.Scalar235 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[25] += (-2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar35 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar25 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar15 - mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.KVector3.Scalar245 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar345 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar24 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector3.Scalar145 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector3.Scalar145 * mv2.Scalar35 - 2 * mv2.Scalar15 * mv1.KVector3.Scalar345 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar45 + mv2.Scalar34 * mv1.KVector3.Scalar145 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector3.Scalar145 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector3.Scalar145 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[26] += (-2 * mv2.Scalar25 * mv1.KVector3.Scalar345 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector3.Scalar245 * mv2.Scalar24 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector3.Scalar245 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar25 - mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector3.Scalar245 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector3.Scalar245 * mv2.Scalar45 + mv2.Scalar34 * mv1.KVector3.Scalar245 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[28] += (-mv2.Scalar35 * mv1.KVector3.Scalar345 * mv2.Scalar35 - mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar235 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar23 + mv2.Scalar34 * mv1.KVector3.Scalar345 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector3.Scalar345 * mv2.Scalar25 + 2 * mv2.Scalar25 * mv1.KVector3.Scalar235 * mv2.Scalar45 + mv2.Scalar45 * mv1.KVector3.Scalar345 * mv2.Scalar45) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector4.Scalar1345 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector4.Scalar1234 * mv2.Scalar24 + mv2.Scalar25 * mv1.KVector4.Scalar1234 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector4.Scalar1234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.KVector4.Scalar1235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector4.Scalar1234 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector4.Scalar1235 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[23] += (-2 * mv2.Scalar12 * mv1.KVector4.Scalar2345 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.KVector4.Scalar2345 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector4.Scalar1235 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector4.Scalar1235 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.KVector4.Scalar1245 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.KVector4.Scalar1245 * mv2.Scalar24 - mv2.Scalar45 * mv1.KVector4.Scalar1235 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector4.Scalar1235 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector4.Scalar1235 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.KVector4.Scalar2345 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector4.Scalar1235 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[27] += (2 * mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector4.Scalar1245 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector4.Scalar1245 * mv2.Scalar23 + mv2.Scalar25 * mv1.KVector4.Scalar1245 * mv2.Scalar25 - mv2.Scalar24 * mv1.KVector4.Scalar1245 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector4.Scalar1245 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar45 - mv2.Scalar45 * mv1.KVector4.Scalar1245 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.KVector4.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[29] += (mv2.Scalar35 * mv1.KVector4.Scalar1345 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector4.Scalar2345 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector4.Scalar1235 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector4.Scalar1345 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector4.Scalar1345 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector4.Scalar2345 * mv2.Scalar24 + mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.KVector4.Scalar2345 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.KVector4.Scalar1235 * mv2.Scalar45 - mv2.Scalar25 * mv1.KVector4.Scalar1345 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector4.Scalar1345 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[30] += (-mv2.Scalar23 * mv1.KVector4.Scalar2345 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector4.Scalar2345 * mv2.Scalar25 - mv2.Scalar34 * mv1.KVector4.Scalar2345 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector4.Scalar2345 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector4.Scalar2345 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector4.Scalar2345 * mv2.Scalar35) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (mv2.Scalar34 * mv1.KVector5.Scalar12345 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector5.Scalar12345 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector5.Scalar12345 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector5.Scalar12345 * mv2.Scalar24 + mv2.Scalar45 * mv1.KVector5.Scalar12345 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector5.Scalar12345 * mv2.Scalar25) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDirectOnDirect(this Ga401Multivector mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar345 * mv1.KVector0.Scalar * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector0.Scalar * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector0.Scalar * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector0.Scalar * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar123 * mv1.KVector1.Scalar4 * mv2.Scalar234 + 2 * mv2.Scalar123 * mv1.KVector1.Scalar5 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.KVector1.Scalar3 * mv2.Scalar234 + 2 * mv2.Scalar124 * mv1.KVector1.Scalar5 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.KVector1.Scalar5 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector1.Scalar1 * mv2.Scalar234 + 2 * mv2.Scalar125 * mv1.KVector1.Scalar3 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.KVector1.Scalar4 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector1.Scalar4 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector1.Scalar1 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.KVector1.Scalar2 * mv2.Scalar245 + 2 * mv2.Scalar145 * mv1.KVector1.Scalar3 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector1.Scalar1 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector1.Scalar1 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar234 * mv1.KVector1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.KVector1.Scalar3 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector1.Scalar2 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector1.Scalar2 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar234 * mv1.KVector1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector1.Scalar3 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector1.Scalar3 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector1.Scalar3 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[8] += (-mv2.Scalar345 * mv1.KVector1.Scalar4 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector1.Scalar4 * mv2.Scalar245 + mv2.Scalar234 * mv1.KVector1.Scalar4 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[16] += (mv2.Scalar235 * mv1.KVector1.Scalar5 * mv2.Scalar235 + mv2.Scalar345 * mv1.KVector1.Scalar5 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector1.Scalar5 * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar234 * mv1.KVector2.Scalar12 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.KVector2.Scalar13 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector2.Scalar12 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector2.Scalar12 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector2.Scalar12 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar124 * mv1.KVector2.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar23 * mv2.Scalar234 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar145 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector2.Scalar13 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector2.Scalar13 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar145 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.KVector2.Scalar23 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector2.Scalar24 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector2.Scalar13 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector2.Scalar13 * mv2.Scalar245 - 2 * mv2.Scalar145 * mv1.KVector2.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar234 * mv1.KVector2.Scalar23 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector2.Scalar23 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector2.Scalar23 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector2.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[9] += (-2 * mv2.Scalar123 * mv1.KVector2.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.KVector2.Scalar35 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.KVector2.Scalar34 * mv2.Scalar234 - 2 * mv2.Scalar145 * mv1.KVector2.Scalar34 * mv2.Scalar345 + 2 * mv2.Scalar124 * mv1.KVector2.Scalar35 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.KVector2.Scalar24 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar235 + mv2.Scalar234 * mv1.KVector2.Scalar14 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector2.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar34 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar135 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar125 - mv2.Scalar245 * mv1.KVector2.Scalar14 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector2.Scalar14 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.KVector2.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar234 * mv1.KVector2.Scalar24 * mv2.Scalar234 - mv2.Scalar345 * mv1.KVector2.Scalar24 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector2.Scalar24 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.KVector2.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[12] += (-mv2.Scalar234 * mv1.KVector2.Scalar34 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector2.Scalar34 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector2.Scalar34 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar23 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector2.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[17] += (mv2.Scalar345 * mv1.KVector2.Scalar15 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.KVector2.Scalar45 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.KVector2.Scalar35 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector2.Scalar15 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar234 + 2 * mv2.Scalar145 * mv1.KVector2.Scalar35 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector2.Scalar45 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar35 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector2.Scalar25 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.KVector2.Scalar25 * mv2.Scalar245 + mv2.Scalar245 * mv1.KVector2.Scalar15 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[18] += (mv2.Scalar235 * mv1.KVector2.Scalar25 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector2.Scalar25 * mv2.Scalar245 + 2 * mv2.Scalar245 * mv1.KVector2.Scalar35 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector2.Scalar25 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[20] += (-mv2.Scalar235 * mv1.KVector2.Scalar35 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar23 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector2.Scalar35 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector2.Scalar35 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[24] += (mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar34 * mv2.Scalar245 - mv2.Scalar234 * mv1.KVector2.Scalar45 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector2.Scalar45 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector2.Scalar45 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-2 * mv2.Scalar123 * mv1.KVector3.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar245 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector3.Scalar123 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar245 * mv2.Scalar125 + mv2.Scalar345 * mv1.KVector3.Scalar123 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.KVector3.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector3.Scalar234 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector3.Scalar123 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector3.Scalar123 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar124 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[11] += (2 * mv2.Scalar123 * mv1.KVector3.Scalar245 * mv2.Scalar235 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector3.Scalar124 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar345 * mv2.Scalar245 + mv2.Scalar234 * mv1.KVector3.Scalar124 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector3.Scalar124 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar145 - mv2.Scalar345 * mv1.KVector3.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar245 * mv1.KVector3.Scalar134 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[13] += (mv2.Scalar245 * mv1.KVector3.Scalar134 * mv2.Scalar245 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar235 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar345 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector3.Scalar134 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector3.Scalar134 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector3.Scalar234 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar135 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar135 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar123 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector3.Scalar134 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.KVector3.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector3.Scalar234 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector3.Scalar234 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector3.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[19] += (2 * mv2.Scalar123 * mv1.KVector3.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.KVector3.Scalar234 * mv2.Scalar235 + 2 * mv2.Scalar245 * mv1.KVector3.Scalar135 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar125 - mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector3.Scalar125 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector3.Scalar125 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector3.Scalar125 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[21] += (-2 * mv2.Scalar145 * mv1.KVector3.Scalar235 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar123 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector3.Scalar135 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector3.Scalar135 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.KVector3.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector3.Scalar245 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector3.Scalar135 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector3.Scalar135 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar145) * mv2NormSquaredInv;
            tempScalar[22] += (mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector3.Scalar235 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector3.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector3.Scalar235 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[25] += (-2 * mv2.Scalar124 * mv1.KVector3.Scalar234 * mv2.Scalar345 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar234 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar134 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar145 - mv2.Scalar234 * mv1.KVector3.Scalar145 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector3.Scalar145 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector3.Scalar145 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.KVector3.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar125 * mv1.KVector3.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector3.Scalar245 * mv2.Scalar235 + mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar235 - 2 * mv2.Scalar145 * mv1.KVector3.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[26] += (-mv2.Scalar234 * mv1.KVector3.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector3.Scalar245 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector3.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.KVector3.Scalar345 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[28] += (mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector3.Scalar345 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector3.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar235 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector3.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector4.Scalar1234 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector4.Scalar1234 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector4.Scalar1235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector4.Scalar1345 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector4.Scalar1234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[23] += (2 * mv2.Scalar123 * mv1.KVector4.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector4.Scalar1235 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector4.Scalar1235 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.KVector4.Scalar2345 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector4.Scalar2345 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector4.Scalar1235 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector4.Scalar1235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector4.Scalar1245 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[27] += (-2 * mv2.Scalar245 * mv1.KVector4.Scalar1345 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector4.Scalar1245 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar245 - mv2.Scalar234 * mv1.KVector4.Scalar1245 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector4.Scalar1245 * mv2.Scalar245 + mv2.Scalar235 * mv1.KVector4.Scalar1245 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[29] += (mv2.Scalar245 * mv1.KVector4.Scalar1345 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector4.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector4.Scalar1345 * mv2.Scalar234 - 2 * mv2.Scalar135 * mv1.KVector4.Scalar2345 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.KVector4.Scalar1235 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector4.Scalar1345 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector4.Scalar1345 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.KVector4.Scalar2345 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[30] += (mv2.Scalar234 * mv1.KVector4.Scalar2345 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector4.Scalar2345 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector4.Scalar2345 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector4.Scalar2345 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (-mv2.Scalar235 * mv1.KVector5.Scalar12345 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector5.Scalar12345 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector5.Scalar12345 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector5.Scalar12345 * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDirectOnDirect(this Ga401Multivector mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar2345 * mv1.KVector0.Scalar * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-2 * mv2.Scalar1234 * mv1.KVector1.Scalar5 * mv2.Scalar2345 + 2 * mv2.Scalar1235 * mv1.KVector1.Scalar4 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector1.Scalar3 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector1.Scalar2 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector1.Scalar1 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar2345 * mv1.KVector1.Scalar2 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar2345 * mv1.KVector1.Scalar3 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[8] += (mv2.Scalar2345 * mv1.KVector1.Scalar4 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[16] += (mv2.Scalar2345 * mv1.KVector1.Scalar5 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (2 * mv2.Scalar1234 * mv1.KVector2.Scalar25 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector2.Scalar24 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.KVector2.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar12 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar1234 * mv1.KVector2.Scalar35 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector2.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector2.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar13 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar2345 * mv1.KVector2.Scalar23 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar1234 * mv1.KVector2.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector2.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector2.Scalar24 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar14 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar2345 * mv1.KVector2.Scalar24 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[12] += (mv2.Scalar2345 * mv1.KVector2.Scalar34 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[17] += (2 * mv2.Scalar1235 * mv1.KVector2.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector2.Scalar35 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector2.Scalar25 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar15 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[18] += (mv2.Scalar2345 * mv1.KVector2.Scalar25 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[20] += (mv2.Scalar2345 * mv1.KVector2.Scalar35 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[24] += (mv2.Scalar2345 * mv1.KVector2.Scalar45 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-2 * mv2.Scalar1234 * mv1.KVector3.Scalar235 * mv2.Scalar2345 + 2 * mv2.Scalar1235 * mv1.KVector3.Scalar234 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector3.Scalar123 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[11] += (-2 * mv2.Scalar1234 * mv1.KVector3.Scalar245 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.KVector3.Scalar234 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector3.Scalar124 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[13] += (-2 * mv2.Scalar1234 * mv1.KVector3.Scalar345 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector3.Scalar234 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector3.Scalar134 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar2345 * mv1.KVector3.Scalar234 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[19] += (-2 * mv2.Scalar1235 * mv1.KVector3.Scalar245 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.KVector3.Scalar235 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector3.Scalar125 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[21] += (-2 * mv2.Scalar1235 * mv1.KVector3.Scalar345 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector3.Scalar235 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector3.Scalar135 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[22] += (mv2.Scalar2345 * mv1.KVector3.Scalar235 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[25] += (-2 * mv2.Scalar1245 * mv1.KVector3.Scalar345 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector3.Scalar245 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector3.Scalar145 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[26] += (mv2.Scalar2345 * mv1.KVector3.Scalar245 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[28] += (mv2.Scalar2345 * mv1.KVector3.Scalar345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (2 * mv2.Scalar1234 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1234 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[23] += (2 * mv2.Scalar1235 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1235 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[27] += (2 * mv2.Scalar1245 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1245 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[29] += (2 * mv2.Scalar1345 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1345 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[30] += (mv2.Scalar2345 * mv1.KVector4.Scalar2345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (-mv2.Scalar2345 * mv1.KVector5.Scalar12345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector ReflectDirectOnDirect(this Ga401Multivector mv1, Ga401KVector5 mv2)
    {
        return Ga401Multivector.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDirectOnDual(this Ga401KVector0 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDirectOnDual(this Ga401KVector0 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar23 * mv1.Scalar * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDirectOnDual(this Ga401KVector0 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar234 * mv1.Scalar * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDirectOnDual(this Ga401KVector0 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar2345 * mv1.Scalar * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDirectOnDual(this Ga401KVector0 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDirectOnDual(this Ga401KVector1 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (-2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar4 * mv2.Scalar4 - 2 * mv2.Scalar1 * mv1.Scalar5 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar1 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar1 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar5 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar2 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar2 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar3 = (-2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar5 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar3 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar3 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar4 = (-2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar4 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar5 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar4 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar5 = (-2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar5 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar5 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.Scalar4 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar5 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar5 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDirectOnDual(this Ga401KVector1 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar4 * mv2.Scalar24 + 2 * mv2.Scalar12 * mv1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.Scalar5 * mv2.Scalar35 + mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.Scalar2 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar1 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar1 * mv2.Scalar34 + 2 * mv2.Scalar15 * mv1.Scalar2 * mv2.Scalar25 + 2 * mv2.Scalar15 * mv1.Scalar3 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.Scalar4 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar1 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar1 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar1 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar2 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar2 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar2 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar2 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar2 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.Scalar3 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar34 * mv1.Scalar3 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar3 * mv2.Scalar35 - mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar3 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar45 * mv1.Scalar3 * mv2.Scalar45 + 2 * mv2.Scalar35 * mv1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar4 = (mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar4 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar4 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar4 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar4 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar4 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar5 = (mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar23 - mv2.Scalar45 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar5 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDirectOnDual(this Ga401KVector1 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (-2 * mv2.Scalar123 * mv1.Scalar4 * mv2.Scalar234 - 2 * mv2.Scalar123 * mv1.Scalar5 * mv2.Scalar235 - 2 * mv2.Scalar124 * mv1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.Scalar5 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.Scalar2 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar5 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar1 * mv2.Scalar234 - 2 * mv2.Scalar125 * mv1.Scalar3 * mv2.Scalar235 - 2 * mv2.Scalar125 * mv1.Scalar4 * mv2.Scalar245 - 2 * mv2.Scalar135 * mv1.Scalar2 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar4 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar1 * mv2.Scalar235 - 2 * mv2.Scalar145 * mv1.Scalar2 * mv2.Scalar245 - 2 * mv2.Scalar145 * mv1.Scalar3 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar1 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar1 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar234 * mv1.Scalar2 * mv2.Scalar234 - 2 * mv2.Scalar245 * mv1.Scalar3 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar2 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar2 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar2 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar234 * mv1.Scalar3 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar3 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar3 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar3 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar4 = (mv2.Scalar345 * mv1.Scalar4 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar4 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar5 = (mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar5 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar5 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar5 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDirectOnDual(this Ga401KVector1 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar1234 * mv1.Scalar5 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar4 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.Scalar3 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar2 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar1 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar2345 * mv1.Scalar2 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar2345 * mv1.Scalar3 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar4 = (-mv2.Scalar2345 * mv1.Scalar4 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar5 = (-mv2.Scalar2345 * mv1.Scalar5 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDirectOnDual(this Ga401KVector1 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector1.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDirectOnDual(this Ga401KVector2 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar25 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar15 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar12 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar12 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar13 = (-2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar35 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar15 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar13 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar13 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar25 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar23 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar23 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar14 = (-2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar15 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar14 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar25 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar24 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar34 = (2 * mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar35 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar34 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar15 = (-2 * mv2.Scalar1 * mv1.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar15 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar15 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.Scalar14 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar15 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar15 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar25 = (-mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar25 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.Scalar24 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar25 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar25 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar35 = (2 * mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar34 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar35 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar35 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar45 = (2 * mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar34 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar35 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar45 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar45 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDirectOnDual(this Ga401KVector2 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (2 * mv2.Scalar25 * mv1.Scalar14 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar12 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar12 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar12 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar12 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar12 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.Scalar13 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar35 * mv1.Scalar14 * mv2.Scalar45 + 2 * mv2.Scalar12 * mv1.Scalar24 * mv2.Scalar34 + 2 * mv2.Scalar12 * mv1.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.Scalar25 * mv2.Scalar25 + mv2.Scalar34 * mv1.Scalar13 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar13 * mv2.Scalar35 - mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar13 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.Scalar23 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar14 - 2 * mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar15 + mv2.Scalar45 * mv1.Scalar13 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar23 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar23 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar25 + mv2.Scalar34 * mv1.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar35 * mv1.Scalar24 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar23 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar14 = (2 * mv2.Scalar12 * mv1.Scalar34 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar25 * mv2.Scalar45 + 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.Scalar34 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.Scalar35 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.Scalar35 * mv2.Scalar35 - mv2.Scalar24 * mv1.Scalar14 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar14 * mv2.Scalar25 - 2 * mv2.Scalar15 * mv1.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar15 + mv2.Scalar45 * mv1.Scalar14 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar14 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar14 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar15) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar35 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar24 * mv2.Scalar35 - mv2.Scalar34 * mv1.Scalar24 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar24 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar34 = (mv2.Scalar25 * mv1.Scalar34 * mv2.Scalar25 + mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.Scalar23 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar34 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar34 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar34 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar15 = (2 * mv2.Scalar12 * mv1.Scalar35 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar45 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.Scalar25 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar45 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.Scalar35 * mv2.Scalar35 + 2 * mv2.Scalar14 * mv1.Scalar25 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar35 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar15 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.Scalar45 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar15 * mv2.Scalar45 - 2 * mv2.Scalar15 * mv1.Scalar25 * mv2.Scalar25 - mv2.Scalar25 * mv1.Scalar15 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar25 = (-mv2.Scalar35 * mv1.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.Scalar35 * mv2.Scalar35 + mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar25 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.Scalar45 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar34 - mv2.Scalar45 * mv1.Scalar25 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar35 = (2 * mv2.Scalar34 * mv1.Scalar24 * mv2.Scalar25 + mv2.Scalar25 * mv1.Scalar35 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar35 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar35 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar24 - mv2.Scalar35 * mv1.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.Scalar45 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.Scalar23 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar35 + mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar45 = (2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar34 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar23 - 2 * mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar45 * mv2.Scalar24 + mv2.Scalar35 * mv1.Scalar45 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar34 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar45 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar45 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar45 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDirectOnDual(this Ga401KVector2 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (mv2.Scalar234 * mv1.Scalar12 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.Scalar13 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar12 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar12 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar12 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar124 * mv1.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.Scalar23 * mv2.Scalar234 - 2 * mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar145 - 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar245 - mv2.Scalar234 * mv1.Scalar13 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar145 + 2 * mv2.Scalar125 * mv1.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.Scalar23 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar24 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar13 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar13 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar13 * mv2.Scalar245 - 2 * mv2.Scalar145 * mv1.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar234 * mv1.Scalar23 * mv2.Scalar234 + mv2.Scalar345 * mv1.Scalar23 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar245 - mv2.Scalar235 * mv1.Scalar23 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar14 = (-2 * mv2.Scalar123 * mv1.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.Scalar35 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.Scalar34 * mv2.Scalar234 - 2 * mv2.Scalar145 * mv1.Scalar34 * mv2.Scalar345 + 2 * mv2.Scalar124 * mv1.Scalar35 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.Scalar24 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar235 + mv2.Scalar234 * mv1.Scalar14 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.Scalar34 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar135 + 2 * mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar125 - mv2.Scalar345 * mv1.Scalar14 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar14 * mv2.Scalar245 - 2 * mv2.Scalar145 * mv1.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar234 * mv1.Scalar24 * mv2.Scalar234 - mv2.Scalar345 * mv1.Scalar24 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar24 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar34 = (-mv2.Scalar234 * mv1.Scalar34 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar34 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar34 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar23 * mv2.Scalar345 - mv2.Scalar345 * mv1.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar15 = (mv2.Scalar245 * mv1.Scalar15 * mv2.Scalar245 - 2 * mv2.Scalar123 * mv1.Scalar45 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.Scalar35 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar234 + 2 * mv2.Scalar145 * mv1.Scalar35 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar45 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.Scalar35 * mv2.Scalar235 + mv2.Scalar345 * mv1.Scalar15 * mv2.Scalar345 + 2 * mv2.Scalar135 * mv1.Scalar25 * mv2.Scalar235 + mv2.Scalar235 * mv1.Scalar15 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.Scalar25 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar25 = (mv2.Scalar235 * mv1.Scalar25 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar25 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar25 * mv2.Scalar345 + 2 * mv2.Scalar245 * mv1.Scalar35 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar35 = (-mv2.Scalar235 * mv1.Scalar35 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.Scalar23 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar234 + mv2.Scalar345 * mv1.Scalar35 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar35 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar45 = (mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar34 * mv2.Scalar245 - mv2.Scalar234 * mv1.Scalar45 * mv2.Scalar234 - mv2.Scalar245 * mv1.Scalar45 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar45 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDirectOnDual(this Ga401KVector2 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (2 * mv2.Scalar1234 * mv1.Scalar25 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar24 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar12 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar1234 * mv1.Scalar35 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar13 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar2345 * mv1.Scalar23 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar14 = (2 * mv2.Scalar1234 * mv1.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar24 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar14 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar2345 * mv1.Scalar24 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar34 = (mv2.Scalar2345 * mv1.Scalar34 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar15 = (2 * mv2.Scalar1235 * mv1.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar35 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar25 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar15 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar25 = (mv2.Scalar2345 * mv1.Scalar25 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar35 = (mv2.Scalar2345 * mv1.Scalar35 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar45 = (mv2.Scalar2345 * mv1.Scalar45 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDirectOnDual(this Ga401KVector2 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector2.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDirectOnDual(this Ga401KVector3 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (-2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar4 - 2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar125 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar123 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar123 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar124 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar124 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar125 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar124 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar134 = (-2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar134 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar134 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar135 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar134 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar2 * mv1.Scalar234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar234 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar234 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar235 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar234 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar125 = (2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar125 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.Scalar124 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar125 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar125 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar135 = (-2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar135 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar134 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar135 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar135 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar235 = (-mv2.Scalar2 * mv1.Scalar235 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar235 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar234 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar235 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar235 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar145 = (-2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar134 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar135 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar145 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar145 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar245 = (-mv2.Scalar2 * mv1.Scalar245 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar234 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar235 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar245 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar245 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar345 = (-2 * mv2.Scalar2 * mv1.Scalar234 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar235 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar245 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar345 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar345 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDirectOnDual(this Ga401KVector3 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (-2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar15 * mv1.Scalar234 * mv2.Scalar45 + 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.Scalar235 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar245 * mv2.Scalar15 + 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar35 + mv2.Scalar35 * mv1.Scalar123 * mv2.Scalar35 + mv2.Scalar34 * mv1.Scalar123 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar123 * mv2.Scalar25 + 2 * mv2.Scalar35 * mv1.Scalar124 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar45 + mv2.Scalar45 * mv1.Scalar123 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar124 = (-2 * mv2.Scalar12 * mv1.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.Scalar345 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar15 + 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar124 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.Scalar125 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar124 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar124 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar12 * mv1.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar25 - 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar23 + 2 * mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar15 - 2 * mv2.Scalar13 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.Scalar123 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.Scalar345 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar24 + 2 * mv2.Scalar15 * mv1.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar25 + mv2.Scalar25 * mv1.Scalar134 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar134 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar23 - mv2.Scalar35 * mv1.Scalar234 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar35 - mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar34 + mv2.Scalar24 * mv1.Scalar234 * mv2.Scalar24 + mv2.Scalar25 * mv1.Scalar234 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.Scalar235 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar125 = (-2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar125 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.Scalar145 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar125 * mv2.Scalar35 - 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar125 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar15 - mv2.Scalar45 * mv1.Scalar125 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.Scalar135 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar135 = (2 * mv2.Scalar15 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar12 * mv1.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar135 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar24 - mv2.Scalar35 * mv1.Scalar135 * mv2.Scalar35 - 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.Scalar145 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar14 - 2 * mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar25 + mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar135 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar235 = (-mv2.Scalar23 * mv1.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar35 * mv1.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar235 * mv2.Scalar34 - mv2.Scalar45 * mv1.Scalar235 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar235 * mv2.Scalar35 + mv2.Scalar25 * mv1.Scalar235 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar145 = (2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar35 + 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar15 + mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar23 + 2 * mv2.Scalar15 * mv1.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.Scalar345 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar145 * mv2.Scalar24 + mv2.Scalar35 * mv1.Scalar145 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar145 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar145 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar145 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar245 = (-mv2.Scalar24 * mv1.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar245 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar23 + mv2.Scalar25 * mv1.Scalar245 * mv2.Scalar25 - mv2.Scalar34 * mv1.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar245 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.Scalar345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar345 = (mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar235 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar345 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar345 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.Scalar235 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar345 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDirectOnDual(this Ga401KVector3 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (2 * mv2.Scalar123 * mv1.Scalar234 * mv2.Scalar234 + 2 * mv2.Scalar123 * mv1.Scalar235 * mv2.Scalar235 + 2 * mv2.Scalar123 * mv1.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.Scalar235 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.Scalar245 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar345 - mv2.Scalar345 * mv1.Scalar123 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar123 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.Scalar245 * mv2.Scalar125 + 2 * mv2.Scalar125 * mv1.Scalar234 * mv2.Scalar245 - 2 * mv2.Scalar135 * mv1.Scalar234 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar123 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar123 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.Scalar124 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar124 = (-2 * mv2.Scalar123 * mv1.Scalar245 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar345 + 2 * mv2.Scalar134 * mv1.Scalar245 * mv2.Scalar345 + 2 * mv2.Scalar134 * mv1.Scalar345 * mv2.Scalar245 + mv2.Scalar245 * mv1.Scalar124 * mv2.Scalar245 - mv2.Scalar234 * mv1.Scalar124 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar124 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar145 + mv2.Scalar345 * mv1.Scalar124 * mv2.Scalar345 + 2 * mv2.Scalar245 * mv1.Scalar134 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar123 * mv1.Scalar235 * mv2.Scalar345 + 2 * mv2.Scalar123 * mv1.Scalar345 * mv2.Scalar235 - 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.Scalar234 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.Scalar345 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar134 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar134 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar234 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar135 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar135 + 2 * mv2.Scalar235 * mv1.Scalar123 * mv2.Scalar345 + mv2.Scalar345 * mv1.Scalar134 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.Scalar234 * mv2.Scalar245 - mv2.Scalar245 * mv1.Scalar134 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar234 * mv2.Scalar235 + mv2.Scalar345 * mv1.Scalar234 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar125 = (-2 * mv2.Scalar123 * mv1.Scalar234 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.Scalar234 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar125 - mv2.Scalar235 * mv1.Scalar125 * mv2.Scalar235 + mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar234 - 2 * mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar125 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar125 * mv2.Scalar345 - 2 * mv2.Scalar245 * mv1.Scalar135 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar135 = (2 * mv2.Scalar145 * mv1.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar123 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar135 * mv2.Scalar234 - mv2.Scalar345 * mv1.Scalar135 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.Scalar245 * mv2.Scalar345 + 2 * mv2.Scalar135 * mv1.Scalar235 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar245 * mv2.Scalar245 + mv2.Scalar235 * mv1.Scalar135 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar135 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar145) * mv2NormSquaredInv,
            Scalar235 = (-mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar235 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar235 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar235 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar145 = (2 * mv2.Scalar124 * mv1.Scalar234 * mv2.Scalar345 + 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.Scalar245 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.Scalar124 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.Scalar134 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar145 + mv2.Scalar234 * mv1.Scalar145 * mv2.Scalar234 + mv2.Scalar245 * mv1.Scalar145 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar145 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar125 * mv1.Scalar345 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar245 * mv2.Scalar235 - mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar245 = (mv2.Scalar234 * mv1.Scalar245 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar245 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar245 * mv1.Scalar345 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar235) * mv2NormSquaredInv,
            Scalar345 = (-mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar345 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar345 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.Scalar235 * mv2.Scalar345 + mv2.Scalar345 * mv1.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDirectOnDual(this Ga401KVector3 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (2 * mv2.Scalar1234 * mv1.Scalar235 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar123 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar124 = (2 * mv2.Scalar1234 * mv1.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar124 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar1234 * mv1.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar134 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar2345 * mv1.Scalar234 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar125 = (2 * mv2.Scalar1235 * mv1.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar125 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar135 = (2 * mv2.Scalar1235 * mv1.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar135 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar235 = (-mv2.Scalar2345 * mv1.Scalar235 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar145 = (2 * mv2.Scalar1245 * mv1.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar245 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar145 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar245 = (-mv2.Scalar2345 * mv1.Scalar245 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar345 = (-mv2.Scalar2345 * mv1.Scalar345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDirectOnDual(this Ga401KVector3 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector3.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDirectOnDual(this Ga401KVector4 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar1235 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar1234 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1235 = (-2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar1235 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar1235 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar1235 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1235 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1245 = (2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar1245 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar1235 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1245 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1245 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1345 = (-2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar1235 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar1245 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1345 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1345 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1345 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar2345 = (-mv2.Scalar2 * mv1.Scalar2345 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar2345 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar2345 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar2345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDirectOnDual(this Ga401KVector4 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (-mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar1345 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar1234 * mv2.Scalar24 + mv2.Scalar25 * mv1.Scalar1234 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar1234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.Scalar1235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar1234 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar1235 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar1235 = (-2 * mv2.Scalar12 * mv1.Scalar2345 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.Scalar2345 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar1235 * mv2.Scalar35 - mv2.Scalar23 * mv1.Scalar1235 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar1235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar1245 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar1235 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.Scalar2345 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar1235 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar1235 * mv2.Scalar45 - 2 * mv2.Scalar35 * mv1.Scalar1245 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar1245 = (2 * mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar1245 * mv2.Scalar23 + mv2.Scalar25 * mv1.Scalar1245 * mv2.Scalar25 - mv2.Scalar24 * mv1.Scalar1245 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1245 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar1245 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar1245 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar1345 = (-2 * mv2.Scalar13 * mv1.Scalar2345 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar1235 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar1345 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar1345 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar2345 * mv2.Scalar24 + mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.Scalar2345 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.Scalar1235 * mv2.Scalar45 - mv2.Scalar25 * mv1.Scalar1345 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar1345 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar2345 = (-mv2.Scalar23 * mv1.Scalar2345 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar2345 * mv2.Scalar25 - mv2.Scalar34 * mv1.Scalar2345 * mv2.Scalar34 - mv2.Scalar45 * mv1.Scalar2345 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar2345 * mv2.Scalar24 + mv2.Scalar35 * mv1.Scalar2345 * mv2.Scalar35) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDirectOnDual(this Ga401KVector4 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (-mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar1234 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar1234 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar1235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar1345 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar1234 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar1235 = (2 * mv2.Scalar123 * mv1.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar1235 * mv2.Scalar234 - 2 * mv2.Scalar125 * mv1.Scalar2345 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar2345 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar1235 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar1235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar1245 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar1235 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar1245 = (-2 * mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar245 - mv2.Scalar234 * mv1.Scalar1245 * mv2.Scalar234 - mv2.Scalar245 * mv1.Scalar1245 * mv2.Scalar245 + mv2.Scalar235 * mv1.Scalar1245 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar1245 * mv2.Scalar345 - 2 * mv2.Scalar245 * mv1.Scalar1345 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar1345 = (2 * mv2.Scalar145 * mv1.Scalar2345 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar1345 * mv2.Scalar234 - 2 * mv2.Scalar135 * mv1.Scalar2345 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.Scalar1235 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar1345 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar1345 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar1345 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar2345 = (mv2.Scalar234 * mv1.Scalar2345 * mv2.Scalar234 + mv2.Scalar245 * mv1.Scalar2345 * mv2.Scalar245 - mv2.Scalar235 * mv1.Scalar2345 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar2345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDirectOnDual(this Ga401KVector4 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (2 * mv2.Scalar1234 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1234 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1235 = (2 * mv2.Scalar1235 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1235 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1245 = (2 * mv2.Scalar1245 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1245 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1345 = (2 * mv2.Scalar1345 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1345 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar2345 = (mv2.Scalar2345 * mv1.Scalar2345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDirectOnDual(this Ga401KVector4 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector4.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDirectOnDual(this Ga401KVector5 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (-mv2.Scalar2 * mv1.Scalar12345 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar12345 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar12345 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar12345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDirectOnDual(this Ga401KVector5 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (-mv2.Scalar23 * mv1.Scalar12345 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar12345 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar12345 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar12345 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar12345 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar12345 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDirectOnDual(this Ga401KVector5 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (-mv2.Scalar234 * mv1.Scalar12345 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar12345 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar12345 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar12345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDirectOnDual(this Ga401KVector5 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (mv2.Scalar2345 * mv1.Scalar12345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDirectOnDual(this Ga401KVector5 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector5.Zero;
    }
    
    public static Ga401Multivector ReflectDirectOnDual(this Ga401Multivector mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector0.Scalar * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector0.Scalar * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector1.Scalar4 * mv2.Scalar4 - 2 * mv2.Scalar1 * mv1.KVector1.Scalar5 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector1.Scalar1 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector1.Scalar1 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector1.Scalar5 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector1.Scalar2 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector1.Scalar2 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[4] += (-2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector1.Scalar5 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector1.Scalar3 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector1.Scalar3 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[8] += (-2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar4 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector1.Scalar5 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector1.Scalar4 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[16] += (-2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector1.Scalar5 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector1.Scalar5 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.KVector1.Scalar4 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector1.Scalar5 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector1.Scalar5 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar25 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar15 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar12 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar12 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[5] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar35 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar15 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar13 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar13 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar25 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar23 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar23 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[9] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar15 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector2.Scalar14 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar25 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector2.Scalar24 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[12] += (2 * mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector2.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar35 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector2.Scalar34 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[17] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector2.Scalar15 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector2.Scalar15 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar14 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar15 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar15 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[18] += (-mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector2.Scalar25 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar24 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar25 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar25 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[20] += (2 * mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar34 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar35 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar35 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[24] += (2 * mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar34 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar35 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar45 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar45 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar4 - 2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar125 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector3.Scalar123 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar123 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[11] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector3.Scalar124 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector3.Scalar125 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector3.Scalar124 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[13] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector3.Scalar134 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar134 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector3.Scalar135 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector3.Scalar134 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar2 * mv1.KVector3.Scalar234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector3.Scalar234 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar234 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector3.Scalar235 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector3.Scalar234 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[19] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector3.Scalar125 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.KVector3.Scalar124 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector3.Scalar125 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector3.Scalar125 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[21] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector3.Scalar135 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector3.Scalar134 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector3.Scalar135 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector3.Scalar135 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[22] += (-mv2.Scalar2 * mv1.KVector3.Scalar235 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar235 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector3.Scalar234 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector3.Scalar235 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector3.Scalar235 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[25] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar134 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar135 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector3.Scalar145 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector3.Scalar145 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[26] += (-mv2.Scalar2 * mv1.KVector3.Scalar245 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar234 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar235 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector3.Scalar245 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector3.Scalar245 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[28] += (-2 * mv2.Scalar2 * mv1.KVector3.Scalar234 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar235 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar245 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector3.Scalar345 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector3.Scalar345 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector3.Scalar345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector4.Scalar1235 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector4.Scalar1234 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[23] += (-2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector4.Scalar1235 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector4.Scalar1235 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector4.Scalar1235 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar1235 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[27] += (2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector4.Scalar1245 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector4.Scalar1235 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector4.Scalar1245 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar1245 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[29] += (-2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector4.Scalar1235 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1245 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector4.Scalar1345 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector4.Scalar1345 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar1345 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[30] += (-mv2.Scalar2 * mv1.KVector4.Scalar2345 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector4.Scalar2345 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector4.Scalar2345 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar2345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (-mv2.Scalar2 * mv1.KVector5.Scalar12345 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector5.Scalar12345 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector5.Scalar12345 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector5.Scalar12345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDirectOnDual(this Ga401Multivector mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23 + mv2.Scalar45 * mv1.KVector0.Scalar * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector0.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector0.Scalar * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector0.Scalar * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector0.Scalar * mv2.Scalar35) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar4 * mv2.Scalar24 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar15 * mv1.KVector1.Scalar2 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar5 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar2 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar5 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector1.Scalar1 * mv2.Scalar24 + mv2.Scalar45 * mv1.KVector1.Scalar1 * mv2.Scalar45 + mv2.Scalar34 * mv1.KVector1.Scalar1 * mv2.Scalar34 + 2 * mv2.Scalar15 * mv1.KVector1.Scalar3 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.KVector1.Scalar4 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector1.Scalar1 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector1.Scalar1 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[2] += (2 * mv2.Scalar25 * mv1.KVector1.Scalar4 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector1.Scalar2 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector1.Scalar2 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector1.Scalar2 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector1.Scalar2 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector1.Scalar2 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.KVector1.Scalar3 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar45 * mv1.KVector1.Scalar3 * mv2.Scalar45 + mv2.Scalar34 * mv1.KVector1.Scalar3 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector1.Scalar3 * mv2.Scalar35 - mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector1.Scalar3 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar45 + 2 * mv2.Scalar35 * mv1.KVector1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[8] += (mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector1.Scalar4 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector1.Scalar4 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector1.Scalar4 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector1.Scalar4 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector1.Scalar4 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[16] += (-mv2.Scalar45 * mv1.KVector1.Scalar5 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector1.Scalar5 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector1.Scalar5 * mv2.Scalar25 + mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar34) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector2.Scalar12 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector2.Scalar12 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector2.Scalar12 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector2.Scalar12 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector2.Scalar12 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar13 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar14 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar24 * mv2.Scalar34 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar25 * mv2.Scalar25 - mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector2.Scalar13 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar23 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar14 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar15 + mv2.Scalar34 * mv1.KVector2.Scalar13 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar45 + mv2.Scalar35 * mv1.KVector2.Scalar13 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector2.Scalar13 * mv2.Scalar45 + 2 * mv2.Scalar35 * mv1.KVector2.Scalar14 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector2.Scalar23 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector2.Scalar23 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector2.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar25 + mv2.Scalar34 * mv1.KVector2.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar35 * mv1.KVector2.Scalar24 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar45 + mv2.Scalar35 * mv1.KVector2.Scalar23 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar34 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar25 * mv2.Scalar45 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar34 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar35 * mv2.Scalar45 + mv2.Scalar45 * mv1.KVector2.Scalar14 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar35 * mv2.Scalar35 - mv2.Scalar24 * mv1.KVector2.Scalar14 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector2.Scalar14 * mv2.Scalar25 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar15 - mv2.Scalar34 * mv1.KVector2.Scalar14 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector2.Scalar14 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar15) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar35 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector2.Scalar24 * mv2.Scalar35 - mv2.Scalar34 * mv1.KVector2.Scalar24 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector2.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector2.Scalar24 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[12] += (mv2.Scalar25 * mv1.KVector2.Scalar34 * mv2.Scalar25 + mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar23 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector2.Scalar34 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector2.Scalar34 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector2.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector2.Scalar34 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[17] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar35 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar45 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar25 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar45 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector2.Scalar15 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar35 * mv2.Scalar35 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar25 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar35 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector2.Scalar15 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar45 * mv2.Scalar45 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar25 * mv2.Scalar25 - mv2.Scalar25 * mv1.KVector2.Scalar15 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[18] += (-mv2.Scalar35 * mv1.KVector2.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar35 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector2.Scalar25 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar45 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector2.Scalar25 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[20] += (2 * mv2.Scalar34 * mv1.KVector2.Scalar24 * mv2.Scalar25 + mv2.Scalar25 * mv1.KVector2.Scalar35 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector2.Scalar35 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector2.Scalar35 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector2.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.KVector2.Scalar45 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar23 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[24] += (2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar34 * mv2.Scalar25 + mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar23 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector2.Scalar45 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector2.Scalar45 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar34 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector2.Scalar45 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector2.Scalar45 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector2.Scalar45 * mv2.Scalar45) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar45 * mv1.KVector3.Scalar123 * mv2.Scalar45 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar235 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar245 * mv2.Scalar15 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar35 + mv2.Scalar35 * mv1.KVector3.Scalar123 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector3.Scalar123 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector3.Scalar123 * mv2.Scalar25 + 2 * mv2.Scalar35 * mv1.KVector3.Scalar124 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar45 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar234 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[11] += (mv2.Scalar45 * mv1.KVector3.Scalar124 * mv2.Scalar45 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar345 * mv2.Scalar25 + mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar15 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector3.Scalar124 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar125 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector3.Scalar124 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar12 * mv1.KVector3.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar25 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar23 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar15 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.KVector3.Scalar123 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar345 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar24 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar25 + mv2.Scalar25 * mv1.KVector3.Scalar134 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector3.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector3.Scalar134 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar35 - mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector3.Scalar234 * mv2.Scalar24 + mv2.Scalar25 * mv1.KVector3.Scalar234 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector3.Scalar234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar235 * mv2.Scalar35 - mv2.Scalar35 * mv1.KVector3.Scalar234 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[19] += (-2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector3.Scalar125 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector3.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar145 * mv2.Scalar45 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector3.Scalar125 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar15 - mv2.Scalar35 * mv1.KVector3.Scalar125 * mv2.Scalar35 - mv2.Scalar45 * mv1.KVector3.Scalar125 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar135 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[21] += (-mv2.Scalar45 * mv1.KVector3.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector3.Scalar135 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector3.Scalar135 * mv2.Scalar35 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.KVector3.Scalar145 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar14 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar25 + mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector3.Scalar135 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[22] += (-mv2.Scalar23 * mv1.KVector3.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar35 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector3.Scalar235 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector3.Scalar235 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector3.Scalar235 * mv2.Scalar35 + mv2.Scalar25 * mv1.KVector3.Scalar235 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[25] += (2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar35 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar15 + mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar23 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar345 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector3.Scalar145 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector3.Scalar145 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector3.Scalar145 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector3.Scalar145 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector3.Scalar145 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[26] += (2 * mv2.Scalar25 * mv1.KVector3.Scalar345 * mv2.Scalar35 - mv2.Scalar24 * mv1.KVector3.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector3.Scalar245 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar25 + mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar23 + mv2.Scalar25 * mv1.KVector3.Scalar245 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector3.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[28] += (mv2.Scalar35 * mv1.KVector3.Scalar345 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar235 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector3.Scalar345 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector3.Scalar345 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar235 * mv2.Scalar45 - mv2.Scalar45 * mv1.KVector3.Scalar345 * mv2.Scalar45) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector4.Scalar1345 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector4.Scalar1234 * mv2.Scalar24 + mv2.Scalar25 * mv1.KVector4.Scalar1234 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector4.Scalar1234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.KVector4.Scalar1235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector4.Scalar1234 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector4.Scalar1235 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[23] += (-2 * mv2.Scalar12 * mv1.KVector4.Scalar2345 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.KVector4.Scalar2345 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector4.Scalar1235 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector4.Scalar1235 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.KVector4.Scalar1245 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.KVector4.Scalar1245 * mv2.Scalar24 - mv2.Scalar45 * mv1.KVector4.Scalar1235 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector4.Scalar1235 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector4.Scalar1235 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.KVector4.Scalar2345 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector4.Scalar1235 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[27] += (2 * mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector4.Scalar1245 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector4.Scalar1245 * mv2.Scalar23 + mv2.Scalar25 * mv1.KVector4.Scalar1245 * mv2.Scalar25 - mv2.Scalar24 * mv1.KVector4.Scalar1245 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector4.Scalar1245 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar45 - mv2.Scalar45 * mv1.KVector4.Scalar1245 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.KVector4.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[29] += (mv2.Scalar35 * mv1.KVector4.Scalar1345 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector4.Scalar2345 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector4.Scalar1235 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector4.Scalar1345 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector4.Scalar1345 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector4.Scalar2345 * mv2.Scalar24 + mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.KVector4.Scalar2345 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.KVector4.Scalar1235 * mv2.Scalar45 - mv2.Scalar25 * mv1.KVector4.Scalar1345 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector4.Scalar1345 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[30] += (-mv2.Scalar23 * mv1.KVector4.Scalar2345 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector4.Scalar2345 * mv2.Scalar25 - mv2.Scalar34 * mv1.KVector4.Scalar2345 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector4.Scalar2345 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector4.Scalar2345 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector4.Scalar2345 * mv2.Scalar35) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (-mv2.Scalar34 * mv1.KVector5.Scalar12345 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector5.Scalar12345 * mv2.Scalar35 - mv2.Scalar23 * mv1.KVector5.Scalar12345 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector5.Scalar12345 * mv2.Scalar24 - mv2.Scalar45 * mv1.KVector5.Scalar12345 * mv2.Scalar45 - mv2.Scalar25 * mv1.KVector5.Scalar12345 * mv2.Scalar25) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDirectOnDual(this Ga401Multivector mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar345 * mv1.KVector0.Scalar * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector0.Scalar * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector0.Scalar * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector0.Scalar * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (-2 * mv2.Scalar123 * mv1.KVector1.Scalar4 * mv2.Scalar234 - 2 * mv2.Scalar123 * mv1.KVector1.Scalar5 * mv2.Scalar235 - 2 * mv2.Scalar124 * mv1.KVector1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.KVector1.Scalar5 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector1.Scalar2 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector1.Scalar5 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector1.Scalar1 * mv2.Scalar234 - 2 * mv2.Scalar125 * mv1.KVector1.Scalar3 * mv2.Scalar235 - 2 * mv2.Scalar125 * mv1.KVector1.Scalar4 * mv2.Scalar245 - 2 * mv2.Scalar135 * mv1.KVector1.Scalar2 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector1.Scalar4 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector1.Scalar1 * mv2.Scalar235 - 2 * mv2.Scalar145 * mv1.KVector1.Scalar2 * mv2.Scalar245 - 2 * mv2.Scalar145 * mv1.KVector1.Scalar3 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector1.Scalar1 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector1.Scalar1 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar234 * mv1.KVector1.Scalar2 * mv2.Scalar234 - 2 * mv2.Scalar245 * mv1.KVector1.Scalar3 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector1.Scalar2 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector1.Scalar2 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector1.Scalar2 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar234 * mv1.KVector1.Scalar3 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector1.Scalar3 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector1.Scalar3 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector1.Scalar3 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[8] += (mv2.Scalar345 * mv1.KVector1.Scalar4 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector1.Scalar4 * mv2.Scalar245 - mv2.Scalar234 * mv1.KVector1.Scalar4 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[16] += (-mv2.Scalar235 * mv1.KVector1.Scalar5 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector1.Scalar5 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector1.Scalar5 * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar234 * mv1.KVector2.Scalar12 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.KVector2.Scalar13 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector2.Scalar12 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector2.Scalar12 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector2.Scalar12 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar124 * mv1.KVector2.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar23 * mv2.Scalar234 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar145 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector2.Scalar13 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector2.Scalar13 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar145 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.KVector2.Scalar23 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector2.Scalar24 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector2.Scalar13 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector2.Scalar13 * mv2.Scalar245 - 2 * mv2.Scalar145 * mv1.KVector2.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar234 * mv1.KVector2.Scalar23 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector2.Scalar23 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector2.Scalar23 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector2.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[9] += (-2 * mv2.Scalar123 * mv1.KVector2.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.KVector2.Scalar35 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.KVector2.Scalar34 * mv2.Scalar234 - 2 * mv2.Scalar145 * mv1.KVector2.Scalar34 * mv2.Scalar345 + 2 * mv2.Scalar124 * mv1.KVector2.Scalar35 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.KVector2.Scalar24 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar235 + mv2.Scalar234 * mv1.KVector2.Scalar14 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector2.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar34 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar135 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar125 - mv2.Scalar245 * mv1.KVector2.Scalar14 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector2.Scalar14 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.KVector2.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar234 * mv1.KVector2.Scalar24 * mv2.Scalar234 - mv2.Scalar345 * mv1.KVector2.Scalar24 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector2.Scalar24 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.KVector2.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[12] += (-mv2.Scalar234 * mv1.KVector2.Scalar34 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector2.Scalar34 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector2.Scalar34 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar23 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector2.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[17] += (mv2.Scalar345 * mv1.KVector2.Scalar15 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.KVector2.Scalar45 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.KVector2.Scalar35 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector2.Scalar15 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar234 + 2 * mv2.Scalar145 * mv1.KVector2.Scalar35 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector2.Scalar45 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar35 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector2.Scalar25 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.KVector2.Scalar25 * mv2.Scalar245 + mv2.Scalar245 * mv1.KVector2.Scalar15 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[18] += (mv2.Scalar235 * mv1.KVector2.Scalar25 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector2.Scalar25 * mv2.Scalar245 + 2 * mv2.Scalar245 * mv1.KVector2.Scalar35 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector2.Scalar25 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[20] += (-mv2.Scalar235 * mv1.KVector2.Scalar35 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar23 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector2.Scalar35 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector2.Scalar35 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[24] += (mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar34 * mv2.Scalar245 - mv2.Scalar234 * mv1.KVector2.Scalar45 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector2.Scalar45 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector2.Scalar45 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (2 * mv2.Scalar123 * mv1.KVector3.Scalar234 * mv2.Scalar234 + 2 * mv2.Scalar123 * mv1.KVector3.Scalar235 * mv2.Scalar235 + 2 * mv2.Scalar123 * mv1.KVector3.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.KVector3.Scalar235 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.KVector3.Scalar245 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector3.Scalar123 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar245 * mv2.Scalar125 - mv2.Scalar345 * mv1.KVector3.Scalar123 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.KVector3.Scalar234 * mv2.Scalar245 - 2 * mv2.Scalar135 * mv1.KVector3.Scalar234 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector3.Scalar123 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector3.Scalar123 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.KVector3.Scalar124 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[11] += (-2 * mv2.Scalar123 * mv1.KVector3.Scalar245 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.KVector3.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector3.Scalar124 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar245 * mv2.Scalar345 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar345 * mv2.Scalar245 - mv2.Scalar234 * mv1.KVector3.Scalar124 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector3.Scalar124 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar145 + mv2.Scalar345 * mv1.KVector3.Scalar124 * mv2.Scalar345 + 2 * mv2.Scalar245 * mv1.KVector3.Scalar134 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[13] += (-mv2.Scalar245 * mv1.KVector3.Scalar134 * mv2.Scalar245 + 2 * mv2.Scalar123 * mv1.KVector3.Scalar235 * mv2.Scalar345 + 2 * mv2.Scalar123 * mv1.KVector3.Scalar345 * mv2.Scalar235 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar234 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar345 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector3.Scalar134 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector3.Scalar134 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector3.Scalar234 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar135 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar135 + 2 * mv2.Scalar235 * mv1.KVector3.Scalar123 * mv2.Scalar345 + mv2.Scalar345 * mv1.KVector3.Scalar134 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.KVector3.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector3.Scalar234 * mv2.Scalar235 + mv2.Scalar345 * mv1.KVector3.Scalar234 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector3.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[19] += (-2 * mv2.Scalar123 * mv1.KVector3.Scalar234 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar234 * mv2.Scalar235 - 2 * mv2.Scalar245 * mv1.KVector3.Scalar135 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar125 + mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector3.Scalar125 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector3.Scalar125 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector3.Scalar125 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[21] += (2 * mv2.Scalar145 * mv1.KVector3.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar123 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector3.Scalar135 * mv2.Scalar234 - mv2.Scalar345 * mv1.KVector3.Scalar135 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.KVector3.Scalar245 * mv2.Scalar345 + 2 * mv2.Scalar135 * mv1.KVector3.Scalar235 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector3.Scalar245 * mv2.Scalar245 + mv2.Scalar235 * mv1.KVector3.Scalar135 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector3.Scalar135 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar145) * mv2NormSquaredInv;
            tempScalar[22] += (-mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector3.Scalar235 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector3.Scalar235 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector3.Scalar235 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[25] += (2 * mv2.Scalar124 * mv1.KVector3.Scalar234 * mv2.Scalar345 + 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar245 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar124 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar134 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar145 + mv2.Scalar234 * mv1.KVector3.Scalar145 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector3.Scalar145 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector3.Scalar145 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.KVector3.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar125 * mv1.KVector3.Scalar345 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector3.Scalar245 * mv2.Scalar235 - mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.KVector3.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[26] += (mv2.Scalar234 * mv1.KVector3.Scalar245 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector3.Scalar245 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector3.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar245 * mv1.KVector3.Scalar345 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[28] += (-mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector3.Scalar345 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector3.Scalar345 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.KVector3.Scalar235 * mv2.Scalar345 + mv2.Scalar345 * mv1.KVector3.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector4.Scalar1234 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector4.Scalar1234 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector4.Scalar1235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector4.Scalar1345 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector4.Scalar1234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[23] += (2 * mv2.Scalar123 * mv1.KVector4.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector4.Scalar1235 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector4.Scalar1235 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.KVector4.Scalar2345 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector4.Scalar2345 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector4.Scalar1235 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector4.Scalar1235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector4.Scalar1245 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[27] += (-2 * mv2.Scalar245 * mv1.KVector4.Scalar1345 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector4.Scalar1245 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar245 - mv2.Scalar234 * mv1.KVector4.Scalar1245 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector4.Scalar1245 * mv2.Scalar245 + mv2.Scalar235 * mv1.KVector4.Scalar1245 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[29] += (mv2.Scalar245 * mv1.KVector4.Scalar1345 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector4.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector4.Scalar1345 * mv2.Scalar234 - 2 * mv2.Scalar135 * mv1.KVector4.Scalar2345 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.KVector4.Scalar1235 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector4.Scalar1345 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector4.Scalar1345 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.KVector4.Scalar2345 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[30] += (mv2.Scalar234 * mv1.KVector4.Scalar2345 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector4.Scalar2345 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector4.Scalar2345 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector4.Scalar2345 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (mv2.Scalar235 * mv1.KVector5.Scalar12345 * mv2.Scalar235 + mv2.Scalar345 * mv1.KVector5.Scalar12345 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector5.Scalar12345 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector5.Scalar12345 * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDirectOnDual(this Ga401Multivector mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar2345 * mv1.KVector0.Scalar * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar1234 * mv1.KVector1.Scalar5 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector1.Scalar4 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.KVector1.Scalar3 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector1.Scalar2 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector1.Scalar1 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar2345 * mv1.KVector1.Scalar2 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar2345 * mv1.KVector1.Scalar3 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[8] += (-mv2.Scalar2345 * mv1.KVector1.Scalar4 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[16] += (-mv2.Scalar2345 * mv1.KVector1.Scalar5 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (2 * mv2.Scalar1234 * mv1.KVector2.Scalar25 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector2.Scalar24 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.KVector2.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar12 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar1234 * mv1.KVector2.Scalar35 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector2.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector2.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar13 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar2345 * mv1.KVector2.Scalar23 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar1234 * mv1.KVector2.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector2.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector2.Scalar24 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar14 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar2345 * mv1.KVector2.Scalar24 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[12] += (mv2.Scalar2345 * mv1.KVector2.Scalar34 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[17] += (2 * mv2.Scalar1235 * mv1.KVector2.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector2.Scalar35 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector2.Scalar25 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar15 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[18] += (mv2.Scalar2345 * mv1.KVector2.Scalar25 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[20] += (mv2.Scalar2345 * mv1.KVector2.Scalar35 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[24] += (mv2.Scalar2345 * mv1.KVector2.Scalar45 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (2 * mv2.Scalar1234 * mv1.KVector3.Scalar235 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector3.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar123 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[11] += (2 * mv2.Scalar1234 * mv1.KVector3.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector3.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar124 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar1234 * mv1.KVector3.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector3.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar134 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar2345 * mv1.KVector3.Scalar234 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[19] += (2 * mv2.Scalar1235 * mv1.KVector3.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector3.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar125 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[21] += (2 * mv2.Scalar1235 * mv1.KVector3.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector3.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar135 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[22] += (-mv2.Scalar2345 * mv1.KVector3.Scalar235 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[25] += (2 * mv2.Scalar1245 * mv1.KVector3.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector3.Scalar245 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar145 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[26] += (-mv2.Scalar2345 * mv1.KVector3.Scalar245 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[28] += (-mv2.Scalar2345 * mv1.KVector3.Scalar345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (2 * mv2.Scalar1234 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1234 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[23] += (2 * mv2.Scalar1235 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1235 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[27] += (2 * mv2.Scalar1245 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1245 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[29] += (2 * mv2.Scalar1345 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1345 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[30] += (mv2.Scalar2345 * mv1.KVector4.Scalar2345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (mv2.Scalar2345 * mv1.KVector5.Scalar12345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector ReflectDirectOnDual(this Ga401Multivector mv1, Ga401KVector5 mv2)
    {
        return Ga401Multivector.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDualOnDirect(this Ga401KVector0 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDualOnDirect(this Ga401KVector0 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (-mv2.Scalar23 * mv1.Scalar * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDualOnDirect(this Ga401KVector0 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar234 * mv1.Scalar * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDualOnDirect(this Ga401KVector0 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (-mv2.Scalar2345 * mv1.Scalar * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDualOnDirect(this Ga401KVector0 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDualOnDirect(this Ga401KVector1 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar2 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar2 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar3 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar3 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar3 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar4 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar4 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar5 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar5 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar5 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.Scalar4 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar5 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar5 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDualOnDirect(this Ga401KVector1 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar4 * mv2.Scalar24 + 2 * mv2.Scalar12 * mv1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.Scalar5 * mv2.Scalar35 + mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.Scalar2 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar1 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar1 * mv2.Scalar34 + 2 * mv2.Scalar15 * mv1.Scalar2 * mv2.Scalar25 + 2 * mv2.Scalar15 * mv1.Scalar3 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.Scalar4 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar1 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar1 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar1 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar2 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar2 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar2 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar2 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar2 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.Scalar3 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar34 * mv1.Scalar3 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar3 * mv2.Scalar35 - mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar3 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar45 * mv1.Scalar3 * mv2.Scalar45 + 2 * mv2.Scalar35 * mv1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar4 = (mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar4 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar4 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar4 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar4 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar4 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar5 = (mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar23 - mv2.Scalar45 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar5 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDualOnDirect(this Ga401KVector1 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar123 * mv1.Scalar4 * mv2.Scalar234 + 2 * mv2.Scalar123 * mv1.Scalar5 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.Scalar3 * mv2.Scalar234 + 2 * mv2.Scalar124 * mv1.Scalar5 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.Scalar5 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar1 * mv2.Scalar234 + 2 * mv2.Scalar125 * mv1.Scalar3 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.Scalar4 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar4 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar1 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.Scalar2 * mv2.Scalar245 + 2 * mv2.Scalar145 * mv1.Scalar3 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar1 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar1 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar234 * mv1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.Scalar3 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar2 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar2 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar234 * mv1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar3 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar3 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar3 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar4 = (-mv2.Scalar345 * mv1.Scalar4 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar4 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar5 = (-mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar5 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar5 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar5 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDualOnDirect(this Ga401KVector1 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar1234 * mv1.Scalar5 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar4 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.Scalar3 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar2 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar1 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar2345 * mv1.Scalar2 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar2345 * mv1.Scalar3 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar4 = (-mv2.Scalar2345 * mv1.Scalar4 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar5 = (-mv2.Scalar2345 * mv1.Scalar5 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDualOnDirect(this Ga401KVector1 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector1.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDualOnDirect(this Ga401KVector2 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar25 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar15 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar12 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar12 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar13 = (-2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar35 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar15 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar13 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar13 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar25 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar23 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar23 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar14 = (-2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar15 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar14 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar25 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar24 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar34 = (2 * mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar35 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar34 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar15 = (-2 * mv2.Scalar1 * mv1.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar15 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar15 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.Scalar14 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar15 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar15 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar25 = (-mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar25 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.Scalar24 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar25 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar25 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar35 = (2 * mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar34 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar35 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar35 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar45 = (2 * mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar34 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar35 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar45 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar45 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDualOnDirect(this Ga401KVector2 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (-2 * mv2.Scalar25 * mv1.Scalar14 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar34 - 2 * mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar35 - mv2.Scalar24 * mv1.Scalar12 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar12 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar45 - mv2.Scalar25 * mv1.Scalar12 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar12 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar12 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.Scalar13 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar13 = (-2 * mv2.Scalar35 * mv1.Scalar14 * mv2.Scalar45 - 2 * mv2.Scalar12 * mv1.Scalar24 * mv2.Scalar34 - 2 * mv2.Scalar12 * mv1.Scalar25 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.Scalar25 * mv2.Scalar25 - mv2.Scalar34 * mv1.Scalar13 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar13 * mv2.Scalar35 + mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar24 + mv2.Scalar25 * mv1.Scalar13 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.Scalar23 * mv2.Scalar24 + 2 * mv2.Scalar15 * mv1.Scalar23 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar14 + 2 * mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar15 - mv2.Scalar45 * mv1.Scalar13 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 + mv2.Scalar25 * mv1.Scalar23 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar23 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar23 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar25 - mv2.Scalar34 * mv1.Scalar23 * mv2.Scalar34 - 2 * mv2.Scalar35 * mv1.Scalar24 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar23 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar14 = (-2 * mv2.Scalar12 * mv1.Scalar34 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.Scalar25 * mv2.Scalar45 - 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar23 + 2 * mv2.Scalar15 * mv1.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.Scalar35 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.Scalar34 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar24 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar25 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.Scalar35 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar14 * mv2.Scalar24 + mv2.Scalar25 * mv1.Scalar14 * mv2.Scalar25 + 2 * mv2.Scalar15 * mv1.Scalar24 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar15 - mv2.Scalar45 * mv1.Scalar14 * mv2.Scalar45 + mv2.Scalar34 * mv1.Scalar14 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar14 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar15) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar24 + 2 * mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar35 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar24 * mv2.Scalar35 + mv2.Scalar34 * mv1.Scalar24 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar24 * mv2.Scalar25 + 2 * mv2.Scalar25 * mv1.Scalar34 * mv2.Scalar35 + 2 * mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar24 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar34 = (-mv2.Scalar25 * mv1.Scalar34 * mv2.Scalar25 - mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.Scalar23 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar34 * mv2.Scalar23 + mv2.Scalar34 * mv1.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar34 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar34 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar15 = (-2 * mv2.Scalar12 * mv1.Scalar35 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.Scalar45 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.Scalar25 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar45 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar23 + 2 * mv2.Scalar15 * mv1.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar14 * mv1.Scalar25 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.Scalar35 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar15 * mv2.Scalar35 - mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar34 + 2 * mv2.Scalar15 * mv1.Scalar45 * mv2.Scalar45 + mv2.Scalar45 * mv1.Scalar15 * mv2.Scalar45 + 2 * mv2.Scalar15 * mv1.Scalar25 * mv2.Scalar25 + mv2.Scalar25 * mv1.Scalar15 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar25 = (mv2.Scalar35 * mv1.Scalar25 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.Scalar35 * mv2.Scalar35 - mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar23 + mv2.Scalar25 * mv1.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.Scalar45 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar34 + mv2.Scalar45 * mv1.Scalar25 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar35 = (-2 * mv2.Scalar34 * mv1.Scalar24 * mv2.Scalar25 - mv2.Scalar25 * mv1.Scalar35 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar35 * mv2.Scalar45 + 2 * mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar35 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar24 + mv2.Scalar35 * mv1.Scalar35 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar24 + 2 * mv2.Scalar35 * mv1.Scalar45 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.Scalar23 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar35 - mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar45 = (-2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar34 * mv2.Scalar25 - mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar23 + 2 * mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar45 * mv2.Scalar24 - mv2.Scalar35 * mv1.Scalar45 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar34 * mv2.Scalar45 + mv2.Scalar34 * mv1.Scalar45 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar45 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar45 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDualOnDirect(this Ga401KVector2 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (mv2.Scalar234 * mv1.Scalar12 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.Scalar13 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar12 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar12 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar12 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar124 * mv1.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.Scalar23 * mv2.Scalar234 - 2 * mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar145 - 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar245 - mv2.Scalar234 * mv1.Scalar13 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar145 + 2 * mv2.Scalar125 * mv1.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.Scalar23 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar24 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar13 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar13 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar13 * mv2.Scalar245 - 2 * mv2.Scalar145 * mv1.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar234 * mv1.Scalar23 * mv2.Scalar234 + mv2.Scalar345 * mv1.Scalar23 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar245 - mv2.Scalar235 * mv1.Scalar23 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar14 = (-2 * mv2.Scalar123 * mv1.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.Scalar35 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.Scalar34 * mv2.Scalar234 - 2 * mv2.Scalar145 * mv1.Scalar34 * mv2.Scalar345 + 2 * mv2.Scalar124 * mv1.Scalar35 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.Scalar24 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar235 + mv2.Scalar234 * mv1.Scalar14 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.Scalar34 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar135 + 2 * mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar125 - mv2.Scalar345 * mv1.Scalar14 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar14 * mv2.Scalar245 - 2 * mv2.Scalar145 * mv1.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar234 * mv1.Scalar24 * mv2.Scalar234 - mv2.Scalar345 * mv1.Scalar24 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar24 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar34 = (-mv2.Scalar234 * mv1.Scalar34 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar34 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar34 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar23 * mv2.Scalar345 - mv2.Scalar345 * mv1.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar15 = (mv2.Scalar245 * mv1.Scalar15 * mv2.Scalar245 - 2 * mv2.Scalar123 * mv1.Scalar45 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.Scalar35 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar234 + 2 * mv2.Scalar145 * mv1.Scalar35 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar45 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.Scalar35 * mv2.Scalar235 + mv2.Scalar345 * mv1.Scalar15 * mv2.Scalar345 + 2 * mv2.Scalar135 * mv1.Scalar25 * mv2.Scalar235 + mv2.Scalar235 * mv1.Scalar15 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.Scalar25 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar25 = (mv2.Scalar235 * mv1.Scalar25 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar25 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar25 * mv2.Scalar345 + 2 * mv2.Scalar245 * mv1.Scalar35 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar35 = (-mv2.Scalar235 * mv1.Scalar35 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.Scalar23 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar234 + mv2.Scalar345 * mv1.Scalar35 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar35 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar45 = (mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar34 * mv2.Scalar245 - mv2.Scalar234 * mv1.Scalar45 * mv2.Scalar234 - mv2.Scalar245 * mv1.Scalar45 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar45 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDualOnDirect(this Ga401KVector2 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (-2 * mv2.Scalar1234 * mv1.Scalar25 * mv2.Scalar2345 + 2 * mv2.Scalar1235 * mv1.Scalar24 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar23 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar12 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar13 = (-2 * mv2.Scalar1234 * mv1.Scalar35 * mv2.Scalar2345 + 2 * mv2.Scalar1235 * mv1.Scalar34 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar23 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar13 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar2345 * mv1.Scalar23 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar14 = (-2 * mv2.Scalar1234 * mv1.Scalar45 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.Scalar34 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar24 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar14 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar2345 * mv1.Scalar24 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar34 = (-mv2.Scalar2345 * mv1.Scalar34 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar15 = (-2 * mv2.Scalar1235 * mv1.Scalar45 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.Scalar35 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar25 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar15 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar25 = (-mv2.Scalar2345 * mv1.Scalar25 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar35 = (-mv2.Scalar2345 * mv1.Scalar35 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar45 = (-mv2.Scalar2345 * mv1.Scalar45 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDualOnDirect(this Ga401KVector2 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector2.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDualOnDirect(this Ga401KVector3 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar125 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar123 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar123 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar124 = (-2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar125 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar124 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar134 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar135 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar134 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar2 * mv1.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar234 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar235 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar234 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar125 = (-2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar125 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.Scalar124 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar125 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar125 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar135 = (2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar134 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar135 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar135 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar235 = (mv2.Scalar2 * mv1.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar235 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar234 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar235 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar235 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar145 = (2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar134 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar135 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar145 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar145 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar245 = (mv2.Scalar2 * mv1.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar234 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar235 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar245 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar245 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar345 = (2 * mv2.Scalar2 * mv1.Scalar234 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar235 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar245 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar345 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar345 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDualOnDirect(this Ga401KVector3 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (-2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar15 * mv1.Scalar234 * mv2.Scalar45 + 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.Scalar235 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar245 * mv2.Scalar15 + 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar35 + mv2.Scalar35 * mv1.Scalar123 * mv2.Scalar35 + mv2.Scalar34 * mv1.Scalar123 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar123 * mv2.Scalar25 + 2 * mv2.Scalar35 * mv1.Scalar124 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar45 + mv2.Scalar45 * mv1.Scalar123 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar124 = (-2 * mv2.Scalar12 * mv1.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.Scalar345 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar15 + 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar124 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.Scalar125 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar124 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar124 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar12 * mv1.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar25 - 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar23 + 2 * mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar15 - 2 * mv2.Scalar13 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.Scalar123 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.Scalar345 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar24 + 2 * mv2.Scalar15 * mv1.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar25 + mv2.Scalar25 * mv1.Scalar134 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar134 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar23 - mv2.Scalar35 * mv1.Scalar234 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar35 - mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar34 + mv2.Scalar24 * mv1.Scalar234 * mv2.Scalar24 + mv2.Scalar25 * mv1.Scalar234 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.Scalar235 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar125 = (-2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar125 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.Scalar145 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar125 * mv2.Scalar35 - 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar125 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar15 - mv2.Scalar45 * mv1.Scalar125 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.Scalar135 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar135 = (2 * mv2.Scalar15 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar12 * mv1.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar135 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar24 - mv2.Scalar35 * mv1.Scalar135 * mv2.Scalar35 - 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.Scalar145 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar14 - 2 * mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar25 + mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar135 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar235 = (-mv2.Scalar23 * mv1.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar35 * mv1.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar235 * mv2.Scalar34 - mv2.Scalar45 * mv1.Scalar235 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar235 * mv2.Scalar35 + mv2.Scalar25 * mv1.Scalar235 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar145 = (2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar35 + 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar15 + mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar23 + 2 * mv2.Scalar15 * mv1.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.Scalar345 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar145 * mv2.Scalar24 + mv2.Scalar35 * mv1.Scalar145 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar145 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar145 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar145 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar245 = (-mv2.Scalar24 * mv1.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar245 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar23 + mv2.Scalar25 * mv1.Scalar245 * mv2.Scalar25 - mv2.Scalar34 * mv1.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar245 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.Scalar345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar345 = (mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar235 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar345 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar345 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.Scalar235 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar345 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDualOnDirect(this Ga401KVector3 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (-2 * mv2.Scalar123 * mv1.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar123 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar123 * mv1.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.Scalar245 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar345 + mv2.Scalar345 * mv1.Scalar123 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar123 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.Scalar245 * mv2.Scalar125 - 2 * mv2.Scalar125 * mv1.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar234 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar123 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar123 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar124 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar124 = (2 * mv2.Scalar123 * mv1.Scalar245 * mv2.Scalar235 - 2 * mv2.Scalar124 * mv1.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.Scalar345 * mv2.Scalar245 - mv2.Scalar245 * mv1.Scalar124 * mv2.Scalar245 + mv2.Scalar234 * mv1.Scalar124 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar124 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar145 - mv2.Scalar345 * mv1.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar245 * mv1.Scalar134 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar134 = (-2 * mv2.Scalar123 * mv1.Scalar235 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.Scalar345 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar134 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar134 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar234 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar135 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar135 - 2 * mv2.Scalar235 * mv1.Scalar123 * mv2.Scalar345 - mv2.Scalar345 * mv1.Scalar134 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.Scalar234 * mv2.Scalar245 + mv2.Scalar245 * mv1.Scalar134 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar234 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar234 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar125 = (2 * mv2.Scalar123 * mv1.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.Scalar234 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar125 + mv2.Scalar235 * mv1.Scalar125 * mv2.Scalar235 - mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar234 + 2 * mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar125 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar125 * mv2.Scalar345 + 2 * mv2.Scalar245 * mv1.Scalar135 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar135 = (-2 * mv2.Scalar145 * mv1.Scalar235 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.Scalar123 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar135 * mv2.Scalar234 + mv2.Scalar345 * mv1.Scalar135 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar245 * mv2.Scalar245 - mv2.Scalar235 * mv1.Scalar135 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar135 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar145) * mv2NormSquaredInv,
            Scalar235 = (mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar235 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar235 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar145 = (-2 * mv2.Scalar124 * mv1.Scalar234 * mv2.Scalar345 - 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar234 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar134 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar145 - mv2.Scalar234 * mv1.Scalar145 * mv2.Scalar234 - mv2.Scalar245 * mv1.Scalar145 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar145 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar125 * mv1.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar245 * mv2.Scalar235 + mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar235 - 2 * mv2.Scalar145 * mv1.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar245 = (-mv2.Scalar234 * mv1.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar245 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.Scalar345 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar235) * mv2NormSquaredInv,
            Scalar345 = (mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar345 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar235 * mv2.Scalar345 - mv2.Scalar345 * mv1.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDualOnDirect(this Ga401KVector3 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (2 * mv2.Scalar1234 * mv1.Scalar235 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar123 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar124 = (2 * mv2.Scalar1234 * mv1.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar124 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar1234 * mv1.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar134 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar2345 * mv1.Scalar234 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar125 = (2 * mv2.Scalar1235 * mv1.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar125 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar135 = (2 * mv2.Scalar1235 * mv1.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar135 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar235 = (-mv2.Scalar2345 * mv1.Scalar235 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar145 = (2 * mv2.Scalar1245 * mv1.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar245 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar145 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar245 = (-mv2.Scalar2345 * mv1.Scalar245 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar345 = (-mv2.Scalar2345 * mv1.Scalar345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDualOnDirect(this Ga401KVector3 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector3.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDualOnDirect(this Ga401KVector4 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar1235 * mv2.Scalar5 + mv2.Scalar5 * mv1.Scalar1234 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1235 = (-2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar1235 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar1235 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar1235 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1235 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1245 = (2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar1245 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.Scalar1235 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1245 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1245 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1345 = (-2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar1235 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar1245 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1345 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1345 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1345 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar2345 = (-mv2.Scalar2 * mv1.Scalar2345 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar2345 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar2345 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar2345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDualOnDirect(this Ga401KVector4 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar1345 * mv2.Scalar35 - mv2.Scalar24 * mv1.Scalar1234 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar1234 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar1234 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.Scalar1235 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar45 + mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar1234 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar1235 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar1235 = (2 * mv2.Scalar12 * mv1.Scalar2345 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.Scalar2345 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar1235 * mv2.Scalar35 + mv2.Scalar23 * mv1.Scalar1235 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar1235 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.Scalar1245 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1235 * mv2.Scalar34 + 2 * mv2.Scalar15 * mv1.Scalar2345 * mv2.Scalar45 - mv2.Scalar25 * mv1.Scalar1235 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar1235 * mv2.Scalar45 + 2 * mv2.Scalar35 * mv1.Scalar1245 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar1245 = (-2 * mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar25 - mv2.Scalar23 * mv1.Scalar1245 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar1245 * mv2.Scalar25 + mv2.Scalar24 * mv1.Scalar1245 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar1245 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar45 + mv2.Scalar45 * mv1.Scalar1245 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar1245 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar1345 = (2 * mv2.Scalar13 * mv1.Scalar2345 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar1235 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar1345 * mv2.Scalar23 + mv2.Scalar34 * mv1.Scalar1345 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar2345 * mv2.Scalar24 - mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar24 + 2 * mv2.Scalar15 * mv1.Scalar2345 * mv2.Scalar25 + 2 * mv2.Scalar25 * mv1.Scalar1235 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar1345 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar1345 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar2345 = (mv2.Scalar23 * mv1.Scalar2345 * mv2.Scalar23 + mv2.Scalar25 * mv1.Scalar2345 * mv2.Scalar25 + mv2.Scalar34 * mv1.Scalar2345 * mv2.Scalar34 + mv2.Scalar45 * mv1.Scalar2345 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar2345 * mv2.Scalar24 - mv2.Scalar35 * mv1.Scalar2345 * mv2.Scalar35) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDualOnDirect(this Ga401KVector4 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (-mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar1234 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar1234 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar1235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar1345 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar1234 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar1235 = (2 * mv2.Scalar123 * mv1.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar1235 * mv2.Scalar234 - 2 * mv2.Scalar125 * mv1.Scalar2345 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar2345 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar1235 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar1235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar1245 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar1235 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar1245 = (-2 * mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar245 - mv2.Scalar234 * mv1.Scalar1245 * mv2.Scalar234 - mv2.Scalar245 * mv1.Scalar1245 * mv2.Scalar245 + mv2.Scalar235 * mv1.Scalar1245 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar1245 * mv2.Scalar345 - 2 * mv2.Scalar245 * mv1.Scalar1345 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar1345 = (2 * mv2.Scalar145 * mv1.Scalar2345 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar1345 * mv2.Scalar234 - 2 * mv2.Scalar135 * mv1.Scalar2345 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.Scalar1235 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar1345 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar1345 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar1345 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar2345 = (mv2.Scalar234 * mv1.Scalar2345 * mv2.Scalar234 + mv2.Scalar245 * mv1.Scalar2345 * mv2.Scalar245 - mv2.Scalar235 * mv1.Scalar2345 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar2345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDualOnDirect(this Ga401KVector4 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (-2 * mv2.Scalar1234 * mv1.Scalar2345 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar1234 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1235 = (-2 * mv2.Scalar1235 * mv1.Scalar2345 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar1235 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1245 = (-2 * mv2.Scalar1245 * mv1.Scalar2345 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar1245 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1345 = (-2 * mv2.Scalar1345 * mv1.Scalar2345 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar1345 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar2345 = (-mv2.Scalar2345 * mv1.Scalar2345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDualOnDirect(this Ga401KVector4 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector4.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDualOnDirect(this Ga401KVector5 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (mv2.Scalar2 * mv1.Scalar12345 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar12345 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar12345 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar12345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDualOnDirect(this Ga401KVector5 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (-mv2.Scalar23 * mv1.Scalar12345 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar12345 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar12345 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar12345 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar12345 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar12345 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDualOnDirect(this Ga401KVector5 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (mv2.Scalar234 * mv1.Scalar12345 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar12345 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar12345 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar12345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDualOnDirect(this Ga401KVector5 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (mv2.Scalar2345 * mv1.Scalar12345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDualOnDirect(this Ga401KVector5 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector5.Zero;
    }
    
    public static Ga401Multivector ReflectDualOnDirect(this Ga401Multivector mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector0.Scalar * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector0.Scalar * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar1 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector1.Scalar1 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar2 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector1.Scalar2 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[4] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector1.Scalar3 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector1.Scalar3 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[8] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector1.Scalar4 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[16] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector1.Scalar5 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector1.Scalar5 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.KVector1.Scalar4 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector1.Scalar5 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector1.Scalar5 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar25 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar15 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar12 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar12 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[5] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar35 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar15 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar13 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar13 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar25 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar23 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar23 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[9] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar14 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar15 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector2.Scalar14 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar25 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector2.Scalar24 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[12] += (2 * mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar34 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector2.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar35 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector2.Scalar34 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[17] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector2.Scalar15 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector2.Scalar15 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar14 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar15 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar15 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[18] += (-mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector2.Scalar25 * mv2.Scalar3 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar24 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar25 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar25 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[20] += (2 * mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector2.Scalar35 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector2.Scalar34 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar35 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar35 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[24] += (2 * mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar34 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar35 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar45 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar45 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar125 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar123 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector3.Scalar123 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[11] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar125 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector3.Scalar124 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector3.Scalar134 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar135 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector3.Scalar134 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar2 * mv1.KVector3.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector3.Scalar234 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector3.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar235 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector3.Scalar234 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[19] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector3.Scalar125 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar124 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar125 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar125 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[21] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar134 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar135 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar135 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[22] += (mv2.Scalar2 * mv1.KVector3.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector3.Scalar235 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar234 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar235 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar235 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[25] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar134 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar135 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar145 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar145 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[26] += (mv2.Scalar2 * mv1.KVector3.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar234 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar235 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar245 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar245 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[28] += (2 * mv2.Scalar2 * mv1.KVector3.Scalar234 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar235 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar245 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar345 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar345 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector4.Scalar1235 * mv2.Scalar5 + mv2.Scalar5 * mv1.KVector4.Scalar1234 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[23] += (-2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector4.Scalar1235 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector4.Scalar1235 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar4 - 2 * mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector4.Scalar1235 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar1235 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[27] += (2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector4.Scalar1245 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar5 - 2 * mv2.Scalar3 * mv1.KVector4.Scalar1235 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector4.Scalar1245 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar1245 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[29] += (-2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector4.Scalar1235 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1245 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector4.Scalar1345 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector4.Scalar1345 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar1345 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[30] += (-mv2.Scalar2 * mv1.KVector4.Scalar2345 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector4.Scalar2345 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector4.Scalar2345 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector4.Scalar2345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (mv2.Scalar2 * mv1.KVector5.Scalar12345 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector5.Scalar12345 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector5.Scalar12345 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector5.Scalar12345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDualOnDirect(this Ga401Multivector mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23 - mv2.Scalar45 * mv1.KVector0.Scalar * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector0.Scalar * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector0.Scalar * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector0.Scalar * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector0.Scalar * mv2.Scalar35) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar4 * mv2.Scalar24 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar15 * mv1.KVector1.Scalar2 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar5 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar2 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar5 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector1.Scalar1 * mv2.Scalar24 + mv2.Scalar45 * mv1.KVector1.Scalar1 * mv2.Scalar45 + mv2.Scalar34 * mv1.KVector1.Scalar1 * mv2.Scalar34 + 2 * mv2.Scalar15 * mv1.KVector1.Scalar3 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.KVector1.Scalar4 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector1.Scalar1 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector1.Scalar1 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[2] += (2 * mv2.Scalar25 * mv1.KVector1.Scalar4 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector1.Scalar2 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector1.Scalar2 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector1.Scalar2 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector1.Scalar2 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector1.Scalar2 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.KVector1.Scalar3 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar45 * mv1.KVector1.Scalar3 * mv2.Scalar45 + mv2.Scalar34 * mv1.KVector1.Scalar3 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector1.Scalar3 * mv2.Scalar35 - mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector1.Scalar3 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar45 + 2 * mv2.Scalar35 * mv1.KVector1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[8] += (mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector1.Scalar4 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector1.Scalar4 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector1.Scalar4 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector1.Scalar4 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector1.Scalar4 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[16] += (-mv2.Scalar45 * mv1.KVector1.Scalar5 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector1.Scalar5 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector1.Scalar5 * mv2.Scalar25 + mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar34) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar34 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar35 - mv2.Scalar24 * mv1.KVector2.Scalar12 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector2.Scalar12 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar45 - mv2.Scalar25 * mv1.KVector2.Scalar12 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector2.Scalar12 * mv2.Scalar35 - mv2.Scalar45 * mv1.KVector2.Scalar12 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar13 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar14 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[5] += (-2 * mv2.Scalar12 * mv1.KVector2.Scalar24 * mv2.Scalar34 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar25 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar25 * mv2.Scalar25 + mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar24 + mv2.Scalar25 * mv1.KVector2.Scalar13 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar23 * mv2.Scalar24 + 2 * mv2.Scalar15 * mv1.KVector2.Scalar23 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar14 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar15 - mv2.Scalar34 * mv1.KVector2.Scalar13 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector2.Scalar13 * mv2.Scalar35 - mv2.Scalar45 * mv1.KVector2.Scalar13 * mv2.Scalar45 - 2 * mv2.Scalar35 * mv1.KVector2.Scalar14 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 + mv2.Scalar25 * mv1.KVector2.Scalar23 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector2.Scalar23 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector2.Scalar23 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar25 - mv2.Scalar34 * mv1.KVector2.Scalar23 * mv2.Scalar34 - 2 * mv2.Scalar35 * mv1.KVector2.Scalar24 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector2.Scalar23 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[9] += (-2 * mv2.Scalar12 * mv1.KVector2.Scalar34 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar25 * mv2.Scalar45 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar23 + 2 * mv2.Scalar15 * mv1.KVector2.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar35 * mv2.Scalar45 - mv2.Scalar45 * mv1.KVector2.Scalar14 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar34 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar24 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar25 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar35 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector2.Scalar14 * mv2.Scalar24 + mv2.Scalar25 * mv1.KVector2.Scalar14 * mv2.Scalar25 + 2 * mv2.Scalar15 * mv1.KVector2.Scalar24 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar15 + mv2.Scalar34 * mv1.KVector2.Scalar14 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector2.Scalar14 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar15) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar24 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar35 * mv2.Scalar45 + mv2.Scalar35 * mv1.KVector2.Scalar24 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector2.Scalar24 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector2.Scalar24 * mv2.Scalar25 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar34 * mv2.Scalar35 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector2.Scalar24 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[12] += (-mv2.Scalar25 * mv1.KVector2.Scalar34 * mv2.Scalar25 - mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar24 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar23 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector2.Scalar34 * mv2.Scalar23 + mv2.Scalar34 * mv1.KVector2.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector2.Scalar34 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar35 - mv2.Scalar45 * mv1.KVector2.Scalar34 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[17] += (-2 * mv2.Scalar12 * mv1.KVector2.Scalar35 * mv2.Scalar23 - 2 * mv2.Scalar12 * mv1.KVector2.Scalar45 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar25 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar45 * mv2.Scalar34 + mv2.Scalar45 * mv1.KVector2.Scalar15 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar23 + 2 * mv2.Scalar15 * mv1.KVector2.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar25 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar35 * mv2.Scalar34 - mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector2.Scalar15 * mv2.Scalar35 - mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar34 + 2 * mv2.Scalar15 * mv1.KVector2.Scalar45 * mv2.Scalar45 + 2 * mv2.Scalar15 * mv1.KVector2.Scalar25 * mv2.Scalar25 + mv2.Scalar25 * mv1.KVector2.Scalar15 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[18] += (mv2.Scalar35 * mv1.KVector2.Scalar25 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar35 * mv2.Scalar35 - mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar23 + mv2.Scalar25 * mv1.KVector2.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar45 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar34 + mv2.Scalar45 * mv1.KVector2.Scalar25 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[20] += (-2 * mv2.Scalar34 * mv1.KVector2.Scalar24 * mv2.Scalar25 - mv2.Scalar25 * mv1.KVector2.Scalar35 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector2.Scalar35 * mv2.Scalar45 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector2.Scalar35 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector2.Scalar35 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar24 + 2 * mv2.Scalar35 * mv1.KVector2.Scalar45 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar23 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar35 - mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[24] += (-2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar34 * mv2.Scalar25 - mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar23 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector2.Scalar45 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector2.Scalar45 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar34 * mv2.Scalar45 + mv2.Scalar34 * mv1.KVector2.Scalar45 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector2.Scalar45 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector2.Scalar45 * mv2.Scalar45) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar45 * mv1.KVector3.Scalar123 * mv2.Scalar45 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar235 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar245 * mv2.Scalar15 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar35 + mv2.Scalar35 * mv1.KVector3.Scalar123 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector3.Scalar123 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector3.Scalar123 * mv2.Scalar25 + 2 * mv2.Scalar35 * mv1.KVector3.Scalar124 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar45 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar234 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[11] += (mv2.Scalar45 * mv1.KVector3.Scalar124 * mv2.Scalar45 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar345 * mv2.Scalar25 + mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar15 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector3.Scalar124 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar125 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector3.Scalar124 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar12 * mv1.KVector3.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar25 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar23 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar15 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.KVector3.Scalar123 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar345 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar24 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar25 + mv2.Scalar25 * mv1.KVector3.Scalar134 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector3.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector3.Scalar134 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar35 - mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector3.Scalar234 * mv2.Scalar24 + mv2.Scalar25 * mv1.KVector3.Scalar234 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector3.Scalar234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar235 * mv2.Scalar35 - mv2.Scalar35 * mv1.KVector3.Scalar234 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[19] += (-2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector3.Scalar125 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector3.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar145 * mv2.Scalar45 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector3.Scalar125 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar15 - mv2.Scalar35 * mv1.KVector3.Scalar125 * mv2.Scalar35 - mv2.Scalar45 * mv1.KVector3.Scalar125 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar135 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[21] += (-mv2.Scalar45 * mv1.KVector3.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector3.Scalar135 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector3.Scalar135 * mv2.Scalar35 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.KVector3.Scalar145 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar14 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar25 + mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector3.Scalar135 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[22] += (-mv2.Scalar23 * mv1.KVector3.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar35 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector3.Scalar235 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector3.Scalar235 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector3.Scalar235 * mv2.Scalar35 + mv2.Scalar25 * mv1.KVector3.Scalar235 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[25] += (2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar35 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar15 + mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar23 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar345 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector3.Scalar145 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector3.Scalar145 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector3.Scalar145 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector3.Scalar145 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector3.Scalar145 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[26] += (2 * mv2.Scalar25 * mv1.KVector3.Scalar345 * mv2.Scalar35 - mv2.Scalar24 * mv1.KVector3.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector3.Scalar245 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar25 + mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar23 + mv2.Scalar25 * mv1.KVector3.Scalar245 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector3.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[28] += (mv2.Scalar35 * mv1.KVector3.Scalar345 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar235 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector3.Scalar345 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector3.Scalar345 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar235 * mv2.Scalar45 - mv2.Scalar45 * mv1.KVector3.Scalar345 * mv2.Scalar45) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector4.Scalar1345 * mv2.Scalar35 - mv2.Scalar24 * mv1.KVector4.Scalar1234 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector4.Scalar1234 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector4.Scalar1234 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.KVector4.Scalar1235 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar45 + mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector4.Scalar1234 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector4.Scalar1235 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[23] += (2 * mv2.Scalar12 * mv1.KVector4.Scalar2345 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.KVector4.Scalar2345 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector4.Scalar1235 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector4.Scalar1235 * mv2.Scalar24 + 2 * mv2.Scalar35 * mv1.KVector4.Scalar1245 * mv2.Scalar45 + 2 * mv2.Scalar23 * mv1.KVector4.Scalar1245 * mv2.Scalar24 + mv2.Scalar45 * mv1.KVector4.Scalar1235 * mv2.Scalar45 + mv2.Scalar35 * mv1.KVector4.Scalar1235 * mv2.Scalar35 - mv2.Scalar34 * mv1.KVector4.Scalar1235 * mv2.Scalar34 + 2 * mv2.Scalar15 * mv1.KVector4.Scalar2345 * mv2.Scalar45 - mv2.Scalar25 * mv1.KVector4.Scalar1235 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[27] += (-2 * mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector4.Scalar1245 * mv2.Scalar35 - mv2.Scalar23 * mv1.KVector4.Scalar1245 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector4.Scalar1245 * mv2.Scalar25 + mv2.Scalar24 * mv1.KVector4.Scalar1245 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector4.Scalar1245 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar45 + mv2.Scalar45 * mv1.KVector4.Scalar1245 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.KVector4.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[29] += (-mv2.Scalar35 * mv1.KVector4.Scalar1345 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.KVector4.Scalar2345 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector4.Scalar1235 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector4.Scalar1345 * mv2.Scalar23 + mv2.Scalar34 * mv1.KVector4.Scalar1345 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector4.Scalar2345 * mv2.Scalar24 - mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar24 + 2 * mv2.Scalar15 * mv1.KVector4.Scalar2345 * mv2.Scalar25 + 2 * mv2.Scalar25 * mv1.KVector4.Scalar1235 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector4.Scalar1345 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector4.Scalar1345 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[30] += (mv2.Scalar23 * mv1.KVector4.Scalar2345 * mv2.Scalar23 + mv2.Scalar25 * mv1.KVector4.Scalar2345 * mv2.Scalar25 + mv2.Scalar34 * mv1.KVector4.Scalar2345 * mv2.Scalar34 + mv2.Scalar45 * mv1.KVector4.Scalar2345 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector4.Scalar2345 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector4.Scalar2345 * mv2.Scalar35) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (-mv2.Scalar34 * mv1.KVector5.Scalar12345 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector5.Scalar12345 * mv2.Scalar35 - mv2.Scalar23 * mv1.KVector5.Scalar12345 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector5.Scalar12345 * mv2.Scalar24 - mv2.Scalar45 * mv1.KVector5.Scalar12345 * mv2.Scalar45 - mv2.Scalar25 * mv1.KVector5.Scalar12345 * mv2.Scalar25) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDualOnDirect(this Ga401Multivector mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar345 * mv1.KVector0.Scalar * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector0.Scalar * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector0.Scalar * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector0.Scalar * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar123 * mv1.KVector1.Scalar4 * mv2.Scalar234 + 2 * mv2.Scalar123 * mv1.KVector1.Scalar5 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.KVector1.Scalar3 * mv2.Scalar234 + 2 * mv2.Scalar124 * mv1.KVector1.Scalar5 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.KVector1.Scalar5 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector1.Scalar1 * mv2.Scalar234 + 2 * mv2.Scalar125 * mv1.KVector1.Scalar3 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.KVector1.Scalar4 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector1.Scalar4 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector1.Scalar1 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.KVector1.Scalar2 * mv2.Scalar245 + 2 * mv2.Scalar145 * mv1.KVector1.Scalar3 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector1.Scalar1 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector1.Scalar1 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar234 * mv1.KVector1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.KVector1.Scalar3 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector1.Scalar2 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector1.Scalar2 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar234 * mv1.KVector1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector1.Scalar3 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector1.Scalar3 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector1.Scalar3 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[8] += (-mv2.Scalar345 * mv1.KVector1.Scalar4 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector1.Scalar4 * mv2.Scalar245 + mv2.Scalar234 * mv1.KVector1.Scalar4 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[16] += (mv2.Scalar235 * mv1.KVector1.Scalar5 * mv2.Scalar235 + mv2.Scalar345 * mv1.KVector1.Scalar5 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector1.Scalar5 * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar234 * mv1.KVector2.Scalar12 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.KVector2.Scalar13 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector2.Scalar12 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector2.Scalar12 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector2.Scalar12 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar124 * mv1.KVector2.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar23 * mv2.Scalar234 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar145 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector2.Scalar13 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector2.Scalar13 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar145 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.KVector2.Scalar23 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector2.Scalar24 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector2.Scalar13 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector2.Scalar13 * mv2.Scalar245 - 2 * mv2.Scalar145 * mv1.KVector2.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar234 * mv1.KVector2.Scalar23 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector2.Scalar23 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector2.Scalar23 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector2.Scalar23 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[9] += (-2 * mv2.Scalar123 * mv1.KVector2.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.KVector2.Scalar35 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.KVector2.Scalar34 * mv2.Scalar234 - 2 * mv2.Scalar145 * mv1.KVector2.Scalar34 * mv2.Scalar345 + 2 * mv2.Scalar124 * mv1.KVector2.Scalar35 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.KVector2.Scalar24 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar235 + mv2.Scalar234 * mv1.KVector2.Scalar14 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector2.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar34 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar135 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar125 - mv2.Scalar245 * mv1.KVector2.Scalar14 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector2.Scalar14 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.KVector2.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar234 * mv1.KVector2.Scalar24 * mv2.Scalar234 - mv2.Scalar345 * mv1.KVector2.Scalar24 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector2.Scalar24 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.KVector2.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[12] += (-mv2.Scalar234 * mv1.KVector2.Scalar34 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector2.Scalar34 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector2.Scalar34 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar23 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector2.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[17] += (mv2.Scalar345 * mv1.KVector2.Scalar15 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.KVector2.Scalar45 * mv2.Scalar234 - 2 * mv2.Scalar124 * mv1.KVector2.Scalar35 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector2.Scalar15 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar234 + 2 * mv2.Scalar145 * mv1.KVector2.Scalar35 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector2.Scalar45 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.KVector2.Scalar35 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector2.Scalar25 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.KVector2.Scalar25 * mv2.Scalar245 + mv2.Scalar245 * mv1.KVector2.Scalar15 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[18] += (mv2.Scalar235 * mv1.KVector2.Scalar25 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector2.Scalar25 * mv2.Scalar245 + 2 * mv2.Scalar245 * mv1.KVector2.Scalar35 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector2.Scalar25 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[20] += (-mv2.Scalar235 * mv1.KVector2.Scalar35 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar23 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector2.Scalar35 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector2.Scalar35 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[24] += (mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar24 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar34 * mv2.Scalar245 - mv2.Scalar234 * mv1.KVector2.Scalar45 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector2.Scalar45 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector2.Scalar45 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-2 * mv2.Scalar123 * mv1.KVector3.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar245 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector3.Scalar123 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar245 * mv2.Scalar125 + mv2.Scalar345 * mv1.KVector3.Scalar123 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.KVector3.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector3.Scalar234 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector3.Scalar123 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector3.Scalar123 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar124 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[11] += (2 * mv2.Scalar123 * mv1.KVector3.Scalar245 * mv2.Scalar235 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector3.Scalar124 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar345 * mv2.Scalar245 + mv2.Scalar234 * mv1.KVector3.Scalar124 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector3.Scalar124 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar145 - mv2.Scalar345 * mv1.KVector3.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar245 * mv1.KVector3.Scalar134 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[13] += (mv2.Scalar245 * mv1.KVector3.Scalar134 * mv2.Scalar245 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar235 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar345 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector3.Scalar134 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector3.Scalar134 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector3.Scalar234 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar135 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar135 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar123 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector3.Scalar134 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.KVector3.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector3.Scalar234 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector3.Scalar234 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector3.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[19] += (2 * mv2.Scalar123 * mv1.KVector3.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.KVector3.Scalar234 * mv2.Scalar235 + 2 * mv2.Scalar245 * mv1.KVector3.Scalar135 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar125 - mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector3.Scalar125 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector3.Scalar125 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector3.Scalar125 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[21] += (-2 * mv2.Scalar145 * mv1.KVector3.Scalar235 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar123 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector3.Scalar135 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector3.Scalar135 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.KVector3.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector3.Scalar245 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector3.Scalar135 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector3.Scalar135 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar145) * mv2NormSquaredInv;
            tempScalar[22] += (mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector3.Scalar235 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector3.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector3.Scalar235 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[25] += (-2 * mv2.Scalar124 * mv1.KVector3.Scalar234 * mv2.Scalar345 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar234 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar134 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar145 - mv2.Scalar234 * mv1.KVector3.Scalar145 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector3.Scalar145 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector3.Scalar145 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.KVector3.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar125 * mv1.KVector3.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector3.Scalar245 * mv2.Scalar235 + mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar235 - 2 * mv2.Scalar145 * mv1.KVector3.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[26] += (-mv2.Scalar234 * mv1.KVector3.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector3.Scalar245 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector3.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.KVector3.Scalar345 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[28] += (mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector3.Scalar345 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector3.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar235 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector3.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector4.Scalar1234 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector4.Scalar1234 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector4.Scalar1235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector4.Scalar1345 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector4.Scalar1234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[23] += (2 * mv2.Scalar123 * mv1.KVector4.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector4.Scalar1235 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector4.Scalar1235 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.KVector4.Scalar2345 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector4.Scalar2345 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector4.Scalar1235 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector4.Scalar1235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector4.Scalar1245 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[27] += (-2 * mv2.Scalar245 * mv1.KVector4.Scalar1345 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector4.Scalar1245 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar245 - mv2.Scalar234 * mv1.KVector4.Scalar1245 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector4.Scalar1245 * mv2.Scalar245 + mv2.Scalar235 * mv1.KVector4.Scalar1245 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[29] += (mv2.Scalar245 * mv1.KVector4.Scalar1345 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector4.Scalar2345 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector4.Scalar1345 * mv2.Scalar234 - 2 * mv2.Scalar135 * mv1.KVector4.Scalar2345 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.KVector4.Scalar1235 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector4.Scalar1345 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector4.Scalar1345 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.KVector4.Scalar2345 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[30] += (mv2.Scalar234 * mv1.KVector4.Scalar2345 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector4.Scalar2345 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector4.Scalar2345 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector4.Scalar2345 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (-mv2.Scalar235 * mv1.KVector5.Scalar12345 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector5.Scalar12345 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector5.Scalar12345 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector5.Scalar12345 * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDualOnDirect(this Ga401Multivector mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar2345 * mv1.KVector0.Scalar * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar1234 * mv1.KVector1.Scalar5 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector1.Scalar4 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.KVector1.Scalar3 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector1.Scalar2 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector1.Scalar1 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar2345 * mv1.KVector1.Scalar2 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar2345 * mv1.KVector1.Scalar3 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[8] += (-mv2.Scalar2345 * mv1.KVector1.Scalar4 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[16] += (-mv2.Scalar2345 * mv1.KVector1.Scalar5 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-2 * mv2.Scalar1234 * mv1.KVector2.Scalar25 * mv2.Scalar2345 + 2 * mv2.Scalar1235 * mv1.KVector2.Scalar24 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector2.Scalar23 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector2.Scalar12 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[5] += (-2 * mv2.Scalar1234 * mv1.KVector2.Scalar35 * mv2.Scalar2345 + 2 * mv2.Scalar1235 * mv1.KVector2.Scalar34 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector2.Scalar23 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector2.Scalar13 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar2345 * mv1.KVector2.Scalar23 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[9] += (-2 * mv2.Scalar1234 * mv1.KVector2.Scalar45 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.KVector2.Scalar34 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector2.Scalar24 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector2.Scalar14 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar2345 * mv1.KVector2.Scalar24 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[12] += (-mv2.Scalar2345 * mv1.KVector2.Scalar34 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[17] += (-2 * mv2.Scalar1235 * mv1.KVector2.Scalar45 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.KVector2.Scalar35 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector2.Scalar25 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector2.Scalar15 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[18] += (-mv2.Scalar2345 * mv1.KVector2.Scalar25 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[20] += (-mv2.Scalar2345 * mv1.KVector2.Scalar35 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[24] += (-mv2.Scalar2345 * mv1.KVector2.Scalar45 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (2 * mv2.Scalar1234 * mv1.KVector3.Scalar235 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector3.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar123 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[11] += (2 * mv2.Scalar1234 * mv1.KVector3.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector3.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar124 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar1234 * mv1.KVector3.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector3.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar134 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar2345 * mv1.KVector3.Scalar234 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[19] += (2 * mv2.Scalar1235 * mv1.KVector3.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector3.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar125 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[21] += (2 * mv2.Scalar1235 * mv1.KVector3.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector3.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar135 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[22] += (-mv2.Scalar2345 * mv1.KVector3.Scalar235 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[25] += (2 * mv2.Scalar1245 * mv1.KVector3.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector3.Scalar245 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar145 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[26] += (-mv2.Scalar2345 * mv1.KVector3.Scalar245 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[28] += (-mv2.Scalar2345 * mv1.KVector3.Scalar345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-2 * mv2.Scalar1234 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector4.Scalar1234 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[23] += (-2 * mv2.Scalar1235 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector4.Scalar1235 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[27] += (-2 * mv2.Scalar1245 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector4.Scalar1245 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[29] += (-2 * mv2.Scalar1345 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector4.Scalar1345 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[30] += (-mv2.Scalar2345 * mv1.KVector4.Scalar2345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (mv2.Scalar2345 * mv1.KVector5.Scalar12345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector ReflectDualOnDirect(this Ga401Multivector mv1, Ga401KVector5 mv2)
    {
        return Ga401Multivector.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDualOnDual(this Ga401KVector0 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (-mv2.Scalar2 * mv1.Scalar * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDualOnDual(this Ga401KVector0 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar23 * mv1.Scalar * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDualOnDual(this Ga401KVector0 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (-mv2.Scalar234 * mv1.Scalar * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDualOnDual(this Ga401KVector0 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector0
        {
            Scalar = (mv2.Scalar2345 * mv1.Scalar * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 ReflectDualOnDual(this Ga401KVector0 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDualOnDual(this Ga401KVector1 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar1 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.Scalar1 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar1 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar1 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar2 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar2 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar3 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar3 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar3 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar4 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar4 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar4 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar5 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar4 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar5 = (2 * mv2.Scalar2 * mv1.Scalar2 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar5 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar3 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar5 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.Scalar4 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar5 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar5 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDualOnDual(this Ga401KVector1 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar12 * mv1.Scalar3 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar4 * mv2.Scalar24 + 2 * mv2.Scalar12 * mv1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.Scalar5 * mv2.Scalar35 + mv2.Scalar23 * mv1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.Scalar2 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar1 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar1 * mv2.Scalar34 + 2 * mv2.Scalar15 * mv1.Scalar2 * mv2.Scalar25 + 2 * mv2.Scalar15 * mv1.Scalar3 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.Scalar4 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar1 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar1 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar1 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar23 * mv1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar2 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar2 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar2 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar2 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar2 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.Scalar3 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar3 = (mv2.Scalar34 * mv1.Scalar3 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar3 * mv2.Scalar35 - mv2.Scalar23 * mv1.Scalar3 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar3 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar3 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar45 * mv1.Scalar3 * mv2.Scalar45 + 2 * mv2.Scalar35 * mv1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar4 = (mv2.Scalar23 * mv1.Scalar4 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar4 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar4 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar4 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar4 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar4 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar5 = (mv2.Scalar23 * mv1.Scalar5 * mv2.Scalar23 - mv2.Scalar45 * mv1.Scalar5 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar5 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar5 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar5 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDualOnDual(this Ga401KVector1 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar123 * mv1.Scalar4 * mv2.Scalar234 + 2 * mv2.Scalar123 * mv1.Scalar5 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.Scalar3 * mv2.Scalar234 + 2 * mv2.Scalar124 * mv1.Scalar5 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.Scalar5 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar1 * mv2.Scalar234 + 2 * mv2.Scalar125 * mv1.Scalar3 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.Scalar4 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar4 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar1 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.Scalar2 * mv2.Scalar245 + 2 * mv2.Scalar145 * mv1.Scalar3 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar1 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar1 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar2 = (mv2.Scalar234 * mv1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.Scalar3 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar2 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar2 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar234 * mv1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar3 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar3 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar3 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar4 = (-mv2.Scalar345 * mv1.Scalar4 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar4 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar4 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar5 = (-mv2.Scalar234 * mv1.Scalar5 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar5 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar5 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar5 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDualOnDual(this Ga401KVector1 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector1.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector1
        {
            Scalar1 = (2 * mv2.Scalar1234 * mv1.Scalar5 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar4 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.Scalar3 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar2 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar1 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar2 = (-mv2.Scalar2345 * mv1.Scalar2 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar3 = (-mv2.Scalar2345 * mv1.Scalar3 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar4 = (-mv2.Scalar2345 * mv1.Scalar4 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar5 = (-mv2.Scalar2345 * mv1.Scalar5 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector1 ReflectDualOnDual(this Ga401KVector1 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector1.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDualOnDual(this Ga401KVector2 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (-2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar1 * mv1.Scalar25 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar15 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar12 * mv2.Scalar3 - mv2.Scalar4 * mv1.Scalar12 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar12 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar1 * mv1.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar1 * mv1.Scalar35 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar13 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar15 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar13 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar13 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar25 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar23 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar23 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar14 = (2 * mv2.Scalar1 * mv1.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar34 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar45 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar14 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar14 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar14 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar15 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar14 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar24 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar24 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar25 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar24 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar34 = (-2 * mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar34 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar34 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar35 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar34 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar15 = (2 * mv2.Scalar1 * mv1.Scalar25 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar35 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar45 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar12 * mv2.Scalar5 - mv2.Scalar2 * mv1.Scalar15 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.Scalar13 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar15 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.Scalar14 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar15 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar15 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar25 = (mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar23 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar25 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.Scalar24 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar25 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar25 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar35 = (-2 * mv2.Scalar2 * mv1.Scalar23 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar35 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar35 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar34 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar35 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar35 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar45 = (-2 * mv2.Scalar2 * mv1.Scalar24 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar25 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar45 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar34 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar35 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar45 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar45 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar45 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDualOnDual(this Ga401KVector2 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (2 * mv2.Scalar25 * mv1.Scalar14 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar12 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar12 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar12 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar12 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar12 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.Scalar13 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar35 * mv1.Scalar14 * mv2.Scalar45 + 2 * mv2.Scalar12 * mv1.Scalar24 * mv2.Scalar34 + 2 * mv2.Scalar12 * mv1.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.Scalar25 * mv2.Scalar25 + mv2.Scalar34 * mv1.Scalar13 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar13 * mv2.Scalar35 - mv2.Scalar23 * mv1.Scalar13 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar13 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar13 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.Scalar23 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar14 - 2 * mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar15 + mv2.Scalar45 * mv1.Scalar13 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar23 = (-mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar23 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar23 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar25 + mv2.Scalar34 * mv1.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar35 * mv1.Scalar24 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar23 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar14 = (2 * mv2.Scalar12 * mv1.Scalar34 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar25 * mv2.Scalar45 + 2 * mv2.Scalar13 * mv1.Scalar24 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.Scalar34 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.Scalar35 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar14 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.Scalar35 * mv2.Scalar35 - mv2.Scalar24 * mv1.Scalar14 * mv2.Scalar24 - mv2.Scalar25 * mv1.Scalar14 * mv2.Scalar25 - 2 * mv2.Scalar15 * mv1.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar15 + mv2.Scalar45 * mv1.Scalar14 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar14 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar14 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar15) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar35 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar24 * mv2.Scalar35 - mv2.Scalar34 * mv1.Scalar24 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar24 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar34 = (mv2.Scalar25 * mv1.Scalar34 * mv2.Scalar25 + mv2.Scalar24 * mv1.Scalar34 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.Scalar23 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar34 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar34 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar34 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar15 = (2 * mv2.Scalar12 * mv1.Scalar35 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.Scalar45 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.Scalar25 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar45 * mv2.Scalar34 + mv2.Scalar23 * mv1.Scalar15 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.Scalar35 * mv2.Scalar35 + 2 * mv2.Scalar14 * mv1.Scalar25 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar35 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar15 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar15 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar15 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.Scalar45 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar15 * mv2.Scalar45 - 2 * mv2.Scalar15 * mv1.Scalar25 * mv2.Scalar25 - mv2.Scalar25 * mv1.Scalar15 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar25 = (-mv2.Scalar35 * mv1.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.Scalar35 * mv2.Scalar35 + mv2.Scalar23 * mv1.Scalar25 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar25 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.Scalar45 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar25 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar25 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar34 - mv2.Scalar45 * mv1.Scalar25 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar35 = (2 * mv2.Scalar34 * mv1.Scalar24 * mv2.Scalar25 + mv2.Scalar25 * mv1.Scalar35 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar35 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.Scalar23 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar35 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar35 * mv2.Scalar24 - mv2.Scalar35 * mv1.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.Scalar45 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.Scalar23 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar35 + mv2.Scalar34 * mv1.Scalar35 * mv2.Scalar34) * mv2NormSquaredInv,
            Scalar45 = (2 * mv2.Scalar23 * mv1.Scalar24 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar34 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar45 * mv2.Scalar23 - 2 * mv2.Scalar24 * mv1.Scalar24 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar45 * mv2.Scalar24 + mv2.Scalar35 * mv1.Scalar45 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar34 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar45 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar45 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar45 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDualOnDual(this Ga401KVector2 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (-mv2.Scalar234 * mv1.Scalar12 * mv2.Scalar234 - 2 * mv2.Scalar245 * mv1.Scalar13 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar12 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar12 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar12 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar13 = (-2 * mv2.Scalar124 * mv1.Scalar25 * mv2.Scalar345 + 2 * mv2.Scalar134 * mv1.Scalar23 * mv2.Scalar234 + 2 * mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar145 + 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar245 + mv2.Scalar234 * mv1.Scalar13 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar145 - 2 * mv2.Scalar125 * mv1.Scalar24 * mv2.Scalar345 + 2 * mv2.Scalar135 * mv1.Scalar23 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar24 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar13 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar13 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar13 * mv2.Scalar245 + 2 * mv2.Scalar145 * mv1.Scalar23 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar234 * mv1.Scalar23 * mv2.Scalar234 - mv2.Scalar345 * mv1.Scalar23 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar245 + mv2.Scalar235 * mv1.Scalar23 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar23 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar14 = (2 * mv2.Scalar123 * mv1.Scalar25 * mv2.Scalar345 + 2 * mv2.Scalar123 * mv1.Scalar35 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.Scalar34 * mv2.Scalar234 + 2 * mv2.Scalar145 * mv1.Scalar34 * mv2.Scalar345 - 2 * mv2.Scalar124 * mv1.Scalar35 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.Scalar24 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar235 - mv2.Scalar234 * mv1.Scalar14 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar14 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar24 * mv2.Scalar235 - 2 * mv2.Scalar125 * mv1.Scalar34 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar135 - 2 * mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar125 + mv2.Scalar345 * mv1.Scalar14 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar14 * mv2.Scalar245 + 2 * mv2.Scalar145 * mv1.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar24 = (-mv2.Scalar234 * mv1.Scalar24 * mv2.Scalar234 + mv2.Scalar345 * mv1.Scalar24 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar24 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar24 * mv2.Scalar245 + 2 * mv2.Scalar245 * mv1.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar34 = (mv2.Scalar234 * mv1.Scalar34 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar34 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar34 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.Scalar23 * mv2.Scalar345 + mv2.Scalar345 * mv1.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar15 = (-mv2.Scalar245 * mv1.Scalar15 * mv2.Scalar245 + 2 * mv2.Scalar123 * mv1.Scalar45 * mv2.Scalar234 + 2 * mv2.Scalar124 * mv1.Scalar35 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.Scalar25 * mv2.Scalar234 + mv2.Scalar234 * mv1.Scalar15 * mv2.Scalar234 - 2 * mv2.Scalar145 * mv1.Scalar35 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.Scalar45 * mv2.Scalar245 - 2 * mv2.Scalar135 * mv1.Scalar45 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.Scalar35 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar15 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.Scalar25 * mv2.Scalar235 - mv2.Scalar235 * mv1.Scalar15 * mv2.Scalar235 - 2 * mv2.Scalar145 * mv1.Scalar25 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar25 = (-mv2.Scalar235 * mv1.Scalar25 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar25 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar25 * mv2.Scalar345 - 2 * mv2.Scalar245 * mv1.Scalar35 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar25 * mv2.Scalar234) * mv2NormSquaredInv,
            Scalar35 = (mv2.Scalar235 * mv1.Scalar35 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.Scalar23 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar35 * mv2.Scalar234 - mv2.Scalar345 * mv1.Scalar35 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar35 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar45 = (-mv2.Scalar235 * mv1.Scalar45 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar24 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.Scalar34 * mv2.Scalar245 + mv2.Scalar234 * mv1.Scalar45 * mv2.Scalar234 + mv2.Scalar245 * mv1.Scalar45 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar45 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDualOnDual(this Ga401KVector2 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector2.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector2
        {
            Scalar12 = (2 * mv2.Scalar1234 * mv1.Scalar25 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar24 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar12 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar13 = (2 * mv2.Scalar1234 * mv1.Scalar35 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar13 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar23 = (mv2.Scalar2345 * mv1.Scalar23 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar14 = (2 * mv2.Scalar1234 * mv1.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar24 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar14 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar24 = (mv2.Scalar2345 * mv1.Scalar24 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar34 = (mv2.Scalar2345 * mv1.Scalar34 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar15 = (2 * mv2.Scalar1235 * mv1.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar35 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.Scalar25 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar15 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar25 = (mv2.Scalar2345 * mv1.Scalar25 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar35 = (mv2.Scalar2345 * mv1.Scalar35 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar45 = (mv2.Scalar2345 * mv1.Scalar45 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector2 ReflectDualOnDual(this Ga401KVector2 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector2.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDualOnDual(this Ga401KVector3 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar125 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar123 * mv2.Scalar4 - mv2.Scalar5 * mv1.Scalar123 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar124 = (-2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar124 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar125 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar124 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar1 * mv1.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar134 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar134 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar135 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar134 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar234 = (mv2.Scalar2 * mv1.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar234 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar235 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar234 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar125 = (-2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.Scalar123 * mv2.Scalar5 - mv2.Scalar3 * mv1.Scalar125 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.Scalar124 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar125 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar125 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar135 = (2 * mv2.Scalar1 * mv1.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.Scalar123 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar135 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar134 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar135 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar135 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar235 = (mv2.Scalar2 * mv1.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar235 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar234 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar235 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar235 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar145 = (2 * mv2.Scalar1 * mv1.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.Scalar124 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.Scalar125 * mv2.Scalar4 - mv2.Scalar2 * mv1.Scalar145 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.Scalar134 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar135 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar145 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar145 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar145 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar245 = (mv2.Scalar2 * mv1.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar234 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar235 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar245 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar245 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar245 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar345 = (2 * mv2.Scalar2 * mv1.Scalar234 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar235 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar245 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar345 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar345 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar345 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDualOnDual(this Ga401KVector3 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (-2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar15 * mv1.Scalar234 * mv2.Scalar45 + 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.Scalar235 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.Scalar245 * mv2.Scalar15 + 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar35 + mv2.Scalar35 * mv1.Scalar123 * mv2.Scalar35 + mv2.Scalar34 * mv1.Scalar123 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar123 * mv2.Scalar25 + 2 * mv2.Scalar35 * mv1.Scalar124 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar45 + mv2.Scalar45 * mv1.Scalar123 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar124 = (-2 * mv2.Scalar12 * mv1.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.Scalar345 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar15 + 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar124 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.Scalar125 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar124 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar124 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar12 * mv1.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar25 - 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar23 + 2 * mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar15 - 2 * mv2.Scalar13 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.Scalar123 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.Scalar345 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar134 * mv2.Scalar24 + 2 * mv2.Scalar15 * mv1.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar25 + mv2.Scalar25 * mv1.Scalar134 * mv2.Scalar25 - mv2.Scalar35 * mv1.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar35 + mv2.Scalar45 * mv1.Scalar134 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar23 - mv2.Scalar35 * mv1.Scalar234 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar35 - mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar34 + mv2.Scalar24 * mv1.Scalar234 * mv2.Scalar24 + mv2.Scalar25 * mv1.Scalar234 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.Scalar235 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar125 = (-2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar45 + mv2.Scalar23 * mv1.Scalar125 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.Scalar145 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar125 * mv2.Scalar35 - 2 * mv2.Scalar14 * mv1.Scalar234 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar125 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar125 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar15 - mv2.Scalar45 * mv1.Scalar125 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.Scalar135 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar135 = (2 * mv2.Scalar15 * mv1.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar12 * mv1.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar123 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar45 - mv2.Scalar23 * mv1.Scalar135 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar135 * mv2.Scalar24 - mv2.Scalar35 * mv1.Scalar135 * mv2.Scalar35 - 2 * mv2.Scalar14 * mv1.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.Scalar145 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar14 - 2 * mv2.Scalar24 * mv1.Scalar123 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.Scalar124 * mv2.Scalar25 + mv2.Scalar34 * mv1.Scalar135 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar135 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar235 = (-mv2.Scalar23 * mv1.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar35 * mv1.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar235 * mv2.Scalar34 - mv2.Scalar45 * mv1.Scalar235 * mv2.Scalar45 - mv2.Scalar35 * mv1.Scalar235 * mv2.Scalar35 + mv2.Scalar25 * mv1.Scalar235 * mv2.Scalar25) * mv2NormSquaredInv,
            Scalar145 = (2 * mv2.Scalar12 * mv1.Scalar234 * mv2.Scalar35 + 2 * mv2.Scalar12 * mv1.Scalar345 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.Scalar245 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.Scalar124 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar134 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar15 + mv2.Scalar23 * mv1.Scalar145 * mv2.Scalar23 + 2 * mv2.Scalar15 * mv1.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.Scalar345 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.Scalar124 * mv2.Scalar45 - mv2.Scalar24 * mv1.Scalar145 * mv2.Scalar24 + mv2.Scalar35 * mv1.Scalar145 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar134 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar145 * mv2.Scalar34 + mv2.Scalar25 * mv1.Scalar145 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar145 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar245 = (-mv2.Scalar24 * mv1.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar34 + mv2.Scalar35 * mv1.Scalar245 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.Scalar234 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar245 * mv2.Scalar23 + mv2.Scalar25 * mv1.Scalar245 * mv2.Scalar25 - mv2.Scalar34 * mv1.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.Scalar234 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar245 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.Scalar345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar345 = (mv2.Scalar24 * mv1.Scalar345 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar235 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar345 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar345 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar345 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.Scalar235 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar345 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDualOnDual(this Ga401KVector3 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (-2 * mv2.Scalar123 * mv1.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar123 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar123 * mv1.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.Scalar245 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar345 + mv2.Scalar345 * mv1.Scalar123 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar123 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.Scalar245 * mv2.Scalar125 - 2 * mv2.Scalar125 * mv1.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.Scalar234 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar123 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar123 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar124 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar124 = (2 * mv2.Scalar123 * mv1.Scalar245 * mv2.Scalar235 - 2 * mv2.Scalar124 * mv1.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.Scalar345 * mv2.Scalar245 - mv2.Scalar245 * mv1.Scalar124 * mv2.Scalar245 + mv2.Scalar234 * mv1.Scalar124 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar124 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar145 - mv2.Scalar345 * mv1.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar245 * mv1.Scalar134 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar134 = (-2 * mv2.Scalar123 * mv1.Scalar235 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.Scalar345 * mv2.Scalar345 - mv2.Scalar234 * mv1.Scalar134 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar134 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar234 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar135 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar135 - 2 * mv2.Scalar235 * mv1.Scalar123 * mv2.Scalar345 - mv2.Scalar345 * mv1.Scalar134 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.Scalar234 * mv2.Scalar245 + mv2.Scalar245 * mv1.Scalar134 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar345 - mv2.Scalar235 * mv1.Scalar234 * mv2.Scalar235 - mv2.Scalar345 * mv1.Scalar234 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar125 = (2 * mv2.Scalar123 * mv1.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.Scalar234 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar125 + mv2.Scalar235 * mv1.Scalar125 * mv2.Scalar235 - mv2.Scalar234 * mv1.Scalar125 * mv2.Scalar234 + 2 * mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar345 + mv2.Scalar245 * mv1.Scalar125 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar125 * mv2.Scalar345 + 2 * mv2.Scalar245 * mv1.Scalar135 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar135 = (-2 * mv2.Scalar145 * mv1.Scalar235 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.Scalar235 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.Scalar123 * mv2.Scalar345 + mv2.Scalar234 * mv1.Scalar135 * mv2.Scalar234 + mv2.Scalar345 * mv1.Scalar135 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.Scalar245 * mv2.Scalar245 - mv2.Scalar235 * mv1.Scalar135 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar135 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar145) * mv2NormSquaredInv,
            Scalar235 = (mv2.Scalar234 * mv1.Scalar235 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar235 * mv2.Scalar235 - mv2.Scalar245 * mv1.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar245 + mv2.Scalar345 * mv1.Scalar235 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar145 = (-2 * mv2.Scalar124 * mv1.Scalar234 * mv2.Scalar345 - 2 * mv2.Scalar124 * mv1.Scalar345 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.Scalar234 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.Scalar134 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar145 - mv2.Scalar234 * mv1.Scalar145 * mv2.Scalar234 - mv2.Scalar245 * mv1.Scalar145 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar145 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar125 * mv1.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.Scalar245 * mv2.Scalar235 + mv2.Scalar235 * mv1.Scalar145 * mv2.Scalar235 - 2 * mv2.Scalar145 * mv1.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar245 = (-mv2.Scalar234 * mv1.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.Scalar234 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar245 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.Scalar345 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar245 * mv2.Scalar235) * mv2NormSquaredInv,
            Scalar345 = (mv2.Scalar234 * mv1.Scalar345 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar345 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.Scalar235 * mv2.Scalar345 - mv2.Scalar345 * mv1.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDualOnDual(this Ga401KVector3 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector3.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector3
        {
            Scalar123 = (2 * mv2.Scalar1234 * mv1.Scalar235 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar123 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar124 = (2 * mv2.Scalar1234 * mv1.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar124 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar134 = (2 * mv2.Scalar1234 * mv1.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar134 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar234 = (-mv2.Scalar2345 * mv1.Scalar234 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar125 = (2 * mv2.Scalar1235 * mv1.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar125 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar135 = (2 * mv2.Scalar1235 * mv1.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar135 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar235 = (-mv2.Scalar2345 * mv1.Scalar235 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar145 = (2 * mv2.Scalar1245 * mv1.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.Scalar245 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.Scalar145 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar245 = (-mv2.Scalar2345 * mv1.Scalar245 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar345 = (-mv2.Scalar2345 * mv1.Scalar345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector3 ReflectDualOnDual(this Ga401KVector3 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector3.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDualOnDual(this Ga401KVector4 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (-2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar5 + mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar5 + mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar5 + mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar1235 * mv2.Scalar5 - mv2.Scalar5 * mv1.Scalar1234 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1235 = (2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar4 + mv2.Scalar2 * mv1.Scalar1235 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar4 + mv2.Scalar3 * mv1.Scalar1235 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.Scalar1234 * mv2.Scalar5 - mv2.Scalar4 * mv1.Scalar1235 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar1235 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1245 = (-2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar3 + mv2.Scalar2 * mv1.Scalar1245 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.Scalar1234 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.Scalar1235 * mv2.Scalar4 - mv2.Scalar3 * mv1.Scalar1245 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar1245 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar1245 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar1345 = (2 * mv2.Scalar1 * mv1.Scalar2345 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.Scalar1234 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.Scalar1235 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.Scalar1245 * mv2.Scalar3 - mv2.Scalar2 * mv1.Scalar1345 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar1345 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar1345 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar1345 * mv2.Scalar5) * mv2NormSquaredInv,
            Scalar2345 = (mv2.Scalar2 * mv1.Scalar2345 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar2345 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar2345 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar2345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDualOnDual(this Ga401KVector4 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (-mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar1345 * mv2.Scalar35 + mv2.Scalar24 * mv1.Scalar1234 * mv2.Scalar24 + mv2.Scalar25 * mv1.Scalar1234 * mv2.Scalar25 + mv2.Scalar45 * mv1.Scalar1234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.Scalar1235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar45 - mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar1234 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.Scalar1235 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar1235 = (-2 * mv2.Scalar12 * mv1.Scalar2345 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.Scalar2345 * mv2.Scalar34 - mv2.Scalar35 * mv1.Scalar1235 * mv2.Scalar35 - mv2.Scalar23 * mv1.Scalar1235 * mv2.Scalar23 - mv2.Scalar24 * mv1.Scalar1235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.Scalar1245 * mv2.Scalar24 + mv2.Scalar34 * mv1.Scalar1235 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.Scalar2345 * mv2.Scalar45 + mv2.Scalar25 * mv1.Scalar1235 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar1235 * mv2.Scalar45 - 2 * mv2.Scalar35 * mv1.Scalar1245 * mv2.Scalar45) * mv2NormSquaredInv,
            Scalar1245 = (2 * mv2.Scalar23 * mv1.Scalar1234 * mv2.Scalar25 + mv2.Scalar23 * mv1.Scalar1245 * mv2.Scalar23 + mv2.Scalar25 * mv1.Scalar1245 * mv2.Scalar25 - mv2.Scalar24 * mv1.Scalar1245 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar1245 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.Scalar1234 * mv2.Scalar45 - mv2.Scalar45 * mv1.Scalar1245 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar1245 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar1345 = (-2 * mv2.Scalar13 * mv1.Scalar2345 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.Scalar1235 * mv2.Scalar34 - mv2.Scalar23 * mv1.Scalar1345 * mv2.Scalar23 - mv2.Scalar34 * mv1.Scalar1345 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.Scalar2345 * mv2.Scalar24 + mv2.Scalar24 * mv1.Scalar1345 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.Scalar2345 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.Scalar1235 * mv2.Scalar45 - mv2.Scalar25 * mv1.Scalar1345 * mv2.Scalar25 - mv2.Scalar45 * mv1.Scalar1345 * mv2.Scalar45 + mv2.Scalar35 * mv1.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv,
            Scalar2345 = (-mv2.Scalar23 * mv1.Scalar2345 * mv2.Scalar23 - mv2.Scalar25 * mv1.Scalar2345 * mv2.Scalar25 - mv2.Scalar34 * mv1.Scalar2345 * mv2.Scalar34 - mv2.Scalar45 * mv1.Scalar2345 * mv2.Scalar45 + mv2.Scalar24 * mv1.Scalar2345 * mv2.Scalar24 + mv2.Scalar35 * mv1.Scalar2345 * mv2.Scalar35) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDualOnDual(this Ga401KVector4 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar234 + mv2.Scalar235 * mv1.Scalar1234 * mv2.Scalar235 + mv2.Scalar345 * mv1.Scalar1234 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.Scalar1235 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.Scalar1345 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar1234 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar1235 = (-2 * mv2.Scalar123 * mv1.Scalar2345 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar1235 * mv2.Scalar234 + 2 * mv2.Scalar125 * mv1.Scalar2345 * mv2.Scalar245 - 2 * mv2.Scalar135 * mv1.Scalar2345 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar1235 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar1235 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.Scalar1245 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar1235 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar1245 = (2 * mv2.Scalar234 * mv1.Scalar1234 * mv2.Scalar245 + mv2.Scalar234 * mv1.Scalar1245 * mv2.Scalar234 + mv2.Scalar245 * mv1.Scalar1245 * mv2.Scalar245 - mv2.Scalar235 * mv1.Scalar1245 * mv2.Scalar235 + mv2.Scalar345 * mv1.Scalar1245 * mv2.Scalar345 + 2 * mv2.Scalar245 * mv1.Scalar1345 * mv2.Scalar345) * mv2NormSquaredInv,
            Scalar1345 = (-2 * mv2.Scalar145 * mv1.Scalar2345 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.Scalar2345 * mv2.Scalar234 - mv2.Scalar234 * mv1.Scalar1345 * mv2.Scalar234 + 2 * mv2.Scalar135 * mv1.Scalar2345 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.Scalar1235 * mv2.Scalar345 + mv2.Scalar235 * mv1.Scalar1345 * mv2.Scalar235 + mv2.Scalar345 * mv1.Scalar1345 * mv2.Scalar345 - mv2.Scalar245 * mv1.Scalar1345 * mv2.Scalar245) * mv2NormSquaredInv,
            Scalar2345 = (-mv2.Scalar234 * mv1.Scalar2345 * mv2.Scalar234 - mv2.Scalar245 * mv1.Scalar2345 * mv2.Scalar245 + mv2.Scalar235 * mv1.Scalar2345 * mv2.Scalar235 + mv2.Scalar345 * mv1.Scalar2345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDualOnDual(this Ga401KVector4 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector4.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector4
        {
            Scalar1234 = (2 * mv2.Scalar1234 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1234 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1235 = (2 * mv2.Scalar1235 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1235 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1245 = (2 * mv2.Scalar1245 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1245 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar1345 = (2 * mv2.Scalar1345 * mv1.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.Scalar1345 * mv2.Scalar2345) * mv2NormSquaredInv,
            Scalar2345 = (mv2.Scalar2345 * mv1.Scalar2345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector4 ReflectDualOnDual(this Ga401KVector4 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector4.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDualOnDual(this Ga401KVector5 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (mv2.Scalar2 * mv1.Scalar12345 * mv2.Scalar2 + mv2.Scalar3 * mv1.Scalar12345 * mv2.Scalar3 + mv2.Scalar4 * mv1.Scalar12345 * mv2.Scalar4 + mv2.Scalar5 * mv1.Scalar12345 * mv2.Scalar5) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDualOnDual(this Ga401KVector5 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (-mv2.Scalar23 * mv1.Scalar12345 * mv2.Scalar23 + mv2.Scalar24 * mv1.Scalar12345 * mv2.Scalar24 - mv2.Scalar34 * mv1.Scalar12345 * mv2.Scalar34 - mv2.Scalar25 * mv1.Scalar12345 * mv2.Scalar25 + mv2.Scalar35 * mv1.Scalar12345 * mv2.Scalar35 - mv2.Scalar45 * mv1.Scalar12345 * mv2.Scalar45) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDualOnDual(this Ga401KVector5 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (mv2.Scalar234 * mv1.Scalar12345 * mv2.Scalar234 - mv2.Scalar235 * mv1.Scalar12345 * mv2.Scalar235 + mv2.Scalar245 * mv1.Scalar12345 * mv2.Scalar245 - mv2.Scalar345 * mv1.Scalar12345 * mv2.Scalar345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDualOnDual(this Ga401KVector5 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector5.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        return new Ga401KVector5
        {
            Scalar12345 = (mv2.Scalar2345 * mv1.Scalar12345 * mv2.Scalar2345) * mv2NormSquaredInv
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector5 ReflectDualOnDual(this Ga401KVector5 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector5.Zero;
    }
    
    public static Ga401Multivector ReflectDualOnDual(this Ga401Multivector mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar2 * mv1.KVector0.Scalar * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector0.Scalar * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector0.Scalar * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector0.Scalar * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar1 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector1.Scalar1 * mv2.Scalar2 - mv2.Scalar3 * mv1.KVector1.Scalar1 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar1 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector1.Scalar1 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector1.Scalar2 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector1.Scalar2 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[4] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector1.Scalar3 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector1.Scalar3 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector1.Scalar3 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[8] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector1.Scalar4 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector1.Scalar4 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector1.Scalar4 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector1.Scalar5 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector1.Scalar4 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[16] += (2 * mv2.Scalar2 * mv1.KVector1.Scalar2 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector1.Scalar5 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector1.Scalar3 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector1.Scalar5 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.KVector1.Scalar4 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector1.Scalar5 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector1.Scalar5 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar4 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar25 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar15 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector2.Scalar12 * mv2.Scalar3 - mv2.Scalar4 * mv1.KVector2.Scalar12 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar12 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar35 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar13 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar15 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector2.Scalar13 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar13 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar25 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector2.Scalar23 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector2.Scalar23 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar34 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector2.Scalar45 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector2.Scalar14 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector2.Scalar14 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar14 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector2.Scalar15 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector2.Scalar14 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector2.Scalar24 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar24 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector2.Scalar25 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector2.Scalar24 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[12] += (-2 * mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar34 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector2.Scalar34 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector2.Scalar34 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector2.Scalar35 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector2.Scalar34 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[17] += (2 * mv2.Scalar1 * mv1.KVector2.Scalar25 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar35 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector2.Scalar45 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar12 * mv2.Scalar5 - mv2.Scalar2 * mv1.KVector2.Scalar15 * mv2.Scalar2 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar13 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector2.Scalar15 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.KVector2.Scalar14 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector2.Scalar15 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar15 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[18] += (mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar23 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector2.Scalar25 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.KVector2.Scalar24 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector2.Scalar25 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar25 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[20] += (-2 * mv2.Scalar2 * mv1.KVector2.Scalar23 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector2.Scalar35 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector2.Scalar35 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector2.Scalar34 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector2.Scalar35 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar35 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[24] += (-2 * mv2.Scalar2 * mv1.KVector2.Scalar24 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector2.Scalar25 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector2.Scalar45 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector2.Scalar34 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector2.Scalar35 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector2.Scalar45 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector2.Scalar45 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector2.Scalar45 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar125 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar123 * mv2.Scalar4 - mv2.Scalar5 * mv1.KVector3.Scalar123 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[11] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar3 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar124 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar124 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar125 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector3.Scalar124 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar134 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar134 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector3.Scalar134 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar135 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector3.Scalar134 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[14] += (mv2.Scalar2 * mv1.KVector3.Scalar234 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector3.Scalar234 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector3.Scalar234 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar235 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector3.Scalar234 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[19] += (-2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar3 - 2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar123 * mv2.Scalar5 - mv2.Scalar3 * mv1.KVector3.Scalar125 * mv2.Scalar3 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar124 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar125 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar125 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[21] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar4 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar123 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar135 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar135 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar134 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar135 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar135 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[22] += (mv2.Scalar2 * mv1.KVector3.Scalar235 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector3.Scalar235 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector3.Scalar234 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector3.Scalar235 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar235 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[25] += (2 * mv2.Scalar1 * mv1.KVector3.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar1 * mv1.KVector3.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar124 * mv2.Scalar5 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar125 * mv2.Scalar4 - mv2.Scalar2 * mv1.KVector3.Scalar145 * mv2.Scalar2 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar134 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar135 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar145 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar145 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar145 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[26] += (mv2.Scalar2 * mv1.KVector3.Scalar245 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector3.Scalar234 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector3.Scalar235 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector3.Scalar245 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar245 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar245 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[28] += (2 * mv2.Scalar2 * mv1.KVector3.Scalar234 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector3.Scalar235 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector3.Scalar245 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector3.Scalar345 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector3.Scalar345 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector3.Scalar345 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector3.Scalar345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar5 + mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar5 + mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar5 + mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector4.Scalar1235 * mv2.Scalar5 - mv2.Scalar5 * mv1.KVector4.Scalar1234 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[23] += (2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar4 + mv2.Scalar2 * mv1.KVector4.Scalar1235 * mv2.Scalar2 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar4 + mv2.Scalar3 * mv1.KVector4.Scalar1235 * mv2.Scalar3 + 2 * mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar4 + 2 * mv2.Scalar4 * mv1.KVector4.Scalar1234 * mv2.Scalar5 - mv2.Scalar4 * mv1.KVector4.Scalar1235 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector4.Scalar1235 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[27] += (-2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar3 + mv2.Scalar2 * mv1.KVector4.Scalar1245 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar3 - 2 * mv2.Scalar3 * mv1.KVector4.Scalar1234 * mv2.Scalar5 + 2 * mv2.Scalar3 * mv1.KVector4.Scalar1235 * mv2.Scalar4 - mv2.Scalar3 * mv1.KVector4.Scalar1245 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector4.Scalar1245 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector4.Scalar1245 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[29] += (2 * mv2.Scalar1 * mv1.KVector4.Scalar2345 * mv2.Scalar2 + 2 * mv2.Scalar2 * mv1.KVector4.Scalar1234 * mv2.Scalar5 - 2 * mv2.Scalar2 * mv1.KVector4.Scalar1235 * mv2.Scalar4 + 2 * mv2.Scalar2 * mv1.KVector4.Scalar1245 * mv2.Scalar3 - mv2.Scalar2 * mv1.KVector4.Scalar1345 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector4.Scalar1345 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector4.Scalar1345 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector4.Scalar1345 * mv2.Scalar5) * mv2NormSquaredInv;
            tempScalar[30] += (mv2.Scalar2 * mv1.KVector4.Scalar2345 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector4.Scalar2345 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector4.Scalar2345 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector4.Scalar2345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (mv2.Scalar2 * mv1.KVector5.Scalar12345 * mv2.Scalar2 + mv2.Scalar3 * mv1.KVector5.Scalar12345 * mv2.Scalar3 + mv2.Scalar4 * mv1.KVector5.Scalar12345 * mv2.Scalar4 + mv2.Scalar5 * mv1.KVector5.Scalar12345 * mv2.Scalar5) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDualOnDual(this Ga401Multivector mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar23 * mv1.KVector0.Scalar * mv2.Scalar23 + mv2.Scalar45 * mv1.KVector0.Scalar * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector0.Scalar * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector0.Scalar * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector0.Scalar * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector0.Scalar * mv2.Scalar35) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar12 * mv1.KVector1.Scalar3 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar4 * mv2.Scalar24 + 2 * mv2.Scalar12 * mv1.KVector1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar15 * mv1.KVector1.Scalar2 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.KVector1.Scalar5 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector1.Scalar1 * mv2.Scalar23 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar2 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector1.Scalar5 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector1.Scalar1 * mv2.Scalar24 + mv2.Scalar45 * mv1.KVector1.Scalar1 * mv2.Scalar45 + mv2.Scalar34 * mv1.KVector1.Scalar1 * mv2.Scalar34 + 2 * mv2.Scalar15 * mv1.KVector1.Scalar3 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.KVector1.Scalar4 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector1.Scalar1 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector1.Scalar1 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[2] += (2 * mv2.Scalar25 * mv1.KVector1.Scalar4 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector1.Scalar2 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector1.Scalar2 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector1.Scalar2 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector1.Scalar2 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector1.Scalar2 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector1.Scalar2 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.KVector1.Scalar3 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[4] += (mv2.Scalar45 * mv1.KVector1.Scalar3 * mv2.Scalar45 + mv2.Scalar34 * mv1.KVector1.Scalar3 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector1.Scalar3 * mv2.Scalar35 - mv2.Scalar23 * mv1.KVector1.Scalar3 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector1.Scalar3 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector1.Scalar3 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar25 + 2 * mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar45 + 2 * mv2.Scalar35 * mv1.KVector1.Scalar4 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[8] += (mv2.Scalar23 * mv1.KVector1.Scalar4 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector1.Scalar4 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector1.Scalar4 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector1.Scalar4 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector1.Scalar4 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector1.Scalar4 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[16] += (-mv2.Scalar45 * mv1.KVector1.Scalar5 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector1.Scalar5 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector1.Scalar5 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector1.Scalar5 * mv2.Scalar25 + mv2.Scalar24 * mv1.KVector1.Scalar5 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector1.Scalar5 * mv2.Scalar34) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (mv2.Scalar23 * mv1.KVector2.Scalar12 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar34 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector2.Scalar12 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector2.Scalar12 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector2.Scalar12 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector2.Scalar12 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector2.Scalar12 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar13 * mv2.Scalar35 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar14 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar24 * mv2.Scalar34 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar23 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar13 * mv1.KVector2.Scalar25 * mv2.Scalar25 - mv2.Scalar23 * mv1.KVector2.Scalar13 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector2.Scalar13 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector2.Scalar13 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar23 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar14 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar15 + mv2.Scalar34 * mv1.KVector2.Scalar13 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar45 + mv2.Scalar35 * mv1.KVector2.Scalar13 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector2.Scalar13 * mv2.Scalar45 + 2 * mv2.Scalar35 * mv1.KVector2.Scalar14 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[6] += (-mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector2.Scalar23 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector2.Scalar23 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector2.Scalar23 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar25 + mv2.Scalar34 * mv1.KVector2.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar35 * mv1.KVector2.Scalar24 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar45 + mv2.Scalar35 * mv1.KVector2.Scalar23 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar34 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar25 * mv2.Scalar45 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar24 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar34 * mv2.Scalar35 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar35 * mv2.Scalar45 + mv2.Scalar45 * mv1.KVector2.Scalar14 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector2.Scalar14 * mv2.Scalar23 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar34 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.KVector2.Scalar35 * mv2.Scalar35 - mv2.Scalar24 * mv1.KVector2.Scalar14 * mv2.Scalar24 - mv2.Scalar25 * mv1.KVector2.Scalar14 * mv2.Scalar25 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar15 - mv2.Scalar34 * mv1.KVector2.Scalar14 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector2.Scalar14 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar15) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar34 + mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar35 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector2.Scalar24 * mv2.Scalar35 - mv2.Scalar34 * mv1.KVector2.Scalar24 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector2.Scalar24 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector2.Scalar24 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[12] += (mv2.Scalar25 * mv1.KVector2.Scalar34 * mv2.Scalar25 + mv2.Scalar24 * mv1.KVector2.Scalar34 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.KVector2.Scalar23 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector2.Scalar34 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector2.Scalar34 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector2.Scalar34 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector2.Scalar34 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[17] += (2 * mv2.Scalar12 * mv1.KVector2.Scalar35 * mv2.Scalar23 + 2 * mv2.Scalar12 * mv1.KVector2.Scalar45 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar25 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector2.Scalar45 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector2.Scalar15 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector2.Scalar15 * mv2.Scalar23 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar35 * mv2.Scalar35 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar25 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector2.Scalar35 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector2.Scalar15 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector2.Scalar15 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector2.Scalar15 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar45 * mv2.Scalar45 - 2 * mv2.Scalar15 * mv1.KVector2.Scalar25 * mv2.Scalar25 - mv2.Scalar25 * mv1.KVector2.Scalar15 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[18] += (-mv2.Scalar35 * mv1.KVector2.Scalar25 * mv2.Scalar35 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar35 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector2.Scalar25 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector2.Scalar25 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.KVector2.Scalar45 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector2.Scalar25 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector2.Scalar25 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector2.Scalar25 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[20] += (2 * mv2.Scalar34 * mv1.KVector2.Scalar24 * mv2.Scalar25 + mv2.Scalar25 * mv1.KVector2.Scalar35 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector2.Scalar35 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar23 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector2.Scalar35 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector2.Scalar35 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector2.Scalar35 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.KVector2.Scalar45 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar23 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector2.Scalar35 * mv2.Scalar34) * mv2NormSquaredInv;
            tempScalar[24] += (2 * mv2.Scalar23 * mv1.KVector2.Scalar24 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector2.Scalar34 * mv2.Scalar25 + mv2.Scalar23 * mv1.KVector2.Scalar45 * mv2.Scalar23 - 2 * mv2.Scalar24 * mv1.KVector2.Scalar24 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector2.Scalar45 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector2.Scalar45 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector2.Scalar34 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector2.Scalar45 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector2.Scalar45 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector2.Scalar45 * mv2.Scalar45) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (mv2.Scalar45 * mv1.KVector3.Scalar123 * mv2.Scalar45 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar24 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar34 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar235 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar245 * mv2.Scalar15 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar35 + mv2.Scalar35 * mv1.KVector3.Scalar123 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector3.Scalar123 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector3.Scalar123 * mv2.Scalar25 + 2 * mv2.Scalar35 * mv1.KVector3.Scalar124 * mv2.Scalar45 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar45 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar234 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[11] += (mv2.Scalar45 * mv1.KVector3.Scalar124 * mv2.Scalar45 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar345 * mv2.Scalar25 + mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar15 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector3.Scalar124 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar125 * mv2.Scalar25 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector3.Scalar124 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar12 * mv1.KVector3.Scalar235 * mv2.Scalar45 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar25 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar23 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar15 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar34 + 2 * mv2.Scalar25 * mv1.KVector3.Scalar123 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar24 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar14 * mv1.KVector3.Scalar345 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector3.Scalar134 * mv2.Scalar24 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar25 + mv2.Scalar25 * mv1.KVector3.Scalar134 * mv2.Scalar25 - mv2.Scalar35 * mv1.KVector3.Scalar134 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar35 + mv2.Scalar45 * mv1.KVector3.Scalar134 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar35 - mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar34 + mv2.Scalar24 * mv1.KVector3.Scalar234 * mv2.Scalar24 + mv2.Scalar25 * mv1.KVector3.Scalar234 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector3.Scalar234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar45 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar235 * mv2.Scalar35 - mv2.Scalar35 * mv1.KVector3.Scalar234 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[19] += (-2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar45 + mv2.Scalar23 * mv1.KVector3.Scalar125 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector3.Scalar125 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar34 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar145 * mv2.Scalar45 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar234 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector3.Scalar125 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector3.Scalar125 * mv2.Scalar34 + 2 * mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar34 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar15 - mv2.Scalar35 * mv1.KVector3.Scalar125 * mv2.Scalar35 - mv2.Scalar45 * mv1.KVector3.Scalar125 * mv2.Scalar45 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar135 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[21] += (-mv2.Scalar45 * mv1.KVector3.Scalar135 * mv2.Scalar45 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar235 * mv2.Scalar25 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar123 * mv2.Scalar35 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar45 - mv2.Scalar23 * mv1.KVector3.Scalar135 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector3.Scalar135 * mv2.Scalar24 - mv2.Scalar35 * mv1.KVector3.Scalar135 * mv2.Scalar35 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.KVector3.Scalar145 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar14 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar123 * mv2.Scalar45 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar35 + 2 * mv2.Scalar34 * mv1.KVector3.Scalar124 * mv2.Scalar25 + mv2.Scalar34 * mv1.KVector3.Scalar135 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector3.Scalar135 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[22] += (-mv2.Scalar23 * mv1.KVector3.Scalar235 * mv2.Scalar23 - 2 * mv2.Scalar35 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector3.Scalar235 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar24 + mv2.Scalar34 * mv1.KVector3.Scalar235 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector3.Scalar235 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector3.Scalar235 * mv2.Scalar35 + mv2.Scalar25 * mv1.KVector3.Scalar235 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[25] += (2 * mv2.Scalar12 * mv1.KVector3.Scalar234 * mv2.Scalar35 + 2 * mv2.Scalar12 * mv1.KVector3.Scalar345 * mv2.Scalar23 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar234 * mv2.Scalar25 + 2 * mv2.Scalar13 * mv1.KVector3.Scalar245 * mv2.Scalar23 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar124 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar134 * mv2.Scalar25 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar15 + mv2.Scalar23 * mv1.KVector3.Scalar145 * mv2.Scalar23 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar245 * mv2.Scalar25 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar345 * mv2.Scalar34 - 2 * mv2.Scalar14 * mv1.KVector3.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar124 * mv2.Scalar45 - mv2.Scalar24 * mv1.KVector3.Scalar145 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector3.Scalar145 * mv2.Scalar35 + 2 * mv2.Scalar15 * mv1.KVector3.Scalar345 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar134 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector3.Scalar145 * mv2.Scalar34 + mv2.Scalar25 * mv1.KVector3.Scalar145 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector3.Scalar145 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[26] += (2 * mv2.Scalar25 * mv1.KVector3.Scalar345 * mv2.Scalar35 - mv2.Scalar24 * mv1.KVector3.Scalar245 * mv2.Scalar24 - 2 * mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector3.Scalar245 * mv2.Scalar35 + 2 * mv2.Scalar23 * mv1.KVector3.Scalar234 * mv2.Scalar25 + mv2.Scalar23 * mv1.KVector3.Scalar245 * mv2.Scalar23 + mv2.Scalar25 * mv1.KVector3.Scalar245 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector3.Scalar245 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector3.Scalar245 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.KVector3.Scalar234 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[28] += (mv2.Scalar35 * mv1.KVector3.Scalar345 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector3.Scalar345 * mv2.Scalar24 - 2 * mv2.Scalar23 * mv1.KVector3.Scalar235 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector3.Scalar345 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector3.Scalar345 * mv2.Scalar34 - mv2.Scalar25 * mv1.KVector3.Scalar345 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.KVector3.Scalar235 * mv2.Scalar45 - mv2.Scalar45 * mv1.KVector3.Scalar345 * mv2.Scalar45) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (-mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector4.Scalar1345 * mv2.Scalar35 + mv2.Scalar24 * mv1.KVector4.Scalar1234 * mv2.Scalar24 + mv2.Scalar25 * mv1.KVector4.Scalar1234 * mv2.Scalar25 + mv2.Scalar45 * mv1.KVector4.Scalar1234 * mv2.Scalar45 + 2 * mv2.Scalar24 * mv1.KVector4.Scalar1235 * mv2.Scalar25 + 2 * mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar45 - mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar34 - mv2.Scalar35 * mv1.KVector4.Scalar1234 * mv2.Scalar35 - 2 * mv2.Scalar34 * mv1.KVector4.Scalar1235 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[23] += (-2 * mv2.Scalar12 * mv1.KVector4.Scalar2345 * mv2.Scalar24 + 2 * mv2.Scalar13 * mv1.KVector4.Scalar2345 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector4.Scalar1235 * mv2.Scalar23 - mv2.Scalar24 * mv1.KVector4.Scalar1235 * mv2.Scalar24 - 2 * mv2.Scalar35 * mv1.KVector4.Scalar1245 * mv2.Scalar45 - 2 * mv2.Scalar23 * mv1.KVector4.Scalar1245 * mv2.Scalar24 - mv2.Scalar45 * mv1.KVector4.Scalar1235 * mv2.Scalar45 - mv2.Scalar35 * mv1.KVector4.Scalar1235 * mv2.Scalar35 + mv2.Scalar34 * mv1.KVector4.Scalar1235 * mv2.Scalar34 - 2 * mv2.Scalar15 * mv1.KVector4.Scalar2345 * mv2.Scalar45 + mv2.Scalar25 * mv1.KVector4.Scalar1235 * mv2.Scalar25) * mv2NormSquaredInv;
            tempScalar[27] += (2 * mv2.Scalar23 * mv1.KVector4.Scalar1234 * mv2.Scalar25 + mv2.Scalar35 * mv1.KVector4.Scalar1245 * mv2.Scalar35 + mv2.Scalar23 * mv1.KVector4.Scalar1245 * mv2.Scalar23 + mv2.Scalar25 * mv1.KVector4.Scalar1245 * mv2.Scalar25 - mv2.Scalar24 * mv1.KVector4.Scalar1245 * mv2.Scalar24 - mv2.Scalar34 * mv1.KVector4.Scalar1245 * mv2.Scalar34 - 2 * mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar34 - 2 * mv2.Scalar34 * mv1.KVector4.Scalar1234 * mv2.Scalar45 - mv2.Scalar45 * mv1.KVector4.Scalar1245 * mv2.Scalar45 + 2 * mv2.Scalar25 * mv1.KVector4.Scalar1345 * mv2.Scalar35) * mv2NormSquaredInv;
            tempScalar[29] += (mv2.Scalar35 * mv1.KVector4.Scalar1345 * mv2.Scalar35 - 2 * mv2.Scalar13 * mv1.KVector4.Scalar2345 * mv2.Scalar23 - 2 * mv2.Scalar23 * mv1.KVector4.Scalar1235 * mv2.Scalar34 - mv2.Scalar23 * mv1.KVector4.Scalar1345 * mv2.Scalar23 - mv2.Scalar34 * mv1.KVector4.Scalar1345 * mv2.Scalar34 + 2 * mv2.Scalar14 * mv1.KVector4.Scalar2345 * mv2.Scalar24 + mv2.Scalar24 * mv1.KVector4.Scalar1345 * mv2.Scalar24 - 2 * mv2.Scalar15 * mv1.KVector4.Scalar2345 * mv2.Scalar25 - 2 * mv2.Scalar25 * mv1.KVector4.Scalar1235 * mv2.Scalar45 - mv2.Scalar25 * mv1.KVector4.Scalar1345 * mv2.Scalar25 - mv2.Scalar45 * mv1.KVector4.Scalar1345 * mv2.Scalar45) * mv2NormSquaredInv;
            tempScalar[30] += (-mv2.Scalar23 * mv1.KVector4.Scalar2345 * mv2.Scalar23 - mv2.Scalar25 * mv1.KVector4.Scalar2345 * mv2.Scalar25 - mv2.Scalar34 * mv1.KVector4.Scalar2345 * mv2.Scalar34 - mv2.Scalar45 * mv1.KVector4.Scalar2345 * mv2.Scalar45 + mv2.Scalar24 * mv1.KVector4.Scalar2345 * mv2.Scalar24 + mv2.Scalar35 * mv1.KVector4.Scalar2345 * mv2.Scalar35) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (-mv2.Scalar34 * mv1.KVector5.Scalar12345 * mv2.Scalar34 + mv2.Scalar35 * mv1.KVector5.Scalar12345 * mv2.Scalar35 - mv2.Scalar23 * mv1.KVector5.Scalar12345 * mv2.Scalar23 + mv2.Scalar24 * mv1.KVector5.Scalar12345 * mv2.Scalar24 - mv2.Scalar45 * mv1.KVector5.Scalar12345 * mv2.Scalar45 - mv2.Scalar25 * mv1.KVector5.Scalar12345 * mv2.Scalar25) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDualOnDual(this Ga401Multivector mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (-mv2.Scalar345 * mv1.KVector0.Scalar * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector0.Scalar * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector0.Scalar * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector0.Scalar * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar123 * mv1.KVector1.Scalar4 * mv2.Scalar234 + 2 * mv2.Scalar123 * mv1.KVector1.Scalar5 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.KVector1.Scalar3 * mv2.Scalar234 + 2 * mv2.Scalar124 * mv1.KVector1.Scalar5 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar134 * mv1.KVector1.Scalar5 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector1.Scalar1 * mv2.Scalar234 + 2 * mv2.Scalar125 * mv1.KVector1.Scalar3 * mv2.Scalar235 + 2 * mv2.Scalar125 * mv1.KVector1.Scalar4 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector1.Scalar4 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector1.Scalar1 * mv2.Scalar235 + 2 * mv2.Scalar145 * mv1.KVector1.Scalar2 * mv2.Scalar245 + 2 * mv2.Scalar145 * mv1.KVector1.Scalar3 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector1.Scalar1 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector1.Scalar1 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[2] += (mv2.Scalar234 * mv1.KVector1.Scalar2 * mv2.Scalar234 + 2 * mv2.Scalar245 * mv1.KVector1.Scalar3 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector1.Scalar2 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector1.Scalar2 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector1.Scalar2 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar234 * mv1.KVector1.Scalar3 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector1.Scalar3 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector1.Scalar3 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector1.Scalar3 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[8] += (-mv2.Scalar345 * mv1.KVector1.Scalar4 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector1.Scalar4 * mv2.Scalar245 + mv2.Scalar234 * mv1.KVector1.Scalar4 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector1.Scalar4 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[16] += (mv2.Scalar235 * mv1.KVector1.Scalar5 * mv2.Scalar235 + mv2.Scalar345 * mv1.KVector1.Scalar5 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector1.Scalar5 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector1.Scalar5 * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (-mv2.Scalar234 * mv1.KVector2.Scalar12 * mv2.Scalar234 - 2 * mv2.Scalar245 * mv1.KVector2.Scalar13 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector2.Scalar12 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector2.Scalar12 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector2.Scalar12 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[5] += (-2 * mv2.Scalar124 * mv1.KVector2.Scalar25 * mv2.Scalar345 + 2 * mv2.Scalar134 * mv1.KVector2.Scalar23 * mv2.Scalar234 + 2 * mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar145 + 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector2.Scalar13 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector2.Scalar13 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar245 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar145 - 2 * mv2.Scalar125 * mv1.KVector2.Scalar24 * mv2.Scalar345 + 2 * mv2.Scalar135 * mv1.KVector2.Scalar23 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector2.Scalar24 * mv2.Scalar245 + mv2.Scalar235 * mv1.KVector2.Scalar13 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector2.Scalar13 * mv2.Scalar245 + 2 * mv2.Scalar145 * mv1.KVector2.Scalar23 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar234 * mv1.KVector2.Scalar23 * mv2.Scalar234 - mv2.Scalar345 * mv1.KVector2.Scalar23 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar245 + mv2.Scalar235 * mv1.KVector2.Scalar23 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector2.Scalar23 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar123 * mv1.KVector2.Scalar25 * mv2.Scalar345 + 2 * mv2.Scalar123 * mv1.KVector2.Scalar35 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.KVector2.Scalar34 * mv2.Scalar234 + 2 * mv2.Scalar145 * mv1.KVector2.Scalar34 * mv2.Scalar345 - 2 * mv2.Scalar124 * mv1.KVector2.Scalar35 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar24 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar235 - mv2.Scalar234 * mv1.KVector2.Scalar14 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector2.Scalar14 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector2.Scalar24 * mv2.Scalar235 - 2 * mv2.Scalar125 * mv1.KVector2.Scalar34 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar135 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar125 + mv2.Scalar245 * mv1.KVector2.Scalar14 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector2.Scalar14 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.KVector2.Scalar24 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[10] += (-mv2.Scalar234 * mv1.KVector2.Scalar24 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector2.Scalar24 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector2.Scalar24 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector2.Scalar24 * mv2.Scalar245 + 2 * mv2.Scalar245 * mv1.KVector2.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[12] += (mv2.Scalar234 * mv1.KVector2.Scalar34 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector2.Scalar34 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector2.Scalar34 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.KVector2.Scalar23 * mv2.Scalar345 + mv2.Scalar345 * mv1.KVector2.Scalar34 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[17] += (-mv2.Scalar345 * mv1.KVector2.Scalar15 * mv2.Scalar345 + 2 * mv2.Scalar123 * mv1.KVector2.Scalar45 * mv2.Scalar234 + 2 * mv2.Scalar124 * mv1.KVector2.Scalar35 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector2.Scalar15 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.KVector2.Scalar25 * mv2.Scalar234 + mv2.Scalar234 * mv1.KVector2.Scalar15 * mv2.Scalar234 - 2 * mv2.Scalar145 * mv1.KVector2.Scalar35 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.KVector2.Scalar45 * mv2.Scalar245 - 2 * mv2.Scalar135 * mv1.KVector2.Scalar45 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.KVector2.Scalar35 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector2.Scalar25 * mv2.Scalar235 - 2 * mv2.Scalar145 * mv1.KVector2.Scalar25 * mv2.Scalar245 - mv2.Scalar245 * mv1.KVector2.Scalar15 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[18] += (-mv2.Scalar235 * mv1.KVector2.Scalar25 * mv2.Scalar235 - 2 * mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector2.Scalar25 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.KVector2.Scalar35 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector2.Scalar25 * mv2.Scalar234 - mv2.Scalar345 * mv1.KVector2.Scalar25 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[20] += (mv2.Scalar235 * mv1.KVector2.Scalar35 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.KVector2.Scalar23 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector2.Scalar35 * mv2.Scalar234 - mv2.Scalar345 * mv1.KVector2.Scalar35 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector2.Scalar35 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[24] += (-mv2.Scalar235 * mv1.KVector2.Scalar45 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar24 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector2.Scalar34 * mv2.Scalar245 + mv2.Scalar234 * mv1.KVector2.Scalar45 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector2.Scalar45 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector2.Scalar45 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (-2 * mv2.Scalar123 * mv1.KVector3.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar245 * mv2.Scalar235 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector3.Scalar123 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar245 * mv2.Scalar125 + mv2.Scalar345 * mv1.KVector3.Scalar123 * mv2.Scalar345 - 2 * mv2.Scalar125 * mv1.KVector3.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar135 * mv1.KVector3.Scalar234 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector3.Scalar123 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector3.Scalar123 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar124 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[11] += (2 * mv2.Scalar123 * mv1.KVector3.Scalar245 * mv2.Scalar235 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector3.Scalar124 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar345 * mv2.Scalar245 + mv2.Scalar234 * mv1.KVector3.Scalar124 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector3.Scalar124 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar145 - mv2.Scalar345 * mv1.KVector3.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar245 * mv1.KVector3.Scalar134 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[13] += (mv2.Scalar245 * mv1.KVector3.Scalar134 * mv2.Scalar245 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar235 * mv2.Scalar345 - 2 * mv2.Scalar123 * mv1.KVector3.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar345 * mv2.Scalar345 - mv2.Scalar234 * mv1.KVector3.Scalar134 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector3.Scalar134 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector3.Scalar234 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar135 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar135 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar123 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector3.Scalar134 * mv2.Scalar345 + 2 * mv2.Scalar145 * mv1.KVector3.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar345 - mv2.Scalar235 * mv1.KVector3.Scalar234 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector3.Scalar234 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector3.Scalar234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[19] += (2 * mv2.Scalar123 * mv1.KVector3.Scalar234 * mv2.Scalar245 + 2 * mv2.Scalar124 * mv1.KVector3.Scalar234 * mv2.Scalar235 + 2 * mv2.Scalar245 * mv1.KVector3.Scalar135 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar125 - mv2.Scalar234 * mv1.KVector3.Scalar125 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector3.Scalar125 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar345 + mv2.Scalar245 * mv1.KVector3.Scalar125 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector3.Scalar125 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[21] += (-2 * mv2.Scalar145 * mv1.KVector3.Scalar235 * mv2.Scalar245 + 2 * mv2.Scalar134 * mv1.KVector3.Scalar235 * mv2.Scalar234 + 2 * mv2.Scalar234 * mv1.KVector3.Scalar123 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector3.Scalar135 * mv2.Scalar234 + mv2.Scalar345 * mv1.KVector3.Scalar135 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.KVector3.Scalar245 * mv2.Scalar345 - 2 * mv2.Scalar135 * mv1.KVector3.Scalar235 * mv2.Scalar235 - 2 * mv2.Scalar135 * mv1.KVector3.Scalar245 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector3.Scalar135 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector3.Scalar135 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar145) * mv2NormSquaredInv;
            tempScalar[22] += (mv2.Scalar234 * mv1.KVector3.Scalar235 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector3.Scalar235 * mv2.Scalar235 - mv2.Scalar245 * mv1.KVector3.Scalar235 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar245 + mv2.Scalar345 * mv1.KVector3.Scalar235 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[25] += (-2 * mv2.Scalar124 * mv1.KVector3.Scalar234 * mv2.Scalar345 - 2 * mv2.Scalar124 * mv1.KVector3.Scalar345 * mv2.Scalar234 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar234 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector3.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar124 * mv2.Scalar345 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar134 * mv2.Scalar245 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar145 - mv2.Scalar234 * mv1.KVector3.Scalar145 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector3.Scalar145 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector3.Scalar145 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.KVector3.Scalar245 * mv2.Scalar245 + 2 * mv2.Scalar125 * mv1.KVector3.Scalar345 * mv2.Scalar235 + 2 * mv2.Scalar135 * mv1.KVector3.Scalar245 * mv2.Scalar235 + mv2.Scalar235 * mv1.KVector3.Scalar145 * mv2.Scalar235 - 2 * mv2.Scalar145 * mv1.KVector3.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv;
            tempScalar[26] += (-mv2.Scalar234 * mv1.KVector3.Scalar245 * mv2.Scalar234 - 2 * mv2.Scalar234 * mv1.KVector3.Scalar234 * mv2.Scalar245 - mv2.Scalar345 * mv1.KVector3.Scalar245 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector3.Scalar245 * mv2.Scalar245 - 2 * mv2.Scalar245 * mv1.KVector3.Scalar345 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector3.Scalar245 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[28] += (mv2.Scalar234 * mv1.KVector3.Scalar345 * mv2.Scalar234 - mv2.Scalar235 * mv1.KVector3.Scalar345 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector3.Scalar345 * mv2.Scalar245 - 2 * mv2.Scalar235 * mv1.KVector3.Scalar235 * mv2.Scalar345 - mv2.Scalar345 * mv1.KVector3.Scalar345 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar234 + mv2.Scalar235 * mv1.KVector4.Scalar1234 * mv2.Scalar235 + mv2.Scalar345 * mv1.KVector4.Scalar1234 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector4.Scalar1235 * mv2.Scalar235 + 2 * mv2.Scalar234 * mv1.KVector4.Scalar1345 * mv2.Scalar345 - mv2.Scalar245 * mv1.KVector4.Scalar1234 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[23] += (-2 * mv2.Scalar123 * mv1.KVector4.Scalar2345 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector4.Scalar1235 * mv2.Scalar234 - mv2.Scalar345 * mv1.KVector4.Scalar1235 * mv2.Scalar345 + 2 * mv2.Scalar125 * mv1.KVector4.Scalar2345 * mv2.Scalar245 - 2 * mv2.Scalar135 * mv1.KVector4.Scalar2345 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector4.Scalar1235 * mv2.Scalar235 + mv2.Scalar245 * mv1.KVector4.Scalar1235 * mv2.Scalar245 + 2 * mv2.Scalar235 * mv1.KVector4.Scalar1245 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[27] += (2 * mv2.Scalar245 * mv1.KVector4.Scalar1345 * mv2.Scalar345 + mv2.Scalar345 * mv1.KVector4.Scalar1245 * mv2.Scalar345 + 2 * mv2.Scalar234 * mv1.KVector4.Scalar1234 * mv2.Scalar245 + mv2.Scalar234 * mv1.KVector4.Scalar1245 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector4.Scalar1245 * mv2.Scalar245 - mv2.Scalar235 * mv1.KVector4.Scalar1245 * mv2.Scalar235) * mv2NormSquaredInv;
            tempScalar[29] += (-mv2.Scalar245 * mv1.KVector4.Scalar1345 * mv2.Scalar245 - 2 * mv2.Scalar134 * mv1.KVector4.Scalar2345 * mv2.Scalar234 - mv2.Scalar234 * mv1.KVector4.Scalar1345 * mv2.Scalar234 + 2 * mv2.Scalar135 * mv1.KVector4.Scalar2345 * mv2.Scalar235 + 2 * mv2.Scalar235 * mv1.KVector4.Scalar1235 * mv2.Scalar345 + mv2.Scalar235 * mv1.KVector4.Scalar1345 * mv2.Scalar235 + mv2.Scalar345 * mv1.KVector4.Scalar1345 * mv2.Scalar345 - 2 * mv2.Scalar145 * mv1.KVector4.Scalar2345 * mv2.Scalar245) * mv2NormSquaredInv;
            tempScalar[30] += (-mv2.Scalar234 * mv1.KVector4.Scalar2345 * mv2.Scalar234 - mv2.Scalar245 * mv1.KVector4.Scalar2345 * mv2.Scalar245 + mv2.Scalar235 * mv1.KVector4.Scalar2345 * mv2.Scalar235 + mv2.Scalar345 * mv1.KVector4.Scalar2345 * mv2.Scalar345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (-mv2.Scalar235 * mv1.KVector5.Scalar12345 * mv2.Scalar235 - mv2.Scalar345 * mv1.KVector5.Scalar12345 * mv2.Scalar345 + mv2.Scalar234 * mv1.KVector5.Scalar12345 * mv2.Scalar234 + mv2.Scalar245 * mv1.KVector5.Scalar12345 * mv2.Scalar245) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    public static Ga401Multivector ReflectDualOnDual(this Ga401Multivector mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401Multivector.Zero;
        
        var mv2NormSquaredInv = 1d / mv2.NormSquared();
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += (mv2.Scalar2345 * mv1.KVector0.Scalar * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += (2 * mv2.Scalar1234 * mv1.KVector1.Scalar5 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector1.Scalar4 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.KVector1.Scalar3 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector1.Scalar2 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector1.Scalar1 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[2] += (-mv2.Scalar2345 * mv1.KVector1.Scalar2 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[4] += (-mv2.Scalar2345 * mv1.KVector1.Scalar3 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[8] += (-mv2.Scalar2345 * mv1.KVector1.Scalar4 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[16] += (-mv2.Scalar2345 * mv1.KVector1.Scalar5 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += (2 * mv2.Scalar1234 * mv1.KVector2.Scalar25 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector2.Scalar24 * mv2.Scalar2345 + 2 * mv2.Scalar1245 * mv1.KVector2.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar12 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[5] += (2 * mv2.Scalar1234 * mv1.KVector2.Scalar35 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector2.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector2.Scalar23 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar13 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[6] += (mv2.Scalar2345 * mv1.KVector2.Scalar23 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[9] += (2 * mv2.Scalar1234 * mv1.KVector2.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector2.Scalar34 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector2.Scalar24 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar14 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[10] += (mv2.Scalar2345 * mv1.KVector2.Scalar24 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[12] += (mv2.Scalar2345 * mv1.KVector2.Scalar34 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[17] += (2 * mv2.Scalar1235 * mv1.KVector2.Scalar45 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector2.Scalar35 * mv2.Scalar2345 + 2 * mv2.Scalar1345 * mv1.KVector2.Scalar25 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector2.Scalar15 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[18] += (mv2.Scalar2345 * mv1.KVector2.Scalar25 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[20] += (mv2.Scalar2345 * mv1.KVector2.Scalar35 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[24] += (mv2.Scalar2345 * mv1.KVector2.Scalar45 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += (2 * mv2.Scalar1234 * mv1.KVector3.Scalar235 * mv2.Scalar2345 - 2 * mv2.Scalar1235 * mv1.KVector3.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar123 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[11] += (2 * mv2.Scalar1234 * mv1.KVector3.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector3.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar124 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[13] += (2 * mv2.Scalar1234 * mv1.KVector3.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector3.Scalar234 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar134 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[14] += (-mv2.Scalar2345 * mv1.KVector3.Scalar234 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[19] += (2 * mv2.Scalar1235 * mv1.KVector3.Scalar245 * mv2.Scalar2345 - 2 * mv2.Scalar1245 * mv1.KVector3.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar125 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[21] += (2 * mv2.Scalar1235 * mv1.KVector3.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector3.Scalar235 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar135 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[22] += (-mv2.Scalar2345 * mv1.KVector3.Scalar235 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[25] += (2 * mv2.Scalar1245 * mv1.KVector3.Scalar345 * mv2.Scalar2345 - 2 * mv2.Scalar1345 * mv1.KVector3.Scalar245 * mv2.Scalar2345 + mv2.Scalar2345 * mv1.KVector3.Scalar145 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[26] += (-mv2.Scalar2345 * mv1.KVector3.Scalar245 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[28] += (-mv2.Scalar2345 * mv1.KVector3.Scalar345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += (2 * mv2.Scalar1234 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1234 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[23] += (2 * mv2.Scalar1235 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1235 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[27] += (2 * mv2.Scalar1245 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1245 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[29] += (2 * mv2.Scalar1345 * mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv2.Scalar2345 * mv1.KVector4.Scalar1345 * mv2.Scalar2345) * mv2NormSquaredInv;
            tempScalar[30] += (mv2.Scalar2345 * mv1.KVector4.Scalar2345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += (mv2.Scalar2345 * mv1.KVector5.Scalar12345 * mv2.Scalar2345) * mv2NormSquaredInv;
        }
        
        return Ga401Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401Multivector ReflectDualOnDual(this Ga401Multivector mv1, Ga401KVector5 mv2)
    {
        return Ga401Multivector.Zero;
    }
    
}

using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Projectors;

public static class RGaFloat64ProjectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ProjectOn(this RGaFloat64Scalar mv, RGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetScalarPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ProjectOn(this RGaFloat64Vector mv, RGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());

        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector ProjectOn(this RGaFloat64Bivector mv, RGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector ProjectOn(this RGaFloat64HigherKVector mv, RGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetHigherKVectorPart(mv.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector ProjectOn(this RGaFloat64KVector mv, RGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetKVectorPart(mv.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector ProjectOn(this RGaFloat64Multivector mv, RGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace);
    }
    


}
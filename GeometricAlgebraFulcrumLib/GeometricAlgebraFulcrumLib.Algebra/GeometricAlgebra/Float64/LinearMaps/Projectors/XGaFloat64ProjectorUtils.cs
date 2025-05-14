using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Projectors;

public static class XGaFloat64ProjectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ProjectOn(this XGaFloat64Scalar mv, XGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetScalarPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ProjectOn(this XGaFloat64Vector mv, XGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());

        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector ProjectOn(this XGaFloat64Bivector mv, XGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector ProjectOn(this XGaFloat64HigherKVector mv, XGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetHigherKVectorPart(mv.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector ProjectOn(this XGaFloat64KVector mv, XGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetKVectorPart(mv.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector ProjectOn(this XGaFloat64Multivector mv, XGaFloat64KVector subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace);
    }
}
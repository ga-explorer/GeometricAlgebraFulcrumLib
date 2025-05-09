using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Projectors;

public static class RGaProjectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ProjectOn<T>(this RGaScalar<T> mv, RGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetScalarPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> ProjectOn<T>(this RGaVector<T> mv, RGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> ProjectOn<T>(this RGaBivector<T> mv, RGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> ProjectOn<T>(this RGaHigherKVector<T> mv, RGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetHigherKVectorPart(mv.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> ProjectOn<T>(this RGaKVector<T> mv, RGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetKVectorPart(mv.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> ProjectOn<T>(this RGaMultivector<T> mv, RGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace);
    }
    


}
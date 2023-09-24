using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Projectors;

public static class XGaProjectorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ProjectOn<T>(this XGaScalar<T> mv, XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetScalarPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ProjectOn<T>(this XGaVector<T> mv, XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> ProjectOn<T>(this XGaBivector<T> mv, XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> ProjectOn<T>(this XGaHigherKVector<T> mv, XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetHigherKVectorPart(mv.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> ProjectOn<T>(this XGaKVector<T> mv, XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace).GetKVectorPart(mv.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> ProjectOn<T>(this XGaMultivector<T> mv, XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return mv.Fdp(subspaceInverse).Gp(subspace);
    }
    


}
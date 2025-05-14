using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;

public static class XGaSubspaceUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<T>(this IXGaSubspace<T> subspace, XGaVector<T> vector, bool nearZeroFlag = false)
    {
        var processor = subspace.ScalarProcessor;

        var mv2 = vector.Op(subspace.GetBlade());

        return subspace.SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<T>(this IXGaSubspace<T> subspace, XGaBivector<T> mv, bool nearZeroFlag = false)
    {
        var processor = subspace.ScalarProcessor;

        var mv2 = mv - subspace.Project(mv);

        return subspace.SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<T>(this IXGaSubspace<T> subspace, XGaKVector<T> mv, bool nearZeroFlag = false)
    {
        var processor = subspace.ScalarProcessor;
            
        var mv2 = mv - subspace.Project(mv);

        return subspace.SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<T>(this IXGaSubspace<T> subspace, IXGaSubspace<T> mv, bool nearZeroFlag = false)
    {
        return subspace.Contains(mv.GetBlade(), nearZeroFlag);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> Project<T>(this IXGaSubspace<T> subspace, XGaVector<T> blade)
    {
        return blade
            .Lcp(subspace.GetBladePseudoInverse())
            .Lcp(subspace.GetBlade())
            .AsVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> Project<T>(this IXGaSubspace<T> subspace, XGaBivector<T> blade)
    {
        return blade
            .Lcp(subspace.GetBladePseudoInverse())
            .Lcp(subspace.GetBlade())
            .AsBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Project<T>(this IXGaSubspace<T> subspace, XGaKVector<T> blade)
    {
        return blade
            .Lcp(subspace.GetBladePseudoInverse())
            .Lcp(subspace.GetBlade());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> Reflect<T>(this IXGaSubspace<T> subspace, XGaVector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(-blade)
            .Gp(subspace.GetBladeInverse())
            .GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> Reflect<T>(this IXGaSubspace<T> subspace, XGaBivector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(blade)
            .Gp(subspace.GetBladeInverse())
            .GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Reflect<T>(this IXGaSubspace<T> subspace, XGaKVector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(subspace.GetBladeInverse())
            .GetKVectorPart(blade.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> VersorProduct<T>(this IXGaSubspace<T> subspace, XGaVector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(subspace.GetBladeInverse())
            .GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> VersorProduct<T>(this IXGaSubspace<T> subspace, XGaBivector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(subspace.GetBladeInverse())
            .GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> VersorProduct<T>(this IXGaSubspace<T> subspace, XGaKVector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(subspace.GetBladeInverse())
            .GetKVectorPart(blade.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Complement<T>(this IXGaSubspace<T> subspace, XGaVector<T> blade)
    {
        return blade.Lcp(subspace.GetBlade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Complement<T>(this IXGaSubspace<T> subspace, XGaBivector<T> blade)
    {
        return blade.Lcp(subspace.GetBlade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Complement<T>(this IXGaSubspace<T> subspace, XGaKVector<T> blade)
    {
        return blade.Lcp(subspace.GetBlade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Complement<T>(this IXGaSubspace<T> subspace, XGaMultivector<T> blade)
    {
        return blade.Lcp(subspace.GetBlade());
    }
}
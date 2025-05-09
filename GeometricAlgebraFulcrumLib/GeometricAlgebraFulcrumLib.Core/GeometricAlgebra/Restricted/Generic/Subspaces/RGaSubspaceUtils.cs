using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Subspaces;

public static class RGaSubspaceUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<T>(this IRGaSubspace<T> subspace, RGaVector<T> vector, bool nearZeroFlag = false)
    {
        var processor = subspace.ScalarProcessor;

        var mv2 = vector.Op(subspace.GetBlade());

        return subspace.SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<T>(this IRGaSubspace<T> subspace, RGaBivector<T> mv, bool nearZeroFlag = false)
    {
        var processor = subspace.ScalarProcessor;

        var mv2 = mv - subspace.Project(mv);

        return subspace.SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<T>(this IRGaSubspace<T> subspace, RGaKVector<T> mv, bool nearZeroFlag = false)
    {
        var processor = subspace.ScalarProcessor;
            
        var mv2 = mv - subspace.Project(mv);

        return subspace.SubspaceDimension >= 2 &&
               (mv2.IsZero || mv2.Scalars.All(s => processor.IsZero(s, nearZeroFlag)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Contains<T>(this IRGaSubspace<T> subspace, IRGaSubspace<T> mv, bool nearZeroFlag = false)
    {
        return subspace.Contains(mv.GetBlade(), nearZeroFlag);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Project<T>(this IRGaSubspace<T> subspace, RGaVector<T> blade)
    {
        return blade
            .Lcp(subspace.GetBladePseudoInverse())
            .Lcp(subspace.GetBlade())
            .GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Project<T>(this IRGaSubspace<T> subspace, RGaBivector<T> blade)
    {
        return blade
            .Lcp(subspace.GetBladePseudoInverse())
            .Lcp(subspace.GetBlade())
            .GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Project<T>(this IRGaSubspace<T> subspace, RGaKVector<T> blade)
    {
        return blade
            .Lcp(subspace.GetBladePseudoInverse())
            .Lcp(subspace.GetBlade())
            .GetKVectorPart(blade.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Reflect<T>(this IRGaSubspace<T> subspace, RGaVector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(-blade)
            .Gp(subspace.GetBladeInverse())
            .GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> Reflect<T>(this IRGaSubspace<T> subspace, RGaBivector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(blade)
            .Gp(subspace.GetBladeInverse())
            .GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Reflect<T>(this IRGaSubspace<T> subspace, RGaKVector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(subspace.GetBladeInverse())
            .GetKVectorPart(blade.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VersorProduct<T>(this IRGaSubspace<T> subspace, RGaVector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(subspace.GetBladeInverse())
            .GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> VersorProduct<T>(this IRGaSubspace<T> subspace, RGaBivector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(subspace.GetBladeInverse())
            .GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> VersorProduct<T>(this IRGaSubspace<T> subspace, RGaKVector<T> blade)
    {
        return subspace
            .GetBlade()
            .Gp(blade.GradeInvolution())
            .Gp(subspace.GetBladeInverse())
            .GetKVectorPart(blade.Grade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Complement<T>(this IRGaSubspace<T> subspace, RGaVector<T> blade)
    {
        return blade.Lcp(subspace.GetBlade()).GetKVectorPart(blade.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Complement<T>(this IRGaSubspace<T> subspace, RGaBivector<T> blade)
    {
        return blade.Lcp(subspace.GetBlade()).GetKVectorPart(blade.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Complement<T>(this IRGaSubspace<T> subspace, RGaKVector<T> blade)
    {
        return blade.Lcp(subspace.GetBlade()).GetKVectorPart(blade.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> Complement<T>(this IRGaSubspace<T> subspace, RGaMultivector<T> blade)
    {
        return blade.Lcp(subspace.GetBlade());
    }
}
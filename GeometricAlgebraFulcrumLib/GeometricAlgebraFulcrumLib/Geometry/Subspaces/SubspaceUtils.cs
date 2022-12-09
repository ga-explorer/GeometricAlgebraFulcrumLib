using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public static class SubspaceUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ISubspace<T> subspace, GaVector<T> vector, bool nearZeroFlag = false)
        {
            var processor = subspace.GeometricProcessor;

            return subspace.SubspaceDimension >= 1 && processor.IsZero(
                vector.Op(subspace.GetBlade()).KVectorStorage,
                nearZeroFlag
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ISubspace<T> subspace, GaBivector<T> mv, bool nearZeroFlag = false)
        {
            var processor = subspace.GeometricProcessor;

            return subspace.SubspaceDimension >= 2 && processor.IsZero(
                (mv - subspace.Project(mv)).BivectorStorage,
                nearZeroFlag
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ISubspace<T> subspace, GaKVector<T> mv, bool nearZeroFlag = false)
        {
            var processor = subspace.GeometricProcessor;

            return subspace.SubspaceDimension >= mv.Grade && processor.IsZero(
                (mv - subspace.Project(mv)).MultivectorStorage,
                nearZeroFlag
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ISubspace<T> subspace, ISubspace<T> mv, bool nearZeroFlag = false)
        {
            return subspace.Contains(mv.GetBlade(), nearZeroFlag);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Project<T>(this ISubspace<T> subspace, GaVector<T> blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade())
                .AsVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> Project<T>(this ISubspace<T> subspace, GaBivector<T> blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade())
                .AsBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Project<T>(this ISubspace<T> subspace, GaKVector<T> blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Reflect<T>(this ISubspace<T> subspace, GaVector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(-blade)
                .Gp(subspace.GetBladeInverse())
                .GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> Reflect<T>(this ISubspace<T> subspace, GaBivector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade)
                .Gp(subspace.GetBladeInverse())
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Reflect<T>(this ISubspace<T> subspace, GaKVector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetKVectorPart(blade.Grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> VersorProduct<T>(this ISubspace<T> subspace, GaVector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> VersorProduct<T>(this ISubspace<T> subspace, GaBivector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> VersorProduct<T>(this ISubspace<T> subspace, GaKVector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetKVectorPart(blade.Grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Complement<T>(this ISubspace<T> subspace, GaVector<T> blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Complement<T>(this ISubspace<T> subspace, GaBivector<T> blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Complement<T>(this ISubspace<T> subspace, GaKVector<T> blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Complement<T>(this ISubspace<T> subspace, GaMultivector<T> blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }
    }
}
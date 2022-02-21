using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class SubspaceUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ISubspace<T> subspace, Vector<T> vector, bool nearZeroFlag = false)
        {
            var processor = subspace.GeometricProcessor;

            return subspace.SubspaceDimension >= 1 && processor.IsZero(
                vector.Op(subspace.GetBlade()).KVectorStorage,
                nearZeroFlag
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ISubspace<T> subspace, Bivector<T> mv, bool nearZeroFlag = false)
        {
            var processor = subspace.GeometricProcessor;

            return subspace.SubspaceDimension >= 2 && processor.IsZero(
                (mv - subspace.Project(mv)).BivectorStorage,
                nearZeroFlag
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ISubspace<T> subspace, KVector<T> mv, bool nearZeroFlag = false)
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
        public static Vector<T> Project<T>(this ISubspace<T> subspace, Vector<T> blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade())
                .AsVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> Project<T>(this ISubspace<T> subspace, Bivector<T> blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade())
                .AsBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Project<T>(this ISubspace<T> subspace, KVector<T> blade)
        {
            return blade
                .Lcp(subspace.GetBladePseudoInverse())
                .Lcp(subspace.GetBlade());
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Reflect<T>(this ISubspace<T> subspace, Vector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(-blade)
                .Gp(subspace.GetBladeInverse())
                .GetVectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> Reflect<T>(this ISubspace<T> subspace, Bivector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade)
                .Gp(subspace.GetBladeInverse())
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Reflect<T>(this ISubspace<T> subspace, KVector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetKVectorPart(blade.Grade);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> VersorProduct<T>(this ISubspace<T> subspace, Vector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetVectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> VersorProduct<T>(this ISubspace<T> subspace, Bivector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> VersorProduct<T>(this ISubspace<T> subspace, KVector<T> blade)
        {
            return subspace
                .GetBlade()
                .Gp(blade.GradeInvolution())
                .Gp(subspace.GetBladeInverse())
                .GetKVectorPart(blade.Grade);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Complement<T>(this ISubspace<T> subspace, Vector<T> blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Complement<T>(this ISubspace<T> subspace, Bivector<T> blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Complement<T>(this ISubspace<T> subspace, KVector<T> blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Complement<T>(this ISubspace<T> subspace, Multivector<T> blade)
        {
            return blade.Lcp(subspace.GetBlade());
        }
    }
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class SubspaceUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ISubspace<T> subspace, VectorStorage<T> vector, bool nearZeroFlag = false)
        {
            var processor = subspace.GeometricProcessor;

            return subspace.SubspaceDimension >= 1 && processor.IsZero(
                processor.Op(vector, subspace.Blade),
                nearZeroFlag
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ISubspace<T> subspace, BivectorStorage<T> mv, bool nearZeroFlag = false)
        {
            var processor = subspace.GeometricProcessor;

            return subspace.SubspaceDimension >= 2 && processor.IsZero(
                processor.Subtract(mv, subspace.Project(mv)),
                nearZeroFlag
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ISubspace<T> subspace, KVectorStorage<T> mv, bool nearZeroFlag = false)
        {
            var processor = subspace.GeometricProcessor;

            return subspace.SubspaceDimension >= mv.Grade && processor.IsZero(
                processor.Subtract(mv, subspace.Project(mv)),
                nearZeroFlag
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this ISubspace<T> subspace, ISubspace<T> mv, bool nearZeroFlag = false)
        {
            return subspace.Contains(mv.Blade, nearZeroFlag);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Project<T>(this ISubspace<T> subspace, VectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Lcp(
                processor.Lcp(blade, subspace.Blade), 
                subspace.BladeInverse
            ).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Project<T>(this ISubspace<T> subspace, BivectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Lcp(
                processor.Lcp(blade, subspace.Blade), 
                subspace.BladeInverse
            ).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Project<T>(this ISubspace<T> subspace, KVectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Lcp(
                processor.Lcp(blade, subspace.Blade), 
                subspace.BladeInverse
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Reflect<T>(this ISubspace<T> subspace, VectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                processor.Negative(blade), 
                subspace.BladeInverse
            ).GetVectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Reflect<T>(this ISubspace<T> subspace, BivectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                blade, 
                subspace.BladeInverse
            ).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Reflect<T>(this ISubspace<T> subspace, KVectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                processor.GradeInvolution(blade), 
                subspace.BladeInverse
            ).GetKVectorPart(blade.Grade);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> VersorProduct<T>(this ISubspace<T> subspace, VectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                processor.GradeInvolution(blade), 
                subspace.BladeInverse
            ).GetVectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> VersorProduct<T>(this ISubspace<T> subspace, BivectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                processor.GradeInvolution(blade), 
                subspace.BladeInverse
            ).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> VersorProduct<T>(this ISubspace<T> subspace, KVectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                processor.GradeInvolution(blade), 
                subspace.BladeInverse
            ).GetKVectorPart(blade.Grade);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Complement<T>(this ISubspace<T> subspace, KVectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Lcp(
                blade, 
                subspace.Blade
            );
        }
    }
}
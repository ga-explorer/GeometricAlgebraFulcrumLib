using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GeoSubspaceUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Project<T>(this IGeoSubspace<T> subspace, VectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Lcp(
                processor.Lcp(blade, subspace.Blade), 
                subspace.BladeInverse
            ).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Project<T>(this IGeoSubspace<T> subspace, BivectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Lcp(
                processor.Lcp(blade, subspace.Blade), 
                subspace.BladeInverse
            ).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Project<T>(this IGeoSubspace<T> subspace, KVectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Lcp(
                processor.Lcp(blade, subspace.Blade), 
                subspace.BladeInverse
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Reflect<T>(this IGeoSubspace<T> subspace, VectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                processor.Negative(blade), 
                subspace.BladeInverse
            ).GetVectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Reflect<T>(this IGeoSubspace<T> subspace, BivectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                blade, 
                subspace.BladeInverse
            ).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Reflect<T>(this IGeoSubspace<T> subspace, KVectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                processor.GradeInvolution(blade), 
                subspace.BladeInverse
            ).GetKVectorPart(blade.Grade);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> VersorProduct<T>(this IGeoSubspace<T> subspace, VectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                processor.GradeInvolution(blade), 
                subspace.BladeInverse
            ).GetVectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> VersorProduct<T>(this IGeoSubspace<T> subspace, BivectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                processor.GradeInvolution(blade), 
                subspace.BladeInverse
            ).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> VersorProduct<T>(this IGeoSubspace<T> subspace, KVectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Gp(
                subspace.Blade, 
                processor.GradeInvolution(blade), 
                subspace.BladeInverse
            ).GetKVectorPart(blade.Grade);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Complement<T>(this IGeoSubspace<T> subspace, KVectorStorage<T> blade)
        {
            var processor = subspace.GeometricProcessor;

            return processor.Lcp(
                blade, 
                subspace.Blade
            );
        }
    }
}
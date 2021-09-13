using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class StorageComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IVectorStorageComposer<T> composer)
        {
            return new LinVector<T>(
                composer.ScalarProcessor,
                composer.CreateLinVectorStorage()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> CreateVector<T>(this IVectorStorageComposer<T> composer)
        {
            return new Vector<T>(
                composer.ScalarProcessor,
                composer.CreateVectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> CreateBivector<T>(this IVectorStorageComposer<T> composer)
        {
            return new Bivector<T>(
                composer.ScalarProcessor,
                composer.CreateBivectorStorage()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> CreateKVector<T>(this IVectorStorageComposer<T> composer, uint grade)
        {
            return new KVector<T>(
                composer.ScalarProcessor,
                composer.CreateKVectorStorage(grade)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> CreateMultivector<T>(this IVectorStorageComposer<T> composer)
        {
            return new Multivector<T>(
                composer.ScalarProcessor,
                composer.CreateMultivectorStorage()
            );
        }
    }
}
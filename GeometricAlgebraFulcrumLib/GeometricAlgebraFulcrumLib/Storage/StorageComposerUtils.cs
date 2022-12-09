using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public static class StorageComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this ILinVectorStorageComposer<T> composer)
        {
            return new LinVector<T>(
                composer.ScalarProcessor,
                composer.CreateLinVectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateVector<T>(this ILinVectorStorageComposer<T> composer)
        {
            return new GaVector<T>(
                composer.ScalarProcessor,
                composer.CreateVectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateBivector<T>(this ILinVectorStorageComposer<T> composer)
        {
            return new GaBivector<T>(
                composer.ScalarProcessor,
                composer.CreateBivectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> CreateKVector<T>(this ILinVectorStorageComposer<T> composer, uint grade)
        {
            return new GaKVector<T>(
                composer.ScalarProcessor,
                composer.CreateKVectorStorage(grade)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> CreateMultivector<T>(this ILinVectorStorageComposer<T> composer)
        {
            return new GaMultivector<T>(
                composer.ScalarProcessor,
                composer.CreateMultivectorStorage()
            );
        }
    }
}
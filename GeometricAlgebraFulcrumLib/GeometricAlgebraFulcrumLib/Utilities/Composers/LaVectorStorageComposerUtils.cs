using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public static class StorageComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> CreateLaVector<T>(this ILaVectorStorageComposer<T> composer)
        {
            return new LaVector<T>(
                composer.ScalarProcessor,
                composer.CreateLaVectorEvenStorage()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> CreateGaVector<T>(this ILaVectorStorageComposer<T> composer)
        {
            return new GaVector<T>(
                composer.ScalarProcessor,
                composer.CreateGaVectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> CreateGaBivector<T>(this ILaVectorStorageComposer<T> composer)
        {
            return new GaBivector<T>(
                composer.ScalarProcessor,
                composer.CreateGaBivectorStorage()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> CreateGaKVector<T>(this ILaVectorStorageComposer<T> composer, uint grade)
        {
            return new GaKVector<T>(
                composer.ScalarProcessor,
                composer.CreateGaKVectorStorage(grade)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> CreateGaMultivector<T>(this ILaVectorStorageComposer<T> composer)
        {
            return new GaMultivector<T>(
                composer.ScalarProcessor,
                composer.CreateGaMultivectorStorage()
            );
        }
    }
}
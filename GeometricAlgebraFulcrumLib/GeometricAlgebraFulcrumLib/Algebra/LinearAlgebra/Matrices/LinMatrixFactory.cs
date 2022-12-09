using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices
{
    public static class LinMatrixFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> CreateLinMatrix<T>(this ILinearAlgebraProcessor<T> processor, ILinMatrixStorage<T> storage)
        {
            return new LinMatrix<T>(
                processor,
                storage
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> CreateLinMatrix<T>(this ILinearAlgebraProcessor<T> processor, T[,] scalarArray)
        {
            return new LinMatrix<T>(
                processor,
                scalarArray.CreateLinMatrixDenseStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> CreateLinMatrix<T>(this T[,] scalarArray, ILinearAlgebraProcessor<T> processor)
        {
            return new LinMatrix<T>(
                processor,
                scalarArray.CreateLinMatrixDenseStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> CreateLinMatrix<T>(this ILinMatrixStorage<T> storage, ILinearAlgebraProcessor<T> processor)
        {
            return new LinMatrix<T>(
                processor,
                storage
            );
        }


    }
}
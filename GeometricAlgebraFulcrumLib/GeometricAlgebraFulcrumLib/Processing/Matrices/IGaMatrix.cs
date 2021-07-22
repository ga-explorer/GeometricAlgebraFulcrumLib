using System;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.Matrices
{
    public interface IGaMatrix<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        int RowsCount { get; }

        int ColumnsCount { get; }

        Tuple<int, int> GetSize();

        T this[int i, int j] { get; }
        
        IGaMatrix<T> Add(IGaMatrix<T> array2);

        IGaMatrix<T> Subtract(IGaMatrix<T> array2);

        IGaMatrix<T> LeftScale(T scalar);

        IGaMatrix<T> RightScale(T scalar);

        IGaMatrix<T> Times(IGaMatrix<T> array2);

        IGaMatrix<T> Divide(T scalar);

        IGaMatrix<T> MatrixProduct(IGaMatrix<T> array2);

        IGaMatrix<T> GetCopy(Func<T, T> mappingFunc);

        IGaMatrix<T> GetCopy(Func<int, int, T, T> mappingFunc);

        IGaMatrix<T> GetNegative();

        IGaMatrix<T> GetAdjoint();

        IGaMatrix<T> GetInverse();

        IGaMatrix<T> GetInverseAdjoint();
    }
}
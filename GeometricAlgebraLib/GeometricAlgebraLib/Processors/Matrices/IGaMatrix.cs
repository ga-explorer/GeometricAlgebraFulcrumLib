using System;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Processors.Matrices
{
    public interface IGaMatrix<TScalar>
    {
        IGaScalarProcessor<TScalar> ScalarProcessor { get; }

        int RowsCount { get; }

        int ColumnsCount { get; }

        Tuple<int, int> GetSize();

        TScalar this[int i, int j] { get; }
        
        IGaMatrix<TScalar> Add(IGaMatrix<TScalar> array2);

        IGaMatrix<TScalar> Subtract(IGaMatrix<TScalar> array2);

        IGaMatrix<TScalar> LeftScale(TScalar scalar);

        IGaMatrix<TScalar> RightScale(TScalar scalar);

        IGaMatrix<TScalar> Times(IGaMatrix<TScalar> array2);

        IGaMatrix<TScalar> Divide(TScalar scalar);

        IGaMatrix<TScalar> MatrixProduct(IGaMatrix<TScalar> array2);

        IGaMatrix<TScalar> GetCopy(Func<TScalar, TScalar> mappingFunc);

        IGaMatrix<TScalar> GetCopy(Func<int, int, TScalar, TScalar> mappingFunc);

        IGaMatrix<TScalar> GetNegative();

        IGaMatrix<TScalar> GetAdjoint();

        IGaMatrix<TScalar> GetInverse();

        IGaMatrix<TScalar> GetInverseAdjoint();
    }
}
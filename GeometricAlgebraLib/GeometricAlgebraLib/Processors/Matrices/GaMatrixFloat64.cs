using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Processors.Scalars;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraLib.Processors.Matrices
{
    public sealed class GaMatrixFloat64 : IGaMatrix<double>
    {
        public static GaMatrixFloat64 Create(Matrix numericMatrix)
        {
            return new GaMatrixFloat64(numericMatrix);
        }


        public Matrix NumericMatrix { get; } 

        public IGaScalarProcessor<double> ScalarProcessor 
            => GaScalarProcessorFloat64.DefaultProcessor;

        public int RowsCount 
            => NumericMatrix.RowCount;

        public int ColumnsCount 
            => NumericMatrix.ColumnCount;

        public double this[int i, int j] 
            => NumericMatrix[i, j];


        private GaMatrixFloat64([NotNull] Matrix numericMatrix)
        {
            NumericMatrix = numericMatrix;
        }


        public Tuple<int, int> GetSize()
        {
            return new Tuple<int, int>(
                NumericMatrix.RowCount,
                NumericMatrix.ColumnCount
            );
        }

        public IGaMatrix<double> Add(IGaMatrix<double> array2)
        {
            if (array2 is GaMatrixFloat64 matrixFloat64)
                return new GaMatrixFloat64(
                    (Matrix) (NumericMatrix + matrixFloat64.NumericMatrix)
                );

            throw new InvalidOperationException();
        }

        public IGaMatrix<double> Subtract(IGaMatrix<double> array2)
        {
            if (array2 is GaMatrixFloat64 matrixFloat64)
                return new GaMatrixFloat64(
                    (Matrix) (NumericMatrix - matrixFloat64.NumericMatrix)
                );

            throw new InvalidOperationException();
        }

        public IGaMatrix<double> LeftScale(double scalar)
        {
            return new GaMatrixFloat64(
                (Matrix) (scalar * NumericMatrix)
            );
        }

        public IGaMatrix<double> RightScale(double scalar)
        {
            return new GaMatrixFloat64(
                (Matrix) (NumericMatrix * scalar)
            );
        }

        public IGaMatrix<double> Times(IGaMatrix<double> array2)
        {
            if (array2.RowsCount != RowsCount || array2.ColumnsCount != ColumnsCount)
                throw new InvalidOperationException();

            var numericMatrix =
                (Matrix) Matrix.Build.Dense(
                    RowsCount, 
                    ColumnsCount,
                    (i, j) => NumericMatrix[i, j] * array2[i, j]
                );

            return new GaMatrixFloat64(numericMatrix);
        }

        public IGaMatrix<double> Divide(double scalar)
        {
            scalar = 1d / scalar;

            return new GaMatrixFloat64(
                (Matrix) (NumericMatrix * scalar)
            );
        }

        public IGaMatrix<double> MatrixProduct(IGaMatrix<double> array2)
        {
            if (array2 is GaMatrixFloat64 matrixFloat64)
                return new GaMatrixFloat64(
                    (Matrix) (NumericMatrix * matrixFloat64.NumericMatrix)
                );

            throw new InvalidOperationException();
        }

        public IGaMatrix<double> GetCopy(Func<double, double> mappingFunc)
        {
            var numericMatrix =
                (Matrix) Matrix.Build.Dense(
                    RowsCount, 
                    ColumnsCount,
                    (i, j) => mappingFunc(NumericMatrix[i, j])
                );

            return new GaMatrixFloat64(numericMatrix);
        }

        public IGaMatrix<double> GetCopy(Func<int, int, double, double> mappingFunc)
        {
            var numericMatrix =
                (Matrix) Matrix.Build.Dense(
                    RowsCount, 
                    ColumnsCount,
                    (i, j) => mappingFunc(i, j, NumericMatrix[i, j])
                );

            return new GaMatrixFloat64(numericMatrix);
        }
        
        public IGaMatrix<double> GetNegative()
        {
            return new GaMatrixFloat64(
                (Matrix) NumericMatrix.Negate()
            );
        }

        public IGaMatrix<double> GetAdjoint()
        {
            return new GaMatrixFloat64(
                (Matrix) NumericMatrix.Transpose()
            );
        }

        public IGaMatrix<double> GetInverse()
        {
            return new GaMatrixFloat64(
                (Matrix) NumericMatrix.Inverse()
            );
        }

        public IGaMatrix<double> GetInverseAdjoint()
        {
            return new GaMatrixFloat64(
                (Matrix) NumericMatrix.Transpose().Inverse()
            );
        }
    }
}
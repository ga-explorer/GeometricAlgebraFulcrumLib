using System;
using GeometricAlgebraLib.Implementations.Float64;
using GeometricAlgebraLib.Processors.Scalars;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraLib.Processors.Matrices
{
    public sealed class GaMatrixProcessorFloat64 
        : IGaMatrixProcessor<Matrix, double>
    {
        public static GaMatrixProcessorFloat64 DefaultProcessor { get; }
            = new GaMatrixProcessorFloat64();


        public IGaScalarProcessor<double> ScalarProcessor 
            => GaScalarProcessorFloat64.DefaultProcessor;


        public int GetRowsCount(Matrix matrix)
        {
            return matrix.RowCount;
        }

        public int GetColumnsCount(Matrix matrix)
        {
            return matrix.ColumnCount;
        }

        public Tuple<int, int> GetSize(Matrix matrix)
        {
            return new Tuple<int, int>(
                matrix.RowCount,
                matrix.ColumnCount
            );
        }

        public double[,] GetArray(Matrix matrix)
        {
            return matrix.AsArray();
        }

        public Matrix CreateMatrix(double[,] array)
        {
            return (Matrix) Matrix.Build.DenseOfArray(array);
        }

        public Matrix CreateRowVectorMatrix(double[] array)
        {
            return (Matrix) Matrix.Build.DenseOfRowMajor(
                1,
                array.Length,
                array
            );
        }

        public Matrix CreateRowVectorMatrix(double[,] array, int rowIndex)
        {
            var columnsCount = array.GetLength(1);
            var rowArray = new double[columnsCount];

            for (var i = 0; i < columnsCount; i++)
                rowArray[i] = array[rowIndex, i];

            return (Matrix) Matrix.Build.DenseOfRowMajor(
                1,
                columnsCount,
                rowArray
            );
        }

        public Matrix CreateColumnVectorMatrix(double[] array)
        {
            return (Matrix) Matrix.Build.DenseOfColumnMajor(
                array.Length,
                1,
                array
            );
        }

        public Matrix CreateColumnVectorMatrix(double[,] array, int columnIndex)
        {
            var rowsCount = array.GetLength(1);
            var columnArray = new double[rowsCount];

            for (var i = 0; i < rowsCount; i++)
                columnArray[i] = array[i, columnIndex];

            return (Matrix) Matrix.Build.DenseOfRowMajor(
                rowsCount,
                1,
                columnArray
            );
        }

        public Matrix CreateZeroMatrix(int size)
        {
            return (Matrix) Matrix.Build.Sparse(
                size, 
                size
            );
        }

        public Matrix CreateZeroMatrix(int rowsCount, int columnsCount)
        {
            return (Matrix) Matrix.Build.Sparse(
                rowsCount, 
                columnsCount
            );
        }

        public Matrix CreateUnityMatrix(int size)
        {
            return (Matrix) Matrix.Build.DiagonalIdentity(size);
        }

        public Matrix AddMatrixScalar(Matrix matrix, double scalar)
        {
            return (Matrix) (matrix + scalar);
        }

        public Matrix AddScalarMatrix(double scalar, Matrix matrix)
        {
            return (Matrix) (scalar + matrix);
        }

        public Matrix AddMatrices(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) (matrix1 + matrix2);
        }

        public Matrix SubtractMatrixScalar(Matrix matrix, double scalar)
        {
            return (Matrix) (matrix - scalar);
        }

        public Matrix SubtractScalarMatrix(double scalar, Matrix matrix)
        {
            return (Matrix) (scalar - matrix);
        }

        public Matrix SubtractMatrices(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) (matrix1 - matrix2);
        }

        public Matrix ScalarMatrixProduct(double scalar, Matrix matrix)
        {
            return (Matrix) (scalar * matrix);
        }

        public Matrix MatrixScalarProduct(Matrix matrix, double scalar)
        {
            return (Matrix) (matrix * scalar);
        }

        public Matrix TimesScalars(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix1.RowCount,
                matrix1.ColumnCount,
                (i, j) => matrix1[i, j] * matrix2[i, j]
            );
        }

        public Matrix DivideScalars(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix1.RowCount,
                matrix1.ColumnCount,
                (i, j) => matrix1[i, j] / matrix2[i, j]
            );
        }

        public Matrix DivideMatrixScalar(Matrix matrix, double scalar)
        {
            return (Matrix) (matrix / scalar);
        }

        public Matrix DivideScalarMatrix(double scalar, Matrix matrix)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix.RowCount,
                matrix.ColumnCount,
                (i, j) => scalar / matrix[i, j]
            );
        }

        public Matrix MatrixProduct(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) (matrix1 * matrix2);
        }

        public Matrix MapScalars(Matrix matrix1, Matrix matrix2, Func<double, double, double> mappingFunc)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix1.RowCount,
                matrix1.ColumnCount,
                (i, j) => mappingFunc(matrix1[i, j], matrix2[i, j])
            );
        }

        public Matrix MapScalars(Matrix matrix, Func<double, double> mappingFunc)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix.RowCount,
                matrix.ColumnCount,
                (i, j) => mappingFunc(matrix[i, j])
            );
        }

        public Matrix Negative(Matrix matrix)
        {
            return (Matrix) (-matrix);
        }

        public Matrix Adjoint(Matrix matrix)
        {
            return (Matrix) matrix.Transpose();
        }

        public Matrix Inverse(Matrix matrix)
        {
            return (Matrix) matrix.Inverse();
        }

        public Matrix InverseAdjoint(Matrix matrix)
        {
            return (Matrix) matrix.Inverse().Transpose();
        }

        public int EigenDecomposition(Matrix matrix, out Tuple<double, double[]>[] realPairs, out Tuple<double, double[]>[] imagPairs)
        {
            var sysExpr = matrix.ToComplex().Evd();

            var count = sysExpr.EigenValues.Count;

            realPairs = new Tuple<double, double[]>[count];
            imagPairs = new Tuple<double, double[]>[count];

            //Console.WriteLine("Eigen Vectors Matrix");
            //Console.WriteLine(
            //    GaTextComposerFloat64.DefaultComposer.GetArrayText(
            //        sysExpr.EigenVectors.Real().ToArray()
            //    )
            //);
            //Console.WriteLine();

            //Console.WriteLine(
            //    GaTextComposerFloat64.DefaultComposer.GetArrayText(
            //        sysExpr.EigenVectors.Imaginary().ToArray()
            //    )
            //);
            //Console.WriteLine();

            for (var j = 0; j < count; j++)
            {
                var complexEigenValue = sysExpr.EigenValues[j];
                var complexEigenVector = sysExpr.EigenVectors.Column(j);

                realPairs[j] = new Tuple<double, double[]>(
                    complexEigenValue.Real,
                    complexEigenVector.Real().AsArray()
                );

                imagPairs[j] = new Tuple<double, double[]>(
                    complexEigenValue.Imaginary,
                    complexEigenVector.Imaginary().AsArray()
                );
            }

            return count;
        }

        public Tuple<double, double>[] EigenValues(Matrix matrix)
        {
            var sysExpr = matrix.Evd();
            var complexEigenValues = sysExpr.EigenValues;
            var eigenValuesArray = new Tuple<double, double>[complexEigenValues.Count];

            var j = 0;
            foreach (var complexScalar in complexEigenValues)
            {
                eigenValuesArray[j] = new Tuple<double, double>(
                    complexScalar.Real,
                    complexScalar.Imaginary
                );

                j++;
            }

            return eigenValuesArray;
        }

        public Tuple<double[], double[]>[] EigenVectors(Matrix matrix)
        {
            var sysExpr = matrix.ToComplex().Evd();
            var count = sysExpr.EigenValues.Count;
            var eigenVectorTuples = new Tuple<double[], double[]>[count];

            //Console.WriteLine("Eigen Vectors Matrix");
            //Console.WriteLine(
            //    GaTextComposerFloat64.DefaultComposer.GetArrayText(
            //        sysExpr.EigenVectors.Real().ToArray()
            //    )
            //);
            //Console.WriteLine();

            //Console.WriteLine(
            //    GaTextComposerFloat64.DefaultComposer.GetArrayText(
            //        sysExpr.EigenVectors.Imaginary().ToArray()
            //    )
            //);
            //Console.WriteLine();

            for (var j = 0; j < count; j++)
            {
                var complexEigenVector = sysExpr.EigenVectors.Column(j);

                eigenVectorTuples[j] = new Tuple<double[], double[]>(
                    complexEigenVector.Real().AsArray(),
                    complexEigenVector.Imaginary().AsArray()
                );
            }

            return eigenVectorTuples;
        }
    }
}
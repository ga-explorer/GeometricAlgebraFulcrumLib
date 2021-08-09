using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Processing.Matrices
{
    public sealed class GaMatrixProcessorFloat64 
        : IGaMatrixProcessor<Matrix, double>
    {
        public static GaMatrixProcessorFloat64 DefaultProcessor { get; }
            = new GaMatrixProcessorFloat64();


        public double ZeroEpsilon { get; set; }
            = 1e-13d;


        public bool IsNumeric 
            => true;

        public bool IsSymbolic 
            => false;

        public double ZeroScalar 
            => 0d;

        public double OneScalar 
            => 1d;

        public double MinusOneScalar 
            => -1d;

        public double PiScalar 
            => Math.PI;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetRowsCount(Matrix matrix)
        {
            return matrix.RowCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetColumnsCount(Matrix matrix)
        {
            return matrix.ColumnCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple<int, int> GetSize(Matrix matrix)
        {
            return new Tuple<int, int>(
                matrix.RowCount,
                matrix.ColumnCount
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetArray(Matrix matrix)
        {
            return matrix.AsArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix CreateMatrix(double[,] array)
        {
            return (Matrix) Matrix.Build.DenseOfArray(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix CreateZeroMatrix(int size)
        {
            return (Matrix) Matrix.Build.Sparse(
                size, 
                size
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix CreateZeroMatrix(int rowsCount, int columnsCount)
        {
            return (Matrix) Matrix.Build.Sparse(
                rowsCount, 
                columnsCount
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix CreateUnityMatrix(int size)
        {
            return (Matrix) Matrix.Build.DiagonalIdentity(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix AddMatrixScalar(Matrix matrix, double scalar)
        {
            return (Matrix) (matrix + scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix AddScalarMatrix(double scalar, Matrix matrix)
        {
            return (Matrix) (scalar + matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix AddMatrices(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) (matrix1 + matrix2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix SubtractMatrixScalar(Matrix matrix, double scalar)
        {
            return (Matrix) (matrix - scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix SubtractScalarMatrix(double scalar, Matrix matrix)
        {
            return (Matrix) (scalar - matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix SubtractMatrices(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) (matrix1 - matrix2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix ScalarMatrixProduct(double scalar, Matrix matrix)
        {
            return (Matrix) (scalar * matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix MatrixScalarProduct(Matrix matrix, double scalar)
        {
            return (Matrix) (matrix * scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix TimesScalars(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix1.RowCount,
                matrix1.ColumnCount,
                (i, j) => matrix1[i, j] * matrix2[i, j]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix DivideScalars(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix1.RowCount,
                matrix1.ColumnCount,
                (i, j) => matrix1[i, j] / matrix2[i, j]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix DivideMatrixScalar(Matrix matrix, double scalar)
        {
            return (Matrix) (matrix / scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix DivideScalarMatrix(double scalar, Matrix matrix)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix.RowCount,
                matrix.ColumnCount,
                (i, j) => scalar / matrix[i, j]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix MatrixProduct(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) (matrix1 * matrix2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix MapScalars(Matrix matrix1, Matrix matrix2, Func<double, double, double> mappingFunc)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix1.RowCount,
                matrix1.ColumnCount,
                (i, j) => mappingFunc(matrix1[i, j], matrix2[i, j])
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix MapScalars(Matrix matrix, Func<double, double> mappingFunc)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix.RowCount,
                matrix.ColumnCount,
                (i, j) => mappingFunc(matrix[i, j])
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix NegativeMatrix(Matrix matrix)
        {
            return (Matrix) (-matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix AdjointMatrix(Matrix matrix)
        {
            return (Matrix) matrix.Transpose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix InverseMatrix(Matrix matrix)
        {
            return (Matrix) matrix.Inverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix InverseAdjointMatrix(Matrix matrix)
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

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Add(double scalar1, double scalar2)
        {
            return scalar1 + scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Add(params double[] scalarsList)
        {
            return scalarsList.Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Add(IEnumerable<double> scalarsList)
        {
            return scalarsList.Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Subtract(double scalar1, double scalar2)
        {
            return scalar1 - scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Times(double scalar1, double scalar2)
        {
            return scalar1 * scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Times(params double[] scalarsList)
        {
            return scalarsList.Aggregate(
                1d, 
                (current, scalar) => current * scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Times(IEnumerable<double> scalarsList)
        {
            return scalarsList.Aggregate(
                1d, 
                (current, scalar) => current * scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NegativeTimes(double scalar1, double scalar2)
        {
            return -scalar1 * scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NegativeTimes(params double[] scalarsList)
        {
            return scalarsList.Aggregate(
                -1d, 
                (current, scalar) => current * scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NegativeTimes(IEnumerable<double> scalarsList)
        {
            return scalarsList.Aggregate(
                -1d, 
                (current, scalar) => current * scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Divide(double scalar1, double scalar2)
        {
            return scalar1 / scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NegativeDivide(double scalar1, double scalar2)
        {
            return -scalar1 / scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Positive(double scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Negative(double scalar)
        {
            return -scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Inverse(double scalar)
        {
            return 1d / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Abs(double scalar)
        {
            return Math.Abs(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sqrt(double scalar)
        {
            return Math.Sqrt(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double SqrtOfAbs(double scalar)
        {
            return Math.Sqrt(Math.Abs(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Exp(double scalar)
        {
            return Math.Exp(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log(double scalar)
        {
            return Math.Log(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log2(double scalar)
        {
            return Math.Log2(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log10(double scalar)
        {
            return Math.Log10(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log(double scalar, double baseScalar)
        {
            return Math.Log(scalar, baseScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Cos(double scalar)
        {
            return Math.Cos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sin(double scalar)
        {
            return Math.Sin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Tan(double scalar)
        {
            return Math.Tan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcCos(double scalar)
        {
            return Math.Acos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcSin(double scalar)
        {
            return Math.Asin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcTan(double scalar)
        {
            return Math.Atan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcTan2(double scalarX, double scalarY)
        {
            return Math.Atan2(scalarY, scalarX);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Cosh(double scalar)
        {
            return Math.Cosh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sinh(double scalar)
        {
            return Math.Sinh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Tanh(double scalar)
        {
            return Math.Tanh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(double scalar)
        {
            return !double.IsNaN(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(double scalar)
        {
            return scalar == 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(double scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar > -ZeroEpsilon && scalar < ZeroEpsilon
                : scalar == 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double scalar)
        {
            return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double TextToScalar(string text)
        {
            return double.Parse(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double IntegerToScalar(int value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Float64ToScalar(double value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetRandomScalar(System.Random randomGenerator, double minValue, double maxValue)
        {
            return minValue + (maxValue - minValue) * randomGenerator.NextDouble();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(double scalar)
        {
            return scalar.ToString("G");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(double scalar)
        {
            return scalar > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(double scalar)
        {
            return scalar < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(double scalar)
        {
            return scalar < -ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(double scalar)
        {
            return scalar > ZeroEpsilon;
        }
    }
}
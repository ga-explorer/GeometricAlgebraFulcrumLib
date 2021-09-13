using System;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Processors.LinearAlgebra
{
    public sealed class LinearAlgebraFloat64Processor 
        : ILinearAlgebraProcessor<Matrix, double>
    {
        public static LinearAlgebraFloat64Processor DefaultProcessor { get; }
            = new LinearAlgebraFloat64Processor();


        public double ZeroEpsilon { get; set; }
            = 1e-13d;

        public bool IsNumeric 
            => true;

        public bool IsSymbolic 
            => false;

        public double ScalarZero 
            => 0d;

        public double ScalarOne 
            => 1d;

        public double ScalarMinusOne 
            => -1d;

        public double ScalarTwo 
            => 2d;

        public double ScalarMinusTwo 
            => -2d;

        public double ScalarTen 
            => 10d;

        public double ScalarMinusTen 
            => -10d;

        public double ScalarPi 
            => Math.PI;

        public double ScalarE 
            => Math.E;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetDenseRowsCount(Matrix matrix)
        {
            return matrix.RowCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetDenseColumnsCount(Matrix matrix)
        {
            return matrix.ColumnCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<int> GetDenseSize(Matrix matrix)
        {
            return new Pair<int>(
                matrix.RowCount,
                matrix.ColumnCount
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalar(Matrix matrix, int rowIndex, int colIndex)
        {
            return matrix[rowIndex, colIndex];
        }

        public ILinVectorStorage<double> MatrixRowToVector(Matrix matrix, int rowIndex)
        {
            var colCount = matrix.ColumnCount;
            var array = new double[colCount];

            for (var j = 0; j < colCount; j++)
                array[j] = matrix[rowIndex, j];

            return array.CreateLinVectorDenseStorage();
        }

        public ILinVectorStorage<double> MatrixColumnToVector(Matrix matrix, int colIndex)
        {
            var rowCount = matrix.RowCount;
            var array = new double[rowCount];

            for (var i = 0; i < rowCount; i++)
                array[i] = matrix[i, colIndex];

            return array.CreateLinVectorDenseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<double> MatrixToArray(Matrix matrix)
        {
            return matrix.AsArray().CreateLinMatrixDenseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix CreateMatrix(ILinMatrixStorage<double> array)
        {
            return (Matrix) Matrix.Build.DenseOfArray(array.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix CreateRowVectorMatrix(ILinVectorStorage<double> array)
        {
            return (Matrix) Matrix.Build.DenseOfRowMajor(
                1,
                array.GetSparseCount(),
                array.ToArray()
            );
        }

        public Matrix CreateRowVectorMatrix(ILinMatrixStorage<double> array, int rowIndex)
        {
            var columnsCount = array.GetDenseCount2();
            var rowArray = new double[columnsCount];

            for (var i = 0; i < columnsCount; i++)
                rowArray[i] = array.GetScalar(rowIndex, i);

            return (Matrix) Matrix.Build.DenseOfRowMajor(
                1,
                columnsCount,
                rowArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix CreateColumnVectorMatrix(ILinVectorStorage<double> array)
        {
            return (Matrix) Matrix.Build.DenseOfColumnMajor(
                array.GetSparseCount(),
                1,
                array.ToArray()
            );
        }

        public Matrix CreateColumnVectorMatrix(ILinMatrixStorage<double> array, int columnIndex)
        {
            var rowsCount = array.GetDenseCount2();
            var columnArray = new double[rowsCount];

            for (var i = 0; i < rowsCount; i++)
                columnArray[i] = array.GetScalar(i, columnIndex);

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
        public Matrix TimesScalarMatrix(double scalar, Matrix matrix)
        {
            return (Matrix) (scalar * matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix TimesMatrixScalar(Matrix matrix, double scalar)
        {
            return (Matrix) (matrix * scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix TimesMatrixItems(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix1.RowCount,
                matrix1.ColumnCount,
                (i, j) => matrix1[i, j] * matrix2[i, j]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix DivideMatrixItems(Matrix matrix1, Matrix matrix2)
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
        public Matrix TimesMatrices(Matrix matrix1, Matrix matrix2)
        {
            return (Matrix) (matrix1 * matrix2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix MapMatrixItems(Matrix matrix, Func<int, int, double, double> mappingFunc)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix.RowCount,
                matrix.ColumnCount,
                (i, j) => mappingFunc(i, j, matrix[i, j])
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix MapMatrixItems(Matrix matrix1, Matrix matrix2, Func<double, double, double> mappingFunc)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix1.RowCount,
                matrix1.ColumnCount,
                (i, j) => mappingFunc(matrix1[i, j], matrix2[i, j])
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix MapMatrixItems(Matrix matrix, Func<double, double> mappingFunc)
        {
            return (Matrix) Matrix.Build.Dense(
                matrix.RowCount,
                matrix.ColumnCount,
                (i, j) => mappingFunc(matrix[i, j])
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix MatrixNegative(Matrix matrix)
        {
            return (Matrix) (-matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix MatrixTranspose(Matrix matrix)
        {
            return (Matrix) matrix.Transpose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix MatrixInverse(Matrix matrix)
        {
            return (Matrix) matrix.Inverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix MatrixInverseTranspose(Matrix matrix)
        {
            return (Matrix) matrix.Inverse().Transpose();
        }

        public int MatrixEigenDecomposition(Matrix matrix, out Tuple<double, ILinVectorStorage<double>>[] realPairs, out Tuple<double, ILinVectorStorage<double>>[] imagPairs)
        {
            var sysExpr = matrix.ToComplex().Evd();

            var count = sysExpr.EigenValues.Count;

            realPairs = new Tuple<double, ILinVectorStorage<double>>[count];
            imagPairs = new Tuple<double, ILinVectorStorage<double>>[count];

            //Console.WriteLine("Eigen Vectors Matrix");
            //Console.WriteLine(
            //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
            //        sysExpr.EigenVectors.Real().ToArray()
            //    )
            //);
            //Console.WriteLine();

            //Console.WriteLine(
            //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
            //        sysExpr.EigenVectors.Imaginary().ToArray()
            //    )
            //);
            //Console.WriteLine();

            for (var j = 0; j < count; j++)
            {
                var complexEigenValue = sysExpr.EigenValues[j];
                var complexEigenVector = sysExpr.EigenVectors.Column(j);

                realPairs[j] = new Tuple<double, ILinVectorStorage<double>>(
                    complexEigenValue.Real,
                    complexEigenVector.Real().CreateLinVectorArrayStorage()
                );

                imagPairs[j] = new Tuple<double, ILinVectorStorage<double>>(
                    complexEigenValue.Imaginary,
                    complexEigenVector.Imaginary().CreateLinVectorArrayStorage()
                );
            }

            return count;
        }

        public Pair<double>[] MatrixEigenValues(Matrix matrix)
        {
            var sysExpr = matrix.Evd();
            var complexEigenValues = sysExpr.EigenValues;
            var eigenValuesArray = new Pair<double>[complexEigenValues.Count];

            var j = 0;
            foreach (var complexScalar in complexEigenValues)
            {
                eigenValuesArray[j] = new Pair<double>(
                    complexScalar.Real,
                    complexScalar.Imaginary
                );

                j++;
            }

            return eigenValuesArray;
        }

        public Pair<ILinVectorStorage<double>>[] MatrixEigenVectors(Matrix matrix)
        {
            var sysExpr = matrix.ToComplex().Evd();
            var count = sysExpr.EigenValues.Count;
            var eigenVectorTuples = new Pair<ILinVectorStorage<double>>[count];

            //Console.WriteLine("Eigen Vectors Matrix");
            //Console.WriteLine(
            //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
            //        sysExpr.EigenVectors.Real().ToArray()
            //    )
            //);
            //Console.WriteLine();

            //Console.WriteLine(
            //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
            //        sysExpr.EigenVectors.Imaginary().ToArray()
            //    )
            //);
            //Console.WriteLine();

            for (var j = 0; j < count; j++)
            {
                var complexEigenVector = sysExpr.EigenVectors.Column(j);

                eigenVectorTuples[j] = new Pair<ILinVectorStorage<double>>(
                    complexEigenVector.Real().CreateLinVectorArrayStorage(),
                    complexEigenVector.Imaginary().CreateLinVectorArrayStorage()
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
        public double NegativeTimes(double scalar1, double scalar2)
        {
            return -scalar1 * scalar2;
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
        public double LogE(double scalar)
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
        public double Power(double baseScalar, double scalar)
        {
            return Math.Pow(baseScalar, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log(double baseScalar, double scalar)
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
        public bool IsNotZero(double scalar)
        {
            return !IsZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotZero(double scalar, bool nearZeroFlag)
        {
            return !IsZero(scalar, nearZeroFlag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearZero(double scalar)
        {
            return !IsNearZero(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromText(string text)
        {
            return double.Parse(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(int value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(uint value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(long value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(ulong value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(float value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromNumber(double value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromRational(long numerator, long denominator)
        {
            return numerator / (double) denominator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
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
        public bool IsNotPositive(double scalar)
        {
            return !IsPositive(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNegative(double scalar)
        {
            return !IsNegative(scalar);
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
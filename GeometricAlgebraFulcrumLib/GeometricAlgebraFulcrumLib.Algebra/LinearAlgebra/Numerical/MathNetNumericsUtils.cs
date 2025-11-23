using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;

using ComplexDenseVector = MathNet.Numerics.LinearAlgebra.Complex.DenseVector;
using ComplexDenseMatrix = MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix;
using ComplexVector = MathNet.Numerics.LinearAlgebra.Vector<Complex>;
using ComplexMatrix = MathNet.Numerics.LinearAlgebra.Matrix<Complex>;
using ComplexSparseVector = MathNet.Numerics.LinearAlgebra.Complex.SparseVector;
using ComplexSparseMatrix = MathNet.Numerics.LinearAlgebra.Complex.SparseMatrix;
using RealDenseVector = MathNet.Numerics.LinearAlgebra.Double.DenseVector;
using RealDenseMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix;
using RealVector = MathNet.Numerics.LinearAlgebra.Vector<double>;
using RealMatrix = MathNet.Numerics.LinearAlgebra.Matrix<double>;
using RealSparseVector = MathNet.Numerics.LinearAlgebra.Double.SparseVector;
using RealSparseMatrix = MathNet.Numerics.LinearAlgebra.Double.SparseMatrix;

public static class MathNetNumericsUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSquare<T>(this Matrix<T> matrix) 
        where T : struct, IEquatable<T>, IFormattable
    {
        return matrix.RowCount == matrix.ColumnCount;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Size<T>(this Matrix<T> matrix) 
        where T : struct, IEquatable<T>, IFormattable
    {
        return matrix.IsSquare() 
            ? matrix.RowCount 
            : throw new ArgumentException("Matrix is not square");
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this RealVector vector)
    {
        return vector.L2Norm().IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this ComplexVector vector)
    {
        return vector.L2Norm().IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this RealMatrix matrix)
    {
        return matrix.FrobeniusNorm().IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this ComplexMatrix matrix)
    {
        return matrix.FrobeniusNorm().IsZero();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this RealVector vector, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector.L2Norm().IsNearZero(tolerance);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this ComplexVector vector, double tolerance = Float64Utils.DefaultTolerance)
    {
        return vector.L2Norm().IsNearZero(tolerance);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this RealMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        return matrix.FrobeniusNorm().IsNearZero(tolerance);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this ComplexMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        return matrix.FrobeniusNorm().IsNearZero(tolerance);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealVector CreateRealVector(int size, bool isDense)
    {
        return isDense
            ? RealDenseVector.Create(size, 0d)
            : RealSparseVector.Create(size, 0d);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealMatrix CreateRealMatrix(int size, bool isDense)
    {
        return isDense
            ? RealDenseMatrix.Create(size, size, 0d)
            : RealSparseMatrix.Create(size, size, 0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealMatrix CreateRealMatrix(int rowCount, int colCount, bool isDense)
    {
        return isDense
            ? RealDenseMatrix.Create(rowCount, colCount, 0d)
            : RealSparseMatrix.Create(rowCount, colCount, 0d);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealMatrix ToMatrix(this double[,] array)
    {
        return RealMatrix.Build.DenseOfArray(array);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealDenseVector ToMathNetDenseVector(this double[] array)
    {
        return RealDenseVector.OfArray(array);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexDenseVector ToMathNetDenseVector(this Complex[] array)
    {
        return ComplexDenseVector.OfArray(array);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealDenseVector ToMathNetDenseVector(this IEnumerable<double> scalarList)
    {
        return RealDenseVector.OfEnumerable(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexDenseVector ToMathNetDenseVector(this IEnumerable<Complex> scalarList)
    {
        return ComplexDenseVector.OfEnumerable(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealDenseVector ToMathNetDenseVector(this IReadOnlyDictionary<int, double> scalarList, int size)
    {
        return RealDenseVector.OfIndexedEnumerable(
            size, 
            scalarList.Select(p => 
                (p.Key, p.Value)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexDenseVector ToMathNetDenseVector(this IReadOnlyDictionary<int, Complex> scalarList, int size)
    {
        return ComplexDenseVector.OfIndexedEnumerable(
            size, 
            scalarList.Select(p => 
                (p.Key, p.Value)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealDenseVector ToMathNetDenseVector(this LinFloat64Vector2D vector)
    {
        return RealDenseVector.OfArray([vector.X, vector.Y]);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealDenseVector ToMathNetDenseVector(this LinFloat64Vector3D vector)
    {
        return RealDenseVector.OfArray([vector.X, vector.Y, vector.Z]);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealDenseVector ToMathNetDenseVector(this LinFloat64Vector4D vector)
    {
        return RealDenseVector.OfArray([vector.X, vector.Y, vector.Z, vector.W]);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealSparseVector ToMathNetSparseVector(this IEnumerable<double> scalarList)
    {
        return RealSparseVector.OfEnumerable(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexSparseVector ToMathNetSparseVector(this IEnumerable<Complex> scalarList)
    {
        return ComplexSparseVector.OfEnumerable(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealSparseVector ToMathNetSparseVector(this IReadOnlyDictionary<int, double> scalarList, int size)
    {
        return RealSparseVector.OfIndexedEnumerable(
            size, 
            scalarList.Select(p => 
                (p.Key, p.Value)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexSparseVector ToMathNetSparseVector(this IReadOnlyDictionary<int, Complex> scalarList, int size)
    {
        return ComplexSparseVector.OfIndexedEnumerable(
            size, 
            scalarList.Select(p => 
                (p.Key, p.Value)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealSparseVector ToMathNetSparseVector(this LinFloat64Vector2D vector)
    {
        return RealSparseVector.OfEnumerable([vector.X, vector.Y]);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealSparseVector ToMathNetSparseVector(this LinFloat64Vector3D vector)
    {
        return RealSparseVector.OfEnumerable([vector.X, vector.Y, vector.Z]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealSparseVector ToMathNetSparseVector(this LinFloat64Vector4D vector)
    {
        return RealSparseVector.OfEnumerable([vector.X, vector.Y, vector.Z, vector.W]);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealDenseVector ToDense(this RealVector vector)
    {
        return RealDenseVector.OfVector(vector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexDenseVector ToDense(this ComplexVector vector)
    {
        return ComplexDenseVector.OfVector(vector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealSparseVector ToSparse(this RealVector vector)
    {
        return RealSparseVector.OfVector(vector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexSparseVector ToSparse(this ComplexVector vector)
    {
        return ComplexSparseVector.OfVector(vector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<(int, double)> GetIndexScalarTuples(this RealVector vector)
    {
        return vector.Storage.EnumerateNonZeroIndexed();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<(int, Complex)> GetIndexScalarTuples(this ComplexVector vector)
    {
        return vector.Storage.EnumerateNonZeroIndexed();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<(int, int, double)> GetIndexScalarTuples(this RealMatrix vector)
    {
        return vector.Storage.EnumerateNonZeroIndexed();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<(int, int, Complex)> GetIndexScalarTuples(this ComplexMatrix vector)
    {
        return vector.Storage.EnumerateNonZeroIndexed();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector ToLinFloat64Vector(this RealVector vector)
    {
        return vector.ToArray().ToLinVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector ToXGaFloat64Vector(this RealVector vector, XGaFloat64Processor processor)
    {
        var composer = processor.CreateVectorComposer();

        foreach (var (index, scalar) in vector.Storage.EnumerateNonZeroIndexed())
            composer.SetVectorTerm(index, scalar);

        return composer.GetVector();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // ReSharper disable once InconsistentNaming
    public static Matrix4x4 ToSystemNumericsMatrix4x4(this RealMatrix array)
    {
        return new Matrix4x4(
            (float)array[0, 0], (float)array[0, 1], (float)array[0, 2], (float)array[0, 3],
            (float)array[1, 0], (float)array[1, 1], (float)array[1, 2], (float)array[1, 3],
            (float)array[2, 0], (float)array[2, 1], (float)array[2, 2], (float)array[2, 3],
            (float)array[3, 0], (float)array[3, 1], (float)array[3, 2], (float)array[3, 3]
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RealVector> Rows(this RealMatrix matrix)
    {
        return matrix.RowCount.GetRange(matrix.Row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RealVector> Columns(this RealMatrix matrix)
    {
        return matrix.ColumnCount.GetRange(matrix.Column);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ComplexVector> Rows(this ComplexMatrix matrix)
    {
        return matrix.RowCount.GetRange(matrix.Row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ComplexVector> Columns(this ComplexMatrix matrix)
    {
        return matrix.ColumnCount.GetRange(matrix.Column);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector> RowsToLinFloat64Vectors(this RealMatrix matrix)
    {
        for (var i = 0; i < matrix.RowCount; i++)
        {
            var composer = LinFloat64VectorComposer.Create();

            for (var j = 0; j < matrix.ColumnCount; j++)
            {
                var scalar = matrix[i, j];
                
                if (scalar.IsZero()) continue;

                composer.SetTerm(j, scalar);
            }

            yield return composer.GetVector();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector> ColumnsToLinFloat64Vectors(this RealMatrix matrix)
    {
        for (var j = 0; j < matrix.ColumnCount; j++)
        {
            var composer = LinFloat64VectorComposer.Create();

            for (var i = 0; i < matrix.RowCount; i++)
            {
                var scalar = matrix[i, j];
                
                if (scalar.IsZero()) continue;

                composer.SetTerm(i, scalar);
            }

            yield return composer.GetVector();
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealDenseMatrix ToMathNetDenseMatrix(this int[,] array)
    {
        return RealDenseMatrix.Create(
            array.GetLength(0),
            array.GetLength(1),
            (i, j) => array[i, j]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealDenseMatrix ToMathNetDenseMatrix(this double[,] array)
    {
        return RealDenseMatrix.OfArray(array);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexDenseMatrix ToMathNetDenseMatrix(this Complex[,] array)
    {
        return ComplexDenseMatrix.OfArray(array);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealDenseMatrix ColumnsToMathNetDenseMatrix(this IReadOnlyList<XGaFloat64Vector> vectorList, int rowCount)
    {
        return RealDenseMatrix.OfColumnVectors(
            vectorList.Select(v => v.VectorToSparseMathNetVector(rowCount))
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealSparseMatrix ToMathNetSparseMatrix(this int[,] array)
    {
        return RealSparseMatrix.Create(
            array.GetLength(0),
            array.GetLength(1),
            (i, j) => array[i, j]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealSparseMatrix ToMathNetSparseMatrix(this double[,] array)
    {
        return RealSparseMatrix.OfArray(array);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexSparseMatrix ToMathNetSparseMatrix(this Complex[,] array)
    {
        return ComplexSparseMatrix.OfArray(array);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealDenseMatrix ToDense(this RealMatrix vector)
    {
        return RealDenseMatrix.OfMatrix(vector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexDenseMatrix ToDense(this ComplexMatrix vector)
    {
        return ComplexDenseMatrix.OfMatrix(vector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RealSparseMatrix ToSparse(this RealMatrix vector)
    {
        return RealSparseMatrix.OfMatrix(vector);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComplexSparseMatrix ToSparse(this ComplexMatrix vector)
    {
        return ComplexSparseMatrix.OfMatrix(vector);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveSmallScalars(this RealVector vector, double tolerance = Float64Utils.DefaultTolerance)
    {
        vector.CoerceZero(tolerance);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveSmallScalars(this ComplexVector vector, double tolerance = Float64Utils.DefaultTolerance)
    {
        vector.CoerceZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveSmallScalars(this RealMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        matrix.CoerceZero(tolerance);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveSmallScalars(this ComplexMatrix matrix, double tolerance = Float64Utils.DefaultTolerance)
    {
        matrix.CoerceZero(tolerance);
    }


    public static string ToMatlabText(this RealMatrix matrix)
    {
        var composer = new StringBuilder();

        composer.AppendLine("[");

        for (var rowIndex = 0; rowIndex < matrix.RowCount; rowIndex++)
        {
            if (rowIndex > 0) composer.AppendLine(";");

            composer.Append("   ");

            for (var colIndex = 0; colIndex < matrix.ColumnCount; colIndex++)
            {
                if (colIndex > 0) composer.Append(", ");

                composer.Append(
                    matrix[rowIndex, colIndex].ToString("G")
                );
            }
        }

        composer.AppendLine();
        composer.AppendLine("]");

        return composer.ToString();
    }
}
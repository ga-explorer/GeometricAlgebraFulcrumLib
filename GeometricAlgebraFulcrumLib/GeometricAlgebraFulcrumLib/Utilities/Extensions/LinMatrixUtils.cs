using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LinMatrixUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple<int, LinVector<T>>> GetRows<T>(this LinMatrix<T> matrix)
        {
            return matrix
                .MatrixStorage
                .GetRows()
                .Select(
                    record => new Tuple<int, LinVector<T>>(
                        (int) record.Index,
                        new LinVector<T>(
                            matrix.LinearProcessor,
                            record.Storage
                        )
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> GetRow<T>(this LinMatrix<T> matrix, int rowIndex)
        {
            return new LinVector<T>(
                matrix.LinearProcessor,
                matrix.MatrixStorage.GetRow((ulong) rowIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> GetScaledRow<T>(this LinMatrix<T> matrix, int rowIndex, T scalingFactor)
        {
            var processor = matrix.LinearProcessor;

            return new LinVector<T>(
                processor,
                matrix
                    .MatrixStorage
                    .GetRow((ulong) rowIndex)
                    .MapScalars(
                        value => processor.Times(scalingFactor, value)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> GetMappedRow<T>(this LinMatrix<T> matrix, int rowIndex, Func<T, T> scalarMapping)
        {
            return new LinVector<T>(
                matrix.LinearProcessor,
                matrix
                    .MatrixStorage
                    .GetRow((ulong) rowIndex)
                    .MapScalars(scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> GetMappedRow<T>(this LinMatrix<T> matrix, int rowIndex, Func<ulong, T, T> indexScalarMapping)
        {
            return new LinVector<T>(
                matrix.LinearProcessor,
                matrix
                    .MatrixStorage
                    .GetRow((ulong) rowIndex)
                    .MapScalars(indexScalarMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple<int, LinVector<T>>> GetColumns<T>(this LinMatrix<T> matrix)
        {
            return matrix
                .MatrixStorage
                .GetColumns()
                .Select(
                    record => new Tuple<int, LinVector<T>>(
                        (int) record.Index,
                        new LinVector<T>(
                            matrix.LinearProcessor,
                            record.Storage
                        )
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> GetColumn<T>(this LinMatrix<T> matrix, int colIndex)
        {
            return new LinVector<T>(
                matrix.LinearProcessor,
                matrix.MatrixStorage.GetColumn((ulong) colIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> GetScaledColumn<T>(this LinMatrix<T> matrix, int colIndex, T scalingFactor)
        {
            var processor = matrix.LinearProcessor;

            return new LinVector<T>(
                processor,
                matrix
                    .MatrixStorage
                    .GetColumn((ulong) colIndex)
                    .MapScalars(
                        value => processor.Times(scalingFactor, value)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> GetMappedColumn<T>(this LinMatrix<T> matrix, int colIndex, Func<T, T> scalarMapping)
        {
            return new LinVector<T>(
                matrix.LinearProcessor,
                matrix
                    .MatrixStorage
                    .GetColumn((ulong) colIndex)
                    .MapScalars(scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> GetMappedColumn<T>(this LinMatrix<T> matrix, int colIndex, Func<ulong, T, T> indexScalarMapping)
        {
            return new LinVector<T>(
                matrix.LinearProcessor,
                matrix
                    .MatrixStorage
                    .GetColumn((ulong) colIndex)
                    .MapScalars(indexScalarMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CombineRows<T>(this ILinMatrixStorage<T> matrix, ILinVectorStorage<T> vector, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            return vector is ILinVectorDenseStorage<T> denseVector
                ? matrix.CombineRows(denseVector.GetScalarsList(), scalingFunc, reducingFunc)
                : matrix.CombineRows(vector.GetIndexScalarRecords(), scalingFunc, reducingFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CombineRows<T>(this ILinMatrixStorage<T> matrix1, ILinMatrixStorage<T> matrix2, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            var vectorsDictionary = new Dictionary<ulong, ILinVectorStorage<T>>();

            foreach (var (index, vector) in matrix2.GetRows())
                vectorsDictionary.Add(
                    index,
                    matrix1.CombineRows(vector, scalingFunc, reducingFunc)
                );

            return vectorsDictionary.CreateLinMatrixRowsListStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> CombineRows<T>(this ILinMatrixGradedStorage<T> matrix, ILinVectorGradedStorage<T> vector, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            var gradesList = matrix.GetGrades().Intersect(vector.GetGrades());
            var gradeVectorDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

            foreach (var grade in gradesList)
            {
                var mappedVector = 
                    matrix.GetMatrixStorage(grade).CombineRows(
                        vector.GetVectorStorage(grade),
                        scalingFunc,
                        reducingFunc
                    );

                gradeVectorDictionary.Add(grade, mappedVector);
            }

            return gradeVectorDictionary.CreateLinVectorGradedStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<T> CombineRows<T>(this ILinMatrixGradedStorage<T> matrix1, ILinMatrixGradedStorage<T> matrix2, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            var gradesList = matrix1.GetGrades().Intersect(matrix2.GetGrades());
            var gradeVectorDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

            foreach (var grade in gradesList)
            {
                var mappedMatrix = 
                    matrix1.GetMatrixStorage(grade).CombineRows(
                        matrix2.GetMatrixStorage(grade),
                        scalingFunc,
                        reducingFunc
                    );

                gradeVectorDictionary.Add(grade, mappedMatrix);
            }

            return gradeVectorDictionary.CreateLinMatrixGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> CombineColumns<T>(this ILinMatrixStorage<T> matrix, ILinVectorStorage<T> vector, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            return vector is ILinVectorDenseStorage<T> denseVector
                ? matrix.CombineColumns(denseVector.GetScalarsList(), scalingFunc, reducingFunc)
                : matrix.CombineColumns(vector.GetIndexScalarRecords(), scalingFunc, reducingFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> CombineColumns<T>(this ILinMatrixStorage<T> matrix1, ILinMatrixStorage<T> matrix2, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            var vectorsDictionary = new Dictionary<ulong, ILinVectorStorage<T>>();

            foreach (var (index, vector) in matrix2.GetColumns())
                vectorsDictionary.Add(
                    index,
                    matrix1.CombineColumns(vector, scalingFunc, reducingFunc)
                );

            return vectorsDictionary.CreateLinMatrixColumnsListStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> CombineColumns<T>(this ILinMatrixGradedStorage<T> matrix, ILinVectorGradedStorage<T> vector, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            var gradesList = matrix.GetGrades().Intersect(vector.GetGrades());
            var gradeVectorDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

            foreach (var grade in gradesList)
            {
                var mappedVector = 
                    matrix.GetMatrixStorage(grade).CombineColumns(
                        vector.GetVectorStorage(grade),
                        scalingFunc,
                        reducingFunc
                    );

                gradeVectorDictionary.Add(grade, mappedVector);
            }

            return gradeVectorDictionary.CreateLinVectorGradedStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<T> CombineColumns<T>(this ILinMatrixGradedStorage<T> matrix1, ILinMatrixGradedStorage<T> matrix2, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            var gradesList = matrix1.GetGrades().Intersect(matrix2.GetGrades());
            var gradeVectorDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

            foreach (var grade in gradesList)
            {
                var mappedMatrix = 
                    matrix1.GetMatrixStorage(grade).CombineColumns(
                        matrix2.GetMatrixStorage(grade),
                        scalingFunc,
                        reducingFunc
                    );

                gradeVectorDictionary.Add(grade, mappedMatrix);
            }

            return gradeVectorDictionary.CreateLinMatrixGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrix, VectorStorage<T> vector)
        {
            return scalarProcessor.MatrixProduct(
                matrix,
                vector.GetLinVectorIndexScalarStorage()
            ).CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> MapBivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrix, BivectorStorage<T> vector)
        {
            return scalarProcessor.MatrixProduct(
                matrix,
                vector.GetLinVectorIndexScalarStorage()
            ).CreateBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> MapKVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrix, KVectorStorage<T> vector)
        {
            var grade = vector.Grade;

            return scalarProcessor.MatrixProduct(
                matrix,
                vector.GetLinVectorIndexScalarStorage()
            ).CreateKVectorStorage(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> MapMultivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrix, IMultivectorStorage<T> vector)
        {
            return scalarProcessor.MatrixProduct(
                matrix,
                vector.GetLinVectorIdScalarStorage()
            ).CreateMultivectorSparseStorage();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> MapVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrix, VectorStorage<T> vector)
        {
            return scalarProcessor.MatrixProduct(
                matrix.GetMatrixStorage(1),
                vector.GetLinVectorIndexScalarStorage()
            ).CreateVectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> MapBivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrix, BivectorStorage<T> vector)
        {
            return scalarProcessor.MatrixProduct(
                matrix.GetMatrixStorage(2),
                vector.GetLinVectorIndexScalarStorage()
            ).CreateBivectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> MapKVector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrix, KVectorStorage<T> vector)
        {
            var grade = vector.Grade;

            return scalarProcessor.MatrixProduct(
                matrix.GetMatrixStorage(grade),
                vector.GetLinVectorIndexScalarStorage()
            ).CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> MapMultivector<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrix, IMultivectorStorage<T> vector)
        {
            return scalarProcessor.MatrixProduct(
                matrix,
                vector.GetLinVectorGradedStorage()
            ).CreateMultivectorGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] CreateVectorToVectorRotationMatrix<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> sourceVector, VectorStorage<T> targetVector, ulong basisVectorIndex, int matrixSize)
        {
            var matrix2 = scalarProcessor.CreateVectorToBasisRotationMatrix(sourceVector, basisVectorIndex, matrixSize);
            var matrix1 = scalarProcessor.CreateBasisToVectorRotationMatrix(basisVectorIndex, targetVector, matrixSize);

            return scalarProcessor.MatrixProduct(matrix1, matrix2);
        }
        
        public static T[,] CreateBasisToVectorRotationMatrix<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ulong basisVectorIndex, VectorStorage<T> unitVector, int matrixSize)
        {
            if (matrixSize < 2)
                throw new ArgumentOutOfRangeException(nameof(matrixSize));

            // Special case: unitVector == e_{basisVectorIndex}
            var v1 = scalarProcessor.GetTermScalarByIndex(unitVector, basisVectorIndex);
            if (scalarProcessor.IsOne(v1))
                return scalarProcessor.CreateArrayIdentity2D(matrixSize);

            v1 = scalarProcessor.Add(scalarProcessor.ScalarOne, v1);

            // Special case: unitVector == -e_{basisVectorIndex}
            if (scalarProcessor.IsZero(v1))
            {
                //TODO: Handle this case
                throw new InvalidOperationException();
            }

            var matrix = 
                scalarProcessor.CreateArrayZero2D(matrixSize);

            var indexScalarPairs = 
                unitVector.GetLinVectorIndexScalarStorage();

            // Fill column number basisVectorIndex
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
                matrix[index, basisVectorIndex] = scalar;

            // Fill row number basisVectorIndex
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
            {
                if (index == basisVectorIndex)
                    continue;

                matrix[basisVectorIndex, index] = 
                    scalarProcessor.Negative(scalar);
            }

            // Fill diagonal
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
            {
                if (index == basisVectorIndex)
                    continue;

                matrix[index, index] = 
                    scalarProcessor.Subtract(
                        scalarProcessor.ScalarOne,
                        scalarProcessor.Divide(
                            scalarProcessor.Square(scalar),
                            v1
                        )
                    );
            }

            // Fill remaining items
            foreach (var (index1, scalar1) in indexScalarPairs.GetIndexScalarRecords())
            {
                if (index1 == basisVectorIndex)
                    continue;

                foreach (var (index2, scalar2) in indexScalarPairs.GetIndexScalarRecords())
                {
                    if (index2 == basisVectorIndex || index2 == index1)
                        continue;

                    matrix[index1, index2] = 
                        scalarProcessor.Divide(
                            scalarProcessor.NegativeTimes(scalar1, scalar2),
                            v1
                        );
                }
            }

            return matrix;
        }
        
        public static T[,] CreateVectorToBasisRotationMatrix<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> unitVector, ulong basisVectorIndex, int matrixSize)
        {
            if (matrixSize < 2)
                throw new ArgumentOutOfRangeException(nameof(matrixSize));

            // Special case: unitVector == e_{basisVectorIndex}
            var v1 = scalarProcessor.GetTermScalarByIndex(unitVector, basisVectorIndex);
            if (scalarProcessor.IsOne(v1))
                return scalarProcessor.CreateArrayIdentity2D(matrixSize);

            v1 = scalarProcessor.Add(scalarProcessor.ScalarOne, v1);

            // Special case: unitVector == -e_{basisVectorIndex}
            if (scalarProcessor.IsZero(v1))
            {
                //TODO: Handle this case
                throw new InvalidOperationException();
            }

            var matrix = 
                scalarProcessor.CreateArrayZero2D(matrixSize);

            var indexScalarPairs = 
                unitVector.GetLinVectorIndexScalarStorage();

            // Fill row number basisVectorIndex
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
                matrix[basisVectorIndex, index] = scalar;

            // Fill column number basisVectorIndex
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
            {
                if (index == basisVectorIndex)
                    continue;

                matrix[index, basisVectorIndex] = 
                    scalarProcessor.Negative(scalar);
            }

            // Fill diagonal
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
            {
                if (index == basisVectorIndex)
                    continue;

                matrix[index, index] = 
                    scalarProcessor.Subtract(
                        scalarProcessor.ScalarOne,
                        scalarProcessor.Divide(
                            scalarProcessor.Square(scalar),
                            v1
                        )
                    );
            }

            // Fill remaining items
            foreach (var (index1, scalar1) in indexScalarPairs.GetIndexScalarRecords())
            {
                if (index1 == basisVectorIndex)
                    continue;

                foreach (var (index2, scalar2) in indexScalarPairs.GetIndexScalarRecords())
                {
                    if (index2 == basisVectorIndex || index2 == index1)
                        continue;

                    matrix[index1, index2] = 
                        scalarProcessor.Divide(
                            scalarProcessor.NegativeTimes(scalar1, scalar2),
                            v1
                        );
                }
            }

            return matrix;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<int> GetSize<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix)
        {
            return matrix.MatrixProcessor.GetDenseSize(matrix.MatrixStorage);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LinMatrixColumn<TMatrix, TScalar>> GetColumns<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix)
        {
            return Enumerable
                .Range(0, matrix.ColumnsCount)
                .Select(colIndex =>
                    new LinMatrixColumn<TMatrix, TScalar>(
                        matrix.MatrixProcessor,
                        matrix.MatrixStorage,
                        colIndex
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixColumn<TMatrix, TScalar> GetColumn<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix, int colIndex)
        {
            return new LinMatrixColumn<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                colIndex
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixColumnScaled<TMatrix, TScalar> GetScaledColumn<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix, int colIndex, TScalar scalingFactor)
        {
            return new LinMatrixColumnScaled<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                colIndex,
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixColumnMapped<TMatrix, TScalar> GetMappedColumn<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix, int colIndex, Func<TScalar, TScalar> scalarMapping)
        {
            return new LinMatrixColumnMapped<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                colIndex,
                scalarMapping
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LinMatrixRow<TMatrix, TScalar>> GetRows<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix)
        {
            return Enumerable
                .Range(0, matrix.RowsCount)
                .Select(rowIndex =>
                    new LinMatrixRow<TMatrix, TScalar>(
                        matrix.MatrixProcessor,
                        matrix.MatrixStorage,
                        rowIndex
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixRow<TMatrix, TScalar> GetRow<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix, int rowIndex)
        {
            return new LinMatrixRow<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                rowIndex
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixRowScaled<TMatrix, TScalar> GetScaledRow<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix, int rowIndex, TScalar scalingFactor)
        {
            return new LinMatrixRowScaled<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                rowIndex,
                scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrixRowMapped<TMatrix, TScalar> GetMappedRow<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix, int rowIndex, Func<TScalar, TScalar> scalarMapping)
        {
            return new LinMatrixRowMapped<TMatrix, TScalar>(
                matrix.MatrixProcessor,
                matrix.MatrixStorage,
                rowIndex,
                scalarMapping
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> MapMatrixItems<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix, Func<TScalar, TScalar> scalarMapping)
        {
            var processor = matrix.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.MapMatrixItems(matrix.MatrixStorage, scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> MapMatrixItems<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix, Func<int, int, TScalar, TScalar> scalarMapping)
        {
            var processor = matrix.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.MapMatrixItems(matrix.MatrixStorage, scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> GetTranspose<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix)
        {
            var processor = matrix.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixTranspose(matrix.MatrixStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> GetInverse<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix)
        {
            var processor = matrix.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixInverse(matrix.MatrixStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<TMatrix, TScalar> GetInverseTranspose<TMatrix, TScalar>(this LinMatrix<TMatrix, TScalar> matrix)
        {
            var processor = matrix.MatrixProcessor;

            return new LinMatrix<TMatrix, TScalar>(
                processor,
                processor.MatrixInverseTranspose(matrix.MatrixStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> MapScalars<T>(this LinMatrix<T> v1, Func<T, T> mappingFunc)
        {
            return new LinMatrix<T>(
                v1.LinearProcessor,
                v1.MatrixStorage.MapScalars(mappingFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> MapScalars<T>(this LinMatrix<T> v1, Func<ulong, ulong, T, T> mappingFunc)
        {
            return new LinMatrix<T>(
                v1.LinearProcessor,
                v1.MatrixStorage.MapScalars(mappingFunc)
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Abs<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.Abs)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Sqrt<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.Sqrt)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> SqrtOfAbs<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.SqrtOfAbs)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Exp<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.Exp)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> LogE<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.LogE)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Log2<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.Log2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Log10<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.Log10)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Log<T>(this LinMatrix<T> v1, T scalar)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(s => processor.Log(s, scalar))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Log<T>(this T scalar, LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(s => processor.Log(scalar, s))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Power<T>(this LinMatrix<T> v1, T scalar)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(s => processor.Power(s, scalar))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Power<T>(this T scalar, LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(s => processor.Power(scalar, s))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Cos<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.Cos)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Sin<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.Sin)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Tan<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.Tan)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> ArcCos<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.ArcCos)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> ArcSin<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.ArcSin)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> ArcTan<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.ArcTan)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Cosh<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.Cosh)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Sinh<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.Sinh)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinMatrix<T> Tanh<T>(this LinMatrix<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinMatrix<T>(
                processor,
                v1.MatrixStorage.MapScalars(processor.Tanh)
            );
        }
    }
}
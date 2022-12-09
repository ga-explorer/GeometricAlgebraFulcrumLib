using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra
{
    public static class ScalarAlgebraFloat64ProcessorUtils
    {
        public static ScalarAlgebraFloat64Processor ScalarProcessor
            => ScalarAlgebraFloat64Processor.DefaultProcessor;

        public static GeometricAlgebraEuclideanProcessor<double> EuclideanProcessor { get; }
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(
                GeometricAlgebraSpaceUtils.MaxVSpaceDimension
            );

        public static MatrixAlgebraFloat64Processor MatrixProcessor
            => MatrixAlgebraFloat64Processor.DefaultProcessor;

        public static LaTeXFloat64Composer LaTeXComposer
            => LaTeXFloat64Composer.DefaultComposer;

        public static TextFloat64Composer TextComposer
            => TextFloat64Composer.DefaultComposer;


        public static GaVector<double> CreateVector(params double[] scalarArray)
        {
            return new GaVector<double>(
                EuclideanProcessor,
                ScalarProcessor.CreateVectorStorage(scalarArray)
            );
        }

        public static GaVector<double> CreateBasisVector(int index)
        {
            return new GaVector<double>(
                EuclideanProcessor,
                ScalarProcessor.CreateVectorStorageBasis(index)
            );
        }

        public static Matrix GetMatrix(this IGaOutermorphism<double> linearMap, int rowsCount, int columnsCount)
        {
            return MatrixProcessor.CreateMatrix(
                linearMap.GetVectorOmMappingMatrix(rowsCount, columnsCount)
            );
        }


        public static Matrix ArrayToMatrix(this double[,] array)
        {
            return (DenseMatrix)DenseMatrix.Build.DenseOfArray(array);
        }

        public static Matrix VectorToRowVectorMatrix(this VectorStorage<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.VectorToArrayVector(vector, vSpaceDimension)
            );
        }

        public static Matrix VectorToColumnVectorMatrix(this VectorStorage<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.VectorToArrayVector(vector, vSpaceDimension)
            );

            //return (DenseMatrix) DenseMatrix.Build.DenseOfIndexed(
            //    vSpaceDimension,
            //    1,
            //    vector
            //        .IndexScalarDictionary
            //        .Select(pair => new Tuple<int, int, double>(
            //            (int) pair.Key, 
            //            0, 
            //            pair.Value)
            //        )
            //);
        }

        public static Matrix BivectorToRowVectorMatrix(this BivectorStorage<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.BivectorToArrayVector(bivector, vSpaceDimension)
            );
        }

        public static Matrix BivectorToColumnVectorMatrix(this BivectorStorage<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.BivectorToArrayVector(bivector, vSpaceDimension)
            );
        }

        public static Matrix BivectorToMatrix(this BivectorStorage<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                ScalarProcessor.BivectorToArray(bivector, vSpaceDimension)
            );
        }

        public static Matrix ScalarPlusBivectorToMatrix(this IMultivectorStorage<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                ScalarProcessor.ScalarPlusBivectorToArray(multivector, vSpaceDimension)
            );
        }

        public static Matrix KVectorToRowVectorMatrix(this VectorStorage<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.KVectorToArrayVector(vector, vSpaceDimension)
            );
        }

        public static Matrix KVectorToColumnVectorMatrix(this VectorStorage<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.KVectorToArrayVector(vector, vSpaceDimension)
            );
        }

        public static Matrix MultivectorToRowVectorMatrix(this IMultivectorStorage<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.MultivectorToArrayVector(multivector, vSpaceDimension)
            );
        }

        public static Matrix MultivectorToColumnVectorMatrix(this IMultivectorStorage<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.MultivectorToArrayVector(multivector, vSpaceDimension)
            );
        }

        public static SparseMatrix VectorToSparseColumnVectorMatrix(this VectorStorage<double> vector, uint vSpaceDimension)
        {
            return (SparseMatrix)SparseMatrix.Build.SparseOfIndexed(
                (int)vSpaceDimension,
                1,
                vector.GetLinVectorIndexScalarStorage()
                    .GetIndexScalarRecords()
                    .Select(pair => new Tuple<int, int, double>(
                        (int)pair.Index,
                        0,
                        pair.Scalar)
                )
            );
        }


        public static string GetText(this IMultivectorStorage<double> mv)
        {
            return TextComposer.GetMultivectorText(mv);
        }

        public static string GetText(this IMultivectorStorageContainer<double> mv)
        {
            return TextComposer.GetMultivectorText(mv);
        }

        public static string GetText(this double[,] array)
        {
            return TextComposer.GetArrayText(array);
        }

        public static string GetLaTeX(this IMultivectorStorage<double> mv)
        {
            return LaTeXComposer.GetMultivectorText(mv);
        }
    }
}

using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaScalarProcessorFloat64Utils
    {
        public static Float64ScalarProcessor ScalarProcessor
            => Float64ScalarProcessor.DefaultProcessor;

        public static GaProcessorEuclidean<double> EuclideanProcessor { get; }
            = ScalarProcessor.CreateGaEuclideanProcessor(
                GaSpaceUtils.MaxVSpaceDimension
            );

        public static LaFloat64Processor MatrixProcessor
            => LaFloat64Processor.DefaultProcessor;

        public static Float64LaTeXComposer LaTeXComposer
            => Float64LaTeXComposer.DefaultComposer;

        public static Float64TextComposer TextComposer
            => Float64TextComposer.DefaultComposer;


        public static IGaVectorStorage<double> CreateVector(params double[] scalarArray)
        {
            return ScalarProcessor.CreateGaVectorStorage(scalarArray);
        }

        public static IGaVectorStorage<double> CreateBasisVector(int index)
        {
            return ScalarProcessor.CreateGaVectorStorage(index);
        }

        public static Matrix GetMatrix(this IGaOutermorphism<double> linearMap, int rowsCount, int columnsCount)
        {
            return MatrixProcessor.CreateMatrix(
                linearMap.GetVectorsMappingArray(rowsCount, columnsCount)
            );
        }


        public static Matrix ArrayToMatrix(this double[,] array)
        {
            return (DenseMatrix) DenseMatrix.Build.DenseOfArray(array);
        }

        public static Matrix VectorToRowVectorMatrix(this IGaVectorStorage<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.VectorToArrayVector(vector, vSpaceDimension)
            );
        }

        public static Matrix VectorToColumnVectorMatrix(this IGaVectorStorage<double> vector, uint vSpaceDimension)
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

        public static Matrix BivectorToRowVectorMatrix(this IGaBivectorStorage<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.BivectorToArrayVector(bivector, vSpaceDimension)
            );
        }

        public static Matrix BivectorToColumnVectorMatrix(this IGaBivectorStorage<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.BivectorToArrayVector(bivector, vSpaceDimension)
            );
        }

        public static Matrix BivectorToMatrix(this IGaBivectorStorage<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                ScalarProcessor.BivectorToArray(bivector, vSpaceDimension)
            );
        }

        public static Matrix ScalarPlusBivectorToMatrix(this IGaMultivectorStorage<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                ScalarProcessor.ScalarPlusBivectorToArray(multivector, vSpaceDimension)
            );
        }

        public static Matrix KVectorToRowVectorMatrix(this IGaVectorStorage<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.KVectorToArrayVector(vector, vSpaceDimension)
            );
        }

        public static Matrix KVectorToColumnVectorMatrix(this IGaVectorStorage<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.KVectorToArrayVector(vector, vSpaceDimension)
            );
        }

        public static Matrix MultivectorToRowVectorMatrix(this IGaMultivectorStorage<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.MultivectorToArrayVector(multivector, vSpaceDimension)
            );
        }

        public static Matrix MultivectorToColumnVectorMatrix(this IGaMultivectorStorage<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.MultivectorToArrayVector(multivector, vSpaceDimension)
            );
        }

        public static SparseMatrix VectorToSparseColumnVectorMatrix(this IGaVectorStorage<double> vector, uint vSpaceDimension)
        {
            return (SparseMatrix) SparseMatrix.Build.SparseOfIndexed(
                (int) vSpaceDimension,
                1,
                vector
                    .IndexScalarList
                    .GetIndexScalarRecords()
                    .Select(pair => new Tuple<int, int, double>(
                        (int) pair.Index, 
                        0, 
                        pair.Scalar)
                )
            );
        }


        public static string GetText(this IGaMultivectorStorage<double> mv)
        {
            return TextComposer.GetMultivectorText(mv);
        }

        public static string GetText(this double[,] array)
        {
            return TextComposer.GetArrayText(array);
        }

        public static string GetLaTeX(this IGaMultivectorStorage<double> mv)
        {
            return LaTeXComposer.GetMultivectorText(mv);
        }
    }
}

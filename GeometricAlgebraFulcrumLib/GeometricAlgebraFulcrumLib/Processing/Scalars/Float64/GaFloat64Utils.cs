using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Generic;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Utils;
using GeometricAlgebraFulcrumLib.TextComposers.Float64;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars.Float64
{
    public static class GaFloat64Utils
    {
        public static GaScalarProcessorFloat64 ScalarProcessor
            => GaScalarProcessorFloat64.DefaultProcessor;

        public static GaProcessorGenericEuclidean<double> EuclideanProcessor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor.CreateEuclideanProcessor(
                GaSpaceUtils.MaxVSpaceDimension
            );

        public static GaMatrixProcessorFloat64 MatrixProcessor
            => GaMatrixProcessorFloat64.DefaultProcessor;

        public static GaLaTeXComposerFloat64 LaTeXComposer
            => GaLaTeXComposerFloat64.DefaultComposer;

        public static GaTextComposerFloat64 TextComposer
            => GaTextComposerFloat64.DefaultComposer;


        public static IGaStorageVector<double> CreateVector(params double[] scalarArray)
        {
            return ScalarProcessor.CreateStorageVector(scalarArray);
        }

        public static IGaStorageVector<double> CreateBasisVector(int index)
        {
            return ScalarProcessor.CreateStorageBasisVector(index);
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

        public static Matrix VectorToRowVectorMatrix(this IGaStorageVector<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.VectorToArray(vector, vSpaceDimension)
            );
        }

        public static Matrix VectorToColumnVectorMatrix(this IGaStorageVector<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.VectorToArray(vector, vSpaceDimension)
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

        public static Matrix BivectorToRowVectorMatrix(this IGaStorageBivector<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.BivectorToArray(bivector, vSpaceDimension)
            );
        }

        public static Matrix BivectorToColumnVectorMatrix(this IGaStorageBivector<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.BivectorToArray(bivector, vSpaceDimension)
            );
        }

        public static Matrix BivectorToMatrix(this IGaStorageBivector<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                ScalarProcessor.BivectorToArray2D(bivector, vSpaceDimension)
            );
        }

        public static Matrix ScalarPlusBivectorToMatrix(this IGaStorageMultivector<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                ScalarProcessor.ScalarPlusBivectorToArray2D(multivector, vSpaceDimension)
            );
        }

        public static Matrix KVectorToRowVectorMatrix(this IGaStorageVector<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.KVectorToArray(vector, vSpaceDimension)
            );
        }

        public static Matrix KVectorToColumnVectorMatrix(this IGaStorageVector<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.KVectorToArray(vector, vSpaceDimension)
            );
        }

        public static Matrix MultivectorToRowVectorMatrix(this IGaStorageMultivector<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                ScalarProcessor.MultivectorToArray(multivector, vSpaceDimension)
            );
        }

        public static Matrix MultivectorToColumnVectorMatrix(this IGaStorageMultivector<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                ScalarProcessor.MultivectorToArray(multivector, vSpaceDimension)
            );
        }

        public static SparseMatrix VectorToSparseColumnVectorMatrix(this IGaStorageVector<double> vector, uint vSpaceDimension)
        {
            return (SparseMatrix) SparseMatrix.Build.SparseOfIndexed(
                (int) vSpaceDimension,
                1,
                vector
                    .IndexScalarDictionary
                    .Select(pair => new Tuple<int, int, double>(
                        (int) pair.Key, 
                        0, 
                        pair.Value)
                )
            );
        }


        public static string GetText(this IGaStorageMultivector<double> mv)
        {
            return TextComposer.GetMultivectorText(mv);
        }

        public static string GetText(this double[,] array)
        {
            return TextComposer.GetArrayText(array);
        }

        public static string GetLaTeX(this IGaStorageMultivector<double> mv)
        {
            return LaTeXComposer.GetMultivectorText(mv);
        }
    }
}

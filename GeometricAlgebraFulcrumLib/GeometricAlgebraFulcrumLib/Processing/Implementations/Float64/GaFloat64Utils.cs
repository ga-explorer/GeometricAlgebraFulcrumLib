using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Generic;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.TextComposers;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Processing.Implementations.Float64
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


        public static IGasVector<double> CreateVector(params double[] scalarArray)
        {
            return ScalarProcessor.CreateVector(scalarArray);
        }

        public static IGasVector<double> CreateBasisVector(int index)
        {
            return ScalarProcessor.CreateBasisVector(index);
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

        public static Matrix VectorToRowVectorMatrix(this IGasVector<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                vector.VectorToArray(vSpaceDimension)
            );
        }

        public static Matrix VectorToColumnVectorMatrix(this IGasVector<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                vector.VectorToArray(vSpaceDimension)
            );

            //return (DenseMatrix) DenseMatrix.Build.DenseOfIndexed(
            //    vSpaceDimension,
            //    1,
            //    vector
            //        .GetIndexScalarPairs()
            //        .Select(pair => new Tuple<int, int, double>(
            //            (int) pair.Key, 
            //            0, 
            //            pair.Value)
            //        )
            //);
        }

        public static Matrix BivectorToRowVectorMatrix(this IGasBivector<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                bivector.BivectorToArray(vSpaceDimension)
            );
        }

        public static Matrix BivectorToColumnVectorMatrix(this IGasBivector<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                bivector.BivectorToArray(vSpaceDimension)
            );
        }

        public static Matrix BivectorToMatrix(this IGasBivector<double> bivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                bivector.BivectorToArray2D(vSpaceDimension)
            );
        }

        public static Matrix ScalarPlusBivectorToMatrix(this IGasMultivector<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                multivector.ScalarPlusBivectorToArray2D(vSpaceDimension)
            );
        }

        public static Matrix KVectorToRowVectorMatrix(this IGasVector<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                vector.KVectorToArray(vSpaceDimension)
            );
        }

        public static Matrix KVectorToColumnVectorMatrix(this IGasVector<double> vector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                vector.KVectorToArray(vSpaceDimension)
            );
        }

        public static Matrix MultivectorToRowVectorMatrix(this IGasMultivector<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                multivector.MultivectorToArray(vSpaceDimension)
            );
        }

        public static Matrix MultivectorToColumnVectorMatrix(this IGasMultivector<double> multivector, uint vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                multivector.MultivectorToArray(vSpaceDimension)
            );
        }

        public static SparseMatrix VectorToSparseColumnVectorMatrix(this IGasVector<double> vector, uint vSpaceDimension)
        {
            return (SparseMatrix) SparseMatrix.Build.SparseOfIndexed(
                (int) vSpaceDimension,
                1,
                vector
                    .GetIndexScalarPairs()
                    .Select(pair => new Tuple<int, int, double>(
                        (int) pair.Key, 
                        0, 
                        pair.Value)
                )
            );
        }


        public static string GetText(this IGasMultivector<double> mv)
        {
            return TextComposer.GetMultivectorText(mv);
        }

        public static string GetText(this double[,] array)
        {
            return TextComposer.GetArrayText(array);
        }

        public static string GetLaTeX(this IGasMultivector<double> mv)
        {
            return LaTeXComposer.GetMultivectorText(mv);
        }
    }
}

using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Geometry;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Text;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Processing.Implementations.Float64
{
    public static class GaFloat64Utils
    {
        public static GaScalarProcessorFloat64 ScalarProcessor
            => GaScalarProcessorFloat64.DefaultProcessor;

        public static GaMatrixProcessorFloat64 MatrixProcessor
            => GaMatrixProcessorFloat64.DefaultProcessor;

        public static GaLaTeXComposerFloat64 LaTeXComposer
            => GaLaTeXComposerFloat64.DefaultComposer;

        public static GaTextComposerFloat64 TextComposer
            => GaTextComposerFloat64.DefaultComposer;


        public static GaVectorStorage<double> CreateVector(params double[] scalarArray)
        {
            return GaVectorStorage<double>.Create(
                ScalarProcessor,
                scalarArray
            );
        }

        public static IGaVectorStorage<double> CreateBasisVector(int index)
        {
            return GaVectorTermStorage<double>.CreateBasisVector(
                ScalarProcessor,
                index
            );
        }

        public static GaVectorsLinearMap<double> CreateVectorsLinearMap(int basisVectorsCount, Func<IGaVectorStorage<double>, IGaVectorStorage<double>> basisVectorMapFunc)
        {
            return GaVectorsLinearMap<double>.Create(
                ScalarProcessor,
                basisVectorsCount,
                basisVectorMapFunc
            );
        }


        public static Matrix GetMatrix(this IGaVectorsLinearMap<double> linearMap, int rowsCount, int columnsCount)
        {
            return MatrixProcessor.CreateMatrix(
                linearMap.GetArray(rowsCount, columnsCount)
            );
        }


        public static Matrix ArrayToMatrix(this double[,] array)
        {
            return (DenseMatrix) DenseMatrix.Build.DenseOfArray(array);
        }

        public static Matrix VectorToRowVectorMatrix(this IGaVectorStorage<double> vector, int vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                vector.VectorToArray(vSpaceDimension)
            );
        }

        public static Matrix VectorToColumnVectorMatrix(this IGaVectorStorage<double> vector, int vSpaceDimension)
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

        public static Matrix BivectorToRowVectorMatrix(this IGaBivectorStorage<double> bivector, int vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                bivector.BivectorToArray(vSpaceDimension)
            );
        }

        public static Matrix BivectorToColumnVectorMatrix(this IGaBivectorStorage<double> bivector, int vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                bivector.BivectorToArray(vSpaceDimension)
            );
        }

        public static Matrix BivectorToMatrix(this IGaBivectorStorage<double> bivector, int vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                bivector.BivectorToArray2D(vSpaceDimension)
            );
        }

        public static Matrix ScalarPlusBivectorToMatrix(this IGaMultivectorStorage<double> multivector, int vSpaceDimension)
        {
            return MatrixProcessor.CreateMatrix(
                multivector.ScalarPlusBivectorToArray2D(vSpaceDimension)
            );
        }

        public static Matrix KVectorToRowVectorMatrix(this IGaVectorStorage<double> vector, int vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                vector.KVectorToArray(vSpaceDimension)
            );
        }

        public static Matrix KVectorToColumnVectorMatrix(this IGaVectorStorage<double> vector, int vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                vector.KVectorToArray(vSpaceDimension)
            );
        }

        public static Matrix MultivectorToRowVectorMatrix(this IGaMultivectorStorage<double> multivector, int vSpaceDimension)
        {
            return MatrixProcessor.CreateRowVectorMatrix(
                multivector.MultivectorToArray(vSpaceDimension)
            );
        }

        public static Matrix MultivectorToColumnVectorMatrix(this IGaMultivectorStorage<double> multivector, int vSpaceDimension)
        {
            return MatrixProcessor.CreateColumnVectorMatrix(
                multivector.MultivectorToArray(vSpaceDimension)
            );
        }

        public static SparseMatrix VectorToSparseColumnVectorMatrix(this IGaVectorStorage<double> vector, int vSpaceDimension)
        {
            return (SparseMatrix) SparseMatrix.Build.SparseOfIndexed(
                vSpaceDimension,
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

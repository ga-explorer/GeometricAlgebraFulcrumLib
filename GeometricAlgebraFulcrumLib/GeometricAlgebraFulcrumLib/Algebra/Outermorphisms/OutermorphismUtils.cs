using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms
{
    public static class OutermorphismUtils
    {
        public static T[,] GetVectorsMappingArray<T>(this IGaOutermorphism<T> linearMap)
        {
            var rowsCount = linearMap.VSpaceDimension;
            var colsCount = linearMap.VSpaceDimension;
            var processor = linearMap.ScalarProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.MapBasisVector((uint) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
                        ? scalar
                        : processor.ZeroScalar;
            }

            return array;
        }

        public static T[,] GetVectorsMappingArray<T>(this IGaOutermorphism<T> linearMap, int rowsCount, int colsCount)
        {
            var processor = linearMap.ScalarProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.MapBasisVector((uint) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
                        ? scalar
                        : processor.ZeroScalar;
            }

            return array;
        }

        public static T[,] GetBivectorsMappingArray<T>(this IGaOutermorphism<T> linearMap)
        {
            return GetKVectorsMappingArray(linearMap, 2);
        }

        public static T[,] GetBivectorsMappingArray<T>(this IGaOutermorphism<T> linearMap, int rowsCount, int colsCount)
        {
            return GetKVectorsMappingArray(linearMap, 2, rowsCount, colsCount);
        }

        public static T[,] GetKVectorsMappingArray<T>(this IGaOutermorphism<T> linearMap, uint grade)
        {
            var rowsCount = (int) GaBasisUtils.KvSpaceDimension(linearMap.VSpaceDimension, grade);
            var colsCount = rowsCount;
            var processor = linearMap.ScalarProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.MapBasisBlade(grade, (uint) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
                        ? scalar
                        : processor.ZeroScalar;
            }

            return array;
        }

        public static T[,] GetKVectorsMappingArray<T>(this IGaOutermorphism<T> linearMap, uint grade, int rowsCount, int colsCount)
        {
            var processor = linearMap.ScalarProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.MapBasisBlade(grade, (uint) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
                        ? scalar
                        : processor.ZeroScalar;
            }

            return array;
        }

        public static T[,] GetMultivectorsMappingArray<T>(this IGaOutermorphism<T> linearMap)
        {
            var processor = linearMap.ScalarProcessor;
            var rowsCount = (int) linearMap.GaSpaceDimension;
            var colsCount = rowsCount;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.MapBasisBlade((ulong) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalar((ulong) i, out var scalar)
                        ? scalar
                        : processor.ZeroScalar;
            }

            return array;
        }

        public static T[,] GetMultivectorsMappingArray<T>(this IGaOutermorphism<T> linearMap, int rowsCount, int colsCount)
        {
            var processor = linearMap.ScalarProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.MapBasisBlade((ulong) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalar((ulong) i, out var scalar)
                        ? scalar
                        : processor.ZeroScalar;
            }

            return array;
        }

        public static T GetEuclideanDeterminant<T>(this IGaOutermorphism<T> om)
        {
            var mappedPseudoScalar = 
                om.MapBasisBlade((1UL << (int) om.VSpaceDimension) - 1);

            return mappedPseudoScalar.ESp(
                om.ScalarProcessor.CreateEuclideanPseudoScalarInverse(om.VSpaceDimension)
            );
        }

        public static T GetDeterminant<T>(this IGaOutermorphism<T> om, IGaSignature processor)
        {
            var mappedPseudoScalar = 
                om.MapBasisBlade((1UL << (int) om.VSpaceDimension) - 1);

            return processor.Sp(
                mappedPseudoScalar, 
                processor.CreatePseudoScalarInverse(om.ScalarProcessor)
            );
        }

        public static T GetDeterminant<T>(this IGaOutermorphism<T> om, IGaProcessor<T> processor)
        {
            var mappedPseudoScalar = 
                om.MapBasisBlade((1UL << (int) om.VSpaceDimension) - 1);

            return processor.Sp(
                mappedPseudoScalar, 
                processor.PseudoScalarInverse
            );
        }
    }
}

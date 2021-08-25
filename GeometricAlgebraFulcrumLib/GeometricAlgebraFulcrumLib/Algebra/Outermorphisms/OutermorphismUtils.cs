using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms
{
    public static class OutermorphismUtils
    {
        public static IGaGridEven<T> GetVectorsMappingArray<T>(this IGaOutermorphism<T> linearMap)
        {
            var rowsCount = linearMap.VSpaceDimension;
            var colsCount = linearMap.VSpaceDimension;
            var processor = linearMap.ScalarsGridProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.MapBasisVector((uint) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
                        ? scalar
                        : processor.GetZeroScalar();
            }

            return array.CreateEvenGridDense();
        }

        public static IGaGridEven<T> GetVectorsMappingArray<T>(this IGaOutermorphism<T> linearMap, int rowsCount, int colsCount)
        {
            var processor = linearMap.ScalarsGridProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.MapBasisVector((uint) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
                        ? scalar
                        : processor.GetZeroScalar();
            }

            return array.CreateEvenGridDense();
        }

        public static IGaGridEven<T> GetBivectorsMappingArray<T>(this IGaOutermorphism<T> linearMap)
        {
            return GetKVectorsMappingArray(linearMap, 2);
        }

        public static IGaGridEven<T> GetBivectorsMappingArray<T>(this IGaOutermorphism<T> linearMap, int rowsCount, int colsCount)
        {
            return GetKVectorsMappingArray(linearMap, 2, rowsCount, colsCount);
        }

        public static IGaGridEven<T> GetKVectorsMappingArray<T>(this IGaOutermorphism<T> linearMap, uint grade)
        {
            var rowsCount = (int) linearMap.VSpaceDimension.KVectorSpaceDimension(grade);
            var colsCount = rowsCount;
            var processor = linearMap.ScalarsGridProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.MapBasisBlade(grade, (uint) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
                        ? scalar
                        : processor.GetZeroScalar();
            }

            return array.CreateEvenGridDense();
        }

        public static IGaGridEven<T> GetKVectorsMappingArray<T>(this IGaOutermorphism<T> linearMap, uint grade, int rowsCount, int colsCount)
        {
            var processor = linearMap.ScalarsGridProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.MapBasisBlade(grade, (uint) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
                        ? scalar
                        : processor.GetZeroScalar();
            }

            return array.CreateEvenGridDense();
        }

        public static IGaGridEven<T> GetMultivectorsMappingArray<T>(this IGaOutermorphism<T> linearMap)
        {
            var processor = linearMap.ScalarsGridProcessor;
            var rowsCount = (int) linearMap.GaSpaceDimension;
            var colsCount = rowsCount;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.MapBasisBlade((ulong) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalar((ulong) i, out var scalar)
                        ? scalar
                        : processor.GetZeroScalar();
            }

            return array.CreateEvenGridDense();
        }

        public static IGaGridEven<T> GetMultivectorsMappingArray<T>(this IGaOutermorphism<T> linearMap, int rowsCount, int colsCount)
        {
            var processor = linearMap.ScalarsGridProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisBlade = linearMap.MapBasisBlade((ulong) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisBlade.TryGetTermScalar((ulong) i, out var scalar)
                        ? scalar
                        : processor.GetZeroScalar();
            }

            return array.CreateEvenGridDense();
        }

        public static T GetEuclideanDeterminant<T>(this IGaOutermorphism<T> om)
        {
            var scalarProcessor = om.ScalarsGridProcessor;

            var mappedPseudoScalar = 
                om.MapBasisBlade((1UL << (int) om.VSpaceDimension) - 1);

            return scalarProcessor.ESp(
                mappedPseudoScalar,
                om.ScalarsGridProcessor.CreateStorageEuclideanPseudoScalarInverse(om.VSpaceDimension)
            );
        }

        public static T GetDeterminant<T>(this IGaOutermorphism<T> om, IGaSignature signature)
        {
            var scalarProcessor = om.ScalarsGridProcessor;

            var mappedPseudoScalar = 
                om.MapBasisBlade((1UL << (int) om.VSpaceDimension) - 1);

            return scalarProcessor.Sp(
                signature,
                mappedPseudoScalar, 
                scalarProcessor.CreateStoragePseudoScalarInverse(signature)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaOutermorphism<T> CreateComputedOutermorphism<T>(this GaVectorsFrame<T> frame)
        {
            return frame.Processor.CreateComputedOutermorphism(
                frame.GetMatrix()
            );
        }
    }
}

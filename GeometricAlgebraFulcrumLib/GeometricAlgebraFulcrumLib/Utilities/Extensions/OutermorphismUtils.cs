using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class OutermorphismUtils
    {
        //TODO: Remove this
        public static ILinMatrixStorage<T> GetVectorOmMappingMatrix<T>(this IOutermorphism<T> linearMap, int rowsCount, int colsCount)
        {
            var processor = linearMap.LinearProcessor;
            var array = new T[rowsCount, colsCount];

            for (var index = 0; index < colsCount; index++)
            {
                var mappedBasisVector = linearMap.OmMapBasisVector((uint) index);

                for (var i = 0; i < rowsCount; i++)
                    array[i, index] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
                        ? scalar
                        : processor.ScalarZero;
            }

            return array.CreateLinMatrixDenseStorage();
        }

        //public static ILinMatrixStorage<T> GetKVectorOmMappingMatrix<T>(this IOutermorphism<T> linearMap, uint grade)
        //{
        //    var rowsCount = (int) linearMap.GeometricProcessor.VSpaceDimension.KVectorSpaceDimension(grade);
        //    var colsCount = rowsCount;
        //    var processor = linearMap.LinearProcessor;
        //    var array = new T[rowsCount, colsCount];

        //    for (var index = 0; index < colsCount; index++)
        //    {
        //        var mappedBasisVector = linearMap.OmMapBasisBlade(grade, (uint) index);

        //        for (var i = 0; i < rowsCount; i++)
        //            array[i, index] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
        //                ? scalar
        //                : processor.ScalarZero;
        //    }

        //    return array.CreateLinMatrixDenseStorage();
        //}

        //public static ILinMatrixStorage<T> GetKVectorsMappingArray<T>(this IOutermorphism<T> linearMap, uint grade, int rowsCount, int colsCount)
        //{
        //    var processor = linearMap.LinearProcessor;
        //    var array = new T[rowsCount, colsCount];

        //    for (var index = 0; index < colsCount; index++)
        //    {
        //        var mappedBasisVector = linearMap.OmMapBasisBlade(grade, (uint) index);

        //        for (var i = 0; i < rowsCount; i++)
        //            array[i, index] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
        //                ? scalar
        //                : processor.ScalarZero;
        //    }

        //    return array.CreateLinMatrixDenseStorage();
        //}

        //public static ILinMatrixStorage<T> GetMultivectorsMappingArray<T>(this IOutermorphism<T> linearMap)
        //{
        //    var processor = linearMap.LinearProcessor;
        //    var rowsCount = (int) linearMap.GeometricProcessor.GaSpaceDimension;
        //    var colsCount = rowsCount;
        //    var array = new T[rowsCount, colsCount];

        //    for (var index = 0; index < colsCount; index++)
        //    {
        //        var mappedBasisVector = linearMap.OmMapBasisBlade((ulong) index);

        //        for (var i = 0; i < rowsCount; i++)
        //            array[i, index] = mappedBasisVector.TryGetTermScalar((ulong) i, out var scalar)
        //                ? scalar
        //                : processor.ScalarZero;
        //    }

        //    return array.CreateLinMatrixDenseStorage();
        //}

        //public static ILinMatrixStorage<T> GetMultivectorsMappingArray<T>(this IOutermorphism<T> linearMap, int rowsCount, int colsCount)
        //{
        //    var processor = linearMap.LinearProcessor;
        //    var array = new T[rowsCount, colsCount];

        //    for (var index = 0; index < colsCount; index++)
        //    {
        //        var mappedBasisBlade = linearMap.OmMapBasisBlade((ulong) index);

        //        for (var i = 0; i < rowsCount; i++)
        //            array[i, index] = mappedBasisBlade.TryGetTermScalar((ulong) i, out var scalar)
        //                ? scalar
        //                : processor.ScalarZero;
        //    }

        //    return array.CreateLinMatrixDenseStorage();
        //}

        public static T GetEuclideanDeterminant<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IOutermorphism<T> om)
        {
            var mappedPseudoScalar = 
                om.OmMapBasisBlade(geometricProcessor.MaxBasisBladeId);

            return geometricProcessor.ESp(
                mappedPseudoScalar,
                geometricProcessor.CreateEuclideanPseudoScalarInverseStorage(
                    geometricProcessor.VSpaceDimension
                )
            );
        }

        public static T GetDeterminant<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IOutermorphism<T> om)
        {
            return geometricProcessor.Sp(
                om.OmMapBasisBlade(geometricProcessor.MaxBasisBladeId), 
                geometricProcessor.PseudoScalarInverse
            );
        }

        public static T GetDeterminant<T>(this IOutermorphism<T> om, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return geometricProcessor.Sp(
                om.OmMapBasisBlade(geometricProcessor.MaxBasisBladeId), 
                geometricProcessor.PseudoScalarInverse
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOutermorphism<T> CreateComputedOutermorphism<T>(this GeoFreeFrame<T> frame)
        {
            return frame.GeometricProcessor.CreateLinearMapOutermorphism(
                frame.GetMatrix()
            );
        }
    }
}

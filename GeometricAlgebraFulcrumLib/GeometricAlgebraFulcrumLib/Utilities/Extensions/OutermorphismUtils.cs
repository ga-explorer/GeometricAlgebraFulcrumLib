using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class OutermorphismUtils
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> OmMapMultivector<T>(this IOutermorphism<T> map, IMultivectorStorage<T> mv)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => map.OmMapVector(mv1),
                BivectorStorage<T> mv1 => map.OmMapBivector(mv1),
                KVectorStorage<T> mv1 => map.OmMapKVector(mv1),
                MultivectorStorage<T> mv1 => map.OmMapMultivector(mv1),
                MultivectorGradedStorage<T> mv1 => map.OmMapMultivector(mv1),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> OmMapFreeFrame<T>(this IOutermorphism<T> outermorphism, GeoFreeFrame<T> frame)
        {
            return new GeoFreeFrame<T>(
                frame.GeometricProcessor,
                frame.FrameKind,
                frame.Select(outermorphism.OmMapVector)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> OmMapPseudoScalar<T>(this IOutermorphism<T> outermorphism, uint vSpaceDimension)
        {
            return outermorphism.OmMapBasisBlade(vSpaceDimension.GetMaxBasisBladeId());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> OmMapBasisVector<T>(this IReadOnlyList<IOutermorphism<T>> omList, ulong index)
        {
            var kVector = omList[0].OmMapBasisVector(index);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMapVector(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> OmMapBasisBivector<T>(this IReadOnlyList<IOutermorphism<T>> omList, ulong index)
        {
            var kVector = omList[0].OmMapBasisBivector(index);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMapBivector(v)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> OmMapBasisBivector<T>(this IReadOnlyList<IOutermorphism<T>> omList, ulong index1, ulong index2)
        {
            var kVector = omList[0].OmMapBasisBivector(index1, index2);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMapBivector(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> OmMapBasisBlade<T>(this IReadOnlyList<IOutermorphism<T>> omList, uint grade, ulong index)
        {
            var kVector = omList[0].OmMapBasisBlade(grade, index);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMapKVector(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> OmMapBasisBlade<T>(this IReadOnlyList<IOutermorphism<T>> omList, ulong id)
        {
            var kVector = omList[0].OmMapBasisBlade(id);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMapKVector(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> OmMapPseudoScalar<T>(this IReadOnlyList<IOutermorphism<T>> omList, uint vSpaceDimension)
        {
            var kVector = omList[0].OmMapPseudoScalar(vSpaceDimension);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMapKVector(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> OmMapVector<T>(this IEnumerable<IOutermorphism<T>> omSeq, VectorStorage<T> vector)
        {
            return omSeq
                .Aggregate(
                    vector, 
                    (v, om) => om.OmMapVector(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> OmMapBivector<T>(this IEnumerable<IOutermorphism<T>> omSeq, BivectorStorage<T> vector)
        {
            return omSeq
                .Aggregate(
                    vector, 
                    (v, om) => om.OmMapBivector(v)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> OmMapKVector<T>(this IEnumerable<IOutermorphism<T>> omSeq, KVectorStorage<T> vector)
        {
            return omSeq
                .Aggregate(
                    vector, 
                    (v, om) => om.OmMapKVector(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> OmMapMultivector<T>(this IEnumerable<IOutermorphism<T>> omSeq, MultivectorGradedStorage<T> vector)
        {
            return omSeq
                .Aggregate(
                    vector, 
                    (v, om) => om.OmMapMultivector(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> OmMapMultivector<T>(this IEnumerable<IOutermorphism<T>> omSeq, MultivectorStorage<T> vector)
        {
            return omSeq
                .Aggregate(
                    vector, 
                    (v, om) => om.OmMapMultivector(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> OmMapFreeFrame<T>(this IEnumerable<IOutermorphism<T>> omSeq, GeoFreeFrame<T> frame)
        {
            return new GeoFreeFrame<T>(
                frame.GeometricProcessor,
                frame.FrameKind,
                frame.Select(omSeq.OmMapVector)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> OmMapMultivector<T>(this IEnumerable<IOutermorphism<T>> omSeq, IMultivectorStorage<T> mv)
        {
            return mv switch
            {
                VectorStorage<T> mv1 => omSeq.OmMapVector(mv1),
                BivectorStorage<T> mv1 => omSeq.OmMapBivector(mv1),
                KVectorStorage<T> mv1 => omSeq.OmMapKVector(mv1),
                MultivectorStorage<T> mv1 => omSeq.OmMapMultivector(mv1),
                MultivectorGradedStorage<T> mv1 => omSeq.OmMapMultivector(mv1),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] GetFinalMappingArray<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<IOutermorphism<T>> omSeq, int rowsCount)
        {
            return omSeq.OmMapFreeFrame(
                geometricProcessor.CreateBasisFreeFrame((uint) rowsCount)
            ).GetArray(rowsCount);
        }


        public static IEnumerable<VectorStorage<T>> OmMapVectorSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, VectorStorage<T> vector)
        {
            var v = vector;

            yield return v;

            foreach (var om in omSeq)
            {
                v = om.OmMapVector(v);

                yield return v;
            }
        }
        
        public static IEnumerable<BivectorStorage<T>> OmMapBivectorSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, BivectorStorage<T> vector)
        {
            var v = vector;

            yield return v;

            foreach (var om in omSeq)
            {
                v = om.OmMapBivector(v);

                yield return v;
            }
        }
        
        public static IEnumerable<KVectorStorage<T>> OmMapKVectorSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, KVectorStorage<T> vector)
        {
            var v = vector;

            yield return v;

            foreach (var om in omSeq)
            {
                v = om.OmMapKVector(v);

                yield return v;
            }
        }
        
        public static IEnumerable<MultivectorGradedStorage<T>> OmMapMultivectorSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, MultivectorGradedStorage<T> vector)
        {
            var v = vector;

            yield return v;

            foreach (var om in omSeq)
            {
                v = om.OmMapMultivector(v);

                yield return v;
            }
        }
        
        public static IEnumerable<MultivectorStorage<T>> OmMapMultivectorSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, MultivectorStorage<T> vector)
        {
            var v = vector;

            yield return v;

            foreach (var om in omSeq)
            {
                v = om.OmMapMultivector(v);

                yield return v;
            }
        }

        public static IEnumerable<GeoFreeFrame<T>> OmMapFreeFrameSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, GeoFreeFrame<T> frame)
        {
            var f = frame;

            yield return f;

            foreach (var om in omSeq)
            {
                f = om.OmMapFreeFrame(f);

                yield return f;
            }
        }


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
                frame.GetArray()
            );
        }
    }
}

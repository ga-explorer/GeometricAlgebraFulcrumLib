using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
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
        public static GaVector<T> OmMap<T>(this IOutermorphism<T> outermorphism, Axis axis)
        {
            return axis.IsPositive
                ? outermorphism.OmMapBasisVector(axis.BasisVectorIndex)
                : -outermorphism.OmMapBasisVector(axis.BasisVectorIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> OmMap<T>(this IOutermorphism<T> map, VectorStorage<T> mv)
        {
            var processor = map.GeometricProcessor;

            return map.OmMap(mv.CreateVector(processor)).VectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> OmMap<T>(this IOutermorphism<T> map, BivectorStorage<T> mv)
        {
            var processor = map.GeometricProcessor;

            return map.OmMap(mv.CreateBivector(processor)).BivectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> OmMap<T>(this IOutermorphism<T> map, KVectorStorage<T> mv)
        {
            var processor = map.GeometricProcessor;

            return mv switch
            {
                VectorStorage<T> mv1 => map.OmMap(mv1.CreateVector(processor)).VectorStorage,
                BivectorStorage<T> mv1 => map.OmMap(mv1.CreateBivector(processor)).BivectorStorage,
                _ => map.OmMap(mv.CreateKVector(processor)).KVectorStorage
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> OmMap<T>(this IOutermorphism<T> map, IMultivectorGradedStorage<T> mv)
        {
            var processor = map.GeometricProcessor;

            return mv switch
            {
                VectorStorage<T> mv1 => map.OmMap(mv1.CreateVector(processor)).VectorStorage,
                BivectorStorage<T> mv1 => map.OmMap(mv1.CreateBivector(processor)).BivectorStorage,
                KVectorStorage<T> mv1 => map.OmMap(mv1.CreateKVector(processor)).KVectorStorage,
                _ => map.OmMap(mv.CreateMultivector(processor)).MultivectorStorage.ToMultivectorGradedStorage()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> OmMap<T>(this IOutermorphism<T> map, IMultivectorStorage<T> mv)
        {
            var processor = map.GeometricProcessor;

            return mv switch
            {
                VectorStorage<T> mv1 => map.OmMap(mv1.CreateVector(processor)).VectorStorage,
                BivectorStorage<T> mv1 => map.OmMap(mv1.CreateBivector(processor)).BivectorStorage,
                KVectorStorage<T> mv1 => map.OmMap(mv1.CreateKVector(processor)).KVectorStorage,
                _ => map.OmMap(mv.CreateMultivector(processor)).MultivectorStorage
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrame<T> OmMap<T>(this IOutermorphism<T> outermorphism, VectorFrame<T> frame)
        {
            return VectorFrame<T>.Create(
                frame.GeometricProcessor,
                frame.FrameSpecs,
                frame.Select(outermorphism.OmMap)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> OmMapPseudoScalar<T>(this IOutermorphism<T> outermorphism, uint vSpaceDimension)
        {
            return outermorphism.OmMapBasisBlade(vSpaceDimension.GetMaxBasisBladeId());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> OmMapBasisVector<T>(this IReadOnlyList<IOutermorphism<T>> omList, ulong index)
        {
            var kVector = omList[0].OmMapBasisVector(index);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMap(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> OmMapBasisBivector<T>(this IReadOnlyList<IOutermorphism<T>> omList, ulong index)
        {
            var kVector = omList[0].OmMapBasisBivector(index);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMap(v)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> OmMapBasisBivector<T>(this IReadOnlyList<IOutermorphism<T>> omList, ulong index1, ulong index2)
        {
            var kVector = omList[0].OmMapBasisBivector(index1, index2);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMap(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> OmMapBasisBlade<T>(this IReadOnlyList<IOutermorphism<T>> omList, uint grade, ulong index)
        {
            var kVector = omList[0].OmMapBasisBlade(grade, index);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMap(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> OmMapBasisBlade<T>(this IReadOnlyList<IOutermorphism<T>> omList, ulong id)
        {
            var kVector = omList[0].OmMapBasisBlade(id);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMap(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> OmMapPseudoScalar<T>(this IReadOnlyList<IOutermorphism<T>> omList, uint vSpaceDimension)
        {
            var kVector = omList[0].OmMapPseudoScalar(vSpaceDimension);

            return omList
                .Skip(1)
                .Aggregate(
                    kVector, 
                    (v, om) => om.OmMap(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> OmMap<T>(this IEnumerable<IOutermorphism<T>> omSeq, GaVector<T> vector)
        {
            return omSeq
                .Aggregate(
                    vector, 
                    (v, om) => om.OmMap(v)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> OmMap<T>(this IEnumerable<IOutermorphism<T>> omSeq, GaBivector<T> vector)
        {
            return omSeq
                .Aggregate(
                    vector, 
                    (v, om) => om.OmMap(v)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> OmMap<T>(this IEnumerable<IOutermorphism<T>> omSeq, GaKVector<T> vector)
        {
            return omSeq
                .Aggregate(
                    vector, 
                    (v, om) => om.OmMap(v)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> OmMap<T>(this IEnumerable<IOutermorphism<T>> omSeq, GaMultivector<T> vector)
        {
            return omSeq
                .Aggregate(
                    vector, 
                    (v, om) => om.OmMap(v)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrame<T> OmMap<T>(this IEnumerable<IOutermorphism<T>> omSeq, VectorFrame<T> frame)
        {
            return VectorFrame<T>.Create(
                frame.GeometricProcessor,
                frame.FrameSpecs,
                frame.Select(omSeq.OmMap)
            );
        }
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IMultivectorStorage<T> OmMap<T>(this IEnumerable<IOutermorphism<T>> omSeq, IMultivectorStorage<T> mv)
        //{
        //    return mv switch
        //    {
        //        VectorStorage<T> mv1 => omSeq.OmMap(mv1),
        //        BivectorStorage<T> mv1 => omSeq.OmMap(mv1),
        //        KVectorStorage<T> mv1 => omSeq.OmMap(mv1),
        //        MultivectorStorage<T> mv1 => omSeq.OmMap(mv1),
        //        MultivectorGradedStorage<T> mv1 => omSeq.OmMap(mv1),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] GetFinalMappingArray<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<IOutermorphism<T>> omSeq, int rowsCount)
        {
            return omSeq.OmMap(
                geometricProcessor.CreateFreeFrameOfBasis((uint) rowsCount)
            ).GetArray(rowsCount);
        }


        public static IEnumerable<GaVector<T>> OmMapVectorSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, GaVector<T> vector)
        {
            var v = vector;

            yield return v;

            foreach (var om in omSeq)
            {
                v = om.OmMap(v);

                yield return v;
            }
        }
        
        public static IEnumerable<GaBivector<T>> OmMapBivectorSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, GaBivector<T> vector)
        {
            var v = vector;

            yield return v;

            foreach (var om in omSeq)
            {
                v = om.OmMap(v);

                yield return v;
            }
        }
        
        public static IEnumerable<GaKVector<T>> OmMapKVectorSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, GaKVector<T> vector)
        {
            var v = vector;

            yield return v;

            foreach (var om in omSeq)
            {
                v = om.OmMap(v);

                yield return v;
            }
        }
        
        //public static IEnumerable<MultivectorGradedStorage<T>> OmMapMultivectorSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, MultivectorGradedStorage<T> vector)
        //{
        //    var v = vector;

        //    yield return v;

        //    foreach (var om in omSeq)
        //    {
        //        v = om.OmMap(v);

        //        yield return v;
        //    }
        //}
        
        public static IEnumerable<GaMultivector<T>> OmMapMultivectorSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, GaMultivector<T> vector)
        {
            var v = vector;

            yield return v;

            foreach (var om in omSeq)
            {
                v = om.OmMap(v);

                yield return v;
            }
        }

        public static IEnumerable<VectorFrame<T>> OmMapFreeFrameSequence<T>(this IEnumerable<IOutermorphism<T>> omSeq, VectorFrame<T> frame)
        {
            var f = frame;

            yield return f;

            foreach (var om in omSeq)
            {
                f = om.OmMap(f);

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
                    array[i, index] = mappedBasisVector.VectorStorage.TryGetTermScalarByIndex((ulong) i, out var scalar)
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
                mappedPseudoScalar.KVectorStorage,
                geometricProcessor.CreateKVectorStorageEuclideanPseudoScalarInverse(
                    geometricProcessor.VSpaceDimension
                )
            );
        }

        public static Scalar<T> GetDeterminant<T>(this IOutermorphism<T> om)
        {
            var geometricProcessor = om.GeometricProcessor;

            return om
                .OmMapBasisBlade(geometricProcessor.MaxBasisBladeId)
                .Sp(geometricProcessor.PseudoScalarInverse);
        }
        
        public static Scalar<T> GetDeterminant<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IOutermorphism<T> om)
        {
            return om
                .OmMapBasisBlade(geometricProcessor.MaxBasisBladeId)
                .Sp(geometricProcessor.PseudoScalarInverse);
        }

        public static Scalar<T> GetDeterminant<T>(this IOutermorphism<T> om, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return om
                .OmMapBasisBlade(geometricProcessor.MaxBasisBladeId)
                .Sp(geometricProcessor.PseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOutermorphism<T> CreateComputedOutermorphism<T>(this VectorFrame<T> frame)
        {
            return frame.GeometricProcessor.CreateLinearMapOutermorphism(
                frame.GetArray()
            );
        }
    }
}

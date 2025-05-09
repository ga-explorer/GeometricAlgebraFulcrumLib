using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;

public static class RGaOutermorphismUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> OmMap<T>(this IRGaOutermorphism<T> outermorphism, LinSignedBasisVector axis)
    {
        return axis.IsPositive
            ? outermorphism.OmMapBasisVector(axis.Index)
            : -outermorphism.OmMapBasisVector(axis.Index);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVectorFrame<T> OmMap<T>(this IRGaOutermorphism<T> outermorphism, RGaVectorFrame<T> frame)
    {
        return RGaVectorFrame<T>.Create(
            frame.FrameSpecs,
            frame.Select(outermorphism.OmMap)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> OmMapPseudoScalar<T>(this IRGaOutermorphism<T> outermorphism, int vSpaceDimensions)
    {
        return outermorphism.OmMapBasisBlade(
            outermorphism.Processor.GetBasisPseudoScalarId(vSpaceDimensions)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> OmMapBasisVector<T>(this IReadOnlyList<IRGaOutermorphism<T>> omList, int index)
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
    public static RGaBivector<T> OmMapBasisBivector<T>(this IReadOnlyList<IRGaOutermorphism<T>> omList, int index1, int index2)
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
    public static RGaKVector<T> OmMapBasisBlade<T>(this IReadOnlyList<IRGaOutermorphism<T>> omList, ulong id)
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
    public static RGaKVector<T> OmMapPseudoScalar<T>(this IReadOnlyList<IRGaOutermorphism<T>> omList, int vSpaceDimensions)
    {
        var kVector = omList[0].OmMapPseudoScalar(vSpaceDimensions);

        return omList
            .Skip(1)
            .Aggregate(
                kVector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> OmMap<T>(this IEnumerable<IRGaOutermorphism<T>> omSeq, RGaVector<T> vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> OmMap<T>(this IEnumerable<IRGaOutermorphism<T>> omSeq, RGaBivector<T> vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> OmMap<T>(this IEnumerable<IRGaOutermorphism<T>> omSeq, RGaKVector<T> vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> OmMap<T>(this IEnumerable<IRGaOutermorphism<T>> omSeq, RGaMultivector<T> vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVectorFrame<T> OmMap<T>(this IEnumerable<IRGaOutermorphism<T>> omSeq, RGaVectorFrame<T> frame)
    {
        return RGaVectorFrame<T>.Create(
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
    public static T[,] GetFinalMappingArray<T>(this RGaProcessor<T> processor, IEnumerable<IRGaOutermorphism<T>> omSeq, int rowsCount)
    {
        return omSeq.OmMap(
            processor.CreateFreeFrameOfBasis(rowsCount)
        ).GetArray(rowsCount);
    }


    public static IEnumerable<RGaVector<T>> OmMapVectorSequence<T>(this IEnumerable<IRGaOutermorphism<T>> omSeq, RGaVector<T> vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<RGaBivector<T>> OmMapBivectorSequence<T>(this IEnumerable<IRGaOutermorphism<T>> omSeq, RGaBivector<T> vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<RGaKVector<T>> OmMapKVectorSequence<T>(this IEnumerable<IRGaOutermorphism<T>> omSeq, RGaKVector<T> vector)
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

    public static IEnumerable<RGaMultivector<T>> OmMapMultivectorSequence<T>(this IEnumerable<IRGaOutermorphism<T>> omSeq, RGaMultivector<T> vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<RGaVectorFrame<T>> OmMapFreeFrameSequence<T>(this IEnumerable<IRGaOutermorphism<T>> omSeq, RGaVectorFrame<T> frame)
    {
        var f = frame;

        yield return f;

        foreach (var om in omSeq)
        {
            f = om.OmMap(f);

            yield return f;
        }
    }


    ////TODO: Remove this
    //public static ILinMatrixStorage<T> GetVectorOmMappingMatrix<T>(this IRGaOutermorphism<T> linearMap, int rowsCount, int colsCount)
    //{
    //    var processor = linearMap.LinearProcessor;
    //    var array = new T[rowsCount, colsCount];

    //    for (var index = 0; index < colsCount; index++)
    //    {
    //        var mappedBasisVector = linearMap.OmMapBasisVector((uint)index);

    //        for (var i = 0; i < rowsCount; i++)
    //            array[i, index] = mappedBasisVector.VectorStorage.TryGetTermScalarByIndex((ulong)i, out var scalar)
    //                ? scalar
    //                : processor.ScalarZero;
    //    }

    //    return array.CreateLinMatrixDenseStorage();
    //}

    //public static ILinMatrixStorage<T> GetKVectorOmMappingMatrix<T>(this IOutermorphism<T> linearMap, uint grade)
    //{
    //    var rowsCount = (int) linearMap.GeometricProcessor.VSpaceDimensions.KVectorSpaceDimension(grade);
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
    //    var rowsCount = (int) linearMap.GeometricProcessor.GaSpaceDimensions;
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

    public static T GetEuclideanDeterminant<T>(this IRGaOutermorphism<T> om, int vSpaceDimensions)
    {
        var mappedPseudoScalar =
            om.OmMapBasisBlade(om.Processor.GetBasisPseudoScalarId(vSpaceDimensions));

        return mappedPseudoScalar.ESp(
            om.Processor.PseudoScalarEInverse(
                vSpaceDimensions
            )
        ).ScalarValue;
    }

    public static Scalar<T> GetDeterminant<T>(this IRGaOutermorphism<T> om, int vSpaceDimensions)
    {
        return om
            .OmMapBasisBlade(om.Processor.GetBasisPseudoScalarId(vSpaceDimensions))
            .Sp(om.Processor.PseudoScalarInverse(
                vSpaceDimensions
            )).Scalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IRGaOutermorphism<T> CreateComputedOutermorphism<T>(this RGaVectorFrame<T> frame)
    {
        return frame
            .Select(v => v.ToLinVector())
            .ToLinUnilinearMap(frame.ScalarProcessor)
            .ToOutermorphism(frame.Processor);
    }
}
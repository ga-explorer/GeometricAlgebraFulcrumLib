using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;

public static class XGaOutermorphismUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> OmMap<T>(this IXGaOutermorphism<T> outermorphism, LinSignedBasisVector axis)
    {
        return axis.IsPositive
            ? outermorphism.OmMapBasisVector(axis.Index)
            : -outermorphism.OmMapBasisVector(axis.Index);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrame<T> OmMap<T>(this IXGaOutermorphism<T> outermorphism, XGaVectorFrame<T> frame)
    {
        return XGaVectorFrame<T>.Create(
            frame.FrameSpecs,
            frame.Select(outermorphism.OmMap)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> OmMapPseudoScalar<T>(this IXGaOutermorphism<T> outermorphism, int vSpaceDimensions)
    {
        return outermorphism.OmMapBasisBlade(
            outermorphism.Metric.GetBasisPseudoScalarId(vSpaceDimensions)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> OmMapBasisVector<T>(this IReadOnlyList<IXGaOutermorphism<T>> omList, int index)
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
    public static XGaBivector<T> OmMapBasisBivector<T>(this IReadOnlyList<IXGaOutermorphism<T>> omList, int index1, int index2)
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
    public static XGaKVector<T> OmMapBasisBlade<T>(this IReadOnlyList<IXGaOutermorphism<T>> omList, IndexSet id)
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
    public static XGaKVector<T> OmMapPseudoScalar<T>(this IReadOnlyList<IXGaOutermorphism<T>> omList, int vSpaceDimensions)
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
    public static XGaVector<T> OmMap<T>(this IEnumerable<IXGaOutermorphism<T>> omSeq, XGaVector<T> vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> OmMap<T>(this IEnumerable<IXGaOutermorphism<T>> omSeq, XGaBivector<T> vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> OmMap<T>(this IEnumerable<IXGaOutermorphism<T>> omSeq, XGaKVector<T> vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> OmMap<T>(this IEnumerable<IXGaOutermorphism<T>> omSeq, XGaMultivector<T> vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrame<T> OmMap<T>(this IEnumerable<IXGaOutermorphism<T>> omSeq, XGaVectorFrame<T> frame)
    {
        return XGaVectorFrame<T>.Create(
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
    public static T[,] GetVectorMapArray<T>(this IXGaOutermorphism<T> om, int size)
    {
        return om.GetVectorMapPart(size).ToArray(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] GetVectorMapArray<T>(this IXGaOutermorphism<T> om, int rowCount, int colCount)
    {
        return om.GetVectorMapPart(colCount).ToArray(rowCount, colCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] GetFinalMappingArray<T>(this XGaProcessor<T> metric, IEnumerable<IXGaOutermorphism<T>> omSeq, int rowsCount)
    {
        return omSeq.OmMap(
            metric.CreateFreeFrameOfBasis(rowsCount)
        ).GetArray(rowsCount);
    }


    public static IEnumerable<XGaVector<T>> OmMapVectorSequence<T>(this IEnumerable<IXGaOutermorphism<T>> omSeq, XGaVector<T> vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<XGaBivector<T>> OmMapBivectorSequence<T>(this IEnumerable<IXGaOutermorphism<T>> omSeq, XGaBivector<T> vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<XGaKVector<T>> OmMapKVectorSequence<T>(this IEnumerable<IXGaOutermorphism<T>> omSeq, XGaKVector<T> vector)
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

    public static IEnumerable<XGaMultivector<T>> OmMapMultivectorSequence<T>(this IEnumerable<IXGaOutermorphism<T>> omSeq, XGaMultivector<T> vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<XGaVectorFrame<T>> OmMapFreeFrameSequence<T>(this IEnumerable<IXGaOutermorphism<T>> omSeq, XGaVectorFrame<T> frame)
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
    //public static ILinMatrixStorage<T> GetVectorOmMappingMatrix<T>(this IXGaOutermorphism<T> linearMap, int rowsCount, int colsCount)
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

    public static T GetEuclideanDeterminant<T>(this IXGaOutermorphism<T> om, int vSpaceDimensions)
    {
        var mappedPseudoScalar =
            om.OmMapBasisBlade(om.Metric.GetBasisPseudoScalarId(vSpaceDimensions));

        return mappedPseudoScalar.ESp(
            om.Processor.PseudoScalarEInverse(vSpaceDimensions)
        ).ScalarValue;
    }

    public static Scalar<T> GetDeterminant<T>(this IXGaOutermorphism<T> om, int vSpaceDimensions)
    {
        return om
            .OmMapBasisBlade(om.Metric.GetBasisPseudoScalarId(vSpaceDimensions))
            .Sp(om.Processor.PseudoScalarInverse(vSpaceDimensions)).Scalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IXGaOutermorphism<T> CreateComputedOutermorphism<T>(this XGaVectorFrame<T> frame)
    {
        return frame
            .Select(v => v.ToLinVector())
            .ToLinUnilinearMap(frame.ScalarProcessor)
            .ToOutermorphism(frame.Processor);
    }
}
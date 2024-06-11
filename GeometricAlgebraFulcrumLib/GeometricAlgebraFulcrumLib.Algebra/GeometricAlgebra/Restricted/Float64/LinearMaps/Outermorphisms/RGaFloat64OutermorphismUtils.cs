using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;

public static class RGaFloat64OutermorphismUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector OmMap(this IRGaFloat64Outermorphism outermorphism, LinSignedBasisVector axis)
    {
        return axis.IsPositive
            ? outermorphism.OmMapBasisVector(axis.Index)
            : -outermorphism.OmMapBasisVector(axis.Index);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrame OmMap(this IRGaFloat64Outermorphism outermorphism, RGaFloat64VectorFrame frame)
    {
        return RGaFloat64VectorFrame.Create(
            frame.FrameSpecs,
            frame.Select(outermorphism.OmMap)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector OmMapPseudoScalar(this IRGaFloat64Outermorphism outermorphism, int vSpaceDimensions)
    {
        return outermorphism.OmMapBasisBlade(
            outermorphism.Processor.GetBasisPseudoScalarId(vSpaceDimensions)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector OmMapBasisVector(this IReadOnlyList<IRGaFloat64Outermorphism> omList, int index)
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
    public static RGaFloat64Bivector OmMapBasisBivector(this IReadOnlyList<IRGaFloat64Outermorphism> omList, int index1, int index2)
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
    public static RGaFloat64KVector OmMapBasisBlade(this IReadOnlyList<IRGaFloat64Outermorphism> omList, ulong id)
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
    public static RGaFloat64KVector OmMapPseudoScalar(this IReadOnlyList<IRGaFloat64Outermorphism> omList, int vSpaceDimensions)
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
    public static RGaFloat64Vector OmMap(this IEnumerable<IRGaFloat64Outermorphism> omSeq, RGaFloat64Vector vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector OmMap(this IEnumerable<IRGaFloat64Outermorphism> omSeq, RGaFloat64Bivector vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector OmMap(this IEnumerable<IRGaFloat64Outermorphism> omSeq, RGaFloat64KVector vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector OmMap(this IEnumerable<IRGaFloat64Outermorphism> omSeq, RGaFloat64Multivector vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrame OmMap(this IEnumerable<IRGaFloat64Outermorphism> omSeq, RGaFloat64VectorFrame frame)
    {
        return RGaFloat64VectorFrame.Create(
            frame.FrameSpecs,
            frame.Select(omSeq.OmMap)
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IMultivectorStorage OmMap(this IEnumerable<IOutermorphism> omSeq, IMultivectorStorage mv)
    //{
    //    return mv switch
    //    {
    //        VectorStorage mv1 => omSeq.OmMap(mv1),
    //        BivectorStorage mv1 => omSeq.OmMap(mv1),
    //        KVectorStorage mv1 => omSeq.OmMap(mv1),
    //        MultivectorStorage mv1 => omSeq.OmMap(mv1),
    //        MultivectorGradedStorage mv1 => omSeq.OmMap(mv1),
    //        _ => throw new InvalidOperationException()
    //    };
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] GetFinalMappingArray(this RGaFloat64Processor metric, IEnumerable<IRGaFloat64Outermorphism> omSeq, int rowsCount)
    {
        return omSeq.OmMap(
            metric.CreateFreeFrameOfBasis(rowsCount)
        ).GetArray(rowsCount);
    }


    public static IEnumerable<RGaFloat64Vector> OmMapVectorSequence(this IEnumerable<IRGaFloat64Outermorphism> omSeq, RGaFloat64Vector vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<RGaFloat64Bivector> OmMapBivectorSequence(this IEnumerable<IRGaFloat64Outermorphism> omSeq, RGaFloat64Bivector vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<RGaFloat64KVector> OmMapKVectorSequence(this IEnumerable<IRGaFloat64Outermorphism> omSeq, RGaFloat64KVector vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    //public static IEnumerable<MultivectorGradedStorage> OmMapMultivectorSequence(this IEnumerable<IOutermorphism> omSeq, MultivectorGradedStorage vector)
    //{
    //    var v = vector;

    //    yield return v;

    //    foreach (var om in omSeq)
    //    {
    //        v = om.OmMap(v);

    //        yield return v;
    //    }
    //}

    public static IEnumerable<RGaFloat64Multivector> OmMapMultivectorSequence(this IEnumerable<IRGaFloat64Outermorphism> omSeq, RGaFloat64Multivector vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<RGaFloat64VectorFrame> OmMapFreeFrameSequence(this IEnumerable<IRGaFloat64Outermorphism> omSeq, RGaFloat64VectorFrame frame)
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
    //public static ILinMatrixStorage GetVectorOmMappingMatrix(this IRGaOutermorphism linearMap, int rowsCount, int colsCount)
    //{
    //    var processor = linearMap.LinearProcessor;
    //    var array = new double[rowsCount, colsCount];

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

    //public static ILinMatrixStorage GetKVectorOmMappingMatrix(this IOutermorphism linearMap, uint grade)
    //{
    //    var rowsCount = (int) linearMap.GeometricProcessor.VSpaceDimensions.KVectorSpaceDimension(grade);
    //    var colsCount = rowsCount;
    //    var processor = linearMap.LinearProcessor;
    //    var array = new double[rowsCount, colsCount];

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

    //public static ILinMatrixStorage GetKVectorsMappingArray(this IOutermorphism linearMap, uint grade, int rowsCount, int colsCount)
    //{
    //    var processor = linearMap.LinearProcessor;
    //    var array = new double[rowsCount, colsCount];

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

    //public static ILinMatrixStorage GetMultivectorsMappingArray(this IOutermorphism linearMap)
    //{
    //    var processor = linearMap.LinearProcessor;
    //    var rowsCount = (int) linearMap.GeometricProcessor.GaSpaceDimensions;
    //    var colsCount = rowsCount;
    //    var array = new double[rowsCount, colsCount];

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

    //public static ILinMatrixStorage GetMultivectorsMappingArray(this IOutermorphism linearMap, int rowsCount, int colsCount)
    //{
    //    var processor = linearMap.LinearProcessor;
    //    var array = new double[rowsCount, colsCount];

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

    public static double GetEuclideanDeterminant(this IRGaFloat64Outermorphism om, int vSpaceDimensions)
    {
        var mappedPseudoScalar =
            om.OmMapBasisBlade(om.Processor.GetBasisPseudoScalarId(vSpaceDimensions));

        return mappedPseudoScalar.ESp(
            om.Processor.PseudoScalarEInverse(vSpaceDimensions)
        ).ScalarValue;
    }

    public static double GetDeterminant(this IRGaFloat64Outermorphism om, int vSpaceDimensions)
    {
        return om
            .OmMapBasisBlade(om.Processor.GetBasisPseudoScalarId(vSpaceDimensions))
            .Sp(om.Processor.PseudoScalarInverse(vSpaceDimensions)).Scalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IRGaFloat64Outermorphism CreateComputedOutermorphism(this RGaFloat64VectorFrame frame)
    {
        return frame
            .Select(v => v.ToLinVector())
            .ToLinUnilinearMap()
            .ToOutermorphism(frame.Processor);
    }
}
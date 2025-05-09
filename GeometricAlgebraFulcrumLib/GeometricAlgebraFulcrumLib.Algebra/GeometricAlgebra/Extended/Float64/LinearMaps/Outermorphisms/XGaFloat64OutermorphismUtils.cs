using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;

public static class XGaFloat64OutermorphismUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector OmMap(this IXGaFloat64Outermorphism outermorphism, LinSignedBasisVector axis)
    {
        return axis.IsPositive
            ? outermorphism.OmMapBasisVector(axis.Index)
            : -outermorphism.OmMapBasisVector(axis.Index);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrame OmMap(this IXGaFloat64Outermorphism outermorphism, XGaFloat64VectorFrame frame)
    {
        return XGaFloat64VectorFrame.Create(
            frame.FrameSpecs,
            frame.Select(outermorphism.OmMap)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector OmMapPseudoScalar(this IXGaFloat64Outermorphism outermorphism, int vSpaceDimensions)
    {
        return outermorphism.OmMapBasisBlade(
            outermorphism.Processor.GetBasisPseudoScalarId(vSpaceDimensions)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector OmMapBasisVector(this IReadOnlyList<IXGaFloat64Outermorphism> omList, int index)
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
    public static XGaFloat64Bivector OmMapBasisBivector(this IReadOnlyList<IXGaFloat64Outermorphism> omList, int index1, int index2)
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
    public static XGaFloat64KVector OmMapBasisBlade(this IReadOnlyList<IXGaFloat64Outermorphism> omList, IndexSet id)
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
    public static XGaFloat64KVector OmMapPseudoScalar(this IReadOnlyList<IXGaFloat64Outermorphism> omList, int vSpaceDimensions)
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
    public static XGaFloat64Vector OmMap(this IEnumerable<IXGaFloat64Outermorphism> omSeq, XGaFloat64Vector vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector OmMap(this IEnumerable<IXGaFloat64Outermorphism> omSeq, XGaFloat64Bivector vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector OmMap(this IEnumerable<IXGaFloat64Outermorphism> omSeq, XGaFloat64KVector vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector OmMap(this IEnumerable<IXGaFloat64Outermorphism> omSeq, XGaFloat64Multivector vector)
    {
        return omSeq
            .Aggregate(
                vector,
                (v, om) => om.OmMap(v)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrame OmMap(this IEnumerable<IXGaFloat64Outermorphism> omSeq, XGaFloat64VectorFrame frame)
    {
        return XGaFloat64VectorFrame.Create(
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
    public static double[,] GetVectorMapArray(this IXGaFloat64Outermorphism om, int size)
    {
        return om.GetVectorMapPart(size).GetMapArray(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] GetVectorMapArray(this IXGaFloat64Outermorphism om, int rowCount, int colCount)
    {
        var size = Math.Max(rowCount, colCount);

        return om.GetVectorMapPart(size).ToArray(rowCount, colCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] GetFinalMappingArray(this XGaFloat64Processor metric, IEnumerable<IXGaFloat64Outermorphism> omSeq, int rowsCount)
    {
        return omSeq.OmMap(
            metric.CreateFreeFrameOfBasis(rowsCount)
        ).GetArray(rowsCount);
    }


    public static IEnumerable<XGaFloat64Vector> OmMapVectorSequence(this IEnumerable<IXGaFloat64Outermorphism> omSeq, XGaFloat64Vector vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<XGaFloat64Bivector> OmMapBivectorSequence(this IEnumerable<IXGaFloat64Outermorphism> omSeq, XGaFloat64Bivector vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<XGaFloat64KVector> OmMapKVectorSequence(this IEnumerable<IXGaFloat64Outermorphism> omSeq, XGaFloat64KVector vector)
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

    public static IEnumerable<XGaFloat64Multivector> OmMapMultivectorSequence(this IEnumerable<IXGaFloat64Outermorphism> omSeq, XGaFloat64Multivector vector)
    {
        var v = vector;

        yield return v;

        foreach (var om in omSeq)
        {
            v = om.OmMap(v);

            yield return v;
        }
    }

    public static IEnumerable<XGaFloat64VectorFrame> OmMapFreeFrameSequence(this IEnumerable<IXGaFloat64Outermorphism> omSeq, XGaFloat64VectorFrame frame)
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
    //public static ILinMatrixStorage GetVectorOmMappingMatrix(this IXGaFloat64Outermorphism linearMap, int rowsCount, int colsCount)
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

    public static double GetEuclideanDeterminant(this IXGaFloat64Outermorphism om, int vSpaceDimensions)
    {
        var mappedPseudoScalar =
            om.OmMapBasisBlade(om.Processor.GetBasisPseudoScalarId(vSpaceDimensions));

        return mappedPseudoScalar.ESp(
            om.Processor.PseudoScalarEInverse(vSpaceDimensions)
        ).ScalarValue;
    }

    public static double GetDeterminant(this IXGaFloat64Outermorphism om, int vSpaceDimensions)
    {
        return om
            .OmMapBasisBlade(om.Processor.GetBasisPseudoScalarId(vSpaceDimensions))
            .Sp(om.Processor.PseudoScalarInverse(vSpaceDimensions)).Scalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IXGaFloat64Outermorphism CreateComputedOutermorphism(this XGaFloat64VectorFrame frame)
    {
        return frame
            .Select(v => v.ToLinVector())
            .ToLinUnilinearMap()
            .ToOutermorphism(frame.Processor);
    }
        
        
    public static Matrix<double> GetOutermorphismMatrix(this Matrix<double> matrix, int grade)
    {
        if (matrix.RowCount != matrix.ColumnCount)
            throw new InvalidOperationException();

        if (grade == 0)
            return Matrix<double>.Build.DenseOfArray(new [,]{{1d}});

        if (grade == 1)
            return matrix;

        var metric = XGaFloat64Processor.Euclidean;
        var dimensions = matrix.RowCount;
        var kVectorDimension = dimensions.GetBinomialCoefficient(grade);
        var columnList = matrix.ColumnsToTuples();

        var array = new double[kVectorDimension, kVectorDimension];

        for (var index = 0UL; index < kVectorDimension; index++)
        {
            var vectorList = 
                BasisBladeUtils
                    .BasisBladeGradeIndexToId((uint) grade, index)
                    .GetSetBitPositions()
                    .Select(i => metric.Vector(columnList[i]))
                    .ToArray();

            var column = 
                vectorList
                    .Skip(1)
                    .Aggregate((XGaFloat64Multivector)vectorList[0], (a, b) => a.Op(b))
                    .BasisScalarPairs;

            foreach (var (i, s) in column)
            {
                var j = i.Id.ToUInt64().BasisBivectorIdToIndex();

                array[j, index] = s;
            }
        }

        return Matrix<double>.Build.DenseOfArray(array);
    }

        
    public static XGaFloat64LinearMapOutermorphism CreateClarkeRotationMap(this XGaFloat64Processor processor, int vectorsCount)
    {
        var clarkeMapArray =
            Float64ArrayUtils.CreateClarkeRotationArray(vectorsCount);

        var basisVectorImagesDictionary = 
            new Dictionary<int, LinFloat64Vector>();

        for (var i = 0; i < vectorsCount; i++)
            basisVectorImagesDictionary.Add(
                i, 
                clarkeMapArray.ColumnToLinVector(i)
            );

        return basisVectorImagesDictionary
            .ToLinUnilinearMap()
            .ToOutermorphism(processor);
    }

}
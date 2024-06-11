using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D;

public static class LinFloat64UnilinearMapComposerUtils
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMapComposer CreateLinUnilinearMapComposer()
    //{
    //    return new LinFloat64UnilinearMapComposer();
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap ToDiagonalLinUnilinearMap(this IEnumerable<double> diagonalVector)
    //{
    //    var indexVectorDictionary = new Dictionary<int, Float64Tuple4D>();

    //    var index = 0;
    //    foreach (var scalar in diagonalVector)
    //    {
    //        if (!scalar.IsZero())
    //            indexVectorDictionary.Add(
    //                index,
    //                index.ToTuple4D(scalar)
    //            );

    //        index++;
    //    }

    //    return indexVectorDictionary.ToLinUnilinearMap();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap ToDiagonalLinUnilinearMap(this IReadOnlyDictionary<int, double> diagonalVector)
    //{
    //    var indexVectorDictionary =
    //        diagonalVector
    //            .Where(p => !p.Value.IsZero())
    //            .ToDictionary(
    //                p => p.Key,
    //                p => p.Key.ToTuple4D(p.Value)
    //            );

    //    return indexVectorDictionary.ToLinUnilinearMap();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap ToDiagonalLinUnilinearMap(this Float64Tuple4D diagonalVector)
    //{
    //    var indexVectorDictionary =
    //        diagonalVector
    //            .ToDictionary(
    //                p => p.Key,
    //                p => p.Key.ToTuple4D(p.Value)
    //            );

    //    return indexVectorDictionary.ToLinUnilinearMap();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap ToLinUnilinearMap(this double[,] array)
    //{
    //    return array
    //        .ColumnsToLinVectors()
    //        .ToLinUnilinearMap();
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap CreateDiagonalLinUnilinearMap(this IEnumerable<double> diagonalVector)
    //{
    //    var indexVectorDictionary = new Dictionary<int, Float64Tuple4D>();

    //    var index = 0;
    //    foreach (var scalar in diagonalVector)
    //    {
    //        if (!scalar.IsZero())
    //            indexVectorDictionary.Add(
    //                index,
    //                index.ToTuple4D(scalar)
    //            );

    //        index++;
    //    }

    //    return indexVectorDictionary.ToLinUnilinearMap();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap CreateDiagonalLinUnilinearMap(this IReadOnlyDictionary<int, double> diagonalVector)
    //{
    //    var indexVectorDictionary =
    //        diagonalVector
    //            .Where(p => !p.Value.IsZero())
    //            .ToDictionary(
    //                p => p.Key,
    //                p => p.Key.ToTuple4D(p.Value)
    //            );

    //    return indexVectorDictionary.ToLinUnilinearMap();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap CreateIdentityLinUnilinearMap(this int vSpaceDimensions)
    //{
    //    return new LinFloat64UnilinearMap(
    //        vSpaceDimensions
    //            .GetRange()
    //            .ToDictionary(
    //                i => i,
    //                i => i.ToTuple4D()
    //            )
    //    );
    //}

    //public static LinFloat64UnilinearMap ToLinUnilinearMap(this IEnumerable<Float64Tuple4D> vectorList)
    //{
    //    var indexVectorDictionary = new Dictionary<int, Float64Tuple4D>();

    //    var i = 0;
    //    foreach (var vector in vectorList)
    //    {
    //        if (!vector.IsZero)
    //            indexVectorDictionary.Add(i, vector);

    //        i++;
    //    }

    //    if (indexVectorDictionary.Count == 0)
    //        return new LinFloat64UnilinearMap(
    //            new EmptyDictionary<int, Float64Tuple4D>()
    //        );

    //    if (indexVectorDictionary.Count == 1)
    //        return new LinFloat64UnilinearMap(
    //            new SingleItemDictionary<int, Float64Tuple4D>(indexVectorDictionary.First())
    //        );

    //    return new LinFloat64UnilinearMap(
    //        indexVectorDictionary
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap ToLinUnilinearMap(this IReadOnlyDictionary<int, Float64Tuple4D> indexVectorDictionary)
    //{
    //    if (indexVectorDictionary.Count == 0 && indexVectorDictionary is not EmptyDictionary<int, Float64Tuple4D>)
    //        return new LinFloat64UnilinearMap(
    //            new EmptyDictionary<int, Float64Tuple4D>()
    //        );

    //    if (indexVectorDictionary.Count == 1 && indexVectorDictionary is not SingleItemDictionary<int, Float64Tuple4D>)
    //        return new LinFloat64UnilinearMap(
    //            new SingleItemDictionary<int, Float64Tuple4D>(
    //                indexVectorDictionary.First()
    //            )
    //        );

    //    return new LinFloat64UnilinearMap(
    //        indexVectorDictionary
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap CreateLinUnilinearMap(this int vSpaceDimensions, Func<int, Float64Tuple4D> indexVectorMapping)
    //{
    //    var indexVectorDictionary = vSpaceDimensions
    //        .GetRange()
    //        .ToDictionary(i => i, indexVectorMapping);

    //    return new LinFloat64UnilinearMap(
    //        indexVectorDictionary
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap CreateLinUnilinearMap(this int vSpaceDimensions, Func<Float64Tuple4D, Float64Tuple4D> indexVectorMapping)
    //{
    //    var indexVectorDictionary = vSpaceDimensions
    //        .GetRange()
    //        .ToDictionary(
    //            i => i,
    //            i => indexVectorMapping(
    //                i.ToTuple4D()
    //            )
    //        );

    //    return new LinFloat64UnilinearMap(
    //        indexVectorDictionary
    //    );
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static KeyValuePair<IndexPair, double> Transpose(this KeyValuePair<IndexPair, double> pair)
    //{
    //    return new KeyValuePair<IndexPair, double>(
    //        pair.Key.Transpose(),
    //        pair.Value
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinFloat64Vector4D> MapVectors(this ILinFloat64UnilinearMap4D map, params ILinFloat64Vector4D[] vectorList)
    {
        return vectorList.Select(map.MapVector).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector4D> MapVectors(this ILinFloat64UnilinearMap4D map, IEnumerable<ILinFloat64Vector4D> vectorList)
    {
        return vectorList.Select(map.MapVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinFloat64DirectionalScalingLinearMap4D CreateDirectionalScaling(this LinUnitBasisVector4D scalingBasisVector, double scalingFactor)
    {
        if (scalingFactor.IsZero())
            throw new ArgumentException(nameof(scalingFactor));

        // An identity map
        if (scalingFactor.IsNearOne())
            return LinFloat64IdentityLinearMap4D.Instance;

        // A hyper plane reflection using a normal basis vector
        if (scalingFactor.IsNearMinusOne())
            return LinFloat64HyperPlaneAxisReflection4D.Create(scalingBasisVector.GetIndex());

        // A general directional scaling using a basis vector
        return LinFloat64AxisDirectionalScaling4D.Create(scalingFactor, scalingBasisVector);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinFloat64DirectionalScalingLinearMap4D CreateDirectionalScaling(this Tuple<double, IReadOnlyList<double>> scalingFactorVectorTuple)
    //{
    //    var (scalingFactor, scalingVector) =
    //        scalingFactorVectorTuple;

    //    return scalingVector.CreateDirectionalScaling4D(scalingFactor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinFloat64DirectionalScalingLinearMap4D CreateDirectionalScaling(this IReadOnlyDictionary<int, double> scalingVector, double scalingFactor)
    //{
    //    if (scalingFactor.IsZero())
    //        throw new ArgumentException(nameof(scalingFactor));

    //    var dimensions = scalingVector.Count;

    //    // An identity map
    //    if (scalingFactor.IsNearOne())
    //        return LinFloat64IdentityLinearMap4D.Instance;

    //    // Find if the given scaling vector is parallel to a basis vector
    //    var (scalingVectorIsAxis, _, scalingAxis) =
    //        scalingVector.TryVectorToAxis();

    //    // A hyper plane reflection
    //    if (scalingFactor.IsNearMinusOne())
    //        return scalingVectorIsAxis
    //            ? LinFloat64HyperPlaneAxisReflection4D.Create(scalingAxis.Index
    //            )
    //            : LinFloat64HyperPlaneNormalReflection4D.Create(
    //                scalingVector
    //            );

    //    // A general directional scaling
    //    return scalingVectorIsAxis
    //        ? LinFloat64AxisDirectionalScaling.Create(
    //            dimensions,
    //            scalingFactor,
    //            scalingAxis.Index
    //        )
    //        : LinFloat64VectorDirectionalScaling.Create(
    //            scalingFactor,
    //            scalingVector
    //        );
    //}

    public static ILinFloat64DirectionalScalingLinearMap4D CreateDirectionalScaling4D(this ILinFloat64Vector4D scalingVector, double scalingFactor)
    {
        if (scalingFactor.IsZero())
            throw new ArgumentException(nameof(scalingFactor));

        //var dimensions = scalingVector.Count;

        // An identity map
        if (scalingFactor.IsNearOne())
            return LinFloat64IdentityLinearMap4D.Instance;

        // Find if the given scaling vector is parallel to a basis vector
        var (scalingVectorIsAxis, _, scalingAxis) =
            scalingVector.TryVectorToAxis();

        // A hyper plane reflection
        if (scalingFactor.IsNearMinusOne())
            return scalingVectorIsAxis
                ? LinFloat64HyperPlaneAxisReflection4D.Create(scalingAxis.GetIndex())
                : LinFloat64HyperPlaneNormalReflection4D.Create(scalingVector);

        // A general directional scaling
        return scalingVectorIsAxis
            ? LinFloat64AxisDirectionalScaling4D.Create(
                scalingFactor,
                scalingAxis.GetIndex()
            )
            : LinFloat64VectorDirectionalScaling4D.Create(
                scalingFactor,
                scalingVector
            );
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinFloat64SimpleVectorToVectorRotation4D CreateSimpleVectorToVectorRotation(this IReadOnlyDictionary<int, double> sourceVector, Float64Tuple4D targetVector)
    //{
    //    var dimensions = sourceVector.Count;

    //    // Find if the given source vector is parallel to a basis vector
    //    var (sourceVectorIsAxis, _, sourceAxis) =
    //        sourceVector.TryVectorToAxis();

    //    // Find if the given target vector is parallel to a basis vector
    //    var (targetVectorIsAxis, _, targetAxis) =
    //        targetVector.TryVectorToAxis();

    //    if (sourceVectorIsAxis)
    //    {
    //        if (targetVectorIsAxis)
    //        {
    //            if (sourceAxis.IsSame(targetAxis))
    //                return LinFloat64IdentityLinearMap4D.Instance;

    //            return new LinFloat64AxisToAxisRotation4D(
    //                sourceAxis.Index,
    //                sourceAxis.IsNegative,
    //                targetAxis.Index,
    //                targetAxis.IsNegative
    //            );
    //        }

    //        return new LinFloat64AxisToVectorRotation4D(
    //            sourceAxis.Index,
    //            sourceAxis.IsNegative,
    //            targetVector
    //        );
    //    }
    //    else
    //    {
    //        if (targetVectorIsAxis)
    //        {
    //            return LinFloat64VectorToAxisRotation4D.Create(
    //                sourceVector,
    //                targetAxis
    //            );
    //        }
    //    }

    //    if (sourceVector.IsVectorNearOrthogonalTo(targetVector))
    //        return new LinFloat64OrthogonalVectorToVectorRotation4D(
    //            sourceVector,
    //            targetVector
    //        );

    //    if (sourceVector.IsVectorNearParallelTo(targetVector))
    //        return LinFloat64IdentityLinearMap4D.Instance;

    //    return LinFloat64VectorToVectorRotation4D.Create(
    //        sourceVector,
    //        targetVector
    //    );
    //}

    public static ILinFloat64SimpleVectorToVectorRotation4D CreateSimpleVectorToVectorRotation(this ILinFloat64Vector4D sourceVector, ILinFloat64Vector4D targetVector)
    {
        //var dimensions = sourceVector.Count;

        // Find if the given source vector is parallel to a basis vector
        var (sourceVectorIsAxis, _, sourceAxis) =
            sourceVector.TryVectorToAxis();

        // Find if the given target vector is parallel to a basis vector
        var (targetVectorIsAxis, _, targetAxis) =
            targetVector.TryVectorToAxis();

        if (sourceVectorIsAxis)
        {
            if (targetVectorIsAxis)
            {
                if (sourceAxis == targetAxis)
                    return LinFloat64IdentityLinearMap4D.Instance;

                return new LinFloat64AxisToAxisRotation4D(
                    sourceAxis.GetIndex(),
                    sourceAxis.IsNegative(),
                    targetAxis.GetIndex(),
                    targetAxis.IsNegative()
                );
            }

            return new LinFloat64AxisToVectorRotation4D(
                sourceAxis.GetIndex(),
                sourceAxis.IsNegative(),
                targetVector
            );
        }
        else
        {
            if (targetVectorIsAxis)
            {
                return LinFloat64VectorToAxisRotation4D.Create(
                    sourceVector,
                    targetAxis
                );
            }
        }

        if (sourceVector.IsNearOrthogonalTo(targetVector))
            return new LinFloat64OrthogonalVectorToVectorRotation4D(
                sourceVector,
                targetVector
            );

        if (sourceVector.IsNearParallelTo(targetVector))
            return LinFloat64IdentityLinearMap4D.Instance;

        return LinFloat64VectorToVectorRotation4D.Create(
            sourceVector,
            targetVector
        );
    }
}
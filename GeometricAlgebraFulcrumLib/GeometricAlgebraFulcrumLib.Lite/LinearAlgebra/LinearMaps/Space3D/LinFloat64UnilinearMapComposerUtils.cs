﻿using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Rotation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space3D;

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
    //    var indexVectorDictionary = new Dictionary<int, Float64Tuple3D>();

    //    var index = 0;
    //    foreach (var scalar in diagonalVector)
    //    {
    //        if (!scalar.IsZero())
    //            indexVectorDictionary.Add(
    //                index,
    //                index.ToTuple3D(scalar)
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
    //                p => p.Key.ToTuple3D(p.Value)
    //            );

    //    return indexVectorDictionary.ToLinUnilinearMap();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap ToDiagonalLinUnilinearMap(this Float64Tuple3D diagonalVector)
    //{
    //    var indexVectorDictionary =
    //        diagonalVector
    //            .ToDictionary(
    //                p => p.Key,
    //                p => p.Key.ToTuple3D(p.Value)
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
    //    var indexVectorDictionary = new Dictionary<int, Float64Tuple3D>();

    //    var index = 0;
    //    foreach (var scalar in diagonalVector)
    //    {
    //        if (!scalar.IsZero())
    //            indexVectorDictionary.Add(
    //                index,
    //                index.ToTuple3D(scalar)
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
    //                p => p.Key.ToTuple3D(p.Value)
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
    //                i => i.ToTuple3D()
    //            )
    //    );
    //}

    //public static LinFloat64UnilinearMap ToLinUnilinearMap(this IEnumerable<Float64Tuple3D> vectorList)
    //{
    //    var indexVectorDictionary = new Dictionary<int, Float64Tuple3D>();

    //    var i = 0;
    //    foreach (var vector in vectorList)
    //    {
    //        if (!vector.IsZero)
    //            indexVectorDictionary.Add(i, vector);

    //        i++;
    //    }

    //    if (indexVectorDictionary.Count == 0)
    //        return new LinFloat64UnilinearMap(
    //            new EmptyDictionary<int, Float64Tuple3D>()
    //        );

    //    if (indexVectorDictionary.Count == 1)
    //        return new LinFloat64UnilinearMap(
    //            new SingleItemDictionary<int, Float64Tuple3D>(indexVectorDictionary.First())
    //        );

    //    return new LinFloat64UnilinearMap(
    //        indexVectorDictionary
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap ToLinUnilinearMap(this IReadOnlyDictionary<int, Float64Tuple3D> indexVectorDictionary)
    //{
    //    if (indexVectorDictionary.Count == 0 && indexVectorDictionary is not EmptyDictionary<int, Float64Tuple3D>)
    //        return new LinFloat64UnilinearMap(
    //            new EmptyDictionary<int, Float64Tuple3D>()
    //        );

    //    if (indexVectorDictionary.Count == 1 && indexVectorDictionary is not SingleItemDictionary<int, Float64Tuple3D>)
    //        return new LinFloat64UnilinearMap(
    //            new SingleItemDictionary<int, Float64Tuple3D>(
    //                indexVectorDictionary.First()
    //            )
    //        );

    //    return new LinFloat64UnilinearMap(
    //        indexVectorDictionary
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap CreateLinUnilinearMap(this int vSpaceDimensions, Func<int, Float64Tuple3D> indexVectorMapping)
    //{
    //    var indexVectorDictionary = vSpaceDimensions
    //        .GetRange()
    //        .ToDictionary(i => i, indexVectorMapping);

    //    return new LinFloat64UnilinearMap(
    //        indexVectorDictionary
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64UnilinearMap CreateLinUnilinearMap(this int vSpaceDimensions, Func<Float64Tuple3D, Float64Tuple3D> indexVectorMapping)
    //{
    //    var indexVectorDictionary = vSpaceDimensions
    //        .GetRange()
    //        .ToDictionary(
    //            i => i,
    //            i => indexVectorMapping(
    //                i.ToTuple3D()
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
    public static IReadOnlyList<Float64Vector3D> MapVectors(this ILinFloat64UnilinearMap3D map, params Float64Vector3D[] vectorList)
    {
        return vectorList.Select(map.MapVector).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Vector3D> MapVectors(this ILinFloat64UnilinearMap3D map, IEnumerable<Float64Vector3D> vectorList)
    {
        return vectorList.Select(map.MapVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinFloat64DirectionalScalingLinearMap3D CreateDirectionalScaling(this LinUnitBasisVector3D scalingBasisVector, double scalingFactor)
    {
        if (scalingFactor.IsZero())
            throw new ArgumentException(nameof(scalingFactor));

        // An identity map
        if (scalingFactor.IsNearOne())
            return LinFloat64IdentityLinearMap3D.Instance;

        // A hyper plane reflection using a normal basis vector
        if (scalingFactor.IsNearMinusOne())
            return LinFloat64HyperPlaneAxisReflection3D.Create(scalingBasisVector.GetIndex());

        // A general directional scaling using a basis vector
        return LinFloat64AxisDirectionalScaling3D.Create(scalingFactor, scalingBasisVector);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinFloat64DirectionalScalingLinearMap3D CreateDirectionalScaling(this Tuple<double, IReadOnlyList<double>> scalingFactorVectorTuple)
    //{
    //    var (scalingFactor, scalingVector) =
    //        scalingFactorVectorTuple;

    //    return scalingVector.CreateDirectionalScaling3D(scalingFactor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinFloat64DirectionalScalingLinearMap3D CreateDirectionalScaling(this IReadOnlyDictionary<int, double> scalingVector, double scalingFactor)
    //{
    //    if (scalingFactor.IsZero())
    //        throw new ArgumentException(nameof(scalingFactor));

    //    var dimensions = scalingVector.Count;

    //    // An identity map
    //    if (scalingFactor.IsNearOne())
    //        return LinFloat64IdentityLinearMap3D.Instance;

    //    // Find if the given scaling vector is parallel to a basis vector
    //    var (scalingVectorIsAxis, _, scalingAxis) =
    //        scalingVector.TryVectorToAxis();

    //    // A hyper plane reflection
    //    if (scalingFactor.IsNearMinusOne())
    //        return scalingVectorIsAxis
    //            ? LinFloat64HyperPlaneAxisReflection3D.Create(scalingAxis.Index
    //            )
    //            : LinFloat64HyperPlaneNormalReflection3D.Create(
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

    public static ILinFloat64DirectionalScalingLinearMap3D CreateDirectionalScaling3D(this IFloat64Vector3D scalingVector, double scalingFactor)
    {
        if (scalingFactor.IsZero())
            throw new ArgumentException(nameof(scalingFactor));

        //var dimensions = scalingVector.Count;

        // An identity map
        if (scalingFactor.IsNearOne())
            return LinFloat64IdentityLinearMap3D.Instance;

        // Find if the given scaling vector is parallel to a basis vector
        var (scalingVectorIsAxis, _, scalingAxis) =
            scalingVector.TryVectorToAxis();

        // A hyper plane reflection
        if (scalingFactor.IsNearMinusOne())
            return scalingVectorIsAxis
                ? LinFloat64HyperPlaneAxisReflection3D.Create(scalingAxis.GetIndex())
                : LinFloat64HyperPlaneNormalReflection3D.Create(scalingVector);

        // A general directional scaling
        return scalingVectorIsAxis
            ? LinFloat64AxisDirectionalScaling3D.Create(
                scalingFactor,
                scalingAxis.GetIndex()
            )
            : LinFloat64VectorDirectionalScaling3D.Create(
                scalingFactor,
                scalingVector
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Rotation3D CreateRotationToVector3D(this IFloat64Vector3D vector, IFloat64Vector3D rotatedVector, bool useShortArc = true)
    {
        if (vector.Subtract(rotatedVector).IsZeroVector())
            return LinFloat64IdentityLinearMap3D.Instance;

        return LinFloat64PlanarRotation3D.CreateFromRotatedVector(vector, rotatedVector, useShortArc);
    }
}
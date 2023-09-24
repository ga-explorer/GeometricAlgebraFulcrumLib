﻿using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;

public static class LinFloat64UnilinearMapComposerUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorDirectionalScaling GetVectorDirectionalScaling(this System.Random random, int dimensions, double minValue, double maxValue)
    {
        return LinFloat64VectorDirectionalScaling.Create(
            random.GetNumber(minValue, maxValue),
            random.GetFloat64Tuple(dimensions).CreateUnitLinVector()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflection GetHyperPlaneNormalReflection(this System.Random random, int dimensions)
    {
        return LinFloat64HyperPlaneNormalReflection.Create(
            random.GetFloat64Tuple(dimensions).CreateUnitLinVector()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation GetVectorToVectorRotation(this System.Random random, int dimensions)
    {
        var u = random.GetFloat64Tuple(dimensions).CreateUnitLinVector();
        var v = random.GetFloat64Tuple(dimensions).CreateUnitLinVector();

        return LinFloat64VectorToVectorRotation.CreateFromRotatedVector(u, v);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneNormalReflection GetHyperPlaneReflection(this System.Random random, int dimensions)
    {
        var u = random.GetFloat64Tuple(dimensions).CreateLinVector();

        return LinFloat64HyperPlaneNormalReflection.Create(u);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMapComposer CreateLinUnilinearMapComposer()
    {
        return new LinFloat64UnilinearMapComposer();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap ToDiagonalLinUnilinearMap(this IEnumerable<double> diagonalVector)
    {
        var indexVectorDictionary = new Dictionary<int, Float64Vector>();

        var index = 0;
        foreach (var scalar in diagonalVector)
        {
            if (!scalar.IsZero())
                indexVectorDictionary.Add(
                    index,
                    index.CreateLinVector(scalar)
                );

            index++;
        }

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap ToDiagonalLinUnilinearMap(this IReadOnlyDictionary<int, double> diagonalVector)
    {
        var indexVectorDictionary =
            diagonalVector
                .Where(p => !p.Value.IsZero())
                .ToDictionary(
                    p => p.Key,
                    p => p.Key.CreateLinVector(p.Value)
                );

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap ToDiagonalLinUnilinearMap(this Float64Vector diagonalVector)
    {
        var indexVectorDictionary =
            diagonalVector
                .ToDictionary(
                    p => p.Key,
                    p => p.Key.CreateLinVector(p.Value)
                );

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap ToLinUnilinearMap(this double[,] array)
    {
        return array
            .ColumnsToLinVectors()
            .ToLinUnilinearMap();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap CreateDiagonalLinUnilinearMap(this IEnumerable<double> diagonalVector)
    {
        var indexVectorDictionary = new Dictionary<int, Float64Vector>();

        var index = 0;
        foreach (var scalar in diagonalVector)
        {
            if (!scalar.IsZero())
                indexVectorDictionary.Add(
                    index,
                    index.CreateLinVector(scalar)
                );

            index++;
        }

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap CreateDiagonalLinUnilinearMap(this IReadOnlyDictionary<int, double> diagonalVector)
    {
        var indexVectorDictionary =
            diagonalVector
                .Where(p => !p.Value.IsZero())
                .ToDictionary(
                    p => p.Key,
                    p => p.Key.CreateLinVector(p.Value)
                );

        return indexVectorDictionary.ToLinUnilinearMap();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap CreateIdentityLinUnilinearMap(this int vSpaceDimensions)
    {
        return new LinFloat64UnilinearMap(
            vSpaceDimensions
                .GetRange()
                .ToDictionary(
                    i => i,
                    i => i.CreateLinVector()
                )
        );
    }

    public static LinFloat64UnilinearMap ToLinUnilinearMap(this IEnumerable<Float64Vector> vectorList)
    {
        var indexVectorDictionary = new Dictionary<int, Float64Vector>();

        var i = 0;
        foreach (var vector in vectorList)
        {
            if (!vector.IsZero)
                indexVectorDictionary.Add(i, vector);

            i++;
        }

        if (indexVectorDictionary.Count == 0)
            return new LinFloat64UnilinearMap(
                new EmptyDictionary<int, Float64Vector>()
            );

        if (indexVectorDictionary.Count == 1)
            return new LinFloat64UnilinearMap(
                new SingleItemDictionary<int, Float64Vector>(indexVectorDictionary.First())
            );

        return new LinFloat64UnilinearMap(
            indexVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap ToLinUnilinearMap(this IReadOnlyDictionary<int, Float64Vector> indexVectorDictionary)
    {
        if (indexVectorDictionary.Count == 0 && indexVectorDictionary is not EmptyDictionary<int, Float64Vector>)
            return new LinFloat64UnilinearMap(
                new EmptyDictionary<int, Float64Vector>()
            );

        if (indexVectorDictionary.Count == 1 && indexVectorDictionary is not SingleItemDictionary<int, Float64Vector>)
            return new LinFloat64UnilinearMap(
                new SingleItemDictionary<int, Float64Vector>(
                    indexVectorDictionary.First()
                )
            );

        return new LinFloat64UnilinearMap(
            indexVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap CreateLinUnilinearMap(this int vSpaceDimensions, Func<int, Float64Vector> indexVectorMapping)
    {
        var indexVectorDictionary = vSpaceDimensions
            .GetRange()
            .ToDictionary(i => i, indexVectorMapping);

        return new LinFloat64UnilinearMap(
            indexVectorDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap CreateLinUnilinearMap(this int vSpaceDimensions, Func<Float64Vector, Float64Vector> indexVectorMapping)
    {
        var indexVectorDictionary = vSpaceDimensions
            .GetRange()
            .ToDictionary(
                i => i,
                i => indexVectorMapping(
                    i.CreateLinVector()
                )
            );

        return new LinFloat64UnilinearMap(
            indexVectorDictionary
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KeyValuePair<IndexPair, double> Transpose(this KeyValuePair<IndexPair, double> pair)
    {
        return new KeyValuePair<IndexPair, double>(
            pair.Key.Transpose(),
            pair.Value
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<Float64Vector> MapVectors(this ILinFloat64UnilinearMap map, params Float64Vector[] vectorList)
    {
        return vectorList.Select(map.MapVector).ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<Float64Vector> MapVectors(this ILinFloat64UnilinearMap map, IEnumerable<Float64Vector> vectorList)
    {
        return vectorList.Select(map.MapVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinFloat64DirectionalScalingLinearMap CreateDirectionalScaling(this LinSignedBasisVector scalingBasisVector, int dimensions, double scalingFactor)
    {
        if (scalingFactor.IsZero())
            throw new ArgumentException(nameof(scalingFactor));

        // An identity map
        if (scalingFactor.IsNearOne())
            return LinFloat64IdentityLinearMap.Create(dimensions);

        // A hyper plane reflection using a normal basis vector
        if (scalingFactor.IsNearMinusOne())
            return LinFloat64HyperPlaneAxisReflection.Create(dimensions, scalingBasisVector);

        // A general directional scaling using a basis vector
        return LinFloat64AxisDirectionalScaling.Create(scalingFactor, dimensions, scalingBasisVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinFloat64DirectionalScalingLinearMap CreateDirectionalScaling(this Tuple<double, IReadOnlyList<double>> scalingFactorVectorTuple)
    {
        var (scalingFactor, scalingVector) =
            scalingFactorVectorTuple;

        return scalingVector.CreateDirectionalScaling(scalingFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinFloat64DirectionalScalingLinearMap CreateDirectionalScaling(this IReadOnlyDictionary<int, double> scalingVector, double scalingFactor)
    {
        if (scalingFactor.IsZero())
            throw new ArgumentException(nameof(scalingFactor));

        var dimensions = scalingVector.Count;

        // An identity map
        if (scalingFactor.IsNearOne())
            return LinFloat64IdentityLinearMap.Create(dimensions);

        // Find if the given scaling vector is parallel to a basis vector
        var (scalingVectorIsAxis, _, scalingAxis) =
            scalingVector.TryVectorToAxis();

        // A hyper plane reflection
        if (scalingFactor.IsNearMinusOne())
            return scalingVectorIsAxis
                ? LinFloat64HyperPlaneAxisReflection.Create(
                    dimensions,
                    scalingAxis.Index
                )
                : LinFloat64HyperPlaneNormalReflection.Create(
                    scalingVector.CreateUnitLinVector()
                );

        // A general directional scaling
        return scalingVectorIsAxis
            ? LinFloat64AxisDirectionalScaling.Create(
                dimensions,
                scalingFactor,
                scalingAxis.Index
            )
            : LinFloat64VectorDirectionalScaling.Create(
                scalingFactor,
                scalingVector.CreateUnitLinVector()
            );
    }

    public static ILinFloat64DirectionalScalingLinearMap CreateDirectionalScaling(this IReadOnlyList<double> scalingVector, double scalingFactor)
    {
        if (scalingFactor.IsZero())
            throw new ArgumentException(nameof(scalingFactor));

        var dimensions = scalingVector.Count;

        // An identity map
        if (scalingFactor.IsNearOne())
            return LinFloat64IdentityLinearMap.Create(dimensions);

        // Find if the given scaling vector is parallel to a basis vector
        var (scalingVectorIsAxis, _, scalingAxis) =
            scalingVector.TryVectorToAxis();

        // A hyper plane reflection
        if (scalingFactor.IsNearMinusOne())
            return scalingVectorIsAxis
                ? LinFloat64HyperPlaneAxisReflection.Create(
                    dimensions,
                    scalingAxis.Index
                )
                : LinFloat64HyperPlaneNormalReflection.Create(
                    scalingVector.CreateUnitLinVector()
                );

        // A general directional scaling
        return scalingVectorIsAxis
            ? LinFloat64AxisDirectionalScaling.Create(
                dimensions,
                scalingFactor,
                scalingAxis.Index
            )
            : LinFloat64VectorDirectionalScaling.Create(
                scalingFactor,
                scalingVector.CreateUnitLinVector()
            );
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinFloat64PlanarRotation CreateSimpleVectorToVectorRotation(this IReadOnlyDictionary<int, double> sourceVector, LinFloat64Vector targetVector)
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
    //                return LinFloat64IdentityLinearMap.Create(dimensions);

    //            return new LinFloat64AxisToAxisRotation(
    //                sourceAxis.Index,
    //                sourceAxis.IsNegative,
    //                targetAxis.Index,
    //                targetAxis.IsNegative
    //            );
    //        }

    //        return new LinFloat64AxisToVectorRotation(
    //            sourceAxis.Index,
    //            sourceAxis.IsNegative,
    //            targetVector.CreateUnitLinVector()
    //        );
    //    }
    //    else
    //    {
    //        if (targetVectorIsAxis)
    //        {
    //            return LinFloat64VectorToAxisRotation.Create(
    //                sourceVector.CreateUnitLinVector(),
    //                targetAxis
    //            );
    //        }
    //    }

    //    if (sourceVector.IsVectorNearOrthogonalTo(targetVector))
    //        return new LinFloat64OrthogonalVectorToVectorRotation(
    //            sourceVector.CreateUnitLinVector(),
    //            targetVector.CreateUnitLinVector()
    //        );

    //    if (sourceVector.IsVectorNearParallelTo(targetVector))
    //        return LinFloat64IdentityLinearMap.Create(dimensions);

    //    return LinFloat64VectorToVectorRotation.CreateFromRotatedVector(
    //        sourceVector.CreateUnitLinVector(),
    //        targetVector.CreateUnitLinVector()
    //    );
    //}

    //public static ILinFloat64PlanarRotation CreateSimpleVectorToVectorRotation(this IReadOnlyList<double> sourceVector, IReadOnlyList<double> targetVector)
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
    //                return LinFloat64IdentityLinearMap.Create(dimensions);

    //            return new LinFloat64AxisToAxisRotation(
    //                sourceAxis.Index,
    //                sourceAxis.IsNegative,
    //                targetAxis.Index,
    //                targetAxis.IsNegative
    //            );
    //        }

    //        return new LinFloat64AxisToVectorRotation(
    //            sourceAxis.Index,
    //            sourceAxis.IsNegative,
    //            targetVector.CreateUnitLinVector()
    //        );
    //    }
    //    else
    //    {
    //        if (targetVectorIsAxis)
    //        {
    //            return LinFloat64VectorToAxisRotation.Create(
    //                sourceVector.CreateUnitLinVector(),
    //                targetAxis
    //            );
    //        }
    //    }

    //    if (sourceVector.IsVectorNearOrthogonalTo(targetVector))
    //        return new LinFloat64OrthogonalVectorToVectorRotation(
    //            sourceVector.CreateUnitLinVector(),
    //            targetVector.CreateUnitLinVector()
    //        );

    //    if (sourceVector.IsVectorNearParallelTo(targetVector))
    //        return LinFloat64IdentityLinearMap.Create(dimensions);

    //    return LinFloat64VectorToVectorRotation.CreateFromRotatedVector(
    //        sourceVector.CreateUnitLinVector(),
    //        targetVector.CreateUnitLinVector()
    //    );
    //}
}
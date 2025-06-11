using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Random;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;

public static class LinFloat64UnilinearMapComposerUtils
{

    
    public static LinFloat64VectorDirectionalScaling GetVectorDirectionalScaling(this Random random, int dimensions, double minValue, double maxValue)
    {
        return LinFloat64VectorDirectionalScaling.Create(
            random.GetFloat64(minValue, maxValue),
            random.GetLinVector(dimensions).CreateUnitLinVector()
        );
    }

    
    public static LinFloat64HyperPlaneNormalReflection GetHyperPlaneNormalReflection(this Random random, int dimensions)
    {
        return LinFloat64HyperPlaneNormalReflection.Create(
            random.GetLinVector(dimensions).CreateUnitLinVector()
        );
    }

    
    public static LinFloat64VectorToVectorRotation GetVectorToVectorRotation(this Random random, int dimensions)
    {
        var u = random.GetLinVector(dimensions).CreateUnitLinVector();
        var v = random.GetLinVector(dimensions).CreateUnitLinVector();

        return LinFloat64VectorToVectorRotation.CreateFromRotatedVector(u, v);
    }

    
    public static LinFloat64HyperPlaneNormalReflection GetHyperPlaneReflection(this Random random, int dimensions)
    {
        var u = random.GetLinVector(dimensions).CreateLinVector();

        return LinFloat64HyperPlaneNormalReflection.Create(u);
    }

    
    public static LinFloat64UnilinearMapComposer CreateLinUnilinearMapComposer()
    {
        return new LinFloat64UnilinearMapComposer();
    }


    
    public static LinFloat64UnilinearMap ToDiagonalLinUnilinearMap(this IEnumerable<double> diagonalVector)
    {
        var indexVectorDictionary = new Dictionary<int, LinFloat64Vector>();

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

    
    public static LinFloat64UnilinearMap ToLinUnilinearMap(this double[,] array)
    {
        return array
            .ColumnsToLinVectors()
            .ToLinUnilinearMap();
    }


    
    public static LinFloat64UnilinearMap CreateDiagonalLinUnilinearMap(this IEnumerable<double> diagonalVector)
    {
        var indexVectorDictionary = new Dictionary<int, LinFloat64Vector>();

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

    public static LinFloat64UnilinearMap ToLinUnilinearMap(this IEnumerable<LinFloat64Vector> vectorList)
    {
        var indexVectorDictionary = new Dictionary<int, LinFloat64Vector>();

        var i = 0;
        foreach (var vector in vectorList)
        {
            if (!vector.IsZero)
                indexVectorDictionary.Add(i, vector);

            i++;
        }

        if (indexVectorDictionary.Count == 0)
            return new LinFloat64UnilinearMap(
                new EmptyDictionary<int, LinFloat64Vector>()
            );

        if (indexVectorDictionary.Count == 1)
            return new LinFloat64UnilinearMap(
                new SingleItemDictionary<int, LinFloat64Vector>(indexVectorDictionary.First())
            );

        return new LinFloat64UnilinearMap(
            indexVectorDictionary
        );
    }

    
    public static LinFloat64UnilinearMap ToLinUnilinearMap(this IReadOnlyDictionary<int, LinFloat64Vector> indexVectorDictionary)
    {
        if (indexVectorDictionary.Count == 0 && indexVectorDictionary is not EmptyDictionary<int, LinFloat64Vector>)
            return new LinFloat64UnilinearMap(
                new EmptyDictionary<int, LinFloat64Vector>()
            );

        if (indexVectorDictionary.Count == 1 && indexVectorDictionary is not SingleItemDictionary<int, LinFloat64Vector>)
            return new LinFloat64UnilinearMap(
                new SingleItemDictionary<int, LinFloat64Vector>(
                    indexVectorDictionary.First()
                )
            );

        return new LinFloat64UnilinearMap(
            indexVectorDictionary
        );
    }

    
    public static LinFloat64UnilinearMap CreateLinUnilinearMap(this int vSpaceDimensions, Func<int, LinFloat64Vector> indexVectorMapping)
    {
        var indexVectorDictionary = vSpaceDimensions
            .GetRange()
            .ToDictionary(i => i, indexVectorMapping);

        return new LinFloat64UnilinearMap(
            indexVectorDictionary
        );
    }

    
    public static LinFloat64UnilinearMap CreateLinUnilinearMap(this int vSpaceDimensions, Func<LinFloat64Vector, LinFloat64Vector> indexVectorMapping)
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


    
    public static KeyValuePair<IndexPair, double> Transpose(this KeyValuePair<IndexPair, double> pair)
    {
        return new KeyValuePair<IndexPair, double>(
            pair.Key.Transpose(),
            pair.Value
        );
    }


    
    public static IReadOnlyList<LinFloat64Vector> MapVectors(this ILinFloat64UnilinearMap map, params LinFloat64Vector[] vectorList)
    {
        return vectorList.Select(map.MapVector).ToArray();
    }

    
    public static IEnumerable<LinFloat64Vector> MapVectors(this ILinFloat64UnilinearMap map, IEnumerable<LinFloat64Vector> vectorList)
    {
        return vectorList.Select(map.MapVector);
    }

    
    public static ILinFloat64DirectionalScalingLinearMap CreateDirectionalScaling(this Tuple<double, IReadOnlyList<double>> scalingFactorVectorTuple)
    {
        var (scalingFactor, scalingVector) =
            scalingFactorVectorTuple;

        return scalingVector.CreateDirectionalScaling(scalingFactor);
    }

    
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


    //
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
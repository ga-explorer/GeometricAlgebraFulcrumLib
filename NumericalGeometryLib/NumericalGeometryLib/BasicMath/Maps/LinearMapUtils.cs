using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Maps.SpaceND;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Scaling;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps
{
    public static class ScalingUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<Float64Tuple> MapVectors(this ILinearMap map, params Float64Tuple[] vectorList)
        {
            return vectorList.Select(map.MapVector).ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Tuple> MapVectors(this ILinearMap map, IEnumerable<Float64Tuple> vectorList)
        {
            return vectorList.Select(map.MapVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDirectionalScalingLinearMap CreateDirectionalScaling(this Axis scalingBasisVector, double scalingFactor)
        {
            if (scalingFactor.IsExactZero())
                throw new ArgumentException(nameof(scalingFactor));

            var dimensions = scalingBasisVector.Dimensions;

            // An identity map
            if (scalingFactor.IsNearOne())
                return IdentityLinearMap.Create(dimensions);

            // A hyper plane reflection using a normal basis vector
            if (scalingFactor.IsNearMinusOne())
                return HyperPlaneAxisReflection.Create(scalingBasisVector);

            // A general directional scaling using a basis vector
            return AxisDirectionalScaling.Create(scalingFactor, scalingBasisVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDirectionalScalingLinearMap CreateDirectionalScaling(this Tuple<double, double[]> scalingFactorVectorTuple)
        {
            var (scalingFactor, scalingVector) =
                scalingFactorVectorTuple;

            return scalingVector.CreateDirectionalScaling(scalingFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDirectionalScalingLinearMap CreateDirectionalScaling(this Float64Tuple scalingVector, double scalingFactor)
        {
            return scalingVector.ScalarArray.CreateDirectionalScaling(scalingFactor);
        }

        public static IDirectionalScalingLinearMap CreateDirectionalScaling(this double[] scalingVector, double scalingFactor)
        {
            if (scalingFactor.IsExactZero())
                throw new ArgumentException(nameof(scalingFactor));

            var dimensions = scalingVector.Length;

            // An identity map
            if (scalingFactor.IsNearOne())
                return IdentityLinearMap.Create(dimensions);

            // Find if the given scaling vector is parallel to a basis vector
            var (scalingVectorIsAxis, _, scalingAxis) = 
                scalingVector.TryVectorToAxis();

            // A hyper plane reflection
            if (scalingFactor.IsNearMinusOne())
                return scalingVectorIsAxis
                    ? HyperPlaneAxisReflection.Create(
                        dimensions,
                        scalingAxis.Index
                    )
                    : HyperPlaneNormalReflection.Create(
                        scalingVector.CreateTuple(true)
                    );

            // A general directional scaling
            return scalingVectorIsAxis
                ? AxisDirectionalScaling.Create(
                    dimensions,
                    scalingFactor,
                    scalingAxis.Index
                )
                : VectorDirectionalScaling.Create(
                    scalingFactor,
                    scalingVector.CreateTuple(true)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISimpleVectorToVectorRotation CreateSimpleVectorToVectorRotation(this Float64Tuple sourceVector, Float64Tuple targetVector)
        {
            return sourceVector.ScalarArray.CreateSimpleVectorToVectorRotation(targetVector.ScalarArray);
        }

        public static ISimpleVectorToVectorRotation CreateSimpleVectorToVectorRotation(this double[] sourceVector, double[] targetVector)
        {
            var dimensions = sourceVector.Length;

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
                    if (sourceAxis.IsSame(targetAxis))
                        return IdentityLinearMap.Create(dimensions);

                    return new AxisToAxisRotation(
                        dimensions,
                        sourceAxis.Index,
                        sourceAxis.IsNegative,
                        targetAxis.Index,
                        targetAxis.IsNegative
                    );
                }

                return new AxisToVectorRotation(
                    sourceAxis.Index,
                    sourceAxis.IsNegative,
                    targetVector.CreateTuple(true)
                );
            }
            else
            {
                if (targetVectorIsAxis)
                {
                    return VectorToAxisRotation.Create(
                        sourceVector.CreateTuple(true),
                        targetAxis
                    );
                }
            }

            if (sourceVector.IsVectorNearOrthogonalTo(targetVector))
                return new OrthogonalVectorToVectorRotation(
                    sourceVector.CreateTuple(true),
                    targetVector.CreateTuple(true)
                );

            if (sourceVector.IsVectorNearParallelTo(targetVector))
                return IdentityLinearMap.Create(dimensions);

            return VectorToVectorRotation.Create(
                sourceVector.CreateTuple(true),
                targetVector.CreateTuple(true)
            );
        }
    }
}

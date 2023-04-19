using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Float64;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Reflection;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Scaling;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps
{
    public static class ScalingUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<LinFloat64Vector> MapVectors(this ILinFloat64UnilinearMap map, params LinFloat64Vector[] vectorList)
        {
            return vectorList.Select(map.MapVector).ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LinFloat64Vector> MapVectors(this ILinFloat64UnilinearMap map, IEnumerable<LinFloat64Vector> vectorList)
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinFloat64SimpleVectorToVectorRotation CreateSimpleVectorToVectorRotation(this IReadOnlyDictionary<int, double> sourceVector, IReadOnlyDictionary<int, double> targetVector)
        {
            var dimensions = sourceVector.Count;

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
                        return LinFloat64IdentityLinearMap.Create(dimensions);

                    return new LinFloat64AxisToAxisRotation(
                        sourceAxis.Index,
                        sourceAxis.IsNegative,
                        targetAxis.Index,
                        targetAxis.IsNegative
                    );
                }

                return new LinFloat64AxisToVectorRotation(
                    sourceAxis.Index,
                    sourceAxis.IsNegative,
                    targetVector.CreateUnitLinVector()
                );
            }
            else
            {
                if (targetVectorIsAxis)
                {
                    return LinFloat64VectorToAxisRotation.Create(
                        sourceVector.CreateUnitLinVector(),
                        targetAxis
                    );
                }
            }

            if (sourceVector.IsVectorNearOrthogonalTo(targetVector))
                return new LinFloat64OrthogonalVectorToVectorRotation(
                    sourceVector.CreateUnitLinVector(),
                    targetVector.CreateUnitLinVector()
                );

            if (sourceVector.IsVectorNearParallelTo(targetVector))
                return LinFloat64IdentityLinearMap.Create(dimensions);

            return LinFloat64VectorToVectorRotation.Create(
                sourceVector.CreateUnitLinVector(),
                targetVector.CreateUnitLinVector()
            );
        }

        public static ILinFloat64SimpleVectorToVectorRotation CreateSimpleVectorToVectorRotation(this IReadOnlyList<double> sourceVector, IReadOnlyList<double> targetVector)
        {
            var dimensions = sourceVector.Count;

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
                        return LinFloat64IdentityLinearMap.Create(dimensions);

                    return new LinFloat64AxisToAxisRotation(
                        sourceAxis.Index,
                        sourceAxis.IsNegative,
                        targetAxis.Index,
                        targetAxis.IsNegative
                    );
                }

                return new LinFloat64AxisToVectorRotation(
                    sourceAxis.Index,
                    sourceAxis.IsNegative,
                    targetVector.CreateUnitLinVector()
                );
            }
            else
            {
                if (targetVectorIsAxis)
                {
                    return LinFloat64VectorToAxisRotation.Create(
                        sourceVector.CreateUnitLinVector(),
                        targetAxis
                    );
                }
            }

            if (sourceVector.IsVectorNearOrthogonalTo(targetVector))
                return new LinFloat64OrthogonalVectorToVectorRotation(
                    sourceVector.CreateUnitLinVector(),
                    targetVector.CreateUnitLinVector()
                );

            if (sourceVector.IsVectorNearParallelTo(targetVector))
                return LinFloat64IdentityLinearMap.Create(dimensions);

            return LinFloat64VectorToVectorRotation.Create(
                sourceVector.CreateUnitLinVector(),
                targetVector.CreateUnitLinVector()
            );
        }
    }
}

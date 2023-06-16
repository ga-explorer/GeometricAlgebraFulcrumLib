using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling
{
    public sealed class LinFloat64VectorDirectionalScaling :
        LinFloat64DirectionalScalingLinearMap
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorDirectionalScaling Create(double scalingFactor, Float64Vector scalingVector)
        {
            return new LinFloat64VectorDirectionalScaling(scalingFactor, scalingVector);
        }


        public override int VSpaceDimensions
            => ScalingVector.VSpaceDimensions;

        public override double ScalingFactor { get; }

        public override Float64Vector ScalingVector { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64VectorDirectionalScaling(double factor, Float64Vector vector)
        {
            Debug.Assert(
                vector.IsNearUnit() &&
                factor.IsValid() &&
                factor.IsNotZero()
            );

            ScalingFactor = factor;
            ScalingVector = vector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return
                ScalingVector.IsNearUnit() &&
                ScalingFactor.IsNotNaN() &&
                ScalingFactor.IsNotZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector MapBasisVector(int basisIndex)
        {
            var s = (ScalingFactor - 1d) * ScalingVector[basisIndex];

            return Float64VectorComposer
                .Create()
                .SetVector(ScalingVector, s)
                .AddTerm(basisIndex, 1d)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector MapVector(Float64Vector vector)
        {
            var s = (ScalingFactor - 1d) * Float64ArrayUtils.VectorDot(vector, ScalingVector);

            return Float64VectorComposer
                .Create()
                .SetVector(vector)
                .AddVector(ScalingVector, s)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
        {
            return GetVectorDirectionalScalingInverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorDirectionalScaling GetVectorDirectionalScalingInverse()
        {
            return new LinFloat64VectorDirectionalScaling(
                1d / ScalingFactor,
                ScalingVector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
        {
            return this;
        }
    }
}
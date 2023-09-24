using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Scaling
{
    public sealed class LinFloat64AxisDirectionalScaling :
        LinFloat64DirectionalScalingLinearMap
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64AxisDirectionalScaling Create(int dimensions, double scalingFactor, int scalingBasisIndex)
        {
            return new LinFloat64AxisDirectionalScaling(
                scalingFactor,
                dimensions,
                scalingBasisIndex
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64AxisDirectionalScaling Create(double scalingFactor, int dimensions, LinSignedBasisVector scalingAxis)
        {
            return new LinFloat64AxisDirectionalScaling(
                scalingFactor,
                dimensions,
                scalingAxis.Index
            );
        }


        public override double ScalingFactor { get; }

        public LinSignedBasisVector ScalingAxis { get; }

        public override int VSpaceDimensions { get; }

        public override Float64Vector ScalingVector
            => ScalingAxis.ToVector();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64AxisDirectionalScaling(double factor, int dimensions, int basisIndex)
        {
            Debug.Assert(
                factor.IsNotZero()
            );

            VSpaceDimensions = dimensions;
            ScalingFactor = factor;
            ScalingAxis = new LinSignedBasisVector(basisIndex, false);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return
                ScalingVector.IsNearUnit() &&
                ScalingFactor.IsNotZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0
            );

            var composer = Float64VectorComposer.Create();

            composer.SetTerm(basisIndex, 1d);

            if (basisIndex == ScalingAxis.Index)
                composer.AddTerm(basisIndex, ScalingFactor - 1d);

            return composer.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector MapVector(Float64Vector vector)
        {
            return Float64VectorComposer.Create()
                .SetVector(vector)
                .AddTerm(
                    ScalingAxis.Index,
                    (ScalingFactor - 1d) * vector[ScalingAxis.Index]
                ).GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
        {
            return GetAxisScalingInverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64AxisDirectionalScaling GetAxisScalingInverse()
        {
            return new LinFloat64AxisDirectionalScaling(
                1d / ScalingFactor,
                VSpaceDimensions,
                ScalingAxis.Index
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
        {
            return LinFloat64VectorDirectionalScaling.Create(
                1d,
                ScalingVector
            );
        }
    }
}
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling
{
    public sealed class LinFloat64AxisDirectionalScaling4D :
        LinFloat64DirectionalScalingLinearMap4D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64AxisDirectionalScaling4D Create(double scalingFactor, int scalingBasisIndex)
        {
            return new LinFloat64AxisDirectionalScaling4D(
                scalingFactor,
                scalingBasisIndex
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64AxisDirectionalScaling4D Create(double scalingFactor, LinUnitBasisVector4D scalingAxis)
        {
            return new LinFloat64AxisDirectionalScaling4D(
                scalingFactor,
                scalingAxis.GetIndex()
            );
        }


        public override double ScalingFactor { get; }

        public LinUnitBasisVector4D ScalingAxis { get; }
        
        public override Float64Vector4D ScalingVector
            => ScalingAxis.ToTuple4D();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64AxisDirectionalScaling4D(double factor, int basisIndex)
        {
            Debug.Assert(
                factor.IsNotZero()
            );

            ScalingFactor = factor;
            ScalingAxis = basisIndex.ToAxis4D(false);
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return 
                ScalingVector.IsNearUnit() &&
                ScalingFactor.IsNotZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0
            );

            var composer = new Float64Vector4DComposer();

            composer.SetTerm(basisIndex, 1d);

            if (basisIndex == ScalingAxis.GetIndex())
                composer.AddTerm(basisIndex, ScalingFactor - 1d);

            return composer.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapVector(IFloat64Tuple4D vector)
        {
            return new Float64Vector4DComposer()
                .SetVector(vector)
                .AddTerm(
                    ScalingAxis.GetIndex(), 
                    (ScalingFactor - 1d) * vector.GetItem(ScalingAxis.GetIndex())
                ).GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinFloat64DirectionalScalingLinearMap4D GetDirectionalScalingInverse()
        {
            return GetAxisScalingInverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64AxisDirectionalScaling4D GetAxisScalingInverse()
        {
            return new LinFloat64AxisDirectionalScaling4D(
                1d / ScalingFactor,
                ScalingAxis.GetIndex()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64VectorDirectionalScaling4D ToVectorDirectionalScaling()
        {
            return LinFloat64VectorDirectionalScaling4D.Create(
                1d, 
                ScalingVector
            );
        }
    }
}
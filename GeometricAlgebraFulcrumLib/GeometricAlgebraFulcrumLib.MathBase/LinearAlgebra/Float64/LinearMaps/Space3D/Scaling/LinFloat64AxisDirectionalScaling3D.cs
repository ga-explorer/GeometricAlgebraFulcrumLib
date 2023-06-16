using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling
{
    public sealed class LinFloat64AxisDirectionalScaling3D :
        LinFloat64DirectionalScalingLinearMap3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64AxisDirectionalScaling3D Create(double scalingFactor, int scalingBasisIndex)
        {
            return new LinFloat64AxisDirectionalScaling3D(
                scalingFactor,
                scalingBasisIndex
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64AxisDirectionalScaling3D Create(double scalingFactor, LinUnitBasisVector3D scalingAxis)
        {
            return new LinFloat64AxisDirectionalScaling3D(
                scalingFactor,
                scalingAxis.GetIndex()
            );
        }


        public override double ScalingFactor { get; }

        public LinUnitBasisVector3D ScalingAxis { get; }
        
        public override Float64Vector3D ScalingVector
            => ScalingAxis.ToVector3D();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64AxisDirectionalScaling3D(double factor, int basisIndex)
        {
            Debug.Assert(
                factor.IsNotZero()
            );

            ScalingFactor = factor;
            ScalingAxis = basisIndex.ToAxis3D(false);
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return 
                ScalingVector.IsNearUnit() &&
                ScalingFactor.IsNotZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector3D MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0
            );

            var composer = Float64Vector3DComposer.Create();

            composer.SetTerm(basisIndex, 1d);

            if (basisIndex == ScalingAxis.GetIndex())
                composer.AddTerm(basisIndex, ScalingFactor - 1d);

            return composer.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector3D MapVector(IFloat64Tuple3D vector)
        {
            return Float64Vector3DComposer.Create()
                .SetVector(vector)
                .AddTerm(
                    ScalingAxis.GetIndex(), 
                    (ScalingFactor - 1d) * vector.GetItem(ScalingAxis.GetIndex())
                ).GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse()
        {
            return GetAxisScalingInverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64AxisDirectionalScaling3D GetAxisScalingInverse()
        {
            return new LinFloat64AxisDirectionalScaling3D(
                1d / ScalingFactor,
                ScalingAxis.GetIndex()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling()
        {
            return LinFloat64VectorDirectionalScaling3D.Create(
                1d, 
                ScalingVector
            );
        }
    }
}
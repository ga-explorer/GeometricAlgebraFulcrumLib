using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Scaling;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps
{
    public sealed class LinFloat64IdentityLinearMap :
        LinFloat64VectorToVectorRotationBase,
        ILinFloat64DirectionalScalingLinearMap
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64IdentityLinearMap Create(int dimensions)
        {
            return new LinFloat64IdentityLinearMap(dimensions);
        }


        public override int VSpaceDimensions { get; }

        public override LinFloat64Vector SourceVector { get; }

        public override LinFloat64Vector TargetOrthogonalVector { get; }

        public override LinFloat64Vector TargetVector
            => SourceVector;

        public override double AngleCos
            => 1d;

        public override Float64PlanarAngle Angle
            => Float64PlanarAngle.Angle0;

        public double ScalingFactor
            => 1d;

        public LinFloat64Vector ScalingVector
            => SourceVector;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64IdentityLinearMap(int dimensions)
        {
            if (dimensions < 1)
                throw new ArgumentOutOfRangeException(nameof(dimensions));

            VSpaceDimensions = dimensions;
            SourceVector = 0.CreateLinVector();
            TargetOrthogonalVector = new LinFloat64Vector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return true;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsIdentity()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsNearIdentity(double epsilon = 1e-12d)
        {
            return true;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector ProjectOnRotationPlane(LinFloat64Vector vector)
        {
            return new LinFloat64Vector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapBasisVector(int axisIndex)
        {
            return axisIndex.CreateLinVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapVector(LinFloat64Vector x)
        {
            return x;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapVectorProjection(LinFloat64Vector vector)
        {
            return new LinFloat64Vector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64SimpleRotationBase GetSimpleVectorRotationInverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
        {
            return LinFloat64VectorDirectionalScaling.Create(
                1d, 
                0.CreateLinVector()
            );
        }
    }
}
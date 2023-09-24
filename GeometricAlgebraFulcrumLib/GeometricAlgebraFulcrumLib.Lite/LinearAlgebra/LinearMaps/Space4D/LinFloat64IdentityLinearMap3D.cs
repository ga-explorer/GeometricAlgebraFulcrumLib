using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space4D.Rotation;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space4D
{
    public sealed class LinFloat64IdentityLinearMap4D :
        LinFloat64VectorToVectorRotationBase4D,
        ILinFloat64DirectionalScalingLinearMap4D
    {
        public static LinFloat64IdentityLinearMap4D Instance { get; }
            = new LinFloat64IdentityLinearMap4D();

        
        public override Float64Vector4D SourceVector { get; }

        public override Float64Vector4D TargetOrthogonalVector { get; }

        public override Float64Vector4D TargetVector
            => SourceVector;

        public override double AngleCos
            => 1d;

        public override Float64PlanarAngle Angle
            => Float64PlanarAngle.Angle0;

        public double ScalingFactor
            => 1d;

        public Float64Vector4D ScalingVector
            => SourceVector;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64IdentityLinearMap4D()
        {
            SourceVector = Float64Vector4D.E1;
            TargetOrthogonalVector = Float64Vector4D.E2;
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
        public override Float64Vector4D ProjectOnRotationPlane(Float64Vector4D vector)
        {
            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapBasisVector(int axisIndex)
        {
            return Float64Vector4D.BasisVectors[axisIndex];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapVector(IFloat64Vector4D x)
        {
            return x.ToTuple4D();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapVectorProjection(Float64Vector4D vector)
        {
            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinFloat64DirectionalScalingLinearMap4D GetDirectionalScalingInverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorDirectionalScaling4D ToVectorDirectionalScaling()
        {
            return LinFloat64VectorDirectionalScaling4D.Create(
                1d, 
                Float64Vector4D.E1
            );
        }
    }
}
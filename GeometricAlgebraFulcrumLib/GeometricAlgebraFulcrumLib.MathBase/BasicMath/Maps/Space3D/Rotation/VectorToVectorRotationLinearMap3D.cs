using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D.Rotation
{
    public abstract class VectorToVectorRotationLinearMap3D :
        SimpleRotationLinearMap3D
    {
        /// <summary>
        /// The unit vector where the rotation starts
        /// </summary>
        public abstract Float64Tuple3D SourceVector { get; }

        /// <summary>
        /// A scaled version of the orthogonal component (rejection) of
        /// TargetVector relative to SourceVector
        /// </summary>
        public abstract Float64Tuple3D TargetOrthogonalVector { get; }

        /// <summary>
        /// The unit vector where the rotation ends
        /// </summary>
        public abstract Float64Tuple3D TargetVector { get; }

        /// <summary>
        /// The dot product of TargetVector and SourceVector
        /// </summary>
        public abstract double AngleCos { get; }

        /// <summary>
        /// The smallest angle between TargetVector and SourceVector
        /// </summary>
        public abstract Float64PlanarAngle Angle { get; }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsIdentity()
        {
            return (AngleCos - 1d).IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsNearIdentity(double epsilon = 1e-12d)
        {
            return (AngleCos - 1d).IsNearZero(epsilon);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D ProjectOnRotationPlane(Float64Tuple3D vector)
        {
            var uvDot = AngleCos;
            var xuDot = vector.VectorDot(SourceVector);
            var xvDot = vector.VectorDot(TargetVector);
            var bivectorNormSquaredInv = 1d / (1d - uvDot * uvDot);

            var uScalar = (xuDot - xvDot * uvDot) * bivectorNormSquaredInv;
            var vScalar = (xvDot - xuDot * uvDot) * bivectorNormSquaredInv;
        
            return uScalar * SourceVector + vScalar * TargetVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D MapVectorProjection(Float64Tuple3D vector)
        {
            var r = vector.VectorDot(TargetOrthogonalVector);
            var s = vector.VectorDot(SourceVector);
        
            var uScalar = r / (AngleCos - 1d);
            var vScalar = s - uScalar * AngleCos;
        
            return uScalar * SourceVector + vScalar * TargetVector;
        }

        //public virtual Float64Tuple MapVectorProjection(Float64Tuple vector)
        //{
        //    return MapVector(ProjectOnRotationPlane(vector));
        //}
    }
}
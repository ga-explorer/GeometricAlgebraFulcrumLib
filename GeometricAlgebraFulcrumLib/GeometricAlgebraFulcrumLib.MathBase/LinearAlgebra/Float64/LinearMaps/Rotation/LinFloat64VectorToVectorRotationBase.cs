using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Reflection;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation
{
    public abstract class LinFloat64VectorToVectorRotationBase :
        LinFloat64SimpleRotationBase,
        ILinFloat64SimpleVectorToVectorRotation
    {
        public override int VSpaceDimensions
            => SourceVector.VSpaceDimensions;

        /// <summary>
        /// The unit vector where the rotation starts
        /// </summary>
        public abstract LinFloat64Vector SourceVector { get; }

        /// <summary>
        /// A scaled version of the orthogonal component (rejection) of
        /// TargetVector relative to SourceVector
        /// </summary>
        public abstract LinFloat64Vector TargetOrthogonalVector { get; }

        /// <summary>
        /// The unit vector where the rotation ends
        /// </summary>
        public abstract LinFloat64Vector TargetVector { get; }

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


        public abstract LinFloat64Vector ProjectOnRotationPlane(LinFloat64Vector vector);

        public abstract LinFloat64Vector MapVectorProjection(LinFloat64Vector vector);

        //public virtual LinFloat64Vector MapVectorProjection(LinFloat64Vector vector)
        //{
        //    return MapVector(ProjectOnRotationPlane(vector));
        //}

        /// <summary>
        /// Construct middle unit vector between SourceVector and TargetVector
        /// </summary>
        /// <returns></returns>
        public LinFloat64Vector GetMiddleUnitVector()
        {
            var wLengthInv = 1d / (2d + 2d * AngleCos).Sqrt();

            return LinFloat64VectorComposer
                .Create()
                .SetVector(SourceVector)
                .AddVector(TargetVector)
                .Times(wLengthInv)
                .GetVector();
        }

        public Pair<LinFloat64HyperPlaneNormalReflection> GetHyperPlaneReflectionPair()
        {
            return new Pair<LinFloat64HyperPlaneNormalReflection>(
                LinFloat64HyperPlaneNormalReflection.Create(SourceVector),
                LinFloat64HyperPlaneNormalReflection.Create(GetMiddleUnitVector())
            );
        }

        /// <summary>
        /// Compute a rotated version of u in the u-v rotational plane by the given angle
        /// </summary>
        /// <param name="angle1"></param>
        /// <returns></returns>
        public LinFloat64Vector GetRotatedSourceVector(Float64PlanarAngle angle1)
        {
            var scalar1 = angle1.Cos();
            var scalar2 = angle1.Sin() / TargetOrthogonalVector.ENorm();

            return LinFloat64VectorComposer
                .Create()
                .SetVector(SourceVector, scalar1)
                .AddVector(TargetOrthogonalVector, scalar2)
                .GetVector();
        }

        public Pair<LinFloat64Vector> GetRotatedSourceVectorPair(Float64PlanarAngle angle1, Float64PlanarAngle angle2)
        {
            var norm = TargetOrthogonalVector.ENorm();
            
            var scalar1 = angle1.Cos();
            var scalar2 = angle1.Sin() / norm;

            var v1 = 
                LinFloat64VectorComposer
                    .Create()
                    .SetVector(SourceVector, scalar1)
                    .AddVector(TargetOrthogonalVector, scalar2)
                    .GetVector();
            
            scalar1 = angle2.Cos();
            scalar2 = angle2.Sin() / norm;

            var v2 = 
                LinFloat64VectorComposer
                    .Create()
                    .SetVector(SourceVector, scalar1)
                    .AddVector(TargetOrthogonalVector, scalar2)
                    .GetVector();
            
            return new Pair<LinFloat64Vector>(v1, v2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
        {
            var reflection = 
                LinFloat64HyperPlaneNormalReflectionSequence.Create(VSpaceDimensions);

            var (r1, r2) = 
                GetHyperPlaneReflectionPair();

            reflection
                .AppendMap(r1)
                .AppendMap(r2);

            return reflection;
        }
    }
}
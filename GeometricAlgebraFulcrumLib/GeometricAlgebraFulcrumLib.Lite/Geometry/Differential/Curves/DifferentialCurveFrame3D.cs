using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Curves
{
    public class DifferentialCurveFrame3D :
        IFloat64Vector3D
    {
        /// <summary>
        /// Create a set of 3 right-handed orthonormal direction vectors from the given vector
        /// </summary>
        /// <param name="parameterValue"></param>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <param name="rightHanded"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DifferentialCurveFrame3D CreateOrthonormal(double parameterValue, IFloat64Vector3D origin, IFloat64Vector3D direction, bool rightHanded = true)
        {
            Debug.Assert(
                !direction.ENormSquared().IsAlmostZero()
            );

            var u = direction.ToUnitVector();
            var v = direction.GetUnitNormal();
            var w = rightHanded ? u.VectorUnitCross(v) : v.VectorUnitCross(u);

            return new DifferentialCurveFrame3D(
                parameterValue,
                origin.ToVector3D(),
                u,
                v,
                w
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DifferentialCurveFrame3D Create(double parameterValue, IFloat64Vector3D origin, IFloat64Vector3D direction1, IPair<IFloat64Vector3D> direction23Pair)
        {
            Debug.Assert(
                !direction1.ENormSquared().IsAlmostZero()
            );

            return new DifferentialCurveFrame3D(
                parameterValue,
                origin.ToVector3D(),
                direction1.ToVector3D(),
                direction23Pair.Item1.ToVector3D(),
                direction23Pair.Item2.ToVector3D()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DifferentialCurveFrame3D Create(double parameterValue, IFloat64Vector3D origin, IFloat64Vector3D direction1, IFloat64Vector3D direction2, IFloat64Vector3D direction3)
        {
            Debug.Assert(
                !direction1.ENormSquared().IsAlmostZero()
            );

            return new DifferentialCurveFrame3D(
                parameterValue,
                origin.ToVector3D(),
                direction1.ToVector3D(),
                direction2.ToVector3D(),
                direction3.ToVector3D()
            );
        }


        public double ParameterValue { get; }
        
        public int VSpaceDimensions 
            => 3;

        public double Item1 
            => Origin.X;

        public double Item2 
            => Origin.Y;

        public double Item3 
            => Origin.Z;

        public Float64Scalar X 
            => Origin.X;

        public Float64Scalar Y 
            => Origin.Y;

        public Float64Scalar Z 
            => Origin.Z;

        public Float64Vector3D Origin { get; }
        
        public Float64Vector3D Direction1 { get; }

        public Float64Vector3D Direction2 { get; }

        public Float64Vector3D Direction3 { get; }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DifferentialCurveFrame3D(double parameterValue, Float64Vector3D origin, Float64Vector3D direction1, Float64Vector3D direction2, Float64Vector3D direction3)
        {
            ParameterValue = parameterValue;
            Origin = origin;
            Direction1 = direction1;
            Direction2 = direction2;
            Direction3 = direction3;

            Debug.Assert(IsValid());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return ParameterValue.IsValid() &&
                   Origin.IsValid() &&
                   Direction1.IsValid() &&
                   Direction2.IsValid() &&
                   Direction3.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsOrthogonal(double epsilon = 1e-12)
        {
            return 
                Direction1.ESp(Direction2).IsNearZero(epsilon) &&
                Direction1.ESp(Direction3).IsNearZero(epsilon) &&
                Direction2.ESp(Direction3).IsNearZero(epsilon);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsOrthonormal(double epsilon = 1e-12)
        {
            return 
                Direction1.ESp(Direction1).IsNearOne(epsilon) &&
                Direction2.ESp(Direction2).IsNearOne(epsilon) &&
                Direction3.ESp(Direction3).IsNearOne(epsilon) &&
                Direction1.ESp(Direction2).IsNearZero(epsilon) &&
                Direction1.ESp(Direction3).IsNearZero(epsilon) &&
                Direction2.ESp(Direction3).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFrame3D()
        {
            return !Direction1.Determinant(Direction2, Direction3).IsNearZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsRightHanded()
        {
            return Direction1.Determinant(Direction2, Direction3) > 0.0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLeftHanded()
        {
            return Direction1.Determinant(Direction2, Direction3) < 0.0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix3 GetDirectionsMatrix()
        {
            return new SquareMatrix3
            {
                Scalar00 = Direction1.X,
                Scalar10 = Direction1.Y,
                Scalar20 = Direction1.Z,

                Scalar01 = Direction2.X,
                Scalar11 = Direction2.Y,
                Scalar21 = Direction2.Z,

                Scalar02 = Direction3.X,
                Scalar12 = Direction3.Y,
                Scalar22 = Direction3.Z
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetLocalVector(double u, double v, double w)
        {
            return u * Direction1 +
                   v * Direction2 +
                   w * Direction3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetLocalVector(ITriplet<double> scalarList)
        {
            return scalarList.Item1 * Direction1 +
                   scalarList.Item2 * Direction2 +
                   scalarList.Item3 * Direction3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetLocalPoint(ITriplet<double> scalarList)
        {
            return Origin +
                   scalarList.Item1 * Direction1 +
                   scalarList.Item2 * Direction2 +
                   scalarList.Item3 * Direction3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetLocalPoint(double u, double v, double w)
        {
            return Origin +
                   u * Direction1 +
                   v * Direction2 +
                   w * Direction3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Quaternion GetRotationPartQuaternion()
        {
            return GetDirectionsMatrix().GetRotationPart().GetQuaternion();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Quaternion GetRelativeRotationQuaternion(SquareMatrix3 refFrameMatrixInverse)
        {
            var matrix = GetDirectionsMatrix() * refFrameMatrixInverse;

            return matrix.GetRotationPart().GetQuaternion();
        }
    }
}
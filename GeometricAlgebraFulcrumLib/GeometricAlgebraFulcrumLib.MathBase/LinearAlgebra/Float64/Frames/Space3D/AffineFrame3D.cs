using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space3D
{
    /// <summary>
    /// This class represents a directions frame of 3 vectors U, V, W where
    /// the components are double precision numbers
    /// </summary>
    public class AffineFrame3D :
        IFloat64Tuple3D
    {
        /// <summary>
        /// Create a set of 3 right-handed orthonormal direction vectors from the given vector
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <param name="rightHanded"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineFrame3D CreateOrthonormal(IFloat64Tuple3D origin, IFloat64Tuple3D direction, bool rightHanded = true)
        {
            Debug.Assert(!direction.ENormSquared().IsAlmostZero());

            var u = direction.ToUnitVector();
            var v = direction.GetUnitNormal();
            var w = rightHanded ? u.VectorUnitCross(v) : v.VectorUnitCross(u);

            return new AffineFrame3D(
                origin.ToVector3D(),
                u,
                v,
                w
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AffineFrame3D Create(IFloat64Tuple3D origin, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, IFloat64Tuple3D direction3)
        {
            Debug.Assert(!direction1.ENormSquared().IsAlmostZero());

            return new AffineFrame3D(
                origin.ToVector3D(),
                direction1.ToVector3D(),
                direction2.ToVector3D(),
                direction3.ToVector3D()
            );
        }

        
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
        private AffineFrame3D(Float64Vector3D origin, Float64Vector3D direction1, Float64Vector3D direction2, Float64Vector3D direction3)
        {
            Origin = origin;
            Direction1 = direction1;
            Direction2 = direction2;
            Direction3 = direction3;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Origin.IsValid() &&
                   Direction1.IsValid() &&
                   Direction2.IsValid() &&
                   Direction3.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFrame3D()
        {
            if (Direction1.ENormSquared().IsNearZero()) return false;
            if (Direction2.ENormSquared().IsNearZero()) return false;
            if (Direction3.ENormSquared().IsNearZero()) return false;

            return true;
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
    }
}
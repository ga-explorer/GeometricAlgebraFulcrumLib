using System.Diagnostics;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Frames.Space3D
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
        public static AffineFrame3D CreateOrthonormal(IFloat64Tuple3D origin, IFloat64Tuple3D direction, bool rightHanded = true)
        {
            Debug.Assert(!direction.GetVectorNormSquared().IsAlmostZero());

            var u = direction.ToUnitVector();
            var v = direction.GetUnitNormal();
            var w = rightHanded ? u.VectorUnitCross(v) : v.VectorUnitCross(u);

            return new AffineFrame3D(
                origin.ToTuple3D(),
                u,
                v,
                w
            );
        }
        
        public static AffineFrame3D Create(IFloat64Tuple3D origin, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, IFloat64Tuple3D direction3)
        {
            Debug.Assert(!direction1.GetVectorNormSquared().IsAlmostZero());

            return new AffineFrame3D(
                origin.ToTuple3D(),
                direction1.ToTuple3D(),
                direction2.ToTuple3D(),
                direction3.ToTuple3D()
            );
        }


        public double Item1 
            => Origin.X;

        public double Item2 
            => Origin.Y;

        public double Item3 
            => Origin.Z;

        public double X 
            => Origin.X;

        public double Y 
            => Origin.Y;

        public double Z 
            => Origin.Z;

        public Float64Tuple3D Origin { get; }
        
        public Float64Tuple3D Direction1 { get; }

        public Float64Tuple3D Direction2 { get; }

        public Float64Tuple3D Direction3 { get; }
        

        private AffineFrame3D(Float64Tuple3D origin, Float64Tuple3D direction1, Float64Tuple3D direction2, Float64Tuple3D direction3)
        {
            Origin = origin;
            Direction1 = direction1;
            Direction2 = direction2;
            Direction3 = direction3;

            Debug.Assert(IsValid());
        }
        

        public bool IsValid()
        {
            return Origin.IsValid() &&
                   Direction1.IsValid() &&
                   Direction2.IsValid() &&
                   Direction3.IsValid();
        }

        public bool IsFrame3D()
        {
            if (Direction1.GetVectorNormSquared().IsNearZero()) return false;
            if (Direction2.GetVectorNormSquared().IsNearZero()) return false;
            if (Direction3.GetVectorNormSquared().IsNearZero()) return false;

            return true;
        }

        public bool IsRightHanded()
        {
            return VectorUtils.Determinant(Direction1, Direction2, Direction3) > 0.0d;
        }

        public bool IsLeftHanded()
        {
            return VectorUtils.Determinant(Direction1, Direction2, Direction3) < 0.0d;
        }

        public Float64Tuple3D GetLocalVector(double u, double v, double w)
        {
            return u * Direction1 +
                   v * Direction2 +
                   w * Direction3;
        }

        public Float64Tuple3D GetLocalVector(ITriplet<double> scalarList)
        {
            return scalarList.Item1 * Direction1 +
                   scalarList.Item2 * Direction2 +
                   scalarList.Item3 * Direction3;
        }
        
        public Float64Tuple3D GetLocalPoint(ITriplet<double> scalarList)
        {
            return Origin +
                   scalarList.Item1 * Direction1 +
                   scalarList.Item2 * Direction2 +
                   scalarList.Item3 * Direction3;
        }
        
        public Float64Tuple3D GetLocalPoint(double u, double v, double w)
        {
            return Origin +
                   u * Direction1 +
                   v * Direction2 +
                   w * Direction3;
        }
    }
}
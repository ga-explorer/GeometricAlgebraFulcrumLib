using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Curves
{
    public class DifferentialCurveFrame3D :
        IFloat64Tuple3D
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
        public static DifferentialCurveFrame3D CreateOrthonormal(double parameterValue, IFloat64Tuple3D origin, IFloat64Tuple3D direction, bool rightHanded = true)
        {
            Debug.Assert(
                !direction.GetVectorNormSquared().IsAlmostZero()
            );

            var u = direction.ToUnitVector();
            var v = direction.GetUnitNormal();
            var w = rightHanded ? u.VectorUnitCross(v) : v.VectorUnitCross(u);

            return new DifferentialCurveFrame3D(
                parameterValue,
                origin.ToTuple3D(),
                u,
                v,
                w
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DifferentialCurveFrame3D Create(double parameterValue, IFloat64Tuple3D origin, IFloat64Tuple3D direction1, IFloat64Tuple3D direction2, IFloat64Tuple3D direction3)
        {
            Debug.Assert(
                !direction1.GetVectorNormSquared().IsAlmostZero()
            );

            return new DifferentialCurveFrame3D(
                parameterValue,
                origin.ToTuple3D(),
                direction1.ToTuple3D(),
                direction2.ToTuple3D(),
                direction3.ToTuple3D()
            );
        }


        public double ParameterValue { get; }

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
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DifferentialCurveFrame3D(double parameterValue, Float64Tuple3D origin, Float64Tuple3D direction1, Float64Tuple3D direction2, Float64Tuple3D direction3)
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
                Direction1.VectorDot(Direction2).IsNearZero(epsilon) &&
                Direction1.VectorDot(Direction3).IsNearZero(epsilon) &&
                Direction2.VectorDot(Direction3).IsNearZero(epsilon);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsOrthonormal(double epsilon = 1e-12)
        {
            return 
                Direction1.VectorDot(Direction1).IsNearOne(epsilon) &&
                Direction2.VectorDot(Direction2).IsNearOne(epsilon) &&
                Direction3.VectorDot(Direction3).IsNearOne(epsilon) &&
                Direction1.VectorDot(Direction2).IsNearZero(epsilon) &&
                Direction1.VectorDot(Direction3).IsNearZero(epsilon) &&
                Direction2.VectorDot(Direction3).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFrame3D()
        {
            return !EuclideanFloat64TupleUtils.Determinant(Direction1, Direction2, Direction3).IsNearZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsRightHanded()
        {
            return EuclideanFloat64TupleUtils.Determinant(Direction1, Direction2, Direction3) > 0.0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLeftHanded()
        {
            return EuclideanFloat64TupleUtils.Determinant(Direction1, Direction2, Direction3) < 0.0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetLocalVector(double u, double v, double w)
        {
            return u * Direction1 +
                   v * Direction2 +
                   w * Direction3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetLocalVector(ITriplet<double> scalarList)
        {
            return scalarList.Item1 * Direction1 +
                   scalarList.Item2 * Direction2 +
                   scalarList.Item3 * Direction3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetLocalPoint(ITriplet<double> scalarList)
        {
            return Origin +
                   scalarList.Item1 * Direction1 +
                   scalarList.Item2 * Direction2 +
                   scalarList.Item3 * Direction3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetLocalPoint(double u, double v, double w)
        {
            return Origin +
                   u * Direction1 +
                   v * Direction2 +
                   w * Direction3;
        }



    }
}
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
{
    public sealed class RotateByAxisAngleMap3D :
        RotateMap3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateByAxisAngleMap3D Create(ITuple3D normal, PlanarAngle angle)
        {
            return new RotateByAxisAngleMap3D(normal, angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateByAxisAngleMap3D CreateFromUnitVectors(ITuple3D vector1, ITuple3D vector2)
        {
            Debug.Assert(
                vector1.IsValid() &&
                vector2.IsValid() &&
                vector1.IsNearUnitVector() &&
                vector2.IsNearUnitVector()
            );

            if (vector1.IsAlmostVector(vector2))
                return new RotateByAxisAngleMap3D(
                    vector1,
                    PlanarAngle.Angle0
                );

            if (vector1.IsAlmostVectorNegative(vector2))
                return new RotateByAxisAngleMap3D(
                    vector1.GetUnitNormal(),
                    PlanarAngle.Angle180
                );
            
            return new RotateByAxisAngleMap3D(
                vector1.VectorUnitCross(vector2),
                PlanarAngle.CreateFromUnitVectors(vector1, vector2)
            );
        }


        private Tuple3D _unitNormal;
        public Tuple3D UnitNormal
        {
            get => _unitNormal;
            set
            {
                _unitNormal = value;

                UpdateRotationMatrix();

                Debug.Assert(IsValid());
            }
        }
        
        private PlanarAngle _angle;
        public PlanarAngle Angle
        {
            get => _angle;
            set
            {
                _angle = value;

                UpdateRotationMatrix();

                Debug.Assert(IsValid());
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RotateByAxisAngleMap3D([NotNull] ITuple3D normal, [NotNull] PlanarAngle angle)
        {
            _unitNormal = normal.ToTuple3D();
            _angle = angle;

            UpdateRotationMatrix();

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return Angle.IsValid() && 
                   UnitNormal.IsValid() &&
                   UnitNormal.IsNearUnitVector() &&
                   RotationMatrix.Determinant.IsNearEqual(1d);
        }

        protected override void UpdateRotationMatrix()
        {
            if (Angle.Degrees == 0d)
            {
                RotationMatrix = SquareMatrix3.CreateIdentityMatrix();
                return;
            }
            
            var x = _unitNormal.X;
            var y = _unitNormal.Y;
            var z = _unitNormal.Z;
            var cosAngle = Angle.Cos();
            var sinAngle = Angle.Sin();
            var oneMinusCosAngle = 1d - cosAngle;
            var xx = x * x;
            var yy = y * y;
            var zz = z * z;
            var xy = x * y;
            var xz = x * z;
            var yz = y * z;

            RotationMatrix.Scalar00 = xx + (1d - xx) * cosAngle;
            RotationMatrix.Scalar10 = xy * oneMinusCosAngle + z * sinAngle;
            RotationMatrix.Scalar20 = xz * oneMinusCosAngle - y * sinAngle;

            RotationMatrix.Scalar01 = xy * oneMinusCosAngle - z * sinAngle;
            RotationMatrix.Scalar11 = yy + (1d - yy) * cosAngle;
            RotationMatrix.Scalar21 = yz * oneMinusCosAngle + x * sinAngle;

            RotationMatrix.Scalar02 = xz * oneMinusCosAngle + y * sinAngle;
            RotationMatrix.Scalar12 = yz * oneMinusCosAngle - x * sinAngle;
            RotationMatrix.Scalar22 = zz + (1d - zz) * cosAngle;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRotateMap3D InverseRotateMap()
        {
            return new RotateByAxisAngleMap3D(UnitNormal, -Angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Tuple<PlanarAngle, Tuple3D> GetAngleAxis()
        {
            return new Tuple<PlanarAngle, Tuple3D>(Angle, _unitNormal);
        }
    }
}
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
{
    public sealed class RotateByVectorToVectorMap3D :
        RotateMap3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateByVectorToVectorMap3D Create(IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2)
        {
            return new RotateByVectorToVectorMap3D(
                unitVector1,
                unitVector2
            );
        }


        private Float64Tuple3D _unitVector1;
        public Float64Tuple3D UnitVector1
        {
            get => _unitVector1;
            set
            {
                _unitVector1 = value;

                UpdateRotationMatrix();

                Debug.Assert(IsValid());
            }
        }

        private Float64Tuple3D _unitVector2;
        public Float64Tuple3D UnitVector2
        {
            get => _unitVector2;
            set
            {
                _unitVector2 = value;

                UpdateRotationMatrix();

                Debug.Assert(IsValid());
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RotateByVectorToVectorMap3D([NotNull] IFloat64Tuple3D unitVector1, [NotNull] IFloat64Tuple3D unitVector2)
        {
            _unitVector1 = unitVector1.ToTuple3D();
            _unitVector2 = unitVector2.ToTuple3D();

            UpdateRotationMatrix();

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _unitVector1.IsValid() &&
                   _unitVector2.IsValid() &&
                   _unitVector1.IsNearUnitVector() &&
                   _unitVector2.IsNearUnitVector() &&
                   RotationMatrix.Determinant.IsNearEqual(1d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void UpdateRotationMatrix()
        {
            RotationMatrix = 
                SquareMatrix3.CreateVectorToVectorRotationMatrix3D(
                    _unitVector1, 
                    _unitVector2
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRotateMap3D InverseRotateMap()
        {
            return new RotateByVectorToVectorMap3D(_unitVector2, _unitVector1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Tuple<PlanarAngle, Float64Tuple3D> GetAngleAxis()
        {
            var angle = PlanarAngle.CreateFromUnitVectors(_unitVector1, _unitVector2);
            var axis = _unitVector1.VectorUnitCross(_unitVector2);

            return new Tuple<PlanarAngle, Float64Tuple3D>(angle, axis);
        }
    }
}
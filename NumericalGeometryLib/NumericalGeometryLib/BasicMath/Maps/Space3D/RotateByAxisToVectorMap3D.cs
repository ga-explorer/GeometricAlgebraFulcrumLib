using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
{
    public sealed class RotateByAxisToVectorMap3D :
        RotateMap3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateByAxisToVectorMap3D Create(Axis3D axis, IFloat64Tuple3D unitVector)
        {
            return new RotateByAxisToVectorMap3D(axis, unitVector);
        }


        public Axis3D Axis { get; set; }

        private Float64Tuple3D _unitVector;
        public Float64Tuple3D UnitVector
        {
            get => _unitVector;
            set
            {
                _unitVector = value;

                UpdateRotationMatrix();

                Debug.Assert(IsValid());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RotateByAxisToVectorMap3D(Axis3D axis, [NotNull] IFloat64Tuple3D unitVector)
        {
            Axis = axis;
            _unitVector = unitVector.ToTuple3D();

            UpdateRotationMatrix();

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _unitVector.IsValid() &&
                   _unitVector.IsNearUnitVector() &&
                   RotationMatrix.Determinant.IsNearEqual(1d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void UpdateRotationMatrix()
        {
            RotationMatrix = 
                SquareMatrix3.CreateAxisToVectorRotationMatrix3D(
                    Axis,
                    _unitVector
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRotateMap3D InverseRotateMap()
        {
            return RotateByVectorToAxisMap3D.Create(_unitVector, Axis);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Tuple<PlanarAngle, Float64Tuple3D> GetAngleAxis()
        {
            var unitVector2 = Axis.GetVector3D();

            var angle = PlanarAngle.CreateFromUnitVectors(_unitVector, unitVector2);
            var axis = _unitVector.VectorUnitCross(unitVector2);

            return new Tuple<PlanarAngle, Float64Tuple3D>(angle, axis);
        }
    }
}
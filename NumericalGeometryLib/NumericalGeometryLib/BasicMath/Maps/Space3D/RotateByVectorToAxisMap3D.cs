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
    public sealed class RotateByVectorToAxisMap3D :
        RotateMap3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateByVectorToAxisMap3D Create(IFloat64Tuple3D unitVector, Axis3D axis)
        {
            return new RotateByVectorToAxisMap3D(unitVector, axis);
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
        private RotateByVectorToAxisMap3D([NotNull] IFloat64Tuple3D unitVector, Axis3D axis)
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
                   _unitVector.IsNearUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void UpdateRotationMatrix()
        {
            RotationMatrix =
                SquareMatrix3.CreateVectorToAxisRotationMatrix3D(_unitVector, Axis);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRotateMap3D InverseRotateMap()
        {
            return RotateByAxisToVectorMap3D.Create(Axis, _unitVector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Tuple<PlanarAngle, Float64Tuple3D> GetAngleAxis()
        {
            var unitVector1 = Axis.GetVector3D();

            var angle = PlanarAngle.CreateFromUnitVectors(unitVector1, _unitVector);
            var axis = unitVector1.VectorUnitCross(_unitVector);

            return new Tuple<PlanarAngle, Float64Tuple3D>(angle, axis);
        }
    }
}
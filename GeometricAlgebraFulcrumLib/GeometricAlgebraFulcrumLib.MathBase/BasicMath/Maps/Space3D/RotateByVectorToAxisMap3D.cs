using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Constants;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D
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
        private RotateByVectorToAxisMap3D(IFloat64Tuple3D unitVector, Axis3D axis)
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
        public override Tuple<Float64PlanarAngle, Float64Tuple3D> GetAngleAxis()
        {
            var unitVector1 = Axis.GetVector3D();

            var angle = Float64PlanarAngle.CreateFromUnitVectors(unitVector1, _unitVector);
            var axis = unitVector1.VectorUnitCross(_unitVector);

            return new Tuple<Float64PlanarAngle, Float64Tuple3D>(angle, axis);
        }
    }
}
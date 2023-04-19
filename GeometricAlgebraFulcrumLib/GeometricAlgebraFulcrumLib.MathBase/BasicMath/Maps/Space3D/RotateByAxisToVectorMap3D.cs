using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Constants;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D
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
        private RotateByAxisToVectorMap3D(Axis3D axis, IFloat64Tuple3D unitVector)
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
        public override Tuple<Float64PlanarAngle, Float64Tuple3D> GetAngleAxis()
        {
            var unitVector2 = Axis.GetVector3D();

            var angle = Float64PlanarAngle.CreateFromUnitVectors(_unitVector, unitVector2);
            var axis = _unitVector.VectorUnitCross(unitVector2);

            return new Tuple<Float64PlanarAngle, Float64Tuple3D>(angle, axis);
        }
    }
}
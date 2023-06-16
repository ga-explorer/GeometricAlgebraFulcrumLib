using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation
{
    public sealed class LinFloat64AxisToVectorRotation4D :
        LinFloat64VectorToVectorRotationBase4D
    {
        public LinUnitBasisVector4D SourceAxis { get; }

        public override Float64Vector4D SourceVector { get; }

        public override Float64Vector4D TargetOrthogonalVector { get; }

        public override Float64Vector4D TargetVector { get; }

        public override double AngleCos { get; }

        public override Float64PlanarAngle Angle
            => AngleCos.ArcCos();


        public LinFloat64AxisToVectorRotation4D(int uAxisIndex, bool uAxisNegative, IFloat64Tuple4D v)
        {
            Debug.Assert(
                v.IsNearUnit()
            );

            SourceAxis = uAxisIndex.ToAxis4D(uAxisNegative);
            SourceVector = SourceAxis.ToTuple4D();
            TargetVector = v.ToTuple4D();

            AngleCos = TargetVector.ESp(SourceAxis).Clamp(-1d, 1d);

            Debug.Assert(
                !AngleCos.IsNearMinusOne()
            );

            TargetOrthogonalVector =
                Float64Vector4DComposer
                    .Create()
                    .SetVector(TargetVector)
                    .SubtractTerm(SourceAxis.GetIndex(), SourceAxis.IsNegative() ? -AngleCos : AngleCos)
                    .Times(1d / (1d + AngleCos))
                    .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return 
                TargetVector.IsNearUnit() &&
                !AngleCos.IsNearMinusOne();
        }
    
        public override Float64Vector4D ProjectOnRotationPlane(Float64Vector4D vector)
        {
            var xuDot = vector.ESp(SourceAxis);
            var xvDot = vector.ESp(TargetVector);
            var bivectorNormSquaredInv = 1d / (1d - AngleCos * AngleCos);

            var uScalar = (xuDot - xvDot * AngleCos) * bivectorNormSquaredInv;
            var vScalar = (xvDot - xuDot * AngleCos) * bivectorNormSquaredInv;

            return Float64Vector4DComposer
                .Create()
                .SetVector(TargetVector, vScalar)
                .AddTerm(SourceAxis.GetIndex(), SourceAxis.IsNegative() ? -uScalar : uScalar)
                .GetVector();
        }

        public override Float64Vector4D MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0 && basisIndex < VSpaceDimensions
            );

            var r = TargetOrthogonalVector[basisIndex];
            var s = 
                basisIndex == SourceAxis.GetIndex() 
                    ? SourceAxis.GetSign().ToFloat64() 
                    : 0d;

            var rsPlus = r + s;
            var rsMinus = r - s;

            return Float64Vector4DComposer
                .Create()
                .SetVector(TargetVector, -rsMinus)
                .AddTerm(basisIndex, 1d)
                .SubtractTerm(SourceAxis.GetIndex(), SourceAxis.IsNegative() ? -rsPlus : rsPlus)
                .GetVector();
        }

        public override Float64Vector4D MapVector(IFloat64Tuple4D vector)
        {
            var r = vector.ESp(TargetOrthogonalVector);
            
            var s = SourceAxis.IsNegative() 
                ? -vector.GetItem(SourceAxis.GetIndex()) 
                : vector.GetItem(SourceAxis.GetIndex());

            var rsPlus = r + s;
            var rsMinus = r - s;

            return Float64Vector4DComposer
                .Create()
                .SetVector(vector)
                .SubtractVector(TargetVector, rsMinus)
                .SubtractTerm(
                    SourceAxis.GetIndex(), 
                    SourceAxis.IsNegative() ? -rsPlus : rsPlus
                ).GetVector();
        }
    
        public override Float64Vector4D MapVectorProjection(Float64Vector4D vector)
        {
            var r = vector.ESp(TargetOrthogonalVector);
            var s = SourceAxis.IsNegative() ? -vector[SourceAxis.GetIndex()] : vector[SourceAxis.GetIndex()];
            
            var uScalar = r / (AngleCos - 1);
            var vScalar = s - uScalar * AngleCos;
            
            return Float64Vector4DComposer
                .Create()
                .SetVector(TargetVector, vScalar)
                .AddTerm(SourceAxis.GetIndex(), SourceAxis.IsNegative() ? -uScalar : uScalar)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
        {
            return LinFloat64VectorToAxisRotation4D.Create(
                TargetVector,
                SourceAxis.GetIndex(),
                SourceAxis.IsNegative()
            );
        }
    }
}
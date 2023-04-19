using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation
{
    public sealed class LinFloat64AxisToVectorRotation :
        LinFloat64VectorToVectorRotationBase
    {
        public LinSignedBasisVector SourceAxis { get; }

        public override LinFloat64Vector SourceVector { get; }

        public override LinFloat64Vector TargetOrthogonalVector { get; }

        public override LinFloat64Vector TargetVector { get; }

        public override double AngleCos { get; }

        public override Float64PlanarAngle Angle
            => AngleCos.ArcCos();


        public LinFloat64AxisToVectorRotation(int uAxisIndex, bool uAxisNegative, LinFloat64Vector v)
        {
            Debug.Assert(
                v.IsNearUnit()
            );

            SourceAxis = new LinSignedBasisVector(uAxisIndex, uAxisNegative);
            SourceVector = SourceAxis.ToVector();
            TargetVector = v;

            AngleCos = TargetVector.VectorDot(SourceAxis).Clamp(-1d, 1d);

            Debug.Assert(
                !AngleCos.IsNearMinusOne()
            );

            TargetOrthogonalVector =
                LinFloat64VectorComposer
                    .Create()
                    .SetVector(TargetVector)
                    .SubtractTerm(SourceAxis.Index, SourceAxis.IsNegative ? -AngleCos : AngleCos)
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
    
        public override LinFloat64Vector ProjectOnRotationPlane(LinFloat64Vector vector)
        {
            var xuDot = vector.VectorDot(SourceAxis);
            var xvDot = vector.VectorDot(TargetVector);
            var bivectorNormSquaredInv = 1d / (1d - AngleCos * AngleCos);

            var uScalar = (xuDot - xvDot * AngleCos) * bivectorNormSquaredInv;
            var vScalar = (xvDot - xuDot * AngleCos) * bivectorNormSquaredInv;

            return LinFloat64VectorComposer
                .Create()
                .SetVector(TargetVector, vScalar)
                .AddTerm(SourceAxis.Index, SourceAxis.IsNegative ? -uScalar : uScalar)
                .GetVector();
        }

        public override LinFloat64Vector MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0 && basisIndex < VSpaceDimensions
            );

            var r = TargetOrthogonalVector[basisIndex];
            var s = 
                basisIndex == SourceAxis.Index 
                    ? SourceAxis.Sign.ToFloat64() 
                    : 0d;

            var rsPlus = r + s;
            var rsMinus = r - s;

            return LinFloat64VectorComposer
                .Create()
                .SetVector(TargetVector, -rsMinus)
                .AddTerm(basisIndex, 1d)
                .SubtractTerm(SourceAxis.Index, SourceAxis.IsNegative ? -rsPlus : rsPlus)
                .GetVector();
        }

        public override LinFloat64Vector MapVector(LinFloat64Vector vector)
        {
            var r = vector.VectorDot(TargetOrthogonalVector);
            
            var s = SourceAxis.IsNegative 
                ? -vector[SourceAxis.Index] 
                : vector[SourceAxis.Index];

            var rsPlus = r + s;
            var rsMinus = r - s;

            return LinFloat64VectorComposer
                .Create()
                .SetVector(vector)
                .SubtractVector(TargetVector, rsMinus)
                .SubtractTerm(
                    SourceAxis.Index, 
                    SourceAxis.IsNegative ? -rsPlus : rsPlus
                ).GetVector();
        }
    
        public override LinFloat64Vector MapVectorProjection(LinFloat64Vector vector)
        {
            var r = vector.VectorDot(TargetOrthogonalVector);
            var s = SourceAxis.IsNegative ? -vector[SourceAxis.Index] : vector[SourceAxis.Index];
            
            var uScalar = r / (AngleCos - 1);
            var vScalar = s - uScalar * AngleCos;
            
            return LinFloat64VectorComposer
                .Create()
                .SetVector(TargetVector, vScalar)
                .AddTerm(SourceAxis.Index, SourceAxis.IsNegative ? -uScalar : uScalar)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64SimpleRotationBase GetSimpleVectorRotationInverse()
        {
            return LinFloat64VectorToAxisRotation.Create(
                TargetVector,
                SourceAxis.Index,
                SourceAxis.IsNegative
            );
        }
    }
}
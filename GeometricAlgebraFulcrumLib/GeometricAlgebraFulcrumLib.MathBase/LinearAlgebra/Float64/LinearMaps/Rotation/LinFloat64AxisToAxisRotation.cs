using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Rotation
{
    public sealed class LinFloat64AxisToAxisRotation :
        LinFloat64VectorToVectorRotationBase
    {
        public LinSignedBasisVector SourceAxis { get; }

        public LinSignedBasisVector TargetAxis { get; }

        public override LinFloat64Vector SourceVector { get; }

        public override LinFloat64Vector TargetOrthogonalVector
            => TargetVector;

        public override LinFloat64Vector TargetVector { get; }

        public override double AngleCos
            => 0;

        public override Float64PlanarAngle Angle
            => Float64PlanarAngle.Angle90;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64AxisToAxisRotation(int uAxisIndex, bool uAxisNegative, int vAxisIndex, bool vAxisNegative)
        {
            Debug.Assert(
                uAxisIndex != vAxisIndex
            );

            SourceAxis = new LinSignedBasisVector(uAxisIndex, uAxisNegative);
            TargetAxis = new LinSignedBasisVector(vAxisIndex, vAxisNegative);

            SourceVector = SourceAxis.ToVector();
            TargetVector = TargetAxis.ToVector();
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return SourceAxis.Index != TargetAxis.Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsIdentity()
        {
            return SourceAxis.Index == TargetAxis.Index &&
                   SourceAxis.IsNegative == TargetAxis.IsNegative;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsNearIdentity(double epsilon = 1e-12d)
        {
            return SourceAxis.Index == TargetAxis.Index &&
                   SourceAxis.IsNegative == TargetAxis.IsNegative;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector ProjectOnRotationPlane(LinFloat64Vector vector)
        {
            return LinFloat64VectorComposer
                .Create()
                .SetTerm(SourceAxis.Index, vector[SourceAxis.Index])
                .SetTerm(TargetAxis.Index, vector[TargetAxis.Index])
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapBasisVector(int basisIndex)
        {
            var s = basisIndex == SourceAxis.Index ? SourceAxis.IsNegative ? -1d : 1d : 0d;
            var r = basisIndex == TargetAxis.Index ? TargetAxis.IsNegative ? -1d : 1d : 0d;
            var rsPlus = r + s;
            var rsMinus = r - s;

            return LinFloat64VectorComposer
                .Create()
                .SetTerm(basisIndex, 1d)
                .SubtractTerm(
                    SourceAxis.Index, 
                    SourceAxis.IsNegative ? -rsPlus : rsPlus
                ).SubtractTerm(
                    TargetAxis.Index,
                    TargetAxis.IsNegative ? -rsMinus : rsMinus
                ).GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapVector(LinFloat64Vector vector)
        {
            var r = TargetAxis.IsNegative 
                ? -vector[TargetAxis.Index] 
                : vector[TargetAxis.Index];

            var s = SourceAxis.IsNegative 
                ? -vector[SourceAxis.Index] 
                : vector[SourceAxis.Index];

            var rsPlus = r + s;
            var rsMinus = r - s;

            return LinFloat64VectorComposer
                .Create()
                .SetVector(vector)
                .SubtractTerm(
                    SourceAxis.Index,
                    SourceAxis.IsNegative ? -rsPlus : rsPlus
                ).SubtractTerm(
                    TargetAxis.Index,
                    TargetAxis.IsNegative ? -rsMinus : rsMinus
                ).GetVector();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64Vector MapVectorProjection(LinFloat64Vector vector)
        {
            var r = TargetAxis.IsNegative 
                ? -vector[TargetAxis.Index] 
                : vector[TargetAxis.Index];

            var s = SourceAxis.IsNegative 
                ? -vector[SourceAxis.Index] 
                : vector[SourceAxis.Index];
            
            return LinFloat64VectorComposer
                .Create()
                .SetTerm(
                    SourceAxis.Index, 
                    SourceAxis.IsNegative ? r : -r
                ).SetTerm(
                    TargetAxis.Index,
                    TargetAxis.IsNegative ? -s : s
                ).GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64SimpleRotationBase GetSimpleVectorRotationInverse()
        {
            return new LinFloat64AxisToAxisRotation(
                TargetAxis.Index,
                TargetAxis.IsNegative,
                SourceAxis.Index,
                SourceAxis.IsNegative
            );
        }
    }
}
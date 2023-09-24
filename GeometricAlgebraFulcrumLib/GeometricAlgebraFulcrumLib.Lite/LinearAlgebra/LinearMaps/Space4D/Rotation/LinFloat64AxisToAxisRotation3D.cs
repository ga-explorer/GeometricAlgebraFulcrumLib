using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space4D.Rotation
{
    public sealed class LinFloat64AxisToAxisRotation4D :
        LinFloat64VectorToVectorRotationBase4D
    {
        public LinUnitBasisVector4D SourceAxis { get; }

        public LinUnitBasisVector4D TargetAxis { get; }

        public override Float64Vector4D SourceVector { get; }

        public override Float64Vector4D TargetOrthogonalVector
            => TargetVector;

        public override Float64Vector4D TargetVector { get; }

        public override double AngleCos
            => 0;

        public override Float64PlanarAngle Angle
            => Float64PlanarAngle.Angle90;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64AxisToAxisRotation4D(int uAxisIndex, bool uAxisNegative, int vAxisIndex, bool vAxisNegative)
        {
            Debug.Assert(
                uAxisIndex != vAxisIndex
            );

            SourceAxis = uAxisIndex.ToAxis4D(uAxisNegative);
            TargetAxis = vAxisIndex.ToAxis4D(vAxisNegative);

            SourceVector = SourceAxis.ToTuple4D();
            TargetVector = TargetAxis.ToTuple4D();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return SourceAxis.GetIndex() != TargetAxis.GetIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsIdentity()
        {
            return SourceAxis.GetIndex() == TargetAxis.GetIndex() &&
                   SourceAxis.IsNegative() == TargetAxis.IsNegative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsNearIdentity(double epsilon = 1e-12d)
        {
            return SourceAxis.GetIndex() == TargetAxis.GetIndex() &&
                   SourceAxis.IsNegative() == TargetAxis.IsNegative();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D ProjectOnRotationPlane(Float64Vector4D vector)
        {
            return Float64Vector4DComposer
                .Create()
                .SetTerm(SourceAxis.GetIndex(), vector[SourceAxis.GetIndex()])
                .SetTerm(TargetAxis.GetIndex(), vector[TargetAxis.GetIndex()])
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapBasisVector(int basisIndex)
        {
            var s = basisIndex == SourceAxis.GetIndex() ? SourceAxis.IsNegative() ? -1d : 1d : 0d;
            var r = basisIndex == TargetAxis.GetIndex() ? TargetAxis.IsNegative() ? -1d : 1d : 0d;
            var rsPlus = r + s;
            var rsMinus = r - s;

            return Float64Vector4DComposer
                .Create()
                .SetTerm(basisIndex, 1d)
                .SubtractTerm(
                    SourceAxis.GetIndex(), 
                    SourceAxis.IsNegative() ? -rsPlus : rsPlus
                ).SubtractTerm(
                    TargetAxis.GetIndex(),
                    TargetAxis.IsNegative() ? -rsMinus : rsMinus
                ).GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapVector(IFloat64Vector4D vector)
        {
            var r = TargetAxis.IsNegative()
                ? -vector.GetItem(TargetAxis.GetIndex()) 
                : vector.GetItem(TargetAxis.GetIndex());

            var s = SourceAxis.IsNegative() 
                ? -vector.GetItem(SourceAxis.GetIndex()) 
                : vector.GetItem(SourceAxis.GetIndex());

            var rsPlus = r + s;
            var rsMinus = r - s;

            return Float64Vector4DComposer
                .Create()
                .SetVector(vector)
                .SubtractTerm(
                    SourceAxis.GetIndex(),
                    SourceAxis.IsNegative() ? -rsPlus : rsPlus
                ).SubtractTerm(
                    TargetAxis.GetIndex(),
                    TargetAxis.IsNegative() ? -rsMinus : rsMinus
                ).GetVector();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector4D MapVectorProjection(Float64Vector4D vector)
        {
            var r = TargetAxis.IsNegative() 
                ? -vector[TargetAxis.GetIndex()] 
                : vector[TargetAxis.GetIndex()];

            var s = SourceAxis.IsNegative() 
                ? -vector[SourceAxis.GetIndex()] 
                : vector[SourceAxis.GetIndex()];
            
            return Float64Vector4DComposer
                .Create()
                .SetTerm(
                    SourceAxis.GetIndex(), 
                    SourceAxis.IsNegative() ? r : -r
                ).SetTerm(
                    TargetAxis.GetIndex(),
                    TargetAxis.IsNegative() ? -s : s
                ).GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
        {
            return new LinFloat64AxisToAxisRotation4D(
                TargetAxis.GetIndex(),
                TargetAxis.IsNegative(),
                SourceAxis.GetIndex(),
                SourceAxis.IsNegative()
            );
        }
    }
}
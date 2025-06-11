//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Mutable;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;

//namespace GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Rotation
//{
//    public sealed class AxisToAxisRotation :
//        VectorToVectorRotationLinearMap
//    {
//        public LinBasisVector SourceAxis { get; }

//        public LinBasisVector TargetAxis { get; }

//        public override Float64Tuple SourceVector { get; }

//        public override Float64Tuple TargetOrthogonalVector
//            => TargetVector;

//        public override Float64Tuple TargetVector { get; }

//        public override double AngleCos
//            => 0;

//        public override Float64PlanarAngle Angle
//            => Float64PlanarAngle.Angle90;


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public AxisToAxisRotation(int dimensions, int uAxisIndex, bool uAxisNegative, int vAxisIndex, bool vAxisNegative)
//        {
//            Debug.Assert(
//                uAxisIndex != vAxisIndex
//            );

//            SourceAxis = new LinBasisVector(uAxisIndex, uAxisNegative);
//            TargetAxis = new LinBasisVector(vAxisIndex, vAxisNegative);

//            SourceVector = SourceAxis.ToTuple(dimensions);
//            TargetVector = TargetAxis.ToTuple(dimensions);
//        }

    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsValid()
//        {
//            return SourceAxis.Index != TargetAxis.Index;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsIdentity()
//        {
//            return SourceAxis.Index == TargetAxis.Index &&
//                   SourceAxis.IsNegative == TargetAxis.IsNegative;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsNearIdentity(double zeroEpsilon = Float64Utils.ZeroEpsilon)
//        {
//            return SourceAxis.Index == TargetAxis.Index &&
//                   SourceAxis.IsNegative == TargetAxis.IsNegative;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple ProjectOnRotationPlane(Float64Tuple vector)
//        {
//            var x = vector.ScalarArray;

//            var y = new double[VSpaceDimensions];

//            y[SourceAxis.Index] = x[SourceAxis.Index];
//            y[TargetAxis.Index] = x[TargetAxis.Index];

//            return Float64Tuple.Create(y);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapBasisVector(int basisIndex)
//        {
//            Debug.Assert(basisIndex >= 0 && basisIndex < VSpaceDimensions);

//            var y = new double[VSpaceDimensions];
//            y[basisIndex] = 1d;
        
//            var s = basisIndex == SourceAxis.Index ? SourceAxis.IsNegative ? -1d : 1d : 0d;
//            var r = basisIndex == TargetAxis.Index ? TargetAxis.IsNegative ? -1d : 1d : 0d;
//            var rsPlus = r + s;
//            var rsMinus = r - s;

//            y[SourceAxis.Index] -= 
//                SourceAxis.IsNegative ? -rsPlus : rsPlus;

//            y[TargetAxis.Index] -= 
//                TargetAxis.IsNegative ? -rsMinus : rsMinus;

//            return Float64Tuple.Create(y);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapVector(Float64Tuple vector)
//        {
//            Debug.Assert(vector.Dimensions == VSpaceDimensions);
            
//            var y = vector.GetScalarArrayCopy();

//            var r = TargetAxis.IsNegative ? -y[TargetAxis.Index] : y[TargetAxis.Index];
//            var s = SourceAxis.IsNegative ? -y[SourceAxis.Index] : y[SourceAxis.Index];
//            var rsPlus = r + s;
//            var rsMinus = r - s;

//            y[SourceAxis.Index] -= 
//                SourceAxis.IsNegative ? -rsPlus : rsPlus;

//            y[TargetAxis.Index] -= 
//                TargetAxis.IsNegative ? -rsMinus : rsMinus;

//            return Float64Tuple.Create(y);
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapVectorProjection(Float64Tuple vector)
//        {
//            var x = vector.ScalarArray;

//            var r = TargetAxis.IsNegative ? -x[TargetAxis.Index] : x[TargetAxis.Index];
//            var s = SourceAxis.IsNegative ? -x[SourceAxis.Index] : x[SourceAxis.Index];

//            var y = new double[VSpaceDimensions];

//            y[SourceAxis.Index] = SourceAxis.IsNegative ? r : -r;
//            y[TargetAxis.Index] = TargetAxis.IsNegative ? -s : s;

//            return Float64Tuple.Create(y);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override SimpleRotationLinearMap GetSimpleVectorRotationInverse()
//        {
//            return new AxisToAxisRotation(
//                SourceVector.Dimensions,
//                TargetAxis.Index,
//                TargetAxis.IsNegative,
//                SourceAxis.Index,
//                SourceAxis.IsNegative
//            );
//        }
//    }
//}
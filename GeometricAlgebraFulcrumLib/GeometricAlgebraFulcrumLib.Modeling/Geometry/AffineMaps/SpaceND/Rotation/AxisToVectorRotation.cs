//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Arrays.Float64;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Mutable;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;

//namespace GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Rotation
//{
//    public sealed class AxisToVectorRotation :
//        VectorToVectorRotationLinearMap
//    {
//        public LinBasisVector SourceAxis { get; }

//        public override Float64Tuple SourceVector { get; }

//        public override Float64Tuple TargetOrthogonalVector { get; }

//        public override Float64Tuple TargetVector { get; }

//        public override double AngleCos { get; }

//        public override Float64PlanarAngle Angle
//            => AngleCos.ArcCos();


//        public AxisToVectorRotation(int uAxisIndex, bool uAxisNegative, Float64Tuple v)
//        {
//            Debug.Assert(
//                v.IsNearUnit()
//            );

//            var dimensions = v.ScalarArray.Length;
//            SourceAxis = new LinBasisVector(uAxisIndex, uAxisNegative);
//            SourceVector = SourceAxis.ToTuple(dimensions);
//            TargetVector = v;

//            AngleCos = TargetVector.VectorDot(SourceAxis).Clamp(-1d, 1d);

//            Debug.Assert(
//                !AngleCos.IsNearMinusOne()
//            );

//            TargetOrthogonalVector = Float64Tuple.CreateCopy(TargetVector);
//            TargetOrthogonalVector.ScalarArray[SourceAxis.Index] -= SourceAxis.IsNegative ? -AngleCos : AngleCos;
//            TargetOrthogonalVector.InPlaceScale(1d / (1d + AngleCos));
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsValid()
//        {
//            return 
//                SourceAxis.Index < TargetVector.Dimensions &&
//                TargetVector.IsNearUnit() &&
//                !AngleCos.IsNearMinusOne();
//        }
    
//        public override Float64Tuple ProjectOnRotationPlane(Float64Tuple vector)
//        {
//            var v = TargetVector.ScalarArray;
//            var x = vector.ScalarArray;

//            var uvDot = AngleCos;
//            var xuDot = vector.VectorDot(SourceAxis);
//            var xvDot = x.VectorDot(v);
//            var bivectorNormSquaredInv = 1d / (1d - uvDot * uvDot);

//            var uScalar = (xuDot - xvDot * uvDot) * bivectorNormSquaredInv;
//            var vScalar = (xvDot - xuDot * uvDot) * bivectorNormSquaredInv;

//            var y = new double[VSpaceDimensions];

//            for (var i = 0; i < VSpaceDimensions; i++)
//                y[i] = vScalar * v[i];

//            y[SourceAxis.Index] += SourceAxis.IsNegative ? -uScalar : uScalar;

//            return Float64Tuple.Create(y);
//        }

//        public override Float64Tuple MapBasisVector(int basisIndex)
//        {
//            Debug.Assert(
//                basisIndex >= 0 && basisIndex < VSpaceDimensions
//            );

//            var y = new double[VSpaceDimensions];
//            y[basisIndex] = 1d;

//            var t = TargetOrthogonalVector.ScalarArray;
//            var v = TargetVector.ScalarArray;

//            var r = y.VectorDot(t);
//            var s = basisIndex == SourceAxis.Index ? SourceAxis.IsNegative ? -1d : 1d : 0d;
//            var rsPlus = r + s;
//            var rsMinus = r - s;

//            for (var i = 0; i < VSpaceDimensions; i++)
//                y[i] -= rsMinus * v[i];

//            y[SourceAxis.Index] -= SourceAxis.IsNegative ? -rsPlus : rsPlus;

//            return Float64Tuple.Create(y);
//        }

//        public override Float64Tuple MapVector(Float64Tuple vector)
//        {
//            Debug.Assert(
//                vector.Dimensions == VSpaceDimensions
//            );
        
//            var y = vector.GetScalarArrayCopy();

//            var t = TargetOrthogonalVector.ScalarArray;
//            var v = TargetVector.ScalarArray;

//            var r = y.VectorDot(t);
//            var s = SourceAxis.IsNegative ? -y[SourceAxis.Index] : y[SourceAxis.Index];
//            var rsPlus = r + s;
//            var rsMinus = r - s;

//            for (var i = 0; i < VSpaceDimensions; i++)
//                y[i] -= rsMinus * v[i];

//            y[SourceAxis.Index] -= SourceAxis.IsNegative ? -rsPlus : rsPlus;

//            return Float64Tuple.Create(y);
//        }
    
//        public override Float64Tuple MapVectorProjection(Float64Tuple vector)
//        {
//            var x = vector.ScalarArray;
//            var t = TargetOrthogonalVector.ScalarArray;
//            var v = TargetVector.ScalarArray;

//            var r = x.VectorDot(t);
//            var s = SourceAxis.IsNegative ? -x[SourceAxis.Index] : x[SourceAxis.Index];

//            var y = new double[VSpaceDimensions];

//            var uScalar = r / (AngleCos - 1);
//            var vScalar = s - uScalar * AngleCos;

//            for (var i = 0; i < VSpaceDimensions; i++)
//                y[i] = vScalar * v[i];

//            y[SourceAxis.Index] += SourceAxis.IsNegative ? -uScalar : uScalar;

//            return Float64Tuple.Create(y);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override SimpleRotationLinearMap GetSimpleVectorRotationInverse()
//        {
//            return VectorToAxisRotation.Create(
//                TargetVector,
//                SourceAxis.Index,
//                SourceAxis.IsNegative
//            );
//        }
//    }
//}
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Mutable;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;

//namespace GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Scaling
//{
//    public sealed class AxisDirectionalScaling :
//        DirectionalScalingLinearMap
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static AxisDirectionalScaling Create(int dimensions, double scalingFactor, int scalingBasisIndex)
//        {
//            return new AxisDirectionalScaling(
//                scalingFactor,
//                dimensions,
//                scalingBasisIndex
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static AxisDirectionalScaling Create(double scalingFactor, int dimensions, LinSignedBasisVector scalingAxis)
//        {
//            return new AxisDirectionalScaling(
//                scalingFactor,
//                dimensions,
//                scalingAxis.Index
//            );
//        }


//        public override double ScalingFactor { get; }

//        public LinSignedBasisVector ScalingAxis { get; }

//        public override int VSpaceDimensions { get; }

//        public override Float64Tuple ScalingVector
//            => ScalingAxis.ToTuple(VSpaceDimensions);


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public AxisDirectionalScaling(double factor, int dimensions, int basisIndex)
//        {
//            Debug.Assert(
//                factor.IsNotZero()
//            );

//            VSpaceDimensions = dimensions;
//            ScalingFactor = factor;
//            ScalingAxis = new LinSignedBasisVector(basisIndex, false);
//        }

    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsValid()
//        {
//            return 
//                ScalingVector.IsNearUnit() &&
//                ScalingFactor.IsNotZero();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapBasisVector(int basisIndex)
//        {
//            Debug.Assert(
//                basisIndex >= 0 && basisIndex < VSpaceDimensions
//            );

//            var y = new double[VSpaceDimensions];
//            y[basisIndex] = 1d;

//            if (basisIndex == ScalingAxis.Index)
//                y[basisIndex] += ScalingFactor - 1d;

//            return Float64Tuple.Create(y);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapVector(Float64Tuple vector)
//        {
//            var x = vector.ScalarArray;
//            var s = (ScalingFactor - 1d) * x[ScalingAxis.Index];

//            var y = vector.GetScalarArrayCopy();

//            y[ScalingAxis.Index] += s;

//            return Float64Tuple.Create(y);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override IDirectionalScalingLinearMap GetDirectionalScalingInverse()
//        {
//            return GetAxisScalingInverse();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public AxisDirectionalScaling GetAxisScalingInverse()
//        {
//            return new AxisDirectionalScaling(
//                1d / ScalingFactor,
//                VSpaceDimensions,
//                ScalingAxis.Index
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override VectorDirectionalScaling ToVectorDirectionalScaling()
//        {
//            return VectorDirectionalScaling.Create(
//                1d, 
//                ScalingVector
//            );
//        }
//    }
//}
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.SpaceND.Scaling;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;
//using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
//using MathNet.Numerics.LinearAlgebra;

//namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.SpaceND.Reflection
//{
//    public sealed class HyperPlaneAxisReflection :
//        ReflectionLinearMap,
//        IHyperPlaneNormalReflectionLinearMap
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static HyperPlaneAxisReflection Create(int dimensions, int axisIndex)
//        {
//            return new HyperPlaneAxisReflection(dimensions, axisIndex);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static HyperPlaneAxisReflection Create(int dimensions, LinSignedBasisVector axis)
//        {
//            return new HyperPlaneAxisReflection(
//                dimensions,
//                axis.Index
//            );
//        }


//        public LinSignedBasisVector ReflectionNormalAxis { get; }

//        public Float64Tuple ReflectionNormal 
//            => ReflectionNormalAxis.ToTuple(VSpaceDimensions);

//        public override int VSpaceDimensions { get; }

//        public override bool SwapsHandedness 
//            => true;

//        public double ScalingFactor 
//            => -1d;

//        public Float64Tuple ScalingVector 
//            => ReflectionNormalAxis.ToTuple(VSpaceDimensions);


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private HyperPlaneAxisReflection(int dimensions, int basisIndex)
//        {
//            VSpaceDimensions = dimensions;
//            ReflectionNormalAxis = new LinSignedBasisVector(basisIndex, false);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsValid()
//        {
//            return true;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsIdentity()
//        {
//            return false;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsNearIdentity(double epsilon = 1E-12)
//        {
//            return false;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapBasisVector(int basisIndex)
//        {
//            Debug.Assert(basisIndex >= 0 && basisIndex < VSpaceDimensions);

//            var y = new double[VSpaceDimensions];
//            y[basisIndex] = ReflectionNormalAxis.Index == basisIndex ? -1d : 1d;

//            return Float64Tuple.Create(y);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapVector(Float64Tuple vector)
//        {
//            var y = vector.GetScalarArrayCopy();

//            y[ReflectionNormalAxis.Index] = -y[ReflectionNormalAxis.Index];

//            return Float64Tuple.Create(y);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override ReflectionLinearMap GetReflectionLinearMapInverse()
//        {
//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Matrix<double> ToMatrix()
//        {
//            return Matrix<double>
//                .Build
//                .DenseOfArray(ToArray());
//        }

//        public override double[,] ToArray()
//        {
//            var array = new double[VSpaceDimensions, VSpaceDimensions];

//            for (var j = 0; j < VSpaceDimensions; j++)
//                array[j, j] = 1d;

//            var i = ReflectionNormalAxis.Index;
//            array[i, i] = -1d;

//            return array;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneAxisReflection GetHyperPlaneAxisReflectionInverse()
//        {
//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IHyperPlaneNormalReflectionLinearMap GetHyperPlaneNormalReflectionLinearMapInverse()
//        {
//            return this;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflection ToHyperPlaneNormalReflection()
//        {
//            return HyperPlaneNormalReflection.Create(ReflectionNormal);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IDirectionalScalingLinearMap GetDirectionalScalingInverse()
//        {
//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
//        {
//            return HyperPlaneNormalReflectionSequence.Create(this);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public VectorDirectionalScaling ToVectorDirectionalScaling()
//        {
//            return VectorDirectionalScaling.Create(
//                -1d, 
//                ReflectionNormal
//            );
//        }
//    }
//}
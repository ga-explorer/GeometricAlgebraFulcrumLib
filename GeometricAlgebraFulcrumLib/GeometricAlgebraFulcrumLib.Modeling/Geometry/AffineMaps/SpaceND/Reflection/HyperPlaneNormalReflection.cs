//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Arrays.Float64;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Scaling;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Mutable;

//namespace GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Reflection
//{
//    public sealed class HyperPlaneNormalReflection :
//        ReflectionLinearMap,
//        IHyperPlaneNormalReflectionLinearMap
//    {
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static HyperPlaneNormalReflection Create(params double[] reflectionNormal)
//        {
//            return new HyperPlaneNormalReflection(
//                reflectionNormal.CreateTuple()
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public static HyperPlaneNormalReflection Create(Float64Tuple reflectionNormal)
//        {
//            return new HyperPlaneNormalReflection(
//                reflectionNormal
//            );
//        }


//        public Float64Tuple ReflectionNormal { get; }

//        public override int VSpaceDimensions 
//            => ReflectionNormal.Dimensions;

//        public override bool SwapsHandedness 
//            => true;

//        public double ScalingFactor 
//            => -1d;

//        public Float64Tuple ScalingVector 
//            => ReflectionNormal;


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private HyperPlaneNormalReflection(Float64Tuple vector)
//        {
//            Debug.Assert(vector.GetVectorNormSquared().IsNearOne());

//            ReflectionNormal = vector;
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsValid()
//        {
//            return ReflectionNormal.IsValid();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsIdentity()
//        {
//            return false;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
//        {
//            return false;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapBasisVector(int basisIndex)
//        {
//            Debug.Assert(basisIndex >= 0 && basisIndex < VSpaceDimensions);

//            var y = new double[VSpaceDimensions];

//            var u = ReflectionNormal.ScalarArray;
//            var s = -2d * u[basisIndex];

//            for (var i = 0; i < VSpaceDimensions; i++)
//                y[i] = s * u[i];

//            y[basisIndex] += 1d;

//            return Float64Tuple.Create(y);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override Float64Tuple MapVector(Float64Tuple vector)
//        {
//            var y = vector.GetScalarArrayCopy();

//            var x = vector.ScalarArray;
//            var u = ReflectionNormal.ScalarArray;
//            var s = -2d * x.VectorDot(u);

//            for (var i = 0; i < VSpaceDimensions; i++)
//                y[i] += s * u[i];

//            return Float64Tuple.Create(y);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override ReflectionLinearMap GetReflectionLinearMapInverse()
//        {
//            return this;
//        }
    
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public HyperPlaneNormalReflection GetHyperPlaneNormalReflectionInverse()
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
//            return this;
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
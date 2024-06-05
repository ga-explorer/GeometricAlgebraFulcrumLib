//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Arrays.Float64;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Reflection;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Mutable;

//namespace GeometricAlgebraFulcrumLib.Algebra.BasicMath.Maps.SpaceND.Rotation
//{
//    public abstract class VectorToVectorRotationLinearMap :
//        SimpleRotationLinearMap,
//        ISimpleVectorToVectorRotation
//    {
//        public override int VSpaceDimensions
//            => SourceVector.ScalarArray.Length;

//        /// <summary>
//        /// The unit vector where the rotation starts
//        /// </summary>
//        public abstract Float64Tuple SourceVector { get; }

//        /// <summary>
//        /// A scaled version of the orthogonal component (rejection) of
//        /// TargetVector relative to SourceVector
//        /// </summary>
//        public abstract Float64Tuple TargetOrthogonalVector { get; }

//        /// <summary>
//        /// The unit vector where the rotation ends
//        /// </summary>
//        public abstract Float64Tuple TargetVector { get; }

//        /// <summary>
//        /// The dot product of TargetVector and SourceVector
//        /// </summary>
//        public abstract double AngleCos { get; }

//        /// <summary>
//        /// The smallest angle between TargetVector and SourceVector
//        /// </summary>
//        public abstract Float64PlanarAngle Angle { get; }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsIdentity()
//        {
//            return (AngleCos - 1d).IsZero();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override bool IsNearIdentity(double epsilon = 1e-12d)
//        {
//            return (AngleCos - 1d).IsNearZero(epsilon);
//        }


//        public abstract Float64Tuple ProjectOnRotationPlane(Float64Tuple vector);

//        public abstract Float64Tuple MapVectorProjection(Float64Tuple vector);

//        //public virtual Float64Tuple MapVectorProjection(Float64Tuple vector)
//        //{
//        //    return MapVector(ProjectOnRotationPlane(vector));
//        //}

//        /// <summary>
//        /// Construct middle unit vector between SourceVector and TargetVector
//        /// </summary>
//        /// <returns></returns>
//        public Float64Tuple GetMiddleUnitVector()
//        {
//            var u = SourceVector.ScalarArray;
//            var v = TargetVector.ScalarArray;

//            var wLengthInv = 1d / (2d + 2d * AngleCos).Sqrt();
//            var w = new double[VSpaceDimensions];

//            for (var i = 0; i < VSpaceDimensions; i++)
//                w[i] = (u[i] + v[i]) * wLengthInv;

//            return Float64Tuple.Create(w);
//        }

//        public Pair<HyperPlaneNormalReflection> GetHyperPlaneReflectionPair()
//        {
//            var u = SourceVector.ScalarArray;
//            var v = TargetVector.ScalarArray;

//            var wLengthInv = 1d / (2d + 2d * AngleCos).Sqrt();
//            var w = new double[VSpaceDimensions];

//            for (var i = 0; i < VSpaceDimensions; i++)
//                w[i] = (u[i] + v[i]) * wLengthInv;

//            return new Pair<HyperPlaneNormalReflection>(
//                HyperPlaneNormalReflection.Create(u),
//                HyperPlaneNormalReflection.Create(w)
//            );
//        }

//        public Float64Tuple GetRotatedSourceVector(Float64PlanarAngle angle1)
//        {
//            var u = SourceVector.ScalarArray;
        
//            // Create a unit normal to u in the u-v rotational plane
//            var v = TargetOrthogonalVector.ScalarArray;
//            v.VectorTimesInPlace(1d / v.GetVectorNorm());

//            // Compute a rotated version of u in the u-v rotational plane by the given angle
//            var cos1 = angle1.Cos();
//            var sin1 = angle1.Sin();

//            var u1 = new double[VSpaceDimensions];

//            for (var i = 0; i < VSpaceDimensions; i++) 
//                u1[i] = u[i] * cos1 + v[i] * sin1;

//            return u1.CreateTuple();
//        }

//        public Pair<Float64Tuple> GetRotatedSourceVectorPair(Float64PlanarAngle angle1, Float64PlanarAngle angle2)
//        {
//            var u = SourceVector.ScalarArray;
        
//            // Create a unit normal to u in the u-v rotational plane
//            var v = TargetOrthogonalVector.ScalarArray;
//            v.VectorTimesInPlace(1d / v.GetVectorNorm());

//            // Compute a rotated version of u in the u-v rotational plane by the given angle
//            var cos1 = angle1.Cos();
//            var cos2 = angle2.Cos();

//            var sin1 = angle1.Sin();
//            var sin2 = angle2.Sin();

//            var u1 = new double[VSpaceDimensions];
//            var u2 = new double[VSpaceDimensions];

//            for (var i = 0; i < VSpaceDimensions; i++)
//            {
//                var x = u[i];
//                var y = v[i];

//                u1[i] = x * cos1 + y * sin1;
//                u2[i] = x * cos2 + y * sin2;
//            }

//            return new Pair<Float64Tuple>(
//                u1.CreateTuple(),
//                u2.CreateTuple()
//            );
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
//        {
//            var reflection = 
//                HyperPlaneNormalReflectionSequence.Create(VSpaceDimensions);

//            var (r1, r2) = 
//                GetHyperPlaneReflectionPair();

//            reflection
//                .AppendMap(r1)
//                .AppendMap(r2);

//            return reflection;
//        }
//    }
//}
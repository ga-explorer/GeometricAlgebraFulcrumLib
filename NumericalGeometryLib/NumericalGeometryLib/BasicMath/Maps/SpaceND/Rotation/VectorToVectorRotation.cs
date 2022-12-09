using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation
{
    public sealed class VectorToVectorRotation :
        VectorToVectorRotationLinearMap
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorToVectorRotation CreateIdentity(int dimensions)
        {
            var u = Float64Tuple.CreateBasis(dimensions, 0);

            return new VectorToVectorRotation(u, u);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorToVectorRotation CreateIdentity(Float64Tuple sourceVector)
        {
            return new VectorToVectorRotation(sourceVector, sourceVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorToVectorRotation Create(Float64Tuple sourceVector, Float64Tuple targetVector)
        {
            return new VectorToVectorRotation(sourceVector, targetVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorToVectorRotation Create(Float64Tuple sourceVector, Float64Tuple targetVector, PlanarAngle angle)
        {
            if (angle.Radians.IsNearZero())
                return new VectorToVectorRotation(sourceVector, sourceVector);

            // Compute a rotated version of v in the u-v rotational plane by the given angle
            var vFinal = sourceVector.RotateToUnitVector(targetVector, angle);

            return new VectorToVectorRotation(sourceVector, vFinal);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorToVectorRotation CreateFromComplexEigenPair(Complex eigenValue, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenVector)
        {
            var angle = Math.Atan2(
                eigenValue.Imaginary,
                eigenValue.Real
            );

            //TODO: Why is this the correct one, but not the reverse??!!
            var u = eigenVector.Imaginary().ToArray().CreateTuple(true);
            var v = eigenVector.Real().ToArray().CreateTuple(true);

            return VectorToVectorRotation.Create(u, v, angle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorToVectorRotation CreateFromComplexEigenPair(double realValue, double imagValue, double[] realVector, double[] imagVector)
        {
            var angle = Math.Atan2(
                imagValue,
                realValue
            );

            //TODO: Why is this the correct one, but not the reverse??!!
            var u = Float64Tuple.Create(imagVector).InPlaceNormalize();
            var v = Float64Tuple.Create(realVector).InPlaceNormalize();

            return VectorToVectorRotation.Create(u, v, angle);
        }


        public override Float64Tuple SourceVector { get; }

        public override Float64Tuple TargetOrthogonalVector { get; }

        public override Float64Tuple TargetVector { get; }

        public override double AngleCos { get; }

        public override PlanarAngle Angle
            => AngleCos.ArcCos();


        internal VectorToVectorRotation(Triplet<double[]> rotationVectors)
        {
            var (sourceVector, targetOrthogonalVector, targetVector) = 
                rotationVectors;

            Debug.Assert(
                sourceVector.Length == targetVector.Length &&
                sourceVector.GetVectorNormSquared().IsNearOne() &&
                targetVector.GetVectorNormSquared().IsNearOne()
            );

            SourceVector = Float64Tuple.Create(sourceVector);
            TargetVector = Float64Tuple.Create(targetVector);

            AngleCos = TargetVector.VectorDot(SourceVector).Clamp(-1d, 1d);

            Debug.Assert(
                !AngleCos.IsNearMinusOne()
            );

            TargetOrthogonalVector = Float64Tuple.Create(targetOrthogonalVector);

            Debug.Assert(
                (TargetOrthogonalVector - (TargetVector - AngleCos * SourceVector) / (1d + AngleCos)).GetVectorNormSquared().IsNearZero()
            );
        }

        private VectorToVectorRotation(Float64Tuple sourceVector, Float64Tuple targetVector)
        {
            Debug.Assert(
                sourceVector.Dimensions == targetVector.Dimensions &&
                sourceVector.IsNearUnit() &&
                targetVector.IsNearUnit()
            );

            SourceVector = sourceVector;
            TargetVector = targetVector;

            AngleCos = TargetVector.VectorDot(SourceVector).Clamp(-1d, 1d);

            Debug.Assert(
                !AngleCos.IsNearMinusOne()
            );

            TargetOrthogonalVector = (TargetVector - AngleCos * SourceVector) / (1d + AngleCos);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return
                SourceVector.Dimensions == TargetVector.Dimensions &&
                SourceVector.IsNearUnit() &&
                TargetVector.IsNearUnit() &&
                !AngleCos.IsNearMinusOne();
        }
        
        public override Float64Tuple ProjectOnRotationPlane(Float64Tuple vector)
        {
            var u = SourceVector.ScalarArray;
            var v = TargetVector.ScalarArray;
            var x = vector.ScalarArray;

            var uvDot = AngleCos;
            var (xuDot, xvDot) = x.VectorDot(u, v);
            var bivectorNormSquaredInv = 1d / (1d - uvDot * uvDot);

            var uScalar = (xuDot - xvDot * uvDot) * bivectorNormSquaredInv;
            var vScalar = (xvDot - xuDot * uvDot) * bivectorNormSquaredInv;

            var y = new double[Dimensions];

            for (var i = 0; i < Dimensions; i++)
                y[i] = uScalar * u[i] + vScalar * v[i];

            return Float64Tuple.Create(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Tuple MapVectorBasis(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0 && basisIndex < Dimensions
            );

            //var r = vector.VectorDot(TargetOrthogonalVector);
            //var s = vector.VectorDot(SourceVector);

            //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

            var y = new double[Dimensions];
            
            y[basisIndex] = 1d;

            var u = SourceVector.ScalarArray;
            var t = TargetOrthogonalVector.ScalarArray;
            var v = TargetVector.ScalarArray;

            var r = t[basisIndex];
            var s = u[basisIndex];
            var rsPlus = r + s;
            var rsMinus = r - s;

            for (var i = 0; i < Dimensions; i++)
                y[i] -= rsPlus * u[i] + rsMinus * v[i];

            return Float64Tuple.Create(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Tuple MapVector(Float64Tuple vector)
        {
            Debug.Assert(
                vector.Dimensions == Dimensions
            );
            
            //var r = vector.VectorDot(TargetOrthogonalVector);
            //var s = vector.VectorDot(SourceVector);

            //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

            var y = vector.GetScalarArrayCopy();

            var u = SourceVector.ScalarArray;
            var t = TargetOrthogonalVector.ScalarArray;
            var v = TargetVector.ScalarArray;

            var (r, s) = y.VectorDot(t, u);
            var rsPlus = r + s;
            var rsMinus = r - s;

            for (var i = 0; i < Dimensions; i++)
                y[i] -= rsPlus * u[i] + rsMinus * v[i];

            return Float64Tuple.Create(y);
        }

        public override Float64Tuple MapVectorProjection(Float64Tuple vector)
        {
            var x = vector.ScalarArray;
            var u = SourceVector.ScalarArray;
            var t = TargetOrthogonalVector.ScalarArray;
            var v = TargetVector.ScalarArray;

            var (r, s) = x.VectorDot(t, u);

            var y = new double[Dimensions];

            var uScalar = r / (AngleCos - 1d);
            var vScalar = s - uScalar * AngleCos;

            for (var i = 0; i < Dimensions; i++)
                y[i] = uScalar * u[i] + vScalar * v[i];

            return Float64Tuple.Create(y);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorToVectorRotation GetVectorToVectorRotationInverse()
        {
            return new VectorToVectorRotation(
                TargetVector,
                SourceVector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override SimpleRotationLinearMap GetSimpleVectorRotationInverse()
        {
            return GetVectorToVectorRotationInverse();
        }
    }
}

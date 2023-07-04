using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation
{
    public sealed class LinFloat64VectorToVectorRotation :
        LinFloat64PlanarRotation
    {
        public static LinFloat64VectorToVectorRotation Identity { get; }
            = new LinFloat64VectorToVectorRotation(
                0.CreateLinVector(), 
                1.CreateLinVector(), 
                Float64PlanarAngle.Angle0
            );
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation CreateFromSpanningVectors(Float64Vector spanningVector1, Float64Vector spanningVector2, Float64PlanarAngle rotationAngle)
        {
            Debug.Assert(
                !spanningVector1.IsNearParallelTo(spanningVector2)
            );

            var basisVector1 = 
                spanningVector1.ToUnitVector();

            var basisVector2 = 
                spanningVector2.IsNearOppositeToUnit(basisVector1)
                    ? basisVector1.GetUnitNormal()
                    : spanningVector2.RejectOnUnitVector(basisVector1).ToUnitVector();
            
            return new LinFloat64VectorToVectorRotation(
                basisVector1, 
                basisVector2, 
                rotationAngle
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation CreateFromOrthogonalVectors(Float64Vector spanningVector1, Float64Vector spanningVector2, Float64PlanarAngle rotationAngle)
        {
            var basisVector1 = spanningVector1.DivideByENorm();
            var basisVector2 = spanningVector2.DivideByENorm();

            return new LinFloat64VectorToVectorRotation(
                basisVector1, 
                basisVector2, 
                rotationAngle
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation CreateFromOrthonormalVectors(Float64Vector basisVector1, Float64Vector basisVector2, Float64PlanarAngle rotationAngle)
        {
            return new LinFloat64VectorToVectorRotation(
                basisVector1, 
                basisVector2, 
                rotationAngle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation CreateFromRotatedVector(Float64Vector vector, Float64Vector rotatedVector, bool useShortArc = true)
        {
            var basisVector1 = 
                vector.ToUnitVector();
        
            var rotationAngle = 
                useShortArc
                    ? rotatedVector.GetAngleWithUnit(basisVector1)
                    : Float64PlanarAngle.Angle360 - rotatedVector.GetAngleWithUnit(basisVector1);

            if (rotationAngle.IsNearStraight() || rotationAngle.IsNearZeroOrFullRotation())
                return new LinFloat64VectorToVectorRotation(
                    basisVector1, 
                    basisVector1.GetUnitNormal(), 
                    rotationAngle
                );

            var basisVector2 = 
                useShortArc
                    ? rotatedVector.RejectOnUnitVector(basisVector1).ToUnitVector() 
                    : rotatedVector.RejectOnUnitVector(basisVector1).ToNegativeUnitVector();
        
            return new LinFloat64VectorToVectorRotation(
                basisVector1, 
                basisVector2, 
                rotationAngle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation CreateFromComplexEigenPair(Complex eigenValue, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenVector)
        {
            var rotationAngle = Math.Atan2(
                eigenValue.Imaginary,
                eigenValue.Real
            ).RadiansToAngle();

            //TODO: Why is this the correct one, but not the reverse??!!
            var basisVector1 = eigenVector.Imaginary().ToArray().CreateUnitLinVector();
            var basisVector2 = eigenVector.Real().ToArray().CreateUnitLinVector();

            return new LinFloat64VectorToVectorRotation(
                basisVector1, 
                basisVector2, 
                rotationAngle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64VectorToVectorRotation CreateFromComplexEigenPair(double realValue, double imagValue, double[] realVector, double[] imagVector)
        {
            var rotationAngle = Math.Atan2(
                imagValue,
                realValue
            ).RadiansToAngle();

            //TODO: Why is this the correct one, but not the reverse??!!
            var basisVector1 = imagVector.CreateUnitLinVector();
            var basisVector2 = realVector.CreateUnitLinVector();

            return new LinFloat64VectorToVectorRotation(
                basisVector1, 
                basisVector2, 
                rotationAngle
            );
        }


        public override Float64Vector BasisVector1 { get; }

        public override Float64Vector BasisVector2 { get; }
        
        
        private LinFloat64VectorToVectorRotation(Float64Vector basisVector1, Float64Vector basisVector2, Float64PlanarAngle rotationAngle)
            : base(rotationAngle)
        {
            BasisVector1 = basisVector1;
            BasisVector2 = basisVector2;
            
            Debug.Assert(IsValid());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Pair<double> BasisESp(int axisIndex)
        {
            return new Pair<double>(
                BasisVector1[axisIndex],
                BasisVector2[axisIndex]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Pair<double> BasisESp(ILinSignedBasisVector axis)
        {
            return new Pair<double>(
                BasisVector1.GetComponent(axis),
                BasisVector2.GetComponent(axis)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Pair<double> BasisESp(Float64Vector vector)
        {
            return new Pair<double>(
                vector.ESp(BasisVector1),
                vector.ESp(BasisVector2)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector MapBasisVector(int basisIndex)
        {
            Debug.Assert(
                basisIndex >= 0
            );

            // Compute the projection components of the given vector on
            // the orthonormal basis vectors defining the plane of rotation
            var (vpx, vpy) = BasisESp(basisIndex);
            
            var rotationAngleCosMinusOne = RotationAngleCos - 1d;
            
            // Compute the scalar factors of u1, u2
            var u1Scalar = rotationAngleCosMinusOne * vpx - RotationAngleSin * vpy;
            var u2Scalar = rotationAngleCosMinusOne * vpy + RotationAngleSin * vpx;

            // The final rotated vector
            return Float64VectorComposer
                .Create()
                .SetVector(BasisVector1, u1Scalar)
                .AddVector(BasisVector2, u2Scalar)
                .AddTerm(basisIndex, 1d)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector MapVector(Float64Vector vector)
        {
            // Compute the projection components of the given vector on
            // the orthonormal basis vectors defining the plane of rotation
            var (vpx, vpy) = BasisESp(vector);
        
            var rotationAngleCosMinusOne = RotationAngleCos - 1d;

            // Compute the scalar factors of u1, u2
            var u1Scalar = rotationAngleCosMinusOne * vpx - RotationAngleSin * vpy;
            var u2Scalar = rotationAngleCosMinusOne * vpy + RotationAngleSin * vpx;

            // The final rotated vector
            return Float64VectorComposer
                .Create()
                .SetVector(vector)
                .AddVector(BasisVector1, u1Scalar)
                .AddVector(BasisVector2, u2Scalar)
                .GetVector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector MapBasisVector1()
        {
            return Float64VectorComposer
                .Create()
                .SetVector(BasisVector1, RotationAngleCos)
                .AddVector(BasisVector2, RotationAngleSin)
                .GetVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector MapBasisVector2()
        {
            return Float64VectorComposer
                .Create()
                .SetVector(BasisVector1, -RotationAngleSin)
                .AddVector(BasisVector2, RotationAngleCos)
                .GetVector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector MapBasisVector1(Float64PlanarAngle rotationAngle)
        {
            return Float64VectorComposer
                .Create()
                .SetVector(BasisVector1, rotationAngle.Cos())
                .AddVector(BasisVector2, rotationAngle.Sin())
                .GetVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector MapBasisVector2(Float64PlanarAngle rotationAngle)
        {
            return Float64VectorComposer
                .Create()
                .SetVector(BasisVector1, -rotationAngle.Sin())
                .AddVector(BasisVector2, rotationAngle.Cos())
                .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector GetVectorProjection(Float64Vector vector)
        {
            var (vpx, vpy) = BasisESp(vector);

            return Float64VectorComposer
                .Create()
                .SetVector(BasisVector1, vpx)
                .AddVector(BasisVector2, vpy)
                .GetVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector GetVectorRejection(Float64Vector vector)
        {
            var (vpx, vpy) = BasisESp(vector);

            return Float64VectorComposer
                .Create()
                .SetVector(vector)
                .AddVector(BasisVector1, -vpx)
                .AddVector(BasisVector2, -vpy)
                .GetVector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64PlanarRotation GetInversePlanarRotation()
        {
            return new LinFloat64VectorToVectorRotation(
                BasisVector2,
                BasisVector1,
                RotationAngle
            );
        }

    }
}

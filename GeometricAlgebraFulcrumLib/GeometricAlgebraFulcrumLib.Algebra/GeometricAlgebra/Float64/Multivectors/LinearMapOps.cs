using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEuclideanRotor()
        {
            return IsEven() && (EGp(Reverse()) - 1d).IsZero;
        }


        /// <summary>
        /// Create a pure rotor from its scalar and bivector parts
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureRotor ToEuclideanPureRotor()
        {
            return XGaFloat64PureRotor.Create(
                GetScalarPart().ScalarValue,
                GetBivectorPart()
            );
        }

        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade
        /// is computed automatically
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor ToPureScalingRotor()
        {
            var sign = SpSquared().Sign();

            if (sign.IsZero)
                return ToPureScalingRotor(IntegerSign.Zero);

            return sign.IsPositive
                ? ToPureScalingRotor(IntegerSign.Positive)
                : ToPureScalingRotor(IntegerSign.Negative);
        }

        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade
        /// is given by the user
        /// </summary>
        /// <param name="bladeSignature"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor ToPureScalingRotor(IntegerSign bladeSignature)
        {
            if (bladeSignature.IsZero)
            {
                var mv = Processor.Scalar(1);

                return XGaFloat64PureScalingRotor.Create(mv, mv);
            }

            if (bladeSignature.IsNegative)
            {
                var alpha = (-SpSquared()).Sqrt();
                var mv = Times(alpha.Sin() / alpha) + alpha.Cos();

                return XGaFloat64PureScalingRotor.Create(mv);
            }
            else
            {
                var alpha = SpSquared().Sqrt();
                var mv = Times(alpha.Sinh() / alpha) + alpha.Cosh();

                return XGaFloat64PureScalingRotor.Create(mv);
            }
        }

        /// <summary>
        /// Create a pure rotor from its scalar and bivector parts
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor ToEuclideanPureScalingRotor()
        {
            return XGaFloat64PureScalingRotor.Create(this);
        }

        ///// <summary>
        ///// Create a pure Euclidean rotor that rotates the given source basis vector
        ///// into the target vector
        ///// </summary>
        ///// <param name="sourceBasisVectorIndex"></param>
        ///// <param name="assumeUnitVector"></param>
        ///// <returns></returns>
        //public XGaFloat64PureScalingRotor CreatePureScalingRotorFromBasisVector(int sourceBasisVectorIndex, bool assumeUnitVector = false)
        //{
        //    var processor = Metric;
        //    var k = sourceBasisVectorIndex;

        //    var v =
        //        assumeUnitVector
        //            ? this
        //            : Divide(ENorm());

        //    var ek = processor.BasisVector(k).ToKVector();

        //    var vk = v.Scalar(k);
        //    var vk1 = 1 + vk;

        //    return XGaFloat64PureScalingRotor.Create(
        //        (vk1 / 2).Sqrt() + v.Op(ek) / (2 * vk1).Sqrt()
        //    );
        //}

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <param name="sourceBasisVectorIndex"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor CreatePureScalingRotorFromBasisVector(int sourceBasisVectorIndex, bool assumeUnitVector = false)
        {
            var processor = Metric;
            var k = sourceBasisVectorIndex;
            var vNorm =
                assumeUnitVector
                    ? 1d
                    : ENorm().ScalarValue;

            var v =
                assumeUnitVector
                    ? this
                    : this / vNorm;

            var ek = processor.BasisVector(k).ToKVector();

            var vk = v.Scalar(k);
            var vk1 = 1 + vk;

            return XGaFloat64PureScalingRotor.Create(
                (vNorm * vk1 / 2).Sqrt() + v.Op(ek) * (vNorm / (vk1 * 2)).Sqrt()
            );
        }

    }

    public sealed partial class XGaFloat64Vector
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64DiagonalOutermorphism ToDiagonalAutomorphism()
        {
            return new XGaFloat64DiagonalOutermorphism(this);
        }
    }

    public sealed partial class XGaFloat64Bivector
    {
        
        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade
        /// is computed automatically using the given processor which must
        /// be of numerical type
        /// </summary>
        /// <returns></returns>
        public new XGaFloat64PureScalingRotor ToPureScalingRotor()
        {
            var bladeSignature = SpSquared();

            if (bladeSignature.IsNearZero())
                return XGaFloat64PureScalingRotor.Create(
                    1d,
                    this
                );

            if (bladeSignature < 0)
            {
                var alpha = (-bladeSignature).Sqrt();
                var scalar = alpha.Cos().ScalarValue;
                var bivector = alpha.Sin() / alpha * this;

                return XGaFloat64PureScalingRotor.Create(
                    scalar,
                    bivector
                );
            }
            else
            {
                var alpha = bladeSignature.Sqrt();
                var scalar = alpha.Cosh().ScalarValue;
                var bivector = alpha.Sinh() / alpha * this;

                return XGaFloat64PureScalingRotor.Create(
                    scalar,
                    bivector
                );
            }
        }

        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade
        /// is given by the user
        /// </summary>
        /// <param name="bladeSignatureSign"></param>
        /// <returns></returns>
        public new XGaFloat64PureScalingRotor ToPureScalingRotor(IntegerSign bladeSignatureSign)
        {
            if (bladeSignatureSign.IsZero)
                return XGaFloat64PureScalingRotor.Create(
                    1d,
                    this
                );

            var bladeSignature = SpSquared();

            if (bladeSignatureSign.IsNegative)
            {
                var alpha = (-bladeSignature).Sqrt();
                var scalar = alpha.Cos().ScalarValue;
                var bivector = alpha.Sin() / alpha * this;

                return XGaFloat64PureScalingRotor.Create(
                    scalar,
                    bivector
                );
            }
            else
            {
                var alpha = bladeSignature.Sqrt();
                var scalar = alpha.Cosh().ScalarValue;
                var bivector = alpha.Sinh() / alpha * this;

                return XGaFloat64PureScalingRotor.Create(
                    scalar,
                    bivector
                );
            }
        }

        /// <summary>
        /// Create a simple rotor from an angle and a 2-blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        public XGaFloat64PureRotor ToPureRotor(LinFloat64Angle rotationAngle)
        {
            var (cosHalfAngle, sinHalfAngle) = 
                rotationAngle.HalfPolarAngle();

            var rotationBladeScalar =
                sinHalfAngle / (-ESpSquared()).Sqrt();

            return XGaFloat64PureRotor.Create(
                cosHalfAngle,
                rotationBladeScalar * this
            );
        }

        /// <summary>
        /// Create a simple rotor from an angle and a 2-blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor ToEuclideanPureScalingRotor(LinFloat64Angle rotationAngle)
        {
            var (sinHalfAngle, cosHalfAngle) =
                (0.5d * rotationAngle.RadiansValue).SinCos();

            var bivectorPart =
                sinHalfAngle / (-ESpSquared()).Sqrt() * this;

            return XGaFloat64PureScalingRotor.Create(
                cosHalfAngle + bivectorPart,
                cosHalfAngle - bivectorPart
            );
        }

    }
}

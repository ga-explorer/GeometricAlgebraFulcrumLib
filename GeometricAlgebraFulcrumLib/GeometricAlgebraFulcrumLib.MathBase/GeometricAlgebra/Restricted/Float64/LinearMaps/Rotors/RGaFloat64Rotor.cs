using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors
{
    public class RGaFloat64Rotor 
        : RGaFloat64RotorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Rotor CreateIdentity(RGaFloat64Processor metric)
        {
            return new RGaFloat64Rotor(
                metric.CreateScalar(1d)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RGaFloat64Rotor Create(RGaFloat64Multivector mv)
        {
            return new RGaFloat64Rotor(mv);
        }

        public static RGaFloat64Rotor CreateEuclideanPureRotor(RGaFloat64Vector sourceVector, RGaFloat64Vector targetVector)
        {
            var norm1 = sourceVector.ENorm();
            var norm2 = targetVector.ENorm();
            var cosAngle = sourceVector.ESp(targetVector) / (norm1 * norm2);

            if (cosAngle.IsOne)
                return CreateIdentity(sourceVector.Processor);
            
            var rotationBlade = 
                cosAngle.IsMinusOne
                    ? sourceVector.GetNormalVector().Op(sourceVector)
                    : targetVector.Op(sourceVector);
                
            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();
            
            var rotorStorage = 
                cosHalfAngle.ScalarValue + sinHalfAngle * unitRotationBlade;
            
            //rotor.IsSimpleRotor();

            return new RGaFloat64Rotor(rotorStorage);
        }

        /// <summary>
        /// Create a simple rotor from an angle and a blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static RGaFloat64Rotor CreateEuclideanPureRotor(double rotationAngle, RGaFloat64KVector rotationBlade)
        {
            var halfRotationAngle = rotationAngle / 2;
            var cosHalfAngle = halfRotationAngle.Cos();
            var sinHalfAngle = halfRotationAngle.Sin();

            var rotationBladeScalar =
                sinHalfAngle * (-rotationBlade.ESp(rotationBlade)).Sqrt();

            var rotorStorage =
                cosHalfAngle + rotationBladeScalar * rotationBlade;

            //rotor.IsSimpleRotor();

            return new RGaFloat64Rotor(rotorStorage);
        }

        public static RGaFloat64Rotor CreateEuclideanPureRotor(RGaFloat64Vector inputVector1, RGaFloat64Vector inputVector2, RGaFloat64Vector rotatedVector1, RGaFloat64Vector rotatedVector2)
        {
            var inputFrame = 
                RGaFloat64VectorFrameSpecs
                    .CreateLinearlyIndependentSpecs()
                    .CreateVectorFrame(
                        inputVector1, 
                        inputVector2
                    );

            var rotatedFrame = 
                RGaFloat64VectorFrameSpecs
                    .CreateLinearlyIndependentSpecs()
                    .CreateVectorFrame(
                        rotatedVector1, 
                        rotatedVector2
                    );

            return RGaFloat64PureRotorsSequence.CreateFromOrthonormalEuclideanFrames(
                inputFrame, 
                rotatedFrame, 
                true
            ).GetFinalRotor();
        }
        
        public static RGaFloat64Rotor CreateEuclideanPureRotor(int baseSpaceDimensions, RGaFloat64Vector inputVector1, RGaFloat64Vector inputVector2, RGaFloat64Vector rotatedVector1, RGaFloat64Vector rotatedVector2)
        {
            var inputFrame = 
                RGaFloat64VectorFrameSpecs
                    .CreateLinearlyIndependentSpecs()
                    .CreateVectorFrame(
                        inputVector1, 
                        inputVector2
                    );

            var rotatedFrame = 
                RGaFloat64VectorFrameSpecs
                    .CreateLinearlyIndependentSpecs()
                    .CreateVectorFrame(
                        rotatedVector1, 
                        rotatedVector2
                    );

            return RGaFloat64PureRotorsSequence.CreateFromEuclideanFrames(
                baseSpaceDimensions, 
                inputFrame, 
                rotatedFrame
            ).GetFinalRotor();
        }

        /// <summary>
        /// Construct a rotor in the e_i-e_j plane with the given angle where i is less than j
        /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        public static RGaFloat64Rotor CreateGivensRotor(int i, int j, double rotationAngle)
        {
            Debug.Assert(i >= 0 && j > i);

            var halfRotationAngle = rotationAngle / 2;
            var cosHalfAngle = halfRotationAngle.Cos();
            var sinHalfAngle = halfRotationAngle.Sin();

            var bladeId = BasisBivectorUtils.IndexPairToBivectorId(i, j);

            var metric = RGaFloat64Processor.Euclidean;
            var composer = metric.CreateComposer();

            composer.SetScalarTerm(cosHalfAngle);
            composer.SetTerm(bladeId, sinHalfAngle);

            return new RGaFloat64Rotor(
                composer.GetMultivector()
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator RGaFloat64Multivector(RGaFloat64Rotor rotor)
        {
            return rotor.Multivector;
        }


        public RGaFloat64Multivector Multivector { get; }

        public RGaFloat64Multivector MultivectorReverse { get; }


        private RGaFloat64Rotor(RGaFloat64Multivector mv)
            : base(mv.Processor)
        {
            Multivector = mv;
            MultivectorReverse = mv.Reverse();
        }

        private RGaFloat64Rotor(RGaFloat64Multivector mv, RGaFloat64Multivector mvReverse)
            : base(mv.Processor)
        {
            Multivector = mv;
            MultivectorReverse = mvReverse;
        }

        
        public override bool IsValid()
        {
            // Make sure the storage and its reverse are correct
            if (!(Multivector.Reverse() - MultivectorReverse).IsNearZero())
                return false;

            // Make sure storage contains only terms of even grade
            if (!Multivector.IsEven(2))
                return false;

            // Make sure storage gp reverse(storage) == 1
            var gp = 
                Multivector.Gp(MultivectorReverse);

            if (!gp.IsScalar())
                return false;

            var diff = gp[0] - 1;

            return diff.IsNearZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaFloat64Rotor GetRotorInverse()
        {
            return new RGaFloat64Rotor(
                MultivectorReverse, 
                Multivector
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector OmMap(RGaFloat64Vector mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector OmMap(RGaFloat64Bivector mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetHigherKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector OmMap(RGaFloat64KVector mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector OmMap(RGaFloat64Multivector mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse);
        }

        public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            throw new NotImplementedException();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivector()
        {
            return Multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorReverse()
        {
            return MultivectorReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector GetMultivectorInverse()
        {
            return MultivectorReverse;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetScalingFactor()
        {
            return 1d;
        }
    }
}
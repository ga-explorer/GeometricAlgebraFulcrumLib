using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors
{
    public class XGaRotor<T> 
        : XGaRotorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaRotor<T> CreateIdentity(XGaProcessor<T> processor)
        {
            return new XGaRotor<T>(
                processor.CreateScalar(processor.ScalarProcessor.ScalarOne)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static XGaRotor<T> Create(XGaMultivector<T> mv)
        {
            return new XGaRotor<T>(mv);
        }

        public static XGaRotor<T> CreateEuclideanPureRotor(XGaVector<T> sourceVector, XGaVector<T> targetVector)
        {
            var norm1 = sourceVector.ENorm();
            var norm2 = targetVector.ENorm();
            var cosAngle = sourceVector.ESp(targetVector) / (norm1 * norm2);

            if (cosAngle.IsOne)
                return CreateIdentity(sourceVector.Processor);
            
            var rotationBlade = 
                cosAngle.IsMinusOne
                    ? throw new InvalidOperationException()//sourceVector.GetNormalVector().Op(sourceVector)
                    : targetVector.Op(sourceVector);
                
            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();
            
            var rotorStorage = 
                cosHalfAngle.ScalarValue() + sinHalfAngle * unitRotationBlade;
            
            //rotor.IsSimpleRotor();

            return new XGaRotor<T>(rotorStorage);
        }

        /// <summary>
        /// Create a simple rotor from an angle and a blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <param name="rotationBlade"></param>
        /// <returns></returns>
        public static XGaRotor<T> CreateEuclideanPureRotor(T rotationAngle, XGaKVector<T> rotationBlade)
        {
            var processor = rotationBlade.ScalarProcessor;

            var halfRotationAngle = processor.Divide(rotationAngle , processor.GetScalarFromNumber(2));
            var cosHalfAngle = processor.Cos(halfRotationAngle);
            var sinHalfAngle = processor.Sin(halfRotationAngle);

            var rotationBladeScalar =
                sinHalfAngle * (-rotationBlade.ESp(rotationBlade)).Sqrt();

            var rotorStorage =
                cosHalfAngle + rotationBladeScalar * rotationBlade;

            //rotor.IsSimpleRotor();

            return new XGaRotor<T>(rotorStorage);
        }

        public static XGaRotor<T> CreateEuclideanPureRotor(XGaVector<T> inputVector1, XGaVector<T> inputVector2, XGaVector<T> rotatedVector1, XGaVector<T> rotatedVector2)
        {
            var inputFrame = 
                XGaVectorFrameSpecs
                    .CreateLinearlyIndependentSpecs()
                    .CreateVectorFrame(
                        inputVector1, 
                        inputVector2
                    );

            var rotatedFrame = 
                XGaVectorFrameSpecs
                    .CreateLinearlyIndependentSpecs()
                    .CreateVectorFrame(
                        rotatedVector1, 
                        rotatedVector2
                    );

            return XGaPureRotorsSequence<T>.CreateFromOrthonormalEuclideanFrames(
                inputFrame, 
                rotatedFrame, 
                true
            ).GetFinalRotor();
        }
        
        public static XGaRotor<T> CreateEuclideanPureRotor(int baseSpaceDimensions, XGaVector<T> inputVector1, XGaVector<T> inputVector2, XGaVector<T> rotatedVector1, XGaVector<T> rotatedVector2)
        {
            var inputFrame = 
                XGaVectorFrameSpecs
                    .CreateLinearlyIndependentSpecs()
                    .CreateVectorFrame(
                        inputVector1, 
                        inputVector2
                    );

            var rotatedFrame = 
                XGaVectorFrameSpecs
                    .CreateLinearlyIndependentSpecs()
                    .CreateVectorFrame(
                        rotatedVector1, 
                        rotatedVector2
                    );

            return XGaPureRotorsSequence<T>.CreateFromEuclideanFrames(
                baseSpaceDimensions, 
                inputFrame, 
                rotatedFrame
            ).GetFinalRotor();
        }

        /// <summary>
        /// Construct a rotor in the e_i-e_j plane with the given angle where i is less than j
        /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
        /// </summary>
        /// <param name="processor"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        public static XGaRotor<T> CreateGivensRotor(XGaProcessor<T> processor, int i, int j, T rotationAngle)
        {
            Debug.Assert(i >= 0 && j > i);

            var scalarProcessor = processor.ScalarProcessor;
            var halfRotationAngle = scalarProcessor.Divide(rotationAngle, scalarProcessor.GetScalarFromNumber(2));
            var cosHalfAngle = scalarProcessor.Cos(halfRotationAngle);
            var sinHalfAngle = scalarProcessor.Sin(halfRotationAngle);

            var bladeId = BasisBivectorUtils.IndexPairToBivectorId(i, j);

            var composer = processor.CreateComposer();

            composer.SetScalarTerm(cosHalfAngle);
            composer.SetTerm(bladeId, sinHalfAngle);

            return new XGaRotor<T>(
                composer.GetMultivector()
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator XGaMultivector<T>(XGaRotor<T> rotor)
        {
            return rotor.Multivector;
        }


        public XGaMultivector<T> Multivector { get; }

        public XGaMultivector<T> MultivectorReverse { get; }


        private XGaRotor(XGaMultivector<T> mv)
            : base(mv.Processor)
        {
            Multivector = mv;
            MultivectorReverse = mv.Reverse();
        }

        private XGaRotor(XGaMultivector<T> mv, XGaMultivector<T> mvReverse)
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

            var diff = gp.Scalar() - 1;

            return diff.IsNearZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IXGaRotor<T> GetRotorInverse()
        {
            return new XGaRotor<T>(
                MultivectorReverse, 
                Multivector
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> OmMap(XGaVector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> OmMap(XGaBivector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse).GetHigherKVectorPart(mv.Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> OmMap(XGaMultivector<T> mv)
        {
            return Multivector.Gp(mv).Gp(MultivectorReverse);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivector()
        {
            return Multivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivectorReverse()
        {
            return MultivectorReverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> GetMultivectorInverse()
        {
            return MultivectorReverse;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalingFactor()
        {
            return ScalarProcessor.CreateScalarOne();
        }
    }
}
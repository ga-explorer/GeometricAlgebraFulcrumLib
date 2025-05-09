using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Frames;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;

public class XGaScaledRotor<T> 
    : XGaScaledRotorBase<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScaledRotor<T> CreateIdentity(XGaProcessor<T> processor)
    {
        return new XGaScaledRotor<T>(
            processor.Scalar(processor.ScalarProcessor.OneValue)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScaledRotor<T> Create(XGaMultivector<T> mv)
    {
        return new XGaScaledRotor<T>(mv);
    }

    public static XGaScaledRotor<T> CreateEuclideanScaledPureRotor(XGaVector<T> sourceVector, XGaVector<T> targetVector)
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

        var (cosHalfAngle, sinHalfAngle) = 
            cosAngle.CosToPolarAngle().HalfPolarAngle();
            
        var rotorStorage = 
            cosHalfAngle + sinHalfAngle * unitRotationBlade;
            
        //rotor.IsSimpleScaledRotor();

        return new XGaScaledRotor<T>(rotorStorage);
    }

    /// <summary>
    /// Create a simple rotor from an angle and a blade
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <param name="rotationBlade"></param>
    /// <returns></returns>
    public static XGaScaledRotor<T> CreateEuclideanScaledPureRotor(LinPolarAngle<T> rotationAngle, XGaKVector<T> rotationBlade)
    {
        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();

        var rotationBladeScalar = sinHalfAngle / (-rotationBlade.ESp(rotationBlade)).Sqrt();
        var rotorStorage = cosHalfAngle + rotationBladeScalar * rotationBlade;

        //rotor.IsSimpleScaledRotor();

        return new XGaScaledRotor<T>(rotorStorage);
    }

    public static XGaScaledRotor<T> CreateEuclideanScaledPureRotor(XGaVector<T> inputVector1, XGaVector<T> inputVector2, XGaVector<T> rotatedVector1, XGaVector<T> rotatedVector2)
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

        return XGaScaledPureRotorsSequence<T>.CreateFromOrthonormalEuclideanFrames(
            inputFrame, 
            rotatedFrame, 
            true
        ).GetFinalScaledRotor();
    }
        
    public static XGaScaledRotor<T> CreateEuclideanScaledPureRotor(int baseSpaceDimensions, XGaVector<T> inputVector1, XGaVector<T> inputVector2, XGaVector<T> rotatedVector1, XGaVector<T> rotatedVector2)
    {
        var inputFrame = 
            XGaVectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(inputVector1, inputVector2);

        var rotatedFrame = 
            XGaVectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(rotatedVector1, rotatedVector2);

        return XGaScaledPureRotorsSequence<T>.CreateFromEuclideanFrames(
            baseSpaceDimensions, 
            inputFrame, 
            rotatedFrame
        ).GetFinalScaledRotor();
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
    public static XGaScaledRotor<T> CreateScaledGivensRotor(XGaProcessor<T> processor, int i, int j, LinPolarAngle<T> rotationAngle)
    {
        Debug.Assert(i >= 0 && j > i);

        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();

        var bladeId = BasisBivectorUtils.IndexPairToBivectorId(i, j);

        var composer = processor.CreateComposer();

        composer.SetTerm(0, cosHalfAngle);
        composer.SetTerm(bladeId, sinHalfAngle);

        return new XGaScaledRotor<T>(
            composer.GetSimpleMultivector()
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator XGaMultivector<T>(XGaScaledRotor<T> rotor)
    {
        return rotor.Multivector;
    }


    public XGaMultivector<T> Multivector { get; }

    public XGaMultivector<T> MultivectorReverse { get; }


    private XGaScaledRotor(XGaMultivector<T> mv)
        : base(mv.Processor)
    {
        Multivector = mv;
        MultivectorReverse = mv.Reverse();
    }

    private XGaScaledRotor(XGaMultivector<T> mv, XGaMultivector<T> mvReverse)
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
        if (!Multivector.IsEven())
            return false;

        // Make sure storage gp reverse(storage) == 1
        var gp = 
            Multivector.Gp(MultivectorReverse);

        return gp.IsScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetScalingFactor()
    {
        return Multivector.Sp(MultivectorReverse).Scalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaScaledRotor<T> GetScaledRotorInverse()
    {
        return new XGaScaledRotor<T>(
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
}
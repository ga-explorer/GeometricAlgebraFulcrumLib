﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Frames;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

public class RGaScaledRotor<T> 
    : RGaScaledRotorBase<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledRotor<T> CreateIdentity(RGaProcessor<T> processor)
    {
        return new RGaScaledRotor<T>(
            processor.Scalar(processor.ScalarProcessor.OneValue)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScaledRotor<T> Create(RGaMultivector<T> mv)
    {
        return new RGaScaledRotor<T>(mv);
    }

    public static RGaScaledRotor<T> CreateEuclideanScaledPureRotor(RGaVector<T> sourceVector, RGaVector<T> targetVector)
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
            LinPolarAngle<T>.CreateHalfAngleFromCos(cosAngle);

        var rotorStorage = 
            cosHalfAngle + sinHalfAngle * unitRotationBlade;
            
        //rotor.IsSimpleScaledRotor();

        return new RGaScaledRotor<T>(rotorStorage);
    }

    /// <summary>
    /// Create a simple rotor from an angle and a blade
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <param name="rotationBlade"></param>
    /// <returns></returns>
    public static RGaScaledRotor<T> CreateEuclideanScaledPureRotor(LinPolarAngle<T> rotationAngle, RGaKVector<T> rotationBlade)
    {
        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();

        var rotationBladeScalar = sinHalfAngle / (-rotationBlade.ESp(rotationBlade)).Sqrt();
        var rotorStorage = cosHalfAngle + rotationBladeScalar * rotationBlade;

        //rotor.IsSimpleScaledRotor();

        return new RGaScaledRotor<T>(rotorStorage);
    }

    public static RGaScaledRotor<T> CreateEuclideanScaledPureRotor(RGaVector<T> inputVector1, RGaVector<T> inputVector2, RGaVector<T> rotatedVector1, RGaVector<T> rotatedVector2)
    {
        var inputFrame = 
            RGaVectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    inputVector1, 
                    inputVector2
                );

        var rotatedFrame = 
            RGaVectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    rotatedVector1, 
                    rotatedVector2
                );

        return RGaScaledPureRotorsSequence<T>.CreateFromOrthonormalEuclideanFrames(
            inputFrame, 
            rotatedFrame, 
            true
        ).GetFinalScaledRotor();
    }
        
    public static RGaScaledRotor<T> CreateEuclideanScaledPureRotor(int baseSpaceDimensions, RGaVector<T> inputVector1, RGaVector<T> inputVector2, RGaVector<T> rotatedVector1, RGaVector<T> rotatedVector2)
    {
        var inputFrame = 
            RGaVectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(inputVector1, inputVector2);

        var rotatedFrame = 
            RGaVectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(rotatedVector1, rotatedVector2);

        return RGaScaledPureRotorsSequence<T>.CreateFromEuclideanFrames(
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
    public static RGaScaledRotor<T> CreateScaledGivensRotor(RGaProcessor<T> processor, int i, int j, LinPolarAngle<T> rotationAngle)
    {
        Debug.Assert(i >= 0 && j > i);

        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();

        var bladeId = BasisBivectorUtils.IndexPairToBivectorId(i, j);

        var composer = processor.CreateComposer();

        composer.SetTerm(0, cosHalfAngle);
        composer.SetTerm(bladeId, sinHalfAngle);

        return new RGaScaledRotor<T>(
            composer.GetSimpleMultivector()
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator RGaMultivector<T>(RGaScaledRotor<T> rotor)
    {
        return rotor.Multivector;
    }


    public RGaMultivector<T> Multivector { get; }

    public RGaMultivector<T> MultivectorReverse { get; }


    private RGaScaledRotor(RGaMultivector<T> mv)
        : base(mv.Processor)
    {
        Multivector = mv;
        MultivectorReverse = mv.Reverse();
    }

    private RGaScaledRotor(RGaMultivector<T> mv, RGaMultivector<T> mvReverse)
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
    public override IRGaScaledRotor<T> GetScaledRotorInverse()
    {
        return new RGaScaledRotor<T>(
            MultivectorReverse, 
            Multivector
        );
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaVector<T> OmMap(RGaVector<T> mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaBivector<T> OmMap(RGaBivector<T> mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaHigherKVector<T> OmMap(RGaHigherKVector<T> mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetHigherKVectorPart(mv.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> OmMap(RGaMultivector<T> mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivector()
    {
        return Multivector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivectorReverse()
    {
        return MultivectorReverse;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivectorInverse()
    {
        return MultivectorReverse;
    }
}
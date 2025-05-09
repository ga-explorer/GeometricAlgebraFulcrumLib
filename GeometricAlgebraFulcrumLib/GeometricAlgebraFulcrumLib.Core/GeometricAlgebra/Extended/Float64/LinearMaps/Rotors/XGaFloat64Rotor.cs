using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Frames;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;

public class XGaFloat64Rotor 
    : XGaFloat64RotorBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Rotor CreateIdentity(XGaFloat64Processor metric)
    {
        return new XGaFloat64Rotor(
            metric.Scalar(1d)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaFloat64Rotor Create(XGaFloat64Multivector mv)
    {
        return new XGaFloat64Rotor(mv);
    }

    public static XGaFloat64Rotor CreateEuclideanPureRotor(XGaFloat64Vector sourceVector, XGaFloat64Vector targetVector)
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

        var (cosHalfAngle, sinHalfAngle) = 
            cosAngle.CosToPolarAngle().HalfPolarAngle();
            
        var rotorStorage = 
            cosHalfAngle + sinHalfAngle * unitRotationBlade;
            
        //rotor.IsSimpleRotor();

        return new XGaFloat64Rotor(rotorStorage);
    }

    /// <summary>
    /// Create a simple rotor from an angle and a blade
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <param name="rotationBlade"></param>
    /// <returns></returns>
    public static XGaFloat64Rotor CreateEuclideanPureRotor(LinFloat64PolarAngle rotationAngle, XGaFloat64Bivector rotationBlade)
    {
        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();

        var rotationBladeScalar =
            sinHalfAngle * (-rotationBlade.ESp(rotationBlade)).Sqrt();

        var rotorStorage =
            cosHalfAngle + rotationBladeScalar * rotationBlade;

        //rotor.IsSimpleRotor();

        return new XGaFloat64Rotor(rotorStorage);
    }

    public static XGaFloat64Rotor CreateEuclideanPureRotor(XGaFloat64Vector inputVector1, XGaFloat64Vector inputVector2, XGaFloat64Vector rotatedVector1, XGaFloat64Vector rotatedVector2)
    {
        var inputFrame = 
            XGaFloat64VectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    inputVector1, 
                    inputVector2
                );

        var rotatedFrame = 
            XGaFloat64VectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    rotatedVector1, 
                    rotatedVector2
                );

        return XGaFloat64PureRotorsSequence.CreateFromOrthonormalEuclideanFrames(
            inputFrame, 
            rotatedFrame, 
            true
        ).GetFinalRotor();
    }
    
    public static XGaFloat64Rotor CreateEuclideanPureRotor(int baseSpaceDimensions, XGaFloat64Vector inputVector1, XGaFloat64Vector inputVector2, XGaFloat64Vector rotatedVector1, XGaFloat64Vector rotatedVector2)
    {
        var inputFrame = 
            XGaFloat64VectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    inputVector1, 
                    inputVector2
                );

        var rotatedFrame = 
            XGaFloat64VectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    rotatedVector1, 
                    rotatedVector2
                );

        return XGaFloat64PureRotorsSequence.CreateFromEuclideanFrames(
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
    public static XGaFloat64Rotor CreateGivensRotor(XGaFloat64Processor processor, int i, int j, LinFloat64PolarAngle rotationAngle)
    {
        Debug.Assert(i >= 0 && j > i);

        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();

        var bladeId = IndexSetUtils.IndexPairToIndexSet(i, j);

        var composer = processor.CreateComposer();

        composer.SetScalarTerm(cosHalfAngle);
        composer.SetTerm(bladeId, sinHalfAngle);

        return new XGaFloat64Rotor(
            composer.GetMultivector()
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator XGaFloat64Multivector(XGaFloat64Rotor rotor)
    {
        return rotor.Multivector;
    }


    public XGaFloat64Multivector Multivector { get; }

    public XGaFloat64Multivector MultivectorReverse { get; }


    private XGaFloat64Rotor(XGaFloat64Multivector mv)
        : base(mv.Processor)
    {
        Multivector = mv;
        MultivectorReverse = mv.Reverse();
    }

    private XGaFloat64Rotor(XGaFloat64Multivector mv, XGaFloat64Multivector mvReverse)
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
    public override IXGaFloat64Rotor GetRotorInverse()
    {
        return new XGaFloat64Rotor(
            MultivectorReverse, 
            Multivector
        );
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetBivectorPart();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse).GetHigherKVectorPart(mv.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
    {
        return Multivector.Gp(mv).Gp(MultivectorReverse);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivector()
    {
        return Multivector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorReverse()
    {
        return MultivectorReverse;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetMultivectorInverse()
    {
        return MultivectorReverse;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetScalingFactor()
    {
        return 1d;
    }
}
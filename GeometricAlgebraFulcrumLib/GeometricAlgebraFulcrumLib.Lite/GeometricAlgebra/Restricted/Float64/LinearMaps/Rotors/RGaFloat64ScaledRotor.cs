using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Frames;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;

public class RGaFloat64ScaledRotor 
    : RGaFloat64ScaledRotorBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledRotor CreateIdentity(RGaFloat64Processor metric)
    {
        return new RGaFloat64ScaledRotor(
            metric.CreateScalar(1d)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledRotor Create(RGaFloat64Multivector mv)
    {
        return new RGaFloat64ScaledRotor(mv);
    }

    public static RGaFloat64ScaledRotor CreateEuclideanScaledPureRotor(RGaFloat64Vector sourceVector, RGaFloat64Vector targetVector)
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
            cosHalfAngle.ScalarValue() + sinHalfAngle * unitRotationBlade;
            
        //rotor.IsSimpleScaledRotor();

        return new RGaFloat64ScaledRotor(rotorStorage);
    }

    /// <summary>
    /// Create a simple rotor from an angle and a blade
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <param name="rotationBlade"></param>
    /// <returns></returns>
    public static RGaFloat64ScaledRotor CreateEuclideanScaledPureRotor(double rotationAngle, RGaFloat64KVector rotationBlade)
    {
        var halfRotationAngle = rotationAngle / 2d;
        var cosHalfAngle = halfRotationAngle.Cos();
        var sinHalfAngle = halfRotationAngle.Sin();
        var rotationBladeScalar = sinHalfAngle / (-rotationBlade.ESp(rotationBlade)).Sqrt();
        var rotorStorage = cosHalfAngle + rotationBladeScalar * rotationBlade;

        //rotor.IsSimpleScaledRotor();

        return new RGaFloat64ScaledRotor(rotorStorage);
    }

    public static RGaFloat64ScaledRotor CreateEuclideanScaledPureRotor(RGaFloat64Vector inputVector1, RGaFloat64Vector inputVector2, RGaFloat64Vector rotatedVector1, RGaFloat64Vector rotatedVector2)
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

        return RGaFloat64ScaledPureRotorsSequence.CreateFromOrthonormalEuclideanFrames(
            inputFrame, 
            rotatedFrame, 
            true
        ).GetFinalScaledRotor();
    }
        
    public static RGaFloat64ScaledRotor CreateEuclideanScaledPureRotor(int baseSpaceDimensions, RGaFloat64Vector inputVector1, RGaFloat64Vector inputVector2, RGaFloat64Vector rotatedVector1, RGaFloat64Vector rotatedVector2)
    {
        var inputFrame = 
            RGaFloat64VectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(inputVector1, inputVector2);

        var rotatedFrame = 
            RGaFloat64VectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(rotatedVector1, rotatedVector2);

        return RGaFloat64ScaledPureRotorsSequence.CreateFromEuclideanFrames(
            baseSpaceDimensions, 
            inputFrame, 
            rotatedFrame
        ).GetFinalScaledRotor();
    }

    /// <summary>
    /// Construct a rotor in the e_i-e_j plane with the given angle where i is less than j
    /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
    /// </summary>
    /// <param name="metric"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="rotationAngle"></param>
    /// <returns></returns>
    public static RGaFloat64ScaledRotor CreateScaledGivensRotor(RGaFloat64Processor metric, int i, int j, double rotationAngle)
    {
        Debug.Assert(i >= 0 && j > i);

        var halfRotationAngle = rotationAngle / 2d;
        var cosHalfAngle = halfRotationAngle.Cos();
        var sinHalfAngle = halfRotationAngle.Sin();

        var bladeId = BasisBivectorUtils.IndexPairToBivectorId(i, j);

        var composer = metric.CreateComposer();

        composer.SetScalarTerm(cosHalfAngle);
        composer.SetTerm(bladeId, sinHalfAngle);

        return new RGaFloat64ScaledRotor(
            composer.GetSimpleMultivector()
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator RGaFloat64Multivector(RGaFloat64ScaledRotor rotor)
    {
        return rotor.Multivector;
    }


    public RGaFloat64Multivector Multivector { get; }

    public RGaFloat64Multivector MultivectorReverse { get; }


    private RGaFloat64ScaledRotor(RGaFloat64Multivector mv)
        : base(mv.Processor)
    {
        Multivector = mv;
        MultivectorReverse = mv.Reverse();
    }

    private RGaFloat64ScaledRotor(RGaFloat64Multivector mv, RGaFloat64Multivector mvReverse)
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
    public override double GetScalingFactor()
    {
        return Multivector.Sp(MultivectorReverse).Scalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaFloat64ScaledRotor GetScaledRotorInverse()
    {
        return new RGaFloat64ScaledRotor(
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
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Frames;

public static class XGaFloat64FrameComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64BasisVectorFrame CreateBasisVectorFrame(this XGaFloat64Processor processor, int vSpaceDimensions)
    {
        return XGaFloat64BasisVectorFrame.Create(
            vSpaceDimensions.GetRange(processor.VectorTerm)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64BasisVectorFrame CreateBasisVectorFrame(params XGaFloat64Vector[] vectorArray)
    {
        return XGaFloat64BasisVectorFrame.Create(vectorArray);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64BasisKVectorFrame CreateBasisKVectorFrame(this IEnumerable<XGaFloat64KVector> kVectorList)
    {
        return XGaFloat64BasisKVectorFrame.Create(kVectorList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64BasisKVectorFrame CreateBasisKVectorFrame(this XGaFloat64BasisVectorFrame vectorFrame)
    {
        return XGaFloat64BasisKVectorFrame.CreateFrom(vectorFrame);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64BasisMultivectorFrame CreateBasisMultivectorFrame(this XGaFloat64BasisVectorFrame vectorFrame)
    {
        return XGaFloat64BasisMultivectorFrame.CreateFrom(vectorFrame);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrame CreateVectorFrame(this XGaFloat64VectorFrameSpecs frameSpecs, XGaFloat64Vector vector1, XGaFloat64Vector vector2)
    {
        return XGaFloat64VectorFrame.Create(
            frameSpecs,
            new[] { vector1, vector2 }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrame CreateVectorFrame(this XGaFloat64VectorFrameSpecs frameSpecs, params XGaFloat64Vector[] vectorsList)
    {
        return XGaFloat64VectorFrame.Create(frameSpecs, vectorsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrame CreateVectorFrame(this XGaFloat64VectorFrameSpecs frameSpecs, IEnumerable<XGaFloat64Vector> vectorsList)
    {
        return XGaFloat64VectorFrame.Create(frameSpecs, vectorsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrame CreateFreeFrameOfBasis(this XGaFloat64Processor metric, int vSpaceDimensions)
    {
        var vectorsList =
            vSpaceDimensions
                .GetRange()
                .Select(index => metric.VectorTerm(index));

        return XGaFloat64VectorFrame.Create(
            XGaFloat64VectorFrameSpecs.CreateUnitBasisSpecs(),
            vectorsList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrame CreateFreeFrameOfScaledBasis(this XGaFloat64Processor metric, int vSpaceDimensions, double scalingFactor)
    {
        var vectorsList =
            vSpaceDimensions
                .GetRange()
                .Select(index => metric.VectorTerm(index, scalingFactor));

        return XGaFloat64VectorFrame.Create(
            XGaFloat64VectorFrameSpecs.CreateScaledBasisSpecs(),
            vectorsList
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrame CreateFreeFrameOfSimplex(this XGaFloat64Processor metric, int vSpaceDimensions, double scalingFactor)
    {
        var vSpaceDimensions1 = vSpaceDimensions + 1;

        // Create ones-vector
        var onesVector = metric.VectorSymmetricUnit(vSpaceDimensions1);
        var basisVector = metric.VectorTerm(vSpaceDimensions);

        // Find a rotor that rotates the ones vector into the last basis vector
        var rotor = onesVector.CreatePureRotor(
            basisVector,
            true
        );

        // Create a subspace from the dual of the ones vector
        var hyperSubspace = 
            onesVector.Dual(vSpaceDimensions + 1).ToSubspace();

        // Project each basis vectors on the subspace, and then rotate
        // the projected vectors and discard the last component of each vector
        var vectorsList =
            vSpaceDimensions1
                .GetRange()
                .Select(index =>
                    rotor.OmMap(
                        hyperSubspace.Project(
                            metric.VectorTerm(index, scalingFactor)
                        )
                    ).GetVectorPart(i => i < vSpaceDimensions)
                );

        return XGaFloat64VectorFrame.Create(
            XGaFloat64VectorFrameSpecs.CreateSimplexSpecs(),
            vectorsList
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrameFixed CreateFixedFrame(this IXGaFloat64VectorFrame frame)
    {
        return XGaFloat64VectorFrameFixed.Create(
            frame.Processor.VectorZero,
            frame
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrameFixed CreateFixedFrame(this IXGaFloat64VectorFrame frame, XGaFloat64Vector point)
    {
        return XGaFloat64VectorFrameFixed.Create(point, frame);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrameFixed CreateBasisVectorFrameFixed(this XGaFloat64Vector point, int vSpaceDimensions)
    {
        return point
            .Processor
            .CreateFreeFrameOfBasis(vSpaceDimensions)
            .CreateFixedFrame(point);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrameFixed CreateBasisVectorFrameFixed(this XGaFloat64Processor metric, int vSpaceDimensions)
    {
        return metric
            .CreateFreeFrameOfBasis(vSpaceDimensions)
            .CreateFixedFrame();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrameFixed CreateFixedFrameOfScaledBasis(this XGaFloat64Processor metric, int vSpaceDimensions, double scalingFactor)
    {
        return metric
            .CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrameFixed CreateFixedFrameOfScaledBasis(this XGaFloat64Vector point, int vSpaceDimensions, double scalingFactor)
    {
        var metric = point.Processor;

        return metric
            .CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame(point);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrameFixed CreateFixedFrameOfSimplex(this XGaFloat64Processor metric, int vSpaceDimensions, double scalingFactor)
    {
        return metric.CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor).CreateFixedFrame();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64VectorFrameFixed CreateFixedFrameOfSimplex(this XGaFloat64Vector point, int vSpaceDimensions, double scalingFactor)
    {
        var metric = point.Processor;

        return metric
            .CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame(point);
    }
}
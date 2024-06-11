using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Frames;

public static class RGaFloat64FrameComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64BasisVectorFrame CreateBasisVectorFrame(this RGaFloat64Processor processor, int vSpaceDimensions)
    {
        return RGaFloat64BasisVectorFrame.Create(
            vSpaceDimensions.GetRange(processor.VectorTerm)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64BasisVectorFrame CreateBasisVectorFrame(params RGaFloat64Vector[] vectorArray)
    {
        return RGaFloat64BasisVectorFrame.Create(vectorArray);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64BasisKVectorFrame CreateBasisKVectorFrame(this IEnumerable<RGaFloat64KVector> kVectorList)
    {
        return RGaFloat64BasisKVectorFrame.Create(kVectorList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64BasisKVectorFrame CreateBasisKVectorFrame(this RGaFloat64BasisVectorFrame vectorFrame)
    {
        return RGaFloat64BasisKVectorFrame.CreateFrom(vectorFrame);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64BasisMultivectorFrame CreateBasisMultivectorFrame(this RGaFloat64BasisVectorFrame vectorFrame)
    {
        return RGaFloat64BasisMultivectorFrame.CreateFrom(vectorFrame);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrame CreateVectorFrame(this RGaFloat64VectorFrameSpecs frameSpecs, RGaFloat64Vector vector1, RGaFloat64Vector vector2)
    {
        return RGaFloat64VectorFrame.Create(
            frameSpecs,
            new[] { vector1, vector2 }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrame CreateVectorFrame(this RGaFloat64VectorFrameSpecs frameSpecs, params RGaFloat64Vector[] vectorsList)
    {
        return RGaFloat64VectorFrame.Create(frameSpecs, vectorsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrame CreateVectorFrame(this RGaFloat64VectorFrameSpecs frameSpecs, IEnumerable<RGaFloat64Vector> vectorsList)
    {
        return RGaFloat64VectorFrame.Create(frameSpecs, vectorsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrame CreateFreeFrameOfBasis(this RGaFloat64Processor metric, int vSpaceDimensions)
    {
        var vectorsList =
            vSpaceDimensions
                .GetRange()
                .Select(index => metric.VectorTerm(index));

        return RGaFloat64VectorFrame.Create(
            RGaFloat64VectorFrameSpecs.CreateUnitBasisSpecs(),
            vectorsList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrame CreateFreeFrameOfScaledBasis(this RGaFloat64Processor metric, int vSpaceDimensions, double scalingFactor)
    {
        var vectorsList =
            vSpaceDimensions
                .GetRange()
                .Select(index => metric.VectorTerm(index, scalingFactor));

        return RGaFloat64VectorFrame.Create(
            RGaFloat64VectorFrameSpecs.CreateScaledBasisSpecs(),
            vectorsList
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrame CreateFreeFrameOfSimplex(this RGaFloat64Processor metric, int vSpaceDimensions, double scalingFactor)
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
                    ).GetVectorPart((int i) => i < vSpaceDimensions)
                );

        return RGaFloat64VectorFrame.Create(
            RGaFloat64VectorFrameSpecs.CreateSimplexSpecs(),
            vectorsList
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrameFixed CreateFixedFrame(this IRGaFloat64VectorFrame frame)
    {
        return RGaFloat64VectorFrameFixed.Create(
            frame.Processor.VectorZero,
            frame
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrameFixed CreateFixedFrame(this IRGaFloat64VectorFrame frame, RGaFloat64Vector point)
    {
        return RGaFloat64VectorFrameFixed.Create(point, frame);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrameFixed CreateBasisVectorFrameFixed(this RGaFloat64Vector point, int vSpaceDimensions)
    {
        return point
            .Processor
            .CreateFreeFrameOfBasis(vSpaceDimensions)
            .CreateFixedFrame(point);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrameFixed CreateBasisVectorFrameFixed(this RGaFloat64Processor metric, int vSpaceDimensions)
    {
        return metric
            .CreateFreeFrameOfBasis(vSpaceDimensions)
            .CreateFixedFrame();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrameFixed CreateFixedFrameOfScaledBasis(this RGaFloat64Processor metric, int vSpaceDimensions, double scalingFactor)
    {
        return metric
            .CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrameFixed CreateFixedFrameOfScaledBasis(this RGaFloat64Vector point, int vSpaceDimensions, double scalingFactor)
    {
        var metric = point.Processor;

        return metric
            .CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame(point);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrameFixed CreateFixedFrameOfSimplex(this RGaFloat64Processor metric, int vSpaceDimensions, double scalingFactor)
    {
        return metric.CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor).CreateFixedFrame();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64VectorFrameFixed CreateFixedFrameOfSimplex(this RGaFloat64Vector point, int vSpaceDimensions, double scalingFactor)
    {
        var metric = point.Processor;

        return metric
            .CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame(point);
    }
}
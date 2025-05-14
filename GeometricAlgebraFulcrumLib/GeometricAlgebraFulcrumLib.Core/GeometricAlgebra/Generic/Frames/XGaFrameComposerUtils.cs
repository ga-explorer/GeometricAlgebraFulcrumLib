using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Frames;

public static class XGaFrameComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBasisVectorFrame<T> CreateBasisVectorFrame<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        return XGaBasisVectorFrame<T>.Create(
            vSpaceDimensions.GetRange(processor.VectorTerm)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBasisVectorFrame<T> CreateBasisVectorFrame<T>(params XGaVector<T>[] vectorArray)
    {
        return XGaBasisVectorFrame<T>.Create(vectorArray);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBasisKVectorFrame<T> CreateBasisKVectorFrame<T>(this IEnumerable<XGaKVector<T>> kVectorList)
    {
        return XGaBasisKVectorFrame<T>.Create(kVectorList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBasisKVectorFrame<T> CreateBasisKVectorFrame<T>(this XGaBasisVectorFrame<T> vectorFrame)
    {
        return XGaBasisKVectorFrame<T>.CreateFrom(vectorFrame);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBasisMultivectorFrame<T> CreateBasisMultivectorFrame<T>(this XGaBasisVectorFrame<T> vectorFrame)
    {
        return XGaBasisMultivectorFrame<T>.CreateFrom(vectorFrame);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrame<T> CreateVectorFrame<T>(this XGaVectorFrameSpecs frameSpecs, XGaVector<T> vector1, XGaVector<T> vector2)
    {
        return XGaVectorFrame<T>.Create(
            frameSpecs,
            new[] { vector1, vector2 }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrame<T> CreateVectorFrame<T>(this XGaVectorFrameSpecs frameSpecs, params XGaVector<T>[] vectorsList)
    {
        return XGaVectorFrame<T>.Create(frameSpecs, vectorsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrame<T> CreateVectorFrame<T>(this XGaVectorFrameSpecs frameSpecs, IEnumerable<XGaVector<T>> vectorsList)
    {
        return XGaVectorFrame<T>.Create(frameSpecs, vectorsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrame<T> CreateFreeFrameOfBasis<T>(this XGaProcessor<T> processor, int vSpaceDimensions)
    {
        var vectorsList =
            vSpaceDimensions
                .GetRange()
                .Select(processor.VectorTerm);

        return XGaVectorFrame<T>.Create(
            XGaVectorFrameSpecs.CreateUnitBasisSpecs(),
            vectorsList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrame<T> CreateFreeFrameOfScaledBasis<T>(this XGaProcessor<T> metric, int vSpaceDimensions, T scalingFactor)
    {
        var vectorsList =
            vSpaceDimensions
                .GetRange()
                .Select(index => metric.VectorTerm(index, scalingFactor));

        return XGaVectorFrame<T>.Create(
            XGaVectorFrameSpecs.CreateScaledBasisSpecs(),
            vectorsList
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrame<T> CreateFreeFrameOfSimplex<T>(this XGaProcessor<T> metric, int vSpaceDimensions, T scalingFactor)
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

        return XGaVectorFrame<T>.Create(
            XGaVectorFrameSpecs.CreateSimplexSpecs(),
            vectorsList
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrameFixed<T> CreateFixedFrame<T>(this IXGaVectorFrame<T> frame)
    {
        return XGaVectorFrameFixed<T>.Create(
            frame.Processor.VectorZero,
            frame
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrameFixed<T> CreateFixedFrame<T>(this IXGaVectorFrame<T> frame, XGaVector<T> point)
    {
        return XGaVectorFrameFixed<T>.Create(point, frame);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrameFixed<T> CreateBasisVectorFrameFixed<T>(this XGaVector<T> point, int vSpaceDimensions)
    {
        return point
            .Processor
            .CreateFreeFrameOfBasis(vSpaceDimensions)
            .CreateFixedFrame(point);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrameFixed<T> CreateBasisVectorFrameFixed<T>(this XGaProcessor<T> metric, int vSpaceDimensions)
    {
        return metric
            .CreateFreeFrameOfBasis(vSpaceDimensions)
            .CreateFixedFrame();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrameFixed<T> CreateFixedFrameOfScaledBasis<T>(this XGaProcessor<T> metric, int vSpaceDimensions, T scalingFactor)
    {
        return metric
            .CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrameFixed<T> CreateFixedFrameOfScaledBasis<T>(this XGaVector<T> point, int vSpaceDimensions, T scalingFactor)
    {
        var processor = point.Processor;

        return processor
            .CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame(point);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrameFixed<T> CreateFixedFrameOfSimplex<T>(this XGaProcessor<T> metric, int vSpaceDimensions, T scalingFactor)
    {
        return metric.CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor).CreateFixedFrame();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrameFixed<T> CreateFixedFrameOfSimplex<T>(this XGaVector<T> point, int vSpaceDimensions, T scalingFactor)
    {
        var processor = point.Processor;

        return processor
            .CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame(point);
    }


        
    /// <summary>
    /// See the paper "Generalized Clarke Components for Poly-phase Networks", 1969
    /// </summary>
    /// <param name="processor"></param>
    /// <param name="vectorsCount"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVectorFrame<T> CreateClarkeRotationFrame<T>(this XGaProcessor<T> processor, int vectorsCount)
    {
        return XGaVectorFrameSpecs
            .CreateUnitBasisSpecs()
            .CreateVectorFrame(
                processor
                    .ScalarProcessor
                    .CreateClarkeRotationArray(vectorsCount)
                    .ColumnsToXGaVectors(processor)
            );
    }

}
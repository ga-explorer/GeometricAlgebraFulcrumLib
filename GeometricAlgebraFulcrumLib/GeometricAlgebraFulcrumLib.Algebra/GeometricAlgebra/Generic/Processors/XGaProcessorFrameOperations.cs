using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors
{
    public partial class XGaProcessor<T>
    {
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisVectorFrame<T> CreateBasisVectorFrame(int vSpaceDimensions)
    {
        return XGaBasisVectorFrame<T>.Create(
            vSpaceDimensions.GetRange(VectorTerm)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVectorFrame<T> CreateFreeFrameOfBasis(int vSpaceDimensions)
    {
        var vectorsList =
            vSpaceDimensions
                .GetRange()
                .Select(VectorTerm);

        return XGaVectorFrame<T>.Create(
            XGaVectorFrameSpecs.CreateUnitBasisSpecs(),
            vectorsList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVectorFrame<T> CreateFreeFrameOfScaledBasis(int vSpaceDimensions, T scalingFactor)
    {
        var vectorsList =
            vSpaceDimensions
                .GetRange()
                .Select(index => VectorTerm(index, scalingFactor));

        return XGaVectorFrame<T>.Create(
            XGaVectorFrameSpecs.CreateScaledBasisSpecs(),
            vectorsList
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVectorFrame<T> CreateFreeFrameOfSimplex(int vSpaceDimensions, T scalingFactor)
    {
        var vSpaceDimensions1 = vSpaceDimensions + 1;

        // Create ones-vector
        var onesVector = VectorSymmetricUnit(vSpaceDimensions1);
        var basisVector = VectorTerm(vSpaceDimensions);

        // Find a rotor that rotates the ones vector into the last basis vector
        var rotor = onesVector.GetEuclideanPureRotorTo(
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
                            VectorTerm(index, scalingFactor)
                        )
                    ).GetVectorPart(i => i < vSpaceDimensions)
                );

        return XGaVectorFrame<T>.Create(
            XGaVectorFrameSpecs.CreateSimplexSpecs(),
            vectorsList
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVectorFrameFixed<T> CreateBasisVectorFrameFixed(int vSpaceDimensions)
    {
        return CreateFreeFrameOfBasis(vSpaceDimensions)
            .CreateFixedFrame();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVectorFrameFixed<T> CreateFixedFrameOfScaledBasis(int vSpaceDimensions, T scalingFactor)
    {
        return CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVectorFrameFixed<T> CreateFixedFrameOfSimplex(int vSpaceDimensions, T scalingFactor)
    {
        return CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor).CreateFixedFrame();
    }
    
    /// <summary>
    /// See the paper "Generalized Clarke Components for Poly-phase Networks", 1969
    /// </summary>
    /// <param name="vectorsCount"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVectorFrame<T> CreateClarkeRotationFrame(int vectorsCount)
    {
        return XGaVectorFrameSpecs
            .CreateUnitBasisSpecs()
            .CreateVectorFrame(
                ScalarProcessor
                    .CreateClarkeRotationArray(vectorsCount)
                    .ColumnsToXGaVectors(this)
            );
    }

    }
}

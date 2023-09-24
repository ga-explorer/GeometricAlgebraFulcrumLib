using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Subspaces;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Frames
{
    public static class RGaFrameComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisVectorFrame<T> CreateBasisVectorFrame<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
        {
            return RGaBasisVectorFrame<T>.Create(
                vSpaceDimensions.GetRange(processor.CreateTermVector)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisVectorFrame<T> CreateBasisVectorFrame<T>(params RGaVector<T>[] vectorArray)
        {
            return RGaBasisVectorFrame<T>.Create(vectorArray);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisKVectorFrame<T> CreateBasisKVectorFrame<T>(this IEnumerable<RGaKVector<T>> kVectorList)
        {
            return RGaBasisKVectorFrame<T>.Create(kVectorList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisKVectorFrame<T> CreateBasisKVectorFrame<T>(this RGaBasisVectorFrame<T> vectorFrame)
        {
            return RGaBasisKVectorFrame<T>.CreateFrom(vectorFrame);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisMultivectorFrame<T> CreateBasisMultivectorFrame<T>(this RGaBasisVectorFrame<T> vectorFrame)
        {
            return RGaBasisMultivectorFrame<T>.CreateFrom(vectorFrame);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrame<T> CreateVectorFrame<T>(this RGaVectorFrameSpecs frameSpecs, RGaVector<T> vector1, RGaVector<T> vector2)
        {
            return RGaVectorFrame<T>.Create(
                frameSpecs,
                new[] { vector1, vector2 }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrame<T> CreateVectorFrame<T>(this RGaVectorFrameSpecs frameSpecs, params RGaVector<T>[] vectorsList)
        {
            return RGaVectorFrame<T>.Create(frameSpecs, vectorsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrame<T> CreateVectorFrame<T>(this RGaVectorFrameSpecs frameSpecs, IEnumerable<RGaVector<T>> vectorsList)
        {
            return RGaVectorFrame<T>.Create(frameSpecs, vectorsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrame<T> CreateFreeFrameOfBasis<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
        {
            var vectorsList =
                vSpaceDimensions
                    .GetRange()
                    .Select(processor.CreateTermVector);

            return RGaVectorFrame<T>.Create(
                RGaVectorFrameSpecs.CreateUnitBasisSpecs(),
                vectorsList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrame<T> CreateFreeFrameOfScaledBasis<T>(this RGaProcessor<T> processor, int vSpaceDimensions, T scalingFactor)
        {
            var vectorsList =
                vSpaceDimensions
                    .GetRange()
                    .Select(index => processor.CreateTermVector(index, scalingFactor));

            return RGaVectorFrame<T>.Create(
                RGaVectorFrameSpecs.CreateScaledBasisSpecs(),
                vectorsList
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrame<T> CreateFreeFrameOfSimplex<T>(this RGaProcessor<T> processor, int vSpaceDimensions, T scalingFactor)
        {
            var vSpaceDimensions1 = vSpaceDimensions + 1;

            // Create ones-vector
            var onesVector = processor.CreateSymmetricUnitVector(vSpaceDimensions1);
            var basisVector = processor.CreateTermVector(vSpaceDimensions);

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
                                processor.CreateTermVector(index, scalingFactor)
                            )
                        ).GetVectorPart(i => i < vSpaceDimensions)
                    );

            return RGaVectorFrame<T>.Create(
                RGaVectorFrameSpecs.CreateSimplexSpecs(),
                vectorsList
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrameFixed<T> CreateFixedFrame<T>(this IRGaVectorFrame<T> frame)
        {
            return RGaVectorFrameFixed<T>.Create(
                frame.Processor.CreateZeroVector(),
                frame
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrameFixed<T> CreateFixedFrame<T>(this IRGaVectorFrame<T> frame, RGaVector<T> point)
        {
            return RGaVectorFrameFixed<T>.Create(point, frame);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrameFixed<T> CreateBasisVectorFrameFixed<T>(this RGaVector<T> point, int vSpaceDimensions)
        {
            return point
                .Processor
                .CreateFreeFrameOfBasis(vSpaceDimensions)
                .CreateFixedFrame(point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrameFixed<T> CreateBasisVectorFrameFixed<T>(this RGaProcessor<T> processor, int vSpaceDimensions)
        {
            return processor
                .CreateFreeFrameOfBasis(vSpaceDimensions)
                .CreateFixedFrame();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrameFixed<T> CreateFixedFrameOfScaledBasis<T>(this RGaProcessor<T> processor, int vSpaceDimensions, T scalingFactor)
        {
            return processor
                .CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
                .CreateFixedFrame();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrameFixed<T> CreateFixedFrameOfScaledBasis<T>(this RGaVector<T> point, int vSpaceDimensions, T scalingFactor)
        {
            return point
                .Processor
                .CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
                .CreateFixedFrame(point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrameFixed<T> CreateFixedFrameOfSimplex<T>(this RGaProcessor<T> processor, int vSpaceDimensions, T scalingFactor)
        {
            return processor.CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor).CreateFixedFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaVectorFrameFixed<T> CreateFixedFrameOfSimplex<T>(this RGaVector<T> point, int vSpaceDimensions, T scalingFactor)
        {
            return point
                .Processor
                .CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor)
                .CreateFixedFrame(point);
        }
    }
}
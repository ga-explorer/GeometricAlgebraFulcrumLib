using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class FrameFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisVectorFrame<T> CreateBasisVectorFrame<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return BasisVectorFrame<T>.Create(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisVectorFrame<T> CreateBasisVectorFrame<T>(this IGeometricAlgebraProcessor<T> processor, params Vector<T>[] vectorArray)
        {
            return BasisVectorFrame<T>.Create(processor, vectorArray);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisKVectorFrame<T> CreateBasisKVectorFrame<T>(this IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return BasisKVectorFrame<T>.Create(geometricProcessor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisKVectorFrame<T> CreateBasisKVectorFrame<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<KVector<T>> kVectorList)
        {
            return BasisKVectorFrame<T>.Create(geometricProcessor, kVectorList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisKVectorFrame<T> CreateBasisKVectorFrame<T>(this BasisVectorFrame<T> vectorFrame)
        {
            return BasisKVectorFrame<T>.CreateFrom(vectorFrame);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisMultivectorFrame<T> CreateBasisMultivectorFrame<T>(this BasisVectorFrame<T> vectorFrame)
        {
            return BasisMultivectorFrame<T>.CreateFrom(vectorFrame);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrame<T> CreateVectorFrame<T>(this IGeometricAlgebraProcessor<T> processor, VectorFrameSpecs frameSpecs)
        {
            return VectorFrame<T>.Create(processor, frameSpecs);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrame<T> CreateVectorFrame<T>(this IGeometricAlgebraProcessor<T> processor, VectorFrameSpecs frameSpecs, Vector<T> vector1, Vector<T> vector2)
        {
            return VectorFrame<T>.Create(
                processor, 
                frameSpecs, 
                new []{ vector1, vector2 }
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrame<T> CreateVectorFrame<T>(this IGeometricAlgebraProcessor<T> processor, VectorFrameSpecs frameSpecs, params Vector<T>[] vectorsList)
        {
            return VectorFrame<T>.Create(processor, frameSpecs, vectorsList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrame<T> CreateVectorFrame<T>(this IGeometricAlgebraProcessor<T> processor, VectorFrameSpecs frameSpecs, IEnumerable<Vector<T>> vectorsList)
        {
            return VectorFrame<T>.Create(processor, frameSpecs, vectorsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrame<T> CreateFreeFrameOfBasis<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            var vectorsList = 
                vSpaceDimension
                    .GetRange()
                    .Select(index => processor.CreateVectorBasis(index));

            return VectorFrame<T>.Create(
                processor, 
                VectorFrameSpecs.CreateUnitBasisSpecs(), 
                vectorsList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrame<T> CreateFreeFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, T scalingFactor)
        {
            var vectorsList = 
                processor.
                    VSpaceDimension
                    .GetRange()
                    .Select(index => processor.CreateVectorTerm(index, scalingFactor));

            return VectorFrame<T>.Create(
                processor, 
                VectorFrameSpecs.CreateScaledBasisSpecs(), 
                vectorsList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrame<T> CreateFreeFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension, T scalingFactor)
        {
            var vectorsList = 
                vSpaceDimension
                    .GetRange()
                    .Select(index => processor.CreateVectorTerm(index, scalingFactor));

            return VectorFrame<T>.Create(
                processor, 
                VectorFrameSpecs.CreateScaledBasisSpecs(), 
                vectorsList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrame<T> CreateFreeFrameOfSimplex<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension, T scalingFactor)
        {
            var processor = 
                scalarProcessor.CreateGeometricAlgebraEuclideanProcessor(vSpaceDimension + 1);

            // Create ones-vector
            var onesVector = processor.CreateVectorStorageSymmetricUnit((int) (vSpaceDimension + 1));
            var basisVector = processor.CreateVectorStorageBasis(vSpaceDimension);
            
            // Find a rotor that rotates the ones vector into the last basis vector
            var rotor = processor.CreatePureRotor(
                processor.CreateVector(onesVector), 
                processor.CreateVector(basisVector),
                true
            );

            // Create a subspace from the dual of the ones vector
            var hyperSubspace = processor.CreateSubspace(
                processor.Dual(onesVector).GetKVectorPart(vSpaceDimension)
            );

            // Project each basis vectors on the subspace, and then rotate
            // the projected vectors and discard the last component of each vector
            var vectorsList = 
                processor.
                    VSpaceDimension
                    .GetRange()
                    .Select(index =>
                        rotor.OmMap(
                            hyperSubspace.Project(
                                processor.CreateVectorTerm(index, scalingFactor)
                            )
                        ).VectorStorage.FilterVectorByIndex(i => i < vSpaceDimension).CreateVector(processor)
                    );

            return VectorFrame<T>.Create(
                scalarProcessor.CreateGeometricAlgebraEuclideanProcessor(vSpaceDimension), 
                VectorFrameSpecs.CreateSimplexSpecs(),
                vectorsList
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateFixedFrame<T>(this IVectorFrame<T> frame)
        {
            return VectorFrameFixed<T>.Create(
                frame.GeometricProcessor.CreateVector(VectorStorage<T>.ZeroVector), 
                frame
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateFixedFrame<T>(this IVectorFrame<T> frame, Vector<T> point)
        {
            return VectorFrameFixed<T>.Create(point, frame);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateBasisVectorFrameFixed<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return processor.CreateBasisVectorFrame().CreateFixedFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateBasisVectorFrameFixed<T>(this IGeometricAlgebraProcessor<T> processor, Vector<T> point)
        {
            return processor.CreateBasisVectorFrameFixed().CreateFixedFrame(point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateBasisVectorFrameFixed<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            return processor.CreateFreeFrameOfBasis(vSpaceDimension).CreateFixedFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateBasisVectorFrameFixed<T>(this IGeometricAlgebraProcessor<T> processor, Vector<T> point, uint vSpaceDimension)
        {
            return processor.CreateFreeFrameOfBasis(vSpaceDimension).CreateFixedFrame(point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateFixedFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, T scalingFactor)
        {
            return processor.CreateFreeFrameOfScaledBasis(scalingFactor).CreateFixedFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateFixedFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, Vector<T> point, T scalingFactor)
        {
            return processor.CreateFreeFrameOfScaledBasis(scalingFactor).CreateFixedFrame(point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateFixedFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension, T scalingFactor)
        {
            return processor.CreateFreeFrameOfScaledBasis(vSpaceDimension, scalingFactor).CreateFixedFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateFixedFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, Vector<T> point, uint vSpaceDimension, T scalingFactor)
        {
            return processor.CreateFreeFrameOfScaledBasis(vSpaceDimension, scalingFactor).CreateFixedFrame(point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateFixedFrameOfSimplex<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension, T scalingFactor)
        {
            return scalarProcessor.CreateFreeFrameOfSimplex(vSpaceDimension, scalingFactor).CreateFixedFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorFrameFixed<T> CreateFixedFrameOfSimplex<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension, Vector<T> point, T scalingFactor)
        {
            return scalarProcessor.CreateFreeFrameOfSimplex(vSpaceDimension, scalingFactor).CreateFixedFrame(point);
        }
    }
}
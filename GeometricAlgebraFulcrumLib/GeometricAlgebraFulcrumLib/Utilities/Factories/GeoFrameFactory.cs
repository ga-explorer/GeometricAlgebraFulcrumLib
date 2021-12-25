using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Geometry.Frames;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GeoFrameFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrame<T>(this IGeometricAlgebraProcessor<T> processor, GeoFreeFrameSpecs frameSpecs)
        {
            return new GeoFreeFrame<T>(processor, frameSpecs);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrame<T>(this IGeometricAlgebraProcessor<T> processor, GeoFreeFrameSpecs frameSpecs, VectorStorage<T> vector1, VectorStorage<T> vector2)
        {
            return new GeoFreeFrame<T>(
                processor, 
                frameSpecs, 
                new []{ vector1, vector2 }
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrame<T>(this IGeometricAlgebraProcessor<T> processor, GeoFreeFrameSpecs frameSpecs, params VectorStorage<T>[] vectorsList)
        {
            return new GeoFreeFrame<T>(processor, frameSpecs, vectorsList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrame<T>(this IGeometricAlgebraProcessor<T> processor, GeoFreeFrameSpecs frameSpecs, IEnumerable<VectorStorage<T>> vectorsList)
        {
            return new GeoFreeFrame<T>(processor, frameSpecs, vectorsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrameOfBasis<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            var vectorsList = 
                processor.
                    VSpaceDimension
                    .GetRange()
                    .Select(index => processor.CreateVectorBasisStorage(index));

            return new GeoFreeFrame<T>(
                processor, 
                GeoFreeFrameSpecs.CreateUnitBasisSpecs(), 
                vectorsList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrameOfBasis<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            var vectorsList = 
                vSpaceDimension
                    .GetRange()
                    .Select(index => processor.CreateVectorBasisStorage(index));

            return new GeoFreeFrame<T>(
                processor, 
                GeoFreeFrameSpecs.CreateUnitBasisSpecs(), 
                vectorsList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, T scalingFactor)
        {
            var vectorsList = 
                processor.
                    VSpaceDimension
                    .GetRange()
                    .Select(index => processor.CreateVectorTermStorage(index, scalingFactor));

            return new GeoFreeFrame<T>(
                processor, 
                GeoFreeFrameSpecs.CreateScaledBasisSpecs(), 
                vectorsList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension, T scalingFactor)
        {
            var vectorsList = 
                vSpaceDimension
                    .GetRange()
                    .Select(index => processor.CreateVectorTermStorage(index, scalingFactor));

            return new GeoFreeFrame<T>(
                processor, 
                GeoFreeFrameSpecs.CreateScaledBasisSpecs(), 
                vectorsList
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFreeFrame<T> CreateFreeFrameOfSimplex<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension, T scalingFactor)
        {
            var processor = 
                scalarProcessor.CreateGeometricAlgebraEuclideanProcessor(vSpaceDimension + 1);

            // Create ones-vector
            var onesVector = processor.CreateStorageVectorUnitOnes((int) (vSpaceDimension + 1));
            var basisVector = processor.CreateVectorBasisStorage(vSpaceDimension);
            
            // Find a rotor that rotates the ones vector into the last basis vector
            var rotor = processor.CreatePureRotor(
                onesVector, 
                basisVector,
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
                        rotor.OmMapVector(
                            hyperSubspace.Project(
                                processor.CreateVectorTermStorage(index, scalingFactor)
                            )
                        ).FilterVectorByIndex(i => i < vSpaceDimension)
                    );

            return new GeoFreeFrame<T>(
                scalarProcessor.CreateGeometricAlgebraEuclideanProcessor(vSpaceDimension), 
                GeoFreeFrameSpecs.CreateSimplexSpecs(),
                vectorsList
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrame<T>(this GeoFreeFrame<T> frame)
        {
            return new GeoFixedFrame<T>(VectorStorage<T>.ZeroVector, frame);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrame<T>(this GeoFreeFrame<T> frame, VectorStorage<T> point)
        {
            return new GeoFixedFrame<T>(point, frame);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrameOfBasis<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return processor.CreateFreeFrameOfBasis().CreateFixedFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrameOfBasis<T>(this IGeometricAlgebraProcessor<T> processor, VectorStorage<T> point)
        {
            return processor.CreateFreeFrameOfBasis().CreateFixedFrame(point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrameOfBasis<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            return processor.CreateFreeFrameOfBasis(vSpaceDimension).CreateFixedFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrameOfBasis<T>(this IGeometricAlgebraProcessor<T> processor, VectorStorage<T> point, uint vSpaceDimension)
        {
            return processor.CreateFreeFrameOfBasis(vSpaceDimension).CreateFixedFrame(point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, T scalingFactor)
        {
            return processor.CreateFreeFrameOfScaledBasis(scalingFactor).CreateFixedFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, VectorStorage<T> point, T scalingFactor)
        {
            return processor.CreateFreeFrameOfScaledBasis(scalingFactor).CreateFixedFrame(point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension, T scalingFactor)
        {
            return processor.CreateFreeFrameOfScaledBasis(vSpaceDimension, scalingFactor).CreateFixedFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrameOfScaledBasis<T>(this IGeometricAlgebraProcessor<T> processor, VectorStorage<T> point, uint vSpaceDimension, T scalingFactor)
        {
            return processor.CreateFreeFrameOfScaledBasis(vSpaceDimension, scalingFactor).CreateFixedFrame(point);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrameOfSimplex<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension, T scalingFactor)
        {
            return scalarProcessor.CreateFreeFrameOfSimplex(vSpaceDimension, scalingFactor).CreateFixedFrame();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoFixedFrame<T> CreateFixedFrameOfSimplex<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension, VectorStorage<T> point, T scalingFactor)
        {
            return scalarProcessor.CreateFreeFrameOfSimplex(vSpaceDimension, scalingFactor).CreateFixedFrame(point);
        }
    }
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SignalAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class ProcessorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64Processor CreateFloat64ScalarSignalProcessor(double samplingRate, int sampleCount)
        {
            return ScalarSignalFloat64Processor.Create(samplingRate, sampleCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalProcessor<T> CreateSignalAlgebraProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int signalSamplesCount)
        {
            return ScalarSignalProcessor<T>.Create(scalarProcessor, signalSamplesCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarTupleProcessor<T> CreateScalarAlgebraTupleProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, int tupleSize)
        {
            return ScalarTupleProcessor<T>.Create(scalarProcessor, tupleSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearAlgebraProcessor<T> CreateLinearAlgebraProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new LinearAlgebraProcessor<T>(scalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraEuclideanProcessor<T> CreateGeometricAlgebraEuclideanProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var geometricProcessor = new GeometricAlgebraEuclideanProcessor<T>(
                scalarProcessor,
                vSpaceDimension
            );

            if (scalarProcessor is MetaContext context)
                context.GeometricProcessor = geometricProcessor as 
                    IGeometricAlgebraEuclideanProcessor<IMetaExpressionAtomic>;

            return geometricProcessor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraConformalProcessor<T> CreateGeometricAlgebraConformalProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var geometricProcessor = new GeometricAlgebraConformalProcessor<T>(
                scalarProcessor, 
                vSpaceDimension
            );

            if (scalarProcessor is MetaContext context)
                context.GeometricProcessor = geometricProcessor as 
                    IGeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic>;

            return geometricProcessor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraProjectiveProcessor<T> CreateGeometricAlgebraProjectiveProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            var geometricProcessor = new GeometricAlgebraProjectiveProcessor<T>(
                scalarProcessor, 
                vSpaceDimension
            );

            if (scalarProcessor is MetaContext context)
                context.GeometricProcessor = geometricProcessor as 
                    IGeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic>;

            return geometricProcessor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraOrthonormalProcessor<T> CreateGeometricAlgebraOrthonormalProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount)
        {
            var geometricProcessor = new GeometricAlgebraOrthonormalProcessor<T>(
                scalarProcessor, 
                BasisBladeSet.Create(positiveCount, negativeCount)
            );

            if (scalarProcessor is MetaContext context)
                context.GeometricProcessor = geometricProcessor as 
                    IGeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic>;

            return geometricProcessor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraOrthonormalProcessor<T> CreateGeometricAlgebraOrthonormalProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount, uint zeroCount)
        {
            var geometricProcessor = new GeometricAlgebraOrthonormalProcessor<T>(
                scalarProcessor, 
                BasisBladeSet.Create(positiveCount, negativeCount, zeroCount)
            );

            if (scalarProcessor is MetaContext context)
                context.GeometricProcessor = geometricProcessor as 
                    IGeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic>;

            return geometricProcessor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraEuclideanProcessor<IMetaExpressionAtomic> CreateGeometricAlgebraEuclideanProcessor(this MetaContext context, uint vSpaceDimension)
        {
            var processor = new GeometricAlgebraEuclideanProcessor<IMetaExpressionAtomic>(
                context,
                vSpaceDimension
            );

            context.GeometricProcessor = processor;

            return processor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraConformalProcessor<IMetaExpressionAtomic> CreateGeometricAlgebraConformalProcessor(this MetaContext context, uint vSpaceDimension)
        {
            var processor = new GeometricAlgebraConformalProcessor<IMetaExpressionAtomic>(
                context, 
                vSpaceDimension
            );
            
            context.GeometricProcessor = processor;

            return processor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraProjectiveProcessor<IMetaExpressionAtomic> CreateGeometricAlgebraProjectiveProcessor(this MetaContext context, uint vSpaceDimension)
        {
            var processor = new GeometricAlgebraProjectiveProcessor<IMetaExpressionAtomic>(
                context, 
                vSpaceDimension
            );
            
            context.GeometricProcessor = processor;

            return processor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic> CreateGeometricAlgebraOrthonormalProcessor(this MetaContext context, uint positiveCount, uint negativeCount)
        {
            var processor = new GeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic>(
                context, 
                BasisBladeSet.Create(positiveCount, negativeCount)
            );
            
            context.GeometricProcessor = processor;

            return processor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic> CreateGeometricAlgebraOrthonormalProcessor(this MetaContext context, uint positiveCount, uint negativeCount, uint zeroCount)
        {
            var processor = new GeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic>(
                context, 
                BasisBladeSet.Create(positiveCount, negativeCount, zeroCount)
            );
            
            context.GeometricProcessor = processor;

            return processor;
        }
    }
}
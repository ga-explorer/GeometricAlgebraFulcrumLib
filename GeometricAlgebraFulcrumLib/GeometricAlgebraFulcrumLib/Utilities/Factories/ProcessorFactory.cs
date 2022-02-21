using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.SymbolicAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class ProcessorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearAlgebraProcessor<T> CreateLinearAlgebraProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new LinearAlgebraProcessor<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraEuclideanProcessor<T> CreateGeometricAlgebraEuclideanProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GeometricAlgebraEuclideanProcessor<T>(
                scalarProcessor,
                vSpaceDimension
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraConformalProcessor<T> CreateGeometricAlgebraConformalProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GeometricAlgebraConformalProcessor<T>(scalarProcessor, vSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraProjectiveProcessor<T> CreateGeometricAlgebraProjectiveProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GeometricAlgebraProjectiveProcessor<T>(scalarProcessor, vSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraOrthonormalProcessor<T> CreateGeometricAlgebraOrthonormalProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount)
        {
            return new GeometricAlgebraOrthonormalProcessor<T>(
                scalarProcessor, 
                BasisBladeSet.Create(positiveCount, negativeCount)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraOrthonormalProcessor<T> CreateGeometricAlgebraOrthonormalProcessor<T>(this IScalarAlgebraProcessor<T> scalarProcessor, uint positiveCount, uint negativeCount, uint zeroCount)
        {
            return new GeometricAlgebraOrthonormalProcessor<T>(
                scalarProcessor, 
                BasisBladeSet.Create(positiveCount, negativeCount, zeroCount)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraEuclideanProcessor<ISymbolicExpressionAtomic> AttachGeometricAlgebraEuclideanProcessor(this SymbolicContext context, uint vSpaceDimension)
        {
            var processor = new GeometricAlgebraEuclideanProcessor<ISymbolicExpressionAtomic>(
                context,
                vSpaceDimension
            );

            context.GeometricProcessor = processor;

            return processor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraConformalProcessor<ISymbolicExpressionAtomic> AttachGeometricAlgebraConformalProcessor(this SymbolicContext context, uint vSpaceDimension)
        {
            var processor = new GeometricAlgebraConformalProcessor<ISymbolicExpressionAtomic>(
                context, 
                vSpaceDimension
            );
            
            context.GeometricProcessor = processor;

            return processor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraProjectiveProcessor<ISymbolicExpressionAtomic> AttachGeometricAlgebraProjectiveProcessor(this SymbolicContext context, uint vSpaceDimension)
        {
            var processor = new GeometricAlgebraProjectiveProcessor<ISymbolicExpressionAtomic>(
                context, 
                vSpaceDimension
            );
            
            context.GeometricProcessor = processor;

            return processor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraOrthonormalProcessor<ISymbolicExpressionAtomic> AttachGeometricAlgebraOrthonormalProcessor(this SymbolicContext context, uint positiveCount, uint negativeCount)
        {
            var processor = new GeometricAlgebraOrthonormalProcessor<ISymbolicExpressionAtomic>(
                context, 
                BasisBladeSet.Create(positiveCount, negativeCount)
            );
            
            context.GeometricProcessor = processor;

            return processor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeometricAlgebraOrthonormalProcessor<ISymbolicExpressionAtomic> AttachGeometricAlgebraOrthonormalProcessor(this SymbolicContext context, uint positiveCount, uint negativeCount, uint zeroCount)
        {
            var processor = new GeometricAlgebraOrthonormalProcessor<ISymbolicExpressionAtomic>(
                context, 
                BasisBladeSet.Create(positiveCount, negativeCount, zeroCount)
            );
            
            context.GeometricProcessor = processor;

            return processor;
        }
    }
}
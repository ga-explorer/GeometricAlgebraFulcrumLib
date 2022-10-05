using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Conformal
{
    public class ConformalIpnsPoint<T> :
        ConformalIpnsVectorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsPoint<T> operator *(ConformalIpnsPoint<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsPoint<T>(
                processor,
                processor.Times(mv.VectorStorage, s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsPoint<T> operator *(T s, ConformalIpnsPoint<T> mv)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsPoint<T>(
                processor,
                processor.Times(s, mv.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsPoint<T> operator /(ConformalIpnsPoint<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsPoint<T>(
                processor,
                processor.Divide(mv.VectorStorage, s)
            );
        }


        internal ConformalIpnsPoint([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] VectorStorage<T> vectorStorage)
            : base(conformalProcessor, vectorStorage)
        {
        }

        internal ConformalIpnsPoint([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] VectorStorage<T> vectorStorage, bool assumeUnitWeight)
            : base(conformalProcessor, vectorStorage, assumeUnitWeight)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> GetPosition()
        {
            return ConformalProcessor.Divide(
                VectorStorage.FilterVectorByIndex(index => index < ConformalProcessor.VSpaceDimension - 2),
                Weight()
            ).CreateVector(ConformalProcessor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ConformalIpnsPoint<T> GetNormalizedPoint()
        {
            if (AssumeUnitWeight)
                return this;

            var vector = ConformalProcessor.Divide(
                VectorStorage,
                Weight()
            );

            return new ConformalIpnsPoint<T>(ConformalProcessor, vector, true);
        }
    }
}
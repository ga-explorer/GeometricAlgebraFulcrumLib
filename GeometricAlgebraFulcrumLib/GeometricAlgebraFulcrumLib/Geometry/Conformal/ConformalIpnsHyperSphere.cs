using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Conformal
{
    public class ConformalIpnsHyperSphere<T> :
        ConformalIpnsVectorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsHyperSphere<T> operator *(ConformalIpnsHyperSphere<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsHyperSphere<T>(
                processor,
                processor.Times(mv.VectorStorage, s)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsHyperSphere<T> operator *(T s, ConformalIpnsHyperSphere<T> mv)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsHyperSphere<T>(
                processor,
                processor.Times(s, mv.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConformalIpnsHyperSphere<T> operator /(ConformalIpnsHyperSphere<T> mv, T s)
        {
            var processor = mv.ConformalProcessor;

            return new ConformalIpnsHyperSphere<T>(
                processor,
                processor.Divide(mv.VectorStorage, s)
            );
        }


        internal ConformalIpnsHyperSphere([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] VectorStorage<T> vectorStorage)
            : base(conformalProcessor, vectorStorage)
        {
        }

        internal ConformalIpnsHyperSphere([NotNull] GeometricAlgebraConformalProcessor<T> conformalProcessor, [NotNull] VectorStorage<T> vectorStorage, bool assumeUnitWeight)
            : base(conformalProcessor, vectorStorage, assumeUnitWeight)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetCenter()
        {
            return VectorStorage
                .FilterVectorByIndex(index => index < ConformalProcessor.VSpaceDimension - 2)
                .CreateVector(ConformalProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetRadiusSquared()
        {
            return MultivectorStorageSpUtils.SpSquared(ConformalProcessor, VectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetRadius()
        {
            return ConformalProcessor.SqrtOfAbs(GetRadiusSquared());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ConformalOpnsHyperSphere<T> ToOpnsHyperSphere()
        {
            return new ConformalOpnsHyperSphere<T>(
                ConformalProcessor,
                ConformalProcessor
                    .UnDual(VectorStorage)
                    .GetKVectorPart(ConformalProcessor.VSpaceDimension - 1)
            );
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ConformalIpnsHyperSphere<T> GetNormalizedSphere()
        {
            if (AssumeUnitWeight)
                return this;

            var vector = ConformalProcessor.Divide(
                VectorStorage,
                Weight()
            );

            return new ConformalIpnsHyperSphere<T>(ConformalProcessor, vector, true);
        }
    }
}
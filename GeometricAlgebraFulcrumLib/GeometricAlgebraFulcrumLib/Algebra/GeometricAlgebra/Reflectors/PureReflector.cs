using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Reflectors
{
    /// <summary>
    /// A pure (direct) reflector is a single vector.
    /// The reflection happens in the unit vector itself, not its dual hyperplane
    /// like the case for pure versors.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PureReflector<T>
        : ReflectorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureReflector<T> Create(IGeometricAlgebraProcessor<T> processor, VectorStorage<T> vector)
        {
            return new PureReflector<T>(processor, vector);
        }
        

        public VectorStorage<T> Vector { get; }

        public VectorStorage<T> VectorInverse { get; }


        internal PureReflector([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] VectorStorage<T> vector)
            : base(processor)
        {
            Vector = vector;
            VectorInverse = processor.BladeInverse(vector);
        }

        private PureReflector([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] VectorStorage<T> vector, [NotNull] VectorStorage<T> vectorInverse)
            : base(processor)
        {
            Vector = vector;
            VectorInverse = vectorInverse;
        }


        public override bool IsValid()
        {
            // Make sure the storage and its reverse are correct
            if (!GeometricProcessor.IsNearZero(GeometricProcessor.Subtract(GeometricProcessor.BladeInverse(Vector), VectorInverse)))
                return false;

            //// Make sure storage gp inverse(storage) == 1
            //var gp = 
            //    GeometricProcessor.Gp(Vector, VectorInverse);

            //if (!gp.IsScalar())
            //    return false;

            //var diff =
            //    GeometricProcessor.Subtract(
            //        GeometricProcessor.GetTermScalar(gp, 0),
            //        GeometricProcessor.ScalarOne
            //    );

            //return GeometricProcessor.IsNearZero(diff);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureReflector<T> GetPureReflectorInverse()
        {
            return new PureReflector<T>(
                GeometricProcessor, 
                VectorInverse, 
                Vector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReflector<T> GetReflectorInverse()
        {
            return GetPureReflectorInverse();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> OmMapVector(VectorStorage<T> mv)
        {
            return GeometricProcessor
                .Gp(Vector, mv, VectorInverse)
                .GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBivector(BivectorStorage<T> mv)
        {
            return GeometricProcessor
                .Gp(Vector, mv, VectorInverse)
                .GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapKVector(KVectorStorage<T> mv)
        {
            return GeometricProcessor
                .Gp(Vector, mv, VectorInverse)
                .GetKVectorPart(mv.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> mv)
        {
            return GeometricProcessor
                .Gp(Vector, mv, VectorInverse)
                .ToMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> mv)
        {
            return GeometricProcessor
                .Gp(Vector, mv, VectorInverse)
                .ToMultivectorStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorage()
        {
            return Vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorInverseStorage()
        {
            return VectorInverse;
        }
    }
}
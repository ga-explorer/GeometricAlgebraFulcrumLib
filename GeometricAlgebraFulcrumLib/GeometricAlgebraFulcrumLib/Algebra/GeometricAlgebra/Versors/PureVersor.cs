using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors
{
    /// <summary>
    /// This class represents a Householder reflection on a hyper-space represented
    /// using its dual unit vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PureVersor<T> 
        : VersorBase<T>
    {
        public static PureVersor<T> Create(IGeometricAlgebraProcessor<T> processor, VectorStorage<T> unitVectorStorage)
        {
            return new PureVersor<T>(processor, unitVectorStorage);
        }


        public VectorStorage<T> Vector { get; }

        public VectorStorage<T> VectorInverse { get; }

        
        internal PureVersor([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] VectorStorage<T> vector)
            : base(processor)
        {
            Vector = vector;
            VectorInverse = processor.BladeInverse(vector);
        }

        private PureVersor([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] VectorStorage<T> vector, [NotNull] VectorStorage<T> vectorInverse)
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

            // Make sure storage gp reverse(storage) == 1
            var gp = 
                GeometricProcessor.Gp(Vector, VectorInverse);

            if (!gp.IsScalar())
                return false;

            var diff =
                GeometricProcessor.Subtract(
                    GeometricProcessor.Abs(GeometricProcessor.GetScalar(gp)),
                    GeometricProcessor.ScalarOne
                );

            return GeometricProcessor.IsNearZero(diff);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureVersor<T> GetPureDualVersorInverse()
        {
            return new PureVersor<T>(GeometricProcessor, VectorInverse, Vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IVersor<T> GetVersorInverse()
        {
            return GetPureDualVersorInverse();
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> OmMapVector(VectorStorage<T> mv)
        {
            return GeometricProcessor.Gp(
                Vector, 
                GeometricProcessor.Negative(mv),
                VectorInverse
            ).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBivector(BivectorStorage<T> mv)
        {
            return GeometricProcessor.Gp(
                Vector,
                mv,
                VectorInverse
            ).GetBivectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapKVector(KVectorStorage<T> mv)
        {
            return GeometricProcessor.Gp(
                Vector, 
                GeometricProcessor.GradeInvolution(mv),
                VectorInverse
            ).GetKVectorPart(mv.Grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> mv)
        {
            return GeometricProcessor.Gp(
                Vector, 
                GeometricProcessor.GradeInvolution(mv),
                VectorInverse
            ).ToMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> storage)
        {
            return GeometricProcessor.Gp(
                Vector, 
                GeometricProcessor.GradeInvolution(storage),
                VectorInverse
            ).ToMultivectorStorage();
        }
    }
}
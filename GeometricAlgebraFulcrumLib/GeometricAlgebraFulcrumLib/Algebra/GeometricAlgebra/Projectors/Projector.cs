using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Projectors
{
    public sealed class Projector<T> :
        OutermorphismBase<T>,
        IMultivectorStorageContainer<T>,
        IProjector<T>
    {
        public override ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }
        
        public KVectorStorage<T> Blade { get; }

        public KVectorStorage<T> BladeInverse { get; }


        internal Projector([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, [NotNull] KVectorStorage<T> blade)
        {
            GeometricProcessor = geometricProcessor;
            Blade = blade;
            BladeInverse = geometricProcessor.BladeInverse(blade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades()
        {
            return GeometricProcessor
                .GaSpaceDimension
                .GetRange()
                .Select(id => 
                    new IdMultivectorStorageRecord<T>(
                        id,
                        OmMapBasisBlade(id)
                    )
                )
                .Where(r => !r.Storage.IsEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IOutermorphism<T> GetOmAdjoint()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> OmMapBasisVector(ulong index)
        {
            return OmMapVector(
                GeometricProcessor.CreateVectorBasisStorage(index)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBasisBivector(ulong index)
        {
            return OmMapBivector(
                GeometricProcessor.CreateBivectorBasisStorage(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            return OmMapBivector(
                GeometricProcessor.CreateBivectorBasisStorage(index1, index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapBasisBlade(ulong id)
        {
            return OmMapKVector(
                GeometricProcessor.CreateKVectorBasisStorage(id)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return OmMapKVector(
                GeometricProcessor.CreateKVectorBasisStorage(grade, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> OmMapVector(VectorStorage<T> vector)
        {
            return GeometricProcessor.Lcp(
                GeometricProcessor.Lcp(vector, Blade), 
                BladeInverse
            ).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBivector(BivectorStorage<T> bivector)
        {
            return GeometricProcessor.Lcp(
                GeometricProcessor.Lcp(bivector, Blade), 
                BladeInverse
            ).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapKVector(KVectorStorage<T> kVector)
        {
            return GeometricProcessor.Lcp(
                GeometricProcessor.Lcp(kVector, Blade), 
                BladeInverse
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> multivector)
        {
            return GeometricProcessor.Lcp(
                GeometricProcessor.Lcp(multivector, Blade), 
                BladeInverse
            ).ToMultivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> multivector)
        {
            return GeometricProcessor.Lcp(
                GeometricProcessor.Lcp(multivector, Blade), 
                BladeInverse
            ).ToMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetVectorOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetBivectorOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IndexVectorStorageRecord<T>> GetOmMappedBasisVectors()
        {
            return ((ulong) GeometricProcessor.VSpaceDimension)
                .GetRange()
                .Select(index => 
                    new IndexVectorStorageRecord<T>(
                        index,
                        OmMapBasisVector(index)
                    )
                )
                .Where(r => !r.Storage.IsEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> GetMultivectorStorage()
        {
            return Blade;
        }
    }
}
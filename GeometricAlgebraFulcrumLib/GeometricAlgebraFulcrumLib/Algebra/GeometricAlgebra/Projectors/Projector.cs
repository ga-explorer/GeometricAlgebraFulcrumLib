using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
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
        public override IGeometricAlgebraProcessor<T> GeometricProcessor { get; }
        
        public KVector<T> Blade { get; }

        public KVector<T> BladePseudoInverse { get; }


        internal Projector([NotNull] KVector<T> blade)
        {
            GeometricProcessor = blade.GeometricProcessor;
            Blade = blade;
            BladePseudoInverse = blade.PseudoInverse();
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
        public override IEnumerable<IdMultivectorRecord<T>> GetMappedBasisBlades()
        {
            return GeometricProcessor
                .GaSpaceDimension
                .GetRange()
                .Select(id => 
                    new IdMultivectorRecord<T>(
                        id,
                        OmMapBasisBlade(id).AsMultivector()
                    )
                )
                .Where(r => !r.Multivector.MultivectorStorage.IsEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IOutermorphism<T> GetOmAdjoint()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector<T> OmMapBasisVector(ulong index)
        {
            return OmMap(
                GeometricProcessor.CreateVectorBasis(index)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Bivector<T> OmMapBasisBivector(ulong index)
        {
            return OmMap(
                GeometricProcessor.CreateBivectorBasis(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Bivector<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            return OmMap(
                GeometricProcessor.CreateBivectorBasis(index1, index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVector<T> OmMapBasisBlade(ulong id)
        {
            return OmMap(
                GeometricProcessor.CreateKVectorStorageBasis(id).CreateKVector(GeometricProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVector<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return OmMap(
                GeometricProcessor.CreateKVectorStorageBasis(grade, index).CreateKVector(GeometricProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector<T> OmMap(Vector<T> vector)
        {
            return vector.Lcp(BladePseudoInverse).Lcp(Blade).AsVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Bivector<T> OmMap(Bivector<T> bivector)
        {
            return bivector.Lcp(BladePseudoInverse).Lcp(Blade).AsBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVector<T> OmMap(KVector<T> kVector)
        {
            return kVector.Lcp(BladePseudoInverse).Lcp(Blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> OmMap(Multivector<T> multivector)
        {
            return multivector.Lcp(BladePseudoInverse).Lcp(Blade);
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
        public override IEnumerable<IndexVectorRecord<T>> GetOmMappedBasisVectors()
        {
            return ((ulong) GeometricProcessor.VSpaceDimension)
                .GetRange()
                .Select(index => 
                    new IndexVectorRecord<T>(
                        index,
                        OmMapBasisVector(index)
                    )
                )
                .Where(r => !r.Vector.VectorStorage.IsEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> GetMultivectorStorage()
        {
            return Blade.KVectorStorage;
        }
    }
}
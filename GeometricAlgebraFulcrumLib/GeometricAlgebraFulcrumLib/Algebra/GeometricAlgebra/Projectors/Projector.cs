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
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Projectors
{
    public sealed class Projector<T> :
        GaOutermorphismBase<T>,
        IMultivectorStorageContainer<T>,
        IProjector<T>
    {
        public override IGeometricAlgebraProcessor<T> GeometricProcessor { get; }
        
        public GaKVector<T> Blade { get; }

        public GaKVector<T> BladePseudoInverse { get; }


        internal Projector([NotNull] GaKVector<T> blade)
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
        public override ILinMatrixStorage<T> GetMultivectorMappingMatrixStorage()
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
        public override IGaOutermorphism<T> GetOmAdjoint()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaVector<T> OmMapBasisVector(ulong index)
        {
            return OmMap(
                GeometricProcessor.CreateVectorBasis(index)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMapBasisBivector(ulong index)
        {
            return OmMap(
                GeometricProcessor.CreateBivectorBasis(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            return OmMap(
                GeometricProcessor.CreateBivectorBasis(index1, index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMapBasisBlade(ulong id)
        {
            return OmMap(
                GeometricProcessor.CreateKVectorStorageBasis(id).CreateKVector(GeometricProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return OmMap(
                GeometricProcessor.CreateKVectorStorageBasis(grade, index).CreateKVector(GeometricProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaVector<T> OmMap(GaVector<T> vector)
        {
            return vector.Lcp(BladePseudoInverse).Lcp(Blade).AsVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMap(GaBivector<T> bivector)
        {
            return bivector.Lcp(BladePseudoInverse).Lcp(Blade).AsBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMap(GaKVector<T> kVector)
        {
            return kVector.Lcp(BladePseudoInverse).Lcp(Blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> OmMap(GaMultivector<T> multivector)
        {
            return multivector.Lcp(BladePseudoInverse).Lcp(Blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetVectorOmMappingMatrixStorage()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetBivectorOmMappingMatrixStorage()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetKVectorOmMappingMatrixStorage(uint grade)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrixStorage()
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
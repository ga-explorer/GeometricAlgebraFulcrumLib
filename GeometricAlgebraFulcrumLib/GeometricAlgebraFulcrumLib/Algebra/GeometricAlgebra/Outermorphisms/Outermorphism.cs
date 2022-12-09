using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public class Outermorphism<T> :
        GaOutermorphismBase<T>
    {
        public override IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public OutermorphismStorage<T> OmStorage { get; }


        internal Outermorphism([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, [NotNull] OutermorphismStorage<T> omStorage)
        {
            GeometricProcessor = geometricProcessor;
            OmStorage = omStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return true;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaOutermorphism<T> GetOmAdjoint()
        {
            return new Outermorphism<T>(
                GeometricProcessor,
                OmStorage.GetTranspose()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaVector<T> OmMapBasisVector(ulong index)
        {
            return GeometricProcessor.CreateVector(
                OmStorage.GetMappedBasisVector(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMapBasisBivector(ulong index)
        {
            return GeometricProcessor.CreateBivector(
                OmStorage.GetMappedBasisBivector(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            return GeometricProcessor.CreateBivector(
                OmStorage.GetMappedBasisBivector(index1, index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMapBasisBlade(ulong id)
        {
            return GeometricProcessor.CreateKVector(
                OmStorage.GetMappedBasisBlade(id)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return GeometricProcessor.CreateKVector(
                OmStorage.GetMappedBasisBlade(grade, index)
            );
        }


        public override GaVector<T> OmMap(GaVector<T> vector)
        {
            var composer = LinearProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in vector.VectorStorage.GetIndexScalarRecords())
                composer.AddScaledTerms(
                    scalar,
                    OmMapBasisVector(index).VectorStorage.GetIndexScalarRecords()
                );

            return composer.CreateVector();
        }

        public override GaBivector<T> OmMap(GaBivector<T> bivector)
        {
            var composer = LinearProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in bivector.BivectorStorage.GetIndexScalarRecords())
                composer.AddScaledTerms(
                    scalar,
                    OmMapBasisBivector(index).BivectorStorage.GetIndexScalarRecords()
                );

            return composer.CreateBivector();
        }

        public override GaKVector<T> OmMap(GaKVector<T> kVector)
        {
            var composer = LinearProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in kVector.KVectorStorage.GetIndexScalarRecords())
                composer.AddScaledTerms(
                    scalar,
                    OmMapBasisVector(index).VectorStorage.GetIndexScalarRecords()
                );

            return composer.CreateKVector(kVector.Grade);
        }

        public override GaMultivector<T> OmMap(GaMultivector<T> multivector)
        {
            var composer = LinearProcessor.CreateVectorStorageComposer();

            foreach (var (id, scalar) in multivector.MultivectorStorage.GetIdScalarRecords())
                composer.AddScaledTerms(
                    scalar,
                    OmMapBasisBlade(id).KVectorStorage.GetIdScalarRecords()
                );

            return composer.CreateMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetMultivectorMappingMatrixStorage()
        {
            return OmStorage
                .GetMultivectorGradedMappingMatrix()
                .ToMatrixStorage(BasisBladeUtils.BasisBladeGradeIndexToId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetVectorOmMappingMatrixStorage()
        {
            return OmStorage.GetVectorMappingMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetBivectorOmMappingMatrixStorage()
        {
            return OmStorage.GetBivectorMappingMatrix();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetKVectorOmMappingMatrixStorage(uint grade)
        {
            return OmStorage.GetKVectorMappingMatrix(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrixStorage()
        {
            return OmStorage.GetMultivectorGradedMappingMatrix();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IdMultivectorRecord<T>> GetMappedBasisBlades()
        {
            return OmStorage
                .GetMappedBasisBladesById()
                .Select(r => 
                    new IdMultivectorRecord<T>(
                        r.Id, 
                        r.Storage.CreateMultivector(GeometricProcessor)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IndexVectorRecord<T>> GetOmMappedBasisVectors()
        {
            return OmStorage
                .GetVectorMappingMatrix()
                .GetColumns()
                .Select(r => 
                    new IndexVectorRecord<T>(
                        r.Index, 
                        r.Storage.CreateVector(GeometricProcessor)
                    )
            );
        }
    }
}
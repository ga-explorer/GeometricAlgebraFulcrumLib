using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public abstract class GaOutermorphismBase<T> :
        IGaOutermorphism<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public abstract IGeometricAlgebraProcessor<T> GeometricProcessor { get; }


        public abstract bool IsValid();

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsInvalid()
        {
            return !IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaUnilinearMap<T> GetAdjoint()
        {
            return GetOmAdjoint();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisScalar()
        {
            return GeometricProcessor.CreateMultivector(
                LinearProcessor.CreateKVectorStorageBasisScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisVector(ulong index)
        {
            return OmMapBasisVector(index).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBivector(ulong index)
        {
            return OmMapBasisBivector(index).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return OmMapBasisBivector(index1, index2).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBlade(ulong id)
        {
            return OmMapBasisBlade(id).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBlade(uint grade, ulong index)
        {
            return OmMapBasisBlade(grade, index).AsMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Map(T scalar)
        {
            return GeometricProcessor.CreateMultivector(
                scalar.CreateKVectorStorageScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Map(GaVector<T> vector)
        {
            return OmMap(vector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Map(GaBivector<T> bivector)
        {
            return OmMap(bivector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Map(GaKVector<T> kVector)
        {
            return OmMap(kVector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Map(GaMultivector<T> multivector)
        {
            return OmMap(multivector);
        }

        public abstract ILinMatrixStorage<T> GetMultivectorMappingMatrixStorage();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrix<T> GetMultivectorMappingMatrix()
        {
            return GetMultivectorMappingMatrixStorage().CreateLinMatrix(LinearProcessor);
        }

        public abstract IEnumerable<IdMultivectorRecord<T>> GetMappedBasisBlades();
        
        public abstract IGaOutermorphism<T> GetOmAdjoint();
        
        public abstract GaVector<T> OmMapBasisVector(ulong index);
        
        public abstract GaBivector<T> OmMapBasisBivector(ulong index);
        
        public abstract GaBivector<T> OmMapBasisBivector(ulong index1, ulong index2);
        
        public abstract GaKVector<T> OmMapBasisBlade(ulong id);
        
        public abstract GaKVector<T> OmMapBasisBlade(uint grade, ulong index);
        
        public abstract GaVector<T> OmMap(GaVector<T> vector);
        
        public abstract GaBivector<T> OmMap(GaBivector<T> bivector);
        
        public abstract GaKVector<T> OmMap(GaKVector<T> kVector);
        
        public abstract GaMultivector<T> OmMap(GaMultivector<T> multivector);

        public abstract ILinMatrixStorage<T> GetVectorOmMappingMatrixStorage();
        
        public abstract ILinMatrixStorage<T> GetBivectorOmMappingMatrixStorage();
        
        public abstract ILinMatrixStorage<T> GetKVectorOmMappingMatrixStorage(uint grade);
        
        public abstract ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrixStorage();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrix<T> GetVectorOmMappingMatrix()
        {
            return GetVectorOmMappingMatrixStorage().CreateLinMatrix(LinearProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrix<T> GetBivectorOmMappingMatrix()
        {
            return GetBivectorOmMappingMatrixStorage().CreateLinMatrix(LinearProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrix<T> GetKVectorOmMappingMatrix(uint grade)
        {
            return GetKVectorOmMappingMatrixStorage(grade).CreateLinMatrix(LinearProcessor);
        }

        public abstract IEnumerable<IndexVectorRecord<T>> GetOmMappedBasisVectors();
    }
}
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public abstract class OutermorphismBase<T> :
        IOutermorphism<T>
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
        public IUnilinearMap<T> GetAdjoint()
        {
            return GetOmAdjoint();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisScalar()
        {
            return GeometricProcessor.CreateMultivector(
                LinearProcessor.CreateKVectorStorageBasisScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisVector(ulong index)
        {
            return OmMapBasisVector(index).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisBivector(ulong index)
        {
            return OmMapBasisBivector(index).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            return OmMapBasisBivector(index1, index2).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisBlade(ulong id)
        {
            return OmMapBasisBlade(id).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> MapBasisBlade(uint grade, ulong index)
        {
            return OmMapBasisBlade(grade, index).AsMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> Map(T scalar)
        {
            return GeometricProcessor.CreateMultivector(
                scalar.CreateKVectorStorageScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> Map(Vector<T> vector)
        {
            return OmMap(vector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> Map(Bivector<T> bivector)
        {
            return OmMap(bivector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> Map(KVector<T> kVector)
        {
            return OmMap(kVector).AsMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> Map(Multivector<T> multivector)
        {
            return OmMap(multivector);
        }

        public abstract ILinMatrixStorage<T> GetMultivectorMappingMatrix();

        public abstract IEnumerable<IdMultivectorRecord<T>> GetMappedBasisBlades();
        
        public abstract IOutermorphism<T> GetOmAdjoint();
        
        public abstract Vector<T> OmMapBasisVector(ulong index);
        
        public abstract Bivector<T> OmMapBasisBivector(ulong index);
        
        public abstract Bivector<T> OmMapBasisBivector(ulong index1, ulong index2);
        
        public abstract KVector<T> OmMapBasisBlade(ulong id);
        
        public abstract KVector<T> OmMapBasisBlade(uint grade, ulong index);
        
        public abstract Vector<T> OmMap(Vector<T> vector);
        
        public abstract Bivector<T> OmMap(Bivector<T> bivector);
        
        public abstract KVector<T> OmMap(KVector<T> kVector);
        
        public abstract Multivector<T> OmMap(Multivector<T> multivector);

        public abstract ILinMatrixStorage<T> GetVectorOmMappingMatrix();
        
        public abstract ILinMatrixStorage<T> GetBivectorOmMappingMatrix();
        
        public abstract ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade);
        
        public abstract ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix();
        
        public abstract IEnumerable<IndexVectorRecord<T>> GetOmMappedBasisVectors();
    }
}
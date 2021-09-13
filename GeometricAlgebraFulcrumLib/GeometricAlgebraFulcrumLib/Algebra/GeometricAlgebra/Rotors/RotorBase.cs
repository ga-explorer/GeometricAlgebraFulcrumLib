using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public abstract class RotorBase<T>
        : IRotor<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public abstract bool IsValid();
        
        public bool IsInvalid()
        {
            return !IsValid();
        }

        protected RotorBase([NotNull] IGeometricAlgebraProcessor<T> processor)
        {
            GeometricProcessor = processor;
        }


        public abstract IRotor<T> GetReverse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IOutermorphism<T> GetOmAdjoint()
        {
            return GetReverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IUnilinearMap<T> GetAdjoint()
        {
            return GetReverse();
        }


        public abstract IMultivectorStorage<T> MapBasisScalar();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisVector(ulong index)
        {
            return MapVector(
                GeometricProcessor.CreateVectorBasisStorage(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBivector(ulong index)
        {
            return MapBivector(
                GeometricProcessor.CreateBivectorBasisStorage(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return KVectorStorage<T>.ZeroBivector;

            return MapBivector(
                index1 < index2
                    ? GeometricProcessor.CreateBivectorBasisStorage(index1, index2)
                    : GeometricProcessor.Negative(GeometricProcessor.CreateBivectorBasisStorage(index2, index1))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBlade(ulong id)
        {
            return MapKVector(
                GeometricProcessor.CreateKVectorBasisStorage(id)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> MapBasisBlade(uint grade, ulong index)
        {
            return MapKVector(
                GeometricProcessor.CreateKVectorBasisStorage(grade, index)
            );
        }


        public abstract IMultivectorStorage<T> MapScalar(T mv);

        public abstract IMultivectorStorage<T> MapVector(VectorStorage<T> mv);

        public abstract IMultivectorStorage<T> MapBivector(BivectorStorage<T> mv);

        public abstract IMultivectorStorage<T> MapMultivector(MultivectorStorage<T> mv);

        public abstract IMultivectorStorage<T> MapKVector(KVectorStorage<T> mv);

        public abstract IMultivectorStorage<T> MapMultivector(MultivectorGradedStorage<T> mv);
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> OmMapBasisVector(ulong index)
        {
            return MapBasisVector(index).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> OmMapBasisBivector(ulong index)
        {
            return MapBasisBivector(index).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            return MapBasisBivector(index1, index2).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> OmMapBasisBlade(ulong id)
        {
            return MapBasisBlade(id).GetKVectorPart(id.BasisBladeIdToGrade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return MapBasisBlade(grade, index).GetKVectorPart(grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> OmMapVector(VectorStorage<T> storage)
        {
            return MapVector(storage).GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> OmMapBivector(BivectorStorage<T> storage)
        {
            return MapBivector(storage).GetBivectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> OmMapKVector(KVectorStorage<T> storage)
        {
            return MapKVector(storage).GetKVectorPart(storage.Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> multivector)
        {
            return MapMultivector(multivector).ToMultivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> multivector)
        {
            return MapMultivector(multivector).ToGradedMultivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            return GeometricProcessor
                .GaSpaceDimension
                .GetRange()
                .Select(id => OmMapBasisBlade(id).GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetVectorOmMappingMatrix()
        {
            return ((ulong) GeometricProcessor.VSpaceDimension)
                .GetRange()
                .Select(index => OmMapBasisVector(index).GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetBivectorOmMappingMatrix()
        {
            return GeometricProcessor
                .BivectorSpaceDimension()
                .GetRange()
                .Select(index => OmMapBasisBivector(index).GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade)
        {
            return GeometricProcessor
                .KVectorSpaceDimension(grade)
                .GetRange()
                .Select(index => OmMapBasisBlade(grade, index).GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix()
        {
            var gradedMatrix = new LinMatrixListGradedStorage<T>();

            for (var grade = 0U; grade <= GeometricProcessor.VSpaceDimension; grade++)
                gradedMatrix.AppendMatrixStorage(GetKVectorOmMappingMatrix(grade));

            return gradedMatrix;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades()
        {
            return GeometricProcessor
                .GaSpaceDimension
                .GetRange()
                .Select(id => 
                    new IdMultivectorStorageRecord<T>(
                        id, 
                        OmMapBasisBlade(id)
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexVectorStorageRecord<T>> GetOmMappedBasisVectors()
        {
            return ((ulong) GeometricProcessor.VSpaceDimension)
                .GetRange()
                .Select(index => 
                    new IndexVectorStorageRecord<T>(
                        index, 
                        OmMapBasisVector(index)
                    )
                );
        }

        public abstract IMultivectorStorage<T> GetMultivectorStorage();

        public abstract IMultivectorStorage<T> GetMultivectorReverseStorage();
    }
}
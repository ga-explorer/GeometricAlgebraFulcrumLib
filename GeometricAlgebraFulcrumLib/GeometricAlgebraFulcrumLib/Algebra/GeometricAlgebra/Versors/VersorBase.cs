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
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors
{
    public abstract class VersorBase<T> :
        GaOutermorphismBase<T>,
        IVersor<T>
    {
        public override IGeometricAlgebraProcessor<T> GeometricProcessor { get; }


        protected VersorBase([NotNull] IGeometricAlgebraProcessor<T> processor)
        {
            GeometricProcessor = processor;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract IVersor<T> GetVersorInverse();

        public abstract GaMultivector<T> GetMultivector();
        
        public abstract GaMultivector<T> GetMultivectorReverse();
        
        public abstract GaMultivector<T> GetMultivectorInverse();
        
        public abstract IMultivectorStorage<T> GetMultivectorStorage();

        public abstract IMultivectorStorage<T> GetMultivectorStorageReverse();
        
        public abstract IMultivectorStorage<T> GetMultivectorStorageInverse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaOutermorphism<T> GetOmAdjoint()
        {
            return GetVersorInverse();
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
            if (index1 == index2)
                return GeometricProcessor.CreateBivectorZero();

            return OmMap(
                index1 < index2
                    ? GeometricProcessor.CreateBivectorBasis(index1, index2)
                    : -GeometricProcessor.CreateBivectorBasis(index2, index1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMapBasisBlade(ulong id)
        {
            return OmMap(
                GeometricProcessor.CreateKVectorBasis(id)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return OmMap(
                GeometricProcessor.CreateKVectorBasis(grade, index)
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetMultivectorMappingMatrixStorage()
        {
            return GeometricProcessor
                .GaSpaceDimension
                .GetRange()
                .Select(id => OmMapBasisBlade(id).KVectorStorage.GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetVectorOmMappingMatrixStorage()
        {
            return ((ulong) GeometricProcessor.VSpaceDimension)
                .GetRange()
                .Select(index => OmMapBasisVector(index).VectorStorage.GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetBivectorOmMappingMatrixStorage()
        {
            return GeometricProcessor
                .BivectorSpaceDimension()
                .GetRange()
                .Select(index => OmMapBasisBivector(index).BivectorStorage.GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetKVectorOmMappingMatrixStorage(uint grade)
        {
            return GeometricProcessor
                .KVectorSpaceDimension(grade)
                .GetRange()
                .Select(index => OmMapBasisBlade(grade, index).KVectorStorage.GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrixStorage()
        {
            var gradedMatrix = new LinMatrixListGradedStorage<T>();

            for (var grade = 0U; grade <= GeometricProcessor.VSpaceDimension; grade++)
                gradedMatrix.AppendMatrixStorage(GetKVectorOmMappingMatrixStorage(grade));

            return gradedMatrix;
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
                );
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
                );
        }


    }
}
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

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors
{
    public abstract class VersorBase<T> :
        OutermorphismBase<T>,
        IVersor<T>
    {
        public override ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }


        protected VersorBase([NotNull] IGeometricAlgebraProcessor<T> processor)
        {
            GeometricProcessor = processor;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract IVersor<T> GetVersorInverse();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IOutermorphism<T> GetOmAdjoint()
        {
            return GetVersorInverse();
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
            if (index1 == index2)
                return KVectorStorage<T>.ZeroBivector;

            return OmMapBivector(
                index1 < index2
                    ? GeometricProcessor.CreateBivectorBasisStorage(index1, index2)
                    : GeometricProcessor.Negative(GeometricProcessor.CreateBivectorBasisStorage(index2, index1))
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
        public override ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            return GeometricProcessor
                .GaSpaceDimension
                .GetRange()
                .Select(id => OmMapBasisBlade(id).GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetVectorOmMappingMatrix()
        {
            return ((ulong) GeometricProcessor.VSpaceDimension)
                .GetRange()
                .Select(index => OmMapBasisVector(index).GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetBivectorOmMappingMatrix()
        {
            return GeometricProcessor
                .BivectorSpaceDimension()
                .GetRange()
                .Select(index => OmMapBasisBivector(index).GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade)
        {
            return GeometricProcessor
                .KVectorSpaceDimension(grade)
                .GetRange()
                .Select(index => OmMapBasisBlade(grade, index).GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix()
        {
            var gradedMatrix = new LinMatrixListGradedStorage<T>();

            for (var grade = 0U; grade <= GeometricProcessor.VSpaceDimension; grade++)
                gradedMatrix.AppendMatrixStorage(GetKVectorOmMappingMatrix(grade));

            return gradedMatrix;
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
                );
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
                );
        }


        public abstract IMultivectorStorage<T> GetMultivectorStorage();

        public abstract IMultivectorStorage<T> GetMultivectorInverseStorage();
    }
}
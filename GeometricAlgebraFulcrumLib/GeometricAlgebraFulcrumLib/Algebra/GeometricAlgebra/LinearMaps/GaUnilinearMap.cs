using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps
{
    public class GaUnilinearMap<T> :
        IGaUnilinearMap<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public ILinMatrixStorage<T> MatrixStorage { get; }


        internal GaUnilinearMap([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, [NotNull] ILinMatrixStorage<T> matrixStorage)
        {
            GeometricProcessor = geometricProcessor;
            MatrixStorage = matrixStorage;
        }


        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }

        public bool IsInvalid()
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaUnilinearMap<T> GetAdjoint()
        {
            return new GaUnilinearMap<T>(
                GeometricProcessor, 
                MatrixStorage.GetTranspose()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisScalar()
        {
            return GeometricProcessor.CreateMultivector(
                MatrixStorage
                    .GetColumn(0)
                    .CreateMultivectorStorageSparse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisVector(ulong index)
        {
            return GeometricProcessor.CreateMultivector(
                MatrixStorage
                    .GetColumn(index.BasisVectorIndexToId())
                    .CreateMultivectorStorageSparse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBivector(ulong index)
        {
            return GeometricProcessor.CreateMultivector(
                MatrixStorage
                    .GetColumn(index.BasisBivectorIndexToId())
                    .CreateMultivectorStorageSparse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBivector(ulong index1, ulong index2)
        {
            if (index1 == index2)
                return GeometricProcessor.CreateMultivector(KVectorStorage<T>.ZeroScalar);

            var columnVector = MatrixStorage.GetColumn(
                BasisBivectorUtils.BasisVectorIndicesToBivectorId(index1, index2)
            );

            return GeometricProcessor.CreateMultivector(
                index1 < index2 
                    ? columnVector.CreateMultivectorStorageSparse() 
                    : LinearProcessor.Negative(columnVector).CreateMultivectorStorageSparse()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBlade(ulong id)
        {
            return GeometricProcessor.CreateMultivector(
                MatrixStorage
                    .GetColumn(id)
                    .CreateMultivectorStorageSparse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapBasisBlade(uint grade, ulong index)
        {
            var id = 
                BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

            return GeometricProcessor.CreateMultivector(
                MatrixStorage
                    .GetColumn(id)
                    .CreateMultivectorStorageSparse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Map(T mv)
        {
            return GeometricProcessor.CreateMultivector(
                LinearProcessor.Times(
                    mv,
                    MatrixStorage.GetColumn(0)
                ).CreateMultivectorStorageSparse()
            );
        }

        public GaMultivector<T> Map(GaVector<T> vector)
        {
            throw new System.NotImplementedException();
        }

        public GaMultivector<T> Map(GaBivector<T> bivector)
        {
            throw new System.NotImplementedException();
        }

        public GaMultivector<T> Map(GaKVector<T> kVector)
        {
            throw new System.NotImplementedException();
        }

        public GaMultivector<T> Map(GaMultivector<T> multivector)
        {
            throw new System.NotImplementedException();
        }

        public VectorStorage<T> OmMap(VectorStorage<T> vector)
        {
            throw new System.NotImplementedException();
        }

        public BivectorStorage<T> OmMap(BivectorStorage<T> bivector)
        {
            throw new System.NotImplementedException();
        }

        public KVectorStorage<T> OmMap(KVectorStorage<T> kVector)
        {
            throw new System.NotImplementedException();
        }

        public MultivectorStorage<T> OmMap(MultivectorStorage<T> multivector)
        {
            throw new System.NotImplementedException();
        }

        public MultivectorGradedStorage<T> OmMap(MultivectorGradedStorage<T> multivector)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetMultivectorMappingMatrixStorage()
        {
            return MatrixStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrix<T> GetMultivectorMappingMatrix()
        {
            return MatrixStorage.CreateLinMatrix(LinearProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IdMultivectorRecord<T>> GetMappedBasisBlades()
        {
            return MatrixStorage
                .GetColumns()
                .Select(r => 
                    new IdMultivectorRecord<T>(
                        r.Index, 
                        GeometricProcessor.CreateMultivector(
                            r.Storage.CreateMultivectorStorageSparse()
                        )
                    )
                );
        }
    }
}
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps
{
    public class LinUnilinearMap<T> :
        ILinUnilinearMap<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => LinearProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor { get; }

        public ILinMatrixStorage<T> MatrixStorage { get; }


        internal LinUnilinearMap([NotNull] ILinearAlgebraProcessor<T> linearProcessor, [NotNull] ILinMatrixStorage<T> matrixStorage)
        {
            LinearProcessor = linearProcessor;
            MatrixStorage = matrixStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinUnilinearMap<T> GetLinAdjoint()
        {
            return new LinUnilinearMap<T>(
                LinearProcessor, 
                MatrixStorage.GetTranspose()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> LinMapBasisVector(ulong index)
        {
            return MatrixStorage.GetColumn(index);
        }

        public ILinVectorStorage<T> LinMapVector(ILinVectorStorage<T> vectorStorage)
        {
            var composer = LinearProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in vectorStorage.GetIndexScalarRecords())
                composer.AddScaledTerms(
                    scalar,
                    LinMapBasisVector(index).GetIndexScalarRecords()
                );

            return composer.CreateLinVectorStorage();
        }

        public ILinMatrixStorage<T> LinMapMatrix(ILinMatrixStorage<T> matrixStorage)
        {
            throw new System.NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetLinMappingMatrix()
        {
            return MatrixStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetLinMappedBasisVectors()
        {
            return MatrixStorage.GetColumns();
        }
    }
}
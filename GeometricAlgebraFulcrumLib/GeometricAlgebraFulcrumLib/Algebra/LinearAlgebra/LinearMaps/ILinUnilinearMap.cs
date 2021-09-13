using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps
{
    public interface ILinUnilinearMap<T>
        : ILinearAlgebraElement<T>
    {
        ILinUnilinearMap<T> GetLinAdjoint();


        ILinVectorStorage<T> LinMapBasisVector(ulong index);

        ILinVectorStorage<T> LinMapVector(ILinVectorStorage<T> vectorStorage);

        ILinMatrixStorage<T> LinMapMatrix(ILinMatrixStorage<T> matrixStorage);


        ILinMatrixStorage<T> GetLinMappingMatrix();


        IEnumerable<IndexLinVectorStorageRecord<T>> GetLinMappedBasisVectors();
    }
}
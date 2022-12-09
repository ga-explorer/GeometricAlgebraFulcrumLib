using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps
{
    public interface IGaUnilinearMap<T> : 
        IGeometricAlgebraElement<T>
    {
        bool IsValid();

        bool IsInvalid();

        IGaUnilinearMap<T> GetAdjoint();


        GaMultivector<T> MapBasisScalar();

        GaMultivector<T> MapBasisVector(ulong index);

        GaMultivector<T> MapBasisBivector(ulong index);

        GaMultivector<T> MapBasisBivector(ulong index1, ulong index2);

        GaMultivector<T> MapBasisBlade(ulong id);

        GaMultivector<T> MapBasisBlade(uint grade, ulong index);


        GaMultivector<T> Map(T mv);

        GaMultivector<T> Map(GaVector<T> vector);

        GaMultivector<T> Map(GaBivector<T> bivector);

        GaMultivector<T> Map(GaKVector<T> kVector);


        GaMultivector<T> Map(GaMultivector<T> multivector);


        //IndexPairRecord GetMultivectorMappingMatrixSize();

        ILinMatrixStorage<T> GetMultivectorMappingMatrixStorage();

        LinMatrix<T> GetMultivectorMappingMatrix();

        IEnumerable<IdMultivectorRecord<T>> GetMappedBasisBlades();
    }
}
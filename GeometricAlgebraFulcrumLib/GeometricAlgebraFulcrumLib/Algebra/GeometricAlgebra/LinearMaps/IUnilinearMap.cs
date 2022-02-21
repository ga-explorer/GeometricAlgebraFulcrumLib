using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps
{
    public interface IUnilinearMap<T> : 
        IGeometricAlgebraElement<T>
    {
        bool IsValid();

        bool IsInvalid();

        IUnilinearMap<T> GetAdjoint();


        Multivector<T> MapBasisScalar();

        Multivector<T> MapBasisVector(ulong index);

        Multivector<T> MapBasisBivector(ulong index);

        Multivector<T> MapBasisBivector(ulong index1, ulong index2);

        Multivector<T> MapBasisBlade(ulong id);

        Multivector<T> MapBasisBlade(uint grade, ulong index);


        Multivector<T> Map(T mv);

        Multivector<T> Map(Vector<T> vector);

        Multivector<T> Map(Bivector<T> bivector);

        Multivector<T> Map(KVector<T> kVector);

        Multivector<T> Map(Multivector<T> multivector);


        //IndexPairRecord GetMultivectorMappingMatrixSize();

        ILinMatrixStorage<T> GetMultivectorMappingMatrix();

        IEnumerable<IdMultivectorRecord<T>> GetMappedBasisBlades();
    }
}
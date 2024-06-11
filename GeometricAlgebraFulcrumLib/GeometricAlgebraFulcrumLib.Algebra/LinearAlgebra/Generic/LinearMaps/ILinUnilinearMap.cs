using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;

public interface ILinUnilinearMap<T> :
    IAlgebraicElement
{
    IScalarProcessor<T> ScalarProcessor { get; }

    int VSpaceDimensions { get; }
        

    LinVector<T> MapBasisVector(int index);

    LinVector<T> Map(LinVector<T> vector);

    IEnumerable<KeyValuePair<int, LinVector<T>>> GetMappedBasisVectors(int vSpaceDimensions);

    T[,] ToArray(int rowCount, int colCount);

    LinUnilinearMap<T> ToUnilinearMap(int vSpaceDimensions);
}
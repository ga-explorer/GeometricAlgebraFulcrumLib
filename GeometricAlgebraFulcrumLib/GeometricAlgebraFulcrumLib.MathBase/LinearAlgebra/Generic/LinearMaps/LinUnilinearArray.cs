using System.Collections;
using DataStructuresLib.Collections;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

public class LinUnilinearArray<T> :
    IReadOnlyList2D<T>
{
    private readonly T[,] _array;


    public IScalarProcessor<T> ScalarProcessor { get; }

    public int Count 
        => Count1 * Count2;

    public int Count1 
        => _array.GetLength(0);

    public int Count2 
        => _array.GetLength(1);
    
    public T this[int index] 
        => throw new NotImplementedException();

    public T this[int index1, int index2] 
        => throw new NotImplementedException();

    
    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public LinUnilinearMap<T> GetAdjoint()
    {
        throw new NotImplementedException();
    }

    public LinVector<T> MapBasisVector(int index)
    {
        throw new NotImplementedException();
    }

    public LinVector<T> Map(LinVector<T> vector)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<int, LinVector<T>>> GetMappedBasisVectors(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    public T[,] GetMapArray(int rowCount, int colCount)
    {
        throw new NotImplementedException();
    }

    
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;

public sealed class LinFloat64MatrixLinearMap :
    ILinFloat64UnilinearMap
{
    private readonly double[,] _mapArray;

    public int VSpaceDimensions
        => _mapArray.GetLength(0);

    public double Determinant { get; }

    public bool SwapsHandedness
        => Determinant < 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64MatrixLinearMap(double[,] mapArray)
    {
        _mapArray = mapArray;
        Determinant = _mapArray.ToMatrix().Determinant();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _mapArray.GetItems().All(s => s.IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector MapBasisVector(int basisIndex)
    {
        return _mapArray.ColumnToArray1D(basisIndex).CreateLinVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector MapVector(Float64Vector vector)
    {
        return _mapArray.MatrixProduct(vector).CreateLinVector();
    }

    public IEnumerable<KeyValuePair<int, Float64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    public double[,] ToArray(int rowCount, int colCount)
    {
        throw new NotImplementedException();
    }

    public Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        throw new NotImplementedException();
    }

    public LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    public double[,] ToArray()
    {
        var array = new double[VSpaceDimensions, VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        for (var i = 0; i < VSpaceDimensions; i++)
            array[i, j] = _mapArray[i, j];

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix<double> ToMatrix()
    {
        return _mapArray.ToMatrix();
    }

    public bool IsNearReflection(double epsilon = 1E-12)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64UnilinearMap GetInverseMap()
    {
        return new LinFloat64MatrixLinearMap(
            _mapArray.ToMatrix().Inverse().ToArray()
        );
    }

    public bool IsIdentity()
    {
        for (var i = 0; i < VSpaceDimensions; i++)
        {
            for (var j = 0; j < i; j++)
                if (!_mapArray[i, j].IsZero())
                    return false;

            if (!_mapArray[i, i].IsOne())
                return false;

            for (var j = i + 1; j < VSpaceDimensions; j++)
                if (!_mapArray[i, j].IsZero())
                    return false;
        }

        return true;
    }

    public bool IsReflection()
    {
        throw new NotImplementedException();
    }

    public bool IsNearIdentity(double epsilon = 1E-12)
    {
        for (var i = 0; i < VSpaceDimensions; i++)
        {
            for (var j = 0; j < i; j++)
                if (!_mapArray[i, j].IsNearZero(epsilon))
                    return false;

            if (!_mapArray[i, i].IsNearOne(epsilon))
                return false;

            for (var j = i + 1; j < VSpaceDimensions; j++)
                if (!_mapArray[i, j].IsNearZero(epsilon))
                    return false;
        }

        return true;
    }
}
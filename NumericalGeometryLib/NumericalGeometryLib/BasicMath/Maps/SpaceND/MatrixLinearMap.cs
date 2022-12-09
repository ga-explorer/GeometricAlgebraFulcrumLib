using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND;

public sealed class MatrixLinearOperator :
    ILinearMap
{
    private readonly double[,] _mapArray;

    public int Dimensions 
        => _mapArray.GetLength(0);

    public double Determinant { get; }

    public bool SwapsHandedness 
        => Determinant < 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MatrixLinearOperator(double[,] mapArray)
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
    public Float64Tuple MapVectorBasis(int basisIndex)
    {
        return Float64Tuple.Create(
            _mapArray.ColumnToArray1D(basisIndex)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Tuple MapVector(Float64Tuple vector)
    {
        return Float64Tuple.Create(
            _mapArray.MatrixProduct(vector.ScalarArray)
        );
    }
    
    public double[,] GetArray()
    {
        var array = new double[Dimensions, Dimensions];

        for (var j = 0; j < Dimensions; j++)
        for (var i = 0; i < Dimensions; i++) 
            array[i, j] = _mapArray[i, j];

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix<double> GetMatrix()
    {
        return _mapArray.ToMatrix();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinearMap GetLinearMapInverse()
    {
        return new MatrixLinearOperator(
            _mapArray.ToMatrix().Inverse().ToArray()
        );
    }

    public bool IsIdentity()
    {
        for (var i = 0; i < Dimensions; i++)
        {
            for (var j = 0; j < i; j++)
                if (!_mapArray[i, j].IsExactZero())
                    return false;

            if (!_mapArray[i, i].IsExactOne())
                return false;

            for (var j = i + 1; j < Dimensions; j++)
                if (!_mapArray[i, j].IsExactZero())
                    return false;
        }

        return true;
    }

    public bool IsNearIdentity(double epsilon = 1E-12)
    {
        for (var i = 0; i < Dimensions; i++)
        {
            for (var j = 0; j < i; j++)
                if (!_mapArray[i, j].IsNearZero(epsilon))
                    return false;

            if (!_mapArray[i, i].IsNearOne(epsilon))
                return false;

            for (var j = i + 1; j < Dimensions; j++)
                if (!_mapArray[i, j].IsNearZero(epsilon))
                    return false;
        }

        return true;
    }
}
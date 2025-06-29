﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;

public sealed class LinFloat64MatrixLinearMap :
    ILinFloat64UnilinearMap
{
    private readonly double[,] _mapArray;

    public int VSpaceDimensions
        => _mapArray.GetLength(0);

    public double Determinant { get; }

    public bool SwapsHandedness
        => Determinant < 0;


    
    private LinFloat64MatrixLinearMap(double[,] mapArray)
    {
        _mapArray = mapArray;
        Determinant = _mapArray.ToMatrix().Determinant();
    }


    
    public bool IsValid()
    {
        return _mapArray.GetItems().All(s => s.IsValid());
    }


    
    public LinFloat64Vector MapBasisVector(int basisIndex)
    {
        return _mapArray.ColumnToArray1D(basisIndex).CreateLinVector();
    }

    
    public LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        return _mapArray.MatrixProduct(vector).CreateLinVector();
    }

    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
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

    
    public Matrix<double> ToMatrix()
    {
        return _mapArray.ToMatrix();
    }

    public bool IsNearReflection(double zeroEpsilon = 1E-12)
    {
        throw new NotImplementedException();
    }

    
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

    public bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        for (var i = 0; i < VSpaceDimensions; i++)
        {
            for (var j = 0; j < i; j++)
                if (!_mapArray[i, j].IsNearZero(zeroEpsilon))
                    return false;

            if (!_mapArray[i, i].IsNearOne(zeroEpsilon))
                return false;

            for (var j = i + 1; j < VSpaceDimensions; j++)
                if (!_mapArray[i, j].IsNearZero(zeroEpsilon))
                    return false;
        }

        return true;
    }
}
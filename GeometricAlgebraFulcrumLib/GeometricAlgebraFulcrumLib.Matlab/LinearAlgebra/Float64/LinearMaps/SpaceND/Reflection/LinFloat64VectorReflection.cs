using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;

public sealed class LinFloat64VectorReflection :
    ILinFloat64UnilinearMap
{
    public LinFloat64Vector ReflectionVector { get; }

    public int VSpaceDimensions { get; }

    public bool SwapsHandedness
        => true;


    
    public LinFloat64VectorReflection(int dimensions, LinFloat64Vector vector)
    {
        Debug.Assert(vector.IsNearUnit());

        VSpaceDimensions = dimensions;
        ReflectionVector = vector;
    }


    
    public bool IsValid()
    {
        return ReflectionVector.IsValid();
    }

    
    public bool IsIdentity()
    {
        return false;
    }

    public bool IsReflection()
    {
        throw new NotImplementedException();
    }

    
    public bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        return false;
    }

    public bool IsNearReflection(double zeroEpsilon = 1E-12)
    {
        throw new NotImplementedException();
    }

    
    public LinFloat64Vector MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        var s = 2d * ReflectionVector[basisIndex];

        return LinFloat64VectorComposer
            .Create()
            .SetVector(ReflectionVector, s)
            .SubtractTerm(basisIndex, 1d)
            .GetVector();
    }

    
    public LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        var s = 2d * Float64ArrayUtils.VectorDot(vector, ReflectionVector);

        return LinFloat64VectorComposer
            .Create()
            .SetVector(ReflectionVector, s)
            .SubtractVector(vector)
            .GetVector();
    }

    public IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetMappedBasisVectors(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    
    public Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        var columnList =
            colCount
                .GetRange()
                .Select(i => MapBasisVector(i).ToArray(rowCount));

        return Matrix<double>
            .Build
            .DenseOfColumnArrays(columnList);
    }

    public LinFloat64UnilinearMap ToUnilinearMap(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    public double[,] ToArray(int rowCount, int colCount)
    {
        var array = new double[rowCount, colCount];

        for (var j = 0; j < colCount; j++)
        {
            var columnVector = MapBasisVector(j);

            if (columnVector.IsZero)
                continue;

            foreach (var (i, s) in columnVector.ToTuples())
                array[i, j] = s;
        }

        return array;
    }

    
    public LinFloat64VectorReflection GetVectorReflectionInverse()
    {
        return this;
    }

    
    public ILinFloat64UnilinearMap GetInverseMap()
    {
        return GetVectorReflectionInverse();
    }
}
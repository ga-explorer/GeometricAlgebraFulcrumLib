using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;

public sealed class LinFloat64VectorReflection :
    ILinFloat64UnilinearMap
{
    public LinFloat64Vector ReflectionVector { get; }

    public int VSpaceDimensions { get; }

    public bool SwapsHandedness
        => true;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorReflection(int dimensions, LinFloat64Vector vector)
    {
        Debug.Assert(vector.IsNearUnit());

        VSpaceDimensions = dimensions;
        ReflectionVector = vector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ReflectionVector.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        return false;
    }

    public bool IsReflection()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        return false;
    }

    public bool IsNearReflection(double zeroEpsilon = 1E-12)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

            foreach (var (i, s) in columnVector)
                array[i, j] = s;
        }

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorReflection GetVectorReflectionInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64UnilinearMap GetInverseMap()
    {
        return GetVectorReflectionInverse();
    }
}